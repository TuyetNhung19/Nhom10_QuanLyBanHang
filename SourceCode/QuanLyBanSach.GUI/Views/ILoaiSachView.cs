// File: QuanLyBanSach.GUI/Views/ILoaiSachView.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.GUI.Views;

/// <summary>
/// View của màn hình Loại sách theo mô hình MVP (slide buổi 15).
/// Presenter chỉ làm việc qua interface này nên không phụ thuộc WinForms,
/// có thể thay Form khác hoặc giả lập view khi kiểm thử.
/// </summary>
public interface ILoaiSachView
{
    /// <summary>Mã loại của dòng đang chọn trên lưới; null nếu chưa chọn.</summary>
    int? MaLoaiDangChon { get; }

    /// <summary>Tên loại trong ô nhập.</summary>
    string TenLoai { get; set; }

    /// <summary>Mô tả trong ô nhập.</summary>
    string MoTa { get; set; }

    /// <summary>Từ khóa tìm kiếm.</summary>
    string TuKhoa { get; }

    /// <summary>Đổ danh sách loại sách lên lưới.</summary>
    void HienDanhSach(List<LoaiSach> danhSach);

    /// <summary>Hiện thông báo thành công / thông tin.</summary>
    void HienThongBao(string thongDiep);

    /// <summary>Hiện lỗi; nếu có tên trường thì gắn ErrorProvider vào ô tương ứng.</summary>
    /// <param name="truong">Tên trường (FieldName) hoặc null nếu lỗi chung.</param>
    /// <param name="thongDiep">Nội dung lỗi.</param>
    void HienLoi(string? truong, string thongDiep);

    /// <summary>Hỏi Có/Không, trả về true nếu người dùng đồng ý.</summary>
    bool XacNhan(string thongDiep);

    /// <summary>Xóa nội dung các ô nhập và các biểu tượng lỗi.</summary>
    void XoaONhap();

    /// <summary>
    /// Đổi trạng thái màn hình: dangSua = true khi đang thêm/sửa (chỉ bật Lưu, Hủy, mở ô nhập);
    /// false khi đang xem (bật Thêm, Sửa, Xóa, khóa ô nhập).
    /// </summary>
    void DatCheDo(bool dangSua);
}
