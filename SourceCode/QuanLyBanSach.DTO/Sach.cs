// File: QuanLyBanSach.DTO/Sach.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Sách (sản phẩm) – ánh xạ bảng Sach, kèm TenLoai lấy từ JOIN LoaiSach.
/// </summary>
public class Sach
{
    /// <summary>Mã sách, ví dụ S0001 (SP-01).</summary>
    public string MaSach { get; set; } = string.Empty;

    /// <summary>ISBN 10 hoặc 13 chữ số, có thể trống.</summary>
    public string? ISBN { get; set; }

    /// <summary>Tên sách.</summary>
    public string TenSach { get; set; } = string.Empty;

    /// <summary>Tác giả.</summary>
    public string? TacGia { get; set; }

    /// <summary>Nhà xuất bản.</summary>
    public string? NhaXuatBan { get; set; }

    /// <summary>Mã loại sách (khóa ngoại).</summary>
    public int MaLoai { get; set; }

    /// <summary>Tên loại sách (hiển thị, lấy từ JOIN).</summary>
    public string TenLoai { get; set; } = string.Empty;

    /// <summary>Giá bán hiện hành (VNĐ, &gt;= 0 – SP-02).</summary>
    public decimal GiaBan { get; set; }

    /// <summary>Số lượng tồn hiện tại (&gt;= 0 – HD-02).</summary>
    public int SoLuongTon { get; set; }

    /// <summary>Mức tồn tối thiểu để cảnh báo sắp hết (mặc định 5).</summary>
    public int TonToiThieu { get; set; } = 5;

    /// <summary>Tên file ảnh bìa trong thư mục Images cạnh file .exe (chỉ lưu tên file).</summary>
    public string? AnhBia { get; set; }

    /// <summary>true = đang bán, false = đã ẩn (SP-03).</summary>
    public bool TrangThai { get; set; } = true;

    /// <summary>Sách sắp hết khi tồn &lt;= tồn tối thiểu.</summary>
    public bool SapHet => SoLuongTon <= TonToiThieu;

    /// <summary>Trạng thái hiển thị: "Đang bán" / "Đã ẩn".</summary>
    public string TrangThaiText => TrangThai ? "Đang bán" : "Đã ẩn";

    /// <summary>Hiển thị dạng "S0001 - Nhà giả kim".</summary>
    public override string ToString() => $"{MaSach} - {TenSach}";
}
