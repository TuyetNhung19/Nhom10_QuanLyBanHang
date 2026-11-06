// File: QuanLyBanSach.GUI/Forms/FrmTraCuuHoaDon.cs
using System.Globalization;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Tra cứu hóa đơn (master-detail), in lại, hủy hóa đơn (chỉ Quản lý – HD-06, TC11, TC12).
/// </summary>
public partial class FrmTraCuuHoaDon : Form
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    private readonly HoaDonBLL _hoaDonBll = new();
    private readonly TaiKhoanBLL _taiKhoanBll = new();
    private readonly Font _chuGachNgang;

    /// <summary>Khởi tạo Form tra cứu hóa đơn.</summary>
    public FrmTraCuuHoaDon()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        _chuGachNgang = new Font(dgvHoaDon.Font, FontStyle.Strikeout);
        colTong.DefaultCellStyle.FormatProvider = VanHoaVN;
        colCtDonGia.DefaultCellStyle.FormatProvider = VanHoaVN;
        colCtThanhTien.DefaultCellStyle.FormatProvider = VanHoaVN;
    }

    private HoaDon? HoaDonDangChon => dgvHoaDon.CurrentRow?.DataBoundItem as HoaDon;

    private void FrmTraCuuHoaDon_Load(object? sender, EventArgs e)
    {
        dtpTuNgay.Value = DateTime.Today.AddDays(-7);
        dtpDenNgay.Value = DateTime.Today;
        cboTrangThai.SelectedIndex = 0;

        // Hủy hóa đơn chỉ dành cho Quản lý (BLL kiểm tra lại)
        btnHuyHoaDon.Visible = PhienDangNhap.LaQuanLy;

        try
        {
            var dsThuNgan = new List<TaiKhoan> { new() { MaTK = 0, HoTen = "Tất cả" } };
            dsThuNgan.AddRange(_taiKhoanBll.LayDanhSachNguoiBan());
            cboThuNgan.DisplayMember = nameof(TaiKhoan.HoTen);
            cboThuNgan.ValueMember = nameof(TaiKhoan.MaTK);
            cboThuNgan.DataSource = dsThuNgan;
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }

        TimKiem();
    }

    private void btnTim_Click(object? sender, EventArgs e) => TimKiem();

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtMaHD.Clear();
        txtKhach.Clear();
        dtpTuNgay.Value = DateTime.Today.AddDays(-7);
        dtpDenNgay.Value = DateTime.Today;
        cboTrangThai.SelectedIndex = 0;
        if (cboThuNgan.Items.Count > 0)
        {
            cboThuNgan.SelectedIndex = 0;
        }

        TimKiem();
    }

    /// <summary>
    /// Tra cứu theo bộ lọc (sp_TraCuuHoaDon). Có mã hóa đơn thì SP bỏ qua khoảng ngày.
    /// Từ ngày &gt; đến ngày → báo lỗi bằng ErrorProvider.
    /// </summary>
    private void TimKiem(string? maHDChon = null)
    {
        errTraCuu.Clear();
        if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
        {
            errTraCuu.SetError(dtpDenNgay, "Đến ngày phải sau hoặc bằng từ ngày.");
            dtpDenNgay.Focus();
            return;
        }

        try
        {
            string? maDangChon = maHDChon ?? HoaDonDangChon?.MaHD;
            byte? trangThai = cboTrangThai.SelectedIndex switch
            {
                1 => (byte)TrangThaiHoaDon.HoanTat,
                2 => (byte)TrangThaiHoaDon.DaHuy,
                _ => null
            };
            int? maTK = cboThuNgan.SelectedValue is int ma && ma > 0 ? ma : null;

            List<HoaDon> ds = _hoaDonBll.TraCuu(txtMaHD.Text, txtKhach.Text, dtpTuNgay.Value, dtpDenNgay.Value, maTK, trangThai);
            dgvHoaDon.DataSource = ds;

            List<HoaDon> hoanTat = ds.Where(hd => !hd.DaHuy).ToList();
            lblTong.Text = $"{ds.Count} hóa đơn – doanh thu {Tien(hoanTat.Sum(hd => hd.TongThanhToan))} (không tính hóa đơn hủy)";

            if (maDangChon != null)
            {
                foreach (DataGridViewRow dong in dgvHoaDon.Rows)
                {
                    if (dong.DataBoundItem is HoaDon hd && hd.MaHD == maDangChon)
                    {
                        dgvHoaDon.CurrentCell = dong.Cells[colMaHD.Index];
                        break;
                    }
                }
            }

            HienChiTiet();
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex, errTraCuu, this);
        }
    }

    private void dgvHoaDon_SelectionChanged(object? sender, EventArgs e) => HienChiTiet();

    /// <summary>Hiện chi tiết và nhóm tổng hợp của hóa đơn đang chọn (đọc đầy đủ từ CSDL).</summary>
    private void HienChiTiet()
    {
        HoaDon? hd = HoaDonDangChon;
        btnInLai.Enabled = hd != null;
        btnHuyHoaDon.Enabled = hd != null && !hd.DaHuy;

        if (hd == null)
        {
            dgvChiTiet.DataSource = null;
            DatTongHop(null);
            return;
        }

        try
        {
            HoaDon? day = _hoaDonBll.LayChiTiet(hd.MaHD);
            dgvChiTiet.DataSource = day?.ChiTiet;
            DatTongHop(day);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    /// <summary>Điền nhóm tổng hợp: tạm tính, giảm, tổng, tiền đưa, tiền thối, lý do hủy.</summary>
    private void DatTongHop(HoaDon? hd)
    {
        grpTongHop.Text = hd == null ? "Tổng hợp" : $"Tổng hợp – {hd.MaHD}";
        lblTamTinh.Text = hd == null ? string.Empty : Tien(hd.TamTinh);
        lblGiamGia.Text = hd == null ? string.Empty : (hd.GiamGia > 0 ? "−" + Tien(hd.GiamGia) : Tien(0));
        lblTenKM.Text = hd?.TenKM ?? string.Empty;
        lblTongTT.Text = hd == null ? string.Empty : Tien(hd.TongThanhToan);
        lblTienDua.Text = hd == null ? string.Empty : Tien(hd.TienKhachDua);
        lblTienThoi.Text = hd == null ? string.Empty : Tien(hd.TienThoi);
        lblLyDoHuy.Text = hd is { DaHuy: true }
    ? "ĐÃ HỦY – Lý do: " + hd.LyDoHuy
      + (string.IsNullOrEmpty(hd.GhiChu) ? string.Empty : "\n" + hd.GhiChu)
    : string.Empty;
    }

    /// <summary>Hóa đơn đã hủy: chữ đỏ gạch ngang.</summary>
    private void dgvHoaDon_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && e.CellStyle != null
            && dgvHoaDon.Rows[e.RowIndex].DataBoundItem is HoaDon hd && hd.DaHuy)
        {
            e.CellStyle.ForeColor = Color.Red;
            e.CellStyle.SelectionForeColor = Color.MistyRose;
            e.CellStyle.Font = _chuGachNgang;
        }
    }

    private void dgvHoaDon_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            InLai();
        }
    }

    private void btnInLai_Click(object? sender, EventArgs e) => InLai();

    /// <summary>In lại hóa đơn đang chọn (bản in có dòng "BẢN IN LẠI").</summary>
    private void InLai()
    {
        if (HoaDonDangChon is not { } hd)
        {
            return;
        }

        using var frm = new FrmInHoaDon(hd.MaHD, banInLai: true);
        frm.ShowDialog(this);
    }

    /// <summary>
    /// Hủy hóa đơn (Quản lý): nhập lý do qua FrmNhapChuoi, BLL kiểm tra ≥ 5 ký tự, SP hoàn tồn kho (TC11).
    /// </summary>
    private void btnHuyHoaDon_Click(object? sender, EventArgs e)
    {
        if (HoaDonDangChon is not { } hd || hd.DaHuy)
        {
            return;
        }

        using var frm = new FrmNhapChuoi("Hủy hóa đơn",
            $"Lý do hủy hóa đơn {hd.MaHD} – {Tien(hd.TongThanhToan)} (ít nhất {HoaDonBLL.DoDaiLyDoToiThieu} ký tự):");
        if (frm.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        if (!ThongBao.XacNhan($"Hủy hóa đơn {hd.MaHD}? Tồn kho các sách trong hóa đơn sẽ được hoàn lại."))
        {
            return;
        }

        try
        {
            _hoaDonBll.HuyHoaDon(hd.MaHD, frm.GiaTri);
            ThongBao.ThanhCong($"Đã hủy hóa đơn {hd.MaHD}.");
            TimKiem(hd.MaHD);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    private void FrmTraCuuHoaDon_FormClosed(object? sender, FormClosedEventArgs e)
    {
        _chuGachNgang.Dispose();
    }

    private static string Tien(decimal soTien) => soTien.ToString("#,##0", VanHoaVN) + "đ";
}
