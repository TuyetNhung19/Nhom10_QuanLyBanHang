// File: QuanLyBanSach.BLL/PhienDangNhap.cs
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Lưu người dùng đang đăng nhập và kiểm tra quyền cho các thao tác nhạy cảm.
/// GUI dùng LaThuNgan/LaQuanLy/LaAdmin để ẩn hiện menu; BLL gọi YeuCauVaiTro để kiểm tra lại.
/// </summary>
public static class PhienDangNhap
{
    /// <summary>Tài khoản đang đăng nhập; null nếu chưa đăng nhập.</summary>
    public static TaiKhoan? TaiKhoanHienTai { get; private set; }

    /// <summary>Thời điểm đăng nhập.</summary>
    public static DateTime? DangNhapLuc { get; private set; }

    /// <summary>true nếu đã có người đăng nhập.</summary>
    public static bool DaDangNhap => TaiKhoanHienTai != null;

    /// <summary>Người dùng hiện tại là Thu ngân.</summary>
    public static bool LaThuNgan => TaiKhoanHienTai?.VaiTro == VaiTro.ThuNgan;

    /// <summary>Người dùng hiện tại là Quản lý.</summary>
    public static bool LaQuanLy => TaiKhoanHienTai?.VaiTro == VaiTro.QuanLy;

    /// <summary>Người dùng hiện tại là Admin.</summary>
    public static bool LaAdmin => TaiKhoanHienTai?.VaiTro == VaiTro.Admin;

    /// <summary>Mã tài khoản hiện tại; ném BusinessException nếu chưa đăng nhập.</summary>
    public static int MaTK => TaiKhoanHienTai?.MaTK
        ?? throw new BusinessException("Bạn chưa đăng nhập. Vui lòng đăng nhập lại.");

    /// <summary>Ghi nhận phiên đăng nhập mới (chỉ TaiKhoanBLL gọi).</summary>
    internal static void BatDau(TaiKhoan taiKhoan)
    {
        TaiKhoanHienTai = taiKhoan;
        DangNhapLuc = DateTime.Now;
    }

    /// <summary>Xóa phiên đăng nhập (chỉ TaiKhoanBLL gọi).</summary>
    internal static void KetThuc()
    {
        TaiKhoanHienTai = null;
        DangNhapLuc = null;
    }

    /// <summary>Ném BusinessException nếu chưa đăng nhập.</summary>
    public static void YeuCauDangNhap()
    {
        if (!DaDangNhap)
        {
            throw new BusinessException("Bạn chưa đăng nhập. Vui lòng đăng nhập lại.");
        }
    }

    /// <summary>
    /// Ném BusinessException nếu người dùng hiện tại không thuộc một trong các vai trò cho phép.
    /// </summary>
    /// <param name="vaiTroChoPhep">Các vai trò được phép thực hiện thao tác.</param>
    public static void YeuCauVaiTro(params VaiTro[] vaiTroChoPhep)
    {
        YeuCauDangNhap();
        if (!vaiTroChoPhep.Contains(TaiKhoanHienTai!.VaiTro))
        {
            throw new BusinessException("Bạn không có quyền thực hiện chức năng này.");
        }
    }
}
