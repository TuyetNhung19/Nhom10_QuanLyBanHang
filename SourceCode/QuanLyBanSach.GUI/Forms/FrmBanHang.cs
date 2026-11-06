// File: QuanLyBanSach.GUI/Forms/FrmBanHang.cs
using System.ComponentModel;
using System.Globalization;
using System.Media;
using System.Text;
using Microsoft.Data.SqlClient;
using QuanLyBanSach.BLL;
using QuanLyBanSach.BLL.KhuyenMai;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Màn hình bán hàng tại quầy (đặc tả 3.4, 6.3) – phần LÕI.
/// Partial class chia nhiều file:
///   FrmBanHang.cs          – lõi: nạp dữ liệu, thêm sách, tính tiền, chọn khách, lưu.
///   FrmBanHang.Designer.cs – toàn bộ control (kể cả của ĐT2, ĐT4).
///   FrmBanHang.PhimTat.cs  – ĐT1: phím tắt, nhập mã + Enter, nút mệnh giá.
///   (Prompt sau) FrmBanHang.DonTreo.cs, FrmBanHang.KhachQuen.cs cài đặt các partial method bên dưới.
/// </summary>
public partial class FrmBanHang : Form
{
    private static readonly CultureInfo VanHoaVN = new("vi-VN");

    private readonly HoaDonBLL _hoaDonBll = new();
    private readonly SachBLL _sachBll = new();
    private readonly KhachHangBLL _khachHangBll = new();
    private readonly KhuyenMaiBLL _khuyenMaiBll = new();
    private readonly DonTreoBLL _donTreoBll = new();

    /// <summary>Danh sách dòng của đơn đang lập (HoaDonBLL thao tác trực tiếp trên List này).</summary>
    private readonly List<ChiTietHoaDon> _dsDong = new();

    /// <summary>BindingList bọc _dsDong để DataGridView hiển thị qua bsChiTiet.</summary>
    private readonly BindingList<ChiTietHoaDon> _chiTiet;

    /// <summary>Sách đang bán (nguồn của lưới chọn sách, lọc tại chỗ bằng LINQ).</summary>
    private List<Sach> _dsSach = new();

    private KhachHang? _khach;
    private KetQuaKhuyenMai _ketQuaKM = new();
    private bool _dangDongBoSoLuong;

    /// <summary>Khởi tạo màn hình bán hàng.</summary>
    public FrmBanHang()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        _chiTiet = new BindingList<ChiTietHoaDon>(_dsDong);
        bsChiTiet.DataSource = _chiTiet;

