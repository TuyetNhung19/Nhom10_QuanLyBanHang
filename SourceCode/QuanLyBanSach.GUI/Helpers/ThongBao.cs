// File: QuanLyBanSach.GUI/Helpers/ThongBao.cs
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using QuanLyBanSach.BLL;

namespace QuanLyBanSach.GUI.Helpers;

/// <summary>
/// Hộp thoại dùng chung cho mọi Form và xử lý ngoại lệ thống nhất:
/// BusinessException → gắn ErrorProvider đúng control theo FieldName;
/// SqlException / lỗi kết nối → "Không kết nối được CSDL"; lỗi khác → thông báo chung.
/// </summary>
public static class ThongBao
{
    private const string TieuDe = "BookSPT";

    /// <summary>Tiền tố tên control NHẬP LIỆU theo quy ước đặt tên (Label không được gắn lỗi).</summary>
    private static readonly Regex TienToControl = new("^(txt|cbo|nud|dtp|chk|dgv|pic|lst|rdo)");

    /// <summary>Hộp thoại lỗi.</summary>
    public static void Loi(string thongDiep) =>
        MessageBox.Show(thongDiep, TieuDe + " – Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

    /// <summary>Hộp thoại cảnh báo (lỗi nghiệp vụ, dữ liệu chưa hợp lệ).</summary>
    public static void CanhBao(string thongDiep) =>
        MessageBox.Show(thongDiep, TieuDe + " – Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    /// <summary>Hộp thoại báo thành công.</summary>
    public static void ThanhCong(string thongDiep) =>
        MessageBox.Show(thongDiep, TieuDe, MessageBoxButtons.OK, MessageBoxIcon.Information);

    /// <summary>Hộp thoại thông tin.</summary>
    public static void ThongTin(string thongDiep) =>
        MessageBox.Show(thongDiep, TieuDe, MessageBoxButtons.OK, MessageBoxIcon.Information);

    /// <summary>Hỏi Có/Không; nút mặc định là "Không" để tránh bấm nhầm Enter.</summary>
    /// <returns>true nếu người dùng chọn Có.</returns>
    public static bool XacNhan(string thongDiep) =>
        MessageBox.Show(thongDiep, TieuDe + " – Xác nhận", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;

    /// <summary>
    /// Xử lý mọi ngoại lệ ở GUI.
    /// - BusinessException: nếu có <paramref name="err"/> và <paramref name="form"/> thì gắn lỗi vào control
    ///   khớp FieldName (txtGiaBan ↔ "GiaBan") và đặt focus; sau đó hiện hộp cảnh báo nếu <paramref name="hienHopThoai"/>.
    /// - SqlException hoặc lỗi kết nối: báo "Không kết nối được CSDL" kèm chi tiết.
    /// - Lỗi khác: báo lỗi chung, không làm crash chương trình.
    /// </summary>
    /// <param name="ex">Ngoại lệ bắt được.</param>
    /// <param name="err">ErrorProvider của Form (tùy chọn).</param>
    /// <param name="form">Control gốc để tìm control theo FieldName (thường là Form).</param>
    /// <param name="hienHopThoai">false nếu Form tự hiển thị thông báo (ví dụ trên Label).</param>
    public static void XuLyNgoaiLe(Exception ex, ErrorProvider? err = null, Control? form = null, bool hienHopThoai = true)
    {
        switch (ex)
        {
            case BusinessException bex:
                if (err != null && form != null && !string.IsNullOrEmpty(bex.FieldName))
                {
                    Control? oLoi = TimControl(form, bex.FieldName);
                    if (oLoi != null)
                    {
                        err.SetError(oLoi, bex.Message);
                        oLoi.Focus();
                        if (oLoi is TextBox txt)
                        {
                            txt.SelectAll();
                        }
                    }
                }

                if (hienHopThoai)
                {
                    CanhBao(bex.Message);
                }

                break;

            case SqlException:
            case not null when HeThongBLL.LaLoiKetNoi(ex):
                Loi("Không kết nối được CSDL. Kiểm tra SQL Server và chuỗi kết nối trong App.config.\n\n" +
                    HeThongBLL.MoTaLoi(ex));
                break;

            default:
                Loi(HeThongBLL.MoTaLoi(ex!));
                break;
        }
    }

    /// <summary>
    /// Tìm control con (đệ quy) ứng với tên trường: Tag bằng tên trường, hoặc tên control bỏ tiền tố
    /// (txt, cbo, nud, dtp...) bằng tên trường, không phân biệt hoa thường. Bỏ qua Label. Trả null nếu không thấy.
    /// </summary>
    public static Control? TimControl(Control goc, string tenTruong)
    {
        foreach (Control con in goc.Controls)
        {
            if (con is Label)
            {
                continue;
            }

            if (string.Equals(con.Tag as string, tenTruong, StringComparison.OrdinalIgnoreCase)
                || string.Equals(TienToControl.Replace(con.Name, string.Empty, 1), tenTruong, StringComparison.OrdinalIgnoreCase))
            {
                return con;
            }

            Control? trongCon = TimControl(con, tenTruong);
            if (trongCon != null)
            {
                return trongCon;
            }
        }

        return null;
    }
}
