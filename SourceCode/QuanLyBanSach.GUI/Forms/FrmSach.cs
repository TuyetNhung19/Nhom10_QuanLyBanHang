// File: QuanLyBanSach.GUI/Forms/FrmSach.cs
using System.Globalization;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;
using QuanLyBanSach.GUI.Printing;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Quản lý sách (Quản lý): thêm, sửa, ẩn/hiện (không xóa cứng – SP-03), ảnh bìa,
/// lọc theo loại, tìm kiếm có độ trễ 300 ms, sắp xếp bằng LINQ, tô màu sách sắp hết.
/// </summary>
public partial class FrmSach : Form
{
    /// <summary>Chế độ làm việc của màn hình.</summary>
    private enum CheDo
    {
        Xem,
        Them,
        Sua
    }

    private static readonly StringComparer SoSanhTiengViet = StringComparer.Create(new CultureInfo("vi-VN"), ignoreCase: true);
    private static readonly Color MauSapHet = Color.MistyRose;
    private static readonly Color MauHetHang = Color.FromArgb(255, 199, 199);

    private readonly SachBLL _sachBll = new();
    private readonly LoaiSachBLL _loaiSachBll = new();
    private readonly Font _chuDam;
    private readonly Font _chuNghieng;

    private List<Sach> _dsSach = new();
    private CheDo _cheDo = CheDo.Xem;
    private Sach? _sachDangSua;
    private string? _duongDanAnhMoi;
    private bool _xoaAnh;
    private bool _dangNapDuLieu;

    /// <summary>Khởi tạo Form quản lý sách.</summary>
    public FrmSach()
    {
        InitializeComponent();
        nudGiaBan.Minimum = -nudGiaBan.Maximum;
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        colGiaBan.DefaultCellStyle.FormatProvider = new CultureInfo("vi-VN");
        _chuDam = new Font(dgvSach.Font, FontStyle.Bold);
        _chuNghieng = new Font(dgvSach.Font, FontStyle.Italic);
    }

    /// <summary>Sách ứng với dòng đang chọn; null nếu chưa chọn.</summary>
    private Sach? SachDangChon => dgvSach.CurrentRow?.DataBoundItem as Sach;

    // ===================== Nạp dữ liệu =====================

