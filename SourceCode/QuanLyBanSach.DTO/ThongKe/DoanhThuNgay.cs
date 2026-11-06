// File: QuanLyBanSach.DTO/ThongKe/DoanhThuNgay.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Doanh thu một ngày – kết quả sp_DoanhThuTheoNgay (biểu đồ 7 ngày ĐT5, thống kê).
/// </summary>
public class DoanhThuNgay
{
    /// <summary>Ngày thống kê.</summary>
    public DateTime Ngay { get; set; }

    /// <summary>Số hóa đơn hoàn tất trong ngày.</summary>
    public int SoHoaDon { get; set; }

    /// <summary>Tổng tạm tính.</summary>
    public decimal TongTamTinh { get; set; }

    /// <summary>Tổng tiền giảm giá.</summary>
    public decimal TongGiamGia { get; set; }

    /// <summary>Doanh thu = Σ TongThanhToan (chỉ hóa đơn hoàn tất – TK-03).</summary>
    public decimal DoanhThu { get; set; }

    /// <summary>Nhãn ngắn cho trục biểu đồ, ví dụ "28/10".</summary>
    public string NhanNgay => Ngay.ToString("dd/MM");
}
