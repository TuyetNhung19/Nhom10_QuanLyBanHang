// File: QuanLyBanSach.GUI/Presenters/DonTreoPresenter.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.GUI.Presenters;

/// <summary>
/// Presenter của màn hình Danh sách đơn treo (MVP): tải đơn treo của thu ngân hiện tại,
/// xem nhanh chi tiết, gọi lại, hủy đơn. Không dùng control hay MessageBox trực tiếp.
/// </summary>
public class DonTreoPresenter
{
    /// <summary>Đơn treo quá thời gian này được tô vàng để nhắc thu ngân.</summary>
    public static readonly TimeSpan ThoiGianTreoLau = TimeSpan.FromMinutes(30);

    private readonly IDonTreoView _view;
    private readonly DonTreoBLL _bll;
    private List<HoaDonTam> _danhSach = new();

    /// <summary>Gắn presenter với view.</summary>
    /// <param name="view">Form (hoặc View giả khi kiểm thử).</param>
    /// <param name="bll">BLL dùng để gọi nghiệp vụ; bỏ trống = DonTreoBLL thật (kiểm thử truyền BLL giả).</param>
    public DonTreoPresenter(IDonTreoView view, DonTreoBLL? bll = null)
    {
        _view = view;
        _bll = bll ?? new DonTreoBLL();
    }

    /// <summary>Tải các đơn treo của thu ngân hiện tại (cũ nhất trước).</summary>
    public void TaiDanhSach()
    {
        try
        {
            _danhSach = _bll.LayDanhSach();
            _view.HienDanhSach(_danhSach, $"{_danhSach.Count}/{DonTreoBLL.SoDonToiDa} đơn treo");
            ChonDong();
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Người dùng chọn dòng khác: xem nhanh chi tiết đơn đó.</summary>
    public void ChonDong()
    {
        HoaDonTam? don = DonDangChon();
        string tieuDe = don == null
            ? "Chi tiết đơn"
            : $"Chi tiết đơn – {don.TenKhachHienThi}" + (string.IsNullOrEmpty(don.GhiChu) ? string.Empty : $" (ghi chú: {don.GhiChu})");
        _view.HienChiTiet(don?.ChiTiet, tieuDe, don != null);
    }

    /// <summary>Gọi lại đơn đang chọn: đóng màn hình và trả mã đơn về FrmBanHang.</summary>
    public void GoiLai()
    {
        if (DonDangChon() is { } don)
        {
            _view.DongVaGoiLai(don.Id);
        }
    }

    /// <summary>Hủy đơn treo đang chọn sau khi xác nhận.</summary>
    public void HuyDon()
    {
        if (DonDangChon() is not { } don)
        {
            return;
        }

        if (!_view.XacNhan($"Hủy đơn treo lúc {don.GioTreoText} của {don.TenKhachHienThi} ({don.SoCuon} cuốn)?"))
        {
            return;
        }

        try
        {
            _bll.Huy(don.Id);
            TaiDanhSach();
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>true nếu đơn đã treo quá 30 phút tính đến <paramref name="bayGio"/> (Form tô vàng dòng này).</summary>
    public static bool LaTreoLau(HoaDonTam don, DateTime bayGio) => bayGio - don.ThoiGianTreo > ThoiGianTreoLau;

    /// <summary>Đơn ứng với dòng đang chọn.</summary>
    private HoaDonTam? DonDangChon()
    {
        Guid? id = _view.IdDangChon;
        return id == null ? null : _danhSach.FirstOrDefault(d => d.Id == id);
    }

    private void XuLyLoi(Exception ex) => _view.HienLoi(ThongDiepLoi.Tu(ex).ThongDiep);
}
