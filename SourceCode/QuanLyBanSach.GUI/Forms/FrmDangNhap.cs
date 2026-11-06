// File: QuanLyBanSach.GUI/Forms/FrmDangNhap.cs
using Microsoft.Data.SqlClient;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Màn hình đăng nhập. Đăng nhập thành công thì đóng với DialogResult.OK (Program mở FrmMain).
/// </summary>
public partial class FrmDangNhap : Form
{
    private const string LoiKetNoi = "Không kết nối được SQL Server, kiểm tra chuỗi kết nối trong App.config.";

    private readonly TaiKhoanBLL _taiKhoanBll = new();

    /// <summary>Khởi tạo Form đăng nhập.</summary>
    public FrmDangNhap()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        Text = Helpers.GiaoDien.TenCuaHang + " – Đăng nhập"; // tên nhà sách lấy từ App.config
    }

    private void FrmDangNhap_Shown(object? sender, EventArgs e)
    {
        txtTenDangNhap.Focus();
    }

    private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e)
    {
        txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
    }

    /// <summary>
    /// Đăng nhập chạy ở luồng nền (Task.Run) để giao diện không treo khi SQL Server phản hồi chậm.
    /// </summary>
    private async void btnDangNhap_Click(object? sender, EventArgs e)
    {
        errDangNhap.Clear();
        lblLoi.Text = string.Empty;

        string ten = txtTenDangNhap.Text;
        string matKhau = txtMatKhau.Text;
        DatDangXuLy(true);

        try
        {
            await Task.Run(() => _taiKhoanBll.DangNhap(ten, matKhau));
            DialogResult = DialogResult.OK;
        }
        catch (BusinessException ex)
        {
            XuLyLoiDangNhap(ex);
        }
        catch (Exception ex) when (ex is SqlException || HeThongBLL.LaLoiKetNoi(ex))
        {
            lblLoi.Text = LoiKetNoi;
            ThongBao.Loi(LoiKetNoi + "\n\n" + HeThongBLL.MoTaLoi(ex));
        }
        catch (Exception ex)
        {
            lblLoi.Text = HeThongBLL.MoTaLoi(ex);
        }
        finally
        {
            if (!IsDisposed && DialogResult != DialogResult.OK)
            {
                DatDangXuLy(false);
            }
        }
    }

    /// <summary>
    /// Hiện lỗi đăng nhập: tài khoản khóa có thông báo riêng (TK-01); sai thông tin thì
    /// xóa mật khẩu và đặt lại focus (TC02).
    /// </summary>
    private void XuLyLoiDangNhap(BusinessException ex)
    {
        lblLoi.Text = ex.Message;

        if (ex.Message == TaiKhoanBLL.LoiTaiKhoanBiKhoa)
        {
            txtMatKhau.Clear();
            ThongBao.CanhBao(ex.Message);
            txtTenDangNhap.Focus();
            txtTenDangNhap.SelectAll();
            return;
        }

        if (ex.FieldName == nameof(TaiKhoan.TenDangNhap))
        {
            errDangNhap.SetError(txtTenDangNhap, ex.Message);
            txtTenDangNhap.Focus();
            return;
        }

        errDangNhap.SetError(txtMatKhau, ex.Message);
        txtMatKhau.Clear();
        txtMatKhau.Focus();
    }

    /// <summary>Khóa/mở các ô nhập trong lúc đang kiểm tra đăng nhập.</summary>
    private void DatDangXuLy(bool dangXuLy)
    {
        txtTenDangNhap.Enabled = !dangXuLy;
        txtMatKhau.Enabled = !dangXuLy;
        chkHienMatKhau.Enabled = !dangXuLy;
        btnDangNhap.Enabled = !dangXuLy;
        btnDangNhap.Text = dangXuLy ? "Đang kiểm tra..." : "Đăng nhập";
        UseWaitCursor = dangXuLy;
    }

    private void lblTenDangNhap_Click(object sender, EventArgs e)
    {

    }
}
