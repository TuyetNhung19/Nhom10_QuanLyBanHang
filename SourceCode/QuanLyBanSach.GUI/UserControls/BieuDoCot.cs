// File: QuanLyBanSach.GUI/UserControls/BieuDoCot.cs
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace QuanLyBanSach.GUI.UserControls;

/// <summary>
/// Biểu đồ cột tự vẽ bằng GDI+ (không dùng thư viện chart) – dùng chung cho FrmThongKe và bảng điều khiển.
/// Trục Y 5 vạch chia "đẹp" (1-2-5), lưới mờ, cột lớn nhất màu đậm, giá trị rút gọn trên đỉnh cột,
/// nhãn trục X tự xoay / bỏ bớt khi dày, ToolTip hiện số đầy đủ khi rê chuột.
/// </summary>
[ToolboxItem(true)]
public class BieuDoCot : UserControl
{
    private const int SoVachChia = 5;
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    private readonly ToolTip _toolTip = new() { InitialDelay = 150, ReshowDelay = 50 };
    private readonly List<(string Nhan, decimal GiaTri)> _duLieu = new();
    private readonly List<RectangleF> _vungCot = new();
    private int _cotDangRe = -1;

    /// <summary>Khởi tạo biểu đồ: vẽ đệm đôi chống nháy, tự vẽ lại khi đổi kích thước.</summary>
    public BieuDoCot()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Color.White;
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    /// <summary>Tiêu đề phía trên biểu đồ.</summary>
    [Category("BookSPT"), DefaultValue("")]
    public string TieuDe { get; set; } = string.Empty;

    /// <summary>Đơn vị ghi cạnh trục Y, ví dụ "nghìn đồng" hoặc "cuốn".</summary>
    [Category("BookSPT"), DefaultValue("")]
    public string DonViTruc { get; set; } = string.Empty;

    /// <summary>Số chia cho nhãn trục Y (1000 khi DonViTruc là "nghìn đồng").</summary>
    [Category("BookSPT"), DefaultValue(typeof(decimal), "1")]
    public decimal HeSoTruc { get; set; } = 1;

    /// <summary>true: giá trị là tiền (rút gọn kiểu 1,2tr / 850k và thêm "đ" trong ToolTip).</summary>
    [Category("BookSPT"), DefaultValue(true)]
    public bool LaTien { get; set; } = true;

    /// <summary>Màu cột thường; cột lớn nhất dùng màu đậm hơn.</summary>
    [Category("BookSPT")]
    public Color MauCot { get; set; } = Color.FromArgb(57, 111, 146);

    /// <summary>Gán dữ liệu (nhãn, giá trị) và vẽ lại.</summary>
    public void GanDuLieu(IList<(string Nhan, decimal GiaTri)> duLieu)
    {
        _duLieu.Clear();
        _duLieu.AddRange(duLieu);
        _cotDangRe = -1;
        _toolTip.Hide(this);
        Invalidate();
    }

