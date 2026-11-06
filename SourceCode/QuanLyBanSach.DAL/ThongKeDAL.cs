// File: QuanLyBanSach.DAL/ThongKeDAL.cs
using System.Data;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.DAL;

/// <summary>
/// Thống kê, bảng điều khiển (ĐT5) và báo cáo cuối ngày (ĐT6). Mọi SP chỉ tính hóa đơn hoàn tất (TK-03).
/// </summary>
public class ThongKeDAL
{
    /// <summary>Doanh thu từng ngày trong khoảng, ngày không bán vẫn có dòng 0 – sp_DoanhThuTheoNgay.</summary>
    /// <param name="tu">Từ ngày.</param>
    /// <param name="den">Đến ngày.</param>
    /// <param name="maTK">Lọc thu ngân; null = tất cả.</param>
    public List<DoanhThuNgay> DoanhThuTheoNgay(DateTime tu, DateTime den, int? maTK)
    {
        return Database.ExecuteReader("sp_DoanhThuTheoNgay", r => new DoanhThuNgay
        {
            Ngay = r.GetDateTimeOrMin("Ngay"),
            SoHoaDon = r.GetIntOrZero("SoHoaDon"),
            TongTamTinh = r.GetDecimalOrZero("TongTamTinh"),
            TongGiamGia = r.GetDecimalOrZero("TongGiamGia"),
            DoanhThu = r.GetDecimalOrZero("DoanhThu")
        }, CommandType.StoredProcedure,
            Database.TaoThamSo("@TuNgay", SqlDbType.Date, tu.Date),
            Database.TaoThamSo("@DenNgay", SqlDbType.Date, den.Date),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK));
    }

    /// <summary>Doanh thu gộp theo tháng – sp_DoanhThuTheoThang.</summary>
    public List<DoanhThuThang> DoanhThuTheoThang(DateTime tu, DateTime den)
    {
        return Database.ExecuteReader("sp_DoanhThuTheoThang", r => new DoanhThuThang
        {
            Nam = r.GetIntOrZero("Nam"),
            Thang = r.GetIntOrZero("Thang"),
            SoHoaDon = r.GetIntOrZero("SoHoaDon"),
            TongGiamGia = r.GetDecimalOrZero("TongGiamGia"),
            DoanhThu = r.GetDecimalOrZero("DoanhThu")
        }, CommandType.StoredProcedure,
            Database.TaoThamSo("@TuNgay", SqlDbType.Date, tu.Date),
            Database.TaoThamSo("@DenNgay", SqlDbType.Date, den.Date));
    }

    /// <summary>Top sách bán chạy – sp_SachBanChay.</summary>
    /// <param name="tu">Từ ngày.</param>
    /// <param name="den">Đến ngày.</param>
    /// <param name="top">Số dòng (top 5 cho bảng điều khiển, top 10 cho thống kê).</param>
    /// <param name="maTK">Lọc thu ngân; null = tất cả.</param>
    public List<SachBanChay> SachBanChay(DateTime tu, DateTime den, int top, int? maTK)
    {
        return Database.ExecuteReader("sp_SachBanChay", r => new SachBanChay
        {
            MaSach = r.GetStringOrEmpty("MaSach"),
            TenSach = r.GetStringOrEmpty("TenSach"),
            SoLuongBan = r.GetIntOrZero("SoLuongBan"),
            DoanhThu = r.GetDecimalOrZero("DoanhThu")
        }, CommandType.StoredProcedure,
            Database.TaoThamSo("@TuNgay", SqlDbType.Date, tu.Date),
            Database.TaoThamSo("@DenNgay", SqlDbType.Date, den.Date),
            Database.TaoThamSo("@Top", SqlDbType.Int, top),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK));
    }

    /// <summary>Doanh thu theo từng thu ngân – sp_DoanhThuTheoThuNgan.</summary>
    public List<DoanhThuThuNgan> DoanhThuTheoThuNgan(DateTime tu, DateTime den)
    {
        return Database.ExecuteReader("sp_DoanhThuTheoThuNgan", r => new DoanhThuThuNgan
        {
            MaTK = r.GetIntOrZero("MaTK"),
            HoTen = r.GetStringOrEmpty("HoTen"),
            SoHoaDon = r.GetIntOrZero("SoHoaDon"),
            DoanhThu = r.GetDecimalOrZero("DoanhThu")
        }, CommandType.StoredProcedure,
            Database.TaoThamSo("@TuNgay", SqlDbType.Date, tu.Date),
            Database.TaoThamSo("@DenNgay", SqlDbType.Date, den.Date));
    }

    /// <summary>Hiệu quả từng khuyến mãi – sp_HieuQuaKhuyenMai.</summary>
    public List<HieuQuaKhuyenMai> HieuQuaKhuyenMai(DateTime tu, DateTime den)
    {
        return Database.ExecuteReader("sp_HieuQuaKhuyenMai", r => new HieuQuaKhuyenMai
        {
            MaKM = r.GetIntOrZero("MaKM"),
            TenKM = r.GetStringOrEmpty("TenKM"),
            SoHoaDonApDung = r.GetIntOrZero("SoHoaDonApDung"),
            TongTienGiam = r.GetDecimalOrZero("TongTienGiam"),
            DoanhThu = r.GetDecimalOrZero("DoanhThu")
        }, CommandType.StoredProcedure,
            Database.TaoThamSo("@TuNgay", SqlDbType.Date, tu.Date),
            Database.TaoThamSo("@DenNgay", SqlDbType.Date, den.Date));
    }

    /// <summary>Số liệu các thẻ bảng điều khiển trong ngày (ĐT5) – sp_TongQuanTrongNgay.</summary>
    /// <param name="ngay">Ngày cần xem.</param>
    /// <param name="maTK">Thu ngân chỉ xem của mình; null = tất cả.</param>
    public TongQuanNgay TongQuanTrongNgay(DateTime ngay, int? maTK)
    {
        return Database.ExecuteReader("sp_TongQuanTrongNgay", r => new TongQuanNgay
        {
            DoanhThu = r.GetDecimalOrZero("DoanhThu"),
            SoHoaDon = r.GetIntOrZero("SoHoaDon"),
            SoCuonBan = r.GetIntOrZero("SoCuonBan"),
            SoSachSapHet = r.GetIntOrZero("SoSachSapHet")
        }, CommandType.StoredProcedure,
            Database.TaoThamSo("@Ngay", SqlDbType.Date, ngay.Date),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK)).FirstOrDefault() ?? new TongQuanNgay();
    }

    /// <summary>
    /// Báo cáo cuối ngày (ĐT6) – sp_BaoCaoCuoiNgay, đọc 3 result set:
    /// (1) số tổng, (2) hóa đơn đã hủy, (3) giảm giá theo khuyến mãi.
    /// </summary>
    /// <param name="ngay">Ngày báo cáo.</param>
    /// <param name="maTK">Thu ngân; null = tất cả (Quản lý).</param>
    public BaoCaoCuoiNgay BaoCaoCuoiNgay(DateTime ngay, int? maTK)
    {
        var bc = new BaoCaoCuoiNgay { Ngay = ngay.Date };

        Database.ExecuteReaderNhieuKetQua("sp_BaoCaoCuoiNgay", reader =>
        {
            // (1) Tổng hợp – luôn có đúng 1 dòng
            if (reader.Read())
            {
                bc.Ngay = reader.GetDateTimeOrNull("Ngay") ?? ngay.Date;
                bc.TenThuNgan = reader.GetStringOrEmpty("TenThuNgan");
                bc.SoHoaDon = reader.GetIntOrZero("SoHoaDon");
                bc.SoHoaDonHuy = reader.GetIntOrZero("SoHoaDonHuy");
                bc.TongTamTinh = reader.GetDecimalOrZero("TongTamTinh");
                bc.TongGiamGia = reader.GetDecimalOrZero("TongGiamGia");
                bc.DoanhThuThuc = reader.GetDecimalOrZero("DoanhThuThuc");
                bc.TienMatPhaiCo = reader.GetDecimalOrZero("TienMatPhaiCo");
            }

            // (2) Hóa đơn đã hủy
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    bc.HoaDonHuy.Add(new HoaDon
                    {
                        MaHD = reader.GetStringOrEmpty("MaHD"),
                        NgayLap = reader.GetDateTimeOrMin("NgayLap"),
                        TongThanhToan = reader.GetDecimalOrZero("TongThanhToan"),
                        LyDoHuy = reader.GetStringOrNull("LyDoHuy"),
                        TrangThai = TrangThaiHoaDon.DaHuy
                    });
                }
            }

            // (3) Giảm giá theo khuyến mãi
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    bc.GiamGiaTheoKhuyenMai.Add(new GiamGiaTheoKM
                    {
                        TenKM = reader.GetStringOrEmpty("TenKM"),
                        SoHoaDon = reader.GetIntOrZero("SoHoaDon"),
                        TongGiam = reader.GetDecimalOrZero("TongGiam")
                    });
                }
            }
        }, CommandType.StoredProcedure,
            Database.TaoThamSo("@Ngay", SqlDbType.Date, ngay.Date),
            Database.TaoThamSo("@MaTK", SqlDbType.Int, maTK));

        return bc;
    }
}
