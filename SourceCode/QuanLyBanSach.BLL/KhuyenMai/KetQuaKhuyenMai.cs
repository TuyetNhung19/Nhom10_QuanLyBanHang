// File: QuanLyBanSach.BLL/KhuyenMai/KetQuaKhuyenMai.cs
namespace QuanLyBanSach.BLL.KhuyenMai;

/// <summary>
/// Kết quả chọn khuyến mãi cho một đơn hàng (KM-01: tối đa 1 khuyến mãi).
/// </summary>
public class KetQuaKhuyenMai
{
    /// <summary>Khuyến mãi được áp dụng; null nếu không có khuyến mãi nào đạt.</summary>
    public DTO.KhuyenMai? KhuyenMaiApDung { get; init; }

    /// <summary>Số tiền giảm (0 nếu không áp dụng).</summary>
    public decimal TienGiam { get; init; }

    /// <summary>Câu nhắc "Mua thêm ... để được giảm ..."; null nếu không có.</summary>
    public string? LoiNhac { get; init; }

    /// <summary>true nếu có khuyến mãi được áp dụng.</summary>
    public bool CoApDung => KhuyenMaiApDung != null && TienGiam > 0;
}
