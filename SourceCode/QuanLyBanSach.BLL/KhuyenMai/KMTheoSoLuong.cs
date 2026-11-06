// File: QuanLyBanSach.BLL/KhuyenMai/KMTheoSoLuong.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL.KhuyenMai;

/// <summary>
/// Loại 3 – mua từ N cuốn giảm số tiền cố định. Ví dụ "Mua từ 5 cuốn giảm 30.000đ".
/// </summary>
public class KMTheoSoLuong : QuyTacKhuyenMaiCoSo
{
    /// <summary>Khởi tạo quy tắc.</summary>
    public KMTheoSoLuong(DTO.KhuyenMai thongTin) : base(thongTin)
    {
    }

    private int Nguong => ThongTin.NguongSoLuong ?? int.MaxValue;

    private decimal SoTienGiam => ThongTin.SoTienGiam ?? 0;

    /// <summary>Σ SoLuong &gt;= NguongSoLuong → SoTienGiam (lớp cơ sở chặn không vượt tạm tính – KM-03).</summary>
    protected override decimal TinhGiamTheoQuyTac(IReadOnlyList<ChiTietHoaDon> chiTiet, decimal tamTinh) =>
        chiTiet.Sum(ct => ct.SoLuong) >= Nguong ? SoTienGiam : 0;

    /// <summary>Nhắc khi còn thiếu đúng 1 cuốn: "Mua thêm 1 cuốn để được giảm 30.000đ".</summary>
    public override string? GoiYMuaThem(IReadOnlyList<ChiTietHoaDon> chiTiet) =>
        Nguong - chiTiet.Sum(ct => ct.SoLuong) == 1
            ? $"Mua thêm 1 cuốn để được giảm {DinhDangTien(SoTienGiam)}"
            : null;

    /// <summary>Mức giảm khi đạt ngưỡng = SoTienGiam.</summary>
    public override decimal GiamTiemNang(IReadOnlyList<ChiTietHoaDon> chiTiet) => SoTienGiam;

    /// <summary>(Ngưỡng − số cuốn) / ngưỡng.</summary>
    public override decimal TiLeConThieu(IReadOnlyList<ChiTietHoaDon> chiTiet) =>
        Nguong is <= 0 or int.MaxValue ? 1 : Math.Max(0, Nguong - chiTiet.Sum(ct => ct.SoLuong)) / (decimal)Nguong;
}
