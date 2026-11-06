// File: QuanLyBanSach.BLL/ThongKeBLL.cs
using System.Globalization;
using System.Reflection;
using System.Text;
using QuanLyBanSach.DAL;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Thống kê, bảng điều khiển (ĐT5), báo cáo cuối ngày (ĐT6), hồ sơ khách và gợi ý mua kèm bằng LINQ (ĐT4).
/// Thu ngân luôn bị ép xem số liệu của chính mình; thống kê tổng hợp yêu cầu Quản lý.
/// </summary>
public class ThongKeBLL
{
    /// <summary>Số gợi ý mua kèm tối đa.</summary>
    public const int SoGoiYToiDa = 3;

    private readonly ThongKeDAL _dal = new();
    private readonly KhachHangDAL _khachHangDal = new();
    private readonly HoaDonDAL _hoaDonDal = new();

    /// <summary>Số liệu các thẻ bảng điều khiển hôm nay (thu ngân: của mình).</summary>
    /// <param name="maTK">Thu ngân cần xem (Quản lý); null = tất cả.</param>
    public TongQuanNgay TongQuanTrongNgay(int? maTK = null)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.TongQuanTrongNgay(DateTime.Today, EpMaTK(maTK)));
    }

    /// <summary>Doanh thu từng ngày (biểu đồ 7 ngày, thống kê); thu ngân chỉ thấy của mình.</summary>
    public List<DoanhThuNgay> DoanhThuTheoNgay(DateTime tu, DateTime den, int? maTK = null)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        KiemTraKhoangNgay(tu, den);
        return XuLyLoiCSDL.Goi(() => _dal.DoanhThuTheoNgay(tu, den, EpMaTK(maTK)));
    }

    /// <summary>
    /// Sách bán chạy. Thu ngân: chỉ trong ngày hôm nay và của chính mình; Quản lý: khoảng ngày tùy chọn.
    /// </summary>
    public List<SachBanChay> SachBanChay(DateTime tu, DateTime den, int top, int? maTK = null)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        if (PhienDangNhap.LaThuNgan)
        {
            tu = DateTime.Today;
            den = DateTime.Today;
        }

        KiemTraKhoangNgay(tu, den);
        int soDong = top <= 0 ? 10 : top;
        return XuLyLoiCSDL.Goi(() => _dal.SachBanChay(tu, den, soDong, EpMaTK(maTK)));
    }

    /// <summary>Báo cáo cuối ngày (ĐT6); thu ngân chỉ in báo cáo của mình, Quản lý chọn thu ngân hoặc tất cả.</summary>
    public BaoCaoCuoiNgay BaoCaoCuoiNgay(DateTime ngay, int? maTK = null)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.BaoCaoCuoiNgay(ngay, EpMaTK(maTK)));
    }

    /// <summary>Doanh thu theo tháng (Quản lý).</summary>
    public List<DoanhThuThang> DoanhThuTheoThang(DateTime tu, DateTime den)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        KiemTraKhoangNgay(tu, den);
        return XuLyLoiCSDL.Goi(() => _dal.DoanhThuTheoThang(tu, den));
    }

    /// <summary>Doanh thu theo thu ngân (Quản lý).</summary>
    public List<DoanhThuThuNgan> DoanhThuTheoThuNgan(DateTime tu, DateTime den)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        KiemTraKhoangNgay(tu, den);
        return XuLyLoiCSDL.Goi(() => _dal.DoanhThuTheoThuNgan(tu, den));
    }

    /// <summary>Hiệu quả khuyến mãi (Quản lý).</summary>
    public List<HieuQuaKhuyenMai> HieuQuaKhuyenMai(DateTime tu, DateTime den)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        KiemTraKhoangNgay(tu, den);
        return XuLyLoiCSDL.Goi(() => _dal.HieuQuaKhuyenMai(tu, den));
    }

    /// <summary>
    /// ĐT4 – hồ sơ khách quen bằng LINQ trên lịch sử mua (chỉ hóa đơn hoàn tất):
    /// SoLanMua = số MaHD khác nhau; TongChiTieu = Σ TongThanhToan; LanMuaGanNhat = Max(NgayLap);
    /// LoaiHayMua = 3 loại có tổng số cuốn nhiều nhất.
    /// </summary>
    public HoSoKhachHang LayHoSoKhach(int maKH)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);

        List<DongLichSuMua> dongMua = XuLyLoiCSDL.Goi(() => _khachHangDal.LayDongLichSuMua(maKH));
        List<HoaDon> hoaDon = XuLyLoiCSDL.Goi(() => _khachHangDal.LayLichSuHoaDon(maKH));

        return new HoSoKhachHang
        {
            MaKH = maKH,
            SoLanMua = dongMua.Select(d => d.MaHD).Distinct().Count(),
            TongChiTieu = hoaDon.Where(hd => hd.TrangThai == TrangThaiHoaDon.HoanTat).Sum(hd => hd.TongThanhToan),
            LanMuaGanNhat = dongMua.Count == 0 ? null : dongMua.Max(d => d.NgayLap),
            LoaiHayMua = dongMua
                .GroupBy(d => d.TenLoai)
                .OrderByDescending(nhom => nhom.Sum(d => d.SoLuong))
                .ThenBy(nhom => nhom.Key)
                .Take(3)
                .Select(nhom => nhom.Key)
                .ToList()
        };
    }

    /// <summary>
    /// ĐT4 – gợi ý tối đa 3 sách "thường được mua kèm" bằng LINQ: lấy các dòng của những hóa đơn
    /// có chứa <paramref name="maSach"/>, bỏ chính nó và sách đã có trong đơn, chỉ giữ sách đang bán
    /// còn tồn, GroupBy theo mã sách, sắp theo số hóa đơn chung giảm dần.
    /// </summary>
    /// <param name="maSach">Sách vừa thêm vào đơn.</param>
    /// <param name="maDaCoTrongDon">Các mã sách đang có trong đơn (không gợi ý lại).</param>
    public List<GoiYSach> GoiYMuaKem(string maSach, IEnumerable<string> maDaCoTrongDon)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);

        var boQua = new HashSet<string>(maDaCoTrongDon, StringComparer.OrdinalIgnoreCase) { maSach };
        List<ChiTietHoaDon> dong = XuLyLoiCSDL.Goi(() => _hoaDonDal.LayDongCuaHoaDonCoSach(maSach));

        return dong
            .Where(ct => !boQua.Contains(ct.MaSach) && ct.SoLuongTon > 0 && ct.SachDangBan)
            .GroupBy(ct => ct.MaSach)
            .OrderByDescending(nhom => nhom.Count())
            .ThenBy(nhom => nhom.First().TenSach)
            .Take(SoGoiYToiDa)
            .Select(nhom => new GoiYSach
            {
                MaSach = nhom.Key,
                TenSach = nhom.First().TenSach,
                GiaBan = nhom.First().DonGia,
                SoLuongTon = nhom.First().SoLuongTon,
                SoLanMuaCung = nhom.Count()
            })
            .ToList();
    }

    /// <summary>
    /// Xuất danh sách ra CSV UTF-8 có BOM (Excel mở không lỗi font tiếng Việt).
    /// Cột = các thuộc tính kiểu đơn giản của <typeparamref name="T"/>; tiền xuất số nguyên không dấu phân cách.
    /// </summary>
    /// <param name="ds">Dữ liệu cần xuất.</param>
    /// <param name="duongDan">Đường dẫn file .csv (thường lấy từ SaveFileDialog).</param>
    public void XuatCsv<T>(IEnumerable<T> ds, string duongDan)
    {
        PhienDangNhap.YeuCauDangNhap();
        if (string.IsNullOrWhiteSpace(duongDan))
        {
            throw new BusinessException("Vui lòng chọn nơi lưu file CSV.");
        }

        PropertyInfo[] cot = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0 && LaKieuDonGian(p.PropertyType))
            .ToArray();

        var sb = new StringBuilder();
        sb.AppendLine(string.Join(",", cot.Select(p => BaoCsv(p.Name))));
        foreach (T dong in ds)
        {
            sb.AppendLine(string.Join(",", cot.Select(p => BaoCsv(DinhDangCsv(p.GetValue(dong))))));
        }

        try
        {
            File.WriteAllText(duongDan, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new BusinessException("Không ghi được file CSV (file có đang mở trong Excel?): " + ex.Message, ex);
        }
    }

    /// <summary>Thu ngân luôn bị ép xem số liệu của chính mình.</summary>
    private static int? EpMaTK(int? maTK) => PhienDangNhap.LaThuNgan ? PhienDangNhap.MaTK : maTK;

    /// <summary>Từ ngày phải &lt;= đến ngày.</summary>
    private static void KiemTraKhoangNgay(DateTime tu, DateTime den)
    {
        if (tu.Date > den.Date)
        {
            throw new BusinessException("Từ ngày phải nhỏ hơn hoặc bằng đến ngày.", "DenNgay");
        }
    }

    /// <summary>Kiểu được xuất ra CSV: số, chuỗi, ngày, bool, enum (bỏ qua danh sách, đối tượng con).</summary>
    private static bool LaKieuDonGian(Type kieu)
    {
        Type goc = Nullable.GetUnderlyingType(kieu) ?? kieu;
        return goc.IsPrimitive || goc.IsEnum || goc == typeof(string) || goc == typeof(decimal) || goc == typeof(DateTime);
    }

    /// <summary>Định dạng giá trị: ngày dd/MM/yyyy HH:mm, số theo InvariantCulture.</summary>
    private static string DinhDangCsv(object? giaTri) => giaTri switch
    {
        null => string.Empty,
        DateTime ngay => ngay.TimeOfDay == TimeSpan.Zero ? ngay.ToString("dd/MM/yyyy") : ngay.ToString("dd/MM/yyyy HH:mm"),
        IFormattable so => so.ToString(null, CultureInfo.InvariantCulture),
        _ => giaTri.ToString() ?? string.Empty
    };

    /// <summary>Bọc giá trị trong dấu nháy kép, nhân đôi dấu nháy bên trong (chuẩn CSV).</summary>
    private static string BaoCsv(string giaTri) => "\"" + giaTri.Replace("\"", "\"\"") + "\"";
}
