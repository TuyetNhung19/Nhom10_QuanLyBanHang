// File: QuanLyBanSach.DTO/ThongKe/BaoCaoCuoiNgay.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Báo cáo cuối ngày theo thu ngân (ĐT6) – gộp 3 result set của sp_BaoCaoCuoiNgay.
/// </summary>
public class BaoCaoCuoiNgay
{
    /// <summary>Ngày báo cáo.</summary>
    public DateTime Ngay { get; set; }

    /// <summary>Tên thu ngân, hoặc "Tất cả thu ngân".</summary>
    public string TenThuNgan { get; set; } = string.Empty;

    /// <summary>Số hóa đơn hoàn tất.</summary>
    public int SoHoaDon { get; set; }

    /// <summary>Số hóa đơn đã hủy.</summary>
    public int SoHoaDonHuy { get; set; }

    /// <summary>Tổng tạm tính (hóa đơn hoàn tất).</summary>
    public decimal TongTamTinh { get; set; }

    /// <summary>Tổng giảm giá (hóa đơn hoàn tất).</summary>
    public decimal TongGiamGia { get; set; }

    /// <summary>Doanh thu thực = Σ TongThanhToan của hóa đơn hoàn tất (TK-03).</summary>
    public decimal DoanhThuThuc { get; set; }

    /// <summary>Tiền mặt phải có trong két (= DoanhThuThuc vì chỉ thu tiền mặt).</summary>
    public decimal TienMatPhaiCo { get; set; }

    /// <summary>Danh sách hóa đơn đã hủy kèm lý do – result set (2).</summary>
    public List<HoaDon> HoaDonHuy { get; set; } = new();

    /// <summary>Giảm giá theo từng khuyến mãi – result set (3).</summary>
    public List<GiamGiaTheoKM> GiamGiaTheoKhuyenMai { get; set; } = new();
}
