// File: QuanLyBanSach.DAL/KhuyenMaiDAL.cs
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.DAL;

/// <summary>
/// Truy cập bảng KhuyenMai (ĐT3).
/// </summary>
public class KhuyenMaiDAL
{
    private const string CotKhuyenMai = @"km.MaKM, km.TenKM, km.LoaiKM, km.NguongTien, km.MaLoai, l.TenLoai,
                                          km.NguongSoLuong, km.PhanTramGiam, km.SoTienGiam,
                                          km.NgayBatDau, km.NgayKetThuc, km.TrangThai";

    /// <summary>Lấy toàn bộ khuyến mãi: đang bật trước, sau đó theo ngày kết thúc mới nhất.</summary>
    public List<KhuyenMai> LayDanhSach()
    {
        string sql = $@"SELECT {CotKhuyenMai}
                        FROM dbo.KhuyenMai AS km LEFT JOIN dbo.LoaiSach AS l ON l.MaLoai = km.MaLoai
                        ORDER BY km.TrangThai DESC, km.NgayKetThuc DESC, km.MaKM";
        return Database.ExecuteReader(sql, DocKhuyenMai);
    }

    /// <summary>Lấy một khuyến mãi theo mã; null nếu không có.</summary>
    public KhuyenMai? LayTheoMa(int maKM)
    {
        string sql = $@"SELECT {CotKhuyenMai}
                        FROM dbo.KhuyenMai AS km LEFT JOIN dbo.LoaiSach AS l ON l.MaLoai = km.MaLoai
                        WHERE km.MaKM = @MaKM";
        return Database.ExecuteReader(sql, DocKhuyenMai, CommandType.Text,
            Database.TaoThamSo("@MaKM", SqlDbType.Int, maKM)).FirstOrDefault();
    }

    /// <summary>
    /// Các khuyến mãi đang bật và còn hiệu lực tại ngày bán (KM-02) – sp_KhuyenMaiHieuLuc.
    /// BLL tính tiền giảm của từng cái và chọn cái giảm nhiều nhất (KM-01).
    /// </summary>
    public List<KhuyenMai> LayHieuLuc(DateTime ngay)
    {
        return Database.ExecuteReader("sp_KhuyenMaiHieuLuc", DocKhuyenMai, CommandType.StoredProcedure,
            Database.TaoThamSo("@Ngay", SqlDbType.Date, ngay.Date));
    }

