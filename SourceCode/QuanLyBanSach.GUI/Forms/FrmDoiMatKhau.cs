// File: QuanLyBanSach.GUI/Forms/FrmDoiMatKhau.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.GUI.Helpers;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Hộp thoại đổi mật khẩu của người đang đăng nhập. Lỗi được gắn đúng ô theo BusinessException.FieldName
/// ("MatKhauCu" → txtMatKhauCu, "MatKhauMoi" → txtMatKhauMoi, "XacNhan" → txtXacNhan).
/// </summary>
public partial class FrmDoiMatKhau : Form
{
    private readonly TaiKhoanBLL _taiKhoanBll = new();

    /// <summary>Khởi tạo hộp thoại đổi mật khẩu.</summary>
    public FrmDoiMatKhau()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
    }

    private void FrmDoiMatKhau_Load(object? sender, EventArgs e)
    {
        lblTaiKhoan.Text = "Tài khoản: " + (PhienDangNhap.TaiKhoanHienTai?.TenDangNhap ?? string.Empty);
    }

    private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e)
    {
        bool an = !chkHienMatKhau.Checked;
        txtMatKhauCu.UseSystemPasswordChar = an;
        txtMatKhauMoi.UseSystemPasswordChar = an;
        txtXacNhan.UseSystemPasswordChar = an;
    }

    private void btnLuu_Click(object? sender, EventArgs e)
    {
        errDoiMatKhau.Clear();
        try
        {
            _taiKhoanBll.DoiMatKhau(txtMatKhauCu.Text, txtMatKhauMoi.Text, txtXacNhan.Text);
            ThongBao.ThanhCong("Đổi mật khẩu thành công.\nLần đăng nhập sau hãy dùng mật khẩu mới.");
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex, errDoiMatKhau, this);
        }
    }
}
