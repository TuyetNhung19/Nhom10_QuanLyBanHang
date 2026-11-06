// File: QuanLyBanSach.DAL/KhachHangDAL.cs
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.DAL;

/// <summary>
/// Truy cập bảng KhachHang và lịch sử mua của khách (ĐT4).
/// </summary>
public class KhachHangDAL
{
    private const string CotKhachHang = "MaKH, HoTen, SDT, Email, DiaChi";

    /// <summary>Lấy toàn bộ khách hàng, sắp theo họ tên.</summary>
    public List<KhachHang> LayDanhSach()
    {
        string sql = $"SELECT {CotKhachHang} FROM dbo.KhachHang ORDER BY HoTen";
        return Database.ExecuteReader(sql, DocKhachHang);
    }

    /// <summary>
    /// Tìm khách theo mã (khớp chính xác khi từ khóa là số), họ tên (không phân biệt dấu) hoặc một phần SĐT.
    /// </summary>
    /// <param name="tuKhoa">Từ khóa; rỗng = tất cả.</param>
    public List<KhachHang> TimKiem(string? tuKhoa)
    {
        int? maKH = int.TryParse(tuKhoa?.Trim(), out int so) ? so : null;

        string sql = $@"SELECT {CotKhachHang} FROM dbo.KhachHang
                        WHERE @Mau IS NULL
                           OR MaKH = @MaKH
                           OR HoTen COLLATE Vietnamese_CI_AI LIKE @Mau
                           OR SDT LIKE @Mau
                        ORDER BY HoTen";
        return Database.ExecuteReader(sql, DocKhachHang, CommandType.Text,
            Database.TaoThamSo("@Mau", SqlDbType.NVarChar, TaoMauTimKiem(tuKhoa), 400),
            Database.TaoThamSo("@MaKH", SqlDbType.Int, maKH));
    }

    /// <summary>Lấy khách theo mã; null nếu không có.</summary>
    public KhachHang? LayTheoMa(int maKH)
    {
        string sql = $"SELECT {CotKhachHang} FROM dbo.KhachHang WHERE MaKH = @MaKH";
        return Database.ExecuteReader(sql, DocKhachHang, CommandType.Text,
            Database.TaoThamSo("@MaKH", SqlDbType.Int, maKH)).FirstOrDefault();
    }

    /// <summary>Lấy khách theo SĐT (khớp chính xác) – dùng khi chọn khách lúc bán hàng.</summary>
    public KhachHang? LayTheoSDT(string sdt)
    {
        string sql = $"SELECT {CotKhachHang} FROM dbo.KhachHang WHERE SDT = @SDT";
        return Database.ExecuteReader(sql, DocKhachHang, CommandType.Text,
            Database.TaoThamSo("@SDT", SqlDbType.VarChar, sdt.Trim(), 15)).FirstOrDefault();
    }

