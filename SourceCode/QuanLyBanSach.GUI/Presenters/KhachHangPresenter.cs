// File: QuanLyBanSach.GUI/Presenters/KhachHangPresenter.cs
using System.Globalization;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.GUI.Presenters;

/// <summary>
/// Presenter của màn hình Khách hàng (MVP): tìm, thêm, sửa, xóa khách, xem lịch sử mua,
/// và chế độ CHỌN KHÁCH khi được mở từ FrmBanHang. Không dùng control hay MessageBox trực tiếp.
/// </summary>
public class KhachHangPresenter
{
    /// <summary>Chế độ làm việc hiện tại của màn hình.</summary>
    private enum CheDo
    {
        Xem,
        Them,
        Sua
    }

    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    private readonly IKhachHangView _view;
    private readonly KhachHangBLL _bll;
    private readonly string? _sdtGoiY;
    private readonly bool _cheDoChon;
    private List<KhachHang> _danhSach = new();
    private CheDo _cheDo = CheDo.Xem;

    /// <summary>Gắn presenter với view.</summary>
    /// <param name="view">Form (hoặc View giả khi kiểm thử).</param>
    /// <param name="sdtGoiY">SĐT gõ sẵn từ màn hình bán hàng; null nếu không có.</param>
    /// <param name="cheDoChon">true: mở để chọn khách cho hóa đơn.</param>
    /// <param name="bll">BLL dùng để gọi nghiệp vụ; bỏ trống = KhachHangBLL thật (kiểm thử truyền BLL giả).</param>
    public KhachHangPresenter(IKhachHangView view, string? sdtGoiY, bool cheDoChon, KhachHangBLL? bll = null)
    {
        _view = view;
        _sdtGoiY = string.IsNullOrWhiteSpace(sdtGoiY) ? null : sdtGoiY.Trim();
        _cheDoChon = cheDoChon;
        _bll = bll ?? new KhachHangBLL();
    }

    /// <summary>
    /// Mở màn hình: cài đặt nút theo quyền/chế độ, tải danh sách.
    /// Chế độ chọn với SĐT chưa có khách: mở sẵn thêm mới với SĐT đã điền.
    /// </summary>
    public void KhoiTao()
    {
        _view.CaiDatManHinh(PhienDangNhap.LaQuanLy, _cheDoChon);
        DatCheDo(CheDo.Xem);

        if (_sdtGoiY != null)
        {
            _view.TuKhoa = _sdtGoiY;
        }

        TaiDanhSach();

        if (_cheDoChon && _sdtGoiY != null && _danhSach.Count == 0)
        {
            Them();
            _view.SDT = _sdtGoiY;
        }
    }

