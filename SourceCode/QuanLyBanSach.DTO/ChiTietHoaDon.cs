// File: QuanLyBanSach.DTO/ChiTietHoaDon.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Một dòng trong hóa đơn – ánh xạ bảng ChiTietHoaDon, dùng chung cho
/// hóa đơn đang lập, đơn treo và hóa đơn đã lưu.
/// </summary>
public class ChiTietHoaDon
{
    /// <summary>Số thứ tự dòng (chỉ để hiển thị và in).</summary>
    public int Stt { get; set; }

    /// <summary>Mã hóa đơn (trống khi hóa đơn chưa lưu).</summary>
    public string MaHD { get; set; } = string.Empty;

    /// <summary>Mã sách.</summary>
    public string MaSach { get; set; } = string.Empty;

    /// <summary>Tên sách (hiển thị).</summary>
    public string TenSach { get; set; } = string.Empty;

    /// <summary>Mã loại sách – dùng tính khuyến mãi theo loại (ĐT3).</summary>
    public int MaLoai { get; set; }

    /// <summary>Số lượng mua (&gt; 0 – HD-01).</summary>
    public int SoLuong { get; set; }

    /// <summary>Đơn giá tại thời điểm bán (HD-04).</summary>
    public decimal DonGia { get; set; }

    /// <summary>Thành tiền = DonGia × SoLuong.</summary>
    public decimal ThanhTien => DonGia * SoLuong;

    /// <summary>Tồn kho hiện tại của sách – dùng kiểm tra không bán vượt tồn (HD-02, BH-01).</summary>
    public int SoLuongTon { get; set; }

    /// <summary>
    /// Sách hiện còn đang bán (Sach.TrangThai = 1) – dùng lọc gợi ý mua kèm (ĐT4)
    /// và kiểm tra lại khi gọi đơn treo (BH-01).
    /// </summary>
    public bool SachDangBan { get; set; } = true;

    /// <summary>Tạo bản sao độc lập (dùng khi treo đơn / gọi lại đơn treo).</summary>
    public ChiTietHoaDon SaoChep() => (ChiTietHoaDon)MemberwiseClone();
}
