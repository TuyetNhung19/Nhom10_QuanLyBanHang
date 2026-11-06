// File: QuanLyBanSach.BLL/KhuyenMai/QuyTacKhuyenMaiFactory.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL.KhuyenMai;

/// <summary>
/// Factory tạo đúng lớp quy tắc theo LoaiKM – nơi DUY NHẤT phải sửa khi thêm loại khuyến mãi mới.
/// </summary>
/// <remarks>
/// SO SÁNH VỚI CÁCH DÙNG DELEGATE (để thuyết trình):
/// Có thể thay toàn bộ các lớp KMTheo... bằng một bảng tra
/// <c>Dictionary&lt;LoaiKhuyenMai, Func&lt;DTO.KhuyenMai, IReadOnlyList&lt;ChiTietHoaDon&gt;, decimal&gt;&gt;</c>,
/// ví dụ: <c>[LoaiKhuyenMai.TheoTongDon] = (km, ct) =&gt; ct.Sum(x =&gt; x.ThanhTien) &gt;= km.NguongTien ? ... : 0</c>.
/// + Delegate: gọn, ít file, hợp khi mỗi loại chỉ có MỘT công thức.
/// − Delegate: khó gắn thêm hành vi thứ hai (câu nhắc mua thêm, giảm tiềm năng), phần dùng chung
///   (làm tròn, chặn KM-03) phải lặp ở từng lambda.
/// + Đa hình (cách đang dùng): mỗi loại là một lớp tự chứa mọi hành vi, phần chung nằm ở lớp cơ sở,
///   dễ kiểm thử từng lớp riêng, đúng nguyên lý đóng/mở (thêm loại = thêm lớp + 1 dòng ở đây).
/// </remarks>
public static class QuyTacKhuyenMaiFactory
{
    /// <summary>Tạo quy tắc tương ứng với loại khuyến mãi.</summary>
    /// <exception cref="ArgumentOutOfRangeException">LoaiKM không hợp lệ.</exception>
    public static IQuyTacKhuyenMai Tao(DTO.KhuyenMai km) => km.LoaiKM switch
    {
        LoaiKhuyenMai.TheoTongDon => new KMTheoTongDon(km),
        LoaiKhuyenMai.TheoLoaiSach => new KMTheoLoaiSach(km),
        LoaiKhuyenMai.TheoSoLuong => new KMTheoSoLuong(km),
        _ => throw new ArgumentOutOfRangeException(nameof(km), $"Loại khuyến mãi không hợp lệ: {km.LoaiKM}")
    };
}
