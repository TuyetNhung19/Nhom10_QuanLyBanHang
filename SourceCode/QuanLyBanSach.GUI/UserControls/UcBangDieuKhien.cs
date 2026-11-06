// File: QuanLyBanSach.GUI/UserControls/UcBangDieuKhien.cs
using System.Globalization;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Forms;

namespace QuanLyBanSach.GUI.UserControls;

/// <summary>
/// Bảng điều khiển trong ngày (ĐT5, TC22): 4 thẻ số liệu, biểu đồ doanh thu 7 ngày, top 5 sách bán chạy hôm nay.
/// Thu ngân thấy số liệu "Của bạn", Quản lý thấy "Toàn cửa hàng" (BLL tự ép mã thu ngân).
/// Tự làm mới khi có hóa đơn được lưu/hủy (event tĩnh của HoaDonBLL) và theo Timer 5 phút.
/// </summary>
public partial class UcBangDieuKhien : UserControl
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");
    private static readonly Color MauTheThuong = Helpers.GiaoDien.MauNhat;
    private static readonly Color MauTheCanhBao = Helpers.GiaoDien.MauNhanNhat;

    private readonly ThongKeBLL _thongKeBll = new();
    private bool _daDangKy;
    private bool _dangTai;
    private bool _canTaiLai;

    /// <summary>Khởi tạo bảng điều khiển.</summary>
    public UcBangDieuKhien()
    {
        InitializeComponent();
        colTopDoanhThu.DefaultCellStyle.FormatProvider = VanHoaVN;
        colTopSoLuong.DefaultCellStyle.FormatProvider = VanHoaVN;
    }

    /// <summary>
    /// Đăng ký nhận sự kiện hóa đơn, bật Timer và tải số liệu lần đầu.
    /// Không chạy trong Designer (DesignMode) vì khi đó chưa có phiên đăng nhập/CSDL.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode)
        {
            return;
        }

        lblTieuDe.Text = PhienDangNhap.LaThuNgan
            ? $"Bảng điều khiển – Của bạn ({PhienDangNhap.TaiKhoanHienTai?.HoTen})"
            : "Bảng điều khiển – Toàn cửa hàng";
        lblTop5.Text = PhienDangNhap.LaThuNgan ? "Top 5 sách bạn bán chạy hôm nay" : "Top 5 sách bán chạy hôm nay";

        DangKySuKien();
        tmrLamMoi.Start();
        _ = LamMoiAsync();
    }

    /// <summary>
    /// HỦY đăng ký event tĩnh khi handle bị hủy (Form đóng / control bị Dispose).
    /// </summary>
    /// <remarks>
    /// VÌ SAO PHẢI HỦY (câu hay được hỏi): HoaDonBLL.HoaDonDaLuu là event TĨNH, sống suốt vòng đời ứng dụng.
    /// Khi đăng ký "+=", delegate giữ tham chiếu tới chính UserControl này. Nếu đóng Form mà không "-=":
    ///  1) Rò rỉ bộ nhớ: GC không thu hồi được control (và cả Form chứa nó) vì event tĩnh vẫn trỏ tới.
    ///  2) Lỗi: lần lưu hóa đơn sau, handler vẫn chạy trên control đã Dispose → BeginInvoke ném
    ///     InvalidOperationException/ObjectDisposedException; mở/đóng bảng điều khiển N lần → handler chạy N lần.
    /// Hủy ở OnHandleDestroyed vì luôn được gọi khi Form đóng; bỏ qua khi RecreatingHandle
    /// (WinForms tạo lại handle khi đổi một số thuộc tính – control vẫn sống, không được hủy đăng ký).
    /// Dispose (trong Designer.cs) cũng hủy lần nữa cho chắc – "-=" hai lần không gây lỗi.
    /// </remarks>
    protected override void OnHandleDestroyed(EventArgs e)
    {
        if (!RecreatingHandle)
        {
            HuyDangKySuKien();
        }

        base.OnHandleDestroyed(e);
    }

    /// <summary>Đăng ký event tĩnh HoaDonDaLuu/HoaDonDaHuy (chỉ một lần).</summary>
    private void DangKySuKien()
    {
        if (_daDangKy)
        {
            return;
        }

        HoaDonBLL.HoaDonDaLuu += HoaDon_ThayDoi;
        HoaDonBLL.HoaDonDaHuy += HoaDon_ThayDoi;
        _daDangKy = true;
    }

    /// <summary>Hủy đăng ký event tĩnh và dừng Timer.</summary>
    private void HuyDangKySuKien()
    {
        HoaDonBLL.HoaDonDaLuu -= HoaDon_ThayDoi;
        HoaDonBLL.HoaDonDaHuy -= HoaDon_ThayDoi;
        _daDangKy = false;
        tmrLamMoi.Stop();
    }

    /// <summary>
    /// Có hóa đơn được lưu/hủy → làm mới. Event có thể được phát từ luồng khác luồng UI
    /// nên kiểm tra InvokeRequired và chuyển về luồng UI bằng BeginInvoke (không chờ, không khóa người bán).
    /// </summary>
    private void HoaDon_ThayDoi(object? sender, HoaDonEventArgs e)
    {
        if (IsDisposed || !IsHandleCreated)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => _ = LamMoiAsync()));
        }
        else
        {
            _ = LamMoiAsync();
        }
    }

    private void tmrLamMoi_Tick(object? sender, EventArgs e) => _ = LamMoiAsync();

    private void btnLamMoi_Click(object? sender, EventArgs e) => _ = LamMoiAsync();

    /// <summary>Làm mới toàn bộ số liệu (gọi được từ Form chứa, ví dụ phím F5).</summary>
    public void LamMoi() => _ = LamMoiAsync();

    /// <summary>
    /// Tải số liệu ở luồng nền (Task.Run) để giao diện không bị đứng; đang tải mà có yêu cầu mới thì
    /// đánh dấu tải lại một lần sau khi xong. Lỗi CSDL hiện bằng chữ nhỏ màu đỏ, KHÔNG MessageBox
    /// (bảng điều khiển tự làm mới, hộp thoại lặp lại mỗi 5 phút sẽ làm phiền thu ngân).
    /// </summary>
    private async Task LamMoiAsync()
    {
        if (_dangTai)
        {
            _canTaiLai = true;
            return;
        }

        _dangTai = true;
        btnLamMoi.Enabled = false;
        try
        {
            DateTime homNay = DateTime.Today;
            var (tongQuan, bayNgay, top5) = await Task.Run(() => (
                _thongKeBll.TongQuanTrongNgay(),
                _thongKeBll.DoanhThuTheoNgay(homNay.AddDays(-6), homNay),
                _thongKeBll.SachBanChay(homNay, homNay, 5)));

            if (IsDisposed)
            {
                return;
            }

            HienTheSoLieu(tongQuan);
            HienBieuDo7Ngay(homNay, bayNgay);
            dgvTop5.DataSource = top5;
            lblLoi.Text = string.Empty;
            lblCapNhatLuc.Text = "Cập nhật lúc: " + DateTime.Now.ToString("HH:mm:ss");
        }
        catch (Exception ex)
        {
            if (!IsDisposed)
            {
                lblLoi.Text = "Không tải được số liệu: " + HeThongBLL.MoTaLoi(ex);
            }
        }
        finally
        {
            _dangTai = false;
            if (!IsDisposed)
            {
                btnLamMoi.Enabled = true;
                if (_canTaiLai)
                {
                    _canTaiLai = false;
                    _ = LamMoiAsync();
                }
            }
        }
    }

    /// <summary>4 thẻ: doanh thu, số hóa đơn, số cuốn, sách sắp hết (cam khi &gt; 0).</summary>
    private void HienTheSoLieu(TongQuanNgay tq)
    {
        lblGiaTriDoanhThu.Text = tq.DoanhThu.ToString("#,##0", VanHoaVN) + "đ";
        lblGiaTriSoHoaDon.Text = tq.SoHoaDon.ToString("#,##0", VanHoaVN);
        lblGiaTriSoCuon.Text = tq.SoCuonBan.ToString("#,##0", VanHoaVN);
        lblGiaTriSapHet.Text = tq.SoSachSapHet.ToString("#,##0", VanHoaVN);

        bool canhBao = tq.SoSachSapHet > 0;
        pnlTheSapHet.BackColor = canhBao ? MauTheCanhBao : MauTheThuong;
        lblGiaTriSapHet.ForeColor = canhBao ? Helpers.GiaoDien.MauNhan : lblGiaTriDoanhThu.ForeColor;
    }

    /// <summary>
    /// Biểu đồ luôn đủ 7 cột: SP đã trả đủ dòng mỗi ngày, ở đây vẫn ghép theo lịch để chắc chắn
    /// (ngày không có hóa đơn → cột 0).
    /// </summary>
    private void HienBieuDo7Ngay(DateTime homNay, List<DoanhThuNgay> ds)
    {
        Dictionary<DateTime, decimal> theoNgay = ds
            .GroupBy(d => d.Ngay.Date)
            .ToDictionary(nhom => nhom.Key, nhom => nhom.Sum(d => d.DoanhThu));

        List<(string Nhan, decimal GiaTri)> duLieu = Enumerable.Range(0, 7)
            .Select(i => homNay.AddDays(i - 6))
            .Select(ngay => (ngay.ToString("dd/MM"), theoNgay.GetValueOrDefault(ngay)))
            .ToList();
        bdc7Ngay.GanDuLieu(duLieu);
    }

    /// <summary>Nhấp thẻ "Sách sắp hết" → mở danh sách sách sắp hết.</summary>
    private void theSapHet_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmSachSapHet();
        frm.ShowDialog(FindForm());
    }
}
