// File: QuanLyBanSach.BLL/KhuyenMaiBLL.cs
using QuanLyBanSach.BLL.KhuyenMai;
using QuanLyBanSach.DAL;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Nghiệp vụ khuyến mãi (ĐT3): tự chọn khuyến mãi có lợi nhất cho đơn và quản lý chương trình (Quản lý).
/// (FrmKhuyenMai dùng MVP: KhuyenMaiPresenter gọi lớp này. Các phương thức virtual để test Presenter thay bằng BLL giả.)
/// </summary>
public class KhuyenMaiBLL
{
    /// <summary>Thời gian giữ danh sách khuyến mãi hiệu lực trong bộ nhớ.</summary>
    private static readonly TimeSpan ThoiGianCache = TimeSpan.FromSeconds(60);

    private static readonly object KhoaCache = new();
    private static List<DTO.KhuyenMai>? _cache;
    private static DateTime _cacheChoNgay;
    private static DateTime _cacheLuc;

    private readonly KhuyenMaiDAL _dal = new();

    /// <summary>
    /// Chọn khuyến mãi giảm nhiều nhất cho đơn tại ngày <paramref name="ngay"/> (KM-01, KM-02).
    /// Danh sách khuyến mãi hiệu lực được cache 60 giây để không gọi CSDL mỗi lần gõ phím.
    /// </summary>
    public KetQuaKhuyenMai ChonKhuyenMaiTotNhat(IReadOnlyList<ChiTietHoaDon> chiTiet, DateTime ngay)
    {
        return ChonKhuyenMaiTotNhat(chiTiet, ngay, LayKhuyenMaiHieuLuc(ngay));
    }

    /// <summary>
    /// Bản không cần CSDL (dùng cho kiểm thử): chọn trên danh sách khuyến mãi truyền vào.
    /// - Chỉ xét KM đang bật và còn hạn tại ngày bán (KM-02).
    /// - Tính tiền giảm từng KM qua quy tắc đa hình, lấy MAX; bằng nhau lấy MaKM nhỏ hơn; không cộng dồn (KM-01).
    /// - LoiNhac: câu nhắc của KM chưa đạt mà mức giảm tiềm năng lớn hơn mức đang áp dụng, ưu tiên ngưỡng gần nhất.
    /// </summary>
    public KetQuaKhuyenMai ChonKhuyenMaiTotNhat(IReadOnlyList<ChiTietHoaDon> chiTiet, DateTime ngay,
        IEnumerable<DTO.KhuyenMai> dsKhuyenMai)
    {
        if (chiTiet.Count == 0)
        {
            return new KetQuaKhuyenMai();
        }

        List<IQuyTacKhuyenMai> dsQuyTac = dsKhuyenMai
            .Where(km => km.ConHieuLuc(ngay))
            .Select(QuyTacKhuyenMaiFactory.Tao)
            .ToList();

        var tot = dsQuyTac
            .Select(qt => new { QuyTac = qt, Giam = qt.TinhGiam(chiTiet) })
            .Where(x => x.Giam > 0)
            .OrderByDescending(x => x.Giam)
            .ThenBy(x => x.QuyTac.ThongTin.MaKM)
            .FirstOrDefault();

        decimal giamHienTai = tot?.Giam ?? 0;

        string? loiNhac = dsQuyTac
            .Where(qt => qt != tot?.QuyTac)
            .Select(qt => new
            {
                LoiNhac = qt.GoiYMuaThem(chiTiet),
                TiemNang = (qt as QuyTacKhuyenMaiCoSo)?.GiamTiemNang(chiTiet) ?? 0,
                ConThieu = (qt as QuyTacKhuyenMaiCoSo)?.TiLeConThieu(chiTiet) ?? 1
            })
            .Where(x => x.LoiNhac != null && x.TiemNang > giamHienTai)
            .OrderBy(x => x.ConThieu)
            .ThenByDescending(x => x.TiemNang)
            .Select(x => x.LoiNhac)
            .FirstOrDefault();

        return new KetQuaKhuyenMai
        {
            KhuyenMaiApDung = tot?.QuyTac.ThongTin,
            TienGiam = giamHienTai,
            LoiNhac = loiNhac
        };
    }