    private void FrmSach_Load(object? sender, EventArgs e)
    {
        _dangNapDuLieu = true;
        try
        {
            cboSapXep.SelectedIndex = 0;
            NapLoaiSach();
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
        finally
        {
            _dangNapDuLieu = false;
        }

        TaiDanhSach();
        DatCheDo(CheDo.Xem);
    }

    /// <summary>Đổ loại sách vào ComboBox nhập liệu và ComboBox lọc ("Tất cả" + các loại).</summary>
    private void NapLoaiSach()
    {
        List<LoaiSach> dsLoai = _loaiSachBll.LayDanhSach();

        cboLoaiSach.DisplayMember = nameof(LoaiSach.TenLoai);
        cboLoaiSach.ValueMember = nameof(LoaiSach.MaLoai);
        cboLoaiSach.DataSource = dsLoai.ToList();

        var dsLoc = new List<LoaiSach> { new() { MaLoai = 0, TenLoai = "Tất cả" } };
        dsLoc.AddRange(dsLoai);
        cboLocLoai.DisplayMember = nameof(LoaiSach.TenLoai);
        cboLocLoai.ValueMember = nameof(LoaiSach.MaLoai);
        cboLocLoai.DataSource = dsLoc;
    }

    /// <summary>
    /// Tìm sách trong CSDL theo từ khóa, loại, có lấy sách ẩn không (sp_TimSach), rồi sắp xếp và hiển thị.
    /// </summary>
    private void TaiDanhSach(string? maSachChon = null)
    {
        if (_dangNapDuLieu)
        {
            return;
        }

        try
        {
            int? maLoai = cboLocLoai.SelectedValue is int ma && ma > 0 ? ma : null;
            _dsSach = _sachBll.TimKiem(txtTimKiem.Text, maLoai, chiDangBan: !chkHienSachAn.Checked);
            HienThiDanhSach(maSachChon ?? SachDangChon?.MaSach);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    /// <summary>Sắp xếp danh sách hiện có bằng LINQ theo lựa chọn của cboSapXep rồi đổ lên lưới.</summary>
    private void HienThiDanhSach(string? maSachChon)
    {
        IEnumerable<Sach> daSapXep = cboSapXep.SelectedIndex switch
        {
            1 => _dsSach.OrderBy(s => s.GiaBan).ThenBy(s => s.TenSach, SoSanhTiengViet),
            2 => _dsSach.OrderByDescending(s => s.GiaBan).ThenBy(s => s.TenSach, SoSanhTiengViet),
            3 => _dsSach.OrderBy(s => s.SoLuongTon).ThenBy(s => s.TenSach, SoSanhTiengViet),
            _ => _dsSach.OrderBy(s => s.TenSach, SoSanhTiengViet)
        };

        List<Sach> ds = daSapXep.ToList();
        dgvSach.DataSource = ds;

        int soSapHet = ds.Count(s => s.TrangThai && s.SapHet);
        lblTongSo.Text = $"Tổng: {ds.Count} sách – {soSapHet} sách sắp hết";

        ChonDongTheoMa(maSachChon);
        HienSachLenONhap(SachDangChon);
        CapNhatNutTheoDong();
    }

    /// <summary>Ở chế độ xem: Sửa và Ẩn/Hiện chỉ bật khi đang chọn một sách.</summary>
    private void CapNhatNutTheoDong()
    {
        if (_cheDo == CheDo.Xem)
        {
            bool coDong = SachDangChon != null;
            btnSua.Enabled = coDong;
            btnAnHien.Enabled = coDong;
        }
    }

    /// <summary>Chọn lại dòng có mã sách cho trước (sau khi lưu / làm mới).</summary>
    private void ChonDongTheoMa(string? maSach)
    {
        if (maSach == null)
        {
            return;
        }

        foreach (DataGridViewRow dong in dgvSach.Rows)
        {
            if (dong.DataBoundItem is Sach s && s.MaSach == maSach)
            {
                dgvSach.CurrentCell = dong.Cells[colTenSach.Index];
                return;
            }
        }
    }

    // ===================== Lọc, tìm, sắp xếp =====================

    private void txtTimKiem_TextChanged(object? sender, EventArgs e)
    {
        // Chờ người dùng ngừng gõ 300 ms rồi mới truy vấn
        tmrTimKiem.Stop();
        tmrTimKiem.Start();
    }

    private void tmrTimKiem_Tick(object? sender, EventArgs e)
    {
        tmrTimKiem.Stop();
        TaiDanhSach();
    }

    private void cboLocLoai_SelectedIndexChanged(object? sender, EventArgs e) => TaiDanhSach();

    private void chkHienSachAn_CheckedChanged(object? sender, EventArgs e) => TaiDanhSach();

    private void cboSapXep_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_dangNapDuLieu)
        {
            HienThiDanhSach(SachDangChon?.MaSach);
        }
    }

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        string? maDangChon = SachDangChon?.MaSach;
        _dangNapDuLieu = true;
        try
        {
            NapLoaiSach();
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
        finally
        {
            _dangNapDuLieu = false;
        }

        TaiDanhSach(maDangChon);
    }

    // ===================== Lưới =====================

    private void dgvSach_SelectionChanged(object? sender, EventArgs e)
    {
        if (_cheDo == CheDo.Xem)
        {
            HienSachLenONhap(SachDangChon);
            CapNhatNutTheoDong();
        }
    }

    /// <summary>
    /// Tô màu dòng: sách ẩn chữ xám nghiêng; hết hàng nền đỏ nhạt; sắp hết nền MistyRose chữ đỏ đậm.
    /// </summary>
    private void dgvSach_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.CellStyle == null || dgvSach.Rows[e.RowIndex].DataBoundItem is not Sach s)
        {
            return;
        }

