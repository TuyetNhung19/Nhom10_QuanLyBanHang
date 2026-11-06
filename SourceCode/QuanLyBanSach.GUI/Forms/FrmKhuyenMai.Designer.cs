// File: QuanLyBanSach.GUI/Forms/FrmKhuyenMai.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmKhuyenMai
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
            lblTenKM = new Label();
            txtTenKM = new TextBox();
            lblLoaiKM = new Label();
            cboLoaiKM = new ComboBox();
            lblNguongTien = new Label();
            nudNguongTien = new NumericUpDown();
            lblLoaiSach = new Label();
            cboLoaiSach = new ComboBox();
            lblPhanTramGiam = new Label();
            nudPhanTramGiam = new NumericUpDown();
            lblNguongSoLuong = new Label();
            nudNguongSoLuong = new NumericUpDown();
            lblSoTienGiam = new Label();
            nudSoTienGiam = new NumericUpDown();
            lblBatDau = new Label();
            dtpBatDau = new DateTimePicker();
            lblKetThuc = new Label();
            dtpKetThuc = new DateTimePicker();
            chkDangBat = new CheckBox();
            lblMoTa = new Label();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLuu = new Button();
            btnHuy = new Button();
            btnLamMoi = new Button();
            dgvKhuyenMai = new DataGridView();
            colTenKM = new DataGridViewTextBoxColumn();
            colLoai = new DataGridViewTextBoxColumn();
            colDieuKien = new DataGridViewTextBoxColumn();
            colTuNgay = new DataGridViewTextBoxColumn();
            colDenNgay = new DataGridViewTextBoxColumn();
            colTinhTrang = new DataGridViewTextBoxColumn();
            errKhuyenMai = new ErrorProvider(components);
            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNguongTien).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPhanTramGiam).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNguongSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoTienGiam).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhuyenMai).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errKhuyenMai).BeginInit();
            SuspendLayout();
            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(lblTenKM);
            grpThongTin.Controls.Add(txtTenKM);
            grpThongTin.Controls.Add(lblLoaiKM);
            grpThongTin.Controls.Add(cboLoaiKM);
            grpThongTin.Controls.Add(lblNguongTien);
            grpThongTin.Controls.Add(nudNguongTien);
            grpThongTin.Controls.Add(lblLoaiSach);
            grpThongTin.Controls.Add(cboLoaiSach);
            grpThongTin.Controls.Add(lblPhanTramGiam);
            grpThongTin.Controls.Add(nudPhanTramGiam);
            grpThongTin.Controls.Add(lblNguongSoLuong);
            grpThongTin.Controls.Add(nudNguongSoLuong);
            grpThongTin.Controls.Add(lblSoTienGiam);
            grpThongTin.Controls.Add(nudSoTienGiam);
            grpThongTin.Controls.Add(lblBatDau);
            grpThongTin.Controls.Add(dtpBatDau);
            grpThongTin.Controls.Add(lblKetThuc);
            grpThongTin.Controls.Add(dtpKetThuc);
            grpThongTin.Controls.Add(chkDangBat);
            grpThongTin.Controls.Add(lblMoTa);
            grpThongTin.Controls.Add(btnThem);
            grpThongTin.Controls.Add(btnSua);
            grpThongTin.Controls.Add(btnXoa);
            grpThongTin.Controls.Add(btnLuu);
            grpThongTin.Controls.Add(btnHuy);
            grpThongTin.Controls.Add(btnLamMoi);
            grpThongTin.Dock = DockStyle.Top;
            grpThongTin.Location = new Point(0, 0);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(1000, 262);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin khuyến mãi";
            // 
            // lblTenKM
            // 
            lblTenKM.AutoSize = true;
            lblTenKM.Location = new Point(16, 35);
            lblTenKM.Name = "lblTenKM";
            lblTenKM.Size = new Size(152, 19);
            lblTenKM.Text = "Tên khuyến mãi (*):";
            // 
            // txtTenKM
            // 
            txtTenKM.Location = new Point(160, 32);
            txtTenKM.MaxLength = 100;
            txtTenKM.Name = "txtTenKM";
            txtTenKM.Size = new Size(380, 25);
            txtTenKM.TabIndex = 0;
            txtTenKM.TextChanged += GiaTriThayDoi;
            // 
            // lblLoaiKM
            // 
            lblLoaiKM.AutoSize = true;
            lblLoaiKM.Location = new Point(560, 35);
            lblLoaiKM.Name = "lblLoaiKM";
            lblLoaiKM.Size = new Size(40, 19);
            lblLoaiKM.Text = "Loại:";
            // 
            // cboLoaiKM
            // 
            cboLoaiKM.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiKM.FormattingEnabled = true;
            cboLoaiKM.Location = new Point(610, 31);
            cboLoaiKM.Name = "cboLoaiKM";
            cboLoaiKM.Size = new Size(210, 25);
            cboLoaiKM.TabIndex = 1;
            cboLoaiKM.SelectedIndexChanged += cboLoaiKM_SelectedIndexChanged;
            // 
            // lblNguongTien
            // 
            lblNguongTien.AutoSize = true;
            lblNguongTien.Location = new Point(16, 73);
            lblNguongTien.Name = "lblNguongTien";
            lblNguongTien.Size = new Size(128, 19);
            lblNguongTien.Text = "Ngưỡng tổng đơn:";
            // 
            // nudNguongTien
            // 
            nudNguongTien.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            nudNguongTien.Location = new Point(160, 70);
            nudNguongTien.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nudNguongTien.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudNguongTien.Name = "nudNguongTien";
            nudNguongTien.Size = new Size(150, 25);
            nudNguongTien.TabIndex = 2;
            nudNguongTien.TextAlign = HorizontalAlignment.Right;
            nudNguongTien.ThousandsSeparator = true;
            nudNguongTien.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudNguongTien.ValueChanged += GiaTriThayDoi;
            // 
            // lblLoaiSach
            // 
            lblLoaiSach.AutoSize = true;
            lblLoaiSach.Location = new Point(330, 73);
            lblLoaiSach.Name = "lblLoaiSach";
            lblLoaiSach.Size = new Size(80, 19);
            lblLoaiSach.Text = "Loại sách:";
            // 
            // cboLoaiSach
            // 
            cboLoaiSach.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiSach.FormattingEnabled = true;
            cboLoaiSach.Location = new Point(410, 69);
            cboLoaiSach.Name = "cboLoaiSach";
            cboLoaiSach.Size = new Size(180, 25);
            cboLoaiSach.TabIndex = 3;
            cboLoaiSach.Tag = "MaLoai";
            cboLoaiSach.SelectedIndexChanged += GiaTriThayDoi;
            // 
            // lblPhanTramGiam
            // 
            lblPhanTramGiam.AutoSize = true;
            lblPhanTramGiam.Location = new Point(610, 73);
            lblPhanTramGiam.Name = "lblPhanTramGiam";
            lblPhanTramGiam.Size = new Size(56, 19);
            lblPhanTramGiam.Text = "% giảm:";
            // 
            // nudPhanTramGiam
            // 
            nudPhanTramGiam.Location = new Point(680, 70);
            nudPhanTramGiam.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudPhanTramGiam.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudPhanTramGiam.Name = "nudPhanTramGiam";
            nudPhanTramGiam.Size = new Size(70, 25);
            nudPhanTramGiam.TabIndex = 4;
            nudPhanTramGiam.TextAlign = HorizontalAlignment.Right;
            nudPhanTramGiam.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudPhanTramGiam.ValueChanged += GiaTriThayDoi;
            // 
            // lblNguongSoLuong
            // 
            lblNguongSoLuong.AutoSize = true;
            lblNguongSoLuong.Location = new Point(16, 111);
            lblNguongSoLuong.Name = "lblNguongSoLuong";
            lblNguongSoLuong.Size = new Size(144, 19);
            lblNguongSoLuong.Text = "Số cuốn tối thiểu:";
            // 
            // nudNguongSoLuong
            // 
            nudNguongSoLuong.Location = new Point(160, 108);
            nudNguongSoLuong.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudNguongSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNguongSoLuong.Name = "nudNguongSoLuong";
            nudNguongSoLuong.Size = new Size(100, 25);
            nudNguongSoLuong.TabIndex = 5;
            nudNguongSoLuong.TextAlign = HorizontalAlignment.Right;
            nudNguongSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudNguongSoLuong.ValueChanged += GiaTriThayDoi;
            // 
            // lblSoTienGiam
            // 
            lblSoTienGiam.AutoSize = true;
            lblSoTienGiam.Location = new Point(330, 111);
            lblSoTienGiam.Name = "lblSoTienGiam";
            lblSoTienGiam.Size = new Size(104, 19);
            lblSoTienGiam.Text = "Số tiền giảm:";
            // 
            // nudSoTienGiam
            // 
            nudSoTienGiam.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudSoTienGiam.Location = new Point(440, 108);
            nudSoTienGiam.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudSoTienGiam.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudSoTienGiam.Name = "nudSoTienGiam";
            nudSoTienGiam.Size = new Size(150, 25);
            nudSoTienGiam.TabIndex = 6;
            nudSoTienGiam.TextAlign = HorizontalAlignment.Right;
            nudSoTienGiam.ThousandsSeparator = true;
            nudSoTienGiam.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            nudSoTienGiam.ValueChanged += GiaTriThayDoi;
            // 
            // lblBatDau
            // 
            lblBatDau.AutoSize = true;
            lblBatDau.Location = new Point(16, 149);
            lblBatDau.Name = "lblBatDau";
            lblBatDau.Size = new Size(64, 19);
            lblBatDau.Text = "Từ ngày:";
            // 
            // dtpBatDau
            // 
            dtpBatDau.CustomFormat = "dd/MM/yyyy";
            dtpBatDau.Format = DateTimePickerFormat.Custom;
            dtpBatDau.Location = new Point(160, 146);
            dtpBatDau.Name = "dtpBatDau";
            dtpBatDau.Size = new Size(150, 25);
            dtpBatDau.TabIndex = 7;
            dtpBatDau.ValueChanged += GiaTriThayDoi;
            // 
            // lblKetThuc
            // 
            lblKetThuc.AutoSize = true;
            lblKetThuc.Location = new Point(330, 149);
            lblKetThuc.Name = "lblKetThuc";
            lblKetThuc.Size = new Size(72, 19);
            lblKetThuc.Text = "Đến ngày:";
            // 
            // dtpKetThuc
            // 
            dtpKetThuc.CustomFormat = "dd/MM/yyyy";
            dtpKetThuc.Format = DateTimePickerFormat.Custom;
            dtpKetThuc.Location = new Point(410, 146);
            dtpKetThuc.Name = "dtpKetThuc";
            dtpKetThuc.Size = new Size(150, 25);
            dtpKetThuc.TabIndex = 8;
            dtpKetThuc.Tag = "NgayKetThuc";
            dtpKetThuc.ValueChanged += GiaTriThayDoi;
            // 
            // chkDangBat
            // 
            chkDangBat.AutoSize = true;
            chkDangBat.CheckState = CheckState.Checked;
            chkDangBat.Checked = true;
            chkDangBat.Location = new Point(610, 147);
            chkDangBat.Name = "chkDangBat";
            chkDangBat.Size = new Size(85, 23);
            chkDangBat.TabIndex = 9;
            chkDangBat.Text = "Đang bật";
            chkDangBat.UseVisualStyleBackColor = true;
            chkDangBat.CheckedChanged += GiaTriThayDoi;
            // 
            // lblMoTa
            // 
            lblMoTa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMoTa.ForeColor = Color.FromArgb(14, 72, 116);
            lblMoTa.Location = new Point(16, 182);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(960, 22);
            lblMoTa.TabIndex = 20;
            lblMoTa.Text = "Điều kiện:";
            // 
            // btnThem
            // 
            btnThem.Location = new Point(160, 212);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(100, 34);
            btnThem.TabIndex = 10;
            btnThem.Text = "➕ Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(266, 212);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 34);
            btnSua.TabIndex = 11;
            btnSua.Text = "✏️ Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(372, 212);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 34);
            btnXoa.TabIndex = 12;
            btnXoa.Text = "🗑 Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(478, 212);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(100, 34);
            btnLuu.TabIndex = 13;
            btnLuu.Text = "💾 Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(584, 212);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(100, 34);
            btnHuy.TabIndex = 14;
            btnHuy.Text = "↩ Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(690, 212);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(100, 34);
            btnLamMoi.TabIndex = 15;
            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // dgvKhuyenMai
            // 
            dgvKhuyenMai.AllowUserToAddRows = false;
            dgvKhuyenMai.AllowUserToDeleteRows = false;
            dgvKhuyenMai.AllowUserToResizeRows = false;
            dgvKhuyenMai.AutoGenerateColumns = false;
            dgvKhuyenMai.BackgroundColor = SystemColors.Window;
            dgvKhuyenMai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhuyenMai.Columns.AddRange(new DataGridViewColumn[] { colTenKM, colLoai, colDieuKien, colTuNgay, colDenNgay, colTinhTrang });
            dgvKhuyenMai.Dock = DockStyle.Fill;
            dgvKhuyenMai.Location = new Point(0, 262);
            dgvKhuyenMai.MultiSelect = false;
            dgvKhuyenMai.Name = "dgvKhuyenMai";
            dgvKhuyenMai.ReadOnly = true;
            dgvKhuyenMai.RowHeadersVisible = false;
            dgvKhuyenMai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhuyenMai.Size = new Size(1000, 378);
            dgvKhuyenMai.TabIndex = 1;
            dgvKhuyenMai.CellFormatting += dgvKhuyenMai_CellFormatting;
            dgvKhuyenMai.SelectionChanged += dgvKhuyenMai_SelectionChanged;
            // 
            // colTenKM
            // 
            colTenKM.DataPropertyName = "TenKM";
            colTenKM.HeaderText = "Tên khuyến mãi";
            colTenKM.Name = "colTenKM";
            colTenKM.ReadOnly = true;
            colTenKM.Width = 230;
            // 
            // colLoai
            // 
            colLoai.DataPropertyName = "TenLoaiKM";
            colLoai.HeaderText = "Loại";
            colLoai.Name = "colLoai";
            colLoai.ReadOnly = true;
            colLoai.Width = 130;
            // 
            // colDieuKien
            // 
            colDieuKien.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDieuKien.DataPropertyName = "MoTaDieuKien";
            colDieuKien.HeaderText = "Điều kiện";
            colDieuKien.Name = "colDieuKien";
            colDieuKien.ReadOnly = true;
            // 
            // colTuNgay
            // 
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            colTuNgay.DataPropertyName = "NgayBatDau";
            colTuNgay.DefaultCellStyle = dataGridViewCellStyle1;
            colTuNgay.HeaderText = "Từ ngày";
            colTuNgay.Name = "colTuNgay";
            colTuNgay.ReadOnly = true;
            colTuNgay.Width = 105;
            // 
            // colDenNgay
            // 
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            colDenNgay.DataPropertyName = "NgayKetThuc";
            colDenNgay.DefaultCellStyle = dataGridViewCellStyle2;
            colDenNgay.HeaderText = "Đến ngày";
            colDenNgay.Name = "colDenNgay";
            colDenNgay.ReadOnly = true;
            colDenNgay.Width = 105;
            // 
            // colTinhTrang
            // 
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            colTinhTrang.DefaultCellStyle = dataGridViewCellStyle3;
            colTinhTrang.HeaderText = "Tình trạng";
            colTinhTrang.Name = "colTinhTrang";
            colTinhTrang.ReadOnly = true;
            colTinhTrang.Width = 130;
            // 
            // errKhuyenMai
            // 
            errKhuyenMai.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errKhuyenMai.ContainerControl = this;
            // 
            // FrmKhuyenMai
            // 
            AcceptButton = btnLuu;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnHuy;
            ClientSize = new Size(1000, 640);
            Controls.Add(dgvKhuyenMai);
            Controls.Add(grpThongTin);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1000, 560);
            Name = "FrmKhuyenMai";
            Text = "Khuyến mãi";
            Load += FrmKhuyenMai_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNguongTien).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPhanTramGiam).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNguongSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoTienGiam).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhuyenMai).EndInit();
            ((System.ComponentModel.ISupportInitialize)errKhuyenMai).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblTenKM;
        private TextBox txtTenKM;
        private Label lblLoaiKM;
        private ComboBox cboLoaiKM;
        private Label lblNguongTien;
        private NumericUpDown nudNguongTien;
        private Label lblLoaiSach;
        private ComboBox cboLoaiSach;
        private Label lblPhanTramGiam;
        private NumericUpDown nudPhanTramGiam;
        private Label lblNguongSoLuong;
        private NumericUpDown nudNguongSoLuong;
        private Label lblSoTienGiam;
        private NumericUpDown nudSoTienGiam;
        private Label lblBatDau;
        private DateTimePicker dtpBatDau;
        private Label lblKetThuc;
        private DateTimePicker dtpKetThuc;
        private CheckBox chkDangBat;
        private Label lblMoTa;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLuu;
        private Button btnHuy;
        private Button btnLamMoi;
        private DataGridView dgvKhuyenMai;
        private DataGridViewTextBoxColumn colTenKM;
        private DataGridViewTextBoxColumn colLoai;
        private DataGridViewTextBoxColumn colDieuKien;
        private DataGridViewTextBoxColumn colTuNgay;
        private DataGridViewTextBoxColumn colDenNgay;
        private DataGridViewTextBoxColumn colTinhTrang;
        private ErrorProvider errKhuyenMai;
    }
}
