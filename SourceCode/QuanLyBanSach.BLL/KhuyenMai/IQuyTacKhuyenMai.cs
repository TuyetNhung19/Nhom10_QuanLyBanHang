// File: QuanLyBanSach.BLL/KhuyenMai/IQuyTacKhuyenMai.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL.KhuyenMai;

/// <summary>
/// Quy tắc tính một chương trình khuyến mãi (ĐT3 – đa hình).
/// Mỗi loại khuyến mãi là một lớp cài đặt interface này; KhuyenMaiBLL chỉ làm việc
/// với interface nên thêm loại mới không phải sửa code chọn khuyến mãi.
/// </summary>
public interface IQuyTacKhuyenMai
{
    /// <summary>Thông tin chương trình khuyến mãi (DTO).</summary>
    DTO.KhuyenMai ThongTin { get; }

    /// <summary>
    /// Tính số tiền giảm cho danh sách dòng hàng; 0 nếu chưa đạt điều kiện.
    /// Kết quả không bao giờ vượt tạm tính (KM-03).
    /// </summary>
    decimal TinhGiam(IReadOnlyList<ChiTietHoaDon> chiTiet);

    /// <summary>
    /// Câu nhắc "mua thêm để được giảm" khi đơn gần đạt điều kiện; null nếu không cần nhắc.
    /// </summary>
    string? GoiYMuaThem(IReadOnlyList<ChiTietHoaDon> chiTiet);
}
