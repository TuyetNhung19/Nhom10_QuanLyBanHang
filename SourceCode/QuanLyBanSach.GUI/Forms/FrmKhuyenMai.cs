// File: QuanLyBanSach.GUI/Forms/FrmKhuyenMai.cs
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;
using QuanLyBanSach.GUI.Presenters;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Quản lý chương trình khuyến mãi (Quản lý – ĐT3) theo MVP: Form chỉ cài IKhuyenMaiView,
/// mọi xử lý (bật ô theo loại, xem trước, lưu, xóa có kiểm tra KM-04) nằm ở KhuyenMaiPresenter.
/// </summary>
public partial class FrmKhuyenMai : Form, IKhuyenMaiView
{
    private readonly KhuyenMaiPresenter _presenter;

    /// <summary>Khởi tạo Form và Presenter.</summary>
    public FrmKhuyenMai()
    {
        InitializeComponent();
        nudPhanTramGiam.Minimum = 0;
        nudPhanTramGiam.Maximum = 100;
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        _presenter = new KhuyenMaiPresenter(this);
    }

    // ===================== IKhuyenMaiView =====================

    /// <inheritdoc/>
    public int? MaKMDangChon => dgvKhuyenMai.CurrentRow?.DataBoundItem is DTO.KhuyenMai km ? km.MaKM : null;

    /// <inheritdoc/>
    public string TenKM
    {
        get => txtTenKM.Text;
        set => txtTenKM.Text = value;
    }

    /// <inheritdoc/>
    public LoaiKhuyenMai LoaiKM
    {
        get => cboLoaiKM.SelectedValue is LoaiKhuyenMai loai ? loai : LoaiKhuyenMai.TheoTongDon;
        set => cboLoaiKM.SelectedValue = value;
    }

    /// <inheritdoc/>
    public decimal NguongTien
    {
        get => nudNguongTien.Value;
        set => nudNguongTien.Value = GioiHan(nudNguongTien, value);
    }

    /// <inheritdoc/>
    public int PhanTramGiam
    {
        get => (int)nudPhanTramGiam.Value;
        set => nudPhanTramGiam.Value = GioiHan(nudPhanTramGiam, value);
    }

    /// <inheritdoc/>
    public int? MaLoaiSach
    {
        get => cboLoaiSach.SelectedValue is int maLoai ? maLoai : null;
        set
        {
            if (value == null)
            {
                cboLoaiSach.SelectedIndex = -1;
            }
            else
            {
                cboLoaiSach.SelectedValue = value.Value;
            }
        }
    }

    /// <inheritdoc/>
    public string? TenLoaiSach => (cboLoaiSach.SelectedItem as LoaiSach)?.TenLoai;

    /// <inheritdoc/>
    public int NguongSoLuong
    {
        get => (int)nudNguongSoLuong.Value;
        set => nudNguongSoLuong.Value = GioiHan(nudNguongSoLuong, value);
    }

    /// <inheritdoc/>
    public decimal SoTienGiam
    {
        get => nudSoTienGiam.Value;
        set => nudSoTienGiam.Value = GioiHan(nudSoTienGiam, value);
    }

    /// <inheritdoc/>
    public DateTime NgayBatDau
    {
        get => dtpBatDau.Value;
        set => dtpBatDau.Value = value;
    }

    /// <inheritdoc/>
    public DateTime NgayKetThuc
    {
        get => dtpKetThuc.Value;
        set => dtpKetThuc.Value = value;
    }

    /// <inheritdoc/>
    public bool DangBat
    {
        get => chkDangBat.Checked;
        set => chkDangBat.Checked = value;
    }

    /// <inheritdoc/>
    public void HienDanhMuc(List<KeyValuePair<LoaiKhuyenMai, string>> dsLoaiKM, List<LoaiSach> dsLoaiSach)
    {
        cboLoaiKM.DisplayMember = nameof(KeyValuePair<LoaiKhuyenMai, string>.Value);
        cboLoaiKM.ValueMember = nameof(KeyValuePair<LoaiKhuyenMai, string>.Key);
        cboLoaiKM.DataSource = dsLoaiKM;

        cboLoaiSach.DisplayMember = nameof(LoaiSach.TenLoai);
        cboLoaiSach.ValueMember = nameof(LoaiSach.MaLoai);
        cboLoaiSach.DataSource = dsLoaiSach;
    }

    /// <inheritdoc/>
    public void HienDanhSach(List<DTO.KhuyenMai> danhSach, int? maKMChon)
    {
        dgvKhuyenMai.DataSource = danhSach;

        if (maKMChon == null)
        {
            return;
        }

        foreach (DataGridViewRow dong in dgvKhuyenMai.Rows)
        {
            if (dong.DataBoundItem is DTO.KhuyenMai km && km.MaKM == maKMChon)
            {
                dgvKhuyenMai.CurrentCell = dong.Cells[colTenKM.Index];
                break;
            }
        }
    }

