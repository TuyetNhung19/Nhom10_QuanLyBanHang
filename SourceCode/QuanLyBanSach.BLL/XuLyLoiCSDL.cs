// File: QuanLyBanSach.BLL/XuLyLoiCSDL.cs
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Helper chung khi BLL gọi DAL: đổi SqlException "có nghĩa nghiệp vụ" thành BusinessException.
/// - 50000–50099: lỗi THROW từ stored procedure → giữ nguyên thông báo tiếng Việt.
/// - 2627/2601: trùng khóa → "Dữ liệu bị trùng...".
/// - 547: vi phạm khóa ngoại / CHECK → thông báo dễ hiểu.
/// Các SqlException khác (mất kết nối, timeout...) ném lên nguyên trạng để GUI báo "Không kết nối được CSDL".
/// </summary>
internal static class XuLyLoiCSDL
{
    /// <summary>Gọi hàm DAL có giá trị trả về.</summary>
    public static T Goi<T>(Func<T> hamDAL)
    {
        try
        {
            return hamDAL();
        }
        catch (SqlException ex) when (LaLoiNghiepVu(ex))
        {
            throw ChuyenThanhLoiNghiepVu(ex);
        }
    }

    /// <summary>Gọi hàm DAL không có giá trị trả về.</summary>
    public static void Goi(Action hamDAL)
    {
        try
        {
            hamDAL();
        }
        catch (SqlException ex) when (LaLoiNghiepVu(ex))
        {
            throw ChuyenThanhLoiNghiepVu(ex);
        }
    }

    /// <summary>Lỗi có thể giải thích cho người dùng (không phải lỗi kết nối).</summary>
    private static bool LaLoiNghiepVu(SqlException ex) =>
        ex.Number is >= 50000 and <= 50099 or 2627 or 2601 or 547;

    /// <summary>Tạo BusinessException tương ứng với mã lỗi SQL.</summary>
    private static BusinessException ChuyenThanhLoiNghiepVu(SqlException ex)
    {
        if (ex.Number is >= 50000 and <= 50099)
        {
            return new BusinessException(ex.Message, ex);
        }

        if (ex.Number is 2627 or 2601)
        {
            Match giaTri = Regex.Match(ex.Message, @"duplicate key value is \((.*?)\)");
            string chiTiet = giaTri.Success ? $" (giá trị: {giaTri.Groups[1].Value})" : string.Empty;
            return new BusinessException("Dữ liệu bị trùng với một bản ghi đã có" + chiTiet + ".", ex);
        }

        // 547: phân biệt khóa ngoại và CHECK để thông báo đúng ý
        Match rangBuoc = Regex.Match(ex.Message, "constraint \"(\\w+)\"");
        string tenRangBuoc = rangBuoc.Success ? rangBuoc.Groups[1].Value : string.Empty;

        if (ex.Message.Contains("REFERENCE", StringComparison.OrdinalIgnoreCase))
        {
            return new BusinessException(
                "Không thể thực hiện vì dữ liệu này đang được sử dụng ở nơi khác " +
                "(ví dụ đã có trong hóa đơn) hoặc tham chiếu tới dữ liệu không tồn tại.", ex);
        }

        return new BusinessException(
            "Dữ liệu không hợp lệ, vi phạm ràng buộc kiểm tra của CSDL" +
            (tenRangBuoc.Length > 0 ? $" ({tenRangBuoc})" : string.Empty) + ".", ex);
    }
}
