// File: QuanLyBanSach.GUI/Forms/FrmPhimTat.cs
namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Bảng phím tắt (đặc tả ĐT1): phím tắt màn hình bán hàng và phím tắt toàn chương trình.
/// </summary>
public partial class FrmPhimTat : Form
{
    private static readonly PhimTat[] DanhSachPhimTat =
    {
        new("F1", "Tìm sách: đưa con trỏ về ô Mã sách/ISBN", "Bán hàng"),
        new("Enter", "Thêm 1 cuốn theo mã sách/ISBN vừa gõ hoặc quét (gõ lại cùng mã để tăng số lượng)", "Bán hàng"),
        new("F2", "Chọn khách hàng theo số điện thoại", "Bán hàng"),
        new("F4", "Nhập tiền khách đưa", "Bán hàng"),
        new("F8", "Treo hóa đơn để phục vụ khách kế tiếp", "Bán hàng"),
        new("F9", "Lưu hóa đơn và xem trước bản in", "Bán hàng"),
        new("Delete", "Xóa dòng sách đang chọn", "Bán hàng"),
        new("+ / −", "Tăng / giảm số lượng dòng đang chọn", "Bán hàng"),
        new("Ctrl+B", "Mở màn hình Lập hóa đơn", "Toàn chương trình"),
        new("Ctrl+T", "Tra cứu hóa đơn", "Toàn chương trình"),
        new("Ctrl+K", "Khách hàng", "Toàn chương trình"),
        new("Ctrl+D", "Bảng điều khiển", "Toàn chương trình"),
        new("Ctrl+Shift+S", "Quản lý sách (Quản lý)", "Toàn chương trình"),
        new("Ctrl+R", "Thống kê doanh thu (Quản lý)", "Toàn chương trình"),
        new("Ctrl+E", "Báo cáo cuối ngày", "Toàn chương trình"),
        new("Ctrl+Shift+L", "Đăng xuất", "Toàn chương trình"),
        new("Ctrl+F1", "Mở bảng phím tắt này", "Toàn chương trình")
    };

    /// <summary>Khởi tạo hộp thoại phím tắt.</summary>
    public FrmPhimTat()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
    }

    private void FrmPhimTat_Load(object? sender, EventArgs e)
    {
        dgvPhimTat.DataSource = DanhSachPhimTat;
    }

    /// <summary>Một dòng trong bảng phím tắt.</summary>
    /// <param name="Phim">Phím hoặc tổ hợp phím.</param>
    /// <param name="ChucNang">Chức năng.</param>
    /// <param name="PhamVi">Màn hình áp dụng.</param>
    private sealed record PhimTat(string Phim, string ChucNang, string PhamVi);
}
