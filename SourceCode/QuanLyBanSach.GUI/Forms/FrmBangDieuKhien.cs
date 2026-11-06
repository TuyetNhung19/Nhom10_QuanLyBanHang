// File: QuanLyBanSach.GUI/Forms/FrmBangDieuKhien.cs
namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Form con MDI chứa UcBangDieuKhien (Dock Fill) – tự mở sau đăng nhập với Thu ngân/Quản lý (ĐT5, TC22).
/// Mọi xử lý nằm trong UserControl; Form chỉ thêm phím F5 để làm mới.
/// </summary>
public partial class FrmBangDieuKhien : Form
{
    /// <summary>Khởi tạo Form bảng điều khiển.</summary>
    public FrmBangDieuKhien()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
    }

    /// <summary>F5: làm mới số liệu ngay.</summary>
    private void FrmBangDieuKhien_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F5)
        {
            ucBangDieuKhien.LamMoi();
            e.Handled = true;
        }
    }
}
