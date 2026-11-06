// File: QuanLyBanSach.GUI/Helpers/GiaoDien.cs
using System.Configuration;

namespace QuanLyBanSach.GUI.Helpers;

/// <summary>
/// Bộ màu và kiểu giao diện dùng chung (theo nhận diện ĐH Sư phạm TP.HCM: xanh dương đậm, trắng, điểm nhấn đỏ).
/// Mọi Form gọi <see cref="ApDung"/> ngay sau InitializeComponent → đổi màu một chỗ, cả chương trình đổi theo.
/// Chỉ đổi màu nền Form, nút, lưới, menu; KHÔNG đổi màu các nhãn mang ý nghĩa (tiền thối xanh lá, lỗi đỏ...).
/// </summary>
public static class GiaoDien
{
    /// <summary>Xanh dương đậm – màu chủ đạo (#0E4874).</summary>
    public static readonly Color MauChinh = Color.FromArgb(14, 72, 116);

    /// <summary>Xanh dương đậm hơn – khi rê chuột/nhấn nút chính (#0A3557).</summary>
    public static readonly Color MauChinhDam = Color.FromArgb(10, 53, 87);

    /// <summary>Xanh dương vừa – dòng đang chọn, cột biểu đồ (#396F92).</summary>
    public static readonly Color MauVua = Color.FromArgb(57, 111, 146);

    /// <summary>Xanh nhạt – nền thẻ, nền vùng thông tin, menu (#DFE6EF).</summary>
    public static readonly Color MauNhat = Color.FromArgb(223, 230, 239);

    /// <summary>Nền Form – trắng ánh xanh (#F3F4FA).</summary>
    public static readonly Color MauNen = Color.FromArgb(243, 244, 250);

    /// <summary>Đỏ điểm nhấn – nút xóa/hủy, cảnh báo (#D13A3D).</summary>
    public static readonly Color MauNhan = Color.FromArgb(209, 58, 61);

    /// <summary>Nền cảnh báo nhạt (thẻ sách sắp hết).</summary>
    public static readonly Color MauNhanNhat = Color.FromArgb(250, 226, 227);

    /// <summary>Nút "chính" của màn hình: nền xanh đậm, chữ trắng.</summary>
    private static readonly HashSet<string> NutChinh = new(StringComparer.OrdinalIgnoreCase)
    {
        "btnDangNhap", "btnLuu", "btnLuuVaIn", "btnXem", "btnTim", "btnOK", "btnChon", "btnGoiLai", "btnIn"
    };

    /// <summary>Nút nguy hiểm (xóa/hủy dữ liệu): viền và chữ đỏ.</summary>
    private static readonly HashSet<string> NutNguyHiem = new(StringComparer.OrdinalIgnoreCase)
    {
        "btnXoa", "btnXoaDong", "btnXoaAnh", "btnHuyHoaDon", "btnHuyDon", "btnKhoaMo"
    };

    /// <summary>Tên nhà sách đọc từ App.config (appSettings TenCuaHang).</summary>
    public static string TenCuaHang => ConfigurationManager.AppSettings["TenCuaHang"] ?? "NHÀ SÁCH SPT";

    /// <summary>
    /// Gọi một lần trong Program.Main: mọi MenuStrip/ToolStrip/StatusStrip dùng bảng màu chung.
    /// </summary>
    public static void KhoiTao()
    {
        ToolStripManager.Renderer = new ToolStripProfessionalRenderer(new BangMauThanhCongCu()) { RoundedEdges = false };
    }

    /// <summary>Áp dụng màu cho Form/UserControl và toàn bộ control con (đệ quy).</summary>
    public static void ApDung(Control goc)
    {
        if (goc is Form form)
        {
            form.BackColor = MauNen;
        }

        ApDungDeQuy(goc, (goc as Form)?.AcceptButton as Button);
    }

    private static void ApDungDeQuy(Control cha, Button? nutMacDinh)
    {
        foreach (Control c in cha.Controls)
        {
            switch (c)
            {
                case MdiClient mdi:
                    mdi.BackColor = MauNhat;
                    break;
                case Button nut:
                    KieuNut(nut, nut == nutMacDinh);
                    break;
                case DataGridView dgv:
                    KieuLuoi(dgv);
                    break;
                case StatusStrip thanhTrangThai:
                    thanhTrangThai.BackColor = MauChinh;
                    thanhTrangThai.ForeColor = Color.White;
                    break;
            }

            if (c.HasChildren && c is not DataGridView)
            {
                ApDungDeQuy(c, nutMacDinh);
            }
        }
    }

