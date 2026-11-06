// File: QuanLyBanSach.DTO/TaiKhoan.cs
namespace QuanLyBanSach.DTO;

/// <summary>
/// Tài khoản đăng nhập – ánh xạ bảng TaiKhoan.
/// </summary>
public class TaiKhoan
{
    /// <summary>Mã tài khoản (IDENTITY).</summary>
    public int MaTK { get; set; }

    /// <summary>Tên đăng nhập, duy nhất (TK-01).</summary>
    public string TenDangNhap { get; set; } = string.Empty;

    /// <summary>Chuỗi băm SHA-256 hex in hoa của mật khẩu (TK-02). Không bao giờ chứa mật khẩu thô.</summary>
    public string MatKhau { get; set; } = string.Empty;

    /// <summary>Họ tên người dùng.</summary>
    public string HoTen { get; set; } = string.Empty;

    /// <summary>Vai trò: Thu ngân, Quản lý, Admin.</summary>
    public VaiTro VaiTro { get; set; } = VaiTro.ThuNgan;

    /// <summary>true = đang hoạt động, false = đã khóa.</summary>
    public bool TrangThai { get; set; } = true;

    /// <summary>Tên vai trò hiển thị trên lưới và thanh trạng thái.</summary>
    public string TenVaiTro => VaiTro switch
    {
        VaiTro.ThuNgan => "Thu ngân",
        VaiTro.QuanLy => "Quản lý",
        VaiTro.Admin => "Quản trị viên",
        _ => "Không xác định"
    };

    /// <summary>Trạng thái hiển thị: "Hoạt động" / "Đã khóa".</summary>
    public string TrangThaiText => TrangThai ? "Hoạt động" : "Đã khóa";

    /// <summary>Hiển thị dạng "Họ tên (tên đăng nhập)".</summary>
    public override string ToString() => $"{HoTen} ({TenDangNhap})";
}
