// File: QuanLyBanSach.GUI/Forms/FrmTaiKhoan.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Quản lý tài khoản (Admin): thêm, sửa họ tên/vai trò, khóa/mở khóa, đặt lại mật khẩu.
/// </summary>
public partial class FrmTaiKhoan : Form
{
    /// <summary>Chế độ làm việc của màn hình.</summary>
    private enum CheDo
    {
        Xem,
        Them,
        Sua
    }

    private readonly TaiKhoanBLL _bll = new();
    private CheDo _cheDo = CheDo.Xem;

    /// <summary>Khởi tạo Form tài khoản.</summary>
    public FrmTaiKhoan()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
    }

    private TaiKhoan? TaiKhoanDangChon => dgvTaiKhoan.CurrentRow?.DataBoundItem as TaiKhoan;

    private void FrmTaiKhoan_Load(object? sender, EventArgs e)
    {
        cboVaiTro.DisplayMember = nameof(LuaChonVaiTro.Ten);
        cboVaiTro.ValueMember = nameof(LuaChonVaiTro.Ma);
        cboVaiTro.DataSource = new List<LuaChonVaiTro>
        {
            new(VaiTro.ThuNgan, "Thu ngân"),
            new(VaiTro.QuanLy, "Quản lý"),
            new(VaiTro.Admin, "Quản trị viên")
        };

        TaiDanhSach();
        DatCheDo(CheDo.Xem);
    }

    /// <summary>Tải danh sách tài khoản, chọn lại tài khoản có mã cho trước.</summary>
    private void TaiDanhSach(int? maTKChon = null)
    {
        try
        {
            int? maDangChon = maTKChon ?? TaiKhoanDangChon?.MaTK;
            dgvTaiKhoan.DataSource = _bll.LayDanhSach();

            if (maDangChon != null)
            {
                foreach (DataGridViewRow dong in dgvTaiKhoan.Rows)
                {
                    if (dong.DataBoundItem is TaiKhoan tk && tk.MaTK == maDangChon)
                    {
                        dgvTaiKhoan.CurrentCell = dong.Cells[colTenDangNhap.Index];
                        break;
                    }
                }
            }

            HienTaiKhoanDangChon();
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    /// <summary>Đổ tài khoản đang chọn lên ô nhập; đổi chữ nút Khóa/Mở khóa.</summary>
    private void HienTaiKhoanDangChon()
    {
        TaiKhoan? tk = TaiKhoanDangChon;
        txtTenDangNhap.Text = tk?.TenDangNhap ?? string.Empty;
        txtHoTen.Text = tk?.HoTen ?? string.Empty;
        txtMatKhau.Clear();
        if (tk != null)
        {
            cboVaiTro.SelectedValue = tk.VaiTro;
        }

        btnKhoaMo.Text = tk != null && !tk.TrangThai ? "🔓 Mở khóa" : "🔒 Khóa";

        if (_cheDo == CheDo.Xem)
        {
            bool coDong = tk != null;
            btnSua.Enabled = coDong;
            btnKhoaMo.Enabled = coDong;
            btnDatLaiMatKhau.Enabled = coDong;
        }
    }

    private void dgvTaiKhoan_SelectionChanged(object? sender, EventArgs e)
    {
        if (_cheDo == CheDo.Xem)
        {
            HienTaiKhoanDangChon();
        }
    }

    /// <summary>Tài khoản bị khóa hiển thị chữ xám.</summary>
    private void dgvTaiKhoan_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && e.CellStyle != null
            && dgvTaiKhoan.Rows[e.RowIndex].DataBoundItem is TaiKhoan tk && !tk.TrangThai)
        {
            e.CellStyle.ForeColor = Color.Gray;
        }
    }

    private void btnThem_Click(object? sender, EventArgs e)
    {
        DatCheDo(CheDo.Them);
        txtTenDangNhap.Clear();
        txtHoTen.Clear();
        txtMatKhau.Clear();
        cboVaiTro.SelectedValue = VaiTro.ThuNgan;
        txtTenDangNhap.Focus();
    }

    private void btnSua_Click(object? sender, EventArgs e)
    {
        if (TaiKhoanDangChon == null)
        {
            ThongBao.CanhBao("Vui lòng chọn tài khoản cần sửa.");
            return;
        }

        DatCheDo(CheDo.Sua);
        txtHoTen.Focus();
    }

    private void btnLuu_Click(object? sender, EventArgs e)
    {
        errTaiKhoan.Clear();
        var tk = new TaiKhoan
        {
            MaTK = _cheDo == CheDo.Sua ? TaiKhoanDangChon?.MaTK ?? 0 : 0,
            TenDangNhap = txtTenDangNhap.Text,
            HoTen = txtHoTen.Text,
            VaiTro = cboVaiTro.SelectedValue is VaiTro vt ? vt : VaiTro.ThuNgan,
            TrangThai = true
        };

        try
        {
            if (_cheDo == CheDo.Them)
            {
                _bll.Them(tk, txtMatKhau.Text);
                ThongBao.ThanhCong($"Đã tạo tài khoản \"{tk.TenDangNhap}\".");
            }
            else
            {
                _bll.Sua(tk);
                ThongBao.ThanhCong("Đã cập nhật tài khoản.");
            }

            DatCheDo(CheDo.Xem);
            TaiDanhSach(tk.MaTK);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex, errTaiKhoan, this);
        }
    }

    private void btnHuy_Click(object? sender, EventArgs e)
    {
        DatCheDo(CheDo.Xem);
        HienTaiKhoanDangChon();
    }

    /// <summary>Khóa hoặc mở khóa tài khoản đang chọn (BLL chặn tự khóa mình và khóa Admin cuối cùng).</summary>
    private void btnKhoaMo_Click(object? sender, EventArgs e)
    {
        TaiKhoan? tk = TaiKhoanDangChon;
        if (tk == null)
        {
            return;
        }

        string hanhDong = tk.TrangThai ? "Khóa" : "Mở khóa";
        if (!ThongBao.XacNhan($"{hanhDong} tài khoản \"{tk.TenDangNhap}\"?"))
        {
            return;
        }

        try
        {
            _bll.KhoaMoKhoa(tk.MaTK, !tk.TrangThai);
            TaiDanhSach(tk.MaTK);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    /// <summary>Đặt lại mật khẩu bằng hộp nhập chuỗi dùng chung (ẩn ký tự).</summary>
    private void btnDatLaiMatKhau_Click(object? sender, EventArgs e)
    {
        TaiKhoan? tk = TaiKhoanDangChon;
        if (tk == null)
        {
            return;
        }

        using var frm = new FrmNhapChuoi("Đặt lại mật khẩu", $"Mật khẩu mới cho \"{tk.TenDangNhap}\" (≥ 6 ký tự):", anKyTu: true);
        if (frm.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            _bll.DatLaiMatKhau(tk.MaTK, frm.GiaTri);
            ThongBao.ThanhCong($"Đã đặt lại mật khẩu cho \"{tk.TenDangNhap}\".");
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    private void btnLamMoi_Click(object? sender, EventArgs e) => TaiDanhSach();

    /// <summary>
    /// Xem: Thêm/Sửa/Khóa/Đặt lại MK bật. Đang nhập: chỉ Lưu/Hủy. Tên đăng nhập và mật khẩu chỉ nhập khi Thêm.
    /// </summary>
    private void DatCheDo(CheDo cheDo)
    {
        _cheDo = cheDo;
        bool nhap = cheDo != CheDo.Xem;
        bool coDong = TaiKhoanDangChon != null;

        txtTenDangNhap.ReadOnly = cheDo != CheDo.Them;
        txtHoTen.ReadOnly = !nhap;
        cboVaiTro.Enabled = nhap;
        txtMatKhau.Enabled = cheDo == CheDo.Them;
        if (cheDo != CheDo.Them)
        {
            txtMatKhau.Clear();
        }

        btnThem.Enabled = !nhap;
        btnSua.Enabled = !nhap && coDong;
        btnKhoaMo.Enabled = !nhap && coDong;
        btnDatLaiMatKhau.Enabled = !nhap && coDong;
        btnLamMoi.Enabled = !nhap;
        btnLuu.Enabled = nhap;
        btnHuy.Enabled = nhap;
        dgvTaiKhoan.Enabled = !nhap;

        if (!nhap)
        {
            errTaiKhoan.Clear();
        }
    }

    /// <summary>Một lựa chọn vai trò trong ComboBox.</summary>
    /// <param name="Ma">Giá trị enum.</param>
    /// <param name="Ten">Tên hiển thị.</param>
    private sealed record LuaChonVaiTro(VaiTro Ma, string Ten);
}
