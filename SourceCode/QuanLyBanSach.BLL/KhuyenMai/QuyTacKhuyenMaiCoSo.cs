// File: QuanLyBanSach.BLL/KhuyenMai/QuyTacKhuyenMaiCoSo.cs
using System.Globalization;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL.KhuyenMai;

/// <summary>
/// Lớp cơ sở trừu tượng cho các quy tắc khuyến mãi (mẫu Template Method):
/// TinhGiam() lo phần chung – tính tạm tính, giới hạn KM-03 – còn lớp con chỉ cài
/// TinhGiamTheoQuyTac() riêng của mình.
/// </summary>
public abstract class QuyTacKhuyenMaiCoSo : IQuyTacKhuyenMai
{
    /// <summary>Văn hóa vi-VN để định dạng tiền "49.000đ".</summary>
    protected static readonly CultureInfo VanHoaVN = new("vi-VN");

    /// <summary>Phần trăm giảm tối đa cho phép (KM-03).</summary>
    public const int PhanTramToiDa = 50;

    /// <summary>Khởi tạo quy tắc từ thông tin khuyến mãi.</summary>
    protected QuyTacKhuyenMaiCoSo(DTO.KhuyenMai thongTin)
    {
        ThongTin = thongTin ?? throw new ArgumentNullException(nameof(thongTin));
    }

    /// <inheritdoc/>
    public DTO.KhuyenMai ThongTin { get; }

    /// <inheritdoc/>
    public decimal TinhGiam(IReadOnlyList<ChiTietHoaDon> chiTiet)
    {
        decimal tamTinh = TinhTamTinh(chiTiet);
        if (tamTinh <= 0)
        {
            return 0;
        }

        decimal giam = TinhGiamTheoQuyTac(chiTiet, tamTinh);

        // KM-03: tiền giảm không âm và không vượt tạm tính
        return Math.Clamp(giam, 0, tamTinh);
    }

    /// <inheritdoc/>
    public virtual string? GoiYMuaThem(IReadOnlyList<ChiTietHoaDon> chiTiet) => null;

    /// <summary>
    /// Mức giảm sẽ nhận được nếu khách mua thêm để đạt ngưỡng (dùng chọn câu nhắc có lợi nhất).
    /// Mặc định 0 = quy tắc không có câu nhắc.
    /// </summary>
    public virtual decimal GiamTiemNang(IReadOnlyList<ChiTietHoaDon> chiTiet) => 0;

    /// <summary>
    /// Tỉ lệ còn thiếu so với ngưỡng (0 = đã đạt, 1 = còn rất xa) – để ưu tiên ngưỡng gần nhất.
    /// </summary>
    public virtual decimal TiLeConThieu(IReadOnlyList<ChiTietHoaDon> chiTiet) => 1;

    /// <summary>Phần tính giảm riêng của từng loại khuyến mãi (chưa giới hạn KM-03).</summary>
    /// <param name="chiTiet">Các dòng hàng.</param>
    /// <param name="tamTinh">Tạm tính của đơn (&gt; 0).</param>
    protected abstract decimal TinhGiamTheoQuyTac(IReadOnlyList<ChiTietHoaDon> chiTiet, decimal tamTinh);

    /// <summary>Tạm tính = Σ ThanhTien.</summary>
    protected static decimal TinhTamTinh(IReadOnlyList<ChiTietHoaDon> chiTiet) => chiTiet.Sum(ct => ct.ThanhTien);

    /// <summary>
    /// Tính coSo × PhanTramGiam %, làm tròn đến đồng theo MidpointRounding.AwayFromZero
    /// (khớp ROUND của sp_LapHoaDon). Phần trăm bị chặn trong [0, 50] (KM-03).
    /// </summary>
    protected decimal TinhTheoPhanTram(decimal coSo)
    {
        int phanTram = Math.Clamp(ThongTin.PhanTramGiam ?? 0, 0, PhanTramToiDa);
        return Math.Round(coSo * phanTram / 100m, 0, MidpointRounding.AwayFromZero);
    }

    /// <summary>Định dạng tiền kiểu "49.000đ".</summary>
    protected static string DinhDangTien(decimal soTien) => soTien.ToString("#,##0", VanHoaVN) + "đ";
}
