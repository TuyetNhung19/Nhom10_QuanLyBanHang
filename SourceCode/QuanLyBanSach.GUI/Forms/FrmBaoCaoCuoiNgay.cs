// File: QuanLyBanSach.GUI/Forms/FrmBaoCaoCuoiNgay.cs
using System.Globalization;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;
using QuanLyBanSach.GUI.Printing;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Báo cáo cuối ngày (ĐT6): chọn ngày + thu ngân, xem trước (PrintPreviewDialog) hoặc in ra trang A4.
/// Thu ngân: ô thu ngân khóa cố định là chính mình (BLL cũng ép lại); Quản lý: "Tất cả" hoặc từng thu ngân.
/// </summary>
public partial class FrmBaoCaoCuoiNgay : Form
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    private readonly ThongKeBLL _thongKeBll = new();
    private readonly TaiKhoanBLL _taiKhoanBll = new();
    private bool _dangNap;

    /// <summary>Khởi tạo Form báo cáo cuối ngày.</summary>
    public FrmBaoCaoCuoiNgay()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
    }

    private void FrmBaoCaoCuoiNgay_Load(object? sender, EventArgs e)
    {
        _dangNap = true;
        dtpNgay.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1); // không chọn ngày tương lai
        dtpNgay.Value = DateTime.Today;
        try
        {
            var ds = new List<TaiKhoan>();
            TaiKhoan? toi = PhienDangNhap.TaiKhoanHienTai;
            if (PhienDangNhap.LaThuNgan && toi != null)
            {
                ds.Add(toi);
                cboThuNgan.Enabled = false;
            }
            else
            {
                ds.Add(new TaiKhoan { MaTK = 0, HoTen = "Tất cả thu ngân" });
                ds.AddRange(_taiKhoanBll.LayDanhSachNguoiBan());
            }

            cboThuNgan.DisplayMember = nameof(TaiKhoan.HoTen);
            cboThuNgan.ValueMember = nameof(TaiKhoan.MaTK);
            cboThuNgan.DataSource = ds;
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
        finally
        {
            _dangNap = false;
        }

        HienTomTat();
    }

    private void boLoc_Changed(object? sender, EventArgs e)
    {
        if (!_dangNap)
        {
            HienTomTat();
        }
    }

    /// <summary>Mã thu ngân đang chọn; 0/"Tất cả" → null.</summary>
    private int? MaTKDangChon => cboThuNgan.SelectedValue is int ma && ma > 0 ? ma : null;

    /// <summary>Lấy số liệu báo cáo; lỗi thì thông báo và trả null.</summary>
    private BaoCaoCuoiNgay? LayBaoCao()
    {
        errBaoCao.Clear();
        try
        {
            return _thongKeBll.BaoCaoCuoiNgay(dtpNgay.Value.Date, MaTKDangChon);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex, errBaoCao, this);
            return null;
        }
    }

    /// <summary>Hiện tóm tắt số liệu ngay trên Form để thu ngân đối chiếu nhanh trước khi in.</summary>
    private void HienTomTat()
    {
        BaoCaoCuoiNgay? bc = LayBaoCao();
        if (bc == null)
        {
            return;
        }

        lblSoHoaDon.Text = bc.SoHoaDon.ToString("#,##0", VanHoaVN);
        lblSoHoaDonHuy.Text = bc.SoHoaDonHuy.ToString("#,##0", VanHoaVN);
        lblTongTamTinh.Text = Tien(bc.TongTamTinh);
        lblTongGiamGia.Text = Tien(bc.TongGiamGia);
        lblDoanhThuThuc.Text = Tien(bc.DoanhThuThuc);
        lblTienMat.Text = Tien(bc.TienMatPhaiCo);
    }

    /// <summary>Tạo bộ in; null nếu máy chưa có máy in nào (PrintPreview cũng cần máy in).</summary>
    private BaoCaoCuoiNgayPrinter? TaoBoIn()
    {
        BaoCaoCuoiNgay? bc = LayBaoCao();
        if (bc == null)
        {
            return null;
        }

        var boIn = new BaoCaoCuoiNgayPrinter(bc, PhienDangNhap.TaiKhoanHienTai?.HoTen ?? string.Empty);
        if (!boIn.TaiLieu.PrinterSettings.IsValid)
        {
            boIn.Dispose();
            ThongBao.Loi("Máy chưa cài máy in nào (kể cả Microsoft Print to PDF) nên không thể xem trước/in.");
            return null;
        }

        return boIn;
    }

    private void btnXem_Click(object? sender, EventArgs e)
    {
        try
        {
            using BaoCaoCuoiNgayPrinter? boIn = TaoBoIn();
            if (boIn == null)
            {
                return;
            }

            using var xemTruoc = new PrintPreviewDialog
            {
                Document = boIn.TaiLieu,
                Width = 900,
                Height = 900,
                StartPosition = FormStartPosition.CenterParent,
                UseAntiAlias = true,
                Text = "Xem trước – Báo cáo cuối ngày"
            };
            xemTruoc.ShowDialog(this);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    private void btnIn_Click(object? sender, EventArgs e)
    {
        try
        {
            using BaoCaoCuoiNgayPrinter? boIn = TaoBoIn();
            if (boIn == null)
            {
                return;
            }

            using var hopIn = new PrintDialog { Document = boIn.TaiLieu, UseEXDialog = true, AllowSomePages = false };
            if (hopIn.ShowDialog(this) == DialogResult.OK)
            {
                boIn.TaiLieu.Print();
                ThongBao.ThanhCong("Đã gửi báo cáo cuối ngày tới máy in.");
            }
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    private static string Tien(decimal so) => so.ToString("#,##0", VanHoaVN) + "đ";
}
