// File: QuanLyBanSach.GUI/Forms/FrmSach.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmSach
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
            lblMaSach = new Label();
            txtMaSach = new TextBox();
            lblISBN = new Label();
            txtISBN = new TextBox();
            lblLoaiSach = new Label();
            cboLoaiSach = new ComboBox();
            lblTenSach = new Label();
            txtTenSach = new TextBox();
            lblTacGia = new Label();
            txtTacGia = new TextBox();
            lblNhaXuatBan = new Label();
            txtNhaXuatBan = new TextBox();
            lblGiaBan = new Label();
            nudGiaBan = new NumericUpDown();
            lblSoLuongTon = new Label();
            nudSoLuongTon = new NumericUpDown();
            lblTonToiThieu = new Label();
            nudTonToiThieu = new NumericUpDown();
            chkDangBan = new CheckBox();
            btnThem = new Button();
            btnSua = new Button();
            btnAnHien = new Button();
            btnLuu = new Button();
            btnHuy = new Button();
            btnLamMoi = new Button();
            picAnhBia = new PictureBox();
            btnChonAnh = new Button();
            btnXoaAnh = new Button();
            pnlLoc = new Panel();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            lblLocLoai = new Label();
            cboLocLoai = new ComboBox();
            chkHienSachAn = new CheckBox();
            lblSapXep = new Label();
            cboSapXep = new ComboBox();
            dgvSach = new DataGridView();
            colMaSach = new DataGridViewTextBoxColumn();
            colISBN = new DataGridViewTextBoxColumn();
            colTenSach = new DataGridViewTextBoxColumn();
            colTacGia = new DataGridViewTextBoxColumn();
            colTenLoai = new DataGridViewTextBoxColumn();
            colGiaBan = new DataGridViewTextBoxColumn();
            colSoLuongTon = new DataGridViewTextBoxColumn();
            colTonToiThieu = new DataGridViewTextBoxColumn();
            colTrangThai = new DataGridViewTextBoxColumn();
            pnlChuThich = new Panel();
            lblChuThich = new Label();
            lblChuThichSapHet = new Label();
            lblChuThichHetHang = new Label();
            lblChuThichDaAn = new Label();
            btnInSapHet = new Button();
            lblTongSo = new Label();
            errSach = new ErrorProvider(components);
            tmrTimKiem = new System.Windows.Forms.Timer(components);
            ofdChonAnh = new OpenFileDialog();
            grpThongTin.SuspendLayout();
            pnlLoc.SuspendLayout();
            pnlChuThich.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiaBan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTonToiThieu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAnhBia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errSach).BeginInit();
            SuspendLayout();
            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(lblMaSach);
            grpThongTin.Controls.Add(txtMaSach);
            grpThongTin.Controls.Add(lblISBN);
            grpThongTin.Controls.Add(txtISBN);
            grpThongTin.Controls.Add(lblLoaiSach);
            grpThongTin.Controls.Add(cboLoaiSach);
            grpThongTin.Controls.Add(lblTenSach);
            grpThongTin.Controls.Add(txtTenSach);
            grpThongTin.Controls.Add(lblTacGia);
            grpThongTin.Controls.Add(txtTacGia);
            grpThongTin.Controls.Add(lblNhaXuatBan);
            grpThongTin.Controls.Add(txtNhaXuatBan);
            grpThongTin.Controls.Add(lblGiaBan);
            grpThongTin.Controls.Add(nudGiaBan);
            grpThongTin.Controls.Add(lblSoLuongTon);
            grpThongTin.Controls.Add(nudSoLuongTon);
            grpThongTin.Controls.Add(lblTonToiThieu);
            grpThongTin.Controls.Add(nudTonToiThieu);
            grpThongTin.Controls.Add(chkDangBan);
            grpThongTin.Controls.Add(btnThem);
            grpThongTin.Controls.Add(btnSua);
            grpThongTin.Controls.Add(btnAnHien);
            grpThongTin.Controls.Add(btnLuu);
            grpThongTin.Controls.Add(btnHuy);
            grpThongTin.Controls.Add(btnLamMoi);
            grpThongTin.Controls.Add(picAnhBia);
            grpThongTin.Controls.Add(btnChonAnh);
            grpThongTin.Controls.Add(btnXoaAnh);
            grpThongTin.Dock = DockStyle.Top;
            grpThongTin.Location = new Point(0, 0);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(1200, 240);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin sách";
            // 
            // lblMaSach
            // 
            lblMaSach.AutoSize = true;
            lblMaSach.Location = new Point(16, 35);
            lblMaSach.Name = "lblMaSach";
            lblMaSach.Size = new Size(96, 19);
            lblMaSach.Text = "Mã sách (*):";
            // 
            // txtMaSach
            // 
            txtMaSach.CharacterCasing = CharacterCasing.Upper;
            txtMaSach.Location = new Point(120, 32);
            txtMaSach.MaxLength = 20;
            txtMaSach.Name = "txtMaSach";
            txtMaSach.Size = new Size(130, 25);
            txtMaSach.TabIndex = 0;
            // 
            // lblISBN
            // 
            lblISBN.AutoSize = true;
            lblISBN.Location = new Point(275, 35);
            lblISBN.Name = "lblISBN";
            lblISBN.Size = new Size(40, 19);
            lblISBN.Text = "ISBN:";
            // 
            // txtISBN
            // 
            txtISBN.Location = new Point(325, 32);
            txtISBN.MaxLength = 13;
            txtISBN.Name = "txtISBN";
            txtISBN.Size = new Size(170, 25);
            txtISBN.TabIndex = 1;
            txtISBN.KeyPress += txtISBN_KeyPress;
            // 
            // lblLoaiSach
            // 
            lblLoaiSach.AutoSize = true;
            lblLoaiSach.Location = new Point(515, 35);
            lblLoaiSach.Name = "lblLoaiSach";
            lblLoaiSach.Size = new Size(112, 19);
            lblLoaiSach.Text = "Loại sách (*):";
            // 
            // cboLoaiSach
            // 
            cboLoaiSach.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiSach.FormattingEnabled = true;
            cboLoaiSach.Location = new Point(620, 31);
            cboLoaiSach.Name = "cboLoaiSach";
            cboLoaiSach.Size = new Size(240, 25);
            cboLoaiSach.TabIndex = 2;
            cboLoaiSach.Tag = "MaLoai";
            // 
            // lblTenSach
            // 
            lblTenSach.AutoSize = true;
            lblTenSach.Location = new Point(16, 73);
            lblTenSach.Name = "lblTenSach";
            lblTenSach.Size = new Size(104, 19);
            lblTenSach.Text = "Tên sách (*):";
            // 
            // txtTenSach
            // 
            txtTenSach.Location = new Point(120, 70);
            txtTenSach.MaxLength = 255;
            txtTenSach.Name = "txtTenSach";
            txtTenSach.Size = new Size(740, 25);
            txtTenSach.TabIndex = 3;
            // 
            // lblTacGia
            // 
            lblTacGia.AutoSize = true;
            lblTacGia.Location = new Point(16, 111);
            lblTacGia.Name = "lblTacGia";
            lblTacGia.Size = new Size(64, 19);
            lblTacGia.Text = "Tác giả:";
            // 
            // txtTacGia
            // 
            txtTacGia.Location = new Point(120, 108);
            txtTacGia.MaxLength = 100;
            txtTacGia.Name = "txtTacGia";
            txtTacGia.Size = new Size(375, 25);
            txtTacGia.TabIndex = 4;
            // 
            // lblNhaXuatBan
            // 
            lblNhaXuatBan.AutoSize = true;
            lblNhaXuatBan.Location = new Point(515, 111);
            lblNhaXuatBan.Name = "lblNhaXuatBan";
            lblNhaXuatBan.Size = new Size(104, 19);
            lblNhaXuatBan.Text = "Nhà xuất bản:";
            // 
            // txtNhaXuatBan
            // 
            txtNhaXuatBan.Location = new Point(620, 108);
            txtNhaXuatBan.MaxLength = 100;
            txtNhaXuatBan.Name = "txtNhaXuatBan";
            txtNhaXuatBan.Size = new Size(240, 25);
            txtNhaXuatBan.TabIndex = 5;
            // 
            // lblGiaBan
            // 
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(16, 149);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Size = new Size(96, 19);
            lblGiaBan.Text = "Giá bán (*):";
            // 
            // nudGiaBan
            // 
            nudGiaBan.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudGiaBan.Location = new Point(120, 146);
            nudGiaBan.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudGiaBan.Name = "nudGiaBan";
            nudGiaBan.Size = new Size(130, 25);
            nudGiaBan.TabIndex = 6;
            nudGiaBan.TextAlign = HorizontalAlignment.Right;
            nudGiaBan.ThousandsSeparator = true;
            // 
            // lblSoLuongTon
            // 
            lblSoLuongTon.AutoSize = true;
            lblSoLuongTon.Location = new Point(275, 149);
            lblSoLuongTon.Name = "lblSoLuongTon";
            lblSoLuongTon.Size = new Size(32, 19);
            lblSoLuongTon.Text = "Tồn:";
            // 
            // nudSoLuongTon
            // 
            nudSoLuongTon.Location = new Point(325, 146);
            nudSoLuongTon.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudSoLuongTon.Name = "nudSoLuongTon";
            nudSoLuongTon.Size = new Size(90, 25);
            nudSoLuongTon.TabIndex = 7;
            nudSoLuongTon.TextAlign = HorizontalAlignment.Right;
            nudSoLuongTon.ThousandsSeparator = true;
            // 
            // lblTonToiThieu
            // 
            lblTonToiThieu.AutoSize = true;
            lblTonToiThieu.Location = new Point(435, 149);
            lblTonToiThieu.Name = "lblTonToiThieu";
            lblTonToiThieu.Size = new Size(112, 19);
            lblTonToiThieu.Text = "Tồn tối thiểu:";
            // 
            // nudTonToiThieu
            // 
            nudTonToiThieu.Location = new Point(540, 146);
            nudTonToiThieu.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudTonToiThieu.Name = "nudTonToiThieu";
            nudTonToiThieu.Size = new Size(70, 25);
            nudTonToiThieu.TabIndex = 8;
            nudTonToiThieu.TextAlign = HorizontalAlignment.Right;
            nudTonToiThieu.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // chkDangBan
            // 
            chkDangBan.AutoSize = true;
            chkDangBan.CheckState = CheckState.Checked;
            chkDangBan.Checked = true;
            chkDangBan.Location = new Point(635, 147);
            chkDangBan.Name = "chkDangBan";
            chkDangBan.Size = new Size(87, 23);
            chkDangBan.TabIndex = 9;
            chkDangBan.Text = "Đang bán";
            chkDangBan.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(120, 188);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(106, 34);
            btnThem.TabIndex = 10;
            btnThem.Text = "➕ Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(232, 188);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(106, 34);
            btnSua.TabIndex = 11;
            btnSua.Text = "✏️ Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnAnHien
            // 
            btnAnHien.Location = new Point(344, 188);
            btnAnHien.Name = "btnAnHien";
            btnAnHien.Size = new Size(106, 34);
            btnAnHien.TabIndex = 12;
            btnAnHien.Text = "🙈 Ẩn sách";
            btnAnHien.UseVisualStyleBackColor = true;
            btnAnHien.Click += btnAnHien_Click;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(456, 188);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(106, 34);
            btnLuu.TabIndex = 13;
            btnLuu.Text = "💾 Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(568, 188);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(106, 34);
            btnHuy.TabIndex = 14;
            btnHuy.Text = "↩ Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(680, 188);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(106, 34);
            btnLamMoi.TabIndex = 15;
            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // picAnhBia
            // 
            picAnhBia.BackColor = Color.WhiteSmoke;
            picAnhBia.BorderStyle = BorderStyle.FixedSingle;
            picAnhBia.Location = new Point(890, 24);
            picAnhBia.Name = "picAnhBia";
            picAnhBia.Size = new Size(150, 200);
            picAnhBia.SizeMode = PictureBoxSizeMode.Zoom;
            picAnhBia.TabIndex = 18;
            picAnhBia.TabStop = false;
            // 
            // btnChonAnh
            // 
            btnChonAnh.Location = new Point(1050, 24);
            btnChonAnh.Name = "btnChonAnh";
            btnChonAnh.Size = new Size(128, 34);
            btnChonAnh.TabIndex = 16;
            btnChonAnh.Text = "🖼 Chọn ảnh...";
            btnChonAnh.UseVisualStyleBackColor = true;
            btnChonAnh.Click += btnChonAnh_Click;
            // 
            // btnXoaAnh
            // 
            btnXoaAnh.Location = new Point(1050, 66);
            btnXoaAnh.Name = "btnXoaAnh";
            btnXoaAnh.Size = new Size(128, 34);
            btnXoaAnh.TabIndex = 17;
            btnXoaAnh.Text = "✖ Xóa ảnh";
            btnXoaAnh.UseVisualStyleBackColor = true;
            btnXoaAnh.Click += btnXoaAnh_Click;
            // 
            // pnlLoc
            // 
            pnlLoc.Controls.Add(lblTimKiem);
            pnlLoc.Controls.Add(txtTimKiem);
            pnlLoc.Controls.Add(lblLocLoai);
            pnlLoc.Controls.Add(cboLocLoai);
            pnlLoc.Controls.Add(chkHienSachAn);
            pnlLoc.Controls.Add(lblSapXep);
            pnlLoc.Controls.Add(cboSapXep);
            pnlLoc.Dock = DockStyle.Top;
            pnlLoc.Location = new Point(0, 240);
            pnlLoc.Name = "pnlLoc";
            pnlLoc.Size = new Size(1200, 48);
            pnlLoc.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(12, 15);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(232, 19);
            lblTimKiem.Text = "Tìm (mã, ISBN, tên, tác giả):";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(226, 12);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(240, 25);
            txtTimKiem.TabIndex = 0;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // lblLocLoai
            // 
            lblLocLoai.AutoSize = true;
            lblLocLoai.Location = new Point(486, 15);
            lblLocLoai.Name = "lblLocLoai";
            lblLocLoai.Size = new Size(40, 19);
            lblLocLoai.Text = "Loại:";
            // 
            // cboLocLoai
            // 
            cboLocLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocLoai.FormattingEnabled = true;
            cboLocLoai.Location = new Point(530, 11);
            cboLocLoai.Name = "cboLocLoai";
            cboLocLoai.Size = new Size(180, 25);
            cboLocLoai.TabIndex = 1;
            cboLocLoai.SelectedIndexChanged += cboLocLoai_SelectedIndexChanged;
            // 
            // chkHienSachAn
            // 
            chkHienSachAn.AutoSize = true;
            chkHienSachAn.Location = new Point(730, 13);
            chkHienSachAn.Name = "chkHienSachAn";
            chkHienSachAn.Size = new Size(150, 23);
            chkHienSachAn.TabIndex = 2;
            chkHienSachAn.Text = "Hiện cả sách đã ẩn";
            chkHienSachAn.UseVisualStyleBackColor = true;
            chkHienSachAn.CheckedChanged += chkHienSachAn_CheckedChanged;
            // 
            // lblSapXep
            // 
            lblSapXep.AutoSize = true;
            lblSapXep.Location = new Point(900, 15);
            lblSapXep.Name = "lblSapXep";
            lblSapXep.Size = new Size(64, 19);
            lblSapXep.Text = "Sắp xếp:";
            // 
            // cboSapXep
            // 
            cboSapXep.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSapXep.FormattingEnabled = true;
            cboSapXep.Items.AddRange(new object[] { "Tên A→Z", "Giá tăng dần", "Giá giảm dần", "Tồn ít nhất" });
            cboSapXep.Location = new Point(968, 11);
            cboSapXep.Name = "cboSapXep";
            cboSapXep.Size = new Size(170, 25);
            cboSapXep.TabIndex = 3;
            cboSapXep.SelectedIndexChanged += cboSapXep_SelectedIndexChanged;
            // 
            // dgvSach
            // 
            dgvSach.AllowUserToAddRows = false;
            dgvSach.AllowUserToDeleteRows = false;
            dgvSach.AllowUserToResizeRows = false;
            dgvSach.AutoGenerateColumns = false;
            dgvSach.BackgroundColor = SystemColors.Window;
            dgvSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSach.Columns.AddRange(new DataGridViewColumn[] { colMaSach, colISBN, colTenSach, colTacGia, colTenLoai, colGiaBan, colSoLuongTon, colTonToiThieu, colTrangThai });
            dgvSach.Dock = DockStyle.Fill;
            dgvSach.Location = new Point(0, 288);
            dgvSach.MultiSelect = false;
            dgvSach.Name = "dgvSach";
            dgvSach.ReadOnly = true;
            dgvSach.RowHeadersVisible = false;
            dgvSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSach.Size = new Size(1200, 396);
            dgvSach.TabIndex = 2;
            dgvSach.CellFormatting += dgvSach_CellFormatting;
            dgvSach.SelectionChanged += dgvSach_SelectionChanged;
            // 
            // colMaSach
            // 
            colMaSach.DataPropertyName = "MaSach";
            colMaSach.HeaderText = "Mã";
            colMaSach.Name = "colMaSach";
            colMaSach.ReadOnly = true;
            colMaSach.Width = 80;
            // 
            // colISBN
            // 
            colISBN.DataPropertyName = "ISBN";
            colISBN.HeaderText = "ISBN";
            colISBN.Name = "colISBN";
            colISBN.ReadOnly = true;
            colISBN.Width = 125;
            // 
            // colTenSach
            // 
            colTenSach.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenSach.DataPropertyName = "TenSach";
            colTenSach.HeaderText = "Tên sách";
            colTenSach.MinimumWidth = 200;
            colTenSach.Name = "colTenSach";
            colTenSach.ReadOnly = true;
            // 
            // colTacGia
            // 
            colTacGia.DataPropertyName = "TacGia";
            colTacGia.HeaderText = "Tác giả";
            colTacGia.Name = "colTacGia";
            colTacGia.ReadOnly = true;
            colTacGia.Width = 170;
            // 
            // colTenLoai
            // 
            colTenLoai.DataPropertyName = "TenLoai";
            colTenLoai.HeaderText = "Loại";
            colTenLoai.Name = "colTenLoai";
            colTenLoai.ReadOnly = true;
            colTenLoai.Width = 120;
            // 
            // colGiaBan
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            colGiaBan.DataPropertyName = "GiaBan";
            colGiaBan.DefaultCellStyle = dataGridViewCellStyle1;
            colGiaBan.HeaderText = "Giá bán";
            colGiaBan.Name = "colGiaBan";
            colGiaBan.ReadOnly = true;
            colGiaBan.Width = 100;
            // 
            // colSoLuongTon
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongTon.DataPropertyName = "SoLuongTon";
            colSoLuongTon.DefaultCellStyle = dataGridViewCellStyle2;
            colSoLuongTon.HeaderText = "Tồn";
            colSoLuongTon.Name = "colSoLuongTon";
            colSoLuongTon.ReadOnly = true;
            colSoLuongTon.Width = 70;
            // 
            // colTonToiThieu
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTonToiThieu.DataPropertyName = "TonToiThieu";
            colTonToiThieu.DefaultCellStyle = dataGridViewCellStyle3;
            colTonToiThieu.HeaderText = "Tồn tối thiểu";
            colTonToiThieu.Name = "colTonToiThieu";
            colTonToiThieu.ReadOnly = true;
            colTonToiThieu.Width = 100;
            // 
            // colTrangThai
            // 
            colTrangThai.DataPropertyName = "TrangThaiText";
            colTrangThai.HeaderText = "Trạng thái";
            colTrangThai.Name = "colTrangThai";
            colTrangThai.ReadOnly = true;
            colTrangThai.Width = 95;
            // 
            // pnlChuThich
            // 
            pnlChuThich.Controls.Add(lblChuThich);
            pnlChuThich.Controls.Add(lblChuThichSapHet);
            pnlChuThich.Controls.Add(lblChuThichHetHang);
            pnlChuThich.Controls.Add(lblChuThichDaAn);
            pnlChuThich.Controls.Add(btnInSapHet);
            pnlChuThich.Controls.Add(lblTongSo);
            pnlChuThich.Dock = DockStyle.Bottom;
            pnlChuThich.Location = new Point(0, 684);
            pnlChuThich.Name = "pnlChuThich";
            pnlChuThich.Size = new Size(1200, 36);
            pnlChuThich.TabIndex = 3;
            // 
            // lblChuThich
            // 
            lblChuThich.AutoSize = true;
            lblChuThich.Location = new Point(12, 9);
            lblChuThich.Name = "lblChuThich";
            lblChuThich.Size = new Size(70, 19);
            lblChuThich.Text = "Chú thích:";
            // 
            // lblChuThichSapHet
            // 
            lblChuThichSapHet.AutoSize = true;
            lblChuThichSapHet.BackColor = Color.MistyRose;
            lblChuThichSapHet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblChuThichSapHet.ForeColor = Color.Red;
            lblChuThichSapHet.Location = new Point(92, 9);
            lblChuThichSapHet.Name = "lblChuThichSapHet";
            lblChuThichSapHet.Padding = new Padding(6, 0, 6, 0);
            lblChuThichSapHet.Size = new Size(110, 19);
            lblChuThichSapHet.Text = "Sắp hết hàng";
            // 
            // lblChuThichHetHang
            // 
            lblChuThichHetHang.AutoSize = true;
            lblChuThichHetHang.BackColor = Color.FromArgb(255, 199, 199);
            lblChuThichHetHang.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblChuThichHetHang.ForeColor = Color.DarkRed;
            lblChuThichHetHang.Location = new Point(212, 9);
            lblChuThichHetHang.Name = "lblChuThichHetHang";
            lblChuThichHetHang.Padding = new Padding(6, 0, 6, 0);
            lblChuThichHetHang.Size = new Size(88, 19);
            lblChuThichHetHang.Text = "Hết hàng";
            // 
            // lblChuThichDaAn
            // 
            lblChuThichDaAn.AutoSize = true;
            lblChuThichDaAn.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblChuThichDaAn.ForeColor = Color.Gray;
            lblChuThichDaAn.Location = new Point(310, 9);
            lblChuThichDaAn.Name = "lblChuThichDaAn";
            lblChuThichDaAn.Padding = new Padding(6, 0, 6, 0);
            lblChuThichDaAn.Size = new Size(70, 19);
            lblChuThichDaAn.Text = "Đã ẩn";
            // 
            // btnInSapHet
            // 
            btnInSapHet.Location = new Point(400, 3);
            btnInSapHet.Name = "btnInSapHet";
            btnInSapHet.Size = new Size(170, 30);
            btnInSapHet.TabIndex = 0;
            btnInSapHet.Text = "🖨 In DS sắp hết (A4)";
            btnInSapHet.UseVisualStyleBackColor = true;
            btnInSapHet.Click += btnInSapHet_Click;
            // 
            // lblTongSo
            // 
            lblTongSo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTongSo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTongSo.Location = new Point(780, 8);
            lblTongSo.Name = "lblTongSo";
            lblTongSo.Size = new Size(408, 21);
            lblTongSo.Text = "Tổng: 0 sách – 0 sách sắp hết";
            lblTongSo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // errSach
            // 
            errSach.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errSach.ContainerControl = this;
            // 
            // tmrTimKiem
            // 
            tmrTimKiem.Interval = 300;
            tmrTimKiem.Tick += tmrTimKiem_Tick;
            // 
            // ofdChonAnh
            // 
            ofdChonAnh.Filter = "Ảnh bìa (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            ofdChonAnh.Title = "Chọn ảnh bìa";
            // 
            // FrmSach
            // 
            AcceptButton = btnLuu;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnHuy;
            ClientSize = new Size(1200, 720);
            Controls.Add(dgvSach);
            Controls.Add(pnlChuThich);
            Controls.Add(pnlLoc);
            Controls.Add(grpThongTin);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1000, 600);
            Name = "FrmSach";
            Text = "Quản lý sách";
            FormClosed += FrmSach_FormClosed;
            Load += FrmSach_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            pnlLoc.ResumeLayout(false);
            pnlLoc.PerformLayout();
            pnlChuThich.ResumeLayout(false);
            pnlChuThich.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiaBan).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTonToiThieu).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAnhBia).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSach).EndInit();
            ((System.ComponentModel.ISupportInitialize)errSach).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblMaSach;
        private TextBox txtMaSach;
        private Label lblISBN;
        private TextBox txtISBN;
        private Label lblLoaiSach;
        private ComboBox cboLoaiSach;
        private Label lblTenSach;
        private TextBox txtTenSach;
        private Label lblTacGia;
        private TextBox txtTacGia;
        private Label lblNhaXuatBan;
        private TextBox txtNhaXuatBan;
        private Label lblGiaBan;
        private NumericUpDown nudGiaBan;
        private Label lblSoLuongTon;
        private NumericUpDown nudSoLuongTon;
        private Label lblTonToiThieu;
        private NumericUpDown nudTonToiThieu;
        private CheckBox chkDangBan;
        private Button btnThem;
        private Button btnSua;
        private Button btnAnHien;
        private Button btnLuu;
        private Button btnHuy;
        private Button btnLamMoi;
        private PictureBox picAnhBia;
        private Button btnChonAnh;
        private Button btnXoaAnh;
        private Panel pnlLoc;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Label lblLocLoai;
        private ComboBox cboLocLoai;
        private CheckBox chkHienSachAn;
        private Label lblSapXep;
        private ComboBox cboSapXep;
        private DataGridView dgvSach;
        private DataGridViewTextBoxColumn colMaSach;
        private DataGridViewTextBoxColumn colISBN;
        private DataGridViewTextBoxColumn colTenSach;
        private DataGridViewTextBoxColumn colTacGia;
        private DataGridViewTextBoxColumn colTenLoai;
        private DataGridViewTextBoxColumn colGiaBan;
        private DataGridViewTextBoxColumn colSoLuongTon;
        private DataGridViewTextBoxColumn colTonToiThieu;
        private DataGridViewTextBoxColumn colTrangThai;
        private Panel pnlChuThich;
        private Label lblChuThich;
        private Label lblChuThichSapHet;
        private Label lblChuThichHetHang;
        private Label lblChuThichDaAn;
        private Button btnInSapHet;
        private Label lblTongSo;
        private ErrorProvider errSach;
        private System.Windows.Forms.Timer tmrTimKiem;
        private OpenFileDialog ofdChonAnh;
    }
}
