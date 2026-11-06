// File: QuanLyBanSach.GUI/Presenters/LoaiSachPresenter.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Views;

namespace QuanLyBanSach.GUI.Presenters;

/// <summary>
/// Presenter của màn hình Loại sách (MVP): nhận yêu cầu từ View, gọi LoaiSachBLL,
/// rồi ra lệnh cho View hiển thị. Không dùng control hay MessageBox trực tiếp.
/// </summary>
public class LoaiSachPresenter
{
    /// <summary>Chế độ làm việc hiện tại của màn hình.</summary>
    private enum CheDo
    {
        Xem,
        Them,
        Sua
    }

    private readonly ILoaiSachView _view;
    private readonly LoaiSachBLL _bll = new();
    private List<LoaiSach> _danhSach = new();
    private CheDo _cheDo = CheDo.Xem;

    /// <summary>Gắn presenter với view.</summary>
    public LoaiSachPresenter(ILoaiSachView view)
    {
        _view = view;
    }

    /// <summary>Tải danh sách theo từ khóa hiện tại và chuyển về chế độ xem.</summary>
    public void TaiDanhSach()
    {
        try
        {
            _danhSach = _bll.TimKiem(_view.TuKhoa);
            _view.HienDanhSach(_danhSach);
            DatCheDo(CheDo.Xem);
            ChonDong();
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Tìm kiếm khi người dùng gõ từ khóa.</summary>
    public void TimKiem() => TaiDanhSach();

    /// <summary>Làm mới danh sách (bỏ từ khóa giữ nguyên).</summary>
    public void LamMoi() => TaiDanhSach();

    /// <summary>Đổ loại sách đang chọn lên ô nhập (chỉ khi đang xem).</summary>
    public void ChonDong()
    {
        if (_cheDo != CheDo.Xem)
        {
            return;
        }

        LoaiSach? loai = LoaiDangChon();
        _view.TenLoai = loai?.TenLoai ?? string.Empty;
        _view.MoTa = loai?.MoTa ?? string.Empty;
    }

    /// <summary>Bắt đầu thêm mới: xóa ô nhập, mở chế độ nhập.</summary>
    public void Them()
    {
        _view.XoaONhap();
        DatCheDo(CheDo.Them);
    }

    /// <summary>Bắt đầu sửa loại đang chọn.</summary>
    public void Sua()
    {
        if (LoaiDangChon() == null)
        {
            _view.HienLoi(null, "Vui lòng chọn loại sách cần sửa.");
            return;
        }

        DatCheDo(CheDo.Sua);
    }

    /// <summary>Lưu thêm mới hoặc sửa; lỗi nghiệp vụ được gắn vào đúng ô nhập.</summary>
    public void Luu()
    {
        var loai = new LoaiSach
        {
            MaLoai = _cheDo == CheDo.Sua ? LoaiDangChon()?.MaLoai ?? 0 : 0,
            TenLoai = _view.TenLoai,
            MoTa = _view.MoTa
        };

        try
        {
            if (_cheDo == CheDo.Them)
            {
                _bll.Them(loai);
                _view.HienThongBao($"Đã thêm loại sách \"{loai.TenLoai}\".");
            }
            else
            {
                _bll.Sua(loai);
                _view.HienThongBao($"Đã cập nhật loại sách \"{loai.TenLoai}\".");
            }

            DatCheDo(CheDo.Xem);
            TaiDanhSach();
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Hủy thao tác thêm/sửa, trả lại dữ liệu dòng đang chọn.</summary>
    public void Huy()
    {
        _view.XoaONhap();
        DatCheDo(CheDo.Xem);
        ChonDong();
    }

    /// <summary>
    /// Xóa loại đang chọn sau khi xác nhận. Loại đang có sách bị BLL chặn kèm số sách (SP-04, TC06).
    /// </summary>
    public void Xoa()
    {
        LoaiSach? loai = LoaiDangChon();
        if (loai == null)
        {
            _view.HienLoi(null, "Vui lòng chọn loại sách cần xóa.");
            return;
        }

        if (!_view.XacNhan($"Xóa loại sách \"{loai.TenLoai}\"?"))
        {
            return;
        }

        try
        {
            _bll.Xoa(loai);
            _view.HienThongBao($"Đã xóa loại sách \"{loai.TenLoai}\".");
            TaiDanhSach();
        }
        catch (Exception ex)
        {
            XuLyLoi(ex);
        }
    }

    /// <summary>Loại sách ứng với dòng đang chọn.</summary>
    private LoaiSach? LoaiDangChon()
    {
        int? ma = _view.MaLoaiDangChon;
        return ma == null ? null : _danhSach.FirstOrDefault(l => l.MaLoai == ma);
    }

    private void DatCheDo(CheDo cheDo)
    {
        _cheDo = cheDo;
        _view.DatCheDo(cheDo != CheDo.Xem);
    }

    /// <summary>Chuyển exception thành lời gọi HienLoi của View (gắn đúng trường khi có FieldName).</summary>
    private void XuLyLoi(Exception ex)
    {
        (string? truong, string thongDiep) = ThongDiepLoi.Tu(ex);
        _view.HienLoi(truong, thongDiep);
    }
}
