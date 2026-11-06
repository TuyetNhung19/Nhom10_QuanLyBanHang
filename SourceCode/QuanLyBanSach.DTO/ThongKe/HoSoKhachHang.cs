// File: QuanLyBanSach.DTO/ThongKe/HoSoKhachHang.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Hồ sơ khách quen hiển thị bên phải FrmBanHang (ĐT4). Chỉ để tham khảo,
/// không phải tích điểm hay hạng thành viên.
/// </summary>
public class HoSoKhachHang
{
    /// <summary>Mã khách hàng.</summary>
    public int MaKH { get; set; }

    /// <summary>Số lần mua (số hóa đơn hoàn tất).</summary>
    public int SoLanMua { get; set; }

    /// <summary>Tổng chi tiêu (Σ TongThanhToan hóa đơn hoàn tất).</summary>
    public decimal TongChiTieu { get; set; }

    /// <summary>Lần mua gần nhất; null nếu chưa mua.</summary>
    public DateTime? LanMuaGanNhat { get; set; }

    /// <summary>Tối đa 3 loại sách khách hay mua nhất.</summary>
    public List<string> LoaiHayMua { get; set; } = new();

    /// <summary>true nếu khách chưa từng mua.</summary>
    public bool ChuaMua => SoLanMua == 0;
}