    /// <summary>Danh sách mọi khuyến mãi (Quản lý).</summary>
    public virtual List<DTO.KhuyenMai> LayDanhSach()
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.LayDanhSach());
    }

    /// <summary>Thêm khuyến mãi (Quản lý); trả về MaKM mới.</summary>
    public virtual int Them(DTO.KhuyenMai km)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        KiemTra(km);
        km.MaKM = XuLyLoiCSDL.Goi(() => _dal.Them(km));
        XoaCache();
        return km.MaKM;
    }

    /// <summary>Sửa khuyến mãi (Quản lý).</summary>
    public virtual void Sua(DTO.KhuyenMai km)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        KiemTra(km);
        if (!XuLyLoiCSDL.Goi(() => _dal.Sua(km)))
        {
            throw new BusinessException("Không tìm thấy khuyến mãi cần sửa.");
        }

        XoaCache();
    }

    /// <summary>Bật/tắt khuyến mãi (Quản lý).</summary>
    public virtual void DoiTrangThai(int maKM, bool dangBat)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        if (!XuLyLoiCSDL.Goi(() => _dal.DoiTrangThai(maKM, dangBat)))
        {
            throw new BusinessException("Không tìm thấy khuyến mãi.");
        }

        XoaCache();
    }

    /// <summary>Khuyến mãi đã được áp dụng vào hóa đơn nào chưa (KM-04) – để GUI đề nghị tắt thay vì xóa.</summary>
    public virtual bool DaDuocApDung(int maKM)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.DaDuocApDung(maKM));
    }

    /// <summary>
    /// Xóa khuyến mãi (Quản lý). Đã áp dụng vào hóa đơn thì chặn và gợi ý tắt (KM-04).
    /// </summary>
    public virtual void Xoa(int maKM)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);

        if (XuLyLoiCSDL.Goi(() => _dal.DaDuocApDung(maKM)))
        {
            throw new BusinessException(
                "Khuyến mãi đã được áp dụng vào hóa đơn nên không thể xóa (KM-04). Hãy tắt khuyến mãi thay vì xóa.");
        }

        if (!XuLyLoiCSDL.Goi(() => _dal.Xoa(maKM)))
        {
            throw new BusinessException("Không tìm thấy khuyến mãi cần xóa.");
        }

        XoaCache();
    }

    /// <summary>Bỏ cache khuyến mãi hiệu lực (gọi sau khi thêm/sửa/bật/tắt/xóa và trước khi lưu hóa đơn).</summary>
    public virtual void XoaCache()
    {
        lock (KhoaCache)
        {
            _cache = null;
        }
    }

    /// <summary>Lấy khuyến mãi hiệu lực tại ngày bán, có cache 60 giây.</summary>
    private List<DTO.KhuyenMai> LayKhuyenMaiHieuLuc(DateTime ngay)
    {
        lock (KhoaCache)
        {
            if (_cache != null && _cacheChoNgay == ngay.Date && DateTime.Now - _cacheLuc < ThoiGianCache)
            {
                return _cache;
            }
        }

        List<DTO.KhuyenMai> ds = XuLyLoiCSDL.Goi(() => _dal.LayHieuLuc(ngay));

        lock (KhoaCache)
        {
            _cache = ds;
            _cacheChoNgay = ngay.Date;
            _cacheLuc = DateTime.Now;
        }

        return ds;
    }

    /// <summary>
    /// Kiểm tra đủ trường theo loại, % trong 1–50 (KM-03), ngày kết thúc &gt;= ngày bắt đầu.
    /// Các trường không thuộc loại được đặt về null để khớp ràng buộc CHECK của CSDL.
    /// </summary>
    private static void KiemTra(DTO.KhuyenMai km)
    {
        km.TenKM = KiemTraDuLieu.BatBuoc(km.TenKM, "Tên khuyến mãi", nameof(DTO.KhuyenMai.TenKM), 100);

        switch (km.LoaiKM)
        {
            case LoaiKhuyenMai.TheoTongDon:
                if (km.NguongTien is null or <= 0)
                {
                    throw new BusinessException("Vui lòng nhập ngưỡng tổng đơn lớn hơn 0.", nameof(DTO.KhuyenMai.NguongTien));
                }

                KiemTraPhanTram(km);
                km.MaLoai = null;
                km.NguongSoLuong = null;
                km.SoTienGiam = null;
                break;

            case LoaiKhuyenMai.TheoLoaiSach:
                if (km.MaLoai is null or <= 0)
                {
                    throw new BusinessException("Vui lòng chọn loại sách được giảm.", nameof(DTO.KhuyenMai.MaLoai));
                }

                KiemTraPhanTram(km);
                km.NguongTien = null;
                km.NguongSoLuong = null;
                km.SoTienGiam = null;
                break;

            case LoaiKhuyenMai.TheoSoLuong:
                if (km.NguongSoLuong is null or <= 0)
                {
                    throw new BusinessException("Vui lòng nhập số cuốn tối thiểu lớn hơn 0.", nameof(DTO.KhuyenMai.NguongSoLuong));
                }

                if (km.SoTienGiam is null or <= 0)
                {
                    throw new BusinessException("Vui lòng nhập số tiền giảm lớn hơn 0.", nameof(DTO.KhuyenMai.SoTienGiam));
                }

                km.NguongTien = null;
                km.MaLoai = null;
                km.PhanTramGiam = null;
                break;

            default:
                throw new BusinessException("Vui lòng chọn loại khuyến mãi.", nameof(DTO.KhuyenMai.LoaiKM));
        }

        if (km.NgayKetThuc.Date < km.NgayBatDau.Date)
        {
            throw new BusinessException("Ngày kết thúc phải sau hoặc bằng ngày bắt đầu.", nameof(DTO.KhuyenMai.NgayKetThuc));
        }
    }

    /// <summary>Phần trăm giảm bắt buộc trong khoảng 1–50 (KM-03).</summary>
    private static void KiemTraPhanTram(DTO.KhuyenMai km)
    {
        if (km.PhanTramGiam is null or < 1 or > QuyTacKhuyenMaiCoSo.PhanTramToiDa)
        {
            throw new BusinessException("Phần trăm giảm phải từ 1 đến 50.", nameof(DTO.KhuyenMai.PhanTramGiam));
        }
    }
}
