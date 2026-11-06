// File: QuanLyBanSach.GUI/Views/IKhachHangView.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.GUI.Views;

/// <summary>
/// View của màn hình Khách hàng theo MVP. KhachHangPresenter chỉ làm việc qua interface này,
/// nên kiểm thử được bằng một View giả mà không cần mở Form.
/// </summary>
public interface IKhachHangView
{
    /// <summary>Mã khách của dòng đang chọn trên lưới; null nếu chưa chọn.</summary>
    int? MaKHDangChon { get; }

    /// <summary>Họ tên trong ô nhập.</summary>
    string HoTen { get; set; }

    /// <summary>Số điện thoại trong ô nhập.</summary>
    string SDT { get; set; }

    /// <summary>Email trong ô nhập.</summary>
    string Email { get; set; }

    /// <summary>Địa chỉ trong ô nhập.</summary>
    string DiaChi { get; set; }

    /// <summary>Từ khóa tìm kiếm (gán được khi mở từ màn hình bán hàng với SĐT gõ sẵn).</summary>
    string TuKhoa { get; set; }

    /// <summary>
    /// Thiết lập một lần khi mở màn hình: hiện nút Xóa hay không (KH-02: chỉ Quản lý),
    /// có phải chế độ CHỌN KHÁCH (hiện nút Chọn/Đóng, đổi tiêu đề) hay không.
    /// </summary>
    void CaiDatManHinh(bool choPhepXoa, bool cheDoChon);

    /// <summary>Đổ danh sách khách lên lưới và chọn lại dòng có mã <paramref name="maKHChon"/> (nếu có).</summary>
    void HienDanhSach(List<KhachHang> danhSach, int? maKHChon);

    /// <summary>Hiện lịch sử mua của khách đang chọn (null = xóa lưới) và dòng tổng kết.</summary>
    void HienLichSu(List<HoaDon>? lichSu, string tongKet);

    /// <summary>Hiện thông báo thành công.</summary>
    void HienThongBao(string thongDiep);

    /// <summary>Hiện lỗi; nếu có tên trường thì gắn ErrorProvider vào ô tương ứng.</summary>
    void HienLoi(string? truong, string thongDiep);

    /// <summary>Hỏi Có/Không, trả về true nếu người dùng đồng ý.</summary>
    bool XacNhan(string thongDiep);

    /// <summary>Xóa nội dung các ô nhập và các biểu tượng lỗi.</summary>
    void XoaONhap();

    /// <summary>
    /// Đổi trạng thái màn hình. dangNhap = true khi đang thêm/sửa (chỉ bật Lưu, Hủy);
    /// coKhachDangChon quyết định nút Sửa/Xóa/Chọn có bật không khi đang xem.
    /// </summary>
    void DatCheDo(bool dangNhap, bool coKhachDangChon);

    /// <summary>Đóng màn hình (chế độ chọn) và trả khách về cho FrmBanHang; null = đóng không chọn.</summary>
    void DongVaTraVe(KhachHang? khach);
}
