// File: QuanLyBanSach.GUI/Forms/FrmSachSapHet.cs
using System.Globalization;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;
using QuanLyBanSach.GUI.Printing;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Hộp thoại danh sách sách sắp hết (SP-04: tồn ≤ tồn tối thiểu), mở khi nhấp thẻ "Sách sắp hết" trên bảng điều khiển.
/// Thêm mới ở bước 12 vì Thu ngân không được mở FrmSach/FrmThongKe nhưng vẫn cần xem danh sách này.
/// </summary>
public partial class FrmSachSapHet : Form
{
    private readonly SachBLL _sachBll = new();
    private List<Sach> _ds = new();

    /// <summary>Khởi tạo hộp thoại.</summary>
    public FrmSachSapHet()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
        var vanHoaVN = new CultureInfo("vi-VN");
        colTon.DefaultCellStyle.FormatProvider = vanHoaVN;
        colToiThieu.DefaultCellStyle.FormatProvider = vanHoaVN;
    }

    private void FrmSachSapHet_Load(object? sender, EventArgs e)
    {
        try
        {
            _ds = _sachBll.LaySachSapHet(null);
            dgvSapHet.DataSource = _ds;
            lblSoLuong.Text = $"{_ds.Count} sách sắp hết";
            btnIn.Enabled = _ds.Count > 0;
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }
    }

    /// <summary>Xem trước và in danh sách (SachSapHetPrinter, A4).</summary>
    private void btnIn_Click(object? sender, EventArgs e)
    {
        try
        {
            using var boIn = new SachSapHetPrinter(_ds);
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

    private void btnDong_Click(object? sender, EventArgs e) => Close();
}
