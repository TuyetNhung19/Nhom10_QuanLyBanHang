// File: QuanLyBanSach.GUI/Forms/FrmMain.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmMain
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mnuChinh = new MenuStrip();
            mnuHeThong = new ToolStripMenuItem();
            mnuDoiMatKhau = new ToolStripMenuItem();
            mnuDangXuat = new ToolStripMenuItem();
            mnuSepHeThong = new ToolStripSeparator();
            mnuThoat = new ToolStripMenuItem();
            mnuDanhMuc = new ToolStripMenuItem();
            mnuLoaiSach = new ToolStripMenuItem();
            mnuSach = new ToolStripMenuItem();
            mnuKhachHang = new ToolStripMenuItem();
            mnuKhuyenMai = new ToolStripMenuItem();
            mnuBanHang = new ToolStripMenuItem();
            mnuLapHoaDon = new ToolStripMenuItem();
            mnuTraCuuHoaDon = new ToolStripMenuItem();
            mnuBaoCao = new ToolStripMenuItem();
            mnuBangDieuKhien = new ToolStripMenuItem();
            mnuThongKe = new ToolStripMenuItem();
            mnuBaoCaoCuoiNgay = new ToolStripMenuItem();
            mnuQuanTri = new ToolStripMenuItem();
            mnuTaiKhoan = new ToolStripMenuItem();
            mnuCuaSo = new ToolStripMenuItem();
            mnuXepChong = new ToolStripMenuItem();
            mnuXepNgang = new ToolStripMenuItem();
            mnuXepDoc = new ToolStripMenuItem();
            mnuSepCuaSo = new ToolStripSeparator();
            mnuDongTatCa = new ToolStripMenuItem();
            mnuTroGiup = new ToolStripMenuItem();
            mnuPhimTat = new ToolStripMenuItem();
            mnuGioiThieu = new ToolStripMenuItem();
            tsChinh = new ToolStrip();
            tsbBanHang = new ToolStripButton();
            tsbTraCuu = new ToolStripButton();
            tsbKhachHang = new ToolStripButton();
            tsbSach = new ToolStripButton();
            tsbThongKe = new ToolStripButton();
            tssDangXuat = new ToolStripSeparator();
            tsbDangXuat = new ToolStripButton();
            ssTrangThai = new StatusStrip();
            tslNguoiDung = new ToolStripStatusLabel();
            tslVaiTro = new ToolStripStatusLabel();
            tslKetNoi = new ToolStripStatusLabel();
            tslKhoangTrong = new ToolStripStatusLabel();
            tslThoiGian = new ToolStripStatusLabel();
            tmrDongHo = new System.Windows.Forms.Timer(components);
            mnuChinh.SuspendLayout();
            tsChinh.SuspendLayout();
            ssTrangThai.SuspendLayout();
            SuspendLayout();
            // 
            // mnuChinh
            // 
            mnuChinh.Items.AddRange(new ToolStripItem[] { mnuHeThong, mnuDanhMuc, mnuBanHang, mnuBaoCao, mnuQuanTri, mnuCuaSo, mnuTroGiup });
            mnuChinh.Location = new Point(0, 0);
            mnuChinh.MdiWindowListItem = mnuCuaSo;
            mnuChinh.Name = "mnuChinh";
            mnuChinh.Size = new Size(1200, 27);
            mnuChinh.TabIndex = 0;
            mnuChinh.Text = "Menu chính";
            // 
            // mnuHeThong
            // 
            mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] { mnuDoiMatKhau, mnuDangXuat, mnuSepHeThong, mnuThoat });
            mnuHeThong.Name = "mnuHeThong";
            mnuHeThong.Size = new Size(90, 23);
            mnuHeThong.Text = "&Hệ thống";
            // 
            // mnuDoiMatKhau
            // 
            mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            mnuDoiMatKhau.Size = new Size(260, 24);
            mnuDoiMatKhau.Text = "Đổi &mật khẩu...";
            mnuDoiMatKhau.Click += mnuDoiMatKhau_Click;
            // 
            // mnuDangXuat
            // 
            mnuDangXuat.Name = "mnuDangXuat";
            mnuDangXuat.ShortcutKeys = Keys.Control | Keys.Shift | Keys.L;
            mnuDangXuat.Size = new Size(260, 24);
            mnuDangXuat.Text = "Đăng &xuất";
            mnuDangXuat.Click += mnuDangXuat_Click;
            // 
            // mnuSepHeThong
            // 
            mnuSepHeThong.Name = "mnuSepHeThong";
            mnuSepHeThong.Size = new Size(260, 6);
            // 
            // mnuThoat
            // 
            mnuThoat.Name = "mnuThoat";
            mnuThoat.ShortcutKeyDisplayString = "Alt+F4";
            mnuThoat.Size = new Size(260, 24);
            mnuThoat.Text = "&Thoát";
            mnuThoat.Click += mnuThoat_Click;
            // 
            // mnuDanhMuc
            // 
            mnuDanhMuc.DropDownItems.AddRange(new ToolStripItem[] { mnuLoaiSach, mnuSach, mnuKhachHang, mnuKhuyenMai });
            mnuDanhMuc.Name = "mnuDanhMuc";
            mnuDanhMuc.Size = new Size(90, 23);
            mnuDanhMuc.Text = "&Danh mục";
            // 
            // mnuLoaiSach
            // 
            mnuLoaiSach.Name = "mnuLoaiSach";
            mnuLoaiSach.Size = new Size(260, 24);
            mnuLoaiSach.Text = "&Loại sách";
            mnuLoaiSach.Click += mnuLoaiSach_Click;
            // 
            // mnuSach
            // 
            mnuSach.Name = "mnuSach";
            mnuSach.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            mnuSach.Size = new Size(260, 24);
            mnuSach.Text = "&Sách";
            mnuSach.Click += mnuSach_Click;
            // 
            // mnuKhachHang
            // 
            mnuKhachHang.Name = "mnuKhachHang";
            mnuKhachHang.ShortcutKeys = Keys.Control | Keys.K;
            mnuKhachHang.Size = new Size(260, 24);
            mnuKhachHang.Text = "&Khách hàng";
            mnuKhachHang.Click += mnuKhachHang_Click;
            // 
            // mnuKhuyenMai
            // 
            mnuKhuyenMai.Name = "mnuKhuyenMai";
            mnuKhuyenMai.Size = new Size(260, 24);
            mnuKhuyenMai.Text = "Khuyến &mãi";
            mnuKhuyenMai.Click += mnuKhuyenMai_Click;
            // 
            // mnuBanHang
            // 
            mnuBanHang.DropDownItems.AddRange(new ToolStripItem[] { mnuLapHoaDon, mnuTraCuuHoaDon });
            mnuBanHang.Name = "mnuBanHang";
            mnuBanHang.Size = new Size(90, 23);
            mnuBanHang.Text = "&Bán hàng";
            // 
            // mnuLapHoaDon
            // 
            mnuLapHoaDon.Name = "mnuLapHoaDon";
            mnuLapHoaDon.ShortcutKeys = Keys.Control | Keys.B;
            mnuLapHoaDon.Size = new Size(260, 24);
            mnuLapHoaDon.Text = "&Lập hóa đơn";
            mnuLapHoaDon.Click += mnuLapHoaDon_Click;
            // 
            // mnuTraCuuHoaDon
            // 
            mnuTraCuuHoaDon.Name = "mnuTraCuuHoaDon";
            mnuTraCuuHoaDon.ShortcutKeys = Keys.Control | Keys.T;
            mnuTraCuuHoaDon.Size = new Size(260, 24);
            mnuTraCuuHoaDon.Text = "&Tra cứu hóa đơn";
            mnuTraCuuHoaDon.Click += mnuTraCuuHoaDon_Click;
            // 
            // mnuBaoCao
            // 
            mnuBaoCao.DropDownItems.AddRange(new ToolStripItem[] { mnuBangDieuKhien, mnuThongKe, mnuBaoCaoCuoiNgay });
            mnuBaoCao.Name = "mnuBaoCao";
            mnuBaoCao.Size = new Size(90, 23);
            mnuBaoCao.Text = "Báo &cáo";
            // 
            // mnuBangDieuKhien
            // 
            mnuBangDieuKhien.Name = "mnuBangDieuKhien";
            mnuBangDieuKhien.ShortcutKeys = Keys.Control | Keys.D;
            mnuBangDieuKhien.Size = new Size(260, 24);
            mnuBangDieuKhien.Text = "&Bảng điều khiển";
            mnuBangDieuKhien.Click += mnuBangDieuKhien_Click;
            // 
            // mnuThongKe
            // 
            mnuThongKe.Name = "mnuThongKe";
            mnuThongKe.ShortcutKeys = Keys.Control | Keys.R;
            mnuThongKe.Size = new Size(260, 24);
            mnuThongKe.Text = "&Thống kê doanh thu";
            mnuThongKe.Click += mnuThongKe_Click;
            // 
            // mnuBaoCaoCuoiNgay
            // 
            mnuBaoCaoCuoiNgay.Name = "mnuBaoCaoCuoiNgay";
            mnuBaoCaoCuoiNgay.ShortcutKeys = Keys.Control | Keys.E;
            mnuBaoCaoCuoiNgay.Size = new Size(260, 24);
            mnuBaoCaoCuoiNgay.Text = "Báo cáo &cuối ngày";
            mnuBaoCaoCuoiNgay.Click += mnuBaoCaoCuoiNgay_Click;
            // 
            // mnuQuanTri
            // 
            mnuQuanTri.DropDownItems.AddRange(new ToolStripItem[] { mnuTaiKhoan });
            mnuQuanTri.Name = "mnuQuanTri";
            mnuQuanTri.Size = new Size(90, 23);
            mnuQuanTri.Text = "&Quản trị";
            // 
            // mnuTaiKhoan
            // 
            mnuTaiKhoan.Name = "mnuTaiKhoan";
            mnuTaiKhoan.Size = new Size(260, 24);
            mnuTaiKhoan.Text = "&Tài khoản";
            mnuTaiKhoan.Click += mnuTaiKhoan_Click;
            // 
            // mnuCuaSo
            // 
            mnuCuaSo.DropDownItems.AddRange(new ToolStripItem[] { mnuXepChong, mnuXepNgang, mnuXepDoc, mnuSepCuaSo, mnuDongTatCa });
            mnuCuaSo.Name = "mnuCuaSo";
            mnuCuaSo.Size = new Size(90, 23);
            mnuCuaSo.Text = "&Cửa sổ";
            // 
            // mnuXepChong
            // 
            mnuXepChong.Name = "mnuXepChong";
            mnuXepChong.Size = new Size(260, 24);
            mnuXepChong.Text = "Xếp &chồng";
            mnuXepChong.Click += mnuXepChong_Click;
            // 
            // mnuXepNgang
            // 
            mnuXepNgang.Name = "mnuXepNgang";
            mnuXepNgang.Size = new Size(260, 24);
            mnuXepNgang.Text = "Xếp &ngang";
            mnuXepNgang.Click += mnuXepNgang_Click;
            // 
            // mnuXepDoc
            // 
            mnuXepDoc.Name = "mnuXepDoc";
            mnuXepDoc.Size = new Size(260, 24);
            mnuXepDoc.Text = "Xếp &dọc";
            mnuXepDoc.Click += mnuXepDoc_Click;
            // 
            // mnuSepCuaSo
            // 
            mnuSepCuaSo.Name = "mnuSepCuaSo";
            mnuSepCuaSo.Size = new Size(260, 6);
            // 
            // mnuDongTatCa
            // 
            mnuDongTatCa.Name = "mnuDongTatCa";
            mnuDongTatCa.Size = new Size(260, 24);
            mnuDongTatCa.Text = "Đóng &tất cả";
            mnuDongTatCa.Click += mnuDongTatCa_Click;
            // 
            // mnuTroGiup
            // 
            mnuTroGiup.DropDownItems.AddRange(new ToolStripItem[] { mnuPhimTat, mnuGioiThieu });
            mnuTroGiup.Name = "mnuTroGiup";
            mnuTroGiup.Size = new Size(90, 23);
            mnuTroGiup.Text = "&Trợ giúp";
            // 
            // mnuPhimTat
            // 
            mnuPhimTat.Name = "mnuPhimTat";
            mnuPhimTat.ShortcutKeys = Keys.Control | Keys.F1;
            mnuPhimTat.Size = new Size(260, 24);
            mnuPhimTat.Text = "&Phím tắt";
            mnuPhimTat.Click += mnuPhimTat_Click;
            // 
            // mnuGioiThieu
            // 
            mnuGioiThieu.Name = "mnuGioiThieu";
            mnuGioiThieu.Size = new Size(260, 24);
            mnuGioiThieu.Text = "&Giới thiệu";
            mnuGioiThieu.Click += mnuGioiThieu_Click;
            // 
            // tsChinh
            // 
            tsChinh.ImageScalingSize = new Size(20, 20);
            tsChinh.Items.AddRange(new ToolStripItem[] { tsbBanHang, tsbTraCuu, tsbKhachHang, tsbSach, tsbThongKe, tssDangXuat, tsbDangXuat });
            tsChinh.Location = new Point(0, 27);
            tsChinh.Name = "tsChinh";
            tsChinh.Padding = new Padding(4, 2, 4, 2);
            tsChinh.Size = new Size(1200, 31);
            tsChinh.TabIndex = 1;
            tsChinh.Text = "Thanh công cụ";
            // 
            // tsbBanHang
            // 
            tsbBanHang.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbBanHang.Font = new Font("Segoe UI", 10F);
            tsbBanHang.Margin = new Padding(2, 1, 2, 2);
            tsbBanHang.Name = "tsbBanHang";
            tsbBanHang.Size = new Size(110, 24);
            tsbBanHang.Text = "🧾 Bán hàng";
            tsbBanHang.ToolTipText = "Lập hóa đơn (Ctrl+B)";
            tsbBanHang.Click += mnuLapHoaDon_Click;
            // 
            // tsbTraCuu
            // 
            tsbTraCuu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbTraCuu.Font = new Font("Segoe UI", 10F);
            tsbTraCuu.Margin = new Padding(2, 1, 2, 2);
            tsbTraCuu.Name = "tsbTraCuu";
            tsbTraCuu.Size = new Size(110, 24);
            tsbTraCuu.Text = "🔍 Tra cứu";
            tsbTraCuu.ToolTipText = "Tra cứu hóa đơn (Ctrl+T)";
            tsbTraCuu.Click += mnuTraCuuHoaDon_Click;
            // 
            // tsbKhachHang
            // 
            tsbKhachHang.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbKhachHang.Font = new Font("Segoe UI", 10F);
            tsbKhachHang.Margin = new Padding(2, 1, 2, 2);
            tsbKhachHang.Name = "tsbKhachHang";
            tsbKhachHang.Size = new Size(110, 24);
            tsbKhachHang.Text = "👥 Khách hàng";
            tsbKhachHang.ToolTipText = "Khách hàng (Ctrl+K)";
            tsbKhachHang.Click += mnuKhachHang_Click;
            // 
            // tsbSach
            // 
            tsbSach.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbSach.Font = new Font("Segoe UI", 10F);
            tsbSach.Margin = new Padding(2, 1, 2, 2);
            tsbSach.Name = "tsbSach";
            tsbSach.Size = new Size(110, 24);
            tsbSach.Text = "📚 Sách";
            tsbSach.ToolTipText = "Quản lý sách (Ctrl+Shift+S)";
            tsbSach.Click += mnuSach_Click;
            // 
            // tsbThongKe
            // 
            tsbThongKe.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbThongKe.Font = new Font("Segoe UI", 10F);
            tsbThongKe.Margin = new Padding(2, 1, 2, 2);
            tsbThongKe.Name = "tsbThongKe";
            tsbThongKe.Size = new Size(110, 24);
            tsbThongKe.Text = "📊 Thống kê";
            tsbThongKe.ToolTipText = "Thống kê doanh thu (Ctrl+R)";
            tsbThongKe.Click += mnuThongKe_Click;
            // 
            // tssDangXuat
            // 
            tssDangXuat.Name = "tssDangXuat";
            tssDangXuat.Size = new Size(6, 27);
            // 
            // tsbDangXuat
            // 
            tsbDangXuat.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbDangXuat.Font = new Font("Segoe UI", 10F);
            tsbDangXuat.Margin = new Padding(2, 1, 2, 2);
            tsbDangXuat.Name = "tsbDangXuat";
            tsbDangXuat.Size = new Size(110, 24);
            tsbDangXuat.Text = "🚪 Đăng xuất";
            tsbDangXuat.ToolTipText = "Đăng xuất (Ctrl+Shift+L)";
            tsbDangXuat.Click += mnuDangXuat_Click;
            // 
            // ssTrangThai
            // 
            ssTrangThai.ImageScalingSize = new Size(20, 20);
            ssTrangThai.Items.AddRange(new ToolStripItem[] { tslNguoiDung, tslVaiTro, tslKetNoi, tslKhoangTrong, tslThoiGian });
            ssTrangThai.Location = new Point(0, 674);
            ssTrangThai.Name = "ssTrangThai";
            ssTrangThai.Size = new Size(1200, 26);
            ssTrangThai.TabIndex = 2;
            ssTrangThai.Text = "Thanh trạng thái";
            // 
            // tslNguoiDung
            // 
            tslNguoiDung.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tslNguoiDung.Margin = new Padding(0, 3, 8, 2);
            tslNguoiDung.Name = "tslNguoiDung";
            tslNguoiDung.Size = new Size(160, 21);
            tslNguoiDung.Text = "Người dùng:";
            // 
            // tslVaiTro
            // 
            tslVaiTro.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tslVaiTro.Margin = new Padding(0, 3, 8, 2);
            tslVaiTro.Name = "tslVaiTro";
            tslVaiTro.Size = new Size(160, 21);
            tslVaiTro.Text = "Vai trò:";
            // 
            // tslKetNoi
            // 
            tslKetNoi.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tslKetNoi.Margin = new Padding(0, 3, 8, 2);
            tslKetNoi.Name = "tslKetNoi";
            tslKetNoi.Size = new Size(160, 21);
            tslKetNoi.Text = "CSDL: Đã kết nối";
            // 
            // tslKhoangTrong
            // 
            tslKhoangTrong.Name = "tslKhoangTrong";
            tslKhoangTrong.Size = new Size(1, 21);
            tslKhoangTrong.Spring = true;
            tslKhoangTrong.Text = "";
            // 
            // tslThoiGian
            // 
            tslThoiGian.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tslThoiGian.Margin = new Padding(0, 3, 8, 2);
            tslThoiGian.Name = "tslThoiGian";
            tslThoiGian.Size = new Size(160, 21);
            tslThoiGian.Text = "00:00:00";
            // 
            // tmrDongHo
            // 
            tmrDongHo.Interval = 1000;
            tmrDongHo.Tick += tmrDongHo_Tick;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1200, 700);
            Controls.Add(ssTrangThai);
            Controls.Add(tsChinh);
            Controls.Add(mnuChinh);
            Font = new Font("Segoe UI", 10F);
            IsMdiContainer = true;
            MainMenuStrip = mnuChinh;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BookSPT – Quản lý bán sách";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            mnuChinh.ResumeLayout(false);
            mnuChinh.PerformLayout();
            tsChinh.ResumeLayout(false);
            tsChinh.PerformLayout();
            ssTrangThai.ResumeLayout(false);
            ssTrangThai.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnuChinh;
        private ToolStripMenuItem mnuHeThong;
        private ToolStripMenuItem mnuDoiMatKhau;
        private ToolStripMenuItem mnuDangXuat;
        private ToolStripSeparator mnuSepHeThong;
        private ToolStripMenuItem mnuThoat;
        private ToolStripMenuItem mnuDanhMuc;
        private ToolStripMenuItem mnuLoaiSach;
        private ToolStripMenuItem mnuSach;
        private ToolStripMenuItem mnuKhachHang;
        private ToolStripMenuItem mnuKhuyenMai;
        private ToolStripMenuItem mnuBanHang;
        private ToolStripMenuItem mnuLapHoaDon;
        private ToolStripMenuItem mnuTraCuuHoaDon;
        private ToolStripMenuItem mnuBaoCao;
        private ToolStripMenuItem mnuBangDieuKhien;
        private ToolStripMenuItem mnuThongKe;
        private ToolStripMenuItem mnuBaoCaoCuoiNgay;
        private ToolStripMenuItem mnuQuanTri;
        private ToolStripMenuItem mnuTaiKhoan;
        private ToolStripMenuItem mnuCuaSo;
        private ToolStripMenuItem mnuXepChong;
        private ToolStripMenuItem mnuXepNgang;
        private ToolStripMenuItem mnuXepDoc;
        private ToolStripSeparator mnuSepCuaSo;
        private ToolStripMenuItem mnuDongTatCa;
        private ToolStripMenuItem mnuTroGiup;
        private ToolStripMenuItem mnuPhimTat;
        private ToolStripMenuItem mnuGioiThieu;
        private ToolStripButton tsbBanHang;
        private ToolStripButton tsbTraCuu;
        private ToolStripButton tsbKhachHang;
        private ToolStripButton tsbSach;
        private ToolStripButton tsbThongKe;
        private ToolStripSeparator tssDangXuat;
        private ToolStripButton tsbDangXuat;
        private ToolStripStatusLabel tslNguoiDung;
        private ToolStripStatusLabel tslVaiTro;
        private ToolStripStatusLabel tslKetNoi;
        private ToolStripStatusLabel tslKhoangTrong;
        private ToolStripStatusLabel tslThoiGian;
        private ToolStrip tsChinh;
        private StatusStrip ssTrangThai;
        private System.Windows.Forms.Timer tmrDongHo;
    }
}
