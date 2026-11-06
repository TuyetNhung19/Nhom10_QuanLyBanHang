// File: QuanLyBanSach.GUI/Forms/FrmGioiThieu.cs
namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// Hộp thoại giới thiệu đồ án và thành viên nhóm.
/// </summary>
public partial class FrmGioiThieu : Form
{
    /// <summary>
    /// THÀNH VIÊN NHÓM – điền MSSV, họ tên, nhiệm vụ vào các dòng dưới đây (thêm/bớt dòng nếu cần).
    /// </summary>
    private static readonly ThanhVienNhom[] DanhSachThanhVien =
    {
        new(1, "", "", ""),
        new(2, "", "", ""),
        new(3, "", "", ""),
        new(4, "", "", "")
    };

    /// <summary>Khởi tạo hộp thoại giới thiệu.</summary>
    public FrmGioiThieu()
    {
        InitializeComponent();
        Helpers.GiaoDien.ApDung(this); // màu giao diện dùng chung (Helpers/GiaoDien.cs)
    }

    private void FrmGioiThieu_Load(object? sender, EventArgs e)
    {
        dgvThanhVien.DataSource = DanhSachThanhVien;
    }

    private void lblTenSanPham_Click(object sender, EventArgs e)
    {

    }

    /// <summary>Một dòng trong bảng thành viên nhóm.</summary>
    /// <param name="STT">Số thứ tự.</param>
    /// <param name="MSSV">Mã số sinh viên.</param>
    /// <param name="HoTen">Họ tên.</param>
    /// <param name="NhiemVu">Nhiệm vụ trong nhóm.</param>
    private sealed record ThanhVienNhom(int STT, string MSSV, string HoTen, string NhiemVu);
}
