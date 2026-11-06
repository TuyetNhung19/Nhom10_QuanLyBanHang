// File: QuanLyBanSach.GUI/UserControls/UcBangDieuKhien.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.UserControls
{
    partial class UcBangDieuKhien
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Hủy đăng ký event tĩnh HoaDonBLL (xem giải thích ở OnHandleDestroyed trong UcBangDieuKhien.cs)
                HuyDangKySuKien();
            }
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            components = new System.ComponentModel.Container();
            splNoiDung = new SplitContainer();
            bdc7Ngay = new QuanLyBanSach.GUI.UserControls.BieuDoCot();
            dgvTop5 = new DataGridView();
            colTopTenSach = new DataGridViewTextBoxColumn();
            colTopSoLuong = new DataGridViewTextBoxColumn();
            colTopDoanhThu = new DataGridViewTextBoxColumn();
            lblTop5 = new Label();
            tlpThe = new TableLayoutPanel();
            pnlTheDoanhThu = new Panel();
            lblGiaTriDoanhThu = new Label();
            lblTenDoanhThu = new Label();
            pnlTheSoHoaDon = new Panel();
            lblGiaTriSoHoaDon = new Label();
            lblTenSoHoaDon = new Label();
            pnlTheSoCuon = new Panel();
            lblGiaTriSoCuon = new Label();
            lblTenSoCuon = new Label();
            pnlTheSapHet = new Panel();
            lblGiaTriSapHet = new Label();
            lblTenSapHet = new Label();
            pnlTieuDe = new Panel();
            lblTieuDe = new Label();
            lblLoi = new Label();
            lblCapNhatLuc = new Label();
            btnLamMoi = new Button();
            tmrLamMoi = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splNoiDung).BeginInit();
            splNoiDung.Panel1.SuspendLayout();
            splNoiDung.Panel2.SuspendLayout();
            splNoiDung.SuspendLayout();
            tlpThe.SuspendLayout();
            pnlTheDoanhThu.SuspendLayout();
            pnlTheSoHoaDon.SuspendLayout();
            pnlTheSoCuon.SuspendLayout();
            pnlTheSapHet.SuspendLayout();
            pnlTieuDe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTop5).BeginInit();
            SuspendLayout();
            // 
            // splNoiDung
            // 
            splNoiDung.Dock = DockStyle.Fill;
            splNoiDung.Location = new Point(0, 0);
            splNoiDung.Name = "splNoiDung";
            splNoiDung.Panel1.Controls.Add(bdc7Ngay);
            splNoiDung.Panel2.Controls.Add(dgvTop5);
            splNoiDung.Panel2.Controls.Add(lblTop5);
            splNoiDung.Size = new Size(1100, 452);
            splNoiDung.SplitterDistance = 640;
            splNoiDung.TabIndex = 2;
            // 
            // bdc7Ngay
            // 
            bdc7Ngay.BackColor = Color.White;
            bdc7Ngay.Dock = DockStyle.Fill;
            bdc7Ngay.DonViTruc = "nghìn đồng";
            bdc7Ngay.HeSoTruc = new decimal(new int[] { 1000, 0, 0, 0 });
            bdc7Ngay.LaTien = true;
            bdc7Ngay.Location = new Point(0, 0);
            bdc7Ngay.Name = "bdc7Ngay";
            bdc7Ngay.Size = new Size(640, 452);
            bdc7Ngay.TabIndex = 0;
            bdc7Ngay.TieuDe = "Doanh thu 7 ngày gần nhất";
            // 
            // dgvTop5
            // 
            dgvTop5.AllowUserToAddRows = false;
            dgvTop5.AllowUserToDeleteRows = false;
            dgvTop5.AllowUserToResizeRows = false;
            dgvTop5.AutoGenerateColumns = false;
            dgvTop5.BackgroundColor = SystemColors.Window;
            dgvTop5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTop5.Columns.AddRange(new DataGridViewColumn[] { colTopTenSach, colTopSoLuong, colTopDoanhThu });
            dgvTop5.Dock = DockStyle.Fill;
            dgvTop5.Location = new Point(0, 32);
            dgvTop5.MultiSelect = false;
            dgvTop5.Name = "dgvTop5";
            dgvTop5.ReadOnly = true;
            dgvTop5.RowHeadersVisible = false;
            dgvTop5.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTop5.Size = new Size(456, 420);
            dgvTop5.TabIndex = 0;
            dgvTop5.TabStop = false;
            // 
            // colTopTenSach
            // 
            colTopTenSach.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTopTenSach.DataPropertyName = "TenSach";
            colTopTenSach.HeaderText = "Tên sách";
            colTopTenSach.Name = "colTopTenSach";
            colTopTenSach.ReadOnly = true;
            // 
            // colTopSoLuong
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTopSoLuong.DataPropertyName = "SoLuongBan";
            colTopSoLuong.DefaultCellStyle = dataGridViewCellStyle1;
            colTopSoLuong.HeaderText = "SL";
            colTopSoLuong.Name = "colTopSoLuong";
            colTopSoLuong.ReadOnly = true;
            colTopSoLuong.Width = 60;
            // 
            // colTopDoanhThu
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            colTopDoanhThu.DataPropertyName = "DoanhThu";
            colTopDoanhThu.DefaultCellStyle = dataGridViewCellStyle2;
            colTopDoanhThu.HeaderText = "Doanh thu";
            colTopDoanhThu.Name = "colTopDoanhThu";
            colTopDoanhThu.ReadOnly = true;
            colTopDoanhThu.Width = 110;
            // 
            // lblTop5
            // 
            lblTop5.Dock = DockStyle.Top;
            lblTop5.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTop5.Location = new Point(0, 0);
            lblTop5.Name = "lblTop5";
            lblTop5.Size = new Size(456, 32);
            lblTop5.Text = "Top 5 sách bán chạy hôm nay";
            lblTop5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tlpThe
            // 
            tlpThe.ColumnCount = 4;
            tlpThe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpThe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpThe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpThe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpThe.Controls.Add(pnlTheDoanhThu, 0, 0);
            tlpThe.Controls.Add(pnlTheSoHoaDon, 1, 0);
            tlpThe.Controls.Add(pnlTheSoCuon, 2, 0);
            tlpThe.Controls.Add(pnlTheSapHet, 3, 0);
            tlpThe.Dock = DockStyle.Top;
            tlpThe.Location = new Point(0, 52);
            tlpThe.Name = "tlpThe";
            tlpThe.Padding = new Padding(4);
            tlpThe.RowCount = 1;
            tlpThe.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpThe.Size = new Size(1100, 112);
            tlpThe.TabIndex = 1;
            // 
            // pnlTheDoanhThu
            // 
            pnlTheDoanhThu.BackColor = Color.FromArgb(223, 230, 239);
            pnlTheDoanhThu.BorderStyle = BorderStyle.FixedSingle;
            pnlTheDoanhThu.Controls.Add(lblGiaTriDoanhThu);
            pnlTheDoanhThu.Controls.Add(lblTenDoanhThu);
            pnlTheDoanhThu.Dock = DockStyle.Fill;
            pnlTheDoanhThu.Location = new Point(10, 10);
            pnlTheDoanhThu.Margin = new Padding(6);
            pnlTheDoanhThu.Name = "pnlTheDoanhThu";
            pnlTheDoanhThu.Size = new Size(261, 92);
            pnlTheDoanhThu.TabIndex = 0;
            // 
            // lblGiaTriDoanhThu
            // 
            lblGiaTriDoanhThu.Dock = DockStyle.Fill;
            lblGiaTriDoanhThu.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblGiaTriDoanhThu.ForeColor = Color.FromArgb(14, 72, 116);
            lblGiaTriDoanhThu.Location = new Point(0, 30);
            lblGiaTriDoanhThu.Name = "lblGiaTriDoanhThu";
            lblGiaTriDoanhThu.Size = new Size(259, 60);
            lblGiaTriDoanhThu.Text = "0đ";
            lblGiaTriDoanhThu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTenDoanhThu
            // 
            lblTenDoanhThu.Dock = DockStyle.Top;
            lblTenDoanhThu.ForeColor = Color.DimGray;
            lblTenDoanhThu.Location = new Point(0, 0);
            lblTenDoanhThu.Name = "lblTenDoanhThu";
            lblTenDoanhThu.Padding = new Padding(8, 0, 0, 0);
            lblTenDoanhThu.Size = new Size(259, 30);
            lblTenDoanhThu.Text = "Doanh thu hôm nay";
            lblTenDoanhThu.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnlTheSoHoaDon
            // 
            pnlTheSoHoaDon.BackColor = Color.FromArgb(223, 230, 239);
            pnlTheSoHoaDon.BorderStyle = BorderStyle.FixedSingle;
            pnlTheSoHoaDon.Controls.Add(lblGiaTriSoHoaDon);
            pnlTheSoHoaDon.Controls.Add(lblTenSoHoaDon);
            pnlTheSoHoaDon.Dock = DockStyle.Fill;
            pnlTheSoHoaDon.Location = new Point(283, 10);
            pnlTheSoHoaDon.Margin = new Padding(6);
            pnlTheSoHoaDon.Name = "pnlTheSoHoaDon";
            pnlTheSoHoaDon.Size = new Size(261, 92);
            pnlTheSoHoaDon.TabIndex = 1;
            // 
            // lblGiaTriSoHoaDon
            // 
            lblGiaTriSoHoaDon.Dock = DockStyle.Fill;
            lblGiaTriSoHoaDon.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblGiaTriSoHoaDon.ForeColor = Color.FromArgb(14, 72, 116);
            lblGiaTriSoHoaDon.Location = new Point(0, 30);
            lblGiaTriSoHoaDon.Name = "lblGiaTriSoHoaDon";
            lblGiaTriSoHoaDon.Size = new Size(259, 60);
            lblGiaTriSoHoaDon.Text = "0";
            lblGiaTriSoHoaDon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTenSoHoaDon
            // 
            lblTenSoHoaDon.Dock = DockStyle.Top;
            lblTenSoHoaDon.ForeColor = Color.DimGray;
            lblTenSoHoaDon.Location = new Point(0, 0);
            lblTenSoHoaDon.Name = "lblTenSoHoaDon";
            lblTenSoHoaDon.Padding = new Padding(8, 0, 0, 0);
            lblTenSoHoaDon.Size = new Size(259, 30);
            lblTenSoHoaDon.Text = "Số hóa đơn";
            lblTenSoHoaDon.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnlTheSoCuon
            // 
            pnlTheSoCuon.BackColor = Color.FromArgb(223, 230, 239);
            pnlTheSoCuon.BorderStyle = BorderStyle.FixedSingle;
            pnlTheSoCuon.Controls.Add(lblGiaTriSoCuon);
            pnlTheSoCuon.Controls.Add(lblTenSoCuon);
            pnlTheSoCuon.Dock = DockStyle.Fill;
            pnlTheSoCuon.Location = new Point(556, 10);
            pnlTheSoCuon.Margin = new Padding(6);
            pnlTheSoCuon.Name = "pnlTheSoCuon";
            pnlTheSoCuon.Size = new Size(261, 92);
            pnlTheSoCuon.TabIndex = 2;
            // 
            // lblGiaTriSoCuon
            // 
            lblGiaTriSoCuon.Dock = DockStyle.Fill;
            lblGiaTriSoCuon.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblGiaTriSoCuon.ForeColor = Color.FromArgb(14, 72, 116);
            lblGiaTriSoCuon.Location = new Point(0, 30);
            lblGiaTriSoCuon.Name = "lblGiaTriSoCuon";
            lblGiaTriSoCuon.Size = new Size(259, 60);
            lblGiaTriSoCuon.Text = "0";
            lblGiaTriSoCuon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTenSoCuon
            // 
            lblTenSoCuon.Dock = DockStyle.Top;
            lblTenSoCuon.ForeColor = Color.DimGray;
            lblTenSoCuon.Location = new Point(0, 0);
            lblTenSoCuon.Name = "lblTenSoCuon";
            lblTenSoCuon.Padding = new Padding(8, 0, 0, 0);
            lblTenSoCuon.Size = new Size(259, 30);
            lblTenSoCuon.Text = "Số cuốn bán";
            lblTenSoCuon.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pnlTheSapHet
            // 
            pnlTheSapHet.BackColor = Color.FromArgb(223, 230, 239);
            pnlTheSapHet.BorderStyle = BorderStyle.FixedSingle;
            pnlTheSapHet.Controls.Add(lblGiaTriSapHet);
            pnlTheSapHet.Controls.Add(lblTenSapHet);
            pnlTheSapHet.Cursor = Cursors.Hand;
            pnlTheSapHet.Dock = DockStyle.Fill;
            pnlTheSapHet.Location = new Point(829, 10);
            pnlTheSapHet.Margin = new Padding(6);
            pnlTheSapHet.Name = "pnlTheSapHet";
            pnlTheSapHet.Size = new Size(261, 92);
            pnlTheSapHet.TabIndex = 3;
            pnlTheSapHet.Click += theSapHet_Click;
            // 
            // lblGiaTriSapHet
            // 
            lblGiaTriSapHet.Dock = DockStyle.Fill;
            lblGiaTriSapHet.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblGiaTriSapHet.ForeColor = Color.FromArgb(14, 72, 116);
            lblGiaTriSapHet.Location = new Point(0, 30);
            lblGiaTriSapHet.Name = "lblGiaTriSapHet";
            lblGiaTriSapHet.Size = new Size(259, 60);
            lblGiaTriSapHet.Text = "0";
            lblGiaTriSapHet.TextAlign = ContentAlignment.MiddleCenter;
            lblGiaTriSapHet.Click += theSapHet_Click;
            // 
            // lblTenSapHet
            // 
            lblTenSapHet.Dock = DockStyle.Top;
            lblTenSapHet.ForeColor = Color.DimGray;
            lblTenSapHet.Location = new Point(0, 0);
            lblTenSapHet.Name = "lblTenSapHet";
            lblTenSapHet.Padding = new Padding(8, 0, 0, 0);
            lblTenSapHet.Size = new Size(259, 30);
            lblTenSapHet.Text = "Sách sắp hết (nhấp để xem)";
            lblTenSapHet.TextAlign = ContentAlignment.BottomLeft;
            lblTenSapHet.Click += theSapHet_Click;
            // 
            // pnlTieuDe
            // 
            pnlTieuDe.Controls.Add(lblTieuDe);
            pnlTieuDe.Controls.Add(lblLoi);
            pnlTieuDe.Controls.Add(lblCapNhatLuc);
            pnlTieuDe.Controls.Add(btnLamMoi);
            pnlTieuDe.Dock = DockStyle.Top;
            pnlTieuDe.Location = new Point(0, 0);
            pnlTieuDe.Name = "pnlTieuDe";
            pnlTieuDe.Size = new Size(1100, 52);
            pnlTieuDe.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(14, 72, 116);
            lblTieuDe.Location = new Point(10, 4);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(260, 25);
            lblTieuDe.Text = "Bảng điều khiển";
            // 
            // lblLoi
            // 
            lblLoi.AutoSize = true;
            lblLoi.Font = new Font("Segoe UI", 8.5F);
            lblLoi.ForeColor = Color.Firebrick;
            lblLoi.Location = new Point(14, 32);
            lblLoi.Name = "lblLoi";
            lblLoi.Size = new Size(0, 15);
            // 
            // lblCapNhatLuc
            // 
            lblCapNhatLuc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCapNhatLuc.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblCapNhatLuc.ForeColor = Color.DimGray;
            lblCapNhatLuc.Location = new Point(700, 16);
            lblCapNhatLuc.Name = "lblCapNhatLuc";
            lblCapNhatLuc.Size = new Size(250, 19);
            lblCapNhatLuc.Text = "Cập nhật lúc: --:--:--";
            lblCapNhatLuc.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLamMoi.Location = new Point(960, 9);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(130, 34);
            btnLamMoi.TabIndex = 0;
            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // tmrLamMoi
            // 
            tmrLamMoi.Interval = 300000;
            tmrLamMoi.Tick += tmrLamMoi_Tick;
            // 
            // UcBangDieuKhien
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            Controls.Add(splNoiDung);
            Controls.Add(tlpThe);
            Controls.Add(pnlTieuDe);
            Font = new Font("Segoe UI", 10F);
            Name = "UcBangDieuKhien";
            Size = new Size(1100, 616);
            splNoiDung.Panel1.ResumeLayout(false);
            splNoiDung.Panel2.ResumeLayout(false);
            splNoiDung.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splNoiDung).EndInit();
            splNoiDung.ResumeLayout(false);
            splNoiDung.PerformLayout();
            tlpThe.ResumeLayout(false);
            tlpThe.PerformLayout();
            pnlTheDoanhThu.ResumeLayout(false);
            pnlTheDoanhThu.PerformLayout();
            pnlTheSoHoaDon.ResumeLayout(false);
            pnlTheSoHoaDon.PerformLayout();
            pnlTheSoCuon.ResumeLayout(false);
            pnlTheSoCuon.PerformLayout();
            pnlTheSapHet.ResumeLayout(false);
            pnlTheSapHet.PerformLayout();
            pnlTieuDe.ResumeLayout(false);
            pnlTieuDe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTop5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splNoiDung;
        private QuanLyBanSach.GUI.UserControls.BieuDoCot bdc7Ngay;
        private DataGridView dgvTop5;
        private DataGridViewTextBoxColumn colTopTenSach;
        private DataGridViewTextBoxColumn colTopSoLuong;
        private DataGridViewTextBoxColumn colTopDoanhThu;
        private Label lblTop5;
        private TableLayoutPanel tlpThe;
        private Panel pnlTheDoanhThu;
        private Label lblGiaTriDoanhThu;
        private Label lblTenDoanhThu;
        private Panel pnlTheSoHoaDon;
        private Label lblGiaTriSoHoaDon;
        private Label lblTenSoHoaDon;
        private Panel pnlTheSoCuon;
        private Label lblGiaTriSoCuon;
        private Label lblTenSoCuon;
        private Panel pnlTheSapHet;
        private Label lblGiaTriSapHet;
        private Label lblTenSapHet;
        private Panel pnlTieuDe;
        private Label lblTieuDe;
        private Label lblLoi;
        private Label lblCapNhatLuc;
        private Button btnLamMoi;
        private System.Windows.Forms.Timer tmrLamMoi;
    }
}
