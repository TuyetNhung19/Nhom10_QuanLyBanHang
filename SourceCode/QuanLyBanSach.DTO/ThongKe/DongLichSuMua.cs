// File: QuanLyBanSach.DTO/ThongKe/DongLichSuMua.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Một dòng lịch sử mua của khách (ĐT4) – nguyên liệu để BLL dùng LINQ
/// tính hồ sơ khách và gợi ý mua kèm.
/// </summary>
public class DongLichSuMua
{
    /// <summary>Mã hóa đơn.</summary>
    public string MaHD { get; set; } = string.Empty;

    /// <summary>Ngày lập hóa đơn.</summary>
    public DateTime NgayLap { get; set; }

    /// <summary>Mã sách.</summary>
    public string MaSach { get; set; } = string.Empty;

    /// <summary>Tên sách.</summary>
    public string TenSach { get; set; } = string.Empty;

    /// <summary>Mã loại sách.</summary>
    public int MaLoai { get; set; }

    /// <summary>Tên loại sách.</summary>
    public string TenLoai { get; set; } = string.Empty;

    /// <summary>Số lượng mua.</summary>
    public int SoLuong { get; set; }

    /// <summary>Thành tiền của dòng.</summary>
    public decimal ThanhTien { get; set; }
}
