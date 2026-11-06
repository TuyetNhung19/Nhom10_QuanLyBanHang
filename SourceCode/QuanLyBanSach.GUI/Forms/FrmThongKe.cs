// File: QuanLyBanSach.GUI/Forms/FrmThongKe.cs
using System.Collections;
using System.Globalization;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;
using QuanLyBanSach.GUI.Printing;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Thống kê doanh thu (Quản lý – đặc tả 3.6): doanh thu theo ngày/tháng, sách bán chạy, sách sắp hết,
/// doanh thu theo thu ngân, hiệu quả khuyến mãi. Mọi số liệu lấy từ hóa đơn hoàn tất, KHÔNG tính hóa đơn hủy (TK-03, TC14).
/// </summary>
public partial class FrmThongKe : Form
{
    /// <summary>
    /// Số dòng lấy về khi xếp sách bán chạy theo doanh thu: SP xếp theo số lượng nên lấy rộng rồi
    /// sắp xếp lại bằng LINQ và cắt Top N, tránh bỏ sót sách bán ít cuốn nhưng giá cao.
    /// </summary>
    private const int SoDongLayRong = 1000;

    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    private readonly ThongKeBLL _thongKeBll = new();
    private readonly SachBLL _sachBll = new();
    private readonly LoaiSachBLL _loaiSachBll = new();

    private List<DoanhThuNgay> _doanhThuNgay = new();
    private List<DoanhThuThang> _doanhThuThang = new();
    private List<SachBanChay> _banChayGoc = new();
    private List<SachBanChay> _banChay = new();
    private List<Sach> _sapHet = new();
    private List<DoanhThuThuNgan> _thuNgan = new();
    private List<HieuQuaKhuyenMai> _khuyenMai = new();

