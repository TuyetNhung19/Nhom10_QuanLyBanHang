// File: QuanLyBanSach.BLL/MatKhauHelper.cs
using System.Security.Cryptography;
using System.Text;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Băm mật khẩu SHA-256 (TK-02). Kết quả khớp với SQL:
/// CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'mk'), 2).
/// </summary>
public static class MatKhauHelper
{
    /// <summary>Độ dài tối thiểu của mật khẩu (TK-01).</summary>
    public const int DoDaiToiThieu = 6;

    /// <summary>
    /// Băm chuỗi mật khẩu thành 64 ký tự hex IN HOA.
    /// </summary>
    /// <param name="matKhau">Mật khẩu thô.</param>
    public static string Bam(string matKhau)
    {
        ArgumentNullException.ThrowIfNull(matKhau);
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(matKhau)));
    }
}
