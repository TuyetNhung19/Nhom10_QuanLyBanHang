// File: QuanLyBanSach.GUI/Forms/FrmBanHang.DonTreo.cs
using System.Media;
using QuanLyBanSach.BLL;
using QuanLyBanSach.DTO;
using QuanLyBanSach.GUI.Helpers;

namespace QuanLyBanSach.GUI.Forms;

/// <summary>
/// ĐT2 – Treo hóa đơn: cài đặt các partial method KhoiTaoDonTreo, TreoDonHienTai, MoDanhSachDonTreo.
/// Đơn treo chỉ nằm trong bộ nhớ (DonTreoBLL), chưa trừ tồn, tối đa 5 đơn mỗi thu ngân (BH-01).
/// </summary>
public partial class FrmBanHang
{
    /// <summary>Thời gian hiển thị thông báo nhanh trên StatusStrip (mili giây).</summary>
    private const int ThoiGianThongBaoNhanh = 4000;

    private ToolStripStatusLabel? _tslThongBao;
    private System.Windows.Forms.Timer? _tmrThongBao;

    /// <summary>
    /// Khởi tạo phần đơn treo khi mở Form. Sự kiện Click của btnTreoDon và btnDonTreo ĐÃ được gắn
    /// trong Designer (btnTreoDon_Click → TreoDonHienTai, btnDonTreo_Click → MoDanhSachDonTreo) nên
    /// KHÔNG gắn lại ở đây (gắn hai lần sẽ treo/mở danh sách hai lần). Hàm này chỉ thêm nhãn thông báo
    /// nhanh vào StatusStrip, đặt chú thích nút và cập nhật chữ "Đơn treo (n)".
    /// </summary>
    partial void KhoiTaoDonTreo()
    {
        _tslThongBao = new ToolStripStatusLabel
        {
            Name = "tslThongBao",
            ForeColor = Color.White, // nằm trên StatusStrip nền xanh đậm
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Spring = true,
            TextAlign = ContentAlignment.MiddleRight
        };
        ssBanHang.Items.Add(_tslThongBao);

        _tmrThongBao = new System.Windows.Forms.Timer(components) { Interval = ThoiGianThongBaoNhanh };
        _tmrThongBao.Tick += (_, _) =>
        {
            _tmrThongBao.Stop();
            if (_tslThongBao != null)
            {
                _tslThongBao.Text = string.Empty;
            }
        };

        var chuThich = new ToolTip(components);
        chuThich.SetToolTip(btnTreoDon, $"Giữ đơn đang lập để phục vụ khách kế tiếp (tối đa {DonTreoBLL.SoDonToiDa} đơn)");
        chuThich.SetToolTip(btnDonTreo, "Xem và gọi lại các đơn đang treo");

        CapNhatNutDonTreo();
    }

    /// <summary>
    /// F8 – Treo đơn đang lập: đơn rỗng hoặc đã đủ 5 đơn treo thì báo; ngược lại tạo HoaDonTam
    /// (sao chép sâu các dòng, khách, ghi chú, giờ treo), giao cho DonTreoBLL rồi làm mới đơn.
    /// Thành công chỉ báo nhẹ trên StatusStrip để không làm chậm thu ngân.
    /// </summary>
    partial void TreoDonHienTai()
    {
        if (_dsDong.Count == 0)
        {
            SystemSounds.Beep.Play();
            HienThongBaoNhanh("Đơn đang trống, không có gì để treo.");
            FocusMaSach();
            return;
        }

        if (_donTreoBll.Dem() >= DonTreoBLL.SoDonToiDa)
        {
            ThongBao.CanhBao($"Đã có {DonTreoBLL.SoDonToiDa} đơn treo (BH-01). Hãy gọi lại hoặc hủy bớt đơn treo trước.");
            FocusMaSach();
            return;
        }

        var don = new HoaDonTam
        {
            MaTK = PhienDangNhap.TaiKhoanHienTai?.MaTK ?? 0,
            ThoiGianTreo = DateTime.Now,
            Khach = _khach,
            GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
            ChiTiet = _dsDong.Select(ct => ct.SaoChep()).ToList()
        };

        try
        {
            _donTreoBll.Treo(don);
            LamMoiDon();
            HienThongBaoNhanh($"Đã treo đơn {don.TenKhachHienThi} – {don.SoCuon} cuốn, {Tien(don.TamTinh)} lúc {don.GioTreoText}.");
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }

        CapNhatNutDonTreo();
        FocusMaSach();
    }

