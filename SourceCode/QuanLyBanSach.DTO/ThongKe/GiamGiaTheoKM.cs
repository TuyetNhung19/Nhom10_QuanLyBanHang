// File: QuanLyBanSach.DTO/ThongKe/GiamGiaTheoKM.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Tổng giảm giá của một khuyến mãi trong ngày – result set (3) của sp_BaoCaoCuoiNgay.
/// </summary>
public class GiamGiaTheoKM
{
    /// <summary>Tên khuyến mãi.</summary>
    public string TenKM { get; set; } = string.Empty;

    /// <summary>Số hóa đơn áp dụng.</summary>
    public int SoHoaDon { get; set; }

    /// <summary>Tổng tiền giảm.</summary>
    public decimal TongGiam { get; set; }
}
