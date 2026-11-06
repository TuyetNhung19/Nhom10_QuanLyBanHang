// File: QuanLyBanSach.Tests/KhachHangPresenterTests.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Presenters;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.Tests;

/// <summary>
/// Kiểm thử KhachHangPresenter KHÔNG mở Form, KHÔNG cần CSDL:
/// - ViewGia cài IKhachHangView, chỉ ghi lại những gì Presenter ra lệnh.
/// - BllGia kế thừa KhachHangBLL, ghi đè các phương thức virtual để trả dữ liệu cố định.
/// Đây là lợi ích chính của MVP: logic màn hình nằm ở Presenter nên kiểm thử được tự động.
/// </summary>
[TestClass]
public class KhachHangPresenterTests
{
    private ViewGia _view = null!;
    private BllGia _bll = null!;

    [TestInitialize]
    public void KhoiTao()
    {
        _view = new ViewGia();
        _bll = new BllGia();
        _bll.DanhSach.Add(new KhachHang { MaKH = 1, HoTen = "Nguyễn Văn An", SDT = "0901234567" });
        _bll.DanhSach.Add(new KhachHang { MaKH = 2, HoTen = "Trần Thị Bình", SDT = "0912345678", Email = "binh@gmail.com" });
    }

    [TestMethod]
    public void KhoiTao_TaiDanhSach_VaDoKhachDauTienLenONhap()
    {
        var presenter = new KhachHangPresenter(_view, null, false, _bll);

        presenter.KhoiTao();

        Assert.AreEqual(2, _view.DanhSach.Count);
        Assert.AreEqual("Nguyễn Văn An", _view.HoTen);
        Assert.AreEqual("0901234567", _view.SDT);
        Assert.IsFalse(_view.DangNhap, "Mở màn hình ở chế độ xem");
        Assert.IsTrue(_view.CoKhachDangChon, "Có dòng chọn nên nút Sửa/Xóa được bật");
    }

    [TestMethod]
    public void ChonDong_DoiKhach_HienThongTinVaTongKetLichSu()
    {
        _bll.LichSu[2] = new List<HoaDon>
        {
            new() { MaHD = "HD1", TongThanhToan = 100_000, TrangThai = TrangThaiHoaDon.HoanTat },
            new() { MaHD = "HD2", TongThanhToan = 50_000, TrangThai = TrangThaiHoaDon.HoanTat },
            new() { MaHD = "HD3", TongThanhToan = 70_000, TrangThai = TrangThaiHoaDon.DaHuy }
        };
        var presenter = new KhachHangPresenter(_view, null, false, _bll);
        presenter.KhoiTao();

        _view.MaKHDangChon = 2; // người dùng bấm dòng thứ hai
        presenter.ChonDong();

        Assert.AreEqual("Trần Thị Bình", _view.HoTen);
        Assert.AreEqual("binh@gmail.com", _view.Email);
        Assert.AreEqual(3, _view.LichSu!.Count);
        // Hóa đơn đã hủy không được cộng vào tổng
        Assert.AreEqual("Trần Thị Bình: 2 lần mua – tổng 150.000đ (1 hóa đơn đã hủy)", _view.TongKet);
    }

    [TestMethod]
    public void Sua_ChuaChonKhach_BaoLoiVaVanOCheDoXem()
    {
        _bll.DanhSach.Clear();
        var presenter = new KhachHangPresenter(_view, null, false, _bll);
        presenter.KhoiTao();

        presenter.Sua();

        Assert.AreEqual("Vui lòng chọn khách hàng cần sửa.", _view.LoiCuoi);
        Assert.IsFalse(_view.DangNhap);
    }

    [TestMethod]
    public void Luu_ThemMoi_GoiBllThem_ThongBao_VaChonLaiKhachVuaThem()
    {
        var presenter = new KhachHangPresenter(_view, null, false, _bll);
        presenter.KhoiTao();

        presenter.Them();
        Assert.IsTrue(_view.DangNhap, "Bấm Thêm thì vào chế độ nhập");
        Assert.AreEqual(string.Empty, _view.HoTen, "Ô nhập được xóa trắng");

        _view.HoTen = "Lê Minh Châu";
        _view.SDT = "0987654321";
        presenter.Luu();

        Assert.AreEqual("Lê Minh Châu", _bll.KhachDaThem?.HoTen);
        Assert.AreEqual("Đã thêm khách hàng \"Lê Minh Châu\".", _view.ThongBaoCuoi);
        Assert.IsFalse(_view.DangNhap, "Lưu xong quay về chế độ xem");
        Assert.AreEqual(3, _view.MaKHDangChon, "Lưới chọn lại đúng khách vừa thêm");
    }

    [TestMethod]
    public void Luu_LoiNghiepVu_GanLoiVaoDungO_VaGiuCheDoNhap()
    {
        _bll.LoiKhiLuu = new BusinessException("Số điện thoại 0901234567 đã thuộc khách hàng khác.", nameof(KhachHang.SDT));
        var presenter = new KhachHangPresenter(_view, null, false, _bll);
        presenter.KhoiTao();
        presenter.Them();

        presenter.Luu();

        Assert.AreEqual("SDT", _view.TruongLoi, "View gắn ErrorProvider vào ô txtSDT");
        Assert.IsTrue(_view.DangNhap, "Lỗi thì vẫn ở chế độ nhập để người dùng sửa");
        Assert.IsNull(_view.ThongBaoCuoi);
    }

    [TestMethod]
    public void Xoa_NguoiDungChonKhong_KhongGoiBll()
    {
        var presenter = new KhachHangPresenter(_view, null, false, _bll);
        presenter.KhoiTao();
        _view.TraLoiXacNhan = false;

        presenter.Xoa();

        Assert.AreEqual("Xóa khách hàng \"Nguyễn Văn An\" (0901234567)?", _view.CauHoiCuoi);
        Assert.IsNull(_bll.MaDaXoa);
    }

