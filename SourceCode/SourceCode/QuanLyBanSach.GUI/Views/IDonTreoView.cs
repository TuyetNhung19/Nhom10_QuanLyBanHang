// File: QuanLyBanSach.GUI/Views/IDonTreoView.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.GUI.Views;

/// <summary>
/// View của màn hình Danh sách đơn treo (ĐT2) theo MVP. DonTreoPresenter chỉ làm việc qua interface này.
/// </summary>
public interface IDonTreoView
{
    /// <summary>Mã đơn treo của dòng đang chọn; null nếu chưa chọn.</summary>
    Guid? IdDangChon { get; }

    /// <summary>Đổ danh sách đơn treo lên lưới, kèm dòng đếm "n/5 đơn treo".</summary>
    void HienDanhSach(List<HoaDonTam> danhSach, string soDon);

    /// <summary>
    /// Xem nhanh các dòng của đơn đang chọn (null = xóa lưới chi tiết), tiêu đề khung chi tiết,
    /// và bật/tắt nút Gọi lại, Hủy đơn theo <paramref name="coDonDangChon"/>.
    /// </summary>
    void HienChiTiet(List<ChiTietHoaDon>? chiTiet, string tieuDe, bool coDonDangChon);

    /// <summary>Hỏi Có/Không, trả về true nếu người dùng đồng ý.</summary>
    bool XacNhan(string thongDiep);

    /// <summary>Hiện lỗi.</summary>
    void HienLoi(string thongDiep);

    /// <summary>Đóng màn hình, trả mã đơn được chọn để FrmBanHang gọi lại.</summary>
    void DongVaGoiLai(Guid id);
}
