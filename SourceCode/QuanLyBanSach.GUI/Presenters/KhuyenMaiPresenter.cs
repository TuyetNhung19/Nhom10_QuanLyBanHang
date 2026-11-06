// File: QuanLyBanSach.GUI/Presenters/KhuyenMaiPresenter.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.GUI.Presenters;

/// <summary>Tình trạng hiển thị của một khuyến mãi tại một ngày (Form đổi thành chữ + màu).</summary>
public enum TinhTrangKhuyenMai
{
    /// <summary>Đã tắt.</summary>
    DangTat,

    /// <summary>Chưa tới ngày bắt đầu.</summary>
    ChuaBatDau,

    /// <summary>Đã qua ngày kết thúc.</summary>
    HetHan,

    /// <summary>Đang bật và trong thời gian hiệu lực.</summary>
    DangHieuLuc
}

/// <summary>
/// Presenter của màn hình Khuyến mãi (MVP, ĐT3): 3 loại khuyến mãi, bật/tắt ô nhập theo loại,
/// xem trước điều kiện, xóa có kiểm tra KM-04 (đã dùng thì đề nghị TẮT).
/// Không dùng control hay MessageBox trực tiếp.
/// </summary>
public class KhuyenMaiPresenter
{
    /// <summary>Chế độ làm việc hiện tại của màn hình.</summary>
    private enum CheDo
    {
        Xem,
        Them,
        Sua
    }

    /// <summary>Phần trăm giảm gợi ý khi thêm mới.</summary>
    private const int PhanTramMacDinh = 10;

    /// <summary>Số ngày hiệu lực gợi ý khi thêm mới.</summary>
    private const int SoNgayMacDinh = 30;

    private readonly IKhuyenMaiView _view;
    private readonly KhuyenMaiBLL _bll;
    private readonly LoaiSachBLL _loaiSachBll;
    private List<KhuyenMai> _danhSach = new();
    private List<LoaiSach> _dsLoaiSach = new();
    private CheDo _cheDo = CheDo.Xem;

    /// <summary>
    /// true khi Presenter đang tự gán giá trị vào View: các sự kiện "giá trị thay đổi" do chính
    /// việc gán đó sinh ra sẽ bị bỏ qua, tránh tính lại xem trước nhiều lần.
    /// </summary>
    private bool _dangDoDuLieu;

    /// <summary>Gắn presenter với view.</summary>
    /// <param name="view">Form (hoặc View giả khi kiểm thử).</param>
    /// <param name="bll">BLL khuyến mãi; bỏ trống = BLL thật (kiểm thử truyền BLL giả).</param>
    /// <param name="loaiSachBll">BLL loại sách để nạp ComboBox; bỏ trống = BLL thật.</param>
    public KhuyenMaiPresenter(IKhuyenMaiView view, KhuyenMaiBLL? bll = null, LoaiSachBLL? loaiSachBll = null)
    {
        _view = view;
        _bll = bll ?? new KhuyenMaiBLL();
        _loaiSachBll = loaiSachBll ?? new LoaiSachBLL();
    }

    /// <summary>Mở màn hình: nạp danh mục, tải danh sách, về chế độ xem.</summary>
    public void KhoiTao()
    {
        try
        {
            _dsLoaiSach = _loaiSachBll.LayDanhSach();
        }
        catch (Exception ex)
        {
            _dsLoaiSach = new List<LoaiSach>();
            XuLyLoi(ex);
        }

        _dangDoDuLieu = true;
        try
        {
            _view.HienDanhMuc(new List<KeyValuePair<LoaiKhuyenMai, string>>
            {
                new(LoaiKhuyenMai.TheoTongDon, "Theo tổng đơn"),
                new(LoaiKhuyenMai.TheoLoaiSach, "Theo loại sách"),
                new(LoaiKhuyenMai.TheoSoLuong, "Theo số cuốn")
            }, _dsLoaiSach);
        }
        finally
        {
            _dangDoDuLieu = false;
        }

        TaiDanhSach();
        DatCheDo(CheDo.Xem);
    }

