// File: QuanLyBanSach.DTO/ThongKe/HieuQuaKhuyenMai.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Hiệu quả một chương trình khuyến mãi – kết quả sp_HieuQuaKhuyenMai.
/// </summary>
public class HieuQuaKhuyenMai
{
    /// <summary>Mã khuyến mãi.</summary>
    public int MaKM { get; set; }

    /// <summary>Tên khuyến mãi.</summary>
    public string TenKM { get; set; } = string.Empty;

    /// <summary>Số hóa đơn đã áp dụng.</summary>
    public int SoHoaDonApDung { get; set; }

    /// <summary>Tổng tiền đã giảm.</summary>
    public decimal TongTienGiam { get; set; }

    /// <summary>Doanh thu của các hóa đơn có áp dụng.</summary>
    public decimal DoanhThu { get; set; }
}