        colSachGia.DefaultCellStyle.FormatProvider = VanHoaVN;
        colCtDonGia.DefaultCellStyle.FormatProvider = VanHoaVN;
        colCtThanhTien.DefaultCellStyle.FormatProvider = VanHoaVN;
        colGyGia.DefaultCellStyle.FormatProvider = VanHoaVN;
    }

    // ===================== Partial method – cài đặt ở các file khác (Prompt 10) =====================
    // Chưa có phần cài đặt thì trình biên dịch tự bỏ lời gọi, build vẫn thành công.

    /// <summary>ĐT2: khởi tạo phần đơn treo khi mở Form.</summary>
    partial void KhoiTaoDonTreo();

    /// <summary>ĐT2: treo đơn đang lập (F8).</summary>
    partial void TreoDonHienTai();

    /// <summary>ĐT2: mở danh sách đơn treo để gọi lại.</summary>
    partial void MoDanhSachDonTreo();

    /// <summary>ĐT4: sau khi chọn / bỏ khách → hiện hồ sơ khách quen.</summary>
    partial void SauKhiChonKhach(KhachHang? kh);

    /// <summary>ĐT4: sau khi thêm một sách vào đơn → gợi ý sách thường mua kèm.</summary>
    partial void SauKhiThemSach(string maSach);

    // ===================== Nạp dữ liệu =====================

    private void FrmBanHang_Load(object? sender, EventArgs e)
    {
        lblThuNgan.Text = "Thu ngân: " + (PhienDangNhap.TaiKhoanHienTai?.HoTen ?? string.Empty);
        lblNgayLap.Text = "Ngày: " + DateTime.Today.ToString("dd/MM/yyyy");
        TaoNutMenhGia();

        try
        {
            CapNhatMaDuKien();
            TaiDanhSachSach();
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }

        KhoiTaoDonTreo();
        CapNhatNutDonTreo();
        DatKhach(null);
        TinhLaiTien();
    }

    private void FrmBanHang_Shown(object? sender, EventArgs e) => FocusMaSach();

    /// <summary>Hiện mã hóa đơn dự kiến (mã chính thức do sp_LapHoaDon cấp khi lưu).</summary>
    private void CapNhatMaDuKien()
    {
        lblMaHD.Text = "Mã HĐ: " + _hoaDonBll.TaoMaDuKien();
    }

    /// <summary>Nạp lại danh sách sách đang bán (gọi khi mở Form và sau khi lưu hóa đơn để cập nhật tồn).</summary>
    private void TaiDanhSachSach()
    {
        _dsSach = _sachBll.TimKiem(null, null, chiDangBan: true);
        LocDanhSachSach();
    }

    /// <summary>
    /// Lọc sách tại chỗ bằng LINQ theo mã, ISBN, tên, tác giả – không phân biệt hoa thường và dấu
    /// (kể cả đ/Đ), nên gõ "dac nhan tam" vẫn ra "Đắc nhân tâm".
    /// </summary>
    private void LocDanhSachSach()
    {
        string tuKhoa = BoDau(txtTimSach.Text.Trim());
        List<Sach> ketQua = tuKhoa.Length == 0
            ? _dsSach
            : _dsSach.Where(s => BoDau(s.MaSach).Contains(tuKhoa)
                                 || BoDau(s.ISBN ?? string.Empty).Contains(tuKhoa)
                                 || BoDau(s.TenSach).Contains(tuKhoa)
                                 || BoDau(s.TacGia ?? string.Empty).Contains(tuKhoa))
                     .ToList();
        dgvSach.DataSource = ketQua;
    }

    /// <summary>Bỏ dấu tiếng Việt và chuyển chữ thường để so khớp không dấu.</summary>
    private static string BoDau(string chuoi)
    {
        string tach = chuoi.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(tach.Length);
        foreach (char c in tach)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC).Replace('đ', 'd').Replace('Đ', 'D').ToLowerInvariant();
    }

    // ===================== Chọn sách =====================

    private Sach? SachDangChonTrongLuoi => dgvSach.CurrentRow?.DataBoundItem as Sach;

    private void txtTimSach_TextChanged(object? sender, EventArgs e) => LocDanhSachSach();

    /// <summary>Enter trong ô tìm: thêm sách đang chọn (hoặc dòng đầu) với số lượng nudSoLuongThem.</summary>
    private void txtTimSach_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            Sach? s = SachDangChonTrongLuoi ?? (dgvSach.Rows.Count > 0 ? dgvSach.Rows[0].DataBoundItem as Sach : null);
            if (s != null)
            {
                ThemSach(s, (int)nudSoLuongThem.Value);
            }
        }
        else if (e.KeyCode == Keys.Down && dgvSach.Rows.Count > 0)
        {
            dgvSach.Focus();
        }
    }

    private void dgvSach_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && dgvSach.Rows[e.RowIndex].DataBoundItem is Sach s)
        {
            ThemSach(s, 1);
        }
    }

    private void btnThemVaoDon_Click(object? sender, EventArgs e)
    {
        Sach? s = SachDangChonTrongLuoi;
        if (s == null)
        {
            ThongBao.CanhBao("Vui lòng chọn sách trong danh sách bên trái.");
            FocusMaSach();
            return;
        }

        ThemSach(s, (int)nudSoLuongThem.Value);
    }

    /// <summary>Sách hết tồn hiển thị chữ xám.</summary>
    private void dgvSach_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && e.CellStyle != null
            && dgvSach.Rows[e.RowIndex].DataBoundItem is Sach s && s.SoLuongTon <= 0)
        {
            e.CellStyle.ForeColor = Color.Gray;
        }
    }

    /// <summary>
    /// Thêm sách vào đơn qua HoaDonBLL.ThemSachVaoDon: đã có thì tăng số lượng; vượt tồn thì cảnh báo,
    /// không thêm (HD-02, TC08). Sau đó gọi SauKhiThemSach (ĐT4) và tính lại tiền.
    /// </summary>
    private void ThemSach(Sach s, int soLuong)
    {
        try
        {
            ChiTietHoaDon dong = _hoaDonBll.ThemSachVaoDon(_dsDong, s, soLuong);
            CapNhatLuoiChiTiet(dong.MaSach);
            nudSoLuongThem.Value = 1;
            SauKhiThemSach(s.MaSach);
        }
        catch (BusinessException ex)
        {
            SystemSounds.Exclamation.Play();
            ThongBao.CanhBao(ex.Message);
        }
        finally
        {
            TinhLaiTien();
            FocusMaSach();
        }
    }

    // ===================== Chi tiết hóa đơn =====================

    private ChiTietHoaDon? DongDangChon => bsChiTiet.Current as ChiTietHoaDon;

    /// <summary>Làm mới lưới chi tiết sau khi BLL sửa _dsDong và chọn lại dòng có mã cho trước.</summary>
    private void CapNhatLuoiChiTiet(string? maSachChon)
    {
        _chiTiet.ResetBindings();
        if (maSachChon != null)
        {
            int viTri = _dsDong.FindIndex(ct => ct.MaSach == maSachChon);
            if (viTri >= 0)
            {
                bsChiTiet.Position = viTri;
            }
        }

        DongBoSoLuongDong();
    }

    private void dgvChiTiet_SelectionChanged(object? sender, EventArgs e) => DongBoSoLuongDong();

    /// <summary>nudSoLuongDong luôn hiển thị số lượng của dòng đang chọn.</summary>
    private void DongBoSoLuongDong()
    {
        _dangDongBoSoLuong = true;
        ChiTietHoaDon? dong = DongDangChon;
        nudSoLuongDong.Enabled = dong != null;
        btnXoaDong.Enabled = dong != null;
        nudSoLuongDong.Value = dong == null ? 0 : Math.Min(dong.SoLuong, (int)nudSoLuongDong.Maximum);
        _dangDongBoSoLuong = false;
    }

    private void nudSoLuongDong_ValueChanged(object? sender, EventArgs e)
    {
        if (!_dangDongBoSoLuong && DongDangChon is { } dong && dong.SoLuong != (int)nudSoLuongDong.Value)
        {
            DoiSoLuong(dong.MaSach, (int)nudSoLuongDong.Value);
        }
    }

    /// <summary>Đổi số lượng một dòng qua BLL (0 = xóa dòng, vượt tồn = cảnh báo).</summary>
    private void DoiSoLuong(string maSach, int soLuongMoi)
    {
        try
        {
            _hoaDonBll.CapNhatSoLuong(_dsDong, maSach, soLuongMoi);
        }
        catch (BusinessException ex)
        {
            SystemSounds.Exclamation.Play();
            ThongBao.CanhBao(ex.Message);
        }
        finally
        {
            CapNhatLuoiChiTiet(maSach);
            TinhLaiTien();
        }
    }

    /// <summary>Tăng/giảm số lượng dòng đang chọn (phím +/−).</summary>
    private void TangGiamSoLuong(int buoc)
    {
        if (DongDangChon is { } dong)
        {
            DoiSoLuong(dong.MaSach, dong.SoLuong + buoc);
        }
    }

    private void btnXoaDong_Click(object? sender, EventArgs e)
    {
        XoaDongDangChon();
        FocusMaSach();
    }

    /// <summary>Xóa dòng đang chọn (Delete).</summary>
    private void XoaDongDangChon()
    {
        if (DongDangChon is { } dong)
        {
            DoiSoLuong(dong.MaSach, 0);
        }
    }

    /// <summary>Dòng vượt tồn (ví dụ đơn treo gọi lại khi sách đã bán bớt – TC21) tô đỏ.</summary>
    private void dgvChiTiet_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && e.CellStyle != null
            && dgvChiTiet.Rows[e.RowIndex].DataBoundItem is ChiTietHoaDon dong && dong.SoLuong > dong.SoLuongTon)
        {
            e.CellStyle.BackColor = Color.MistyRose;
            e.CellStyle.ForeColor = Color.Red;
        }
    }

    // ===================== Tính tiền =====================

    /// <summary>
    /// Tính lại toàn bộ tiền sau MỌI thay đổi dòng hoặc tiền khách đưa: khuyến mãi tốt nhất do
    /// KhuyenMaiBLL tự chọn (thu ngân không có ô nhập tiền giảm – KM-01), tạm tính, giảm, tổng,
    /// dòng nhắc mua thêm (ĐT3), tiền thối. Bật Lưu khi có dòng (HD-01) và đủ tiền (HD-03, TC09).
    /// </summary>
    /// <param name="tienKhachDuaTam">Số tiền đang gõ dở trong ô tiền khách đưa (chưa xác nhận); null = dùng Value.</param>
    private void TinhLaiTien(decimal? tienKhachDuaTam = null)
    {
        decimal tamTinh = HoaDonBLL.TinhTamTinh(_dsDong);

        try
        {
            _ketQuaKM = _khuyenMaiBll.ChonKhuyenMaiTotNhat(_dsDong, DateTime.Today);
        }
        catch (Exception ex) when (ex is SqlException || ex is BusinessException || HeThongBLL.LaLoiKetNoi(ex))
        {
            _ketQuaKM = new KetQuaKhuyenMai();
        }

        decimal giam = _ketQuaKM.TienGiam;
        decimal tong = tamTinh - giam;
        decimal tienThoi = HoaDonBLL.TinhTienThoi(tong, tienKhachDuaTam ?? nudTienKhachDua.Value);

        lblTamTinh.Text = Tien(tamTinh);
        lblGiamGia.Text = giam > 0 ? "−" + Tien(giam) : Tien(0);
        lblTenKM.Text = _ketQuaKM.CoApDung ? "KM: " + _ketQuaKM.KhuyenMaiApDung!.TenKM : "Không có khuyến mãi";
        lblNhacKM.Text = _ketQuaKM.LoiNhac == null ? string.Empty : "💡 " + _ketQuaKM.LoiNhac;
        lblTongThanhToan.Text = Tien(tong);
        lblSoCuon.Text = $"{_dsDong.Sum(ct => ct.SoLuong)} cuốn";

        if (_dsDong.Count == 0)
        {
            lblTienThoi.Text = Tien(0);
            lblTienThoi.ForeColor = Color.ForestGreen;
        }
        else if (tienThoi >= 0)
        {
            lblTienThoi.Text = Tien(tienThoi);
            lblTienThoi.ForeColor = Color.ForestGreen;
        }
        else
        {
            lblTienThoi.Text = "Còn thiếu " + Tien(-tienThoi);
            lblTienThoi.ForeColor = Color.Red;
        }

        bool coDong = _dsDong.Count > 0;
        btnLuu.Enabled = coDong;
        btnLuuVaIn.Enabled = coDong;
        btnTreoDon.Enabled = coDong;
    }

    private void nudTienKhachDua_ValueChanged(object? sender, EventArgs e) => TinhLaiTien();

    /// <summary>
    /// NumericUpDown chỉ cập nhật Value khi rời ô hoặc bấm Enter. Để tiền thối thay đổi NGAY khi gõ,
    /// đọc tạm phần chữ số đang gõ và tính lại (không gán Value để không làm nhảy con trỏ).
    /// </summary>
    private void nudTienKhachDua_KeyUp(object? sender, KeyEventArgs e)
    {
        string chuSo = new(nudTienKhachDua.Text.Where(char.IsDigit).ToArray());
        TinhLaiTien(decimal.TryParse(chuSo, out decimal tien) ? tien : 0);
    }

    /// <summary>Định dạng "225.900đ".</summary>
    private static string Tien(decimal soTien) => soTien.ToString("#,##0", VanHoaVN) + "đ";

    // ===================== Khách hàng =====================

    private void btnTimKhach_Click(object? sender, EventArgs e) => ChonKhachTheoSDT();

    private void txtSDTKhach_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            ChonKhachTheoSDT();
        }
    }

    private void txtSDTKhach_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    /// <summary>
    /// Chọn khách theo SĐT: có thì gắn ngay; chưa có thì hỏi "Thêm khách mới?" và mở FrmKhachHang ở chế độ chọn.
    /// Ô SĐT trống thì mở danh sách khách để chọn.
    /// </summary>
    private void ChonKhachTheoSDT()
    {
        string sdt = txtSDTKhach.Text.Trim();
        try
        {
            KhachHang? kh = sdt.Length == 0 ? null : _khachHangBll.LayTheoSDT(sdt);

            if (kh == null)
            {
                if (sdt.Length > 0 && !ThongBao.XacNhan($"Chưa có khách hàng có SĐT {sdt}. Thêm khách mới?"))
                {
                    FocusMaSach();
                    return;
                }

                using var frm = new FrmKhachHang(sdt.Length > 0 ? sdt : null, cheDoChon: true);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    kh = frm.KhachDaChon;
                }
            }

            if (kh != null)
            {
                DatKhach(kh);
            }
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }

        FocusMaSach();
    }

    private void btnKhachLe_Click(object? sender, EventArgs e)
    {
        DatKhach(null);
        FocusMaSach();
    }

    /// <summary>Gắn khách cho đơn (null = khách lẻ) và báo cho phần hồ sơ khách quen (ĐT4).</summary>
    private void DatKhach(KhachHang? kh)
    {
        _khach = kh;
        lblTenKhach.Text = kh == null ? "Khách lẻ" : $"{kh.HoTen} – {kh.SDT}";
        txtSDTKhach.Text = kh?.SDT ?? string.Empty;
        btnKhachLe.Enabled = kh != null;
        SauKhiChonKhach(kh);
    }

    // ===================== Lưu hóa đơn =====================

    private void btnLuu_Click(object? sender, EventArgs e) => LuuHoaDon(inSauKhiLuu: false);

    private void btnLuuVaIn_Click(object? sender, EventArgs e) => LuuHoaDon(inSauKhiLuu: true);

    /// <summary>
    /// Lưu hóa đơn qua HoaDonBLL.LuuHoaDon (BLL tải lại giá/tồn, tính lại khuyến mãi, gọi sp_LapHoaDon).
    /// - Thành công: báo mã hóa đơn, (tùy chọn) mở xem trước bản in, làm mới đơn.
    /// - BusinessException (máy khác vừa bán hết, giá đổi...): báo lỗi, tải lại tồn/giá các dòng.
    /// - Mất kết nối: báo lỗi và GIỮ NGUYÊN đơn trên màn hình để lưu lại (TC10).
    /// </summary>
    private void LuuHoaDon(bool inSauKhiLuu)
    {
        if (_dsDong.Count == 0)
        {
            SystemSounds.Beep.Play();
            FocusMaSach();
            return;
        }

        var hd = new HoaDon
        {
            MaKH = _khach?.MaKH,
            TenKhach = _khach?.HoTen ?? "Khách lẻ",
            TienKhachDua = nudTienKhachDua.Value,
            GhiChu = txtGhiChu.Text,
            ChiTiet = _dsDong,
            TamTinh = HoaDonBLL.TinhTamTinh(_dsDong)
        };

        try
        {
            UseWaitCursor = true;
            string maHD = _hoaDonBll.LuuHoaDon(hd);
            UseWaitCursor = false;

            ThongBao.ThanhCong($"Đã lưu hóa đơn {maHD}.\n\nTổng thanh toán: {Tien(hd.TongThanhToan)}\nTiền thối: {Tien(hd.TienThoi)}");

            if (inSauKhiLuu)
            {
                MoXemTruocIn(maHD);
            }

            LamMoiDon();
            TaiDanhSachSach();
        }
        catch (BusinessException ex)
        {
            UseWaitCursor = false;
            ThongBao.CanhBao(ex.Message);
            TaiLaiTonVaGia();
        }
        catch (Exception ex) when (ex is SqlException || HeThongBLL.LaLoiKetNoi(ex))
        {
            UseWaitCursor = false;
            ThongBao.Loi("Mất kết nối CSDL – hóa đơn CHƯA được lưu.\n" +
                         "Đơn vẫn được giữ nguyên trên màn hình; kiểm tra kết nối rồi bấm Lưu lại.\n\n" +
                         HeThongBLL.MoTaLoi(ex));
        }
        catch (Exception ex)
        {
            UseWaitCursor = false;
            ThongBao.XuLyNgoaiLe(ex);
        }
        finally
        {
            CapNhatLuoiChiTiet(DongDangChon?.MaSach);
            TinhLaiTien();
            FocusMaSach();
        }
    }

    /// <summary>
    /// Tải lại danh sách sách rồi cập nhật tồn hiện tại và giá bán hiện hành cho từng dòng của đơn
    /// (dòng vượt tồn sẽ tô đỏ để thu ngân sửa).
    /// </summary>
    private void TaiLaiTonVaGia()
    {
        try
        {
            TaiDanhSachSach();
            Dictionary<string, Sach> theoMa = _dsSach.ToDictionary(s => s.MaSach, StringComparer.OrdinalIgnoreCase);
            foreach (ChiTietHoaDon dong in _dsDong)
            {
                if (theoMa.TryGetValue(dong.MaSach, out Sach? s))
                {
                    dong.DonGia = s.GiaBan;
                    dong.SoLuongTon = s.SoLuongTon;
                    dong.SachDangBan = true;
                }
                else
                {
                    dong.SoLuongTon = 0;
                    dong.SachDangBan = false;
                }
            }
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    /// <summary>Mở xem trước bản in hóa đơn vừa lưu (dữ liệu đọc lại từ CSDL – TC13, TC16).</summary>
    private void MoXemTruocIn(string maHD)
    {
        using var frm = new FrmInHoaDon(maHD, banInLai: false);
        frm.ShowDialog(this);
    }

    // ===================== Làm mới, đơn treo, đóng Form =====================

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        if (_dsDong.Count > 0 && !ThongBao.XacNhan("Bỏ đơn đang lập và bắt đầu đơn mới?"))
        {
            FocusMaSach();
            return;
        }

        LamMoiDon();
    }

    /// <summary>Xóa đơn đang lập về trạng thái mới (khách lẻ, tiền khách đưa 0, mã dự kiến mới).</summary>
    private void LamMoiDon()
    {
        _dsDong.Clear();
        CapNhatLuoiChiTiet(null);
        DatKhach(null);
        nudTienKhachDua.Value = 0;
        nudSoLuongThem.Value = 1;
        txtGhiChu.Clear();
        txtTimSach.Clear();

        try
        {
            CapNhatMaDuKien();
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }

        CapNhatNutDonTreo();
        TinhLaiTien();
        FocusMaSach();
    }

    private void btnTreoDon_Click(object? sender, EventArgs e) => TreoDonHienTai();

    private void btnDonTreo_Click(object? sender, EventArgs e) => MoDanhSachDonTreo();

    /// <summary>Cập nhật chữ "Đơn treo (n)".</summary>
    private void CapNhatNutDonTreo()
    {
        btnDonTreo.Text = $"📋 Đơn treo ({_donTreoBll.Dem()})";
    }

    /// <summary>
    /// Đóng Form khi đơn đang có dòng hoặc còn đơn treo → xác nhận (BH-02); đồng ý thì bỏ đơn treo.
    /// Khi Form chính đang đóng (đăng xuất/thoát), FrmMain đã hỏi về đơn treo nên ở đây chỉ hỏi về đơn đang lập.
    /// </summary>
    private void FrmBanHang_FormClosing(object? sender, FormClosingEventArgs e)
    {
        bool formChinhDangDong = e.CloseReason == CloseReason.MdiFormClosing;
        int soDonTreo = formChinhDangDong ? 0 : _donTreoBll.Dem();

        if (_dsDong.Count == 0 && soDonTreo == 0)
        {
            return;
        }

        var noiDung = new StringBuilder("Đóng màn hình bán hàng?");
        if (_dsDong.Count > 0)
        {
            noiDung.Append($"\n- Đơn đang lập ({_dsDong.Count} dòng) sẽ bị bỏ.");
        }

        if (soDonTreo > 0)
        {
            noiDung.Append($"\n- Còn {soDonTreo} đơn treo sẽ bị hủy.");
        }

        if (!ThongBao.XacNhan(noiDung.ToString()))
        {
            e.Cancel = true;
            return;
        }

        if (soDonTreo > 0)
        {
            _donTreoBll.HuyTatCa();
        }
    }

    /// <summary>Đưa con trỏ về ô Mã sách/ISBN (luôn gọi sau mỗi thao tác).</summary>
    private void FocusMaSach()
    {
        txtMaSach.Focus();
        txtMaSach.SelectAll();
    }
}
