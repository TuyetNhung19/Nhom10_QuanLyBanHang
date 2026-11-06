// File: QuanLyBanSach.GUI/Forms/FrmMain.cs
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Form chính MDI: menu, thanh công cụ, thanh trạng thái, phân quyền theo vai trò.
/// </summary>
public partial class FrmMain : Form
{
    /// <summary>Đã hỏi xác nhận thoát/đăng xuất (tránh hỏi lại trong FormClosing).</summary>
    private bool _daXacNhanDong;

    /// <summary>Khởi tạo Form chính.</summary>
    public FrmMain()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
    }

    /// <summary>
    /// true nếu Form đóng do người dùng chọn Đăng xuất → Program quay lại màn hình đăng nhập.
    /// </summary>
    public bool DangXuat { get; private set; }

    private void FrmMain_Load(object? sender, EventArgs e)
    {
        Text = "BookSPT – " + Helpers.GiaoDien.TenCuaHang; // tên nhà sách lấy từ App.config
        HienThongTinNguoiDung();
        PhanQuyen();
        CapNhatDongHo();
        tmrDongHo.Start();
        MoManHinhMacDinh();
    }

    /// <summary>
    /// Mở Form con trong MDI: đã mở thì đưa lên trước, chưa mở thì tạo mới với MdiParent = this.
    /// </summary>
    /// <typeparam name="T">Loại Form con.</typeparam>
    /// <param name="phongToToiDa">true: WindowState = Maximized.</param>
    private T MoFormCon<T>(bool phongToToiDa = true) where T : Form, new()
    {
        T? daMo = MdiChildren.OfType<T>().FirstOrDefault();
        if (daMo != null)
        {
            if (daMo.WindowState == FormWindowState.Minimized)
            {
                daMo.WindowState = phongToToiDa ? FormWindowState.Maximized : FormWindowState.Normal;
            }

            daMo.Activate();
            return daMo;
        }

        var formMoi = new T { MdiParent = this };
        if (phongToToiDa)
        {
            formMoi.WindowState = FormWindowState.Maximized;
        }

        formMoi.Show();
        return formMoi;
    }

    /// <summary>Sau đăng nhập, Thu ngân/Quản lý tự mở Bảng điều khiển (ĐT5, TC22); Admin không có.</summary>
    private void MoManHinhMacDinh()
    {
        if (PhienDangNhap.LaThuNgan || PhienDangNhap.LaQuanLy)
        {
            MoFormCon<FrmBangDieuKhien>();
        }
    }

    /// <summary>Hiện họ tên, vai trò lên thanh trạng thái.</summary>
    private void HienThongTinNguoiDung()
    {
        TaiKhoan? tk = PhienDangNhap.TaiKhoanHienTai;
        tslNguoiDung.Text = "Người dùng: " + (tk?.HoTen ?? "(chưa đăng nhập)");
        tslVaiTro.Text = "Vai trò: " + (tk?.TenVaiTro ?? string.Empty);
        tslKetNoi.Text = "CSDL: Đã kết nối";
    }

    /// <summary>
    /// Phân quyền theo ma trận đặc tả 2.2 (Available = false để ẩn hẳn mục không được dùng):
    /// - Thu ngân: Đổi MK, Đăng xuất, Khách hàng, Lập hóa đơn, Tra cứu, Bảng điều khiển, Báo cáo cuối ngày (TC03).
    /// - Quản lý: mọi chức năng trừ Tài khoản.
    /// - Admin: Đổi MK, Đăng xuất, Tài khoản, Cửa sổ, Trợ giúp.
    /// BLL vẫn kiểm tra lại vai trò ở các thao tác nhạy cảm.
    /// </summary>
    private void PhanQuyen()
    {
        bool thuNgan = PhienDangNhap.LaThuNgan;
        bool quanLy = PhienDangNhap.LaQuanLy;
        bool admin = PhienDangNhap.LaAdmin;
        bool banHang = thuNgan || quanLy;

        // Danh mục
        mnuLoaiSach.Available = quanLy;
        mnuSach.Available = quanLy;
        mnuKhachHang.Available = banHang;
        mnuKhuyenMai.Available = quanLy;

        // Bán hàng
        mnuLapHoaDon.Available = banHang;
        mnuTraCuuHoaDon.Available = banHang;

        // Báo cáo
        mnuBangDieuKhien.Available = banHang;
        mnuThongKe.Available = quanLy;
        mnuBaoCaoCuoiNgay.Available = banHang;

        // Quản trị
        mnuTaiKhoan.Available = admin;

        // Thanh công cụ
        tsbBanHang.Available = banHang;
        tsbTraCuu.Available = banHang;
        tsbKhachHang.Available = banHang;
        tsbSach.Available = quanLy;
        tsbThongKe.Available = quanLy;
        tssDangXuat.Available = banHang;

        // Ẩn menu cha khi mọi mục con đều bị ẩn
        foreach (ToolStripMenuItem menuCha in mnuChinh.Items.OfType<ToolStripMenuItem>())
        {
            if (menuCha.DropDownItems.Count > 0 && menuCha != mnuCuaSo)
            {
                menuCha.Available = menuCha.DropDownItems.OfType<ToolStripMenuItem>().Any(muc => muc.Available);
            }
        }
    }

    // ===================== Hệ thống =====================

    private void mnuDoiMatKhau_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmDoiMatKhau();
        frm.ShowDialog(this);
    }

    private void mnuDangXuat_Click(object? sender, EventArgs e)
    {
        if (!XacNhanDong(laDangXuat: true))
        {
            return;
        }

        _daXacNhanDong = true;
        DangXuat = true;
        Close();

        // Một Form con có thể từ chối đóng → Form chính vẫn mở, trả lại trạng thái
        if (!IsDisposed && Visible)
        {
            _daXacNhanDong = false;
            DangXuat = false;
        }
    }

    private void mnuThoat_Click(object? sender, EventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Đóng Form chính (thoát hoặc đăng xuất): hỏi xác nhận, hủy đơn treo (BH-02), đăng xuất khỏi BLL.
    /// </summary>
    private void FrmMain_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (e.Cancel)
        {
            return;
        }

        if (!_daXacNhanDong && e.CloseReason != CloseReason.WindowsShutDown && !XacNhanDong(laDangXuat: false))
        {
            e.Cancel = true;
            return;
        }

        tmrDongHo.Stop();
        new DonTreoBLL().HuyTatCa();
        new TaiKhoanBLL().DangXuat();
    }

    /// <summary>
    /// Hỏi xác nhận trước khi đăng xuất/thoát. Còn đơn treo thì cảnh báo đơn treo sẽ bị hủy (BH-02).
    /// </summary>
    private static bool XacNhanDong(bool laDangXuat)
    {
        int soDonTreo = new DonTreoBLL().Dem();
        if (soDonTreo > 0)
        {
            return ThongBao.XacNhan($"Còn {soDonTreo} đơn treo sẽ bị hủy, tiếp tục?");
        }

        return ThongBao.XacNhan(laDangXuat ? "Bạn có chắc muốn đăng xuất?" : "Bạn có chắc muốn thoát BookSPT?");
    }

    // ===================== Danh mục =====================

    private void mnuLoaiSach_Click(object? sender, EventArgs e) => MoFormCon<FrmLoaiSach>();

    private void mnuSach_Click(object? sender, EventArgs e) => MoFormCon<FrmSach>();

    private void mnuKhachHang_Click(object? sender, EventArgs e) => MoFormCon<FrmKhachHang>();

    private void mnuKhuyenMai_Click(object? sender, EventArgs e) => MoFormCon<FrmKhuyenMai>();

    // ===================== Bán hàng =====================

    private void mnuLapHoaDon_Click(object? sender, EventArgs e) => MoFormCon<FrmBanHang>();

    private void mnuTraCuuHoaDon_Click(object? sender, EventArgs e) => MoFormCon<FrmTraCuuHoaDon>();

    // ===================== Báo cáo =====================

    private void mnuBangDieuKhien_Click(object? sender, EventArgs e) => MoFormCon<FrmBangDieuKhien>();

    private void mnuThongKe_Click(object? sender, EventArgs e) => MoFormCon<FrmThongKe>();

    /// <summary>Form báo cáo cuối ngày nhỏ (chọn ngày/thu ngân) nên không phóng to.</summary>
    private void mnuBaoCaoCuoiNgay_Click(object? sender, EventArgs e) => MoFormCon<FrmBaoCaoCuoiNgay>(phongToToiDa: false);

    // ===================== Quản trị =====================

    private void mnuTaiKhoan_Click(object? sender, EventArgs e) => MoFormCon<FrmTaiKhoan>();

    // ===================== Cửa sổ =====================

    private void mnuXepChong_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.Cascade);

    private void mnuXepNgang_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.TileHorizontal);

    private void mnuXepDoc_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.TileVertical);

    private void mnuDongTatCa_Click(object? sender, EventArgs e)
    {
        foreach (Form con in MdiChildren)
        {
            con.Close();
        }
    }

    // ===================== Trợ giúp =====================

    private void mnuPhimTat_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmPhimTat();
        frm.ShowDialog(this);
    }

    private void mnuGioiThieu_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmGioiThieu();
        frm.ShowDialog(this);
    }

    // ===================== Đồng hồ =====================

    private void tmrDongHo_Tick(object? sender, EventArgs e)
    {
        CapNhatDongHo();
    }

    private void CapNhatDongHo()
    {
        tslThoiGian.Text = DateTime.Now.ToString("HH:mm:ss  dd/MM/yyyy");
    }
}
