// File: QuanLyBanSach.Tests/KhuyenMaiTests.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.BLL.KhuyenMai;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.Tests;

/// <summary>
/// Kiểm thử chọn khuyến mãi (ĐT3) theo TC07, TC17, TC18, TC19 và KM-01, KM-03.
/// Dùng overload KhuyenMaiBLL.ChonKhuyenMaiTotNhat nhận sẵn danh sách khuyến mãi nên không cần CSDL.
/// Dữ liệu giống 02_NapDuLieuMau.sql.
/// </summary>
[TestClass]
public class KhuyenMaiTests
{
    private const int MaLoaiVanHoc = 1;
    private const int MaLoaiKyNang = 2;
    private const int MaLoaiThieuNhi = 3;

    private static readonly DateTime HomNay = new(2026, 10, 28);

    private readonly KhuyenMaiBLL _bll = new();

    /// <summary>KM1–KM5 như dữ liệu mẫu (KM4 hết hạn, KM5 đang tắt).</summary>
    private static List<KhuyenMai> DanhSachMau() => new()
    {
        new KhuyenMai
        {
            MaKM = 1, TenKM = "Giảm 10% đơn từ 200.000đ", LoaiKM = LoaiKhuyenMai.TheoTongDon,
            NguongTien = 200_000, PhanTramGiam = 10,
            NgayBatDau = HomNay.AddDays(-30), NgayKetThuc = HomNay.AddDays(180), TrangThai = true
        },
        new KhuyenMai
        {
            MaKM = 2, TenKM = "Sách Thiếu nhi giảm 15%", LoaiKM = LoaiKhuyenMai.TheoLoaiSach,
            MaLoai = MaLoaiThieuNhi, TenLoaiSach = "Thiếu nhi", PhanTramGiam = 15,
            NgayBatDau = HomNay.AddDays(-30), NgayKetThuc = HomNay.AddDays(180), TrangThai = true
        },
        new KhuyenMai
        {
            MaKM = 3, TenKM = "Mua từ 5 cuốn giảm 30.000đ", LoaiKM = LoaiKhuyenMai.TheoSoLuong,
            NguongSoLuong = 5, SoTienGiam = 30_000,
            NgayBatDau = HomNay.AddDays(-30), NgayKetThuc = HomNay.AddDays(180), TrangThai = true
        },
        new KhuyenMai
        {
            MaKM = 4, TenKM = "Giảm 5% đơn từ 100.000đ", LoaiKM = LoaiKhuyenMai.TheoTongDon,
            NguongTien = 100_000, PhanTramGiam = 5,
            NgayBatDau = HomNay.AddDays(-60), NgayKetThuc = HomNay.AddDays(-11), TrangThai = true
        },
        new KhuyenMai
        {
            MaKM = 5, TenKM = "Giảm 20% đơn từ 300.000đ", LoaiKM = LoaiKhuyenMai.TheoTongDon,
            NguongTien = 300_000, PhanTramGiam = 20,
            NgayBatDau = HomNay.AddDays(-30), NgayKetThuc = HomNay.AddDays(180), TrangThai = false
        }
    };

    private static ChiTietHoaDon Dong(string maSach, decimal donGia, int soLuong, int maLoai) =>
        new() { MaSach = maSach, TenSach = maSach, DonGia = donGia, SoLuong = soLuong, MaLoai = maLoai };

    /// <summary>TC07: Nhà giả kim ×1 + Đắc nhân tâm ×2 = 251.000 → KM1 giảm 25.100.</summary>
    [TestMethod]
    public void TC07_Don251000_GiamMuoiPhanTram()
    {
        var chiTiet = new List<ChiTietHoaDon>
        {
            Dong("S0001", 79_000, 1, MaLoaiVanHoc),
            Dong("S0002", 86_000, 2, MaLoaiKyNang)
        };

        KetQuaKhuyenMai kq = _bll.ChonKhuyenMaiTotNhat(chiTiet, HomNay, DanhSachMau());

        Assert.AreEqual(25_100m, kq.TienGiam);
        Assert.AreEqual(1, kq.KhuyenMaiApDung?.MaKM);
    }

    /// <summary>TC17: đơn 151.000 → không giảm, nhắc "Mua thêm 49.000đ để được giảm 10%".</summary>
    [TestMethod]
    public void TC17_Don151000_KhongGiam_CoLoiNhac()
    {
        var chiTiet = new List<ChiTietHoaDon>
        {
            Dong("S0001", 79_000, 1, MaLoaiVanHoc),
            Dong("S0008", 72_000, 1, MaLoaiVanHoc)
        };

        KetQuaKhuyenMai kq = _bll.ChonKhuyenMaiTotNhat(chiTiet, HomNay, DanhSachMau());

        Assert.AreEqual(0m, kq.TienGiam);
        Assert.IsNull(kq.KhuyenMaiApDung);
        Assert.AreEqual("Mua thêm 49.000đ để được giảm 10%", kq.LoiNhac);
    }

    /// <summary>TC18: 2 sách Thiếu nhi 218.000 thỏa KM1 (21.800) và KM2 (32.700) → chọn KM2.</summary>
    [TestMethod]
    public void TC18_ThoaHaiKhuyenMai_LayMucCaoHon()
    {
        var chiTiet = new List<ChiTietHoaDon>
        {
            Dong("S0020", 120_000, 1, MaLoaiThieuNhi),
            Dong("S0018", 98_000, 1, MaLoaiThieuNhi)
        };

        KetQuaKhuyenMai kq = _bll.ChonKhuyenMaiTotNhat(chiTiet, HomNay, DanhSachMau());

        Assert.AreEqual(2, kq.KhuyenMaiApDung?.MaKM);
        Assert.AreEqual(32_700m, kq.TienGiam);
    }

