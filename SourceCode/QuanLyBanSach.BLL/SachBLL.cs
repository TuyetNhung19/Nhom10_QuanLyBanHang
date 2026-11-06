// File: QuanLyBanSach.BLL/SachBLL.cs
using System.Text.RegularExpressions;
using QuanLyBanSach.DAL;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Nghiệp vụ sách. Xem/tìm: Thu ngân, Quản lý (Admin không xem sách); thêm/sửa/ẩn/giá: Quản lý.
/// Đồ án không xóa cứng sách: chỉ ẩn/hiện (SP-03).
/// </summary>
public class SachBLL
{
    private static readonly Regex MauISBN = new(@"^(\d{10}|\d{13})$");
    private static readonly string[] DuoiAnhHopLe = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

    /// <summary>Giá lớn nhất cột DECIMAL(12,0) chứa được.</summary>
    private const decimal GiaToiDa = 999_999_999_999m;

    private readonly SachDAL _dal = new();

    /// <summary>Thư mục Images cạnh file .exe – nơi lưu ảnh bìa (CSDL chỉ lưu tên file).</summary>
    public static string ThuMucAnh => Path.Combine(AppContext.BaseDirectory, "Images");

    /// <summary>
    /// Tìm sách theo từ khóa và loại. Thu ngân luôn chỉ thấy sách đang bán.
    /// </summary>
    public List<Sach> TimKiem(string? tuKhoa, int? maLoai, bool chiDangBan)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        bool chiLayDangBan = chiDangBan || !PhienDangNhap.LaQuanLy;
        return XuLyLoiCSDL.Goi(() => _dal.TimSach(tuKhoa, maLoai, chiLayDangBan));
    }

    /// <summary>
    /// ĐT1: tìm sách đang bán theo mã sách hoặc ISBN (gõ/quét + Enter); null nếu không thấy.
    /// </summary>
    public Sach? TimTheoMaHoacISBN(string? ma)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        if (string.IsNullOrWhiteSpace(ma))
        {
            return null;
        }

        return XuLyLoiCSDL.Goi(() => _dal.LayTheoMaHoacISBN(ma.Trim()));
    }

    /// <summary>Sách sắp hết (tồn &lt;= tồn tối thiểu).</summary>
    public List<Sach> LaySachSapHet(int? maLoai)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.LaySachSapHet(maLoai));
    }

    /// <summary>Gợi ý mã sách mới (S + 4 số) cho Form thêm sách (Quản lý).</summary>
    public string TaoMaSachMoi()
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        return XuLyLoiCSDL.Goi(() => _dal.TaoMaSachMoi());
    }

    /// <summary>Thêm sách (Quản lý) – SP-01, SP-02.</summary>
    public void Them(Sach s)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);

        s.MaSach = KiemTraDuLieu.BatBuoc(s.MaSach, "Mã sách", nameof(Sach.MaSach), 20).ToUpperInvariant();
        if (XuLyLoiCSDL.Goi(() => _dal.KiemTraTrungMa(s.MaSach)))
        {
            throw new BusinessException($"Mã sách \"{s.MaSach}\" đã tồn tại.", nameof(Sach.MaSach));
        }

        KiemTra(s, null);
        XuLyLoiCSDL.Goi(() => _dal.Them(s));
    }

    /// <summary>Sửa sách, kể cả giá bán (Quản lý – BLL kiểm tra lại quyền sửa giá).</summary>
    public void Sua(Sach s)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        KiemTra(s, s.MaSach);
        if (!XuLyLoiCSDL.Goi(() => _dal.Sua(s)))
        {
            throw new BusinessException("Không tìm thấy sách cần sửa.");
        }
    }

    /// <summary>
    /// Ẩn (false) hoặc hiện lại (true) sách (Quản lý). Sách đã có trong hóa đơn chỉ được ẩn (SP-03);
    /// sách chưa bán lần nào cũng chỉ ẩn vì đồ án không xóa cứng sách.
    /// </summary>
    public void AnHien(string maSach, bool dangBan)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);
        if (!XuLyLoiCSDL.Goi(() => _dal.CapNhatTrangThai(maSach, dangBan)))
        {
            throw new BusinessException("Không tìm thấy sách.");
        }
    }

    /// <summary>
    /// Chép ảnh bìa vào thư mục Images cạnh .exe với tên &lt;MaSach&gt;&lt;đuôi&gt; (ví dụ S0001.jpg),
    /// xóa ảnh cũ khác đuôi của cùng sách. Trả về tên file để lưu vào cột AnhBia.
    /// </summary>
    public string LuuAnhBia(string duongDanNguon, string maSach)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.QuanLy);

        if (string.IsNullOrWhiteSpace(maSach))
        {
            throw new BusinessException("Vui lòng nhập mã sách trước khi chọn ảnh bìa.", nameof(Sach.MaSach));
        }

        if (string.IsNullOrWhiteSpace(duongDanNguon) || !File.Exists(duongDanNguon))
        {
            throw new BusinessException("Không tìm thấy file ảnh đã chọn.", nameof(Sach.AnhBia));
        }

        string duoi = Path.GetExtension(duongDanNguon).ToLowerInvariant();
        if (!DuoiAnhHopLe.Contains(duoi))
        {
            throw new BusinessException("Chỉ chấp nhận ảnh .jpg, .jpeg, .png, .bmp, .gif.", nameof(Sach.AnhBia));
        }

        string tenFile = maSach.Trim().ToUpperInvariant() + duoi;
        string dich = Path.Combine(ThuMucAnh, tenFile);

        try
        {
            Directory.CreateDirectory(ThuMucAnh);

            if (!string.Equals(Path.GetFullPath(duongDanNguon), Path.GetFullPath(dich), StringComparison.OrdinalIgnoreCase))
            {
                File.Copy(duongDanNguon, dich, overwrite: true);
            }

            foreach (string cu in Directory.GetFiles(ThuMucAnh, maSach.Trim() + ".*"))
            {
                if (!string.Equals(Path.GetFileName(cu), tenFile, StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(cu);
                }
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new BusinessException("Không lưu được ảnh bìa: " + ex.Message, ex, nameof(Sach.AnhBia));
        }

        return tenFile;
    }

    /// <summary>Đường dẫn đầy đủ của ảnh bìa để PictureBox hiển thị; null nếu không có file.</summary>
    public static string? LayDuongDanAnhBia(string? tenFile)
    {
        if (string.IsNullOrWhiteSpace(tenFile))
        {
            return null;
        }

        string duongDan = Path.Combine(ThuMucAnh, tenFile);
        return File.Exists(duongDan) ? duongDan : null;
    }

    /// <summary>Kiểm tra các trường của sách (SP-01, SP-02, ISBN, tồn tối thiểu).</summary>
    private void KiemTra(Sach s, string? boQuaMaSach)
    {
        s.TenSach = KiemTraDuLieu.BatBuoc(s.TenSach, "Tên sách", nameof(Sach.TenSach), 255);
        s.TacGia = KiemTraDuLieu.TuyChon(s.TacGia, "Tác giả", nameof(Sach.TacGia), 100);
        s.NhaXuatBan = KiemTraDuLieu.TuyChon(s.NhaXuatBan, "Nhà xuất bản", nameof(Sach.NhaXuatBan), 100);

        if (s.MaLoai <= 0)
        {
            throw new BusinessException("Vui lòng chọn loại sách.", nameof(Sach.MaLoai));
        }

        if (s.GiaBan < 0)
        {
            throw new BusinessException("Giá bán không được âm.", nameof(Sach.GiaBan));
        }

        if (s.GiaBan > GiaToiDa || s.GiaBan != decimal.Truncate(s.GiaBan))
        {
            throw new BusinessException("Giá bán không hợp lệ (số nguyên, tối đa 12 chữ số).", nameof(Sach.GiaBan));
        }

        if (s.SoLuongTon < 0)
        {
            throw new BusinessException("Số lượng tồn không được âm.", nameof(Sach.SoLuongTon));
        }

        if (s.TonToiThieu < 0)
        {
            throw new BusinessException("Tồn tối thiểu không được âm.", nameof(Sach.TonToiThieu));
        }

        s.ISBN = string.IsNullOrWhiteSpace(s.ISBN) ? null : s.ISBN.Trim().Replace("-", string.Empty);
        if (s.ISBN != null)
        {
            if (!MauISBN.IsMatch(s.ISBN))
            {
                throw new BusinessException("ISBN phải gồm đúng 10 hoặc 13 chữ số.", nameof(Sach.ISBN));
            }

            string isbn = s.ISBN;
            if (XuLyLoiCSDL.Goi(() => _dal.KiemTraTrungISBN(isbn, boQuaMaSach)))
            {
                throw new BusinessException($"ISBN {isbn} đã được dùng cho sách khác.", nameof(Sach.ISBN));
            }
        }
    }
}
