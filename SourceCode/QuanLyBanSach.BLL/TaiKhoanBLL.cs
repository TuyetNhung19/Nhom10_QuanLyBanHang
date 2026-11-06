// File: QuanLyBanSach.BLL/TaiKhoanBLL.cs
using System.Text.RegularExpressions;
using QuanLyBanSach.DAL;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Nghiệp vụ tài khoản: đăng nhập, đổi mật khẩu (mọi vai trò) và quản lý tài khoản (Admin).
/// </summary>
public class TaiKhoanBLL
{
    /// <summary>Tên đăng nhập 3–50 ký tự: chữ không dấu, số, gạch dưới, dấu chấm.</summary>
    private static readonly Regex MauTenDangNhap = new(@"^[a-zA-Z0-9_.]{3,50}$");

    /// <summary>Thông báo khi sai tên đăng nhập hoặc mật khẩu (TC02).</summary>
    public const string LoiSaiThongTin = "Sai tên đăng nhập hoặc mật khẩu.";

    /// <summary>Thông báo khi tài khoản bị khóa (TK-01) – GUI so khớp để hiện thông báo riêng.</summary>
    public const string LoiTaiKhoanBiKhoa = "Tài khoản đã bị khóa, liên hệ quản trị viên.";

    private readonly TaiKhoanDAL _dal = new();

    /// <summary>
    /// Đăng nhập (TK-01). Thành công thì gán PhienDangNhap và trả về tài khoản.
    /// </summary>
    /// <exception cref="BusinessException">Thiếu thông tin, sai tên/mật khẩu hoặc tài khoản bị khóa.</exception>
    public TaiKhoan DangNhap(string tenDangNhap, string matKhau)
    {
        string ten = (tenDangNhap ?? string.Empty).Trim();
        if (ten.Length == 0)
        {
            throw new BusinessException("Vui lòng nhập tên đăng nhập.", nameof(TaiKhoan.TenDangNhap));
        }

        if (string.IsNullOrEmpty(matKhau))
        {
            throw new BusinessException("Vui lòng nhập mật khẩu.", nameof(TaiKhoan.MatKhau));
        }

        TaiKhoan? tk = XuLyLoiCSDL.Goi(() => _dal.DangNhap(ten, MatKhauHelper.Bam(matKhau)));
        if (tk == null)
        {
            throw new BusinessException(LoiSaiThongTin, nameof(TaiKhoan.MatKhau));
        }

        if (!tk.TrangThai)
        {
            throw new BusinessException(LoiTaiKhoanBiKhoa, nameof(TaiKhoan.TenDangNhap));
        }

        PhienDangNhap.BatDau(tk);
        return tk;
    }

    /// <summary>
    /// Đăng xuất: bỏ toàn bộ đơn treo của người dùng (đơn treo không lưu CSDL – BH-02;
    /// GUI phải hỏi xác nhận trước khi gọi) rồi xóa phiên.
    /// </summary>
    public void DangXuat()
    {
        if (PhienDangNhap.DaDangNhap)
        {
            new DonTreoBLL().HuyTatCa();
        }

        PhienDangNhap.KetThuc();
    }

    /// <summary>
    /// Người dùng tự đổi mật khẩu: mật khẩu cũ đúng, mới &gt;= 6 ký tự, khác cũ, xác nhận khớp.
    /// </summary>
    public void DoiMatKhau(string matKhauCu, string matKhauMoi, string xacNhan)
    {
        PhienDangNhap.YeuCauDangNhap();

        if (string.IsNullOrEmpty(matKhauCu))
        {
            throw new BusinessException("Vui lòng nhập mật khẩu hiện tại.", "MatKhauCu");
        }

        KiemTraMatKhauMoi(matKhauMoi, "MatKhauMoi");

        if (matKhauMoi == matKhauCu)
        {
            throw new BusinessException("Mật khẩu mới phải khác mật khẩu hiện tại.", "MatKhauMoi");
        }

        if (xacNhan != matKhauMoi)
        {
            throw new BusinessException("Xác nhận mật khẩu không khớp.", "XacNhan");
        }

        bool daDoi = XuLyLoiCSDL.Goi(() =>
            _dal.DoiMatKhau(PhienDangNhap.MaTK, MatKhauHelper.Bam(matKhauCu), MatKhauHelper.Bam(matKhauMoi)));
        if (!daDoi)
        {
            throw new BusinessException("Mật khẩu hiện tại không đúng.", "MatKhauCu");
        }
    }

