// File: QuanLyBanSach.DAL/TaiKhoanDAL.cs
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.DAL;

/// <summary>
/// Truy cập bảng TaiKhoan. Không bao giờ trả cột MatKhau ra ngoài (trừ khi cần so khớp trong SQL).
/// </summary>
public class TaiKhoanDAL
{
    private const string CotTaiKhoan = "MaTK, TenDangNhap, HoTen, VaiTro, TrangThai";

    /// <summary>
    /// Đăng nhập bằng sp_DangNhap. Trả về tài khoản (kể cả đang bị khóa để BLL báo lỗi TK-01),
    /// hoặc null nếu sai tên đăng nhập / mật khẩu.
    /// </summary>
    /// <param name="tenDangNhap">Tên đăng nhập.</param>
    /// <param name="matKhauBam">Mật khẩu đã băm SHA-256 hex in hoa (TK-02).</param>
    public TaiKhoan? DangNhap(string tenDangNhap, string matKhauBam)
    {
        List<TaiKhoan> ds = Database.ExecuteReader("sp_DangNhap", DocTaiKhoan, CommandType.StoredProcedure,
            Database.TaoThamSo("@TenDangNhap", SqlDbType.VarChar, tenDangNhap, 50),
            Database.TaoThamSo("@MatKhau", SqlDbType.VarChar, matKhauBam, 100));
        return ds.FirstOrDefault();
    }

    /// <summary>Lấy toàn bộ tài khoản, Admin và Quản lý lên trước.</summary>
    public List<TaiKhoan> LayDanhSach()
    {
        string sql = $"SELECT {CotTaiKhoan} FROM dbo.TaiKhoan ORDER BY VaiTro DESC, TenDangNhap";
        return Database.ExecuteReader(sql, DocTaiKhoan);
    }

    /// <summary>Lấy một tài khoản theo mã; null nếu không có.</summary>
    public TaiKhoan? LayTheoMa(int maTK)
    {
        string sql = $"SELECT {CotTaiKhoan} FROM dbo.TaiKhoan WHERE MaTK = @MaTK";
        return Database.ExecuteReader(sql, DocTaiKhoan, CommandType.Text,
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK)).FirstOrDefault();
    }

    /// <summary>
    /// Kiểm tra tên đăng nhập đã tồn tại chưa (TK-01).
    /// </summary>
    /// <param name="ten">Tên đăng nhập cần kiểm tra.</param>
    /// <param name="boQuaMaTK">Mã tài khoản đang sửa (bỏ qua chính nó); null khi thêm mới.</param>
    public bool KiemTraTrungTen(string ten, int? boQuaMaTK)
    {
        const string sql = @"SELECT COUNT(*) FROM dbo.TaiKhoan
                             WHERE TenDangNhap = @TenDangNhap AND (@BoQuaMaTK IS NULL OR MaTK <> @BoQuaMaTK)";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@TenDangNhap", SqlDbType.VarChar, ten, 50),
            Database.TaoThamSo("@BoQuaMaTK", SqlDbType.Int, boQuaMaTK));
        return Convert.ToInt32(kq ?? 0) > 0;
    }

    /// <summary>
    /// Thêm tài khoản; tk.MatKhau phải là chuỗi đã băm. Trả về MaTK mới (OUTPUT INSERTED.MaTK).
    /// </summary>
    public int Them(TaiKhoan tk)
    {
        const string sql = @"INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, TrangThai)
                             OUTPUT INSERTED.MaTK
                             VALUES (@TenDangNhap, @MatKhau, @HoTen, @VaiTro, @TrangThai)";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@TenDangNhap", SqlDbType.VarChar, tk.TenDangNhap, 50),
            Database.TaoThamSo("@MatKhau", SqlDbType.VarChar, tk.MatKhau, 100),
            Database.TaoThamSo("@HoTen", SqlDbType.NVarChar, tk.HoTen, 100),
            Database.TaoThamSo("@VaiTro", SqlDbType.TinyInt, (byte)tk.VaiTro),
            Database.TaoThamSo("@TrangThai", SqlDbType.Bit, tk.TrangThai));
        return Convert.ToInt32(kq);
    }

    /// <summary>Sửa họ tên và vai trò (không sửa tên đăng nhập, mật khẩu). Trả về true nếu có dòng được sửa.</summary>
    public bool Sua(TaiKhoan tk)
    {
        const string sql = "UPDATE dbo.TaiKhoan SET HoTen = @HoTen, VaiTro = @VaiTro WHERE MaTK = @MaTK";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@HoTen", SqlDbType.NVarChar, tk.HoTen, 100),
            Database.TaoThamSo("@VaiTro", SqlDbType.TinyInt, (byte)tk.VaiTro),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, tk.MaTK)) > 0;
    }

    /// <summary>Khóa (false) hoặc mở khóa (true) tài khoản.</summary>
    public bool DoiTrangThai(int maTK, bool hoatDong)
    {
        const string sql = "UPDATE dbo.TaiKhoan SET TrangThai = @TrangThai WHERE MaTK = @MaTK";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@TrangThai", SqlDbType.Bit, hoatDong),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK)) > 0;
    }

    /// <summary>Admin đặt lại mật khẩu (không cần mật khẩu cũ).</summary>
    /// <param name="maTK">Mã tài khoản.</param>
    /// <param name="matKhauBam">Mật khẩu mới đã băm.</param>
    public bool DatLaiMatKhau(int maTK, string matKhauBam)
    {
        const string sql = "UPDATE dbo.TaiKhoan SET MatKhau = @MatKhau WHERE MaTK = @MaTK";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@MatKhau", SqlDbType.VarChar, matKhauBam, 100),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK)) > 0;
    }

    /// <summary>
    /// Người dùng tự đổi mật khẩu: chỉ cập nhật khi mật khẩu cũ khớp.
    /// </summary>
    /// <returns>false nếu mật khẩu cũ sai.</returns>
    public bool DoiMatKhau(int maTK, string bamCu, string bamMoi)
    {
        const string sql = "UPDATE dbo.TaiKhoan SET MatKhau = @MatKhauMoi WHERE MaTK = @MaTK AND MatKhau = @MatKhauCu";
        return Database.ExecuteNonQuery(sql, CommandType.Text,
            Database.TaoThamSo("@MatKhauMoi", SqlDbType.VarChar, bamMoi, 100),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK),
            Database.TaoThamSo("@MatKhauCu", SqlDbType.VarChar, bamCu, 100)) > 0;
    }

    /// <summary>Đếm số Admin đang hoạt động (không cho khóa/hạ quyền Admin cuối cùng).</summary>
    public int DemAdminDangHoatDong()
    {
        const string sql = "SELECT COUNT(*) FROM dbo.TaiKhoan WHERE VaiTro = @VaiTro AND TrangThai = 1";
        object? kq = Database.ExecuteScalar(sql, CommandType.Text,
            Database.TaoThamSo("@VaiTro", SqlDbType.TinyInt, (byte)VaiTro.Admin));
        return Convert.ToInt32(kq ?? 0);
    }

    /// <summary>Ánh xạ một dòng thành TaiKhoan (không đọc mật khẩu).</summary>
    private static TaiKhoan DocTaiKhoan(SqlDataReader r) => new()
    {
        MaTK = r.GetIntOrZero("MaTK"),
        TenDangNhap = r.GetStringOrEmpty("TenDangNhap"),
        HoTen = r.GetStringOrEmpty("HoTen"),
        VaiTro = (VaiTro)(byte)r.GetIntOrZero("VaiTro"),
        TrangThai = r.GetBoolOrFalse("TrangThai")
    };
}