    /// <summary>Tìm khách theo từ khóa và đổ lên lưới, chọn lại khách có mã cho trước.</summary>
    public void TaiDanhSach(int? maKHChon = null)
    {
        try
        {
            int? maDangChon = maKHChon ?? _view.MaKHDangChon;
            _danhSach = _bll.TimKiem(_view.TuKhoa);
            _view.HienDanhSach(_danhSach, maDangChon);
            HienKhachDangChon();
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Tìm kiếm (Form gọi sau khi người dùng ngừng gõ).</summary>
    public void TimKiem() => TaiDanhSach();

    /// <summary>Người dùng chọn dòng khác trên lưới (chỉ xử lý khi đang xem).</summary>
    public void ChonDong()
    {
        if (_cheDo == CheDo.Xem)
        {
            HienKhachDangChon();
        }
    }

    /// <summary>Bắt đầu thêm mới.</summary>
    public void Them()
    {
        DatCheDo(CheDo.Them);
        _view.XoaONhap();
    }

    /// <summary>Bắt đầu sửa khách đang chọn.</summary>
    public void Sua()
    {
        if (KhachDangChon() == null)
        {
            _view.HienLoi(null, "Vui lòng chọn khách hàng cần sửa.");
            return;
        }

        DatCheDo(CheDo.Sua);
    }

    /// <summary>Lưu thêm mới hoặc sửa. Chế độ chọn: thêm xong thì chọn luôn khách vừa thêm.</summary>
    public void Luu()
    {
        var kh = new KhachHang
        {
            MaKH = _cheDo == CheDo.Sua ? KhachDangChon()?.MaKH ?? 0 : 0,
            HoTen = _view.HoTen,
            SDT = _view.SDT,
            Email = _view.Email,
            DiaChi = _view.DiaChi
        };

        try
        {
            if (_cheDo == CheDo.Them)
            {
                _bll.Them(kh);
            }
            else
            {
                _bll.Sua(kh);
            }

            if (_cheDoChon && _cheDo == CheDo.Them)
            {
                _view.DongVaTraVe(kh);
                return;
            }

            _view.HienThongBao(_cheDo == CheDo.Them ? $"Đã thêm khách hàng \"{kh.HoTen}\"." : "Đã cập nhật khách hàng.");
            DatCheDo(CheDo.Xem);
            TaiDanhSach(kh.MaKH);
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Hủy thêm/sửa, trả lại dữ liệu khách đang chọn.</summary>
    public void Huy()
    {
        DatCheDo(CheDo.Xem);
        HienKhachDangChon();
    }

    /// <summary>Xóa khách đang chọn sau khi xác nhận (BLL chặn khách đã có hóa đơn – KH-02).</summary>
    public void Xoa()
    {
        KhachHang? kh = KhachDangChon();
        if (kh == null)
        {
            _view.HienLoi(null, "Vui lòng chọn khách hàng cần xóa.");
            return;
        }

        if (!_view.XacNhan($"Xóa khách hàng \"{kh.HoTen}\" ({kh.SDT})?"))
        {
            return;
        }

        try
        {
            _bll.Xoa(kh.MaKH);
            _view.HienThongBao("Đã xóa khách hàng.");
            TaiDanhSach();
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Nút Chọn: trả khách đang chọn về FrmBanHang.</summary>
    public void Chon()
    {
        KhachHang? kh = KhachDangChon();
        if (kh == null)
        {
            _view.HienLoi(null, "Vui lòng chọn một khách hàng.");
            return;
        }

        _view.DongVaTraVe(kh);
    }

    /// <summary>Nhấp đúp một dòng: chỉ có tác dụng ở chế độ chọn.</summary>
    public void NhapDupDong()
    {
        if (_cheDoChon)
        {
            Chon();
        }
    }

    /// <summary>Nút Đóng (chế độ chọn): đóng mà không chọn khách.</summary>
    public void Dong() => _view.DongVaTraVe(null);

    /// <summary>
    /// Dòng tổng kết lịch sử: "Tên: n lần mua – tổng Xđ (m hóa đơn đã hủy)". Chỉ cộng hóa đơn hoàn tất.
    /// </summary>
    public static string TaoTongKet(KhachHang kh, List<HoaDon> lichSu)
    {
        List<HoaDon> hoanTat = lichSu.Where(hd => hd.TrangThai == TrangThaiHoaDon.HoanTat).ToList();
        int soHuy = lichSu.Count - hoanTat.Count;
        decimal tong = hoanTat.Sum(hd => hd.TongThanhToan);

        return $"{kh.HoTen}: {hoanTat.Count} lần mua – tổng {tong.ToString("#,##0", VanHoaVN)}đ"
               + (soHuy > 0 ? $" ({soHuy} hóa đơn đã hủy)" : string.Empty);
    }

    /// <summary>Đổ khách đang chọn lên ô nhập, tải lịch sử mua, cập nhật nút theo việc có dòng chọn hay không.</summary>
    private void HienKhachDangChon()
    {
        KhachHang? kh = KhachDangChon();
        _view.HoTen = kh?.HoTen ?? string.Empty;
        _view.SDT = kh?.SDT ?? string.Empty;
        _view.Email = kh?.Email ?? string.Empty;
        _view.DiaChi = kh?.DiaChi ?? string.Empty;
        TaiLichSu(kh);

        if (_cheDo == CheDo.Xem)
        {
            _view.DatCheDo(false, kh != null);
        }
    }

    /// <summary>Lịch sử hóa đơn của khách; lỗi thì xóa lưới lịch sử và báo lỗi.</summary>
    private void TaiLichSu(KhachHang? kh)
    {
        if (kh == null)
        {
            _view.HienLichSu(null, "Chưa chọn khách hàng");
            return;
        }

        try
        {
            List<HoaDon> lichSu = _bll.LayLichSuHoaDon(kh.MaKH);
            _view.HienLichSu(lichSu, TaoTongKet(kh, lichSu));
        }
        catch (Exception ex)
        {
            _view.HienLichSu(null, "Không tải được lịch sử mua.");
            XuLyLoi(ex);
        }
    }

    /// <summary>Khách ứng với dòng đang chọn.</summary>
    private KhachHang? KhachDangChon()
    {
        int? ma = _view.MaKHDangChon;
        return ma == null ? null : _danhSach.FirstOrDefault(k => k.MaKH == ma);
    }

    private void DatCheDo(CheDo cheDo)
    {
        _cheDo = cheDo;
        _view.DatCheDo(cheDo != CheDo.Xem, KhachDangChon() != null);
    }

    private void XuLyLoi(Exception ex)
    {
        (string? truong, string thongDiep) = ThongDiepLoi.Tu(ex);
        _view.HienLoi(truong, thongDiep);
    }
}
