// File: QuanLyBanSach.DTO/VaiTro.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Vai trò của tài khoản, khớp cột TaiKhoan.VaiTro (TINYINT).
/// </summary>
public enum VaiTro : byte
{
    /// <summary>Thu ngân: bán hàng, treo đơn, in hóa đơn, khách hàng (không xóa).</summary>
    ThuNgan = 1,

    /// <summary>Quản lý: toàn bộ nghiệp vụ, sách, giá, khuyến mãi, hủy hóa đơn, thống kê.</summary>
    QuanLy = 2,

    /// <summary>Quản trị viên: quản lý tài khoản; không bán hàng.</summary>
    Admin = 3
}
