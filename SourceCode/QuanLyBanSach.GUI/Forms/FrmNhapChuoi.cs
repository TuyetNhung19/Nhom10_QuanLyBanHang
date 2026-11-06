// File: QuanLyBanSach.GUI/Forms/FrmNhapChuoi.cs
namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Hộp thoại nhập một chuỗi dùng chung (mật khẩu mới, lý do hủy...). Có thể ẩn ký tự khi nhập mật khẩu.
/// Việc kiểm tra giá trị do BLL đảm nhận.
/// </summary>
public partial class FrmNhapChuoi : Form
{
    /// <summary>Constructor mặc định cho Designer.</summary>
    public FrmNhapChuoi() : this("Nhập dữ liệu", "Giá trị:")
    {
    }

    /// <summary>Tạo hộp thoại nhập chuỗi.</summary>
    /// <param name="tieuDe">Tiêu đề cửa sổ.</param>
    /// <param name="nhan">Nhãn mô tả ô nhập.</param>
    /// <param name="anKyTu">true: hiện dấu chấm thay ký tự (mật khẩu).</param>
    /// <param name="giaTriMacDinh">Giá trị điền sẵn.</param>
    public FrmNhapChuoi(string tieuDe, string nhan, bool anKyTu = false, string giaTriMacDinh = "")
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        Text = tieuDe;
        lblNhan.Text = nhan;
        txtGiaTri.UseSystemPasswordChar = anKyTu;
        chkHienKyTu.Visible = anKyTu;
        txtGiaTri.Text = giaTriMacDinh;
    }

    /// <summary>Chuỗi người dùng đã nhập.</summary>
    public string GiaTri => txtGiaTri.Text;

    private void chkHienKyTu_CheckedChanged(object? sender, EventArgs e)
    {
        txtGiaTri.UseSystemPasswordChar = !chkHienKyTu.Checked;
    }

    private void FrmNhapChuoi_Shown(object? sender, EventArgs e)
    {
        txtGiaTri.Focus();
        txtGiaTri.SelectAll();
    }
}
