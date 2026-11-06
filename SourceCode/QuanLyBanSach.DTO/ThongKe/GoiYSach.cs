// File: QuanLyBanSach.DTO/ThongKe/GoiYSach.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Sách gợi ý "thường được mua kèm" (ĐT4) – kết quả sp_SachMuaKem hoặc LINQ ở BLL.
/// </summary>
public class GoiYSach
{
    /// <summary>Mã sách gợi ý.</summary>
    public string MaSach { get; set; } = string.Empty;

    /// <summary>Tên sách gợi ý.</summary>
    public string TenSach { get; set; } = string.Empty;

    /// <summary>Giá bán hiện hành.</summary>
    public decimal GiaBan { get; set; }

    /// <summary>Tồn kho hiện tại (chỉ gợi ý sách còn tồn).</summary>
    public int SoLuongTon { get; set; }

    /// <summary>Số hóa đơn đã mua cùng sách gốc (cột SoHoaDonChung của sp_SachMuaKem).</summary>
    public int SoLanMuaCung { get; set; }

    /// <summary>Hiển thị dạng "Tên sách (mua cùng n lần)".</summary>
    public override string ToString() => $"{TenSach} (mua cùng {SoLanMuaCung} lần)";
}
