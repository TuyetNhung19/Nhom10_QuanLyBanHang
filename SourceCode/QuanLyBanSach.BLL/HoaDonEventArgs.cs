// File: QuanLyBanSach.BLL/HoaDonEventArgs.cs
namespace QuanLyBanSach.BLL;

/// <summary>
/// Dữ liệu kèm sự kiện HoaDonDaLuu / HoaDonDaHuy của HoaDonBLL (ĐT5 – bảng điều khiển tự cập nhật).
/// </summary>
public class HoaDonEventArgs : EventArgs
{
    /// <summary>Tạo dữ liệu sự kiện.</summary>
    public HoaDonEventArgs(string maHD, int maTK, decimal tongThanhToan)
    {
        MaHD = maHD;
        MaTK = maTK;
        TongThanhToan = tongThanhToan;
    }

    /// <summary>Mã hóa đơn vừa lưu / vừa hủy.</summary>
    public string MaHD { get; }

    /// <summary>Thu ngân lập hóa đơn (bảng điều khiển của thu ngân khác có thể bỏ qua).</summary>
    public int MaTK { get; }

    /// <summary>Tổng thanh toán của hóa đơn.</summary>
    public decimal TongThanhToan { get; }
}
