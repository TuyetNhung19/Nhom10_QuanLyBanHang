// File: QuanLyBanSach.Tests/KhuyenMaiPresenterTests.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Presenters;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.Tests;

/// <summary>
/// Kiểm thử KhuyenMaiPresenter bằng View giả và BLL giả: bật ô theo loại khuyến mãi,
/// xem trước điều kiện, lưu đúng trường theo loại, xóa có kiểm tra KM-04.
/// </summary>
[TestClass]
public class KhuyenMaiPresenterTests
{
    private ViewGia _view = null!;
    private BllGia _bll = null!;
    private LoaiSachBllGia _loaiSachBll = null!;

    [TestInitialize]
    public void KhoiTao()
    {
        _view = new ViewGia();
        _bll = new BllGia();
        _loaiSachBll = new LoaiSachBllGia();
        _bll.DanhSach.Add(new KhuyenMai
        {
            MaKM = 1, TenKM = "Giảm 10% đơn từ 200.000đ", LoaiKM = LoaiKhuyenMai.TheoTongDon,
            NguongTien = 200_000, PhanTramGiam = 10,
            NgayBatDau = new DateTime(2026, 10, 1), NgayKetThuc = new DateTime(2026, 12, 31), TrangThai = true
        });
    }

    [TestMethod]
    public void KhoiTao_NapDanhMuc_HienKhuyenMaiDauTien_KhoaONhap()
    {
        var presenter = new KhuyenMaiPresenter(_view, _bll, _loaiSachBll);

        presenter.KhoiTao();

        Assert.AreEqual(3, _view.SoLoaiKM);
        Assert.AreEqual("Giảm 10% đơn từ 200.000đ", _view.TenKM);
        Assert.AreEqual(200_000, _view.NguongTien);
        Assert.IsFalse(_view.DangNhap);
        Assert.AreEqual((false, false, false, false), _view.ONhapBat, "Đang xem thì mọi ô riêng đều khóa");
        Assert.AreEqual("Điều kiện: Đơn từ 200.000đ giảm 10%  •  Hiệu lực: 01/10/2026 – 31/12/2026", _view.XemTruoc);
    }

    [TestMethod]
    public void Them_DatGiaTriGoiY_VaBatOCuaLoaiTongDon()
    {
        var presenter = new KhuyenMaiPresenter(_view, _bll, _loaiSachBll);
        presenter.KhoiTao();

        presenter.Them();

        Assert.IsTrue(_view.DangNhap);
        Assert.AreEqual(string.Empty, _view.TenKM);
        Assert.AreEqual(LoaiKhuyenMai.TheoTongDon, _view.LoaiKM);
        Assert.AreEqual(10, _view.PhanTramGiam, "Gợi ý giảm 10%");
        Assert.AreEqual(DateTime.Today.AddDays(30), _view.NgayKetThuc, "Gợi ý hiệu lực 30 ngày");
        Assert.AreEqual((true, false, true, false), _view.ONhapBat, "Loại 1: ngưỡng tiền + phần trăm");
    }

    [TestMethod]
    public void DoiLoai_SangTheoLoaiSach_BatOLoaiSach_VaChonSanLoaiDauTien()
    {
        var presenter = new KhuyenMaiPresenter(_view, _bll, _loaiSachBll);
        presenter.KhoiTao();
        presenter.Them();

        _view.LoaiKM = LoaiKhuyenMai.TheoLoaiSach;
        presenter.DoiLoaiKhuyenMai();

        Assert.AreEqual((false, true, true, false), _view.ONhapBat, "Loại 2: loại sách + phần trăm");
        Assert.AreEqual(1, _view.MaLoaiSach, "Tự chọn loại sách đầu tiên");
        StringAssert.StartsWith(_view.XemTruoc, "Điều kiện: Sách Văn học giảm 10%");
    }

    [TestMethod]
    public void Luu_TheoSoLuong_ChiGuiTruongCuaLoaiDo()
    {
        var presenter = new KhuyenMaiPresenter(_view, _bll, _loaiSachBll);
        presenter.KhoiTao();
        presenter.Them();
        _view.TenKM = "Mua 5 cuốn giảm 30.000đ";
        _view.LoaiKM = LoaiKhuyenMai.TheoSoLuong;
        presenter.DoiLoaiKhuyenMai();
        _view.NguongSoLuong = 5;
        _view.SoTienGiam = 30_000;

        presenter.Luu();

        KhuyenMai daLuu = _bll.KhuyenMaiDaThem!;
        Assert.AreEqual(5, daLuu.NguongSoLuong);
        Assert.AreEqual(30_000, daLuu.SoTienGiam);
        Assert.IsNull(daLuu.PhanTramGiam, "Loại 3 không có phần trăm");
        Assert.IsNull(daLuu.NguongTien, "Loại 3 không có ngưỡng tiền");
        Assert.IsTrue(_bll.DaXoaCache);
        Assert.AreEqual("Đã lưu khuyến mãi \"Mua 5 cuốn giảm 30.000đ\".", _view.ThongBaoCuoi);
        Assert.IsFalse(_view.DangNhap);
    }

    [TestMethod]
    public void Xoa_DaDungTrongHoaDon_DeNghiTatThayViXoa()
    {
        _bll.DaApDung = true;
        var presenter = new KhuyenMaiPresenter(_view, _bll, _loaiSachBll);
        presenter.KhoiTao();
        _view.TraLoiXacNhan = true;

        presenter.Xoa();

        Assert.AreEqual("Khuyến mãi đã dùng trong hóa đơn, chuyển sang TẮT?", _view.CauHoiCuoi);
        Assert.AreEqual((1, false), _bll.DoiTrangThaiThanh);
        Assert.IsNull(_bll.MaDaXoa, "KM-04: không được xóa");
    }