    /// <summary>Khởi tạo Form thống kê.</summary>
    public FrmThongKe()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)

        // Tiền định dạng N0 theo vi-VN (dấu chấm ngăn nghìn) cho mọi cột tiền/số
        foreach (DataGridView dgv in new[] { dgvDoanhThu, dgvBanChay, dgvSapHet, dgvThuNgan, dgvKhuyenMai })
        {
            foreach (DataGridViewColumn cot in dgv.Columns)
            {
                cot.DefaultCellStyle.FormatProvider = VanHoaVN;
            }
        }
    }

    private void FrmThongKe_Load(object? sender, EventArgs e)
    {
        if (!PhienDangNhap.LaQuanLy)
        {
            ThongBao.CanhBao("Chức năng thống kê chỉ dành cho Quản lý.");
            BeginInvoke(Close);
            return;
        }

        DatKhoangNgay(DateTime.Today.AddDays(-6), DateTime.Today);
        try
        {
            var dsLoai = new List<LoaiSach> { new() { MaLoai = 0, TenLoai = "Tất cả loại" } };
            dsLoai.AddRange(_loaiSachBll.LayDanhSach());
            cboLoaiSach.DisplayMember = nameof(LoaiSach.TenLoai);
            cboLoaiSach.ValueMember = nameof(LoaiSach.MaLoai);
            cboLoaiSach.DataSource = dsLoai;
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }

        TaiTatCa();
    }

    // ===================== Khoảng ngày =====================

    private void DatKhoangNgay(DateTime tu, DateTime den)
    {
        dtpTuNgay.Value = tu;
        dtpDenNgay.Value = den;
    }

    private void btnHomNay_Click(object? sender, EventArgs e)
    {
        DatKhoangNgay(DateTime.Today, DateTime.Today);
        TaiTatCa();
    }

    private void btn7Ngay_Click(object? sender, EventArgs e)
    {
        DatKhoangNgay(DateTime.Today.AddDays(-6), DateTime.Today);
        TaiTatCa();
    }

    private void btnThangNay_Click(object? sender, EventArgs e)
    {
        DatKhoangNgay(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);
        TaiTatCa();
    }

    private void btnXem_Click(object? sender, EventArgs e) => TaiTatCa();

    /// <summary>
    /// Nạp lại số liệu cả 5 tab theo khoảng ngày. Từ ngày &gt; đến ngày → BLL ném BusinessException("DenNgay").
    /// </summary>
    private void TaiTatCa()
    {
        errThongKe.Clear();
        Cursor = Cursors.WaitCursor;
        try
        {
            DateTime tu = dtpTuNgay.Value.Date;
            DateTime den = dtpDenNgay.Value.Date;

            _doanhThuNgay = _thongKeBll.DoanhThuTheoNgay(tu, den);
            _doanhThuThang = _thongKeBll.DoanhThuTheoThang(tu, den);
            _banChayGoc = _thongKeBll.SachBanChay(tu, den, SoDongLayRong);
            _thuNgan = _thongKeBll.DoanhThuTheoThuNgan(tu, den);
            _khuyenMai = _thongKeBll.HieuQuaKhuyenMai(tu, den);

            HienDoanhThu();
            HienBanChay();
            HienThuNgan();
            dgvKhuyenMai.DataSource = _khuyenMai;
            TaiSachSapHet();
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex, errThongKe, this);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    // ===================== Tab Doanh thu =====================

    private void rdoKieuDoanhThu_CheckedChanged(object? sender, EventArgs e)
    {
        // Sự kiện bắn 2 lần (nút bỏ chọn + nút được chọn) → chỉ xử lý ở nút được chọn
        if (sender is RadioButton { Checked: true })
        {
            HienDoanhThu();
        }
    }

    /// <summary>
    /// Hiện bảng + biểu đồ doanh thu theo ngày hoặc theo tháng và dòng tổng.
    /// Tổng lấy từ chính danh sách đang hiện nên luôn khớp bảng (TC14).
    /// </summary>
    private void HienDoanhThu()
    {
        bool theoNgay = rdoTheoNgay.Checked;
        colDtNhan.HeaderText = theoNgay ? "Ngày" : "Tháng";
        colDtNhan.DataPropertyName = theoNgay ? nameof(DoanhThuNgay.NhanNgay) : nameof(DoanhThuThang.NhanThang);
        colDtTamTinh.Visible = theoNgay;

        int soHoaDon;
        decimal tongGiam;
        decimal doanhThu;
        if (theoNgay)
        {
            dgvDoanhThu.DataSource = _doanhThuNgay;
            bdcDoanhThu.TieuDe = "Doanh thu theo ngày";
            bdcDoanhThu.GanDuLieu(_doanhThuNgay.Select(d => (d.NhanNgay, d.DoanhThu)).ToList());
            soHoaDon = _doanhThuNgay.Sum(d => d.SoHoaDon);
            tongGiam = _doanhThuNgay.Sum(d => d.TongGiamGia);
            doanhThu = _doanhThuNgay.Sum(d => d.DoanhThu);
        }
        else
        {
            dgvDoanhThu.DataSource = _doanhThuThang;
            bdcDoanhThu.TieuDe = "Doanh thu theo tháng";
            bdcDoanhThu.GanDuLieu(_doanhThuThang.Select(d => (d.NhanThang, d.DoanhThu)).ToList());
            soHoaDon = _doanhThuThang.Sum(d => d.SoHoaDon);
            tongGiam = _doanhThuThang.Sum(d => d.TongGiamGia);
            doanhThu = _doanhThuThang.Sum(d => d.DoanhThu);
        }

        lblTongDoanhThu.Text = $"Tổng: {soHoaDon.ToString("N0", VanHoaVN)} hóa đơn   |   Tổng giảm giá: {Tien(tongGiam)}   |   " +
                               $"Doanh thu: {Tien(doanhThu)}   (không tính hóa đơn hủy)";
    }

    // ===================== Tab Sách bán chạy =====================

    private void rdoKieuBanChay_CheckedChanged(object? sender, EventArgs e)
    {
        if (sender is RadioButton { Checked: true })
        {
            HienBanChay();
        }
    }

    private void nudTop_ValueChanged(object? sender, EventArgs e) => HienBanChay();

    /// <summary>Sắp xếp theo số lượng hoặc doanh thu (LINQ) rồi lấy Top N.</summary>
    private void HienBanChay()
    {
        bool theoSoLuong = rdoTheoSoLuong.Checked;
        IOrderedEnumerable<SachBanChay> sapXep = theoSoLuong
            ? _banChayGoc.OrderByDescending(s => s.SoLuongBan).ThenByDescending(s => s.DoanhThu)
            : _banChayGoc.OrderByDescending(s => s.DoanhThu).ThenByDescending(s => s.SoLuongBan);
        _banChay = sapXep.ThenBy(s => s.TenSach).Take((int)nudTop.Value).ToList();

        dgvBanChay.DataSource = _banChay;
        bdcBanChay.LaTien = !theoSoLuong;
        bdcBanChay.DonViTruc = theoSoLuong ? "cuốn" : "nghìn đồng";
        bdcBanChay.HeSoTruc = theoSoLuong ? 1 : 1000;
        bdcBanChay.TieuDe = $"Top {_banChay.Count} sách bán chạy theo {(theoSoLuong ? "số lượng" : "doanh thu")}";
        bdcBanChay.GanDuLieu(_banChay
            .Select(s => (RutGonTen(s.TenSach), theoSoLuong ? s.SoLuongBan : s.DoanhThu))
            .ToList());
    }

    /// <summary>Tên sách dài làm nhãn trục X quá rộng → cắt còn 18 ký tự (ToolTip vẫn hiện số đầy đủ).</summary>
    private static string RutGonTen(string ten) => ten.Length <= 18 ? ten : ten[..17] + "…";

    // ===================== Tab Sách sắp hết =====================

    private void cboLoaiSach_SelectionChangeCommitted(object? sender, EventArgs e)
    {
        try
        {
            TaiSachSapHet();
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    /// <summary>Sách có tồn ≤ tồn tối thiểu (SP-04), lọc theo loại; không phụ thuộc khoảng ngày.</summary>
    private void TaiSachSapHet()
    {
        int? maLoai = cboLoaiSach.SelectedValue is int ma && ma > 0 ? ma : null;
        _sapHet = _sachBll.LaySachSapHet(maLoai);
        dgvSapHet.DataSource = _sapHet;
        tabSachSapHet.Text = $"Sách sắp hết ({_sapHet.Count})";
    }

    private void btnInSapHet_Click(object? sender, EventArgs e)
    {
        if (_sapHet.Count == 0)
        {
            ThongBao.ThongTin("Không có sách nào sắp hết.");
            return;
        }

        try
        {
            using var boIn = new SachSapHetPrinter(_sapHet);
            if (!boIn.TaiLieu.PrinterSettings.IsValid)
            {
                ThongBao.Loi("Máy chưa cài máy in nào (kể cả Microsoft Print to PDF) nên không thể xem trước/in.");
                return;
            }

            using var xemTruoc = new PrintPreviewDialog
            {
                Document = boIn.TaiLieu,
                Width = 900,
                Height = 760,
                StartPosition = FormStartPosition.CenterParent,
                UseAntiAlias = true,
                Text = "Xem trước – Danh sách sách sắp hết"
            };
            xemTruoc.ShowDialog(this);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    // ===================== Tab Thu ngân =====================

    private void HienThuNgan()
    {
        dgvThuNgan.DataSource = _thuNgan;
        bdcThuNgan.GanDuLieu(_thuNgan.Select(t => (t.HoTen, t.DoanhThu)).ToList());
    }

    // ===================== Xuất CSV =====================

    /// <summary>Xuất dữ liệu của tab đang chọn ra CSV (UTF-8 có BOM, mở được bằng Excel).</summary>
    private void btnXuatCsv_Click(object? sender, EventArgs e)
    {
        string tuDen = $"{dtpTuNgay.Value:yyyyMMdd}_{dtpDenNgay.Value:yyyyMMdd}";
        (IEnumerable duLieu, string tenFile) = tabThongKe.SelectedTab?.Name switch
        {
            nameof(tabDoanhThu) when rdoTheoNgay.Checked => ((IEnumerable)_doanhThuNgay, $"DoanhThuTheoNgay_{tuDen}"),
            nameof(tabDoanhThu) => (_doanhThuThang, $"DoanhThuTheoThang_{tuDen}"),
            nameof(tabSachBanChay) => (_banChay, $"SachBanChay_{tuDen}"),
            nameof(tabSachSapHet) => (_sapHet, $"SachSapHet_{DateTime.Today:yyyyMMdd}"),
            nameof(tabThuNgan) => (_thuNgan, $"DoanhThuThuNgan_{tuDen}"),
            _ => (_khuyenMai, $"HieuQuaKhuyenMai_{tuDen}")
        };

        if (!duLieu.Cast<object>().Any())
        {
            ThongBao.ThongTin("Tab đang chọn chưa có dữ liệu để xuất. Hãy bấm Xem trước.");
            return;
        }

        using var hopLuu = new SaveFileDialog
        {
            Filter = "Tệp CSV (*.csv)|*.csv",
            FileName = tenFile + ".csv",
            Title = "Xuất CSV",
            OverwritePrompt = true
        };
        if (hopLuu.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            switch (duLieu)
            {
                case List<DoanhThuNgay> ds: _thongKeBll.XuatCsv(ds, hopLuu.FileName); break;
                case List<DoanhThuThang> ds: _thongKeBll.XuatCsv(ds, hopLuu.FileName); break;
                case List<SachBanChay> ds: _thongKeBll.XuatCsv(ds, hopLuu.FileName); break;
                case List<Sach> ds: _thongKeBll.XuatCsv(ds, hopLuu.FileName); break;
                case List<DoanhThuThuNgan> ds: _thongKeBll.XuatCsv(ds, hopLuu.FileName); break;
                case List<HieuQuaKhuyenMai> ds: _thongKeBll.XuatCsv(ds, hopLuu.FileName); break;
            }

            ThongBao.ThanhCong("Đã xuất: " + hopLuu.FileName);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    private static string Tien(decimal so) => so.ToString("#,##0", VanHoaVN) + "đ";
}
