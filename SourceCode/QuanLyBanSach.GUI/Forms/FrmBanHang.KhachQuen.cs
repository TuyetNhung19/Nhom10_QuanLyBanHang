// File: QuanLyBanSach.GUI/Forms/FrmBanHang.KhachQuen.cs
using System.Diagnostics;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// ĐT4 – Hồ sơ khách quen và gợi ý "thường được mua kèm": cài đặt SauKhiChonKhach, SauKhiThemSach.
/// Dữ liệu chỉ để tham khảo; mọi lỗi ở phần này KHÔNG được chặn việc bán hàng.
/// </summary>
public partial class FrmBanHang
{
    private readonly ThongKeBLL _thongKeBll = new();
    private bool _daKhoiTaoKhachQuen;

    /// <summary>
    /// Hiện hồ sơ khách: số lần mua, tổng chi tiêu, lần mua gần nhất, 3 loại hay mua (LINQ ở ThongKeBLL).
    /// Khách lẻ → ẩn hồ sơ; khách chưa mua → "Khách mới – chưa có lịch sử".
    /// </summary>
    partial void SauKhiChonKhach(KhachHang? kh)
    {
        KhoiTaoKhachQuen();

        // Bắt đầu đơn mới (không còn dòng) thì bỏ gợi ý cũ
        if (_dsDong.Count == 0)
        {
            AnGoiY();
        }

        if (kh == null)
        {
            HienHoSo(false, "Chưa chọn khách");
            return;
        }

        try
        {
            HoSoKhachHang hoSo = _thongKeBll.LayHoSoKhach(kh.MaKH);
            if (hoSo.ChuaMua)
            {
                HienHoSo(false, "Khách mới – chưa có lịch sử");
                return;
            }

            lblSoLanMua.Text = $"Số lần mua: {hoSo.SoLanMua}";
            lblTongChiTieu.Text = $"Tổng chi tiêu: {Tien(hoSo.TongChiTieu)}";
            lblLanGanNhat.Text = "Lần gần nhất: " + (hoSo.LanMuaGanNhat?.ToString("dd/MM/yyyy") ?? "—");
            lblLoaiHayMua.Text = "Hay mua: " + (hoSo.LoaiHayMua.Count > 0 ? string.Join(", ", hoSo.LoaiHayMua) : "—");
            HienHoSo(true, string.Empty);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ĐT4 – lỗi tải hồ sơ khách: " + ex);
            HienHoSo(false, "Không tải được hồ sơ khách");
        }
    }

    /// <summary>
    /// Sau khi thêm sách: lấy tối đa 3 sách thường được mua kèm (bỏ sách đã có trong đơn, chỉ sách còn tồn).
    /// Không có gợi ý thì ẩn nhóm. Lỗi chỉ ghi Debug, không làm gián đoạn bán hàng.
    /// </summary>
    partial void SauKhiThemSach(string maSach)
    {
        KhoiTaoKhachQuen();
        try
        {
            List<GoiYSach> goiY = _thongKeBll.GoiYMuaKem(maSach, _dsDong.Select(ct => ct.MaSach));
            dgvGoiY.DataSource = goiY;
            grpGoiY.Visible = goiY.Count > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ĐT4 – lỗi lấy gợi ý mua kèm: " + ex);
            AnGoiY();
        }
    }

    /// <summary>Khởi tạo một lần: ghi chú "tham khảo", sự kiện nhấp đúp gợi ý, ẩn nhóm gợi ý.</summary>
    private void KhoiTaoKhachQuen()
    {
        if (_daKhoiTaoKhachQuen)
        {
            return;
        }

        _daKhoiTaoKhachQuen = true;

        grpHoSoKhach.Controls.Add(new Label
        {
            Name = "lblGhiChuHoSo",
            Text = "Thông tin tham khảo, không phải tích điểm",
            Dock = DockStyle.Bottom,
            Height = 20,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
            ForeColor = Color.DimGray,
            Padding = new Padding(8, 0, 0, 0)
        });

        dgvGoiY.CellDoubleClick += dgvGoiY_CellDoubleClick;
        AnGoiY();
    }

    /// <summary>TC23 – nhấp đúp sách gợi ý để thêm 1 cuốn vào đơn.</summary>
    private void dgvGoiY_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvGoiY.Rows[e.RowIndex].DataBoundItem is not GoiYSach goiY)
        {
            return;
        }

        try
        {
            Sach? s = _dsSach.FirstOrDefault(x => x.MaSach == goiY.MaSach) ?? _sachBll.TimTheoMaHoacISBN(goiY.MaSach);
            if (s != null)
            {
                ThemSach(s, 1);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ĐT4 – lỗi thêm sách gợi ý: " + ex);
            FocusMaSach();
        }
    }

    /// <summary>Hiện/ẩn các nhãn hồ sơ; khi ẩn thì hiện dòng thông báo thay thế.</summary>
    private void HienHoSo(bool hien, string thongBaoKhiAn)
    {
        lblSoLanMua.Visible = hien;
        lblTongChiTieu.Visible = hien;
        lblLanGanNhat.Visible = hien;
        lblLoaiHayMua.Visible = hien;
        lblChuaChonKhach.Visible = !hien;
        lblChuaChonKhach.Text = thongBaoKhiAn;
    }

    private void AnGoiY()
    {
        dgvGoiY.DataSource = null;
        grpGoiY.Visible = false;
    }
}
