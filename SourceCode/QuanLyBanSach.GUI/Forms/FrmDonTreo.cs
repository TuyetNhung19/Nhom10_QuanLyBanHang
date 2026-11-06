// File: QuanLyBanSach.GUI/Forms/FrmDonTreo.cs
using System.Globalization;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;
using QuanLyBanSach.GUI.Presenters;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// ĐT2 – Danh sách đơn treo theo MVP: Form chỉ cài IDonTreoView, mọi xử lý nằm ở DonTreoPresenter.
/// Đóng với DialogResult.OK và IdDuocChon khi người dùng chọn gọi lại.
/// </summary>
public partial class FrmDonTreo : Form, IDonTreoView
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    private readonly DonTreoPresenter _presenter;

    /// <summary>Khởi tạo Form và Presenter.</summary>
    public FrmDonTreo()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        colTamTinh.DefaultCellStyle.FormatProvider = VanHoaVN;
        colCtDonGia.DefaultCellStyle.FormatProvider = VanHoaVN;
        colCtThanhTien.DefaultCellStyle.FormatProvider = VanHoaVN;
        _presenter = new DonTreoPresenter(this);
    }

    /// <summary>Mã đơn treo được chọn để gọi lại; null nếu đóng mà không chọn.</summary>
    public Guid? IdDuocChon { get; private set; }

    // ===================== IDonTreoView =====================

    /// <inheritdoc/>
    public Guid? IdDangChon => dgvDonTreo.CurrentRow?.DataBoundItem is HoaDonTam don ? don.Id : null;

    /// <inheritdoc/>
    public void HienDanhSach(List<HoaDonTam> danhSach, string soDon)
    {
        dgvDonTreo.DataSource = danhSach;
        lblSoDon.Text = soDon;
    }

    /// <inheritdoc/>
    public void HienChiTiet(List<ChiTietHoaDon>? chiTiet, string tieuDe, bool coDonDangChon)
    {
        dgvXemChiTiet.DataSource = chiTiet;
        lblXemChiTiet.Text = tieuDe;
        btnGoiLai.Enabled = coDonDangChon;
        btnHuyDon.Enabled = coDonDangChon;
    }

    /// <inheritdoc/>
    public bool XacNhan(string thongDiep) => ThongBao.XacNhan(thongDiep);

    /// <inheritdoc/>
    public void HienLoi(string thongDiep) => ThongBao.CanhBao(thongDiep);

    /// <inheritdoc/>
    public void DongVaGoiLai(Guid id)
    {
        IdDuocChon = id;
        DialogResult = DialogResult.OK;
    }

    // ===================== Sự kiện: chuyển cho Presenter =====================

    private void FrmDonTreo_Load(object? sender, EventArgs e) => _presenter.TaiDanhSach();

    private void dgvDonTreo_SelectionChanged(object? sender, EventArgs e) => _presenter.ChonDong();

    private void btnGoiLai_Click(object? sender, EventArgs e) => _presenter.GoiLai();

    private void btnHuyDon_Click(object? sender, EventArgs e) => _presenter.HuyDon();

    private void dgvDonTreo_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            _presenter.GoiLai();
        }
    }

    /// <summary>
    /// Enter trên lưới cũng gọi lại: DataGridView tự dùng Enter để xuống dòng nên AcceptButton không nhận được,
    /// vì vậy chặn ở ProcessCmdKey.
    /// </summary>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Enter && dgvDonTreo.Focused)
        {
            _presenter.GoiLai();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    /// <summary>Đơn treo lâu hơn 30 phút tô vàng (Presenter quyết định "lâu", Form chỉ tô màu).</summary>
    private void dgvDonTreo_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && e.CellStyle != null
            && dgvDonTreo.Rows[e.RowIndex].DataBoundItem is HoaDonTam don
            && DonTreoPresenter.LaTreoLau(don, DateTime.Now))
        {
            e.CellStyle.BackColor = Color.LightYellow;
        }
    }
}
