// File: QuanLyBanSach.BLL/HeThongBLL.cs
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using QuanLyBanSach.DAL;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Chức năng hệ thống dùng chung: kiểm tra kết nối, mô tả lỗi cho GUI và
/// helper gọi DAL đổi lỗi SQL thành lỗi nghiệp vụ. Tồn tại để GUI không phải gọi thẳng DAL.
/// </summary>
public static class HeThongBLL
{
    /// <summary>
    /// Kiểm tra kết nối CSDL (dùng lúc khởi động chương trình).
    /// </summary>
    /// <param name="loi">Thông báo lỗi tiếng Việt khi thất bại.</param>
    /// <returns>true nếu kết nối được.</returns>
    public static bool KiemTraKetNoi(out string loi) => Database.KiemTraKetNoi(out loi);

    /// <summary>
    /// Chuyển một exception bất kỳ thành thông báo tiếng Việt để GUI hiển thị.
    /// </summary>
    /// <param name="ex">Lỗi cần mô tả.</param>
    public static string MoTaLoi(Exception ex)
    {
        Exception goc = ex is AggregateException agg && agg.InnerException != null ? agg.InnerException : ex;

        return goc switch
        {
            BusinessException bex => bex.Message,
            SqlException sqlEx => Database.MoTaLoiSql(sqlEx),
            InvalidOperationException ioe when ioe.Message.Contains("App.config") => ioe.Message,
            _ => "Đã xảy ra lỗi không mong muốn: " + goc.Message
        };
    }

    /// <summary>
    /// true nếu lỗi do không kết nối được CSDL (để GUI hiển thị "Không kết nối được CSDL").
    /// </summary>
    public static bool LaLoiKetNoi(Exception ex) =>
        ex is SqlException sqlEx && sqlEx.Number is -2 or 2 or 53 or -1 or 26 or 40 or 4060 or 18456;

    /// <summary>
    /// Helper chung khi BLL gọi DAL: đổi SqlException nghiệp vụ thành BusinessException.
    /// <list type="bullet">
    /// <item>50000–50099: lỗi THROW từ stored procedure → giữ nguyên thông báo tiếng Việt.</item>
    /// <item>2627/2601: trùng khóa chính/UNIQUE → "Dữ liệu bị trùng".</item>
    /// <item>547: vi phạm khóa ngoại hoặc CHECK → thông báo dễ hiểu.</item>
    /// </list>
    /// Các SqlException khác (mất kết nối, timeout...) ném lên nguyên trạng để GUI báo lỗi kết nối.
    /// </summary>
    /// <typeparam name="T">Kiểu kết quả của hàm DAL.</typeparam>
    /// <param name="hamDAL">Hàm gọi DAL.</param>
    internal static T GoiDAL<T>(Func<T> hamDAL)
    {
        try
        {
            return hamDAL();
        }
        catch (SqlException ex)
        {
            BusinessException? loiNghiepVu = ChuyenThanhLoiNghiepVu(ex);
            if (loiNghiepVu != null)
            {
                throw loiNghiepVu;
            }

            throw;
        }
    }

    /// <summary>Phiên bản không trả kết quả của <see cref="GoiDAL{T}(Func{T})"/>.</summary>
    /// <param name="hamDAL">Hàm gọi DAL.</param>
    internal static void GoiDAL(Action hamDAL)
    {
        GoiDAL(() =>
        {
            hamDAL();
            return true;
        });
    }

    /// <summary>Trả về BusinessException tương ứng, hoặc null nếu là lỗi hệ thống.</summary>
    private static BusinessException? ChuyenThanhLoiNghiepVu(SqlException ex)
    {
        if (ex.Number is >= 50000 and <= 50099)
        {
            return new BusinessException(ex.Message, ex);
        }

        if (ex.Number is 2627 or 2601)
        {
            return new BusinessException("Dữ liệu bị trùng với một bản ghi đã có.", ex);
        }

        if (ex.Number == 547)
        {
            string tenRangBuoc = Regex.Match(ex.Message, "constraint \"(\\w+)\"").Groups[1].Value;
            bool laCheck = ex.Message.Contains("CHECK", StringComparison.OrdinalIgnoreCase);
            string thongBao = laCheck
                ? "Dữ liệu không hợp lệ, vi phạm ràng buộc của CSDL"
                  + (tenRangBuoc.Length > 0 ? $" ({tenRangBuoc})." : ".")
                : "Không thể thực hiện vì dữ liệu này đang được sử dụng ở nơi khác (ví dụ đã có trong hóa đơn).";
            return new BusinessException(thongBao, ex);
        }

        return null;
    }
}