        if (!s.TrangThai)
        {
            e.CellStyle.ForeColor = Color.Gray;
            e.CellStyle.Font = _chuNghieng;
        }
        else if (s.SoLuongTon == 0)
        {
            e.CellStyle.BackColor = MauHetHang;
            e.CellStyle.ForeColor = Color.DarkRed;
            e.CellStyle.Font = _chuDam;
        }
        else if (s.SapHet)
        {
            e.CellStyle.BackColor = MauSapHet;
            e.CellStyle.ForeColor = Color.Red;
            e.CellStyle.Font = _chuDam;
        }
    }

    // ===================== Thêm / Sửa / Lưu / Hủy / Ẩn hiện =====================

    private void btnThem_Click(object? sender, EventArgs e)
    {
        try
        {
            XoaONhap();
            txtMaSach.Text = _sachBll.TaoMaSachMoi();
            _sachDangSua = null;
            DatCheDo(CheDo.Them);
            txtTenSach.Focus();
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    private void btnSua_Click(object? sender, EventArgs e)
    {
        _sachDangSua = SachDangChon;
        if (_sachDangSua == null)
        {
            ThongBao.CanhBao("Vui lòng chọn sách cần sửa.");
            return;
        }

        DatCheDo(CheDo.Sua);
        txtTenSach.Focus();
    }

    /// <summary>
    /// Lưu sách: ghi CSDL trước, sau đó mới chép ảnh bìa (tránh ghi đè ảnh của sách khác
    /// khi mã bị trùng – TC04) rồi cập nhật tên file ảnh.
    /// </summary>
    private void btnLuu_Click(object? sender, EventArgs e)
    {
        errSach.Clear();
        Sach s = DocONhap();

        try
        {
            if (_cheDo == CheDo.Them)
            {
                _sachBll.Them(s);
            }
            else
            {
                _sachBll.Sua(s);
            }

            if (_duongDanAnhMoi != null)
            {
                s.AnhBia = _sachBll.LuuAnhBia(_duongDanAnhMoi, s.MaSach);
                _sachBll.Sua(s);
            }

            ThongBao.ThanhCong(_cheDo == CheDo.Them
                ? $"Đã thêm sách \"{s.TenSach}\" ({s.MaSach})."
                : $"Đã cập nhật sách \"{s.TenSach}\" ({s.MaSach}).");

            DatCheDo(CheDo.Xem);
            TaiDanhSach(s.MaSach);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex, errSach, this);
        }
    }

    private void btnHuy_Click(object? sender, EventArgs e)
    {
        DatCheDo(CheDo.Xem);
        HienSachLenONhap(SachDangChon);
    }

    /// <summary>Ẩn hoặc hiện lại sách đang chọn (SP-03: không xóa cứng sách).</summary>
    private void btnAnHien_Click(object? sender, EventArgs e)
    {
        Sach? s = SachDangChon;
        if (s == null)
        {
            ThongBao.CanhBao("Vui lòng chọn sách.");
            return;
        }

        string hoi = s.TrangThai
            ? $"Ẩn sách \"{s.TenSach}\"? Sách ẩn không bán được nhưng vẫn giữ trong lịch sử hóa đơn."
            : $"Hiện lại sách \"{s.TenSach}\" để tiếp tục bán?";
        if (!ThongBao.XacNhan(hoi))
        {
            return;
        }

        try
        {
            _sachBll.AnHien(s.MaSach, !s.TrangThai);
            TaiDanhSach(s.MaSach);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    // ===================== In danh sách sắp hết (đặc tả 3.6) =====================

    /// <summary>Xem trước và in danh sách sách sắp hết (mọi loại) khổ A4.</summary>
    private void btnInSapHet_Click(object? sender, EventArgs e)
    {
        try
        {
            List<Sach> ds = _sachBll.LaySachSapHet(null);
            if (ds.Count == 0)
            {
                ThongBao.ThongTin("Không có sách nào sắp hết.");
                return;
            }

            using var boIn = new SachSapHetPrinter(ds);
            if (!boIn.TaiLieu.PrinterSettings.IsValid)
            {
                ThongBao.Loi("Máy chưa cài máy in nào (kể cả Microsoft Print to PDF) nên không thể xem trước/in.");
                return;
            }

            using var xemTruoc = new PrintPreviewDialog
            {
                Document = boIn.TaiLieu,
                Width = 900,
                Height = 760,
                StartPosition = FormStartPosition.CenterParent,
                UseAntiAlias = true,
                Text = "Xem trước – Danh sách sách sắp hết"
            };
            xemTruoc.ShowDialog(this);
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    // ===================== Ảnh bìa =====================

    private void btnChonAnh_Click(object? sender, EventArgs e)
    {
        if (ofdChonAnh.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        if (HienAnh(ofdChonAnh.FileName))
        {
            _duongDanAnhMoi = ofdChonAnh.FileName;
            _xoaAnh = false;
        }
    }

    private void btnXoaAnh_Click(object? sender, EventArgs e)
    {
        _duongDanAnhMoi = null;
        _xoaAnh = true;
        HienAnh(null);
    }

    /// <summary>
    /// Hiện ảnh KHÔNG khóa file: đọc toàn bộ byte vào MemoryStream, tạo Bitmap bản sao,
    /// giải phóng ảnh cũ trước khi gán ảnh mới. Trả về false nếu file không phải ảnh hợp lệ.
    /// </summary>
    private bool HienAnh(string? duongDan)
    {
        Image? anhCu = picAnhBia.Image;
        picAnhBia.Image = null;
        anhCu?.Dispose();

        if (string.IsNullOrEmpty(duongDan) || !File.Exists(duongDan))
        {
            return false;
        }

        try
        {
            using var luong = new MemoryStream(File.ReadAllBytes(duongDan));
            using Image anhTam = Image.FromStream(luong);
            picAnhBia.Image = new Bitmap(anhTam);
            return true;
        }
        catch (Exception ex) when (ex is ArgumentException or IOException or OutOfMemoryException)
        {
            ThongBao.CanhBao("Không đọc được file ảnh: " + ex.Message);
            return false;
        }
    }

    // ===================== Ô nhập =====================

    /// <summary>ISBN chỉ nhận chữ số (BLL vẫn kiểm tra lại khi dán chuỗi).</summary>
    private void txtISBN_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    /// <summary>Đổ sách lên các ô nhập và ảnh bìa; null thì xóa trắng.</summary>
    private void HienSachLenONhap(Sach? s)
    {
        if (s == null)
        {
            XoaONhap();
            return;
        }

        txtMaSach.Text = s.MaSach;
        txtISBN.Text = s.ISBN ?? string.Empty;
        txtTenSach.Text = s.TenSach;
        txtTacGia.Text = s.TacGia ?? string.Empty;
        txtNhaXuatBan.Text = s.NhaXuatBan ?? string.Empty;
        cboLoaiSach.SelectedValue = s.MaLoai;
        nudGiaBan.Value = Math.Clamp(s.GiaBan, nudGiaBan.Minimum, nudGiaBan.Maximum);
        nudSoLuongTon.Value = Math.Clamp(s.SoLuongTon, nudSoLuongTon.Minimum, nudSoLuongTon.Maximum);
        nudTonToiThieu.Value = Math.Clamp(s.TonToiThieu, nudTonToiThieu.Minimum, nudTonToiThieu.Maximum);
        chkDangBan.Checked = s.TrangThai;
        HienAnh(SachBLL.LayDuongDanAnhBia(s.AnhBia));

        btnAnHien.Text = s.TrangThai ? "🙈 Ẩn sách" : "👁 Hiện lại";
        _duongDanAnhMoi = null;
        _xoaAnh = false;
    }

    /// <summary>Xóa trắng ô nhập (giữ loại sách đầu danh sách, tồn tối thiểu mặc định 5).</summary>
    private void XoaONhap()
    {
        txtMaSach.Clear();
        txtISBN.Clear();
        txtTenSach.Clear();
        txtTacGia.Clear();
        txtNhaXuatBan.Clear();
        if (cboLoaiSach.Items.Count > 0)
        {
            cboLoaiSach.SelectedIndex = 0;
        }

        nudGiaBan.Value = 0;
        nudSoLuongTon.Value = 0;
        nudTonToiThieu.Value = 5;
        chkDangBan.Checked = true;
        HienAnh(null);
        _duongDanAnhMoi = null;
        _xoaAnh = false;
        errSach.Clear();
    }

    /// <summary>Đọc dữ liệu từ ô nhập thành Sach (BLL kiểm tra hợp lệ).</summary>
    private Sach DocONhap()
    {
        string? anhBia = null;
        if (_cheDo == CheDo.Sua && !_xoaAnh)
        {
            anhBia = _sachDangSua?.AnhBia;
        }

        return new Sach
        {
            MaSach = txtMaSach.Text.Trim(),
            ISBN = txtISBN.Text,
            TenSach = txtTenSach.Text,
            TacGia = txtTacGia.Text,
            NhaXuatBan = txtNhaXuatBan.Text,
            MaLoai = cboLoaiSach.SelectedValue is int maLoai ? maLoai : 0,
            TenLoai = (cboLoaiSach.SelectedItem as LoaiSach)?.TenLoai ?? string.Empty,
            GiaBan = nudGiaBan.Value,
            SoLuongTon = (int)nudSoLuongTon.Value,
            TonToiThieu = (int)nudTonToiThieu.Value,
            TrangThai = chkDangBan.Checked,
            AnhBia = anhBia
        };
    }

    /// <summary>
    /// Trạng thái nút: xem → Thêm/Sửa/Ẩn hiện/Làm mới; đang nhập → chỉ Lưu/Hủy, khóa lưới và bộ lọc.
    /// Mã sách chỉ sửa được khi thêm mới.
    /// </summary>
    private void DatCheDo(CheDo cheDo)
    {
        _cheDo = cheDo;
        bool nhap = cheDo != CheDo.Xem;
        bool coDong = SachDangChon != null;

        txtMaSach.ReadOnly = cheDo != CheDo.Them;
        txtISBN.ReadOnly = !nhap;
        txtTenSach.ReadOnly = !nhap;
        txtTacGia.ReadOnly = !nhap;
        txtNhaXuatBan.ReadOnly = !nhap;
        cboLoaiSach.Enabled = nhap;
        nudGiaBan.Enabled = nhap;
        nudSoLuongTon.Enabled = nhap;
        nudTonToiThieu.Enabled = nhap;
        chkDangBan.Enabled = nhap;
        btnChonAnh.Enabled = nhap;
        btnXoaAnh.Enabled = nhap;

        btnThem.Enabled = !nhap;
        btnSua.Enabled = !nhap && coDong;
        btnAnHien.Enabled = !nhap && coDong;
        btnLamMoi.Enabled = !nhap;
        btnLuu.Enabled = nhap;
        btnHuy.Enabled = nhap;

        pnlLoc.Enabled = !nhap;
        dgvSach.Enabled = !nhap;

        if (!nhap)
        {
            errSach.Clear();
        }
    }

    /// <summary>Giải phóng ảnh và font tự tạo khi đóng Form.</summary>
    private void FrmSach_FormClosed(object? sender, FormClosedEventArgs e)
    {
        tmrTimKiem.Stop();
        picAnhBia.Image?.Dispose();
        picAnhBia.Image = null;
        _chuDam.Dispose();
        _chuNghieng.Dispose();
    }
}