    /// <summary>Vẽ toàn bộ biểu đồ.</summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Ve(e.Graphics, ClientRectangle, Font, TieuDe, DonViTruc, HeSoTruc, LaTien, MauCot, _duLieu, _vungCot);
    }

    /// <summary>ToolTip: rê chuột lên cột hiện nhãn và số đầy đủ.</summary>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        int cot = _vungCot.FindIndex(r => r.Contains(e.Location));
        if (cot == _cotDangRe)
        {
            return;
        }

        _cotDangRe = cot;
        if (cot < 0 || cot >= _duLieu.Count)
        {
            _toolTip.Hide(this);
            return;
        }

        (string nhan, decimal giaTri) = _duLieu[cot];
        string chu = $"{nhan}: {giaTri.ToString("#,##0", VanHoaVN)}{(LaTien ? "đ" : string.Empty)}";
        _toolTip.Show(chu, this, e.X + 12, e.Y - 24);
    }

    /// <summary>Rời chuột khỏi biểu đồ thì ẩn ToolTip.</summary>
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _cotDangRe = -1;
        _toolTip.Hide(this);
    }

    /// <summary>Giải phóng ToolTip.</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _toolTip.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Phần vẽ tách riêng (không phụ thuộc trạng thái control) để dễ kiểm thử: vẽ vào Graphics bất kỳ.
    /// </summary>
    /// <param name="vungCot">Nhận lại hình chữ nhật của từng cột (phục vụ ToolTip).</param>
    internal static void Ve(Graphics g, Rectangle khung, Font font, string tieuDe, string donVi, decimal heSo,
        bool laTien, Color mauCot, IReadOnlyList<(string Nhan, decimal GiaTri)> duLieu, List<RectangleF> vungCot)
    {
        vungCot.Clear();
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        using var chuTieuDe = new Font(font.FontFamily, font.Size + 1F, FontStyle.Bold);
        using var chuNho = new Font(font.FontFamily, Math.Max(7F, font.Size - 1.5F));
        using var butTruc = new Pen(Color.Gray, 1F);
        using var butLuoi = new Pen(Color.FromArgb(225, 225, 225), 1F) { DashStyle = DashStyle.Dash };
        using var coChu = new SolidBrush(Color.FromArgb(64, 64, 64));
        using var canhGiua = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        using var canhPhai = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        float caoTieuDe = 0;
        if (!string.IsNullOrEmpty(tieuDe))
        {
            caoTieuDe = g.MeasureString(tieuDe, chuTieuDe).Height + 4;
            g.DrawString(tieuDe, chuTieuDe, coChu, new RectangleF(khung.Left, khung.Top + 2, khung.Width, caoTieuDe), canhGiua);
        }

        if (duLieu.Count == 0)
        {
            g.DrawString("Chưa có dữ liệu", font, Brushes.Gray, khung, canhGiua);
            return;
        }

        // ---- Thang trục Y: bước "đẹp" theo dãy 1-2-5 ----
        decimal lonNhat = duLieu.Max(d => d.GiaTri);
        decimal buoc = BuocDep(lonNhat, SoVachChia);
        decimal dinhTruc = buoc * SoVachChia;
        string[] nhanTrucY = Enumerable.Range(0, SoVachChia + 1)
            .Select(i => (buoc * i / (heSo == 0 ? 1 : heSo)).ToString("#,##0.##", VanHoaVN))
            .ToArray();
        float rongNhanY = nhanTrucY.Max(n => g.MeasureString(n, chuNho).Width);
        float caoChu = g.MeasureString("Ag", chuNho).Height;

        // ---- Nhãn trục X: xoay 45° khi không đủ chỗ, bỏ bớt khi quá dày ----
        float trai = khung.Left + rongNhanY + 10;
        float phai = khung.Right - 10;
        float rongVe = Math.Max(10, phai - trai);
        float oCot = rongVe / duLieu.Count;
        float rongNhanX = duLieu.Max(d => g.MeasureString(d.Nhan, chuNho).Width);
        bool xoay = rongNhanX > oCot * 0.95F;
        int buocNhan = xoay && oCot < caoChu * 1.3F ? (int)Math.Ceiling(caoChu * 1.3F / oCot) : 1;
        float caoNhanX = xoay ? Math.Min(rongNhanX * 0.72F + caoChu * 0.5F, khung.Height * 0.35F) : caoChu + 4;

        // Chừa một dòng cho chữ đơn vị và nửa dòng cho nhãn vạch trên cùng (nhãn canh giữa theo vạch)
        float tren = khung.Top + caoTieuDe + caoChu * 1.6F + 6;
        float duoi = khung.Bottom - caoNhanX - 6;
        float caoVe = Math.Max(10, duoi - tren);

        if (!string.IsNullOrEmpty(donVi))
        {
            g.DrawString("(" + donVi + ")", chuNho, Brushes.Gray, khung.Left + 2, khung.Top + caoTieuDe);
        }

        // ---- Lưới và nhãn trục Y ----
        for (int i = 0; i <= SoVachChia; i++)
        {
            float y = duoi - caoVe * i / SoVachChia;
            if (i > 0)
            {
                g.DrawLine(butLuoi, trai, y, phai, y);
            }

            g.DrawString(nhanTrucY[i], chuNho, coChu, new RectangleF(khung.Left, y - caoChu / 2, rongNhanY + 4, caoChu), canhPhai);
        }

        g.DrawLine(butTruc, trai, tren, trai, duoi);
        g.DrawLine(butTruc, trai, duoi, phai, duoi);

        if (lonNhat <= 0)
        {
            g.DrawString("Chưa có số liệu trong khoảng này", font, Brushes.Gray,
                new RectangleF(trai, tren, rongVe, caoVe), canhGiua);
        }

        // ---- Cột, giá trị trên đỉnh, nhãn X ----
        using var coCot = new SolidBrush(mauCot);
        using var coCotLonNhat = new SolidBrush(ControlPaint.Dark(mauCot, 0.25F));
        float rongCot = Math.Min(oCot * 0.62F, 64F);

        for (int i = 0; i < duLieu.Count; i++)
        {
            (string nhan, decimal giaTri) = duLieu[i];
            float giua = trai + oCot * i + oCot / 2;
            float cao = dinhTruc <= 0 ? 0 : (float)(giaTri / dinhTruc) * caoVe;
            var cot = new RectangleF(giua - rongCot / 2, duoi - cao, rongCot, cao);
            vungCot.Add(new RectangleF(trai + oCot * i, tren, oCot, caoVe));

            if (cao > 0)
            {
                g.FillRectangle(giaTri == lonNhat ? coCotLonNhat : coCot, cot);
            }

            bool veNhan = i % buocNhan == 0;
            string chuGiaTri = RutGon(giaTri, laTien);

            // Cột hẹp hơn chữ giá trị → chỉ ghi giá trị cột lớn nhất để khỏi chồng chữ (ToolTip vẫn có số đầy đủ)
            bool duCho = g.MeasureString(chuGiaTri, chuNho).Width <= oCot - 2;
            if (giaTri > 0 && (duCho || giaTri == lonNhat))
            {
                g.DrawString(chuGiaTri, chuNho, coChu, new RectangleF(giua - oCot, cot.Top - caoChu - 1, oCot * 2, caoChu), canhGiua);
            }

            if (!veNhan)
            {
                continue;
            }

            if (xoay)
            {
                GraphicsState trangThai = g.Save();
                g.TranslateTransform(giua, duoi + 4);
                g.RotateTransform(-45F);
                using var canhCuoi = new StringFormat { Alignment = StringAlignment.Far };
                g.DrawString(nhan, chuNho, coChu, new RectangleF(-rongNhanX - 4, 0, rongNhanX + 4, caoChu), canhCuoi);
                g.Restore(trangThai);
            }
            else
            {
                g.DrawString(nhan, chuNho, coChu, new RectangleF(giua - oCot / 2, duoi + 3, oCot, caoChu + 2), canhGiua);
            }
        }
    }

    /// <summary>
    /// Bước chia trục "đẹp" theo dãy 1-2-5 × 10^k sao cho (bước × số vạch) ≥ giá trị lớn nhất.
    /// Ví dụ max 1.234.000, 5 vạch: max/5 = 246.800 → bước 500.000 (1-2-5 gần nhất ≥ 246.800), trục 0 – 2.500.000;
    /// max 380.000 → 76.000 → bước 100.000, trục 0 – 500.000.
    /// </summary>
    internal static decimal BuocDep(decimal lonNhat, int soVach)
    {
        if (lonNhat <= 0)
        {
            return 1;
        }

        double tho = (double)lonNhat / soVach;
        double luyThua = Math.Pow(10, Math.Floor(Math.Log10(tho)));
        double tiLe = tho / luyThua;
        double heSo = tiLe <= 1 ? 1 : tiLe <= 2 ? 2 : tiLe <= 5 ? 5 : 10;
        return (decimal)(heSo * luyThua);
    }

    /// <summary>Rút gọn số: tiền → "1,2tr" / "850k"; số lượng → "1.234".</summary>
    internal static string RutGon(decimal giaTri, bool laTien)
    {
        if (!laTien)
        {
            return giaTri.ToString("#,##0", VanHoaVN);
        }

        if (giaTri >= 1_000_000_000)
        {
            return (giaTri / 1_000_000_000).ToString("0.#", VanHoaVN) + "tỷ";
        }

        if (giaTri >= 1_000_000)
        {
            return (giaTri / 1_000_000).ToString("0.#", VanHoaVN) + "tr";
        }

        return giaTri >= 1_000 ? (giaTri / 1_000).ToString("0.#", VanHoaVN) + "k" : giaTri.ToString("0", VanHoaVN);
    }
}
