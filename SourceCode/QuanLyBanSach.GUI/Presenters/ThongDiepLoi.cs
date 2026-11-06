// File: QuanLyBanSach.GUI/Presenters/ThongDiepLoi.cs
using QuanLyBanSach.BLL;

namespace QuanLyBanSach.GUI.Presenters;

/// <summary>
/// Dùng chung cho mọi Presenter: đổi một exception thành (tên trường, thông điệp) để gọi View.HienLoi.
/// - BusinessException: giữ FieldName để View gắn ErrorProvider vào đúng ô nhập.
/// - Lỗi kết nối CSDL: thêm tiền tố "Không kết nối được CSDL."
/// - Lỗi khác: thông điệp tiếng Việt từ HeThongBLL.MoTaLoi.
/// </summary>
public static class ThongDiepLoi
{
    /// <summary>Phân loại exception.</summary>
    /// <param name="ex">Lỗi bắt được trong Presenter.</param>
    /// <returns>Tên trường (null nếu lỗi chung) và nội dung lỗi.</returns>
    public static (string? Truong, string ThongDiep) Tu(Exception ex)
    {
        if (ex is BusinessException bex)
        {
            return (bex.FieldName, bex.Message);
        }

        string thongDiep = HeThongBLL.LaLoiKetNoi(ex)
            ? "Không kết nối được CSDL. " + HeThongBLL.MoTaLoi(ex)
            : HeThongBLL.MoTaLoi(ex);
        return (null, thongDiep);
    }
}
