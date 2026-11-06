// File: QuanLyBanSach.BLL/KhuyenMai/KMTheoLoaiSach.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL.KhuyenMai;

/// <summary>
/// Loại 2 – giảm % trên tổng thành tiền các sách thuộc một loại. Ví dụ "Sách Thiếu nhi giảm 15%".
/// Làm tròn MỘT lần trên tổng tiền của loại (khớp sp_LapHoaDon). Không có câu nhắc.
/// </summary>
public class KMTheoLoaiSach : QuyTacKhuyenMaiCoSo
{
    /// <summary>Khởi tạo quy tắc.</summary>
    public KMTheoLoaiSach(DTO.KhuyenMai thongTin) : base(thongTin)
    {
    }

    /// <summary>% × Σ ThanhTien của các dòng có MaLoai = KhuyenMai.MaLoai.</summary>
    protected override decimal TinhGiamTheoQuyTac(IReadOnlyList<ChiTietHoaDon> chiTiet, decimal tamTinh)
    {
        decimal tienLoai = chiTiet
            .Where(ct => ct.MaLoai == ThongTin.MaLoai)
            .Sum(ct => ct.ThanhTien);
        return TinhTheoPhanTram(tienLoai);
    }
}
