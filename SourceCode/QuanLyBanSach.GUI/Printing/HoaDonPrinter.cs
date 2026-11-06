// File: QuanLyBanSach.GUI/Printing/HoaDonPrinter.cs
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.GUI.Printing;

/// <summary>Khổ giấy in hóa đơn.</summary>
public enum KhoGiayHoaDon
{
    /// <summary>Giấy nhiệt 80 mm, một trang dài theo nội dung.</summary>
    K80,

    /// <summary>Khổ A5, tự sang trang khi nhiều dòng.</summary>
    A5
}

/// <summary>
/// In hóa đơn theo mẫu đặc tả mục 7: bọc một PrintDocument và tự vẽ trong sự kiện PrintPage
/// bằng Graphics.DrawString / DrawLine (không dùng thư viện báo cáo).
/// Nội dung được dựng thành danh sách "mục in" trước, nhờ đó:
/// - K80 tính được chiều cao giấy đúng bằng nội dung;
/// - A5 sang trang (HasMorePages) giữa các mục.
/// </summary>
public sealed class HoaDonPrinter : IDisposable
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    /// <summary>Tỉ lệ độ rộng 5 cột: STT | Tên sách | SL | Đ.giá | T.tiền.</summary>
    private static readonly float[] TiLeCot = { 0.08f, 0.42f, 0.09f, 0.20f, 0.21f };

    /// <summary>Chiều cao "vô hạn" của vùng chữ khi đo/vẽ (để chữ tự xuống dòng theo độ rộng).</summary>
    private const float VoHan = 100_000F;

    private readonly HoaDon _hd;
    private readonly bool _banInLai;
    private readonly KhoGiayHoaDon _khoGiay;
    private readonly Font _chuThuong;
    private readonly Font _chuDam;
    private readonly Font _chuNho;
    private readonly Font _tieuDe;
    private readonly Font _chuTong;
    private readonly Pen _netLien;
    private readonly Pen _netDut;
    private readonly StringFormat _canhTrai = new() { Alignment = StringAlignment.Near };
    private readonly StringFormat _canhGiua = new() { Alignment = StringAlignment.Center };
    private readonly StringFormat _canhPhai = new() { Alignment = StringAlignment.Far };
    private readonly List<MucIn> _dsMuc = new();

    private int _mucTiepTheo;
    private DateTime _thoiDiemIn;

    /// <summary>Tạo bộ in cho một hóa đơn đã lưu (lấy từ CSDL để khớp dữ liệu – TC13).</summary>
    /// <param name="hd">Hóa đơn kèm chi tiết.</param>
    /// <param name="banInLai">true: in thêm dòng "BẢN IN LẠI" và thời điểm in lại.</param>
    /// <param name="khoGiay">K80 hoặc A5.</param>
    public HoaDonPrinter(HoaDon hd, bool banInLai, KhoGiayHoaDon khoGiay)
    {
        _hd = hd;
        _banInLai = banInLai;
        _khoGiay = khoGiay;

        float co = khoGiay == KhoGiayHoaDon.K80 ? 8F : 10F;
        _chuThuong = new Font("Segoe UI", co);
        _chuDam = new Font("Segoe UI", co, FontStyle.Bold);
        _chuNho = new Font("Segoe UI", co - 1F);
        _tieuDe = new Font("Segoe UI", co + 4F, FontStyle.Bold);
        _chuTong = new Font("Segoe UI", co + 2F, FontStyle.Bold);
        _netLien = new Pen(Color.Black, 1F);
        _netDut = new Pen(Color.Black, 1F) { DashStyle = DashStyle.Dash };

        TaiLieu = new PrintDocument { DocumentName = "Hóa đơn " + hd.MaHD };
        TaiLieu.BeginPrint += TaiLieu_BeginPrint;
        TaiLieu.PrintPage += TaiLieu_PrintPage;

        _thoiDiemIn = DateTime.Now;
        DungNoiDung();
        CaiDatKhoGiay();
    }

    /// <summary>PrintDocument để gắn vào PrintPreviewControl / PrintDialog.</summary>
    public PrintDocument TaiLieu { get; }

    /// <summary>Kích thước giấy đang dùng (đơn vị 1/100 inch) – để hiển thị thông tin khổ giấy.</summary>
    public Size KichThuocGiay { get; private set; }

    /// <summary>Giải phóng font, bút, StringFormat và PrintDocument.</summary>
    public void Dispose()
    {
        TaiLieu.BeginPrint -= TaiLieu_BeginPrint;
        TaiLieu.PrintPage -= TaiLieu_PrintPage;
        TaiLieu.Dispose();
        _chuThuong.Dispose();
        _chuDam.Dispose();
        _chuNho.Dispose();
        _tieuDe.Dispose();
        _chuTong.Dispose();
        _netLien.Dispose();
        _netDut.Dispose();
        _canhTrai.Dispose();
        _canhGiua.Dispose();
        _canhPhai.Dispose();
    }

    // ===================== Dựng nội dung =====================

    /// <summary>Dựng danh sách mục in theo mẫu hóa đơn (đặc tả mục 7).</summary>
    private void DungNoiDung()
    {
        string tenCuaHang = ConfigurationManager.AppSettings["TenCuaHang"] ?? "NHÀ SÁCH SPT";
        string diaChi = ConfigurationManager.AppSettings["DiaChiCuaHang"] ?? string.Empty;
        string sdt = ConfigurationManager.AppSettings["SDTCuaHang"] ?? string.Empty;

        _dsMuc.Clear();
        _dsMuc.Add(new MucChu(tenCuaHang.ToUpper(VanHoaVN), _tieuDe, _canhGiua));
        _dsMuc.Add(new MucChu(string.Join(" · ", new[] { diaChi, "ĐT: " + sdt }.Where(s => s.Trim().Length > 4)), _chuNho, _canhGiua));
        _dsMuc.Add(new MucKeVach(Dut: true));
        _dsMuc.Add(new MucChu("HÓA ĐƠN BÁN HÀNG", _chuTong, _canhGiua));

        if (_banInLai)
        {
            _dsMuc.Add(new MucChu("BẢN IN LẠI", _chuDam, _canhGiua));
            _dsMuc.Add(new MucChu("In lại lúc " + _thoiDiemIn.ToString("HH:mm dd/MM/yyyy"), _chuNho, _canhGiua));
        }

        if (_hd.DaHuy)
        {
            _dsMuc.Add(new MucChu("ĐÃ HỦY – " + (_hd.LyDoHuy ?? string.Empty), _chuDam, _canhGiua));
        }

        _dsMuc.Add(new MucKhoangTrong(4));
        _dsMuc.Add(new MucCapGiaTri("Số: " + _hd.MaHD, "Ngày: " + _hd.NgayLap.ToString("dd/MM/yyyy HH:mm"), _chuThuong));
        _dsMuc.Add(new MucChu("Thu ngân: " + _hd.TenThuNgan, _chuThuong, _canhTrai));
        _dsMuc.Add(new MucChu("Khách hàng: " + (string.IsNullOrWhiteSpace(_hd.TenKhach) ? "Khách lẻ" : _hd.TenKhach), _chuThuong, _canhTrai));
        _dsMuc.Add(new MucKeVach(Dut: true));

        _dsMuc.Add(new MucDongBang(new[] { "STT", "Tên sách", "SL", "Đ.giá", "T.tiền" }, _chuDam));
        foreach (ChiTietHoaDon ct in _hd.ChiTiet)
        {
            _dsMuc.Add(new MucDongBang(new[]
            {
                ct.Stt.ToString(VanHoaVN), ct.TenSach, ct.SoLuong.ToString(VanHoaVN), Tien(ct.DonGia), Tien(ct.ThanhTien)
            }, _chuThuong));
        }

        _dsMuc.Add(new MucKeVach(Dut: true));
        _dsMuc.Add(new MucCapGiaTri("Tạm tính:", Tien(_hd.TamTinh), _chuThuong));
        if (_hd.GiamGia > 0)
        {
            _dsMuc.Add(new MucCapGiaTri("KM: " + (_hd.TenKM ?? "Khuyến mãi"), "-" + Tien(_hd.GiamGia), _chuThuong));
        }

        _dsMuc.Add(new MucCapGiaTri("TỔNG THANH TOÁN:", Tien(_hd.TongThanhToan), _chuTong));
        _dsMuc.Add(new MucCapGiaTri("Tiền khách đưa:", Tien(_hd.TienKhachDua), _chuThuong));
        _dsMuc.Add(new MucCapGiaTri("Tiền thối:", Tien(_hd.TienThoi), _chuThuong));
        _dsMuc.Add(new MucKeVach(Dut: true));

        // Mã QR mang số tiền tổng thanh toán (bỏ qua với hóa đơn đã hủy để không ai chuyển tiền cho hóa đơn hủy)
        if (!_hd.DaHuy)
        {
            float canhQR = _khoGiay == KhoGiayHoaDon.K80 ? 120F : 140F;
            _dsMuc.Add(new MucMaQR(MaQR.TaoMaTran(NoiDungQRHoaDon.Tao(_hd, tenCuaHang)), canhQR));
            _dsMuc.Add(new MucChu(NoiDungQRHoaDon.ChuThich(_hd), _chuNho, _canhGiua));
            _dsMuc.Add(new MucKeVach(Dut: true));
        }

        _dsMuc.Add(new MucChu("Cảm ơn quý khách! Hẹn gặp lại", _chuDam, _canhGiua));
    }

    /// <summary>
    /// Khổ giấy: K80 = 315 × (chiều cao nội dung) phần trăm inch, lề 10; A5 = khổ chuẩn, lề 40.
    /// Chiều cao K80 đo bằng một Bitmap tạm độ phân giải 100 dpi: 1 điểm ảnh = 1/100 inch,
    /// cùng đơn vị với Graphics của máy in (GraphicsUnit.Display = 1/100 inch).
    /// </summary>
    private void CaiDatKhoGiay()
    {
        PageSettings trang = TaiLieu.DefaultPageSettings;
        if (_khoGiay == KhoGiayHoaDon.K80)
        {
            trang.Margins = new Margins(10, 10, 10, 10);
            const int rongK80 = 315;
            float cao;
            using (var anh = new Bitmap(1, 1))
            {
                anh.SetResolution(100F, 100F);
                using Graphics g = Graphics.FromImage(anh);
                g.PageUnit = GraphicsUnit.Pixel;
                cao = _dsMuc.Sum(m => m.DoCao(g, rongK80 - 20, this));
            }

            KichThuocGiay = new Size(rongK80, (int)Math.Ceiling(cao) + 40);
            trang.PaperSize = new PaperSize("K80", KichThuocGiay.Width, KichThuocGiay.Height);
        }
        else
        {
            trang.Margins = new Margins(40, 40, 40, 40);
            KichThuocGiay = new Size(583, 827);
            trang.PaperSize = new PaperSize("A5", KichThuocGiay.Width, KichThuocGiay.Height);
        }
    }

    // ===================== Vẽ =====================

    private void TaiLieu_BeginPrint(object? sender, PrintEventArgs e)
    {
        _mucTiepTheo = 0;
    }

    /// <summary>
    /// Vẽ các mục từ vị trí đang dừng; hết chỗ trên trang thì HasMorePages = true (A5 nhiều trang).
    /// Graphics của máy in mặc định dùng đơn vị 1/100 inch, trùng đơn vị của MarginBounds.
    /// </summary>
    private void TaiLieu_PrintPage(object? sender, PrintPageEventArgs e)
    {
        if (e.Graphics == null)
        {
            return;
        }

        Graphics g = e.Graphics;
        RectangleF vung = e.MarginBounds;
        float y = vung.Top;

        while (_mucTiepTheo < _dsMuc.Count)
        {
            MucIn muc = _dsMuc[_mucTiepTheo];
            float cao = muc.DoCao(g, vung.Width, this);

            // Mục đầu tiên của trang luôn được vẽ để tránh lặp vô hạn khi một mục cao hơn cả trang
            if (y + cao > vung.Bottom && y > vung.Top)
            {
                e.HasMorePages = true;
                return;
            }

            muc.Ve(g, vung.Left, y, vung.Width, this);
            y += cao;
            _mucTiepTheo++;
        }

        e.HasMorePages = false;
    }

    private static string Tien(decimal soTien) => soTien.ToString("#,##0", VanHoaVN);

    // ===================== Các loại mục in =====================

    /// <summary>Một mục trên hóa đơn: biết tự đo chiều cao và tự vẽ.</summary>
    private abstract record MucIn
    {
        public abstract float DoCao(Graphics g, float rong, HoaDonPrinter p);

        public abstract void Ve(Graphics g, float x, float y, float rong, HoaDonPrinter p);
    }

    /// <summary>Một đoạn chữ tự xuống dòng, canh trái/giữa/phải.</summary>
    private sealed record MucChu(string Chu, Font Font, StringFormat Canh) : MucIn
    {
        public override float DoCao(Graphics g, float rong, HoaDonPrinter p) =>
            g.MeasureString(Chu, Font, new SizeF(rong, VoHan), Canh).Height;

        public override void Ve(Graphics g, float x, float y, float rong, HoaDonPrinter p) =>
            g.DrawString(Chu, Font, Brushes.Black, new RectangleF(x, y, rong, VoHan), Canh);
    }

    /// <summary>Mã QR vuông cạnh <paramref name="Canh"/> (1/100 inch), canh giữa khổ giấy.</summary>
    private sealed record MucMaQR(bool[,] MaTran, float Canh) : MucIn
    {
        public override float DoCao(Graphics g, float rong, HoaDonPrinter p) => Canh + 4F;

        public override void Ve(Graphics g, float x, float y, float rong, HoaDonPrinter p) =>
            MaQR.Ve(g, MaTran, new RectangleF(x + (rong - Canh) / 2, y + 2F, Canh, Canh));
    }

    /// <summary>Đường kẻ ngang (liền hoặc đứt).</summary>
    private sealed record MucKeVach(bool Dut) : MucIn
    {
        public override float DoCao(Graphics g, float rong, HoaDonPrinter p) => 8F;

        public override void Ve(Graphics g, float x, float y, float rong, HoaDonPrinter p)
        {
            float giua = y + 4F;
            g.DrawLine(Dut ? p._netDut : p._netLien, x, giua, x + rong, giua);
        }
    }

    /// <summary>Khoảng trống (đơn vị 1/100 inch).</summary>
    private sealed record MucKhoangTrong(float Cao) : MucIn
    {
        public override float DoCao(Graphics g, float rong, HoaDonPrinter p) => Cao;

        public override void Ve(Graphics g, float x, float y, float rong, HoaDonPrinter p)
        {
        }
    }

    /// <summary>Nhãn bên trái, giá trị canh phải trên cùng một dòng (ví dụ "Tạm tính: … 251.000").</summary>
    private sealed record MucCapGiaTri(string Nhan, string GiaTri, Font Font) : MucIn
    {
        public override float DoCao(Graphics g, float rong, HoaDonPrinter p)
        {
            float rongGiaTri = g.MeasureString(GiaTri, Font).Width;
            float caoNhan = g.MeasureString(Nhan, Font, new SizeF(Math.Max(30F, rong - rongGiaTri - 5F), VoHan)).Height;
            return Math.Max(caoNhan, g.MeasureString(GiaTri, Font).Height);
        }

        public override void Ve(Graphics g, float x, float y, float rong, HoaDonPrinter p)
        {
            float rongGiaTri = g.MeasureString(GiaTri, Font).Width;
            g.DrawString(Nhan, Font, Brushes.Black, new RectangleF(x, y, Math.Max(30F, rong - rongGiaTri - 5F), VoHan), p._canhTrai);
            g.DrawString(GiaTri, Font, Brushes.Black, new RectangleF(x, y, rong, VoHan), p._canhPhai);
        }
    }

    /// <summary>
    /// Một dòng của bảng 5 cột. Tên sách dài tự xuống dòng (MeasureString theo độ rộng cột),
    /// các cột số (SL, Đ.giá, T.tiền) canh phải bằng StringFormat.
    /// </summary>
    private sealed record MucDongBang(string[] O, Font Font) : MucIn
    {
        public override float DoCao(Graphics g, float rong, HoaDonPrinter p)
        {
            float cao = 0;
            for (int i = 0; i < O.Length; i++)
            {
                cao = Math.Max(cao, g.MeasureString(O[i], Font, new SizeF(rong * TiLeCot[i] - 2F, VoHan)).Height);
            }

            return cao;
        }

        public override void Ve(Graphics g, float x, float y, float rong, HoaDonPrinter p)
        {
            float trai = x;
            for (int i = 0; i < O.Length; i++)
            {
                float rongCot = rong * TiLeCot[i];
                StringFormat canh = i switch
                {
                    0 => p._canhGiua,
                    1 => p._canhTrai,
                    _ => p._canhPhai
                };
                g.DrawString(O[i], Font, Brushes.Black, new RectangleF(trai, y, rongCot - 2F, VoHan), canh);
                trai += rongCot;
            }
        }
    }
}
