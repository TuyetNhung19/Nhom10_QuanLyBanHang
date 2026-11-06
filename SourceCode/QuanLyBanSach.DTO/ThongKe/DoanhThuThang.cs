// File: QuanLyBanSach.DTO/ThongKe/DoanhThuThang.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Doanh thu một tháng – kết quả sp_DoanhThuTheoThang.
/// </summary>
public class DoanhThuThang
{
    /// <summary>Năm.</summary>
    public int Nam { get; set; }

    /// <summary>Tháng (1–12).</summary>
    public int Thang { get; set; }

    /// <summary>Số hóa đơn hoàn tất.</summary>
    public int SoHoaDon { get; set; }

    /// <summary>Tổng tiền giảm giá.</summary>
    public decimal TongGiamGia { get; set; }

    /// <summary>Doanh thu (TK-03).</summary>
    public decimal DoanhThu { get; set; }

    /// <summary>Nhãn hiển thị, ví dụ "10/2026".</summary>
    public string NhanThang => $"{Thang:00}/{Nam}";
}