    [TestMethod]
    public void Xoa_ChuaDung_XacNhanRoiXoa()
    {
        var presenter = new KhuyenMaiPresenter(_view, _bll, _loaiSachBll);
        presenter.KhoiTao();
        _view.TraLoiXacNhan = true;

        presenter.Xoa();

        Assert.AreEqual("Xóa khuyến mãi \"Giảm 10% đơn từ 200.000đ\"?", _view.CauHoiCuoi);
        Assert.AreEqual(1, _bll.MaDaXoa);
    }

    [TestMethod]
    public void TinhTrang_TheoNgayVaTrangThai()
    {
        var km = new KhuyenMai { NgayBatDau = new DateTime(2026, 10, 1), NgayKetThuc = new DateTime(2026, 10, 31), TrangThai = true };

        Assert.AreEqual(TinhTrangKhuyenMai.ChuaBatDau, KhuyenMaiPresenter.TinhTrang(km, new DateTime(2026, 9, 30)));
        Assert.AreEqual(TinhTrangKhuyenMai.DangHieuLuc, KhuyenMaiPresenter.TinhTrang(km, new DateTime(2026, 10, 31)));
        Assert.AreEqual(TinhTrangKhuyenMai.HetHan, KhuyenMaiPresenter.TinhTrang(km, new DateTime(2026, 11, 1)));

        km.TrangThai = false;
        Assert.AreEqual(TinhTrangKhuyenMai.DangTat, KhuyenMaiPresenter.TinhTrang(km, new DateTime(2026, 10, 15)));
    }

    // ===================== View giả và BLL giả =====================

    private sealed class ViewGia : IKhuyenMaiView
    {
        private List<LoaiSach> _dsLoaiSach = new();

        public int SoLoaiKM { get; private set; }
        public bool DangNhap { get; private set; }
        public (bool, bool, bool, bool) ONhapBat { get; private set; }
        public string XemTruoc { get; private set; } = string.Empty;
        public string? ThongBaoCuoi { get; private set; }
        public string? CauHoiCuoi { get; private set; }
        public bool TraLoiXacNhan { get; set; }

        public int? MaKMDangChon { get; set; }
        public string TenKM { get; set; } = string.Empty;
        public LoaiKhuyenMai LoaiKM { get; set; }
        public decimal NguongTien { get; set; }
        public int PhanTramGiam { get; set; }
        public int? MaLoaiSach { get; set; }
        public string? TenLoaiSach => _dsLoaiSach.FirstOrDefault(l => l.MaLoai == MaLoaiSach)?.TenLoai;
        public int NguongSoLuong { get; set; }
        public decimal SoTienGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool DangBat { get; set; }

        public void HienDanhMuc(List<KeyValuePair<LoaiKhuyenMai, string>> dsLoaiKM, List<LoaiSach> dsLoaiSach)
        {
            SoLoaiKM = dsLoaiKM.Count;
            _dsLoaiSach = dsLoaiSach;
        }

        public void HienDanhSach(List<KhuyenMai> danhSach, int? maKMChon) =>
            MaKMDangChon = danhSach.Any(k => k.MaKM == maKMChon) ? maKMChon : danhSach.FirstOrDefault()?.MaKM;

        public void HienXemTruoc(string moTa) => XemTruoc = moTa;

        public void DatCheDo(bool dangNhap, bool coDongDangChon) => DangNhap = dangNhap;

        public void BatONhapTheoLoai(bool nguongTien, bool loaiSach, bool phanTram, bool soLuong) =>
            ONhapBat = (nguongTien, loaiSach, phanTram, soLuong);

        public void XoaLoi()
        {
        }

        public void HienThongBao(string thongDiep) => ThongBaoCuoi = thongDiep;

        public void HienLoi(string? truong, string thongDiep)
        {
        }

        public bool XacNhan(string thongDiep)
        {
            CauHoiCuoi = thongDiep;
            return TraLoiXacNhan;
        }
    }

    private sealed class BllGia : KhuyenMaiBLL
    {
        public List<KhuyenMai> DanhSach { get; } = new();
        public bool DaApDung { get; set; }
        public KhuyenMai? KhuyenMaiDaThem { get; private set; }
        public int? MaDaXoa { get; private set; }
        public (int, bool)? DoiTrangThaiThanh { get; private set; }
        public bool DaXoaCache { get; private set; }

        public override List<KhuyenMai> LayDanhSach() => DanhSach.ToList();

        public override int Them(KhuyenMai km)
        {
            km.MaKM = DanhSach.Count + 1;
            DanhSach.Add(km);
            KhuyenMaiDaThem = km;
            return km.MaKM;
        }

        public override void Sua(KhuyenMai km)
        {
        }

        public override bool DaDuocApDung(int maKM) => DaApDung;

        public override void DoiTrangThai(int maKM, bool dangBat) => DoiTrangThaiThanh = (maKM, dangBat);

        public override void Xoa(int maKM) => MaDaXoa = maKM;

        public override void XoaCache() => DaXoaCache = true;
    }

    private sealed class LoaiSachBllGia : LoaiSachBLL
    {
        public override List<LoaiSach> LayDanhSach() => new()
        {
            new LoaiSach { MaLoai = 1, TenLoai = "Văn học" },
            new LoaiSach { MaLoai = 2, TenLoai = "Kỹ năng" }
        };
    }
}