    /// <summary>Tải danh sách khuyến mãi và chọn lại dòng có mã cho trước.</summary>
    public void TaiDanhSach(int? maKMChon = null)
    {
        try
        {
            int? maDangChon = maKMChon ?? _view.MaKMDangChon;
            _danhSach = _bll.LayDanhSach();
            _view.HienDanhSach(_danhSach, maDangChon);
            HienKhuyenMaiDangChon();
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Nút Làm mới.</summary>
    public void LamMoi() => TaiDanhSach();

    /// <summary>Người dùng chọn dòng khác trên lưới (chỉ xử lý khi đang xem).</summary>
    public void ChonDong()
    {
        if (_cheDo == CheDo.Xem)
        {
            HienKhuyenMaiDangChon();
        }
    }

    /// <summary>Người dùng đổi loại khuyến mãi: bật đúng các ô của loại đó.</summary>
    public void DoiLoaiKhuyenMai()
    {
        if (!_dangDoDuLieu)
        {
            CapNhatTheoLoai();
        }
    }

    /// <summary>Một ô nhập bất kỳ đổi giá trị: cập nhật dòng xem trước.</summary>
    public void GiaTriThayDoi()
    {
        if (!_dangDoDuLieu)
        {
            CapNhatXemTruoc();
        }
    }

    /// <summary>Bắt đầu thêm mới với giá trị gợi ý.</summary>
    public void Them()
    {
        DatCheDo(CheDo.Them);
        XoaONhap();
        CapNhatTheoLoai();
    }

    /// <summary>Bắt đầu sửa khuyến mãi đang chọn.</summary>
    public void Sua()
    {
        if (KhuyenMaiDangChon() == null)
        {
            _view.HienLoi(null, "Vui lòng chọn khuyến mãi cần sửa.");
            return;
        }

        DatCheDo(CheDo.Sua);
    }

    /// <summary>Lưu thêm mới hoặc sửa; lỗi nghiệp vụ được gắn vào đúng ô nhập.</summary>
    public void Luu()
    {
        KhuyenMai km = DocONhap();

        try
        {
            if (_cheDo == CheDo.Them)
            {
                _bll.Them(km);
            }
            else
            {
                _bll.Sua(km);
            }

            _bll.XoaCache();
            _view.HienThongBao($"Đã lưu khuyến mãi \"{km.TenKM}\".");
            DatCheDo(CheDo.Xem);
            TaiDanhSach(km.MaKM);
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Hủy thêm/sửa, trả lại dữ liệu dòng đang chọn.</summary>
    public void Huy()
    {
        DatCheDo(CheDo.Xem);
        HienKhuyenMaiDangChon();
    }

    /// <summary>
    /// Xóa khuyến mãi. Đã dùng trong hóa đơn (KM-04) thì không xóa mà đề nghị chuyển sang TẮT.
    /// </summary>
    public void Xoa()
    {
        KhuyenMai? km = KhuyenMaiDangChon();
        if (km == null)
        {
            _view.HienLoi(null, "Vui lòng chọn khuyến mãi cần xóa.");
            return;
        }

        try
        {
            if (_bll.DaDuocApDung(km.MaKM))
            {
                if (!km.TrangThai)
                {
                    _view.HienThongBao("Khuyến mãi đã dùng trong hóa đơn nên không thể xóa, và hiện đã ở trạng thái TẮT.");
                    return;
                }

                if (_view.XacNhan("Khuyến mãi đã dùng trong hóa đơn, chuyển sang TẮT?"))
                {
                    _bll.DoiTrangThai(km.MaKM, false);
                    _bll.XoaCache();
                    TaiDanhSach(km.MaKM);
                }

                return;
            }

            if (_view.XacNhan($"Xóa khuyến mãi \"{km.TenKM}\"?"))
            {
                _bll.Xoa(km.MaKM);
                _bll.XoaCache();
                TaiDanhSach();
            }
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Tình trạng của khuyến mãi tại ngày <paramref name="homNay"/> (cột "Tình trạng" trên lưới).</summary>
    public static TinhTrangKhuyenMai TinhTrang(KhuyenMai km, DateTime homNay) => km switch
    {
        { TrangThai: false } => TinhTrangKhuyenMai.DangTat,
        _ when homNay.Date < km.NgayBatDau.Date => TinhTrangKhuyenMai.ChuaBatDau,
        _ when homNay.Date > km.NgayKetThuc.Date => TinhTrangKhuyenMai.HetHan,
        _ => TinhTrangKhuyenMai.DangHieuLuc
    };

    /// <summary>Đổ khuyến mãi đang chọn lên ô nhập (không chọn thì xóa trắng).</summary>
    private void HienKhuyenMaiDangChon()
    {
        KhuyenMai? km = KhuyenMaiDangChon();
        if (km == null)
        {
            XoaONhap();
        }
        else
        {
            _dangDoDuLieu = true;
            try
            {
                _view.TenKM = km.TenKM;
                _view.LoaiKM = km.LoaiKM;
                _view.NguongTien = km.NguongTien ?? 0;
                _view.PhanTramGiam = km.PhanTramGiam ?? 0;
                _view.NguongSoLuong = km.NguongSoLuong ?? 0;
                _view.SoTienGiam = km.SoTienGiam ?? 0;
                _view.MaLoaiSach = km.MaLoai;
                _view.NgayBatDau = km.NgayBatDau;
                _view.NgayKetThuc = km.NgayKetThuc;
                _view.DangBat = km.TrangThai;
            }
            finally
            {
                _dangDoDuLieu = false;
            }
        }

        CapNhatTheoLoai();

        if (_cheDo == CheDo.Xem)
        {
            _view.DatCheDo(false, km != null);
        }
    }

    /// <summary>
    /// Bật ô thuộc loại đang chọn, tắt và đưa về giá trị nhỏ nhất các ô không thuộc loại:
    /// Loại 1: ngưỡng tiền + %; Loại 2: loại sách + %; Loại 3: số cuốn + số tiền giảm.
    /// </summary>
    private void CapNhatTheoLoai()
    {
        bool nhap = _cheDo != CheDo.Xem;
        LoaiKhuyenMai loai = _view.LoaiKM;

        bool dungNguongTien = loai == LoaiKhuyenMai.TheoTongDon;
        bool dungLoaiSach = loai == LoaiKhuyenMai.TheoLoaiSach;
        bool dungPhanTram = loai != LoaiKhuyenMai.TheoSoLuong;
        bool dungSoLuong = loai == LoaiKhuyenMai.TheoSoLuong;

        _view.BatONhapTheoLoai(nhap && dungNguongTien, nhap && dungLoaiSach, nhap && dungPhanTram, nhap && dungSoLuong);

        if (nhap)
        {
            _dangDoDuLieu = true;
            try
            {
                // Gán 0 = về giá trị nhỏ nhất của ô (View tự đưa về Minimum)
                if (!dungNguongTien)
                {
                    _view.NguongTien = 0;
                }

                if (!dungLoaiSach)
                {
                    _view.MaLoaiSach = null;
                }
                else if (_view.MaLoaiSach == null && _dsLoaiSach.Count > 0)
                {
                    _view.MaLoaiSach = _dsLoaiSach[0].MaLoai;
                }

                if (!dungPhanTram)
                {
                    _view.PhanTramGiam = 0;
                }

                if (!dungSoLuong)
                {
                    _view.NguongSoLuong = 0;
                    _view.SoTienGiam = 0;
                }
            }
            finally
            {
                _dangDoDuLieu = false;
            }
        }

        CapNhatXemTruoc();
    }

    /// <summary>Xem trước điều kiện bằng KhuyenMai.MoTaDieuKien, kèm thời gian hiệu lực.</summary>
    private void CapNhatXemTruoc()
    {
        KhuyenMai km = DocONhap();
        _view.HienXemTruoc($"Điều kiện: {km.MoTaDieuKien}  •  Hiệu lực: {km.ThoiGianText}");
    }

    /// <summary>Đọc ô nhập thành DTO; chỉ lấy các trường thuộc loại đang chọn.</summary>
    private KhuyenMai DocONhap()
    {
        LoaiKhuyenMai loai = _view.LoaiKM;
        var km = new KhuyenMai
        {
            MaKM = _cheDo == CheDo.Sua ? KhuyenMaiDangChon()?.MaKM ?? 0 : 0,
            TenKM = _view.TenKM,
            LoaiKM = loai,
            NgayBatDau = _view.NgayBatDau.Date,
            NgayKetThuc = _view.NgayKetThuc.Date,
            TrangThai = _view.DangBat
        };

        switch (loai)
        {
            case LoaiKhuyenMai.TheoTongDon:
                km.NguongTien = _view.NguongTien;
                km.PhanTramGiam = _view.PhanTramGiam;
                break;
            case LoaiKhuyenMai.TheoLoaiSach:
                km.MaLoai = _view.MaLoaiSach;
                km.TenLoaiSach = _view.TenLoaiSach;
                km.PhanTramGiam = _view.PhanTramGiam;
                break;
            case LoaiKhuyenMai.TheoSoLuong:
                km.NguongSoLuong = _view.NguongSoLuong;
                km.SoTienGiam = _view.SoTienGiam;
                break;
        }

        return km;
    }

    /// <summary>Xóa trắng ô nhập: loại 1, giảm 10%, từ hôm nay đến 30 ngày sau, đang bật.</summary>
    private void XoaONhap()
    {
        _dangDoDuLieu = true;
        try
        {
            _view.TenKM = string.Empty;
            _view.LoaiKM = LoaiKhuyenMai.TheoTongDon;
            _view.NguongTien = 0;
            _view.PhanTramGiam = PhanTramMacDinh;
            _view.NguongSoLuong = 0;
            _view.SoTienGiam = 0;
            _view.MaLoaiSach = null;
            _view.NgayBatDau = DateTime.Today;
            _view.NgayKetThuc = DateTime.Today.AddDays(SoNgayMacDinh);
            _view.DangBat = true;
            _view.XoaLoi();
        }
        finally
        {
            _dangDoDuLieu = false;
        }
    }

    /// <summary>Khuyến mãi ứng với dòng đang chọn.</summary>
    private KhuyenMai? KhuyenMaiDangChon()
    {
        int? ma = _view.MaKMDangChon;
        return ma == null ? null : _danhSach.FirstOrDefault(k => k.MaKM == ma);
    }

    private void DatCheDo(CheDo cheDo)
    {
        _cheDo = cheDo;
        _view.DatCheDo(cheDo != CheDo.Xem, KhuyenMaiDangChon() != null);
        CapNhatTheoLoai();
    }

    private void XuLyLoi(Exception ex)
    {
        (string? truong, string thongDiep) = ThongDiepLoi.Tu(ex);
        _view.HienLoi(truong, thongDiep);
    }
}