    [TestMethod]
    public void Xoa_NguoiDungChonCo_GoiBllXoa()
    {
        var presenter = new KhachHangPresenter(_view, null, false, _bll);
        presenter.KhoiTao();
        _view.TraLoiXacNhan = true;

        presenter.Xoa();

        Assert.AreEqual(1, _bll.MaDaXoa);
        Assert.AreEqual("Đã xóa khách hàng.", _view.ThongBaoCuoi);
    }

    [TestMethod]
    public void CheDoChon_SdtChuaCo_MoSanThemMoi_LuuXongTraKhachVeBanHang()
    {
        var presenter = new KhachHangPresenter(_view, "0999888777", cheDoChon: true, _bll);

        presenter.KhoiTao();

        Assert.IsTrue(_view.CheDoChon);
        Assert.IsTrue(_view.DangNhap, "Không tìm thấy SĐT nên mở sẵn chế độ thêm mới");
        Assert.AreEqual("0999888777", _view.SDT, "SĐT gõ ở màn hình bán hàng được điền sẵn");

        _view.HoTen = "Khách mới";
        presenter.Luu();

        Assert.IsTrue(_view.DaDong);
        Assert.AreEqual("Khách mới", _view.KhachTraVe?.HoTen);
    }

    [TestMethod]
    public void NhapDupDong_KhongPhaiCheDoChon_KhongDongManHinh()
    {
        var presenter = new KhachHangPresenter(_view, null, false, _bll);
        presenter.KhoiTao();

        presenter.NhapDupDong();

        Assert.IsFalse(_view.DaDong);
    }

    // ===================== View giả và BLL giả =====================

    /// <summary>View giả: lưu lại mọi lệnh Presenter gửi tới để kiểm tra.</summary>
    private sealed class ViewGia : IKhachHangView
    {
        public List<KhachHang> DanhSach { get; private set; } = new();
        public List<HoaDon>? LichSu { get; private set; }
        public string TongKet { get; private set; } = string.Empty;
        public bool DangNhap { get; private set; }
        public bool CoKhachDangChon { get; private set; }
        public bool CheDoChon { get; private set; }
        public string? LoiCuoi { get; private set; }
        public string? TruongLoi { get; private set; }
        public string? ThongBaoCuoi { get; private set; }
        public string? CauHoiCuoi { get; private set; }
        public bool TraLoiXacNhan { get; set; }
        public bool DaDong { get; private set; }
        public KhachHang? KhachTraVe { get; private set; }

        public int? MaKHDangChon { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string TuKhoa { get; set; } = string.Empty;

        public void CaiDatManHinh(bool choPhepXoa, bool cheDoChon) => CheDoChon = cheDoChon;

        /// <summary>Giống DataGridView: chọn dòng có mã yêu cầu, không có thì chọn dòng đầu.</summary>
        public void HienDanhSach(List<KhachHang> danhSach, int? maKHChon)
        {
            DanhSach = danhSach;
            MaKHDangChon = danhSach.Any(k => k.MaKH == maKHChon) ? maKHChon : danhSach.FirstOrDefault()?.MaKH;
        }

        public void HienLichSu(List<HoaDon>? lichSu, string tongKet)
        {
            LichSu = lichSu;
            TongKet = tongKet;
        }

        public void HienThongBao(string thongDiep) => ThongBaoCuoi = thongDiep;

        public void HienLoi(string? truong, string thongDiep)
        {
            TruongLoi = truong;
            LoiCuoi = thongDiep;
        }

        public bool XacNhan(string thongDiep)
        {
            CauHoiCuoi = thongDiep;
            return TraLoiXacNhan;
        }

        public void XoaONhap()
        {
            HoTen = SDT = Email = DiaChi = string.Empty;
        }

        public void DatCheDo(bool dangNhap, bool coKhachDangChon)
        {
            DangNhap = dangNhap;
            CoKhachDangChon = coKhachDangChon;
        }

        public void DongVaTraVe(KhachHang? khach)
        {
            DaDong = true;
            KhachTraVe = khach;
        }
    }

    /// <summary>BLL giả: dữ liệu trong bộ nhớ, không đụng CSDL, không kiểm tra đăng nhập.</summary>
    private sealed class BllGia : KhachHangBLL
    {
        public List<KhachHang> DanhSach { get; } = new();
        public Dictionary<int, List<HoaDon>> LichSu { get; } = new();
        public Exception? LoiKhiLuu { get; set; }
        public KhachHang? KhachDaThem { get; private set; }
        public int? MaDaXoa { get; private set; }

        public override List<KhachHang> TimKiem(string? tuKhoa) =>
            DanhSach.Where(k => string.IsNullOrWhiteSpace(tuKhoa) || k.SDT.Contains(tuKhoa) || k.HoTen.Contains(tuKhoa)).ToList();

        public override List<HoaDon> LayLichSuHoaDon(int maKH) =>
            LichSu.TryGetValue(maKH, out List<HoaDon>? ds) ? ds : new List<HoaDon>();

        public override int Them(KhachHang kh)
        {
            if (LoiKhiLuu != null)
            {
                throw LoiKhiLuu;
            }

            kh.MaKH = DanhSach.Max(k => k.MaKH) + 1;
            DanhSach.Add(kh);
            KhachDaThem = kh;
            return kh.MaKH;
        }

        public override void Sua(KhachHang kh)
        {
            if (LoiKhiLuu != null)
            {
                throw LoiKhiLuu;
            }
        }

        public override void Xoa(int maKH) => MaDaXoa = maKH;
    }
}
