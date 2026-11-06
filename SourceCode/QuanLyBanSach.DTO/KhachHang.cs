// File: QuanLyBanSach.DTO/KhachHang.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Khách hàng (khách quen) – ánh xạ bảng KhachHang.
/// </summary>
public class KhachHang
{
    /// <summary>Mã khách hàng (IDENTITY).</summary>
    public int MaKH { get; set; }

    /// <summary>Họ tên khách.</summary>
    public string HoTen { get; set; } = string.Empty;

    /// <summary>Số điện thoại 10 số bắt đầu bằng 0, duy nhất (KH-01).</summary>
    public string SDT { get; set; } = string.Empty;

    /// <summary>Email (có thể trống).</summary>
    public string? Email { get; set; }

    /// <summary>Địa chỉ (có thể trống).</summary>
    public string? DiaChi { get; set; }

    /// <summary>Hiển thị dạng "Họ tên - SĐT".</summary>
    public override string ToString() => $"{HoTen} - {SDT}";
}