    /// <summary>
    /// Kiểm tra SĐT đã thuộc khách khác chưa (KH-01).
    /// </summary>
    /// <param name="sdt">Số điện thoại.</param>
    /// <param name="boQuaMaKH">Mã khách đang sửa; null khi thêm mới.</param>
    public bool KiemTraTrungSDT(string sdt, int? boQuaMaKH)
    {
        const string sql = @"SELECT COUNT(*) FROM dbo.KhachHang
                             WHERE SDT = @SDT AND (@BoQuaMaKH IS NULL OR MaKH <> @BoQuaMaKH)";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@SDT", SqlDbType.VarChar, sdt, 15),
            Database.TaoThamSo("@BoQuaMaKH", SqlDbType.Int, boQuaMaKH));
        return Convert.ToInt32(kq ?? 0) > 0;
    }

    /// <summary>Khách đã có hóa đơn nào chưa (KH-02: đã có thì không được xóa).</summary>
    public bool DaCoHoaDon(int maKH)
    {
        const string sql = "SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.HoaDon WHERE MaKH = @MaKH) THEN 1 ELSE 0 END";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@MaKH", SqlDbType.Int, maKH));
        return Convert.ToInt32(kq ?? 0) == 1;
    }

    /// <summary>Thêm khách; trả về MaKH mới.</summary>
    public int Them(KhachHang kh)
    {
        const string sql = @"INSERT INTO dbo.KhachHang (HoTen, SDT, Email, DiaChi)
                             OUTPUT INSERTED.MaKH
                             VALUES (@HoTen, @SDT, @Email, @DiaChi)";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@HoTen", SqlDbType.NVarChar, kh.HoTen, 100),
            Database.TaoThamSo("@SDT", SqlDbType.VarChar, kh.SDT, 15),
            Database.TaoThamSo("@Email", SqlDbType.VarChar, string.IsNullOrWhiteSpace(kh.Email) ? null : kh.Email, 100),
            Database.TaoThamSo("@DiaChi", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(kh.DiaChi) ? null : kh.DiaChi, 255));
        return Convert.ToInt32(kq);
    }

    /// <summary>Sửa thông tin khách theo MaKH.</summary>
    public bool Sua(KhachHang kh)
    {
        const string sql = @"UPDATE dbo.KhachHang
                             SET HoTen = @HoTen, SDT = @SDT, Email = @Email, DiaChi = @DiaChi
                             WHERE MaKH = @MaKH";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@HoTen", SqlDbType.NVarChar, kh.HoTen, 100),
            Database.TaoThamSo("@SDT", SqlDbType.VarChar, kh.SDT, 15),
            Database.TaoThamSo("@Email", SqlDbType.VarChar, string.IsNullOrWhiteSpace(kh.Email) ? null : kh.Email, 100),
            Database.TaoThamSo("@DiaChi", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(kh.DiaChi) ? null : kh.DiaChi, 255),
            Database.TaoThamSo("@MaKH", SqlDbType.Int, kh.MaKH)) > 0;
    }

    /// <summary>Xóa khách (BLL kiểm tra quyền Quản lý và DaCoHoaDon = false trước – KH-02).</summary>
    public bool Xoa(int maKH)
    {
        const string sql = "DELETE FROM dbo.KhachHang WHERE MaKH = @MaKH";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@MaKH", SqlDbType.Int, maKH)) > 0;
    }

    /// <summary>
    /// Lịch sử hóa đơn của khách (cả hoàn tất và đã hủy), mới nhất trước – không kèm chi tiết.
    /// </summary>
    public List<HoaDon> LayLichSuHoaDon(int maKH)
    {
        const string sql = @"SELECT hd.MaHD, hd.NgayLap, hd.MaTK, tk.HoTen AS TenThuNgan,
                                    hd.MaKH, kh.HoTen AS TenKhach, kh.SDT,
                                    hd.TamTinh, hd.GiamGia, hd.MaKM, km.TenKM,
                                    hd.TongThanhToan, hd.TienKhachDua, hd.TienThoi,
                                    hd.TrangThai, hd.LyDoHuy, hd.GhiChu
                             FROM dbo.HoaDon AS hd
                             INNER JOIN dbo.TaiKhoan  AS tk ON tk.MaTK = hd.MaTK
                             INNER JOIN dbo.KhachHang AS kh ON kh.MaKH = hd.MaKH
                             LEFT  JOIN dbo.KhuyenMai AS km ON km.MaKM = hd.MaKM
                             WHERE hd.MaKH = @MaKH
                             ORDER BY hd.NgayLap DESC";
        return Database.ExecuteReader(sql, HoaDonDAL.DocHoaDon, CommandType.Text,
            Database.TaoThamSo("@MaKH", SqlDbType.Int, maKH));
    }

    /// <summary>
    /// Từng dòng sách khách đã mua trong các hóa đơn HOÀN TẤT (TK-03), kèm loại sách –
    /// đầu vào để BLL dùng LINQ tính hồ sơ khách quen (ĐT4).
    /// </summary>
    public List<DongLichSuMua> LayDongLichSuMua(int maKH)
    {
        const string sql = @"SELECT hd.MaHD, hd.NgayLap, ct.MaSach, s.TenSach, s.MaLoai, l.TenLoai,
                                    ct.SoLuong, ct.ThanhTien
                             FROM dbo.HoaDon AS hd
                             INNER JOIN dbo.ChiTietHoaDon AS ct ON ct.MaHD = hd.MaHD
                             INNER JOIN dbo.Sach          AS s  ON s.MaSach = ct.MaSach
                             INNER JOIN dbo.LoaiSach      AS l  ON l.MaLoai = s.MaLoai
                             WHERE hd.MaKH = @MaKH AND hd.TrangThai = 1
                             ORDER BY hd.NgayLap DESC";
        return Database.ExecuteReader(sql, r => new DongLichSuMua
        {
            MaHD = r.GetStringOrEmpty("MaHD"),
            NgayLap = r.GetDateTimeOrMin("NgayLap"),
            MaSach = r.GetStringOrEmpty("MaSach"),
            TenSach = r.GetStringOrEmpty("TenSach"),
            MaLoai = r.GetIntOrZero("MaLoai"),
            TenLoai = r.GetStringOrEmpty("TenLoai"),
            SoLuong = r.GetIntOrZero("SoLuong"),
            ThanhTien = r.GetDecimalOrZero("ThanhTien")
        }, CommandType.Text, Database.TaoThamSo("@MaKH", SqlDbType.Int, maKH));
    }

    /// <summary>Ánh xạ một dòng thành KhachHang.</summary>
    private static KhachHang DocKhachHang(SqlDataReader r) => new()
    {
        MaKH = r.GetIntOrZero("MaKH"),
        HoTen = r.GetStringOrEmpty("HoTen"),
        SDT = r.GetStringOrEmpty("SDT"),
        Email = r.GetStringOrNull("Email"),
        DiaChi = r.GetStringOrNull("DiaChi")
    };

    /// <summary>
    /// Tạo mẫu LIKE "%từ khóa%" và thoát ký tự đặc biệt [, %, _; trả null nếu từ khóa rỗng.
    /// Chỉ ghép chuỗi vào GIÁ TRỊ tham số, không ghép vào câu SQL.
    /// </summary>
    private static string? TaoMauTimKiem(string? tuKhoa)
    {
        if (string.IsNullOrWhiteSpace(tuKhoa))
        {
            return null;
        }

        string daThoat = tuKhoa.Trim().Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
        return "%" + daThoat + "%";
    }
}
