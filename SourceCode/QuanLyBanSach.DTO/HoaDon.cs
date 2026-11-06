// File: QuanLyBanSach.DTO/HoaDon.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Hóa đơn – ánh xạ bảng HoaDon, kèm các tên hiển thị lấy từ JOIN và danh sách chi tiết.
/// </summary>
public class HoaDon
{
    /// <summary>Mã hóa đơn dạng HD + yyyyMMdd + 3 số (HD-07).</summary>
    public string MaHD { get; set; } = string.Empty;

    /// <summary>Ngày giờ lập.</summary>
    public DateTime NgayLap { get; set; } = DateTime.Now;

    /// <summary>Mã tài khoản thu ngân lập hóa đơn.</summary>
    public int MaTK { get; set; }

    /// <summary>Mã khách hàng; null = khách lẻ.</summary>
    public int? MaKH { get; set; }

    /// <summary>Tạm tính = Σ ThanhTien.</summary>
    public decimal TamTinh { get; set; }

    /// <summary>Tiền giảm do khuyến mãi (KM-01..KM-03).</summary>
    public decimal GiamGia { get; set; }

    /// <summary>Mã khuyến mãi được áp dụng; null = không có.</summary>
    public int? MaKM { get; set; }

    /// <summary>Tổng thanh toán = TamTinh − GiamGia.</summary>
    public decimal TongThanhToan { get; set; }

    /// <summary>Tiền khách đưa (&gt;= TongThanhToan – HD-03).</summary>
    public decimal TienKhachDua { get; set; }

    /// <summary>Tiền thối = TienKhachDua − TongThanhToan.</summary>
    public decimal TienThoi { get; set; }

    /// <summary>Trạng thái: hoàn tất / đã hủy.</summary>
    public TrangThaiHoaDon TrangThai { get; set; } = TrangThaiHoaDon.HoanTat;

    /// <summary>Lý do hủy (bắt buộc khi hủy – HD-06).</summary>
    public string? LyDoHuy { get; set; }

    /// <summary>Ghi chú.</summary>
    public string? GhiChu { get; set; }

    /// <summary>Họ tên thu ngân (hiển thị, in hóa đơn).</summary>
    public string TenThuNgan { get; set; } = string.Empty;

    /// <summary>Họ tên khách; "Khách lẻ" khi MaKH null.</summary>
    public string TenKhach { get; set; } = "Khách lẻ";

    /// <summary>SĐT khách (hiển thị khi tra cứu).</summary>
    public string? SDTKhach { get; set; }

    /// <summary>Tên khuyến mãi được áp dụng (in trên hóa đơn).</summary>
    public string? TenKM { get; set; }

    /// <summary>Các dòng chi tiết của hóa đơn.</summary>
    public List<ChiTietHoaDon> ChiTiet { get; set; } = new();

    /// <summary>Tổng số cuốn trong hóa đơn.</summary>
    public int SoCuon => ChiTiet.Sum(ct => ct.SoLuong);

    /// <summary>true nếu hóa đơn đã bị hủy.</summary>
    public bool DaHuy => TrangThai == TrangThaiHoaDon.DaHuy;

    /// <summary>Trạng thái hiển thị: "Hoàn tất" / "Đã hủy".</summary>
    public string TrangThaiText => TrangThai == TrangThaiHoaDon.HoanTat ? "Hoàn tất" : "Đã hủy";
}
