// File: QuanLyBanSach.BLL/HoaDonBLL.cs
using System.Globalization;
using QuanLyBanSach.BLL.KhuyenMai;
using QuanLyBanSach.DAL;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Nghiệp vụ hóa đơn: thêm sách vào đơn, tính tiền, lưu, hủy, tra cứu. Phát sự kiện cho bảng điều khiển (ĐT5).
/// </summary>
public class HoaDonBLL
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    /// <summary>Độ dài tối thiểu của lý do hủy.</summary>
    public const int DoDaiLyDoToiThieu = 5;

    private readonly HoaDonDAL _dal = new();
    private readonly SachDAL _sachDal = new();
    private readonly KhuyenMaiBLL _khuyenMaiBll = new();

    /// <summary>Phát sau khi lưu hóa đơn thành công (UcBangDieuKhien đăng ký để tự cập nhật).</summary>
    public static event EventHandler<HoaDonEventArgs>? HoaDonDaLuu;

    /// <summary>Phát sau khi hủy hóa đơn thành công.</summary>
    public static event EventHandler<HoaDonEventArgs>? HoaDonDaHuy;

    /// <summary>Mã hóa đơn dự kiến hôm nay (hiển thị khi mở Form bán hàng).</summary>
    public string TaoMaDuKien()
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.TaoMaHoaDonDuKien(DateTime.Today));
    }

    /// <summary>Tạm tính = Σ ThanhTien.</summary>
    public static decimal TinhTamTinh(IEnumerable<ChiTietHoaDon> chiTiet) => chiTiet.Sum(ct => ct.ThanhTien);

    /// <summary>Tiền thối = tiền khách đưa − tổng thanh toán (âm nghĩa là khách đưa chưa đủ).</summary>
    public static decimal TinhTienThoi(decimal tongThanhToan, decimal tienKhachDua) => tienKhachDua - tongThanhToan;

    /// <summary>
    /// Thêm sách vào đơn đang lập: đã có thì cộng số lượng; tổng không vượt tồn (HD-02); sách ẩn không bán.
    /// </summary>
    /// <param name="ds">Danh sách dòng của đơn (bị sửa trực tiếp).</param>
    /// <param name="s">Sách vừa tìm được (giá, tồn hiện hành).</param>
    /// <param name="soLuong">Số cuốn thêm (mặc định 1 khi quét mã).</param>
    /// <returns>Dòng vừa thêm / cập nhật.</returns>
    public ChiTietHoaDon ThemSachVaoDon(List<ChiTietHoaDon> ds, Sach s, int soLuong = 1)
    {
        if (!s.TrangThai)
        {
            throw new BusinessException($"Sách \"{s.TenSach}\" đã ngừng bán.");
        }

        if (soLuong <= 0)
        {
            throw new BusinessException("Số lượng phải lớn hơn 0.", nameof(ChiTietHoaDon.SoLuong));
        }

        ChiTietHoaDon? dong = ds.FirstOrDefault(ct => string.Equals(ct.MaSach, s.MaSach, StringComparison.OrdinalIgnoreCase));
        int tongSoLuong = (dong?.SoLuong ?? 0) + soLuong;
        KiemTraTon(s.TenSach, tongSoLuong, s.SoLuongTon);

        if (dong == null)
        {
            dong = new ChiTietHoaDon { MaSach = s.MaSach, TenSach = s.TenSach, MaLoai = s.MaLoai };
            ds.Add(dong);
        }

        dong.SoLuong = tongSoLuong;
        dong.DonGia = s.GiaBan;
        dong.SoLuongTon = s.SoLuongTon;
        dong.SachDangBan = true;
        DanhSoThuTu(ds);
        return dong;
    }

    /// <summary>
    /// Đổi số lượng một dòng: 0 = xóa dòng; không vượt tồn đã biết của dòng (HD-02).
    /// </summary>
    public void CapNhatSoLuong(List<ChiTietHoaDon> ds, string maSach, int soLuongMoi)
    {
        ChiTietHoaDon dong = ds.FirstOrDefault(ct => string.Equals(ct.MaSach, maSach, StringComparison.OrdinalIgnoreCase))
            ?? throw new BusinessException("Không tìm thấy sách trong hóa đơn.");

        if (soLuongMoi < 0)
        {
            throw new BusinessException("Số lượng không được âm.", nameof(ChiTietHoaDon.SoLuong));
        }

        if (soLuongMoi == 0)
        {
            ds.Remove(dong);
        }
        else
        {
            KiemTraTon(dong.TenSach, soLuongMoi, dong.SoLuongTon);
            dong.SoLuong = soLuongMoi;
        }

        DanhSoThuTu(ds);
    }

    /// <summary>
    /// Lưu hóa đơn:
    /// 1) quyền Thu ngân/Quản lý; 2) HD-01; 3) tải lại giá + tồn hiện tại – giá đổi thì cập nhật dòng và
    /// báo thu ngân kiểm tra lại, vượt tồn thì báo lỗi; 4) TÍNH LẠI khuyến mãi (không tin số từ GUI – KM-01);
    /// 5) HD-03; 6) MaTK lấy từ phiên đăng nhập; 7) gọi sp_LapHoaDon (transaction trong SP – HD-05);
    /// 8) phát HoaDonDaLuu. Các trường tiền của <paramref name="hd"/> được ghi lại theo số đã lưu.
    /// </summary>
    /// <returns>Mã hóa đơn đã lưu.</returns>
    public string LuuHoaDon(HoaDon hd)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);

        // HD-01
        if (hd.ChiTiet.Count == 0)
        {
            throw new BusinessException("Hóa đơn chưa có sách nào.", nameof(HoaDon.ChiTiet));
        }

        if (hd.ChiTiet.Any(ct => ct.SoLuong <= 0))
        {
            throw new BusinessException("Số lượng mỗi dòng phải lớn hơn 0.", nameof(ChiTietHoaDon.SoLuong));
        }

        // Tải lại giá và tồn hiện hành (HD-02, HD-04)
        Dictionary<string, Sach> sachHienTai = XuLyLoiCSDL
            .Goi(() => _sachDal.LayTheoDanhSachMa(hd.ChiTiet.Select(ct => ct.MaSach)))
            .ToDictionary(s => s.MaSach, StringComparer.OrdinalIgnoreCase);

        var giaDaDoi = new List<string>();
        foreach (ChiTietHoaDon dong in hd.ChiTiet)
        {
            if (!sachHienTai.TryGetValue(dong.MaSach, out Sach? s) || !s.TrangThai)
            {
                throw new BusinessException($"Sách \"{dong.TenSach}\" đã ngừng bán, vui lòng bỏ khỏi hóa đơn.");
            }

            if (s.GiaBan != dong.DonGia)
            {
                giaDaDoi.Add($"\"{s.TenSach}\": {DinhDangTien(dong.DonGia)} → {DinhDangTien(s.GiaBan)}");
                dong.DonGia = s.GiaBan;
            }

            dong.SoLuongTon = s.SoLuongTon;
            dong.MaLoai = s.MaLoai;
        }

        foreach (var nhom in hd.ChiTiet.GroupBy(ct => ct.MaSach, StringComparer.OrdinalIgnoreCase))
        {
            ChiTietHoaDon dau = nhom.First();
            KiemTraTon(dau.TenSach, nhom.Sum(ct => ct.SoLuong), dau.SoLuongTon);
        }

        if (giaDaDoi.Count > 0)
        {
            throw new BusinessException(
                "Giá sách đã thay đổi, hóa đơn đã được cập nhật giá mới:\n" + string.Join("\n", giaDaDoi) +
                "\nVui lòng kiểm tra lại tổng tiền rồi lưu lại.", nameof(HoaDon.ChiTiet));
        }

        // Tính lại khuyến mãi từ dữ liệu mới nhất (KM-01, KM-02)
        _khuyenMaiBll.XoaCache();
        KetQuaKhuyenMai km = _khuyenMaiBll.ChonKhuyenMaiTotNhat(hd.ChiTiet, DateTime.Today);

        hd.TamTinh = TinhTamTinh(hd.ChiTiet);
        hd.GiamGia = km.TienGiam;
        hd.MaKM = km.CoApDung ? km.KhuyenMaiApDung!.MaKM : null;
        hd.TenKM = km.CoApDung ? km.KhuyenMaiApDung!.TenKM : null;
        hd.TongThanhToan = hd.TamTinh - hd.GiamGia;

        // HD-03
        if (hd.TienKhachDua < hd.TongThanhToan)
        {
            throw new BusinessException(
                $"Tiền khách đưa chưa đủ. Cần thanh toán {DinhDangTien(hd.TongThanhToan)}.", nameof(HoaDon.TienKhachDua));
        }

        hd.TienThoi = TinhTienThoi(hd.TongThanhToan, hd.TienKhachDua);
        hd.MaTK = PhienDangNhap.MaTK;
        hd.TenThuNgan = PhienDangNhap.TaiKhoanHienTai!.HoTen;

        string maHD = XuLyLoiCSDL.Goi(() => _dal.LapHoaDon(hd));

        hd.MaHD = maHD;
        hd.NgayLap = DateTime.Now;
        hd.TrangThai = TrangThaiHoaDon.HoanTat;
        foreach (ChiTietHoaDon dong in hd.ChiTiet)
        {
            dong.MaHD = maHD;
        }

        PhatSuKien(HoaDonDaLuu, new HoaDonEventArgs(maHD, hd.MaTK, hd.TongThanhToan));
        return maHD;
    }

    /// <summary>
    /// Hủy hóa đơn (chỉ Quản lý, lý do &gt;= 5 ký tự – HD-06); tồn kho được hoàn trong sp_HuyHoaDon.
    /// </summary>
    public void HuyHoaDon(string maHD, string lyDo)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);

        string lyDoDaCat = (lyDo ?? string.Empty).Trim();
        if (lyDoDaCat.Length < DoDaiLyDoToiThieu)
        {
            throw new BusinessException($"Lý do hủy phải có ít nhất {DoDaiLyDoToiThieu} ký tự.", nameof(HoaDon.LyDoHuy));
        }

        HoaDon hd = XuLyLoiCSDL.Goi(() => _dal.LayChiTiet(maHD))
            ?? throw new BusinessException($"Không tìm thấy hóa đơn {maHD}.");

        if (hd.DaHuy)
        {
            throw new BusinessException($"Hóa đơn {maHD} đã bị hủy trước đó.");
        }

        XuLyLoiCSDL.Goi(() => _dal.HuyHoaDon(maHD, PhienDangNhap.MaTK, lyDoDaCat));
        PhatSuKien(HoaDonDaHuy, new HoaDonEventArgs(maHD, hd.MaTK, hd.TongThanhToan));
    }

    /// <summary>
    /// Tra cứu hóa đơn (Thu ngân được tra cứu mọi hóa đơn – đặc tả 3.5).
    /// </summary>
    public List<HoaDon> TraCuu(string? maHD, string? tuKhoaKhach, DateTime tuNgay, DateTime denNgay,
        int? maTK, byte? trangThai)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        if (tuNgay.Date > denNgay.Date)
        {
            throw new BusinessException("Từ ngày phải nhỏ hơn hoặc bằng đến ngày.", "DenNgay");
        }

        return XuLyLoiCSDL.Goi(() => _dal.TraCuu(maHD, tuKhoaKhach, tuNgay, denNgay, maTK, trangThai));
    }

    /// <summary>Hóa đơn kèm chi tiết để xem / in lại; null nếu không có.</summary>
    public HoaDon? LayChiTiet(string maHD)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.LayChiTiet(maHD));
    }

    /// <summary>Không bán vượt tồn (HD-02).</summary>
    private static void KiemTraTon(string tenSach, int soLuongCan, int soLuongTon)
    {
        if (soLuongTon <= 0)
        {
            throw new BusinessException($"Sách \"{tenSach}\" đã hết hàng.", nameof(ChiTietHoaDon.SoLuong));
        }

        if (soLuongCan > soLuongTon)
        {
            throw new BusinessException($"Sách \"{tenSach}\" chỉ còn {soLuongTon} cuốn.", nameof(ChiTietHoaDon.SoLuong));
        }
    }

    /// <summary>Đánh lại số thứ tự 1..n.</summary>
    private static void DanhSoThuTu(List<ChiTietHoaDon> ds)
    {
        for (int i = 0; i < ds.Count; i++)
        {
            ds[i].Stt = i + 1;
        }
    }

    /// <summary>Định dạng "225.900đ".</summary>
    private static string DinhDangTien(decimal soTien) => soTien.ToString("#,##0", VanHoaVN) + "đ";

    /// <summary>
    /// Gọi từng hàm đăng ký sự kiện. Hóa đơn ĐÃ lưu/hủy xong nên lỗi ở nơi nhận (ví dụ bảng điều khiển
    /// làm mới thất bại) không được làm thu ngân tưởng thao tác thất bại – lỗi đó bị bỏ qua.
    /// </summary>
    private void PhatSuKien(EventHandler<HoaDonEventArgs>? suKien, HoaDonEventArgs e)
    {
        if (suKien == null)
        {
            return;
        }

        foreach (EventHandler<HoaDonEventArgs> xuLy in suKien.GetInvocationList().Cast<EventHandler<HoaDonEventArgs>>())
        {
            try
            {
                xuLy(this, e);
            }
            catch (Exception)
            {
                // Bỏ qua: nơi nhận tự chịu trách nhiệm hiển thị lỗi của mình.
            }
        }
    }
}
