// File: QuanLyBanSach.DTO/ThongKe/SachBanChay.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Một dòng top sách bán chạy – kết quả sp_SachBanChay.
/// </summary>
public class SachBanChay
{
    /// <summary>Mã sách.</summary>
    public string MaSach { get; set; } = string.Empty;

    /// <summary>Tên sách.</summary>
    public string TenSach { get; set; } = string.Empty;

    /// <summary>Tổng số cuốn đã bán.</summary>
    public int SoLuongBan { get; set; }

    /// <summary>Doanh thu theo sách (Σ ThanhTien).</summary>
    public decimal DoanhThu { get; set; }
}