    /// <summary>
    /// Mở danh sách đơn treo. Chọn gọi lại khi đơn hiện tại đang có dòng → hỏi treo đơn hiện tại trước
    /// (Có = treo, Không = bỏ đơn hiện tại, Hủy = thôi). Đơn gọi lại được DonTreoBLL cập nhật giá/tồn
    /// hiện hành; cảnh báo hiện trong một hộp thoại, dòng vượt tồn tô đỏ và BLL không cho lưu đến khi sửa (TC21).
    /// </summary>
    partial void MoDanhSachDonTreo()
    {
        if (_donTreoBll.Dem() == 0)
        {
            SystemSounds.Beep.Play();
            HienThongBaoNhanh("Không có đơn treo.");
            FocusMaSach();
            return;
        }

        Guid id;
        using (var frm = new FrmDonTreo())
        {
            if (frm.ShowDialog(this) != DialogResult.OK || frm.IdDuocChon is not Guid idChon)
            {
                CapNhatNutDonTreo();
                FocusMaSach();
                return;
            }

            id = idChon;
        }

        bool treoDonHienTai = false;
        if (_dsDong.Count > 0)
        {
            DialogResult traLoi = MessageBox.Show(
                "Đơn hiện tại đang có sách.\nTreo đơn hiện tại trước khi gọi lại?\n\n" +
                "Có = treo đơn hiện tại\nKhông = bỏ đơn hiện tại\nHủy = không gọi lại",
                "BookSPT – Gọi lại đơn treo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (traLoi == DialogResult.Cancel)
            {
                FocusMaSach();
                return;
            }

            treoDonHienTai = traLoi == DialogResult.Yes;
        }

        try
        {
            // Lấy đơn ra trước (giải phóng 1 chỗ) rồi mới treo đơn hiện tại – tránh vướng giới hạn 5 đơn
            HoaDonTam donGoiLai = _donTreoBll.GoiLai(id, out List<string> canhBao);

            if (treoDonHienTai)
            {
                TreoDonHienTai();
            }

            NapDonTreoVaoForm(donGoiLai);

            if (canhBao.Count > 0)
            {
                ThongBao.CanhBao("Đơn treo đã được cập nhật theo dữ liệu hiện tại:\n\n- " + string.Join("\n- ", canhBao) +
                                 "\n\nDòng tô đỏ đang vượt tồn – hãy giảm số lượng hoặc xóa dòng trước khi lưu.");
            }
            else
            {
                HienThongBaoNhanh($"Đã gọi lại đơn {donGoiLai.TenKhachHienThi} ({donGoiLai.SoCuon} cuốn).");
            }
        }
        catch (Exception ex)
        {
            ThongBao.XuLyNgoaiLe(ex);
        }

        CapNhatNutDonTreo();
        FocusMaSach();
    }

    /// <summary>Đổ đơn treo vào Form: dòng, khách, ghi chú; tiền khách đưa về 0 và tính lại tiền.</summary>
    private void NapDonTreoVaoForm(HoaDonTam don)
    {
        _dsDong.Clear();
        _dsDong.AddRange(don.ChiTiet);
        for (int i = 0; i < _dsDong.Count; i++)
        {
            _dsDong[i].Stt = i + 1;
        }

        CapNhatLuoiChiTiet(_dsDong.Count > 0 ? _dsDong[0].MaSach : null);
        DatKhach(don.Khach);
        txtGhiChu.Text = don.GhiChu ?? string.Empty;
        nudTienKhachDua.Value = 0;
        TinhLaiTien();
    }

    /// <summary>Thông báo nhanh trên StatusStrip, tự xóa sau vài giây (không dùng MessageBox).</summary>
    private void HienThongBaoNhanh(string thongDiep)
    {
        if (_tslThongBao == null || _tmrThongBao == null)
        {
            return;
        }

        _tslThongBao.Text = thongDiep;
        _tmrThongBao.Stop();
        _tmrThongBao.Start();
    }
}
