// File: QuanLyBanSach.GUI/Views/IKhuyenMaiView.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.GUI.Views;

/// <summary>
/// View của màn hình Khuyến mãi (ĐT3) theo MVP. KhuyenMaiPresenter chỉ làm việc qua interface này.
/// Các ô số (NumericUpDown) tự đưa giá trị gán vào về khoảng [Minimum, Maximum] của ô.
/// </summary>
public interface IKhuyenMaiView
{
    /// <summary>Mã khuyến mãi của dòng đang chọn; null nếu chưa chọn.</summary>
    int? MaKMDangChon { get; }

    /// <summary>Tên khuyến mãi.</summary>
    string TenKM { get; set; }

    /// <summary>Loại khuyến mãi đang chọn trong ComboBox.</summary>
    LoaiKhuyenMai LoaiKM { get; set; }

    /// <summary>Ngưỡng tổng đơn (loại 1).</summary>
    decimal NguongTien { get; set; }

    /// <summary>Phần trăm giảm (loại 1, 2).</summary>
    int PhanTramGiam { get; set; }

    /// <summary>Mã loại sách được giảm (loại 2); null = không chọn.</summary>
    int? MaLoaiSach { get; set; }

    /// <summary>Tên loại sách đang chọn (để xem trước điều kiện).</summary>
    string? TenLoaiSach { get; }

    /// <summary>Số cuốn tối thiểu (loại 3).</summary>
    int NguongSoLuong { get; set; }

    /// <summary>Số tiền giảm cố định (loại 3).</summary>
    decimal SoTienGiam { get; set; }

    /// <summary>Ngày bắt đầu hiệu lực.</summary>
    DateTime NgayBatDau { get; set; }

    /// <summary>Ngày kết thúc hiệu lực.</summary>
    DateTime NgayKetThuc { get; set; }

    /// <summary>Khuyến mãi đang bật.</summary>
    bool DangBat { get; set; }

    /// <summary>Nạp danh mục cho 2 ComboBox: các loại khuyến mãi (mã → tên) và các loại sách.</summary>
    void HienDanhMuc(List<KeyValuePair<LoaiKhuyenMai, string>> dsLoaiKM, List<LoaiSach> dsLoaiSach);

    /// <summary>Đổ danh sách khuyến mãi lên lưới và chọn lại dòng có mã <paramref name="maKMChon"/> (nếu có).</summary>
    void HienDanhSach(List<KhuyenMai> danhSach, int? maKMChon);

    /// <summary>Hiện dòng xem trước điều kiện khuyến mãi.</summary>
    void HienXemTruoc(string moTa);

    /// <summary>
    /// Đổi trạng thái màn hình. dangNhap = true khi đang thêm/sửa (chỉ bật Lưu, Hủy, mở các ô chung);
    /// coDongDangChon quyết định nút Sửa/Xóa có bật không khi đang xem.
    /// </summary>
    void DatCheDo(bool dangNhap, bool coDongDangChon);

    /// <summary>Bật/tắt các ô riêng theo loại khuyến mãi (Presenter đã tính sẵn theo chế độ và loại).</summary>
    void BatONhapTheoLoai(bool nguongTien, bool loaiSach, bool phanTram, bool soLuong);

    /// <summary>Xóa các biểu tượng lỗi trên ô nhập.</summary>
    void XoaLoi();

    /// <summary>Hiện thông báo (thành công / thông tin).</summary>
    void HienThongBao(string thongDiep);

    /// <summary>Hiện lỗi; nếu có tên trường thì gắn ErrorProvider vào ô tương ứng.</summary>
    void HienLoi(string? truong, string thongDiep);

    /// <summary>Hỏi Có/Không, trả về true nếu người dùng đồng ý.</summary>
    bool XacNhan(string thongDiep);
}