    /// <summary>Thêm khuyến mãi; trả về MaKM mới.</summary>
    public int Them(KhuyenMai km)
    {
        const string sql = @"INSERT INTO dbo.KhuyenMai (TenKM, LoaiKM, NguongTien, MaLoai, NguongSoLuong,
                                                        PhanTramGiam, SoTienGiam, NgayBatDau, NgayKetThuc, TrangThai)
                             OUTPUT INSERTED.MaKM
                             VALUES (@TenKM, @LoaiKM, @NguongTien, @MaLoai, @NguongSoLuong,
                                     @PhanTramGiam, @SoTienGiam, @NgayBatDau, @NgayKetThuc, @TrangThai)";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text, ThamSoKhuyenMai(km, coMaKM: false));
        return Convert.ToInt32(kq);
    }

    /// <summary>Sửa khuyến mãi theo MaKM.</summary>
    public bool Sua(KhuyenMai km)
    {
        const string sql = @"UPDATE dbo.KhuyenMai
                             SET TenKM = @TenKM, LoaiKM = @LoaiKM, NguongTien = @NguongTien, MaLoai = @MaLoai,
                                 NguongSoLuong = @NguongSoLuong, PhanTramGiam = @PhanTramGiam,
                                 SoTienGiam = @SoTienGiam, NgayBatDau = @NgayBatDau,
                                 NgayKetThuc = @NgayKetThuc, TrangThai = @TrangThai
                             WHERE MaKM = @MaKM";
        return Database.ExecuteNonQuery(sql, CommandType.Text, ThamSoKhuyenMai(km, coMaKM: true)) > 0;
    }

    /// <summary>Bật (true) hoặc tắt (false) khuyến mãi (KM-04: đã dùng thì chỉ được tắt).</summary>
    public bool DoiTrangThai(int maKM, bool dangBat)
    {
        const string sql = "UPDATE dbo.KhuyenMai SET TrangThai = @TrangThai WHERE MaKM = @MaKM";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@TrangThai", SqlDbType.Bit, dangBat),
            Database.TaoThamSo("@MaKM", SqlDbType.Int, maKM)) > 0;
    }

    /// <summary>Khuyến mãi đã được áp dụng vào hóa đơn nào chưa (KM-04).</summary>
    public bool DaDuocApDung(int maKM)
    {
        const string sql = "SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.HoaDon WHERE MaKM = @MaKM) THEN 1 ELSE 0 END";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@MaKM", SqlDbType.Int, maKM));
        return Convert.ToInt32(kq ?? 0) == 1;
    }

    /// <summary>Xóa khuyến mãi (BLL kiểm tra DaDuocApDung = false trước – KM-04).</summary>
    public bool Xoa(int maKM)
    {
        const string sql = "DELETE FROM dbo.KhuyenMai WHERE MaKM = @MaKM";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@MaKM", SqlDbType.Int, maKM)) > 0;
    }

    /// <summary>Tạo bộ tham số của KhuyenMai; thêm @MaKM khi sửa.</summary>
    private static SqlParameter[] ThamSoKhuyenMai(KhuyenMai km, bool coMaKM)
    {
        var ds = new List<SqlParameter>
        {
            Database.TaoThamSo("@TenKM", SqlDbType.NVarChar, km.TenKM, 100),
            Database.TaoThamSo("@LoaiKM", SqlDbType.TinyInt, (byte)km.LoaiKM),
            Database.TaoThamSo("@NguongTien", SqlDbType.Decimal, km.NguongTien),
            Database.TaoThamSo("@MaLoai", SqlDbType.Int, km.MaLoai),
            Database.TaoThamSo("@NguongSoLuong", SqlDbType.Int, km.NguongSoLuong),
            Database.TaoThamSo("@PhanTramGiam", SqlDbType.TinyInt,
                km.PhanTramGiam.HasValue ? (byte)km.PhanTramGiam.Value : null),
            Database.TaoThamSo("@SoTienGiam", SqlDbType.Decimal, km.SoTienGiam),
            Database.TaoThamSo("@NgayBatDau", SqlDbType.Date, km.NgayBatDau.Date),
            Database.TaoThamSo("@NgayKetThuc", SqlDbType.Date, km.NgayKetThuc.Date),
            Database.TaoThamSo("@TrangThai", SqlDbType.Bit, km.TrangThai)
        };

        if (coMaKM)
        {
            ds.Add(Database.TaoThamSo("@MaKM", SqlDbType.Int, km.MaKM));
        }

        return ds.ToArray();
    }

    /// <summary>Ánh xạ một dòng thành KhuyenMai (cột TenLoai lấy từ LEFT JOIN LoaiSach).</summary>
    private static KhuyenMai DocKhuyenMai(SqlDataReader r) => new()
    {
        MaKM = r.GetIntOrZero("MaKM"),
        TenKM = r.GetStringOrEmpty("TenKM"),
        LoaiKM = (LoaiKhuyenMai)(byte)r.GetIntOrZero("LoaiKM"),
        NguongTien = r.GetDecimalOrNull("NguongTien"),
        MaLoai = r.GetIntOrNull("MaLoai"),
        TenLoaiSach = r.GetStringOrNull("TenLoai"),
        NguongSoLuong = r.GetIntOrNull("NguongSoLuong"),
        PhanTramGiam = r.GetIntOrNull("PhanTramGiam"),
        SoTienGiam = r.GetDecimalOrNull("SoTienGiam"),
        NgayBatDau = r.GetDateTimeOrMin("NgayBatDau"),
        NgayKetThuc = r.GetDateTimeOrMin("NgayKetThuc"),
        TrangThai = r.GetBoolOrFalse("TrangThai")
    };
}