    /// <summary>TC18 biến thể: Số đỏ ×5 = 275.000 → KM1 27.500 &lt; KM3 30.000 → chọn KM3.</summary>
    [TestMethod]
    public void TC18_NamCuon_ChonGiamTheoSoLuong()
    {
        var chiTiet = new List<ChiTietHoaDon> { Dong("S0009", 55_000, 5, MaLoaiVanHoc) };

        KetQuaKhuyenMai kq = _bll.ChonKhuyenMaiTotNhat(chiTiet, HomNay, DanhSachMau());

        Assert.AreEqual(3, kq.KhuyenMaiApDung?.MaKM);
        Assert.AreEqual(30_000m, kq.TienGiam);
    }

    /// <summary>TC19: KM4 hết hạn và KM5 đang tắt không được áp dụng; đơn 350.000 chỉ nhận KM1.</summary>
    [TestMethod]
    public void TC19_KhuyenMaiHetHanHoacTat_KhongApDung()
    {
        var donNho = new List<ChiTietHoaDon> { Dong("S0006", 110_000, 1, MaLoaiVanHoc) };
        var donLon = new List<ChiTietHoaDon> { Dong("S0032", 350_000, 1, 7) };

        KetQuaKhuyenMai kqNho = _bll.ChonKhuyenMaiTotNhat(donNho, HomNay, DanhSachMau());
        KetQuaKhuyenMai kqLon = _bll.ChonKhuyenMaiTotNhat(donLon, HomNay, DanhSachMau());

        Assert.AreEqual(0m, kqNho.TienGiam, "KM4 đã hết hạn nên đơn 110.000 không được giảm");
        Assert.AreEqual(1, kqLon.KhuyenMaiApDung?.MaKM, "KM5 đang tắt nên không được chọn");
        Assert.AreEqual(35_000m, kqLon.TienGiam);
    }

    /// <summary>KM-03: giảm cố định 30.000 cho đơn 25.000 (5 cuốn × 5.000) chỉ được giảm 25.000.</summary>
    [TestMethod]
    public void KM03_TienGiamKhongVuotTamTinh()
    {
        var chiTiet = new List<ChiTietHoaDon> { Dong("SX", 5_000, 5, MaLoaiVanHoc) };
        IQuyTacKhuyenMai quyTac = QuyTacKhuyenMaiFactory.Tao(DanhSachMau()[2]);

        Assert.AreEqual(25_000m, quyTac.TinhGiam(chiTiet));
    }

    /// <summary>KM-01: hai khuyến mãi giảm bằng nhau → chọn MaKM nhỏ hơn, không cộng dồn.</summary>
    [TestMethod]
    public void KM01_BangNhau_ChonMaNhoHon_KhongCongDon()
    {
        var ds = new List<KhuyenMai>
        {
            new() { MaKM = 9, LoaiKM = LoaiKhuyenMai.TheoTongDon, NguongTien = 100_000, PhanTramGiam = 10,
                    NgayBatDau = HomNay, NgayKetThuc = HomNay, TrangThai = true },
            new() { MaKM = 7, LoaiKM = LoaiKhuyenMai.TheoSoLuong, NguongSoLuong = 1, SoTienGiam = 20_000,
                    NgayBatDau = HomNay, NgayKetThuc = HomNay, TrangThai = true }
        };
        var chiTiet = new List<ChiTietHoaDon> { Dong("S0001", 200_000, 1, MaLoaiVanHoc) };

        KetQuaKhuyenMai kq = _bll.ChonKhuyenMaiTotNhat(chiTiet, HomNay, ds);

        Assert.AreEqual(7, kq.KhuyenMaiApDung?.MaKM);
        Assert.AreEqual(20_000m, kq.TienGiam);
    }

    /// <summary>Còn thiếu đúng 1 cuốn → "Mua thêm 1 cuốn để được giảm 30.000đ".</summary>
    [TestMethod]
    public void KMTheoSoLuong_ThieuMotCuon_CoLoiNhac()
    {
        var chiTiet = new List<ChiTietHoaDon> { Dong("S0004", 45_000, 4, MaLoaiThieuNhi) };
        IQuyTacKhuyenMai quyTac = QuyTacKhuyenMaiFactory.Tao(DanhSachMau()[2]);

        Assert.AreEqual("Mua thêm 1 cuốn để được giảm 30.000đ", quyTac.GoiYMuaThem(chiTiet));
        Assert.AreEqual(0m, quyTac.TinhGiam(chiTiet));
    }

    /// <summary>Đơn rỗng → không khuyến mãi, không lời nhắc.</summary>
    [TestMethod]
    public void DonRong_KhongCoKhuyenMai()
    {
        KetQuaKhuyenMai kq = _bll.ChonKhuyenMaiTotNhat(new List<ChiTietHoaDon>(), HomNay, DanhSachMau());

        Assert.AreEqual(0m, kq.TienGiam);
        Assert.IsNull(kq.KhuyenMaiApDung);
        Assert.IsNull(kq.LoiNhac);
    }

    /// <summary>TK-02: băm SHA-256 hex IN HOA, 64 ký tự, khớp HASHBYTES của SQL Server.</summary>
    [TestMethod]
    public void MatKhauHelper_BamDungChuan()
    {
        string bam = MatKhauHelper.Bam("admin123");

        Assert.AreEqual(64, bam.Length);
        Assert.AreEqual(bam.ToUpperInvariant(), bam);
        Assert.AreEqual("240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9", bam);
    }
}
