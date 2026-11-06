// File: QuanLyBanSach.BLL/LoaiSachBLL.cs
using QuanLyBanSach.DAL;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Nghiệp vụ loại sách. Xem: mọi vai trò bán hàng; thêm/sửa/xóa: Quản lý.
/// (FrmLoaiSach dùng mô hình MVP: LoaiSachPresenter gọi lớp này.)
/// </summary>
public class LoaiSachBLL
{
    private readonly LoaiSachDAL _dal = new();

    /// <summary>Danh sách loại sách (đổ ComboBox, lưới).</summary>
    public virtual List<LoaiSach> LayDanhSach()
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.LayDanhSach());
    }

    /// <summary>Tìm loại sách theo tên/mô tả.</summary>
    public List<LoaiSach> TimKiem(string? tuKhoa)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.TimKiem(tuKhoa));
    }

    /// <summary>Thêm loại sách (Quản lý); trả về MaLoai mới.</summary>
    public int Them(LoaiSach loai)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        KiemTra(loai, null);
        loai.MaLoai = XuLyLoiCSDL.Goi(() => _dal.Them(loai));
        return loai.MaLoai;
    }

    /// <summary>Sửa loại sách (Quản lý).</summary>
    public void Sua(LoaiSach loai)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        if (loai.MaLoai <= 0)
        {
            throw new BusinessException("Vui lòng chọn loại sách cần sửa.");
        }

        KiemTra(loai, loai.MaLoai);
        if (!XuLyLoiCSDL.Goi(() => _dal.Sua(loai)))
        {
            throw new BusinessException("Không tìm thấy loại sách cần sửa.");
        }
    }

    /// <summary>
    /// Xóa loại sách (Quản lý). Loại đang có sách thì không được xóa (SP-04).
    /// </summary>
    public void Xoa(LoaiSach loai)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);

        int soSach = XuLyLoiCSDL.Goi(() => _dal.DemSachTheoLoai(loai.MaLoai));
        if (soSach > 0)
        {
            throw new BusinessException(
                $"Không thể xóa loại \"{loai.TenLoai}\" vì đang có {soSach} sách thuộc loại này (SP-04). " +
                "Hãy chuyển các sách sang loại khác trước.");
        }

        if (!XuLyLoiCSDL.Goi(() => _dal.Xoa(loai.MaLoai)))
        {
            throw new BusinessException("Không tìm thấy loại sách cần xóa.");
        }
    }

    /// <summary>Tên bắt buộc, &lt;= 100 ký tự, không trùng; mô tả &lt;= 255 ký tự.</summary>
    private void KiemTra(LoaiSach loai, int? boQuaMaLoai)
    {
        loai.TenLoai = KiemTraDuLieu.BatBuoc(loai.TenLoai, "Tên loại", nameof(LoaiSach.TenLoai), 100);
        loai.MoTa = KiemTraDuLieu.TuyChon(loai.MoTa, "Mô tả", nameof(LoaiSach.MoTa), 255);

        if (XuLyLoiCSDL.Goi(() => _dal.KiemTraTrungTen(loai.TenLoai, boQuaMaLoai)))
        {
            throw new BusinessException($"Loại sách \"{loai.TenLoai}\" đã tồn tại.", nameof(LoaiSach.TenLoai));
        }
    }
}
