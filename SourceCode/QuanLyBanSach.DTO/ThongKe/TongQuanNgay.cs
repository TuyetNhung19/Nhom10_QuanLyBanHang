// File: QuanLyBanSach.DTO/ThongKe/TongQuanNgay.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Số liệu các thẻ trên bảng điều khiển (ĐT5) – kết quả sp_TongQuanTrongNgay.
/// </summary>
public class TongQuanNgay
{
    /// <summary>Doanh thu trong ngày (TK-03).</summary>
    public decimal DoanhThu { get; set; }

    /// <summary>Số hóa đơn hoàn tất trong ngày.</summary>
    public int SoHoaDon { get; set; }

    /// <summary>Số cuốn đã bán trong ngày.</summary>
    public int SoCuonBan { get; set; }

    /// <summary>Số đầu sách đang bán có tồn &lt;= tồn tối thiểu.</summary>
    public int SoSachSapHet { get; set; }
}
