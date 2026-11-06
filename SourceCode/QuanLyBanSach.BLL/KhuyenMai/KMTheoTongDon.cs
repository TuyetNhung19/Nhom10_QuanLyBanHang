// File: QuanLyBanSach.BLL/KhuyenMai/KMTheoTongDon.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL.KhuyenMai;

/// <summary>
/// Loại 1 – giảm % khi tổng đơn đạt ngưỡng. Ví dụ "Đơn từ 200.000đ giảm 10%".
/// </summary>
public class KMTheoTongDon : QuyTacKhuyenMaiCoSo
{
    /// <summary>Chỉ nhắc khi còn thiếu dưới 30% ngưỡng (theo đặc tả ĐT3).</summary>
    private const decimal TiLeNhac = 0.30m;

    /// <summary>Khởi tạo quy tắc.</summary>
    public KMTheoTongDon(DTO.KhuyenMai thongTin) : base(thongTin)
    {
    }

    private decimal Nguong => ThongTin.NguongTien ?? 0;

    /// <summary>TamTinh &gt;= NguongTien → TamTinh × %.</summary>
    protected override decimal TinhGiamTheoQuyTac(IReadOnlyList<ChiTietHoaDon> chiTiet, decimal tamTinh) =>
        tamTinh >= Nguong ? TinhTheoPhanTram(tamTinh) : 0;

    /// <summary>
    /// Nhắc khi 0 &lt; số tiền còn thiếu &lt; 30% ngưỡng, ví dụ "Mua thêm 49.000đ để được giảm 10%".
    /// </summary>
    public override string? GoiYMuaThem(IReadOnlyList<ChiTietHoaDon> chiTiet)
    {
        decimal conThieu = Nguong - TinhTamTinh(chiTiet);
        if (conThieu <= 0 || conThieu >= Nguong * TiLeNhac)
        {
            return null;
        }

        return $"Mua thêm {DinhDangTien(conThieu)} để được giảm {ThongTin.PhanTramGiam ?? 0}%";
    }

    /// <summary>Mức giảm khi vừa chạm ngưỡng = NguongTien × %.</summary>
    public override decimal GiamTiemNang(IReadOnlyList<ChiTietHoaDon> chiTiet) => TinhTheoPhanTram(Nguong);

    /// <summary>(Ngưỡng − tạm tính) / ngưỡng.</summary>
    public override decimal TiLeConThieu(IReadOnlyList<ChiTietHoaDon> chiTiet) =>
        Nguong <= 0 ? 0 : Math.Max(0, Nguong - TinhTamTinh(chiTiet)) / Nguong;
}
