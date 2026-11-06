// File: QuanLyBanSach.DTO/ThongKe/DoanhThuThuNgan.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Doanh thu theo thu ngân – kết quả sp_DoanhThuTheoThuNgan.
/// </summary>
public class DoanhThuThuNgan
{
    /// <summary>Mã tài khoản thu ngân.</summary>
    public int MaTK { get; set; }

    /// <summary>Họ tên thu ngân.</summary>
    public string HoTen { get; set; } = string.Empty;

    /// <summary>Số hóa đơn hoàn tất.</summary>
    public int SoHoaDon { get; set; }

    /// <summary>Doanh thu (TK-03).</summary>
    public decimal DoanhThu { get; set; }
}
