// File: QuanLyBanSach.GUI/Forms/FrmLoaiSach.cs
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;
using QuanLyBanSach.GUI.Presenters;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Màn hình Loại sách theo MVP: Form chỉ cài ILoaiSachView, mọi xử lý nằm ở LoaiSachPresenter.
/// Mỗi sự kiện chỉ gọi đúng một dòng _presenter.Xxx().
/// </summary>
public partial class FrmLoaiSach : Form, ILoaiSachView
{
    private readonly LoaiSachPresenter _presenter;

    /// <summary>Khởi tạo Form và Presenter.</summary>
    public FrmLoaiSach()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        _presenter = new LoaiSachPresenter(this);
    }

    // ===================== ILoaiSachView =====================

    /// <inheritdoc/>
    public int? MaLoaiDangChon => dgvLoaiSach.CurrentRow?.DataBoundItem is LoaiSach loai ? loai.MaLoai : null;

    /// <inheritdoc/>
    public string TenLoai
    {
        get => txtTenLoai.Text;
        set => txtTenLoai.Text = value;
    }

    /// <inheritdoc/>
    public string MoTa
    {
        get => txtMoTa.Text;
        set => txtMoTa.Text = value;
    }

    /// <inheritdoc/>
    public string TuKhoa => txtTimKiem.Text;

    /// <inheritdoc/>
    public void HienDanhSach(List<LoaiSach> danhSach)
    {
        dgvLoaiSach.DataSource = danhSach;
        lblTongSo.Text = $"Tổng: {danhSach.Count} loại sách";
    }

    /// <inheritdoc/>
    public void HienThongBao(string thongDiep) => ThongBao.ThanhCong(thongDiep);

    /// <inheritdoc/>
    public void HienLoi(string? truong, string thongDiep)
    {
        errLoaiSach.Clear();
        Control? oLoi = string.IsNullOrEmpty(truong) ? null : ThongBao.TimControl(this, truong);
        if (oLoi != null)
        {
            errLoaiSach.SetError(oLoi, thongDiep);
            oLoi.Focus();
        }

        ThongBao.CanhBao(thongDiep);
    }

    /// <inheritdoc/>
    public bool XacNhan(string thongDiep) => ThongBao.XacNhan(thongDiep);

    /// <inheritdoc/>
    public void XoaONhap()
    {
        txtTenLoai.Clear();
        txtMoTa.Clear();
        errLoaiSach.Clear();
    }

    /// <inheritdoc/>
    public void DatCheDo(bool dangSua)
    {
        txtTenLoai.ReadOnly = !dangSua;
        txtMoTa.ReadOnly = !dangSua;

        btnThem.Enabled = !dangSua;
        btnSua.Enabled = !dangSua;
        btnXoa.Enabled = !dangSua;
        btnLamMoi.Enabled = !dangSua;
        btnLuu.Enabled = dangSua;
        btnHuy.Enabled = dangSua;

        txtTimKiem.Enabled = !dangSua;
        dgvLoaiSach.Enabled = !dangSua;

        if (dangSua)
        {
            txtTenLoai.Focus();
        }
        else
        {
            errLoaiSach.Clear();
        }
    }

    // ===================== Sự kiện: mỗi hàm gọi một dòng Presenter =====================

    private void FrmLoaiSach_Load(object? sender, EventArgs e) => _presenter.TaiDanhSach();

    private void btnThem_Click(object? sender, EventArgs e) => _presenter.Them();

    private void btnSua_Click(object? sender, EventArgs e) => _presenter.Sua();

    private void btnXoa_Click(object? sender, EventArgs e) => _presenter.Xoa();

    private void btnLuu_Click(object? sender, EventArgs e) => _presenter.Luu();

    private void btnHuy_Click(object? sender, EventArgs e) => _presenter.Huy();

    private void btnLamMoi_Click(object? sender, EventArgs e) => _presenter.LamMoi();

    private void txtTimKiem_TextChanged(object? sender, EventArgs e) => _presenter.TimKiem();

    private void dgvLoaiSach_SelectionChanged(object? sender, EventArgs e) => _presenter.ChonDong();
}