    /// <summary>Nút phẳng: chính (xanh đậm, chữ trắng) / nguy hiểm (đỏ) / thường (trắng, viền xanh).</summary>
    public static void KieuNut(Button nut, bool laNutChinh = false)
    {
        nut.FlatStyle = FlatStyle.Flat;
        nut.UseVisualStyleBackColor = false;
        nut.Cursor = Cursors.Hand;
        nut.FlatAppearance.BorderSize = 1;

        if (laNutChinh || NutChinh.Contains(nut.Name))
        {
            nut.BackColor = MauChinh;
            nut.ForeColor = Color.White;
            nut.FlatAppearance.BorderColor = MauChinh;
            nut.FlatAppearance.MouseOverBackColor = MauVua;
            nut.FlatAppearance.MouseDownBackColor = MauChinhDam;
        }
        else if (NutNguyHiem.Contains(nut.Name))
        {
            nut.BackColor = Color.White;
            nut.ForeColor = MauNhan;
            nut.FlatAppearance.BorderColor = MauNhan;
            nut.FlatAppearance.MouseOverBackColor = MauNhanNhat;
            nut.FlatAppearance.MouseDownBackColor = MauNhanNhat;
        }
        else
        {
            nut.BackColor = Color.White;
            nut.ForeColor = MauChinh;
            nut.FlatAppearance.BorderColor = MauVua;
            nut.FlatAppearance.MouseOverBackColor = MauNhat;
            nut.FlatAppearance.MouseDownBackColor = MauNhat;
        }
    }

    /// <summary>Lưới: tiêu đề cột xanh đậm chữ trắng, dòng xen kẽ nhạt, dòng chọn xanh vừa.</summary>
    public static void KieuLuoi(DataGridView dgv)
    {
        dgv.EnableHeadersVisualStyles = false;
        dgv.BackgroundColor = Color.White;
        dgv.BorderStyle = BorderStyle.FixedSingle;
        dgv.GridColor = MauNhat;
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

        DataGridViewCellStyle tieuDe = dgv.ColumnHeadersDefaultCellStyle;
        tieuDe.BackColor = MauChinh;
        tieuDe.ForeColor = Color.White;
        tieuDe.SelectionBackColor = MauChinh;
        tieuDe.SelectionForeColor = Color.White;
        tieuDe.Font = new Font(dgv.Font, FontStyle.Bold);

        dgv.DefaultCellStyle.SelectionBackColor = MauVua;
        dgv.DefaultCellStyle.SelectionForeColor = Color.White;
        dgv.AlternatingRowsDefaultCellStyle.BackColor = MauNen;
        dgv.RowHeadersDefaultCellStyle.BackColor = MauNhat;
    }

    /// <summary>Bảng màu cho menu, thanh công cụ (ToolStripProfessionalRenderer).</summary>
    private sealed class BangMauThanhCongCu : ProfessionalColorTable
    {
        public override Color MenuStripGradientBegin => MauNhat;
        public override Color MenuStripGradientEnd => MauNhat;
        public override Color ToolStripGradientBegin => MauNen;
        public override Color ToolStripGradientMiddle => MauNen;
        public override Color ToolStripGradientEnd => MauNen;
        public override Color ToolStripBorder => MauNhat;
        public override Color ToolStripDropDownBackground => Color.White;
        public override Color ImageMarginGradientBegin => MauNen;
        public override Color ImageMarginGradientMiddle => MauNen;
        public override Color ImageMarginGradientEnd => MauNen;
        public override Color MenuBorder => MauVua;
        public override Color MenuItemBorder => MauVua;
        public override Color MenuItemSelected => MauNhat;
        public override Color MenuItemSelectedGradientBegin => MauNhat;
        public override Color MenuItemSelectedGradientEnd => MauNhat;
        public override Color MenuItemPressedGradientBegin => MauNhat;
        public override Color MenuItemPressedGradientMiddle => MauNhat;
        public override Color MenuItemPressedGradientEnd => MauNhat;
        public override Color ButtonSelectedHighlight => MauNhat;
        public override Color ButtonSelectedGradientBegin => MauNhat;
        public override Color ButtonSelectedGradientMiddle => MauNhat;
        public override Color ButtonSelectedGradientEnd => MauNhat;
        public override Color ButtonSelectedBorder => MauVua;
        public override Color ButtonPressedGradientBegin => MauVua;
        public override Color ButtonPressedGradientMiddle => MauVua;
        public override Color ButtonPressedGradientEnd => MauVua;
        public override Color ButtonCheckedGradientBegin => MauNhat;
        public override Color ButtonCheckedGradientEnd => MauNhat;
        public override Color SeparatorDark => MauVua;
        public override Color SeparatorLight => Color.White;
        public override Color StatusStripGradientBegin => MauChinh;
        public override Color StatusStripGradientEnd => MauChinh;
    }
}
