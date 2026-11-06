// File: QuanLyBanSach.Tests/DonTreoPresenterTests.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Presenters;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.Tests;

/// <summary>
/// Kiểm thử DonTreoPresenter bằng View giả và BLL giả (không mở Form, không cần đăng nhập).
/// </summary>
[TestClass]
public class DonTreoPresenterTests
{
    private ViewGia _view = null!;
    private BllGia _bll = null!;
    private HoaDonTam _donCuaAn = null!;

    [TestInitialize]
    public void KhoiTao()
    {
        _view = new ViewGia();
        _bll = new BllGia();
        _donCuaAn = new HoaDonTam
        {
            ThoiGianTreo = new DateTime(2026, 10, 29, 9, 15, 0),
            Khach = new KhachHang { MaKH = 1, HoTen = "Nguyễn Văn An" },
            GhiChu = "Khách quay lại lấy",
            ChiTiet = new List<ChiTietHoaDon>
            {
                new() { MaSach = "S01", TenSach = "Dế Mèn", SoLuong = 2, DonGia = 50_000 }
            }
        };
        _bll.DanhSach.Add(_donCuaAn);
        _bll.DanhSach.Add(new HoaDonTam());
    }

    [TestMethod]
    public void TaiDanhSach_HienSoDon_VaChiTietDonDauTien()
    {
        var presenter = new DonTreoPresenter(_view, _bll);

        presenter.TaiDanhSach();

        Assert.AreEqual($"2/{DonTreoBLL.SoDonToiDa} đơn treo", _view.SoDon);
        Assert.AreEqual(1, _view.ChiTiet!.Count);
        Assert.AreEqual("Chi tiết đơn – Nguyễn Văn An (ghi chú: Khách quay lại lấy)", _view.TieuDe);
        Assert.IsTrue(_view.NutBat);
    }

    [TestMethod]
    public void TaiDanhSach_KhongCoDon_TatNutGoiLaiVaHuy()
    {
        _bll.DanhSach.Clear();
        var presenter = new DonTreoPresenter(_view, _bll);

        presenter.TaiDanhSach();

        Assert.AreEqual("Chi tiết đơn", _view.TieuDe);
        Assert.IsNull(_view.ChiTiet);
        Assert.IsFalse(_view.NutBat);
    }

    [TestMethod]
    public void GoiLai_TraVeMaDonDangChon()
    {
        var presenter = new DonTreoPresenter(_view, _bll);
        presenter.TaiDanhSach();

        presenter.GoiLai();

        Assert.AreEqual(_donCuaAn.Id, _view.IdGoiLai);
    }

    [TestMethod]
    public void HuyDon_NguoiDungChonCo_GoiBllHuy_VaTaiLaiDanhSach()
    {
        var presenter = new DonTreoPresenter(_view, _bll);
        presenter.TaiDanhSach();
        _view.TraLoiXacNhan = true;

        presenter.HuyDon();

        Assert.AreEqual("Hủy đơn treo lúc 09:15 của Nguyễn Văn An (2 cuốn)?", _view.CauHoiCuoi);
        Assert.AreEqual(_donCuaAn.Id, _bll.IdDaHuy);
        Assert.AreEqual($"1/{DonTreoBLL.SoDonToiDa} đơn treo", _view.SoDon);
    }

    [TestMethod]
    public void HuyDon_NguoiDungChonKhong_KhongHuy()
    {
        var presenter = new DonTreoPresenter(_view, _bll);
        presenter.TaiDanhSach();
        _view.TraLoiXacNhan = false;

        presenter.HuyDon();

        Assert.IsNull(_bll.IdDaHuy);
    }

    [TestMethod]
    public void LaTreoLau_QuaBaMuoiPhut()
    {
        var don = new HoaDonTam { ThoiGianTreo = new DateTime(2026, 10, 29, 9, 0, 0) };

        Assert.IsFalse(DonTreoPresenter.LaTreoLau(don, new DateTime(2026, 10, 29, 9, 29, 0)));
        Assert.IsTrue(DonTreoPresenter.LaTreoLau(don, new DateTime(2026, 10, 29, 9, 31, 0)));
    }

    // ===================== View giả và BLL giả =====================

    private sealed class ViewGia : IDonTreoView
    {
        public string SoDon { get; private set; } = string.Empty;
        public List<ChiTietHoaDon>? ChiTiet { get; private set; }
        public string TieuDe { get; private set; } = string.Empty;
        public bool NutBat { get; private set; }
        public string? CauHoiCuoi { get; private set; }
        public bool TraLoiXacNhan { get; set; }
        public Guid? IdGoiLai { get; private set; }

        public Guid? IdDangChon { get; set; }

        /// <summary>Giống DataGridView: có dữ liệu thì chọn dòng đầu.</summary>
        public void HienDanhSach(List<HoaDonTam> danhSach, string soDon)
        {
            SoDon = soDon;
            IdDangChon = danhSach.FirstOrDefault()?.Id;
        }

        public void HienChiTiet(List<ChiTietHoaDon>? chiTiet, string tieuDe, bool coDonDangChon)
        {
            ChiTiet = chiTiet;
            TieuDe = tieuDe;
            NutBat = coDonDangChon;
        }

        public bool XacNhan(string thongDiep)
        {
            CauHoiCuoi = thongDiep;
            return TraLoiXacNhan;
        }

        public void HienLoi(string thongDiep)
        {
        }

        public void DongVaGoiLai(Guid id) => IdGoiLai = id;
    }

    private sealed class BllGia : DonTreoBLL
    {
        public List<HoaDonTam> DanhSach { get; } = new();
        public Guid? IdDaHuy { get; private set; }

        public override List<HoaDonTam> LayDanhSach() => DanhSach.ToList();

        public override void Huy(Guid id)
        {
            IdDaHuy = id;
            DanhSach.RemoveAll(d => d.Id == id);
        }
    }
}
