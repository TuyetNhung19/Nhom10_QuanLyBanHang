// File: QuanLyBanSach.DAL/HoaDonDAL.cs
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.DAL;

/// <summary>
/// Truy cập hóa đơn: sinh mã, lập, hủy, tra cứu, xem chi tiết và dữ liệu gợi ý mua kèm (ĐT4).
/// </summary>
public class HoaDonDAL
{
    /// <summary>Tên kiểu bảng trong CSDL dùng truyền danh sách chi tiết vào sp_LapHoaDon.</summary>
    private const string KieuBangChiTiet = "dbo.ChiTietHoaDonType";

    /// <summary>
    /// Mã hóa đơn dự kiến của ngày (hiển thị khi mở FrmBanHang) – sp_TaoMaHoaDon, đọc tham số OUTPUT.
    /// Mã chính thức được sinh lại trong sp_LapHoaDon lúc lưu (HD-07).
    /// </summary>
    public string TaoMaHoaDonDuKien(DateTime ngay)
    {
        SqlParameter maHD = Database.TaoThamSoOutput("@MaHD", SqlDbType.VarChar, 20);
        Database.ExecuteNonQuery("sp_TaoMaHoaDon", CommandType.StoredProcedure,
            Database.TaoThamSo("@Ngay", SqlDbType.Date, ngay.Date),
            maHD);
        return Convert.ToString(maHD.Value) ?? string.Empty;
    }

    /// <summary>
    /// Lưu hóa đơn bằng sp_LapHoaDon và trả về mã hóa đơn đã lưu.
    /// Toàn bộ việc kiểm tra tồn, lấy đơn giá hiện hành, ghi HoaDon + ChiTietHoaDon và trừ tồn
    /// nằm trong MỘT transaction bên trong stored procedure (SET XACT_ABORT ON, lỗi thì ROLLBACK –
    /// HD-05), nên DAL không mở transaction riêng. Lỗi nghiệp vụ được SP THROW mã 50001–50008
    /// và để nguyên SqlException cho BLL xử lý.
    /// </summary>
    /// <param name="hd">Hóa đơn: MaTK, MaKH, MaKM, GiamGia, TamTinh (tạm tính đang hiển thị),
    /// TienKhachDua, GhiChu và danh sách ChiTiet (MaSach, SoLuong).</param>
    public string LapHoaDon(HoaDon hd)
    {
        // Table-valued parameter: 2 cột đúng thứ tự của dbo.ChiTietHoaDonType.
        // Gộp các dòng trùng mã sách vì kiểu bảng có khóa chính MaSach.
        var bangChiTiet = new DataTable();
        bangChiTiet.Columns.Add("MaSach", typeof(string));
        bangChiTiet.Columns.Add("SoLuong", typeof(int));
        foreach (var nhom in hd.ChiTiet.GroupBy(ct => ct.MaSach))
        {
            bangChiTiet.Rows.Add(nhom.Key, nhom.Sum(ct => ct.SoLuong));
        }

        SqlParameter maHD = Database.TaoThamSoOutput("@MaHD", SqlDbType.VarChar, 20);

        Database.ExecuteNonQuery("sp_LapHoaDon", CommandType.StoredProcedure,
            Database.TaoThamSo("@MaTK", SqlDbType.Int, hd.MaTK),
            Database.TaoThamSo("@MaKH", SqlDbType.Int, hd.MaKH),
            Database.TaoThamSo("@MaKM", SqlDbType.Int, hd.MaKM),
            Database.TaoThamSo("@GiamGia", SqlDbType.Decimal, hd.GiamGia),
            Database.TaoThamSo("@TamTinhDuKien", SqlDbType.Decimal, hd.TamTinh),
            Database.TaoThamSo("@TienKhachDua", SqlDbType.Decimal, hd.TienKhachDua),
            Database.TaoThamSo("@GhiChu", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(hd.GhiChu) ? null : hd.GhiChu, 255),
            Database.TaoThamSoBang("@ChiTiet", KieuBangChiTiet, bangChiTiet),
            maHD);

        return Convert.ToString(maHD.Value) ?? string.Empty;
    }

