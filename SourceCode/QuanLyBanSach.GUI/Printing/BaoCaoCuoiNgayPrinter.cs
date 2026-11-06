// File: QuanLyBanSach.GUI/Printing/BaoCaoCuoiNgayPrinter.cs
using System.Configuration;
using System.Drawing.Printing;
using System.Globalization;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.GUI.Printing;

/// <summary>
/// In báo cáo cuối ngày (ĐT6) trên MỘT trang A4: tiêu đề, ngày, thu ngân, người in + giờ in;
/// bảng tổng hợp; bảng giảm giá theo khuyến mãi; danh sách hóa đơn hủy kèm lý do; dòng ký Thu ngân / Quản lý;
/// ghi chú "Báo cáo tổng hợp, không thay thế đối soát ca". Đơn vị vẽ: 1/100 inch (mặc định của PrintDocument).
/// </summary>
public sealed class BaoCaoCuoiNgayPrinter : IDisposable
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    /// <summary>Chiều cao dành sẵn cho phần ký tên + ghi chú cuối trang (1/100 inch).</summary>
    private const float CaoPhanKy = 150F;

    private readonly BaoCaoCuoiNgay _bc;
    private readonly string _nguoiIn;
    private readonly DateTime _gioIn;

    private readonly Font _chuCuaHang = new("Segoe UI", 11F, FontStyle.Bold);
    private readonly Font _tieuDe = new("Segoe UI", 16F, FontStyle.Bold);
    private readonly Font _tieuMuc = new("Segoe UI", 11F, FontStyle.Bold);
    private readonly Font _chuThuong = new("Segoe UI", 10F);
    private readonly Font _chuDam = new("Segoe UI", 10F, FontStyle.Bold);
    private readonly Font _chuNghieng = new("Segoe UI", 9F, FontStyle.Italic);
    private readonly Pen _net = new(Color.Black, 1F);
    private readonly SolidBrush _nenTieuDeCot = new(Color.FromArgb(230, 230, 230));
    private readonly StringFormat _canhTrai = new() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
    private readonly StringFormat _canhGiua = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
    private readonly StringFormat _canhPhai = new() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

    /// <summary>Tạo bộ in báo cáo cuối ngày.</summary>
    /// <param name="bc">Số liệu báo cáo (ThongKeBLL.BaoCaoCuoiNgay).</param>
    /// <param name="nguoiIn">Họ tên người in (tài khoản đang đăng nhập).</param>
    public BaoCaoCuoiNgayPrinter(BaoCaoCuoiNgay bc, string nguoiIn)
    {
        _bc = bc;
        _nguoiIn = nguoiIn;
        _gioIn = DateTime.Now;
        TaiLieu = new PrintDocument { DocumentName = $"Báo cáo cuối ngày {bc.Ngay:dd-MM-yyyy}" };
        TaiLieu.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
        TaiLieu.DefaultPageSettings.Margins = new Margins(60, 60, 50, 50);
        TaiLieu.PrintPage += TaiLieu_PrintPage;
    }

    /// <summary>PrintDocument để gắn vào PrintPreviewDialog hoặc gọi Print().</summary>
    public PrintDocument TaiLieu { get; }

    /// <summary>Giải phóng font, bút, cọ, StringFormat và PrintDocument.</summary>
    public void Dispose()
    {
        TaiLieu.PrintPage -= TaiLieu_PrintPage;
        TaiLieu.Dispose();
        _chuCuaHang.Dispose();
        _tieuDe.Dispose();
        _tieuMuc.Dispose();
        _chuThuong.Dispose();
        _chuDam.Dispose();
        _chuNghieng.Dispose();
        _net.Dispose();
        _nenTieuDeCot.Dispose();
        _canhTrai.Dispose();
        _canhGiua.Dispose();
        _canhPhai.Dispose();
    }

    /// <summary>Vẽ trang A4 duy nhất (HasMorePages luôn false).</summary>
    private void TaiLieu_PrintPage(object? sender, PrintPageEventArgs e)
    {
        if (e.Graphics == null)
        {
            return;
        }

        Graphics g = e.Graphics;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        RectangleF vung = e.MarginBounds;
        float y = VeDauTrang(g, vung);
        float gioiHanDuoi = vung.Bottom - CaoPhanKy;

        // ---- I. Tổng hợp ----
        y = VeTieuMuc(g, vung, y, "I. TỔNG HỢP");
        var cotTongHop = new[] { ("Chỉ tiêu", 0.6F, _canhTrai), ("Giá trị", 0.4F, _canhPhai) };
        var dongTongHop = new List<string[]>
        {
            new[] { "Số hóa đơn hoàn tất", So(_bc.SoHoaDon) },
            new[] { "Số hóa đơn đã hủy", So(_bc.SoHoaDonHuy) },
            new[] { "Tổng tạm tính", Tien(_bc.TongTamTinh) },
            new[] { "Tổng giảm giá", Tien(_bc.TongGiamGia) },
            new[] { "Doanh thu thực", Tien(_bc.DoanhThuThuc) },
            new[] { "Tiền mặt phải có trong két", Tien(_bc.TienMatPhaiCo) }
        };
        (y, _) = VeBang(g, vung.Left, y, vung.Width * 0.7F, cotTongHop, dongTongHop, soDongDam: 2, gioiHanDuoi);
        y += 18;

        // ---- II. Giảm giá theo khuyến mãi ----
        y = VeTieuMuc(g, vung, y, "II. GIẢM GIÁ THEO KHUYẾN MÃI");
        if (_bc.GiamGiaTheoKhuyenMai.Count == 0)
        {
            y = VeGhiChu(g, vung, y, "Không có hóa đơn nào áp dụng khuyến mãi.");
        }
        else
        {
            var cotKM = new[] { ("STT", 0.08F, _canhGiua), ("Khuyến mãi", 0.52F, _canhTrai), ("Số HĐ", 0.15F, _canhPhai), ("Tổng giảm", 0.25F, _canhPhai) };
            List<string[]> dongKM = _bc.GiamGiaTheoKhuyenMai
                .Select((km, i) => new[] { (i + 1).ToString(VanHoaVN), km.TenKM, So(km.SoHoaDon), Tien(km.TongGiam) })
                .ToList();
            dongKM.Add(new[] { string.Empty, "Cộng", So(_bc.GiamGiaTheoKhuyenMai.Sum(k => k.SoHoaDon)), Tien(_bc.GiamGiaTheoKhuyenMai.Sum(k => k.TongGiam)) });
            (y, int daVe) = VeBang(g, vung.Left, y, vung.Width, cotKM, dongKM, soDongDam: 1, gioiHanDuoi);
            if (daVe < dongKM.Count)
            {
                y = VeGhiChu(g, vung, y, $"… còn {dongKM.Count - daVe} dòng không đủ chỗ trên trang.");
            }
        }

        y += 18;

        // ---- III. Hóa đơn hủy ----
        y = VeTieuMuc(g, vung, y, "III. HÓA ĐƠN ĐÃ HỦY");
        if (_bc.HoaDonHuy.Count == 0)
        {
            VeGhiChu(g, vung, y, "Không có hóa đơn nào bị hủy.");
        }
        else
        {
            var cotHuy = new[] { ("STT", 0.07F, _canhGiua), ("Mã hóa đơn", 0.2F, _canhTrai), ("Giờ lập", 0.11F, _canhGiua), ("Tổng tiền", 0.17F, _canhPhai), ("Lý do hủy", 0.45F, _canhTrai) };
            List<string[]> dongHuy = _bc.HoaDonHuy
                .Select((hd, i) => new[] { (i + 1).ToString(VanHoaVN), hd.MaHD, hd.NgayLap.ToString("HH:mm"), Tien(hd.TongThanhToan), hd.LyDoHuy ?? string.Empty })
                .ToList();

            // Chừa một dòng cho câu "còn N hóa đơn..." nếu phải cắt (báo cáo chỉ một trang)
            (y, int daVe) = VeBang(g, vung.Left, y, vung.Width, cotHuy, dongHuy, soDongDam: 0, gioiHanDuoi - 22);
            if (daVe < dongHuy.Count)
            {
                VeGhiChu(g, vung, y, $"… còn {dongHuy.Count - daVe} hóa đơn hủy khác – xem chi tiết ở màn hình Tra cứu hóa đơn.");
            }
        }

        VeChanTrang(g, vung);
        e.HasMorePages = false;
    }

    /// <summary>Tên cửa hàng, tiêu đề, ngày, thu ngân, người in + giờ in. Trả về y tiếp theo.</summary>
    private float VeDauTrang(Graphics g, RectangleF vung)
    {
        float y = vung.Top;
        string tenCuaHang = ConfigurationManager.AppSettings["TenCuaHang"] ?? "NHÀ SÁCH SPT";
        string diaChi = ConfigurationManager.AppSettings["DiaChiCuaHang"] ?? string.Empty;
        g.DrawString(tenCuaHang, _chuCuaHang, Brushes.Black, vung.Left, y);
        y += 20;
        if (!string.IsNullOrEmpty(diaChi))
        {
            g.DrawString(diaChi, _chuNghieng, Brushes.Black, vung.Left, y);
            y += 18;
        }

        y += 14;
        g.DrawString("BÁO CÁO CUỐI NGÀY", _tieuDe, Brushes.Black, new RectangleF(vung.Left, y, vung.Width, 32), _canhGiua);
        y += 40;

        string thuNgan = string.IsNullOrWhiteSpace(_bc.TenThuNgan) ? "Tất cả thu ngân" : _bc.TenThuNgan;
        float nuaRong = vung.Width / 2;
        g.DrawString($"Ngày: {_bc.Ngay:dd/MM/yyyy}", _chuDam, Brushes.Black, vung.Left, y);
        g.DrawString($"Thu ngân: {thuNgan}", _chuDam, Brushes.Black, vung.Left + nuaRong, y);
        y += 22;
        g.DrawString($"Người in: {_nguoiIn}", _chuThuong, Brushes.Black, vung.Left, y);
        g.DrawString($"In lúc: {_gioIn:HH:mm:ss dd/MM/yyyy}", _chuThuong, Brushes.Black, vung.Left + nuaRong, y);
        y += 26;
        g.DrawLine(_net, vung.Left, y, vung.Right, y);
        return y + 14;
    }

    private float VeTieuMuc(Graphics g, RectangleF vung, float y, string chu)
    {
        g.DrawString(chu, _tieuMuc, Brushes.Black, vung.Left, y);
        return y + 24;
    }

    private float VeGhiChu(Graphics g, RectangleF vung, float y, string chu)
    {
        g.DrawString(chu, _chuNghieng, Brushes.Black, vung.Left + 4, y);
        return y + 20;
    }

    /// <summary>
    /// Vẽ bảng có kẻ ô: hàng tiêu đề nền xám, các dòng dữ liệu (n dòng cuối in đậm), ô tự xuống dòng.
    /// Dừng khi dòng tiếp theo vượt <paramref name="gioiHanDuoi"/>. Trả về (y tiếp theo, số dòng đã vẽ).
    /// </summary>
    private (float Y, int SoDongDaVe) VeBang(Graphics g, float x, float y, float rong,
        (string TieuDe, float TiLe, StringFormat Canh)[] cot, List<string[]> dong, int soDongDam, float gioiHanDuoi)
    {
        float caoTieuDe = DoCaoDong(g, rong, cot, cot.Select(c => c.TieuDe).ToArray(), _chuDam);
        g.FillRectangle(_nenTieuDeCot, x, y, rong, caoTieuDe);
        VeO(g, x, y, rong, caoTieuDe, cot, cot.Select(c => c.TieuDe).ToArray(), _chuDam, true);
        y += caoTieuDe;

        int daVe = 0;
        for (int i = 0; i < dong.Count; i++)
        {
            Font font = i >= dong.Count - soDongDam ? _chuDam : _chuThuong;
            float cao = DoCaoDong(g, rong, cot, dong[i], font);
            if (y + cao > gioiHanDuoi)
            {
                break;
            }

            VeO(g, x, y, rong, cao, cot, dong[i], font, false);
            y += cao;
            daVe++;
        }

        return (y, daVe);
    }

    private float DoCaoDong(Graphics g, float rong, (string TieuDe, float TiLe, StringFormat Canh)[] cot, string[] o, Font font)
    {
        float cao = 0;
        for (int i = 0; i < o.Length; i++)
        {
            cao = Math.Max(cao, g.MeasureString(o[i], font, (int)(rong * cot[i].TiLe - 8)).Height);
        }

        return cao + 8;
    }

    private void VeO(Graphics g, float x, float y, float rong, float cao,
        (string TieuDe, float TiLe, StringFormat Canh)[] cot, string[] o, Font font, bool laTieuDe)
    {
        float xo = x;
        for (int i = 0; i < o.Length; i++)
        {
            float rongO = rong * cot[i].TiLe;
            g.DrawRectangle(_net, xo, y, rongO, cao);
            g.DrawString(o[i], font, Brushes.Black, new RectangleF(xo + 4, y + 1, rongO - 8, cao - 2), laTieuDe ? _canhGiua : cot[i].Canh);
            xo += rongO;
        }
    }

    /// <summary>Hai ô ký "Thu ngân" / "Quản lý" và ghi chú cuối trang.</summary>
    private void VeChanTrang(Graphics g, RectangleF vung)
    {
        float y = vung.Bottom - CaoPhanKy + 16;
        float nuaRong = vung.Width / 2;
        foreach ((string vaiTro, float x) in new[] { ("Thu ngân", vung.Left), ("Quản lý", vung.Left + nuaRong) })
        {
            g.DrawString(vaiTro, _chuDam, Brushes.Black, new RectangleF(x, y, nuaRong, 20), _canhGiua);
            g.DrawString("(Ký, ghi rõ họ tên)", _chuNghieng, Brushes.Black, new RectangleF(x, y + 20, nuaRong, 18), _canhGiua);
        }

        float yGhiChu = vung.Bottom - 22;
        g.DrawLine(_net, vung.Left, yGhiChu - 4, vung.Right, yGhiChu - 4);
        g.DrawString("Báo cáo tổng hợp, không thay thế đối soát ca. Không tính hóa đơn đã hủy vào doanh thu (TK-03).",
            _chuNghieng, Brushes.Black, new RectangleF(vung.Left, yGhiChu, vung.Width, 20), _canhGiua);
    }

    private static string Tien(decimal so) => so.ToString("#,##0", VanHoaVN) + "đ";

    private static string So(int so) => so.ToString("#,##0", VanHoaVN);
}