    /// <summary>
    /// Danh sách người bán hàng (Thu ngân, Quản lý – kể cả tài khoản đã khóa vì vẫn còn hóa đơn cũ)
    /// để đổ ComboBox lọc ở màn hình tra cứu hóa đơn và báo cáo.
    /// </summary>
    public List<TaiKhoan> LayDanhSachNguoiBan()
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.LayDanhSach())
            .Where(tk => tk.VaiTro != VaiTro.Admin)
            .OrderBy(tk => tk.HoTen)
            .ToList();
    }

    /// <summary>Danh sách tài khoản (Admin).</summary>
    public List<TaiKhoan> LayDanhSach()
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.Admin);
        return XuLyLoiCSDL.Goi(() => _dal.LayDanhSach());
    }

    /// <summary>
    /// Thêm tài khoản (Admin): tên 3–50 ký tự [a-zA-Z0-9_.], không trùng; mật khẩu &gt;= 6 ký tự.
    /// </summary>
    /// <param name="tk">Thông tin tài khoản (TenDangNhap, HoTen, VaiTro, TrangThai).</param>
    /// <param name="matKhau">Mật khẩu thô ban đầu.</param>
    /// <returns>MaTK mới.</returns>
    public int Them(TaiKhoan tk, string matKhau)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.Admin);

        tk.TenDangNhap = (tk.TenDangNhap ?? string.Empty).Trim();
        if (!MauTenDangNhap.IsMatch(tk.TenDangNhap))
        {
            throw new BusinessException(
                "Tên đăng nhập dài 3–50 ký tự, chỉ gồm chữ không dấu, số, dấu gạch dưới hoặc dấu chấm.",
                nameof(TaiKhoan.TenDangNhap));
        }

        KiemTraThongTinChung(tk);
        KiemTraMatKhauMoi(matKhau, nameof(TaiKhoan.MatKhau));

        if (XuLyLoiCSDL.Goi(() => _dal.KiemTraTrungTen(tk.TenDangNhap, null)))
        {
            throw new BusinessException($"Tên đăng nhập \"{tk.TenDangNhap}\" đã tồn tại.", nameof(TaiKhoan.TenDangNhap));
        }

        tk.MatKhau = MatKhauHelper.Bam(matKhau);
        tk.MaTK = XuLyLoiCSDL.Goi(() => _dal.Them(tk));
        return tk.MaTK;
    }

    /// <summary>
    /// Sửa họ tên, vai trò (Admin). Không cho tự hạ quyền chính mình và luôn còn ít nhất 1 Admin hoạt động.
    /// </summary>
    public void Sua(TaiKhoan tk)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.Admin);
        KiemTraThongTinChung(tk);

        TaiKhoan cu = LayTonTai(tk.MaTK);
        bool haQuyenAdmin = cu.VaiTro == VaiTro.Admin && tk.VaiTro != VaiTro.Admin;

        if (haQuyenAdmin && tk.MaTK == PhienDangNhap.MaTK)
        {
            throw new BusinessException("Không thể tự đổi vai trò của tài khoản đang đăng nhập.", nameof(TaiKhoan.VaiTro));
        }

        if (haQuyenAdmin && cu.TrangThai && XuLyLoiCSDL.Goi(() => _dal.DemAdminDangHoatDong()) <= 1)
        {
            throw new BusinessException("Hệ thống phải còn ít nhất 1 Admin đang hoạt động.", nameof(TaiKhoan.VaiTro));
        }

        XuLyLoiCSDL.Goi(() => _dal.Sua(tk));
    }

    /// <summary>
    /// Khóa / mở khóa tài khoản (Admin). Không tự khóa chính mình; luôn còn ít nhất 1 Admin hoạt động.
    /// </summary>
    /// <param name="maTK">Tài khoản cần đổi.</param>
    /// <param name="hoatDong">true = mở khóa, false = khóa.</param>
    public void KhoaMoKhoa(int maTK, bool hoatDong)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.Admin);
        TaiKhoan tk = LayTonTai(maTK);

        if (!hoatDong)
        {
            if (maTK == PhienDangNhap.MaTK)
            {
                throw new BusinessException("Không thể tự khóa tài khoản đang đăng nhập.");
            }

            if (tk.VaiTro == VaiTro.Admin && tk.TrangThai && XuLyLoiCSDL.Goi(() => _dal.DemAdminDangHoatDong()) <= 1)
            {
                throw new BusinessException("Không thể khóa Admin cuối cùng đang hoạt động.");
            }
        }

        XuLyLoiCSDL.Goi(() => _dal.DoiTrangThai(maTK, hoatDong));
    }

    /// <summary>Đặt lại mật khẩu cho tài khoản (Admin), mật khẩu mới &gt;= 6 ký tự.</summary>
    public void DatLaiMatKhau(int maTK, string matKhauMoi)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.Admin);
        KiemTraMatKhauMoi(matKhauMoi, "MatKhauMoi");
        LayTonTai(maTK);
        XuLyLoiCSDL.Goi(() => _dal.DatLaiMatKhau(maTK, MatKhauHelper.Bam(matKhauMoi)));
    }

    /// <summary>Mật khẩu mới phải có ít nhất 6 ký tự (TK-01).</summary>
    private static void KiemTraMatKhauMoi(string? matKhau, string truong)
    {
        if (string.IsNullOrEmpty(matKhau) || matKhau.Length < MatKhauHelper.DoDaiToiThieu)
        {
            throw new BusinessException($"Mật khẩu phải có ít nhất {MatKhauHelper.DoDaiToiThieu} ký tự.", truong);
        }
    }

    /// <summary>Kiểm tra họ tên và vai trò hợp lệ.</summary>
    private static void KiemTraThongTinChung(TaiKhoan tk)
    {
        tk.HoTen = KiemTraDuLieu.BatBuoc(tk.HoTen, "Họ tên", nameof(TaiKhoan.HoTen), 100);
        if (!Enum.IsDefined(tk.VaiTro))
        {
            throw new BusinessException("Vai trò không hợp lệ.", nameof(TaiKhoan.VaiTro));
        }
    }

    /// <summary>Lấy tài khoản theo mã, ném lỗi nếu không tồn tại.</summary>
    private TaiKhoan LayTonTai(int maTK) =>
        XuLyLoiCSDL.Goi(() => _dal.LayTheoMa(maTK))
        ?? throw new BusinessException("Không tìm thấy tài khoản.");
}
