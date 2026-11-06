// File: QuanLyBanSach.GUI/Forms/FrmKhachHang.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmKhachHang
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            components = new System.ComponentModel.Container();
            grpThongTin = new GroupBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblSDT = new Label();
            txtSDT = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblDiaChi = new Label();
            txtDiaChi = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLuu = new Button();
            btnHuy = new Button();
            btnChon = new Button();
            btnDong = new Button();
            pnlTim = new Panel();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            lblSoKhach = new Label();
            splKhachHang = new SplitContainer();
            dgvKhachHang = new DataGridView();
            colMaKH = new DataGridViewTextBoxColumn();
            colHoTen = new DataGridViewTextBoxColumn();
            colSDT = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colDiaChi = new DataGridViewTextBoxColumn();
            dgvLichSu = new DataGridView();
            lblTongKet = new Label();
            lblLichSu = new Label();
            colLsMaHD = new DataGridViewTextBoxColumn();
            colLsNgayLap = new DataGridViewTextBoxColumn();
            colLsTongThanhToan = new DataGridViewTextBoxColumn();
            colLsTrangThai = new DataGridViewTextBoxColumn();
            errKhachHang = new ErrorProvider(components);
            tmrTimKiem = new System.Windows.Forms.Timer(components);
            grpThongTin.SuspendLayout();
            pnlTim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splKhachHang).BeginInit();
            splKhachHang.Panel1.SuspendLayout();
            splKhachHang.Panel2.SuspendLayout();
            splKhachHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLichSu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errKhachHang).BeginInit();
            SuspendLayout();
            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(lblHoTen);
            grpThongTin.Controls.Add(txtHoTen);
            grpThongTin.Controls.Add(lblSDT);
            grpThongTin.Controls.Add(txtSDT);
            grpThongTin.Controls.Add(lblEmail);
            grpThongTin.Controls.Add(txtEmail);
            grpThongTin.Controls.Add(lblDiaChi);
            grpThongTin.Controls.Add(txtDiaChi);
            grpThongTin.Controls.Add(btnThem);
            grpThongTin.Controls.Add(btnSua);
            grpThongTin.Controls.Add(btnXoa);
            grpThongTin.Controls.Add(btnLuu);
            grpThongTin.Controls.Add(btnHuy);
            grpThongTin.Controls.Add(btnChon);
            grpThongTin.Controls.Add(btnDong);
            grpThongTin.Dock = DockStyle.Top;
            grpThongTin.Location = new Point(0, 0);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(960, 160);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin khách hàng";
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(16, 35);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(88, 19);
            lblHoTen.Text = "Họ tên (*):";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(110, 32);
            txtHoTen.MaxLength = 100;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(300, 25);
            txtHoTen.TabIndex = 0;
            // 
            // lblSDT
            // 
            lblSDT.AutoSize = true;
            lblSDT.Location = new Point(430, 35);
            lblSDT.Name = "lblSDT";
            lblSDT.Size = new Size(64, 19);
            lblSDT.Text = "SĐT (*):";
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(500, 32);
            txtSDT.MaxLength = 10;
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(160, 25);
            txtSDT.TabIndex = 1;
            txtSDT.KeyPress += txtSDT_KeyPress;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(16, 73);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(48, 19);
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(110, 70);
            txtEmail.MaxLength = 100;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 25);
            txtEmail.TabIndex = 2;
            // 
            // lblDiaChi
            // 
            lblDiaChi.AutoSize = true;
            lblDiaChi.Location = new Point(430, 73);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(64, 19);
            lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(500, 70);
            txtDiaChi.MaxLength = 255;
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(440, 25);
            txtDiaChi.TabIndex = 3;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(110, 112);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(96, 34);
            btnThem.TabIndex = 4;
            btnThem.Text = "➕ Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(212, 112);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(96, 34);
            btnSua.TabIndex = 5;
            btnSua.Text = "✏️ Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(314, 112);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(96, 34);
            btnXoa.TabIndex = 6;
            btnXoa.Text = "🗑 Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(416, 112);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(96, 34);
            btnLuu.TabIndex = 7;
            btnLuu.Text = "💾 Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(518, 112);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(96, 34);
            btnHuy.TabIndex = 8;
            btnHuy.Text = "↩ Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnChon
            // 
            btnChon.Location = new Point(640, 112);
            btnChon.Name = "btnChon";
            btnChon.Size = new Size(130, 34);
            btnChon.TabIndex = 9;
            btnChon.Text = "✔ Chọn khách";
            btnChon.UseVisualStyleBackColor = true;
            btnChon.Visible = false;
            btnChon.Click += btnChon_Click;
            // 
            // btnDong
            // 
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.Location = new Point(776, 112);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(90, 34);
            btnDong.TabIndex = 10;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            btnDong.Visible = false;
            btnDong.Click += btnDong_Click;
            // 
            // pnlTim
            // 
            pnlTim.Controls.Add(lblTimKiem);
            pnlTim.Controls.Add(txtTimKiem);
            pnlTim.Controls.Add(lblSoKhach);
            pnlTim.Dock = DockStyle.Top;
            pnlTim.Location = new Point(0, 160);
            pnlTim.Name = "pnlTim";
            pnlTim.Size = new Size(960, 44);
            pnlTim.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(12, 13);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(152, 19);
            lblTimKiem.Text = "Tìm (mã, tên, SĐT):";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(150, 10);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(260, 25);
            txtTimKiem.TabIndex = 0;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // lblSoKhach
            // 
            lblSoKhach.AutoSize = true;
            lblSoKhach.Location = new Point(430, 13);
            lblSoKhach.Name = "lblSoKhach";
            lblSoKhach.Size = new Size(100, 19);
            lblSoKhach.Text = "0 khách hàng";
            // 
            // splKhachHang
            // 
            splKhachHang.Dock = DockStyle.Fill;
            splKhachHang.Location = new Point(0, 204);
            splKhachHang.Name = "splKhachHang";
            splKhachHang.Orientation = Orientation.Horizontal;
            splKhachHang.Panel1.Controls.Add(dgvKhachHang);
            splKhachHang.Panel2.Controls.Add(dgvLichSu);
            splKhachHang.Panel2.Controls.Add(lblTongKet);
            splKhachHang.Panel2.Controls.Add(lblLichSu);
            splKhachHang.Size = new Size(960, 436);
            splKhachHang.SplitterDistance = 230;
            splKhachHang.TabIndex = 2;
            // 
            // dgvKhachHang
            // 
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.AllowUserToDeleteRows = false;
            dgvKhachHang.AllowUserToResizeRows = false;
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.BackgroundColor = SystemColors.Window;
            dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhachHang.Columns.AddRange(new DataGridViewColumn[] { colMaKH, colHoTen, colSDT, colEmail, colDiaChi });
            dgvKhachHang.Dock = DockStyle.Fill;
            dgvKhachHang.Location = new Point(0, 0);
            dgvKhachHang.MultiSelect = false;
            dgvKhachHang.Name = "dgvKhachHang";
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.Size = new Size(960, 230);
            dgvKhachHang.TabIndex = 0;
            dgvKhachHang.CellDoubleClick += dgvKhachHang_CellDoubleClick;
            dgvKhachHang.SelectionChanged += dgvKhachHang_SelectionChanged;
            // 
            // colMaKH
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaKH.DataPropertyName = "MaKH";
            colMaKH.DefaultCellStyle = dataGridViewCellStyle1;
            colMaKH.HeaderText = "Mã";
            colMaKH.Name = "colMaKH";
            colMaKH.ReadOnly = true;
            colMaKH.Width = 60;
            // 
            // colHoTen
            // 
            colHoTen.DataPropertyName = "HoTen";
            colHoTen.HeaderText = "Họ tên";
            colHoTen.Name = "colHoTen";
            colHoTen.ReadOnly = true;
            colHoTen.Width = 220;
            // 
            // colSDT
            // 
            colSDT.DataPropertyName = "SDT";
            colSDT.HeaderText = "SĐT";
            colSDT.Name = "colSDT";
            colSDT.ReadOnly = true;
            colSDT.Width = 120;
            // 
            // colEmail
            // 
            colEmail.DataPropertyName = "Email";
            colEmail.HeaderText = "Email";
            colEmail.Name = "colEmail";
            colEmail.ReadOnly = true;
            colEmail.Width = 220;
            // 
            // colDiaChi
            // 
            colDiaChi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDiaChi.DataPropertyName = "DiaChi";
            colDiaChi.HeaderText = "Địa chỉ";
            colDiaChi.Name = "colDiaChi";
            colDiaChi.ReadOnly = true;
            // 
            // dgvLichSu
            // 
            dgvLichSu.AllowUserToAddRows = false;
            dgvLichSu.AllowUserToDeleteRows = false;
            dgvLichSu.AllowUserToResizeRows = false;
            dgvLichSu.AutoGenerateColumns = false;
            dgvLichSu.BackgroundColor = SystemColors.Window;
            dgvLichSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichSu.Columns.AddRange(new DataGridViewColumn[] { colLsMaHD, colLsNgayLap, colLsTongThanhToan, colLsTrangThai });
            dgvLichSu.Dock = DockStyle.Fill;
            dgvLichSu.Location = new Point(0, 28);
            dgvLichSu.MultiSelect = false;
            dgvLichSu.Name = "dgvLichSu";
            dgvLichSu.ReadOnly = true;
            dgvLichSu.RowHeadersVisible = false;
            dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLichSu.Size = new Size(960, 146);
            dgvLichSu.TabIndex = 1;
            dgvLichSu.CellFormatting += dgvLichSu_CellFormatting;
            // 
            // lblTongKet
            // 
            lblTongKet.Dock = DockStyle.Bottom;
            lblTongKet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTongKet.ForeColor = Color.FromArgb(14, 72, 116);
            lblTongKet.Location = new Point(0, 174);
            lblTongKet.Name = "lblTongKet";
            lblTongKet.Padding = new Padding(6, 0, 0, 0);
            lblTongKet.Size = new Size(960, 28);
            lblTongKet.TabIndex = 2;
            lblTongKet.Text = "Chưa chọn khách hàng";
            lblTongKet.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLichSu
            // 
            lblLichSu.Dock = DockStyle.Top;
            lblLichSu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLichSu.Location = new Point(0, 0);
            lblLichSu.Name = "lblLichSu";
            lblLichSu.Padding = new Padding(6, 0, 0, 0);
            lblLichSu.Size = new Size(960, 28);
            lblLichSu.TabIndex = 0;
            lblLichSu.Text = "Lịch sử mua hàng";
            lblLichSu.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // colLsMaHD
            // 
            colLsMaHD.DataPropertyName = "MaHD";
            colLsMaHD.HeaderText = "Mã hóa đơn";
            colLsMaHD.Name = "colLsMaHD";
            colLsMaHD.ReadOnly = true;
            colLsMaHD.Width = 150;
            // 
            // colLsNgayLap
            // 
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm";
            colLsNgayLap.DataPropertyName = "NgayLap";
            colLsNgayLap.DefaultCellStyle = dataGridViewCellStyle2;
            colLsNgayLap.HeaderText = "Ngày lập";
            colLsNgayLap.Name = "colLsNgayLap";
            colLsNgayLap.ReadOnly = true;
            colLsNgayLap.Width = 160;
            // 
            // colLsTongThanhToan
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            colLsTongThanhToan.DataPropertyName = "TongThanhToan";
            colLsTongThanhToan.DefaultCellStyle = dataGridViewCellStyle3;
            colLsTongThanhToan.HeaderText = "Tổng thanh toán";
            colLsTongThanhToan.Name = "colLsTongThanhToan";
            colLsTongThanhToan.ReadOnly = true;
            colLsTongThanhToan.Width = 150;
            // 
            // colLsTrangThai
            // 
            colLsTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colLsTrangThai.DataPropertyName = "TrangThaiText";
            colLsTrangThai.HeaderText = "Trạng thái";
            colLsTrangThai.Name = "colLsTrangThai";
            colLsTrangThai.ReadOnly = true;
            // 
            // errKhachHang
            // 
            errKhachHang.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errKhachHang.ContainerControl = this;
            // 
            // tmrTimKiem
            // 
            tmrTimKiem.Interval = 300;
            tmrTimKiem.Tick += tmrTimKiem_Tick;
            // 
            // FrmKhachHang
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(960, 640);
            Controls.Add(splKhachHang);
            Controls.Add(pnlTim);
            Controls.Add(grpThongTin);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(900, 560);
            Name = "FrmKhachHang";
            Text = "Khách hàng";
            Load += FrmKhachHang_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            pnlTim.ResumeLayout(false);
            pnlTim.PerformLayout();
            splKhachHang.Panel1.ResumeLayout(false);
            splKhachHang.Panel2.ResumeLayout(false);
            splKhachHang.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splKhachHang).EndInit();
            splKhachHang.ResumeLayout(false);
            splKhachHang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLichSu).EndInit();
            ((System.ComponentModel.ISupportInitialize)errKhachHang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblSDT;
        private TextBox txtSDT;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblDiaChi;
        private TextBox txtDiaChi;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLuu;
        private Button btnHuy;
        private Button btnChon;
        private Button btnDong;
        private Panel pnlTim;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Label lblSoKhach;
        private SplitContainer splKhachHang;
        private DataGridView dgvKhachHang;
        private DataGridViewTextBoxColumn colMaKH;
        private DataGridViewTextBoxColumn colHoTen;
        private DataGridViewTextBoxColumn colSDT;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colDiaChi;
        private DataGridView dgvLichSu;
        private Label lblTongKet;
        private Label lblLichSu;
        private DataGridViewTextBoxColumn colLsMaHD;
        private DataGridViewTextBoxColumn colLsNgayLap;
        private DataGridViewTextBoxColumn colLsTongThanhToan;
        private DataGridViewTextBoxColumn colLsTrangThai;
        private ErrorProvider errKhachHang;
        private System.Windows.Forms.Timer tmrTimKiem;
    }
}