    /// <inheritdoc/>
    public void HienXemTruoc(string moTa) => lblMoTa.Text = moTa;

    /// <summary>
    /// Xem: Thêm/Sửa/Xóa bật, ô nhập khóa. Đang nhập: chỉ Lưu/Hủy, mở các ô chung
    /// (các ô riêng theo loại do BatONhapTheoLoai quyết định).
    /// </summary>
    public void DatCheDo(bool dangNhap, bool coDongDangChon)
    {
        txtTenKM.ReadOnly = !dangNhap;
        cboLoaiKM.Enabled = dangNhap;
        dtpBatDau.Enabled = dangNhap;
        dtpKetThuc.Enabled = dangNhap;
        chkDangBat.Enabled = dangNhap;

        btnThem.Enabled = !dangNhap;
        btnSua.Enabled = !dangNhap && coDongDangChon;
        btnXoa.Enabled = !dangNhap && coDongDangChon;
        btnLamMoi.Enabled = !dangNhap;
        btnLuu.Enabled = dangNhap;
        btnHuy.Enabled = dangNhap;
        dgvKhuyenMai.Enabled = !dangNhap;

        if (dangNhap)
        {
            txtTenKM.Focus();
        }
        else
        {
            errKhuyenMai.Clear();
        }
    }

    /// <inheritdoc/>
    public void BatONhapTheoLoai(bool nguongTien, bool loaiSach, bool phanTram, bool soLuong)
    {
        nudNguongTien.Enabled = nguongTien;
        cboLoaiSach.Enabled = loaiSach;
        nudPhanTramGiam.Enabled = phanTram;
        nudNguongSoLuong.Enabled = soLuong;
        nudSoTienGiam.Enabled = soLuong;
    }

    /// <inheritdoc/>
    public void XoaLoi() => errKhuyenMai.Clear();

    /// <inheritdoc/>
    public void HienThongBao(string thongDiep) => ThongBao.ThanhCong(thongDiep);

    /// <inheritdoc/>
    public void HienLoi(string? truong, string thongDiep)
    {
        errKhuyenMai.Clear();
        Control? oLoi = string.IsNullOrEmpty(truong) ? null : ThongBao.TimControl(this, truong);
        if (oLoi != null)
        {
            errKhuyenMai.SetError(oLoi, thongDiep);
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

    // ===================== Sự kiện: chuyển cho Presenter =====================

    private void FrmKhuyenMai_Load(object? sender, EventArgs e) => _presenter.KhoiTao();

    private void btnThem_Click(object? sender, EventArgs e) => _presenter.Them();

    private void btnSua_Click(object? sender, EventArgs e) => _presenter.Sua();

    private void btnXoa_Click(object? sender, EventArgs e) => _presenter.Xoa();

    private void btnLuu_Click(object? sender, EventArgs e) => _presenter.Luu();

    private void btnHuy_Click(object? sender, EventArgs e) => _presenter.Huy();

    private void btnLamMoi_Click(object? sender, EventArgs e) => _presenter.LamMoi();

    private void dgvKhuyenMai_SelectionChanged(object? sender, EventArgs e) => _presenter.ChonDong();

    private void cboLoaiKM_SelectedIndexChanged(object? sender, EventArgs e) => _presenter.DoiLoaiKhuyenMai();

    /// <summary>Mọi ô nhập đổi giá trị (tên, số, loại sách, ngày, bật/tắt) đều gắn vào hàm này.</summary>
    private void GiaTriThayDoi(object? sender, EventArgs e) => _presenter.GiaTriThayDoi();

    /// <summary>
    /// Cột "Tình trạng" (không gắn dữ liệu): Presenter tính tình trạng, Form đổi thành chữ + màu.
    /// </summary>
    private void dgvKhuyenMai_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != colTinhTrang.Index || e.CellStyle == null
            || dgvKhuyenMai.Rows[e.RowIndex].DataBoundItem is not DTO.KhuyenMai km)
        {
            return;
        }

        (string chu, Color mau) = KhuyenMaiPresenter.TinhTrang(km, DateTime.Today) switch
        {
            TinhTrangKhuyenMai.DangTat => ("Đang tắt", Color.DarkOrange),
            TinhTrangKhuyenMai.ChuaBatDau => ("Chưa bắt đầu", Color.SteelBlue),
            TinhTrangKhuyenMai.HetHan => ("Hết hạn", Color.Gray),
            _ => ("Đang hiệu lực", Color.ForestGreen)
        };

        e.Value = chu;
        e.CellStyle.ForeColor = mau;
        e.CellStyle.SelectionForeColor = mau;
        e.FormattingApplied = true;
    }

    /// <summary>Đưa giá trị vào khoảng [Minimum, Maximum] của NumericUpDown.</summary>
    private static decimal GioiHan(NumericUpDown nud, decimal giaTri) => Math.Clamp(giaTri, nud.Minimum, nud.Maximum);
}
