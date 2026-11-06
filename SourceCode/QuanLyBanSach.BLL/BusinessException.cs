// File: QuanLyBanSach.BLL/BusinessException.cs
namespace QuanLyBanSach.BLL;

/// <summary>
/// Lỗi nghiệp vụ do BLL phát hiện (dữ liệu không hợp lệ, vi phạm quy tắc).
/// GUI bắt lỗi này để hiển thị thông báo và gắn ErrorProvider vào đúng control
/// dựa theo <see cref="FieldName"/>.
/// </summary>
public class BusinessException : Exception
{
    /// <summary>
    /// Tên thuộc tính/trường bị lỗi (ví dụ "GiaBan", "SDT"); null nếu lỗi chung.
    /// </summary>
    public string? FieldName { get; }

    /// <summary>Tạo lỗi nghiệp vụ.</summary>
    /// <param name="message">Thông báo tiếng Việt cho người dùng.</param>
    /// <param name="fieldName">Tên trường bị lỗi (tùy chọn).</param>
    public BusinessException(string message, string? fieldName = null)
        : base(message)
    {
        FieldName = fieldName;
    }

    /// <summary>Tạo lỗi nghiệp vụ bọc một lỗi gốc (ví dụ lỗi THROW 5000x từ stored procedure).</summary>
    /// <param name="message">Thông báo tiếng Việt cho người dùng.</param>
    /// <param name="innerException">Lỗi gốc.</param>
    /// <param name="fieldName">Tên trường bị lỗi (tùy chọn).</param>
    public BusinessException(string message, Exception innerException, string? fieldName = null)
        : base(message, innerException)
    {
        FieldName = fieldName;
    }
}
