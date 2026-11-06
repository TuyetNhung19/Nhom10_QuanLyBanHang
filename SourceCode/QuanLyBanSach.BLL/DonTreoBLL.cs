// File: QuanLyBanSach.BLL/DonTreoBLL.cs
using System.Globalization;
using QuanLyBanSach.DAL;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.BLL;

/// <summary>
/// Treo hóa đơn (ĐT2): đơn treo chỉ nằm trong bộ nhớ theo từng thu ngân, KHÔNG ghi CSDL,
/// chưa trừ tồn (BH-01); đăng xuất thì bỏ hết (BH-02).
/// (FrmDonTreo dùng MVP: DonTreoPresenter gọi lớp này. Các phương thức virtual để test Presenter thay bằng BLL giả.)
/// </summary>
public class DonTreoBLL
{
    /// <summary>Số đơn treo tối đa của mỗi thu ngân (BH-01).</summary>
    public const int SoDonToiDa = 5;

    private static readonly CultureInfo VanHoaVN = new("vi-VN");
    private static readonly object Khoa = new();

    /// <summary>Đơn treo theo MaTK – dùng chung cho mọi Form trong chương trình.</summary>
    private static readonly Dictionary<int, List<HoaDonTam>> DonTreo = new();

    private readonly SachDAL _sachDal = new();

    /// <summary>
    /// Treo đơn đang lập. Đơn rỗng không treo; tối đa 5 đơn mỗi thu ngân.
    /// Lưu BẢN SAO các dòng để Form xóa đơn hiện tại không ảnh hưởng đơn đã treo.
    /// </summary>
    public void Treo(HoaDonTam don)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);

        if (don.ChiTiet.Count == 0)
        {
            throw new BusinessException("Đơn chưa có sách nào, không thể treo.");
        }

        int maTK = PhienDangNhap.MaTK;
        lock (Khoa)
        {
            List<HoaDonTam> ds = LayDanhSachCuaToi(maTK);
            if (ds.Count >= SoDonToiDa)
            {
                throw new BusinessException($"Mỗi thu ngân chỉ được treo tối đa {SoDonToiDa} đơn (BH-01).");
            }

            ds.Add(new HoaDonTam
            {
                Id = don.Id,
                MaTK = maTK,
                ThoiGianTreo = DateTime.Now,
                Khach = don.Khach,
                GhiChu = don.GhiChu,
                ChiTiet = don.ChiTiet.Select(ct => ct.SaoChep()).ToList()
            });
        }
    }

    /// <summary>Các đơn treo của thu ngân hiện tại, cũ nhất trước.</summary>
    public virtual List<HoaDonTam> LayDanhSach()
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        lock (Khoa)
        {
            return LayDanhSachCuaToi(PhienDangNhap.MaTK).OrderBy(d => d.ThoiGianTreo).ToList();
        }
    }

    /// <summary>Số đơn treo của thu ngân hiện tại (hiển thị "Đơn treo (n)").</summary>
    public int Dem()
    {
        if (!PhienDangNhap.DaDangNhap)
        {
            return 0;
        }

        lock (Khoa)
        {
            return LayDanhSachCuaToi(PhienDangNhap.MaTK).Count;
        }
    }

    /// <summary>
    /// Gọi lại đơn treo (BH-01): tải lại giá + tồn hiện hành, cập nhật DonGia/SoLuongTon từng dòng
    /// và trả cảnh báo; sách đã ẩn bị loại khỏi đơn. Dòng vượt tồn vẫn giữ để thu ngân sửa –
    /// HoaDonBLL.LuuHoaDon sẽ chặn nếu chưa sửa (TC21). Đơn được lấy ra khỏi danh sách treo.
    /// </summary>
    /// <param name="id">Mã đơn treo.</param>
    /// <param name="canhBao">Danh sách cảnh báo hiển thị cho thu ngân.</param>
    public HoaDonTam GoiLai(Guid id, out List<string> canhBao)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        int maTK = PhienDangNhap.MaTK;

        HoaDonTam don;
        lock (Khoa)
        {
            don = LayDanhSachCuaToi(maTK).FirstOrDefault(d => d.Id == id)
                ?? throw new BusinessException("Không tìm thấy đơn treo (có thể đã được gọi lại).");
        }

        // Tải dữ liệu mới TRƯỚC khi lấy đơn ra, để mất kết nối thì đơn treo không bị mất
        Dictionary<string, Sach> sachHienTai = XuLyLoiCSDL
            .Goi(() => _sachDal.LayTheoDanhSachMa(don.ChiTiet.Select(ct => ct.MaSach)))
            .ToDictionary(s => s.MaSach, StringComparer.OrdinalIgnoreCase);

        canhBao = new List<string>();
        var dongGiuLai = new List<ChiTietHoaDon>();

        foreach (ChiTietHoaDon dong in don.ChiTiet)
        {
            if (!sachHienTai.TryGetValue(dong.MaSach, out Sach? s) || !s.TrangThai)
            {
                canhBao.Add($"'{dong.TenSach}' đã ngừng bán, đã bỏ khỏi đơn.");
                continue;
            }

            if (s.GiaBan != dong.DonGia)
            {
                canhBao.Add($"Giá '{s.TenSach}' đổi {DinhDang(dong.DonGia)} → {DinhDang(s.GiaBan)}");
                dong.DonGia = s.GiaBan;
            }

            dong.TenSach = s.TenSach;
            dong.MaLoai = s.MaLoai;
            dong.SoLuongTon = s.SoLuongTon;
            dong.SachDangBan = true;

            if (dong.SoLuong > s.SoLuongTon)
            {
                canhBao.Add(s.SoLuongTon <= 0
                    ? $"'{s.TenSach}' đã hết hàng (đơn cần {dong.SoLuong})"
                    : $"'{s.TenSach}' chỉ còn {s.SoLuongTon} cuốn (đơn cần {dong.SoLuong})");
            }

            dongGiuLai.Add(dong);
        }

        for (int i = 0; i < dongGiuLai.Count; i++)
        {
            dongGiuLai[i].Stt = i + 1;
        }

        don.ChiTiet = dongGiuLai;

        lock (Khoa)
        {
            LayDanhSachCuaToi(maTK).RemoveAll(d => d.Id == id);
        }

        return don;
    }

    /// <summary>Bỏ một đơn treo.</summary>
    public virtual void Huy(Guid id)
    {
        PhienDangNhap.YeuCauVaiTro(VaiTro.ThuNgan, VaiTro.QuanLy);
        lock (Khoa)
        {
            LayDanhSachCuaToi(PhienDangNhap.MaTK).RemoveAll(d => d.Id == id);
        }
    }

    /// <summary>Bỏ toàn bộ đơn treo của người dùng hiện tại (khi đăng xuất / đóng Form bán hàng – BH-02).</summary>
    public void HuyTatCa()
    {
        if (!PhienDangNhap.DaDangNhap)
        {
            return;
        }

        lock (Khoa)
        {
            DonTreo.Remove(PhienDangNhap.MaTK);
        }
    }

    /// <summary>Danh sách đơn treo của một thu ngân (tạo mới nếu chưa có). Gọi trong lock.</summary>
    private static List<HoaDonTam> LayDanhSachCuaToi(int maTK)
    {
        if (!DonTreo.TryGetValue(maTK, out List<HoaDonTam>? ds))
        {
            ds = new List<HoaDonTam>();
            DonTreo[maTK] = ds;
        }

        return ds;
    }

    /// <summary>Định dạng tiền "79.000".</summary>
    private static string DinhDang(decimal soTien) => soTien.ToString("#,##0", VanHoaVN);
}
