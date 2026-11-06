// File: QuanLyBanSach.BLL/KhachHangBLL.cs
using System.Text.RegularExpressions;
using QuanLyBanSach.DAL;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Nghiệp vụ khách hàng. Thêm/sửa/tìm: Thu ngân, Quản lý; xóa: chỉ Quản lý (KH-02).
/// (FrmKhachHang dùng MVP: KhachHangPresenter gọi lớp này. Các phương thức virtual để test Presenter thay bằng BLL giả.)
/// </summary>
public class KhachHangBLL
{
    private static readonly Regex MauSDT = new(@"^0\d{9}$");
    private static readonly Regex MauEmail = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    private readonly KhachHangDAL _dal = new();

    /// <summary>Tìm khách theo mã, tên hoặc SĐT; từ khóa rỗng = tất cả.</summary>
    public virtual List<KhachHang> TimKiem(string? tuKhoa)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.TimKiem(tuKhoa));
    }

    /// <summary>Tìm khách theo SĐT chính xác khi bán hàng; null nếu SĐT sai định dạng hoặc chưa có.</summary>
    public KhachHang? LayTheoSDT(string? sdt)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        string so = (sdt ?? string.Empty).Trim();
        if (!MauSDT.IsMatch(so))
        {
            return null;
        }

        return XuLyLoiCSDL.Goi(() => _dal.LayTheoSDT(so));
    }

    /// <summary>Lịch sử hóa đơn của khách, mới nhất trước.</summary>
    public virtual List<HoaDon> LayLichSuHoaDon(int maKH)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.LayLichSuHoaDon(maKH));
    }

    /// <summary>Thêm khách hàng; trả về MaKH mới.</summary>
    public virtual int Them(KhachHang kh)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        KiemTra(kh, null);
        kh.MaKH = XuLyLoiCSDL.Goi(() => _dal.Them(kh));
        return kh.MaKH;
    }

    /// <summary>Sửa thông tin khách hàng.</summary>
    public virtual void Sua(KhachHang kh)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        if (kh.MaKH <= 0)
        {
            throw new BusinessException("Vui lòng chọn khách hàng cần sửa.");
        }

        KiemTra(kh, kh.MaKH);
        if (!XuLyLoiCSDL.Goi(() => _dal.Sua(kh)))
        {
            throw new BusinessException("Không tìm thấy khách hàng cần sửa.");
        }
    }

    /// <summary>Xóa khách hàng (chỉ Quản lý); khách đã có hóa đơn thì không được xóa (KH-02).</summary>
    public virtual void Xoa(int maKH)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);

        if (XuLyLoiCSDL.Goi(() => _dal.DaCoHoaDon(maKH)))
        {
            throw new BusinessException("Không thể xóa khách hàng đã có hóa đơn (KH-02).");
        }

        if (!XuLyLoiCSDL.Goi(() => _dal.Xoa(maKH)))
        {
            throw new BusinessException("Không tìm thấy khách hàng cần xóa.");
        }
    }

    /// <summary>Họ tên bắt buộc; SĐT ^0\d{9}$ và không trùng (KH-01); email hợp lệ nếu có.</summary>
    private void KiemTra(KhachHang kh, int? boQuaMaKH)
    {
        kh.HoTen = KiemTraDuLieu.BatBuoc(kh.HoTen, "Họ tên", nameof(KhachHang.HoTen), 100);

        kh.SDT = (kh.SDT ?? string.Empty).Trim().Replace(" ", string.Empty).Replace(".", string.Empty);
        if (!MauSDT.IsMatch(kh.SDT))
        {
            throw new BusinessException("Số điện thoại phải gồm 10 chữ số, bắt đầu bằng 0.", nameof(KhachHang.SDT));
        }

        if (XuLyLoiCSDL.Goi(() => _dal.KiemTraTrungSDT(kh.SDT, boQuaMaKH)))
        {
            throw new BusinessException($"Số điện thoại {kh.SDT} đã thuộc khách hàng khác.", nameof(KhachHang.SDT));
        }

        kh.Email = KiemTraDuLieu.TuyChon(kh.Email, "Email", nameof(KhachHang.Email), 100);
        if (kh.Email != null && !MauEmail.IsMatch(kh.Email))
        {
            throw new BusinessException("Email không hợp lệ.", nameof(KhachHang.Email));
        }

        kh.DiaChi = KiemTraDuLieu.TuyChon(kh.DiaChi, "Địa chỉ", nameof(KhachHang.DiaChi), 255);
    }
}
