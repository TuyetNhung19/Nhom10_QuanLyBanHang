// File: QuanLyBanSach.BLL/KiemTraDuLieu.cs
namespace QuanLyBanSach.BLL;

/// <summary>
/// Các hàm kiểm tra chuỗi dùng chung cho các lớp BLL (tránh lặp code).
/// Lỗi ném BusinessException kèm tên trường để GUI gắn ErrorProvider.
/// </summary>
internal static class KiemTraDuLieu
{
    /// <summary>
    /// Bắt buộc nhập, cắt khoảng trắng, không vượt độ dài tối đa. Trả về chuỗi đã cắt.
    /// </summary>
    /// <param name="giaTri">Giá trị người dùng nhập.</param>
    /// <param name="tenHienThi">Tên trường hiển thị trong thông báo, ví dụ "Tên sách".</param>
    /// <param name="truong">Tên thuộc tính (FieldName).</param>
    /// <param name="doDaiToiDa">Độ dài cột trong CSDL.</param>
    public static string BatBuoc(string? giaTri, string tenHienThi, string truong, int doDaiToiDa)
    {
        string daCat = (giaTri ?? string.Empty).Trim();
        if (daCat.Length == 0)
        {
            throw new BusinessException($"{tenHienThi} không được để trống.", truong);
        }

        if (daCat.Length > doDaiToiDa)
        {
            throw new BusinessException($"{tenHienThi} tối đa {doDaiToiDa} ký tự.", truong);
        }

        return daCat;
    }

    /// <summary>
    /// Không bắt buộc: chuỗi rỗng → null; có giá trị thì cắt khoảng trắng và kiểm tra độ dài.
    /// </summary>
    public static string? TuyChon(string? giaTri, string tenHienThi, string truong, int doDaiToiDa)
    {
        string daCat = (giaTri ?? string.Empty).Trim();
        if (daCat.Length == 0)
        {
            return null;
        }

        if (daCat.Length > doDaiToiDa)
        {
            throw new BusinessException($"{tenHienThi} tối đa {doDaiToiDa} ký tự.", truong);
        }

        return daCat;
    }
}
