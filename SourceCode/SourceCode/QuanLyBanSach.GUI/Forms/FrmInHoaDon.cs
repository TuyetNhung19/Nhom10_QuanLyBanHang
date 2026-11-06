// File: QuanLyBanSach.GUI/Forms/FrmInHoaDon.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;
using QuanLyBanSach.GUI.Printing;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Xem trước và in hóa đơn (K80 hoặc A5). Dữ liệu luôn đọc lại từ CSDL qua HoaDonBLL.LayChiTiet
/// để bản in khớp đúng dữ liệu đã lưu (TC13).
/// </summary>
public partial class FrmInHoaDon : Form
{
    private readonly HoaDonBLL _hoaDonBll = new();
    private readonly string _maHD;
    private readonly bool _banInLai;

    private HoaDon? _hoaDon;
    private HoaDonPrinter? _boIn;

    /// <summary>Constructor mặc định cho Designer.</summary>
    public FrmInHoaDon() : this(string.Empty, false)
    {
    }

    /// <summary>Mở xem trước bản in của hóa đơn.</summary>
    /// <param name="maHD">Mã hóa đơn đã lưu.</param>
    /// <param name="banInLai">true: in lại từ màn hình tra cứu (thêm dòng "BẢN IN LẠI").</param>
    public FrmInHoaDon(string maHD, bool banInLai)
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        _maHD = maHD;
        _banInLai = banInLai;
        Text = (banInLai ? "In lại hóa đơn " : "In hóa đơn ") + maHD;
    }

    private void FrmInHoaDon_Load(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_maHD))
        {
            return;
        }

        try
        {
            _hoaDon = _hoaDonBll.LayChiTiet(_maHD);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }

        if (_hoaDon == null)
        {
            ThongBao.Loi($"Không đọc được hóa đơn {_maHD}.");
            BeginInvoke(new Action(Close));
            return;
        }

        cboKhoGiay.SelectedIndex = 0;
    }

    private void cboKhoGiay_SelectedIndexChanged(object? sender, EventArgs e) => TaoBanXemTruoc();

    /// <summary>
    /// Tạo bộ in theo khổ giấy đang chọn và gắn vào PrintPreviewControl (AutoZoom = vừa trang).
    /// Máy chưa cài máy in nào thì không xem trước được (PrintPreviewControl cần máy in mặc định).
    /// </summary>
    private void TaoBanXemTruoc()
    {
        if (_hoaDon == null)
        {
            return;
        }

        KhoGiayHoaDon kho = cboKhoGiay.SelectedIndex == 1 ? KhoGiayHoaDon.A5 : KhoGiayHoaDon.K80;
        HoaDonPrinter? cu = _boIn;
        _boIn = new HoaDonPrinter(_hoaDon, _banInLai, kho);

        if (!_boIn.TaiLieu.PrinterSettings.IsValid)
        {
            ppcXemTruoc.Document = null;
            btnIn.Enabled = false;
            lblThongTin.Text = "Máy chưa cài máy in nào (kể cả Microsoft Print to PDF) – không thể xem trước/in.";
            lblThongTin.ForeColor = Color.Red;
        }
        else
        {
            ppcXemTruoc.Document = _boIn.TaiLieu;
            ppcXemTruoc.AutoZoom = true;
            ppcXemTruoc.InvalidatePreview();
            btnIn.Enabled = true;
            float rongMm = _boIn.KichThuocGiay.Width * 0.254F;
            float caoMm = _boIn.KichThuocGiay.Height * 0.254F;
            lblThongTin.Text = $"Khổ {(kho == KhoGiayHoaDon.K80 ? "K80" : "A5")}: {rongMm:0} × {caoMm:0} mm";
            lblThongTin.ForeColor = SystemColors.ControlText;
        }

        cu?.Dispose();
    }

    /// <summary>Chọn máy in bằng PrintDialog rồi in.</summary>
    private void btnIn_Click(object? sender, EventArgs e)
    {
        if (_boIn == null)
        {
            return;
        }

        using var hopThoai = new PrintDialog
        {
            Document = _boIn.TaiLieu,
            UseEXDialog = true,
            AllowSomePages = false
        };

        if (hopThoai.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            _boIn.TaiLieu.Print();
        }
        catch (Exception ex)
        {
            ThongBao.Loi("Không in được hóa đơn: " + ex.Message);
        }
    }

    private void FrmInHoaDon_FormClosed(object? sender, FormClosedEventArgs e)
    {
        ppcXemTruoc.Document = null;
        _boIn?.Dispose();
        _boIn = null;
    }
}
