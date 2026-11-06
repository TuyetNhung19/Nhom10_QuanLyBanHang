// File: QuanLyBanSach.GUI/Printing/SachSapHetPrinter.cs
using System.Configuration;
using System.Drawing.Printing;
using System.Globalization;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.GUI.Printing;

/// <summary>
/// In danh sách sách sắp hết (đặc tả 3.6) khổ A4, nhiều trang; mỗi trang lặp lại tiêu đề cột và số trang.
/// </summary>
public sealed class SachSapHetPrinter : IDisposable
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    /// <summary>Tiêu đề và tỉ lệ độ rộng các cột.</summary>
    private static readonly (string TieuDe, float TiLe)[] Cot =
    {
        ("STT", 0.06f), ("Mã sách", 0.11f), ("Tên sách", 0.43f), ("Loại", 0.18f), ("Tồn", 0.10f), ("Tối thiểu", 0.12f)
    };

    private readonly List<Sach> _ds;
    private readonly Font _tieuDe = new("Segoe UI", 14F, FontStyle.Bold);
    private readonly Font _chuThuong = new("Segoe UI", 10F);
    private readonly Font _chuDam = new("Segoe UI", 10F, FontStyle.Bold);
    private readonly Font _chuNho = new("Segoe UI", 8.5F, FontStyle.Italic);
    private readonly Pen _net = new(Color.Black, 1F);
    private readonly StringFormat _canhTrai = new() { Alignment = StringAlignment.Near };
    private readonly StringFormat _canhGiua = new() { Alignment = StringAlignment.Center };
    private readonly StringFormat _canhPhai = new() { Alignment = StringAlignment.Far };

    private int _dongTiepTheo;
    private int _soTrang;

    /// <summary>Tạo bộ in cho danh sách sách sắp hết.</summary>
    public SachSapHetPrinter(List<Sach> ds)
    {
        _ds = ds;
        TaiLieu = new PrintDocument { DocumentName = "Sách sắp hết" };
        TaiLieu.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
        TaiLieu.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
        TaiLieu.BeginPrint += TaiLieu_BeginPrint;
        TaiLieu.PrintPage += TaiLieu_PrintPage;
    }

    /// <summary>PrintDocument để gắn vào PrintPreviewDialog.</summary>
    public PrintDocument TaiLieu { get; }

    /// <summary>Giải phóng font, bút, StringFormat và PrintDocument.</summary>
    public void Dispose()
    {
        TaiLieu.BeginPrint -= TaiLieu_BeginPrint;
        TaiLieu.PrintPage -= TaiLieu_PrintPage;
        TaiLieu.Dispose();
        _tieuDe.Dispose();
        _chuThuong.Dispose();
        _chuDam.Dispose();
        _chuNho.Dispose();
        _net.Dispose();
        _canhTrai.Dispose();
        _canhGiua.Dispose();
        _canhPhai.Dispose();
    }

    private void TaiLieu_BeginPrint(object? sender, PrintEventArgs e)
    {
        _dongTiepTheo = 0;
        _soTrang = 0;
    }

    /// <summary>Vẽ một trang: tiêu đề (trang đầu), hàng tiêu đề cột, các dòng; hết chỗ thì sang trang.</summary>
    private void TaiLieu_PrintPage(object? sender, PrintPageEventArgs e)
    {
        if (e.Graphics == null)
        {
            return;
        }

        Graphics g = e.Graphics;
        RectangleF vung = e.MarginBounds;
        float y = vung.Top;
        _soTrang++;

        if (_soTrang == 1)
        {
            string tenCuaHang = ConfigurationManager.AppSettings["TenCuaHang"] ?? "NHÀ SÁCH SPT";
            g.DrawString(tenCuaHang, _chuDam, Brushes.Black, new RectangleF(vung.Left, y, vung.Width, 30), _canhTrai);
            y += 24;
            g.DrawString("DANH SÁCH SÁCH SẮP HẾT", _tieuDe, Brushes.Black, new RectangleF(vung.Left, y, vung.Width, 40), _canhGiua);
            y += 34;
            g.DrawString($"Ngày in: {DateTime.Now:dd/MM/yyyy HH:mm} – {_ds.Count} đầu sách có tồn ≤ tồn tối thiểu",
                _chuNho, Brushes.Black, new RectangleF(vung.Left, y, vung.Width, 24), _canhGiua);
            y += 30;
        }

        y = VeDong(g, vung, y, Cot.Select(c => c.TieuDe).ToArray(), _chuDam);
        g.DrawLine(_net, vung.Left, y, vung.Right, y);
        y += 4;

        float chanTrang = vung.Bottom - 24;
        while (_dongTiepTheo < _ds.Count)
        {
            Sach s = _ds[_dongTiepTheo];
            string[] o =
            {
                (_dongTiepTheo + 1).ToString(VanHoaVN), s.MaSach, s.TenSach, s.TenLoai,
                s.SoLuongTon.ToString(VanHoaVN), s.TonToiThieu.ToString(VanHoaVN)
            };

            if (y + DoCaoDong(g, vung.Width, o, _chuThuong) > chanTrang)
            {
                break;
            }

            y = VeDong(g, vung, y, o, _chuThuong);
            _dongTiepTheo++;
        }

        g.DrawString($"Trang {_soTrang}", _chuNho, Brushes.Black, new RectangleF(vung.Left, vung.Bottom - 20, vung.Width, 20), _canhPhai);
        e.HasMorePages = _dongTiepTheo < _ds.Count;
    }

    /// <summary>Chiều cao một dòng (cột Tên sách tự xuống dòng).</summary>
    private static float DoCaoDong(Graphics g, float rong, string[] o, Font font)
    {
        float cao = 0;
        for (int i = 0; i < o.Length; i++)
        {
            cao = Math.Max(cao, g.MeasureString(o[i], font, new SizeF(rong * Cot[i].TiLe - 6, 10_000F)).Height);
        }

        return cao + 4;
    }

    /// <summary>Vẽ một dòng bảng; cột số canh phải. Trả về y của dòng kế tiếp.</summary>
    private float VeDong(Graphics g, RectangleF vung, float y, string[] o, Font font)
    {
        float cao = DoCaoDong(g, vung.Width, o, font);
        float x = vung.Left;
        for (int i = 0; i < o.Length; i++)
        {
            float rongCot = vung.Width * Cot[i].TiLe;
            StringFormat canh = i is 0 or 4 or 5 ? (i == 0 ? _canhGiua : _canhPhai) : _canhTrai;
            g.DrawString(o[i], font, Brushes.Black, new RectangleF(x + 3, y + 2, rongCot - 6, cao), canh);
            x += rongCot;
        }

        return y + cao;
    }
}
