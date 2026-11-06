// File: QuanLyBanSach.GUI/Forms/FrmBanHang.PhimTat.cs
using System.Media;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// ĐT1 – Bán hàng nhanh: phím tắt, nhập/quét mã + Enter, nút mệnh giá tiền.
/// </summary>
public partial class FrmBanHang
{
    /// <summary>Mệnh giá nút nhanh; 0 = "Đủ tiền" (gán bằng tổng thanh toán).</summary>
    private static readonly decimal[] MenhGia = { 0, 50_000, 100_000, 200_000, 500_000 };

    /// <summary>Tag của nút "Xóa" tiền khách đưa.</summary>
    private const decimal TagXoaTien = -1;

    /// <summary>
    /// Xử lý phím tắt ở mức Form.
    /// Dùng ProcessCmdKey thay vì KeyDown (kể cả khi KeyPreview = true) vì ProcessCmdKey chạy ở giai đoạn
    /// tiền xử lý, TRƯỚC khi control đang focus nhận phím: DataGridView tự dùng F2 (sửa ô) và Delete,
    /// NumericUpDown/Button "ăn" một số phím, nên KeyDown của Form có thể không bao giờ nhận được.
    /// Trả về true để báo phím đã xử lý, không chuyển tiếp cho control.
    /// </summary>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.F1:
                txtTimSach.Focus();
                txtTimSach.SelectAll();
                return true;

            case Keys.F2:
                txtSDTKhach.Focus();
                txtSDTKhach.SelectAll();
                return true;

            case Keys.F4:
                nudTienKhachDua.Focus();
                nudTienKhachDua.Select(0, nudTienKhachDua.Text.Length);
                return true;

            case Keys.F8:
                TreoDonHienTai();
                return true;

            case Keys.F9:
                // TC16: lưu hóa đơn và mở xem trước bản in
                LuuHoaDon(inSauKhiLuu: true);
                return true;

            case Keys.Delete when ChoPhepPhimSuaDong():
                XoaDongDangChon();
                FocusMaSach();
                return true;

            case Keys.Add or Keys.Oemplus or (Keys.Oemplus | Keys.Shift) when ChoPhepPhimSuaDong():
                TangGiamSoLuong(+1);
                FocusMaSach();
                return true;

            case Keys.Subtract or Keys.OemMinus when ChoPhepPhimSuaDong():
                TangGiamSoLuong(-1);
                FocusMaSach();
                return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    /// <summary>
    /// Delete và +/− chỉ dùng để sửa dòng khi KHÔNG chặn việc gõ chữ: con trỏ không nằm trong ô chữ
    /// (TextBox, phần nhập của NumericUpDown) đang có nội dung. Ô Mã sách rỗng vẫn cho phép.
    /// </summary>
    private bool ChoPhepPhimSuaDong()
    {
        if (DongDangChon == null)
        {
            return false;
        }

        Control? dangFocus = ActiveControl;
        while (dangFocus is ContainerControl khung && khung.ActiveControl != null)
        {
            dangFocus = khung.ActiveControl;
        }

        return dangFocus is not TextBoxBase oChu || oChu.TextLength == 0;
    }

    /// <summary>
    /// Ô Mã sách/ISBN + Enter: tìm đúng mã hoặc ISBN (sách đang bán); thấy thì thêm 1 cuốn, xóa ô, giữ focus
    /// (gõ lại cùng mã → tăng số lượng – TC15); không thấy thì bíp và chuyển từ khóa sang ô tìm theo tên.
    /// Máy quét mã vạch USB hoạt động như bàn phím (gửi chuỗi mã rồi Enter) nên dùng được ngay, không cần cài đặt.
    /// </summary>
    private void txtMaSach_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        // Chặn tiếng "ding" mặc định của Windows khi bấm Enter trong TextBox một dòng
        e.SuppressKeyPress = true;

        string ma = txtMaSach.Text.Trim();
        if (ma.Length == 0)
        {
            return;
        }

        try
        {
            Sach? s = _sachBll.TimTheoMaHoacISBN(ma);
            if (s != null)
            {
                txtMaSach.Clear();
                ThemSach(s, 1);
            }
            else
            {
                SystemSounds.Beep.Play();
                txtMaSach.Clear();
                txtTimSach.Text = ma;
                txtTimSach.Focus();
                txtTimSach.SelectAll();
            }
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
            FocusMaSach();
        }
    }

    /// <summary>
    /// Tạo động các nút mệnh giá vào flpMenhGia, dùng chung một hàm xử lý qua Tag:
    /// "Đủ tiền" = tổng thanh toán; các nút khác CỘNG DỒN; "Xóa" đưa về 0.
    /// </summary>
    private void TaoNutMenhGia()
    {
        flpMenhGia.Controls.Clear();

        foreach (decimal menhGia in MenhGia)
        {
            flpMenhGia.Controls.Add(TaoNut(menhGia == 0 ? "Đủ tiền" : "+" + menhGia.ToString("#,##0", VanHoaVN), menhGia));
        }

        flpMenhGia.Controls.Add(TaoNut("Xóa", TagXoaTien));
    }

    private Button TaoNut(string chu, decimal tag)
    {
        var nut = new Button
        {
            Text = chu,
            Tag = tag,
            AutoSize = true,
            MinimumSize = new Size(62, 36),
            Margin = new Padding(2),
            TabStop = false,
            UseVisualStyleBackColor = true
        };
        nut.Click += NutMenhGia_Click;
        Helpers.GiaoDien.KieuNut(nut); // nút tạo bằng code sau ApDung nên phải tô màu riêng
        return nut;
    }

    /// <summary>Hàm xử lý chung cho mọi nút mệnh giá (phân biệt bằng Tag).</summary>
    private void NutMenhGia_Click(object? sender, EventArgs e)
    {
        if (sender is not Button { Tag: decimal menhGia })
        {
            return;
        }

        decimal tong = TongThanhToanHienTai();
        decimal giaTriMoi = menhGia switch
        {
            TagXoaTien => 0,
            0 => tong,
            _ => nudTienKhachDua.Value + menhGia
        };

        nudTienKhachDua.Value = Math.Clamp(giaTriMoi, nudTienKhachDua.Minimum, nudTienKhachDua.Maximum);
        TinhLaiTien();
        FocusMaSach();
    }

    /// <summary>Tổng thanh toán hiện tại = tạm tính − tiền giảm của khuyến mãi đang áp dụng.</summary>
    private decimal TongThanhToanHienTai() => BLL.HoaDonBLL.TinhTamTinh(_dsDong) - _ketQuaKM.TienGiam;
}
