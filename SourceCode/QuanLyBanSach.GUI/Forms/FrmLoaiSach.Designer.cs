// File: QuanLyBanSach.GUI/Forms/FrmLoaiSach.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmLoaiSach
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
            components = new System.ComponentModel.Container();
            grpThongTin = new GroupBox();
            lblTenLoai = new Label();
            txtTenLoai = new TextBox();
            lblMoTa = new Label();
            txtMoTa = new TextBox();
            pnlNut = new Panel();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLuu = new Button();
            btnHuy = new Button();
            btnLamMoi = new Button();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            dgvLoaiSach = new DataGridView();
            colMaLoai = new DataGridViewTextBoxColumn();
            colTenLoai = new DataGridViewTextBoxColumn();
            colMoTa = new DataGridViewTextBoxColumn();
            lblTongSo = new Label();
            errLoaiSach = new ErrorProvider(components);
            grpThongTin.SuspendLayout();
            pnlNut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLoaiSach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errLoaiSach).BeginInit();
            SuspendLayout();
            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(lblTenLoai);
            grpThongTin.Controls.Add(txtTenLoai);
            grpThongTin.Controls.Add(lblMoTa);
            grpThongTin.Controls.Add(txtMoTa);
            grpThongTin.Dock = DockStyle.Top;
            grpThongTin.Location = new Point(0, 0);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Padding = new Padding(10);
            grpThongTin.Size = new Size(900, 112);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin loại sách";
            // 
            // lblTenLoai
            // 
            lblTenLoai.AutoSize = true;
            lblTenLoai.Location = new Point(16, 35);
            lblTenLoai.Name = "lblTenLoai";
            lblTenLoai.Size = new Size(104, 19);
            lblTenLoai.Text = "Tên loại (*):";
            // 
            // txtTenLoai
            // 
            txtTenLoai.Location = new Point(120, 32);
            txtTenLoai.MaxLength = 100;
            txtTenLoai.Name = "txtTenLoai";
            txtTenLoai.Size = new Size(360, 25);
            txtTenLoai.TabIndex = 0;
            // 
            // lblMoTa
            // 
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(16, 72);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(48, 19);
            lblMoTa.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            txtMoTa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMoTa.Location = new Point(120, 69);
            txtMoTa.MaxLength = 255;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(640, 25);
            txtMoTa.TabIndex = 1;
            // 
            // pnlNut
            // 
            pnlNut.Controls.Add(btnThem);
            pnlNut.Controls.Add(btnSua);
            pnlNut.Controls.Add(btnXoa);
            pnlNut.Controls.Add(btnLuu);
            pnlNut.Controls.Add(btnHuy);
            pnlNut.Controls.Add(btnLamMoi);
            pnlNut.Controls.Add(lblTimKiem);
            pnlNut.Controls.Add(txtTimKiem);
            pnlNut.Dock = DockStyle.Top;
            pnlNut.Location = new Point(0, 112);
            pnlNut.Name = "pnlNut";
            pnlNut.Size = new Size(900, 52);
            pnlNut.TabIndex = 1;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(12, 9);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(96, 34);
            btnThem.TabIndex = 0;
            btnThem.Text = "➕ Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(112, 9);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(96, 34);
            btnSua.TabIndex = 1;
            btnSua.Text = "✏️ Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(212, 9);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(96, 34);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "🗑 Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(312, 9);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(96, 34);
            btnLuu.TabIndex = 3;
            btnLuu.Text = "💾 Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(412, 9);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(96, 34);
            btnHuy.TabIndex = 4;
            btnHuy.Text = "↩ Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(512, 9);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(96, 34);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(626, 16);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(72, 19);
            lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(700, 13);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(190, 25);
            txtTimKiem.TabIndex = 6;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // dgvLoaiSach
            // 
            dgvLoaiSach.AllowUserToAddRows = false;
            dgvLoaiSach.AllowUserToDeleteRows = false;
            dgvLoaiSach.AllowUserToResizeRows = false;
            dgvLoaiSach.AutoGenerateColumns = false;
            dgvLoaiSach.BackgroundColor = SystemColors.Window;
            dgvLoaiSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLoaiSach.Columns.AddRange(new DataGridViewColumn[] { colMaLoai, colTenLoai, colMoTa });
            dgvLoaiSach.Dock = DockStyle.Fill;
            dgvLoaiSach.Location = new Point(0, 164);
            dgvLoaiSach.MultiSelect = false;
            dgvLoaiSach.Name = "dgvLoaiSach";
            dgvLoaiSach.ReadOnly = true;
            dgvLoaiSach.RowHeadersVisible = false;
            dgvLoaiSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiSach.Size = new Size(900, 400);
            dgvLoaiSach.TabIndex = 2;
            dgvLoaiSach.SelectionChanged += dgvLoaiSach_SelectionChanged;
            // 
            // colMaLoai
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaLoai.DataPropertyName = "MaLoai";
            colMaLoai.DefaultCellStyle = dataGridViewCellStyle1;
            colMaLoai.HeaderText = "Mã loại";
            colMaLoai.Name = "colMaLoai";
            colMaLoai.ReadOnly = true;
            colMaLoai.Width = 90;
            // 
            // colTenLoai
            // 
            colTenLoai.DataPropertyName = "TenLoai";
            colTenLoai.HeaderText = "Tên loại";
            colTenLoai.Name = "colTenLoai";
            colTenLoai.ReadOnly = true;
            colTenLoai.Width = 260;
            // 
            // colMoTa
            // 
            colMoTa.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMoTa.DataPropertyName = "MoTa";
            colMoTa.HeaderText = "Mô tả";
            colMoTa.Name = "colMoTa";
            colMoTa.ReadOnly = true;
            // 
            // lblTongSo
            // 
            lblTongSo.Dock = DockStyle.Bottom;
            lblTongSo.Location = new Point(0, 564);
            lblTongSo.Name = "lblTongSo";
            lblTongSo.Padding = new Padding(8, 0, 0, 0);
            lblTongSo.Size = new Size(900, 36);
            lblTongSo.TabIndex = 3;
            lblTongSo.Text = "Tổng: 0 loại sách";
            lblTongSo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // errLoaiSach
            // 
            errLoaiSach.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errLoaiSach.ContainerControl = this;
            // 
            // FrmLoaiSach
            // 
            AcceptButton = btnLuu;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnHuy;
            ClientSize = new Size(900, 600);
            Controls.Add(dgvLoaiSach);
            Controls.Add(lblTongSo);
            Controls.Add(pnlNut);
            Controls.Add(grpThongTin);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(760, 480);
            Name = "FrmLoaiSach";
            Text = "Loại sách";
            Load += FrmLoaiSach_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            pnlNut.ResumeLayout(false);
            pnlNut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLoaiSach).EndInit();
            ((System.ComponentModel.ISupportInitialize)errLoaiSach).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblTenLoai;
        private TextBox txtTenLoai;
        private Label lblMoTa;
        private TextBox txtMoTa;
        private Panel pnlNut;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLuu;
        private Button btnHuy;
        private Button btnLamMoi;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private DataGridView dgvLoaiSach;
        private DataGridViewTextBoxColumn colMaLoai;
        private DataGridViewTextBoxColumn colTenLoai;
        private DataGridViewTextBoxColumn colMoTa;
        private Label lblTongSo;
        private ErrorProvider errLoaiSach;
    }
}
