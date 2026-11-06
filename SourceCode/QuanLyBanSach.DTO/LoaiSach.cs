// File: QuanLyBanSach.DTO/LoaiSach.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Loại sách (thể loại) – ánh xạ bảng LoaiSach.
/// </summary>
public class LoaiSach
{
    /// <summary>Mã loại (IDENTITY).</summary>
    public int MaLoai { get; set; }

    /// <summary>Tên loại, duy nhất.</summary>
    public string TenLoai { get; set; } = string.Empty;

    /// <summary>Mô tả ngắn (có thể trống).</summary>
    public string? MoTa { get; set; }

    /// <summary>Trả về tên loại để ComboBox hiển thị đúng khi không đặt DisplayMember.</summary>
    public override string ToString() => TenLoai;
}
