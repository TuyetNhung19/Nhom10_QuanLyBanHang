// File: QuanLyBanSach.DTO/HoaDonTam.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Đơn treo (ĐT2) – chỉ nằm trong bộ nhớ, KHÔNG lưu CSDL và chưa trừ tồn (BH-01, BH-02).
/// </summary>
public class HoaDonTam
{
    /// <summary>Mã định danh đơn treo trong bộ nhớ.</summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>Thu ngân treo đơn (mỗi thu ngân tối đa 5 đơn treo).</summary>
    public int MaTK { get; set; }

    /// <summary>Thời điểm treo.</summary>
    public DateTime ThoiGianTreo { get; set; } = DateTime.Now;

    /// <summary>Khách hàng đã chọn; null = khách lẻ.</summary>
    public KhachHang? Khach { get; set; }

    /// <summary>Các dòng sách của đơn treo.</summary>
    public List<ChiTietHoaDon> ChiTiet { get; set; } = new();

    /// <summary>Ghi chú (ví dụ "Khách đi lấy thêm sách").</summary>
    public string? GhiChu { get; set; }

    /// <summary>Tổng số cuốn = Σ SoLuong.</summary>
    public int SoCuon => ChiTiet.Sum(ct => ct.SoLuong);

    /// <summary>Tạm tính = Σ ThanhTien (theo giá lúc treo; gọi lại phải cập nhật giá hiện hành).</summary>
    public decimal TamTinh => ChiTiet.Sum(ct => ct.ThanhTien);

    /// <summary>Tên khách hiển thị; "Khách lẻ" nếu chưa chọn khách.</summary>
    public string TenKhachHienThi => Khach == null ? "Khách lẻ" : Khach.HoTen;

    /// <summary>Giờ treo dạng "HH:mm" để hiển thị trên FrmDonTreo.</summary>
    public string GioTreoText => ThoiGianTreo.ToString("HH:mm");
}
