// File: QuanLyBanSach.GUI/Forms/FrmTaiKhoan.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmTaiKhoan
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
            grpThongTin = new GroupBox();
            lblTenDangNhap = new Label();
            txtTenDangNhap = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblVaiTro = new Label();
            cboVaiTro = new ComboBox();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnLuu = new Button();
            btnHuy = new Button();
            btnKhoaMo = new Button();
            btnDatLaiMatKhau = new Button();
            btnLamMoi = new Button();
            dgvTaiKhoan = new DataGridView();
            colTenDangNhap = new DataGridViewTextBoxColumn();
            colHoTen = new DataGridViewTextBoxColumn();
            colVaiTro = new DataGridViewTextBoxColumn();
            colTrangThai = new DataGridViewTextBoxColumn();
            errTaiKhoan = new ErrorProvider(components);
            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errTaiKhoan).BeginInit();
            SuspendLayout();
            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(lblTenDangNhap);
            grpThongTin.Controls.Add(txtTenDangNhap);
            grpThongTin.Controls.Add(lblHoTen);
            grpThongTin.Controls.Add(txtHoTen);
            grpThongTin.Controls.Add(lblVaiTro);
            grpThongTin.Controls.Add(cboVaiTro);
            grpThongTin.Controls.Add(lblMatKhau);
            grpThongTin.Controls.Add(txtMatKhau);
            grpThongTin.Controls.Add(btnThem);
            grpThongTin.Controls.Add(btnSua);
            grpThongTin.Controls.Add(btnLuu);
            grpThongTin.Controls.Add(btnHuy);
            grpThongTin.Controls.Add(btnKhoaMo);
            grpThongTin.Controls.Add(btnDatLaiMatKhau);
            grpThongTin.Controls.Add(btnLamMoi);
            grpThongTin.Dock = DockStyle.Top;
            grpThongTin.Location = new Point(0, 0);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(900, 168);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin tài khoản";
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Location = new Point(16, 35);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(112, 19);
            lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(135, 32);
            txtTenDangNhap.MaxLength = 50;
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(220, 25);
            txtTenDangNhap.TabIndex = 0;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(380, 35);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(56, 19);
            lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(450, 32);
            txtHoTen.MaxLength = 100;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(300, 25);
            txtHoTen.TabIndex = 1;
            // 
            // lblVaiTro
            // 
            lblVaiTro.AutoSize = true;
            lblVaiTro.Location = new Point(16, 73);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(64, 19);
            lblVaiTro.Text = "Vai trò:";
            // 
            // cboVaiTro
            // 
            cboVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVaiTro.FormattingEnabled = true;
            cboVaiTro.Location = new Point(135, 69);
            cboVaiTro.Name = "cboVaiTro";
            cboVaiTro.Size = new Size(220, 25);
            cboVaiTro.TabIndex = 2;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(380, 73);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(136, 19);
            lblMatKhau.Text = "Mật khẩu ban đầu:";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(510, 70);
            txtMatKhau.MaxLength = 100;
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(240, 25);
            txtMatKhau.TabIndex = 3;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(16, 114);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(96, 34);
            btnThem.TabIndex = 4;
            btnThem.Text = "➕ Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(118, 114);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(96, 34);
            btnSua.TabIndex = 5;
            btnSua.Text = "✏️ Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(220, 114);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(96, 34);
            btnLuu.TabIndex = 6;
            btnLuu.Text = "💾 Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(322, 114);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(96, 34);
            btnHuy.TabIndex = 7;
            btnHuy.Text = "↩ Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnKhoaMo
            // 
            btnKhoaMo.Location = new Point(424, 114);
            btnKhoaMo.Name = "btnKhoaMo";
            btnKhoaMo.Size = new Size(120, 34);
            btnKhoaMo.TabIndex = 8;
            btnKhoaMo.Text = "🔒 Khóa";
            btnKhoaMo.UseVisualStyleBackColor = true;
            btnKhoaMo.Click += btnKhoaMo_Click;
            // 
            // btnDatLaiMatKhau
            // 
            btnDatLaiMatKhau.Location = new Point(550, 114);
            btnDatLaiMatKhau.Name = "btnDatLaiMatKhau";
            btnDatLaiMatKhau.Size = new Size(170, 34);
            btnDatLaiMatKhau.TabIndex = 9;
            btnDatLaiMatKhau.Text = "🔑 Đặt lại mật khẩu";
            btnDatLaiMatKhau.UseVisualStyleBackColor = true;
            btnDatLaiMatKhau.Click += btnDatLaiMatKhau_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(726, 114);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 34);
            btnLamMoi.TabIndex = 10;
            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // dgvTaiKhoan
            // 
            dgvTaiKhoan.AllowUserToAddRows = false;
            dgvTaiKhoan.AllowUserToDeleteRows = false;
            dgvTaiKhoan.AllowUserToResizeRows = false;
            dgvTaiKhoan.AutoGenerateColumns = false;
            dgvTaiKhoan.BackgroundColor = SystemColors.Window;
            dgvTaiKhoan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTaiKhoan.Columns.AddRange(new DataGridViewColumn[] { colTenDangNhap, colHoTen, colVaiTro, colTrangThai });
            dgvTaiKhoan.Dock = DockStyle.Fill;
            dgvTaiKhoan.Location = new Point(0, 168);
            dgvTaiKhoan.MultiSelect = false;
            dgvTaiKhoan.Name = "dgvTaiKhoan";
            dgvTaiKhoan.ReadOnly = true;
            dgvTaiKhoan.RowHeadersVisible = false;
            dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaiKhoan.Size = new Size(900, 432);
            dgvTaiKhoan.TabIndex = 1;
            dgvTaiKhoan.CellFormatting += dgvTaiKhoan_CellFormatting;
            dgvTaiKhoan.SelectionChanged += dgvTaiKhoan_SelectionChanged;
            // 
            // colTenDangNhap
            // 
            colTenDangNhap.DataPropertyName = "TenDangNhap";
            colTenDangNhap.HeaderText = "Tên đăng nhập";
            colTenDangNhap.Name = "colTenDangNhap";
            colTenDangNhap.ReadOnly = true;
            colTenDangNhap.Width = 180;
            // 
            // colHoTen
            // 
            colHoTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colHoTen.DataPropertyName = "HoTen";
            colHoTen.HeaderText = "Họ tên";
            colHoTen.Name = "colHoTen";
            colHoTen.ReadOnly = true;
            // 
            // colVaiTro
            // 
            colVaiTro.DataPropertyName = "TenVaiTro";
            colVaiTro.HeaderText = "Vai trò";
            colVaiTro.Name = "colVaiTro";
            colVaiTro.ReadOnly = true;
            colVaiTro.Width = 150;
            // 
            // colTrangThai
            // 
            colTrangThai.DataPropertyName = "TrangThaiText";
            colTrangThai.HeaderText = "Trạng thái";
            colTrangThai.Name = "colTrangThai";
            colTrangThai.ReadOnly = true;
            colTrangThai.Width = 130;
            // 
            // errTaiKhoan
            // 
            errTaiKhoan.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errTaiKhoan.ContainerControl = this;
            // 
            // FrmTaiKhoan
            // 
            AcceptButton = btnLuu;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnHuy;
            ClientSize = new Size(900, 600);
            Controls.Add(dgvTaiKhoan);
            Controls.Add(grpThongTin);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(900, 480);
            Name = "FrmTaiKhoan";
            Text = "Tài khoản";
            Load += FrmTaiKhoan_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).EndInit();
            ((System.ComponentModel.ISupportInitialize)errTaiKhoan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblTenDangNhap;
        private TextBox txtTenDangNhap;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblVaiTro;
        private ComboBox cboVaiTro;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private Button btnThem;
        private Button btnSua;
        private Button btnLuu;
        private Button btnHuy;
        private Button btnKhoaMo;
        private Button btnDatLaiMatKhau;
        private Button btnLamMoi;
        private DataGridView dgvTaiKhoan;
        private DataGridViewTextBoxColumn colTenDangNhap;
        private DataGridViewTextBoxColumn colHoTen;
        private DataGridViewTextBoxColumn colVaiTro;
        private DataGridViewTextBoxColumn colTrangThai;
        private ErrorProvider errTaiKhoan;
    }
}
