// File: QuanLyBanSach.DTO/LoaiKhuyenMai.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Loại chương trình khuyến mãi, khớp cột KhuyenMai.LoaiKM (TINYINT, CHECK 1–3).
/// </summary>
public enum LoaiKhuyenMai : byte
{
    /// <summary>Giảm % khi tổng đơn đạt ngưỡng (NguongTien + PhanTramGiam).</summary>
    TheoTongDon = 1,

    /// <summary>Giảm % cho sách thuộc một loại (MaLoai + PhanTramGiam).</summary>
    TheoLoaiSach = 2,

    /// <summary>Mua từ N cuốn giảm số tiền cố định (NguongSoLuong + SoTienGiam).</summary>
    TheoSoLuong = 3
}