    /// <summary>
    /// Hủy hóa đơn và hoàn tồn – sp_HuyHoaDon (chỉ Quản lý, bắt buộc lý do – HD-06; lỗi THROW 50007).
    /// </summary>
    public void HuyHoaDon(string maHD, int maTK, string lyDo)
    {
        Database.ExecuteNonQuery("sp_HuyHoaDon", CommandType.StoredProcedure,
            Database.TaoThamSo("@MaHD", SqlDbType.VarChar, maHD, 20),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK),
            Database.TaoThamSo("@LyDo", SqlDbType.NVarChar, lyDo, 255));
    }

    /// <summary>
    /// Tra cứu hóa đơn – sp_TraCuuHoaDon. Khi có <paramref name="maHD"/> thì SP bỏ qua khoảng ngày.
    /// </summary>
    /// <param name="maHD">Một phần mã hóa đơn; null = không lọc.</param>
    /// <param name="tuKhoaKhach">Tên hoặc SĐT khách; null = không lọc.</param>
    /// <param name="tuNgay">Từ ngày.</param>
    /// <param name="denNgay">Đến ngày.</param>
    /// <param name="maTK">Lọc thu ngân (thu ngân chỉ xem của mình); null = tất cả.</param>
    /// <param name="trangThai">1 hoàn tất, 2 đã hủy; null = tất cả.</param>
    public List<HoaDon> TraCuu(string? maHD, string? tuKhoaKhach, DateTime tuNgay, DateTime denNgay,
        int? maTK, byte? trangThai)
    {
        return Database.ExecuteReader("sp_TraCuuHoaDon", DocHoaDon, CommandType.StoredProcedure,
            Database.TaoThamSo("@MaHD", SqlDbType.VarChar, string.IsNullOrWhiteSpace(maHD) ? null : maHD.Trim(), 20),
            Database.TaoThamSo("@TuKhoaKhach", SqlDbType.NVarChar,
                string.IsNullOrWhiteSpace(tuKhoaKhach) ? null : tuKhoaKhach.Trim(), 100),
            Database.TaoThamSo("@TuNgay", SqlDbType.Date, tuNgay.Date),
            Database.TaoThamSo("@DenNgay", SqlDbType.Date, denNgay.Date),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK),
            Database.TaoThamSo("@TrangThai", SqlDbType.TinyInt, trangThai));
    }

    /// <summary>
    /// Lấy đầy đủ một hóa đơn kèm chi tiết để xem và in lại – sp_LayChiTietHoaDon
    /// (result set 1: thông tin chung, result set 2: các dòng). Null nếu không có hóa đơn.
    /// </summary>
    public HoaDon? LayChiTiet(string maHD)
    {
        HoaDon? hd = null;

        Database.ExecuteReaderNhieuKetQua("sp_LayChiTietHoaDon", reader =>
        {
            if (reader.Read())
            {
                hd = DocHoaDon(reader);
            }

            if (hd != null && reader.NextResult())
            {
                while (reader.Read())
                {
                    hd.ChiTiet.Add(new ChiTietHoaDon
                    {
                        Stt = reader.GetIntOrZero("STT"),
                        MaHD = hd.MaHD,
                        MaSach = reader.GetStringOrEmpty("MaSach"),
                        TenSach = reader.GetStringOrEmpty("TenSach"),
                        SoLuong = reader.GetIntOrZero("SoLuong"),
                        DonGia = reader.GetDecimalOrZero("DonGia")
                    });
                }
            }
        }, CommandType.StoredProcedure,
            Database.TaoThamSo("@MaHD", SqlDbType.VarChar, maHD, 20));

        return hd;
    }

    /// <summary>
    /// ĐT4 – mọi dòng chi tiết thuộc các hóa đơn HOÀN TẤT có chứa <paramref name="maSach"/>,
    /// kèm tồn kho và trạng thái hiện tại của từng sách. BLL dùng LINQ GroupBy để tìm
    /// sách thường được mua kèm. DonGia ở đây là GIÁ BÁN HIỆN HÀNH để hiển thị gợi ý.
    /// </summary>
    public List<ChiTietHoaDon> LayDongCuaHoaDonCoSach(string maSach)
    {
        const string sql = @"SELECT ct.MaHD, ct.MaSach, s.TenSach, s.MaLoai, ct.SoLuong,
                                    s.GiaBan AS DonGia, s.SoLuongTon, s.TrangThai
                             FROM dbo.ChiTietHoaDon AS ct
                             INNER JOIN dbo.HoaDon AS hd ON hd.MaHD = ct.MaHD AND hd.TrangThai = 1
                             INNER JOIN dbo.Sach   AS s  ON s.MaSach = ct.MaSach
                             WHERE ct.MaHD IN (SELECT x.MaHD FROM dbo.ChiTietHoaDon AS x WHERE x.MaSach = @MaSach)";
        return Database.ExecuteReader(sql, r => new ChiTietHoaDon
        {
            MaHD = r.GetStringOrEmpty("MaHD"),
            MaSach = r.GetStringOrEmpty("MaSach"),
            TenSach = r.GetStringOrEmpty("TenSach"),
            MaLoai = r.GetIntOrZero("MaLoai"),
            SoLuong = r.GetIntOrZero("SoLuong"),
            DonGia = r.GetDecimalOrZero("DonGia"),
            SoLuongTon = r.GetIntOrZero("SoLuongTon"),
            SachDangBan = r.GetBoolOrFalse("TrangThai")
        }, CommandType.Text, Database.TaoThamSo("@MaSach", SqlDbType.VarChar, maSach, 20));
    }

    /// <summary>
    /// ĐT4 – top sách thường được mua kèm (đang bán, còn tồn) – sp_SachMuaKem.
    /// </summary>
    public List<GoiYSach> SachMuaKem(string maSach, int top)
    {
        return Database.ExecuteReader("sp_SachMuaKem", r => new GoiYSach
        {
            MaSach = r.GetStringOrEmpty("MaSach"),
            TenSach = r.GetStringOrEmpty("TenSach"),
            GiaBan = r.GetDecimalOrZero("GiaBan"),
            SoLuongTon = r.GetIntOrZero("SoLuongTon"),
            SoLanMuaCung = r.GetIntOrZero("SoHoaDonChung")
        }, CommandType.StoredProcedure,
            Database.TaoThamSo("@MaSach", SqlDbType.VarChar, maSach, 20),
            Database.TaoThamSo("@Top", SqlDbType.Int, top));
    }

    /// <summary>
    /// Ánh xạ một dòng thành HoaDon (không kèm chi tiết). Dùng chung cho sp_TraCuuHoaDon,
    /// sp_LayChiTietHoaDon và KhachHangDAL.LayLichSuHoaDon – cột nào không có thì để mặc định.
    /// </summary>
    internal static HoaDon DocHoaDon(SqlDataReader r)
    {
        var hd = new HoaDon
        {
            MaHD = r.GetStringOrEmpty("MaHD"),
            NgayLap = r.GetDateTimeOrMin("NgayLap"),
            TongThanhToan = r.GetDecimalOrZero("TongThanhToan")
        };

        if (r.CoCot("MaTK")) hd.MaTK = r.GetIntOrZero("MaTK");
        if (r.CoCot("MaKH")) hd.MaKH = r.GetIntOrNull("MaKH");
        if (r.CoCot("TamTinh")) hd.TamTinh = r.GetDecimalOrZero("TamTinh");
        if (r.CoCot("GiamGia")) hd.GiamGia = r.GetDecimalOrZero("GiamGia");
        if (r.CoCot("MaKM")) hd.MaKM = r.GetIntOrNull("MaKM");
        if (r.CoCot("TienKhachDua")) hd.TienKhachDua = r.GetDecimalOrZero("TienKhachDua");
        if (r.CoCot("TienThoi")) hd.TienThoi = r.GetDecimalOrZero("TienThoi");
        if (r.CoCot("TrangThai")) hd.TrangThai = (TrangThaiHoaDon)(byte)r.GetIntOrZero("TrangThai");
        if (r.CoCot("LyDoHuy")) hd.LyDoHuy = r.GetStringOrNull("LyDoHuy");
        if (r.CoCot("GhiChu")) hd.GhiChu = r.GetStringOrNull("GhiChu");
        if (r.CoCot("TenThuNgan")) hd.TenThuNgan = r.GetStringOrEmpty("TenThuNgan");
        if (r.CoCot("TenKhach")) hd.TenKhach = r.GetStringOrNull("TenKhach") ?? "Khách lẻ";
        if (r.CoCot("SDT")) hd.SDTKhach = r.GetStringOrNull("SDT");
        if (r.CoCot("TenKM")) hd.TenKM = r.GetStringOrNull("TenKM");

        return hd;
    }
}
