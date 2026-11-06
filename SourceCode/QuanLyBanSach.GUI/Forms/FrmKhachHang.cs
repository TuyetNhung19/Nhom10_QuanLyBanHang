// File: QuanLyBanSach.GUI/Forms/FrmKhachHang.cs
using System.Globalization;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;
using QuanLyBanSach.GUI.Presenters;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Màn hình Khách hàng theo MVP: Form chỉ cài IKhachHangView, mọi xử lý nằm ở KhachHangPresenter.
/// Có chế độ CHỌN KHÁCH dùng từ FrmBanHang: mở dạng dialog, trả về KhachDaChon.
/// Phần còn lại trong Form là việc thuần giao diện: hẹn giờ tìm kiếm, chặn phím chữ ở ô SĐT, tô màu lưới.
/// </summary>
public partial class FrmKhachHang : Form, IKhachHangView
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    private readonly KhachHangPresenter _presenter;
    private bool _cheDoChon;

    /// <summary>Mở ở chế độ quản lý thông thường (dùng cho menu và Designer).</summary>
    public FrmKhachHang() : this(null, false)
    {
    }

    /// <summary>Khởi tạo Form và Presenter.</summary>
    /// <param name="sdtGoiY">SĐT gõ sẵn từ màn hình bán hàng (điền vào ô tìm / ô SĐT khi thêm mới).</param>
    /// <param name="cheDoChon">true: mở để chọn khách cho hóa đơn (dialog, có nút Chọn).</param>
    public FrmKhachHang(string? sdtGoiY, bool cheDoChon)
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        colLsTongThanhToan.DefaultCellStyle.FormatProvider = VanHoaVN;
        _presenter = new KhachHangPresenter(this, sdtGoiY, cheDoChon);
    }

    /// <summary>Khách được chọn (chế độ chọn); null nếu chưa chọn.</summary>
    public KhachHang? KhachDaChon { get; private set; }

    // ===================== IKhachHangView =====================

    /// <inheritdoc/>
    public int? MaKHDangChon => dgvKhachHang.CurrentRow?.DataBoundItem is KhachHang kh ? kh.MaKH : null;

    /// <inheritdoc/>
    public string HoTen
    {
        get => txtHoTen.Text;
        set => txtHoTen.Text = value;
    }

    /// <inheritdoc/>
    public string SDT
    {
        get => txtSDT.Text;
        set => txtSDT.Text = value;
    }

    /// <inheritdoc/>
    public string Email
    {
        get => txtEmail.Text;
        set => txtEmail.Text = value;
    }

    /// <inheritdoc/>
    public string DiaChi
    {
        get => txtDiaChi.Text;
        set => txtDiaChi.Text = value;
    }

    /// <inheritdoc/>
    public string TuKhoa
    {
        get => txtTimKiem.Text;
        set
        {
            txtTimKiem.Text = value;
            tmrTimKiem.Stop(); // Presenter tự tải danh sách ngay, không cần chờ hẹn giờ
        }
    }

    /// <inheritdoc/>
    public void CaiDatManHinh(bool choPhepXoa, bool cheDoChon)
    {
        _cheDoChon = cheDoChon;
        btnXoa.Visible = choPhepXoa;
        btnChon.Visible = cheDoChon;
        btnDong.Visible = cheDoChon;

        if (cheDoChon)
        {
            Text = "Chọn khách hàng";
            StartPosition = FormStartPosition.CenterParent;
        }
    }

    /// <inheritdoc/>
    public void HienDanhSach(List<KhachHang> danhSach, int? maKHChon)
    {
        dgvKhachHang.DataSource = danhSach;
        lblSoKhach.Text = $"{danhSach.Count} khách hàng";

        if (maKHChon == null)
        {
            return;
        }

        foreach (DataGridViewRow dong in dgvKhachHang.Rows)
        {
            if (dong.DataBoundItem is KhachHang kh && kh.MaKH == maKHChon)
            {
                dgvKhachHang.CurrentCell = dong.Cells[colHoTen.Index];
                break;
            }
        }
    }

    /// <inheritdoc/>
    public void HienLichSu(List<HoaDon>? lichSu, string tongKet)
    {
        dgvLichSu.DataSource = lichSu;
        lblTongKet.Text = tongKet;
    }

    /// <inheritdoc/>
    public void HienThongBao(string thongDiep) => ThongBao.ThanhCong(thongDiep);

    /// <inheritdoc/>
    public void HienLoi(string? truong, string thongDiep)
    {
        errKhachHang.Clear();
        Control? oLoi = string.IsNullOrEmpty(truong) ? null : ThongBao.TimControl(this, truong);
        if (oLoi != null)
        {
            errKhachHang.SetError(oLoi, thongDiep);
            oLoi.Focus();
            if (oLoi is TextBox txt)
            {
                txt.SelectAll();
            }
        }

        ThongBao.CanhBao(thongDiep);
    }

    /// <inheritdoc/>
    public bool XacNhan(string thongDiep) => ThongBao.XacNhan(thongDiep);

    /// <inheritdoc/>
    public void XoaONhap()
    {
        txtHoTen.Clear();
        txtSDT.Clear();
        txtEmail.Clear();
        txtDiaChi.Clear();
        errKhachHang.Clear();
    }

    /// <summary>
    /// Xem: Thêm/Sửa/Xóa/Chọn bật, ô nhập khóa. Đang nhập: chỉ Lưu/Hủy, khóa lưới và ô tìm.
    /// AcceptButton/CancelButton đổi theo chế độ (Enter = Lưu / Chọn, Esc = Hủy / Đóng).
    /// </summary>
    public void DatCheDo(bool dangNhap, bool coKhachDangChon)
    {
        txtHoTen.ReadOnly = !dangNhap;
        txtSDT.ReadOnly = !dangNhap;
        txtEmail.ReadOnly = !dangNhap;
        txtDiaChi.ReadOnly = !dangNhap;

        btnThem.Enabled = !dangNhap;
        btnSua.Enabled = !dangNhap && coKhachDangChon;
        btnXoa.Enabled = !dangNhap && coKhachDangChon;
        btnChon.Enabled = !dangNhap && coKhachDangChon;
        btnLuu.Enabled = dangNhap;
        btnHuy.Enabled = dangNhap;
        txtTimKiem.Enabled = !dangNhap;
        splKhachHang.Enabled = !dangNhap;

        AcceptButton = dangNhap ? btnLuu : (_cheDoChon ? btnChon : null);
        CancelButton = dangNhap ? btnHuy : (_cheDoChon ? btnDong : null);

        if (dangNhap)
        {
            txtHoTen.Focus();
        }
        else
        {
            errKhachHang.Clear();
        }
    }

    /// <inheritdoc/>
    public void DongVaTraVe(KhachHang? khach)
    {
        KhachDaChon = khach;
        DialogResult = khach == null ? DialogResult.Cancel : DialogResult.OK;
    }

    // ===================== Sự kiện: chuyển cho Presenter =====================

    private void FrmKhachHang_Load(object? sender, EventArgs e) => _presenter.KhoiTao();

    private void btnThem_Click(object? sender, EventArgs e) => _presenter.Them();

    private void btnSua_Click(object? sender, EventArgs e) => _presenter.Sua();

    private void btnXoa_Click(object? sender, EventArgs e) => _presenter.Xoa();

    private void btnLuu_Click(object? sender, EventArgs e) => _presenter.Luu();

    private void btnHuy_Click(object? sender, EventArgs e) => _presenter.Huy();

    private void btnChon_Click(object? sender, EventArgs e) => _presenter.Chon();

    private void btnDong_Click(object? sender, EventArgs e) => _presenter.Dong();

    private void dgvKhachHang_SelectionChanged(object? sender, EventArgs e) => _presenter.ChonDong();

    private void dgvKhachHang_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            _presenter.NhapDupDong();
        }
    }

    private void tmrTimKiem_Tick(object? sender, EventArgs e)
    {
        tmrTimKiem.Stop();
        _presenter.TimKiem();
    }

    // ===================== Việc thuần giao diện (không cần Presenter) =====================

    /// <summary>Gõ phím trong ô tìm: chờ người dùng ngừng gõ rồi mới tìm (tmrTimKiem).</summary>
    private void txtTimKiem_TextChanged(object? sender, EventArgs e)
    {
        tmrTimKiem.Stop();
        tmrTimKiem.Start();
    }

    /// <summary>SĐT chỉ nhận chữ số (BLL kiểm tra lại định dạng ^0\d{9}$).</summary>
    private void txtSDT_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    /// <summary>Hóa đơn đã hủy trong lịch sử hiển thị chữ xám.</summary>
    private void dgvLichSu_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && e.CellStyle != null
            && dgvLichSu.Rows[e.RowIndex].DataBoundItem is HoaDon hd && hd.DaHuy)
        {
            e.CellStyle.ForeColor = Color.Gray;
        }
    }
}
