// File: QuanLyBanSach.GUI/Forms/FrmBanHang.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmBanHang
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            components = new System.ComponentModel.Container();
            pnlThongTin = new Panel();
            lblMaHD = new Label();
            lblGhiChuMaHD = new Label();
            lblNgayLap = new Label();
            lblThuNgan = new Label();
            lblSDTKhach = new Label();
            txtSDTKhach = new TextBox();
            btnTimKhach = new Button();
            lblTenKhach = new Label();
            btnKhachLe = new Button();
            tlpChinh = new TableLayoutPanel();
            grpChonSach = new GroupBox();
            dgvSach = new DataGridView();
            colSachMa = new DataGridViewTextBoxColumn();
            colSachTen = new DataGridViewTextBoxColumn();
            colSachGia = new DataGridViewTextBoxColumn();
            colSachTon = new DataGridViewTextBoxColumn();
            pnlThemSach = new Panel();
            lblSoLuongThem = new Label();
            nudSoLuongThem = new NumericUpDown();
            btnThemVaoDon = new Button();
            pnlNhapMa = new Panel();
            lblMaSach = new Label();
            txtMaSach = new TextBox();
            lblTimSach = new Label();
            txtTimSach = new TextBox();
            grpChiTiet = new GroupBox();
            dgvChiTiet = new DataGridView();
            colCtStt = new DataGridViewTextBoxColumn();
            colCtMa = new DataGridViewTextBoxColumn();
            colCtTen = new DataGridViewTextBoxColumn();
            colCtSoLuong = new DataGridViewTextBoxColumn();
            colCtDonGia = new DataGridViewTextBoxColumn();
            colCtThanhTien = new DataGridViewTextBoxColumn();
            pnlDong = new Panel();
            lblSoLuongDong = new Label();
            nudSoLuongDong = new NumericUpDown();
            btnXoaDong = new Button();
            lblSoCuon = new Label();
            pnlKhachQuen = new Panel();
            grpGoiY = new GroupBox();
            dgvGoiY = new DataGridView();
            colGyTen = new DataGridViewTextBoxColumn();
            colGyGia = new DataGridViewTextBoxColumn();
            lblGoiYHuongDan = new Label();
            grpHoSoKhach = new GroupBox();
            lblChuaChonKhach = new Label();
            lblSoLanMua = new Label();
            lblTongChiTieu = new Label();
            lblLanGanNhat = new Label();
            lblLoaiHayMua = new Label();
            pnlThanhToan = new Panel();
            lblTamTinhNhan = new Label();
            lblTamTinh = new Label();
            lblGiamGiaNhan = new Label();
            lblGiamGia = new Label();
            lblTenKM = new Label();
            lblNhacKM = new Label();
            lblTongNhan = new Label();
            lblTongThanhToan = new Label();
            lblTienKhachDuaNhan = new Label();
            nudTienKhachDua = new NumericUpDown();
            flpMenhGia = new FlowLayoutPanel();
            lblTienThoiNhan = new Label();
            lblTienThoi = new Label();
            lblGhiChu = new Label();
            txtGhiChu = new TextBox();
            btnLuu = new Button();
            btnLuuVaIn = new Button();
            btnTreoDon = new Button();
            btnDonTreo = new Button();
            btnLamMoi = new Button();
            ssBanHang = new StatusStrip();
            tslPhimTat = new ToolStripStatusLabel();
            bsChiTiet = new BindingSource(components);
            pnlThongTin.SuspendLayout();
            tlpChinh.SuspendLayout();
            grpChonSach.SuspendLayout();
            pnlThemSach.SuspendLayout();
            pnlNhapMa.SuspendLayout();
            grpChiTiet.SuspendLayout();
            pnlDong.SuspendLayout();
            pnlKhachQuen.SuspendLayout();
            grpGoiY.SuspendLayout();
            grpHoSoKhach.SuspendLayout();
            pnlThanhToan.SuspendLayout();
            flpMenhGia.SuspendLayout();
            ssBanHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongThem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongDong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGoiY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTienKhachDua).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsChiTiet).BeginInit();
            SuspendLayout();
            // 
            // pnlThongTin
            // 
            pnlThongTin.BackColor = Color.FromArgb(223, 230, 239);
            pnlThongTin.Controls.Add(lblMaHD);
            pnlThongTin.Controls.Add(lblGhiChuMaHD);
            pnlThongTin.Controls.Add(lblNgayLap);
            pnlThongTin.Controls.Add(lblThuNgan);
            pnlThongTin.Controls.Add(lblSDTKhach);
            pnlThongTin.Controls.Add(txtSDTKhach);
            pnlThongTin.Controls.Add(btnTimKhach);
            pnlThongTin.Controls.Add(lblTenKhach);
            pnlThongTin.Controls.Add(btnKhachLe);
            pnlThongTin.Dock = DockStyle.Top;
            pnlThongTin.Location = new Point(0, 0);
            pnlThongTin.Name = "pnlThongTin";
            pnlThongTin.Size = new Size(1280, 56);
            pnlThongTin.TabIndex = 3;
            // 
            // lblMaHD
            // 
            lblMaHD.AutoSize = true;
            lblMaHD.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMaHD.Location = new Point(10, 6);
            lblMaHD.Name = "lblMaHD";
            lblMaHD.Size = new Size(80, 19);
            lblMaHD.Text = "Mã HĐ: ...";
            // 
            // lblGhiChuMaHD
            // 
            lblGhiChuMaHD.AutoSize = true;
            lblGhiChuMaHD.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblGhiChuMaHD.ForeColor = Color.DimGray;
            lblGhiChuMaHD.Location = new Point(10, 31);
            lblGhiChuMaHD.Name = "lblGhiChuMaHD";
            lblGhiChuMaHD.Size = new Size(320, 19);
            lblGhiChuMaHD.Text = "(mã dự kiến – mã chính thức cấp khi lưu)";
            // 
            // lblNgayLap
            // 
            lblNgayLap.AutoSize = true;
            lblNgayLap.Location = new Point(330, 7);
            lblNgayLap.Name = "lblNgayLap";
            lblNgayLap.Size = new Size(72, 19);
            lblNgayLap.Text = "Ngày: ...";
            // 
            // lblThuNgan
            // 
            lblThuNgan.AutoSize = true;
            lblThuNgan.Location = new Point(330, 30);
            lblThuNgan.Name = "lblThuNgan";
            lblThuNgan.Size = new Size(104, 19);
            lblThuNgan.Text = "Thu ngân: ...";
            // 
            // lblSDTKhach
            // 
            lblSDTKhach.AutoSize = true;
            lblSDTKhach.Location = new Point(590, 18);
            lblSDTKhach.Name = "lblSDTKhach";
            lblSDTKhach.Size = new Size(120, 19);
            lblSDTKhach.Text = "SĐT khách (F2):";
            // 
            // txtSDTKhach
            // 
            txtSDTKhach.Location = new Point(710, 15);
            txtSDTKhach.MaxLength = 10;
            txtSDTKhach.Name = "txtSDTKhach";
            txtSDTKhach.Size = new Size(130, 25);
            txtSDTKhach.TabIndex = 1;
            txtSDTKhach.KeyDown += txtSDTKhach_KeyDown;
            txtSDTKhach.KeyPress += txtSDTKhach_KeyPress;
            // 
            // btnTimKhach
            // 
            btnTimKhach.Location = new Point(846, 11);
            btnTimKhach.Name = "btnTimKhach";
            btnTimKhach.Size = new Size(80, 34);
            btnTimKhach.TabIndex = 2;
            btnTimKhach.Text = "🔍 Tìm";
            btnTimKhach.UseVisualStyleBackColor = true;
            btnTimKhach.Click += btnTimKhach_Click;
            // 
            // lblTenKhach
            // 
            lblTenKhach.AutoSize = true;
            lblTenKhach.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTenKhach.ForeColor = Color.FromArgb(14, 72, 116);
            lblTenKhach.Location = new Point(936, 18);
            lblTenKhach.Name = "lblTenKhach";
            lblTenKhach.Size = new Size(64, 19);
            lblTenKhach.Text = "Khách lẻ";
            // 
            // btnKhachLe
            // 
            btnKhachLe.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnKhachLe.Location = new Point(1166, 11);
            btnKhachLe.Name = "btnKhachLe";
            btnKhachLe.Size = new Size(100, 34);
            btnKhachLe.TabIndex = 3;
            btnKhachLe.Text = "Khách lẻ";
            btnKhachLe.UseVisualStyleBackColor = true;
            btnKhachLe.Click += btnKhachLe_Click;
            // 
            // tlpChinh
            // 
            tlpChinh.ColumnCount = 3;
            tlpChinh.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpChinh.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            tlpChinh.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpChinh.Controls.Add(grpChonSach, 0, 0);
            tlpChinh.Controls.Add(grpChiTiet, 1, 0);
            tlpChinh.Controls.Add(pnlKhachQuen, 2, 0);
            tlpChinh.Dock = DockStyle.Fill;
            tlpChinh.Location = new Point(0, 56);
            tlpChinh.Name = "tlpChinh";
            tlpChinh.RowCount = 1;
            tlpChinh.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpChinh.Size = new Size(1280, 438);
            tlpChinh.TabIndex = 0;
            // 
            // grpChonSach
            // 
            grpChonSach.Controls.Add(dgvSach);
            grpChonSach.Controls.Add(pnlThemSach);
            grpChonSach.Controls.Add(pnlNhapMa);
            grpChonSach.Dock = DockStyle.Fill;
            grpChonSach.Location = new Point(3, 3);
            grpChonSach.Name = "grpChonSach";
            grpChonSach.Padding = new Padding(6);
            grpChonSach.Size = new Size(403, 432);
            grpChonSach.TabIndex = 0;
            grpChonSach.TabStop = false;
            grpChonSach.Text = "Chọn sách";
            // 
            // dgvSach
            // 
            dgvSach.AllowUserToAddRows = false;
            dgvSach.AllowUserToDeleteRows = false;
            dgvSach.AllowUserToResizeRows = false;
            dgvSach.AutoGenerateColumns = false;
            dgvSach.BackgroundColor = SystemColors.Window;
            dgvSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSach.Columns.AddRange(new DataGridViewColumn[] { colSachMa, colSachTen, colSachGia, colSachTon });
            dgvSach.Dock = DockStyle.Fill;
            dgvSach.Location = new Point(6, 128);
            dgvSach.MultiSelect = false;
            dgvSach.Name = "dgvSach";
            dgvSach.ReadOnly = true;
            dgvSach.RowHeadersVisible = false;
            dgvSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSach.Size = new Size(391, 252);
            dgvSach.TabIndex = 1;
            dgvSach.CellDoubleClick += dgvSach_CellDoubleClick;
            dgvSach.CellFormatting += dgvSach_CellFormatting;
            // 
            // colSachMa
            // 
            colSachMa.DataPropertyName = "MaSach";
            colSachMa.HeaderText = "Mã";
            colSachMa.Name = "colSachMa";
            colSachMa.ReadOnly = true;
            colSachMa.Width = 62;
            // 
            // colSachTen
            // 
            colSachTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSachTen.DataPropertyName = "TenSach";
            colSachTen.HeaderText = "Tên sách";
            colSachTen.Name = "colSachTen";
            colSachTen.ReadOnly = true;
            // 
            // colSachGia
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            colSachGia.DataPropertyName = "GiaBan";
            colSachGia.DefaultCellStyle = dataGridViewCellStyle1;
            colSachGia.HeaderText = "Giá";
            colSachGia.Name = "colSachGia";
            colSachGia.ReadOnly = true;
            colSachGia.Width = 80;
            // 
            // colSachTon
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSachTon.DataPropertyName = "SoLuongTon";
            colSachTon.DefaultCellStyle = dataGridViewCellStyle2;
            colSachTon.HeaderText = "Tồn";
            colSachTon.Name = "colSachTon";
            colSachTon.ReadOnly = true;
            colSachTon.Width = 50;
            // 
            // pnlThemSach
            // 
            pnlThemSach.Controls.Add(lblSoLuongThem);
            pnlThemSach.Controls.Add(nudSoLuongThem);
            pnlThemSach.Controls.Add(btnThemVaoDon);
            pnlThemSach.Dock = DockStyle.Bottom;
            pnlThemSach.Location = new Point(6, 380);
            pnlThemSach.Name = "pnlThemSach";
            pnlThemSach.Size = new Size(391, 46);
            pnlThemSach.TabIndex = 2;
            // 
            // lblSoLuongThem
            // 
            lblSoLuongThem.AutoSize = true;
            lblSoLuongThem.Location = new Point(2, 13);
            lblSoLuongThem.Name = "lblSoLuongThem";
            lblSoLuongThem.Size = new Size(72, 19);
            lblSoLuongThem.Text = "Số lượng:";
            // 
            // nudSoLuongThem
            // 
            nudSoLuongThem.Location = new Point(76, 10);
            nudSoLuongThem.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nudSoLuongThem.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuongThem.Name = "nudSoLuongThem";
            nudSoLuongThem.Size = new Size(70, 25);
            nudSoLuongThem.TabIndex = 0;
            nudSoLuongThem.TextAlign = HorizontalAlignment.Right;
            nudSoLuongThem.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnThemVaoDon
            // 
            btnThemVaoDon.Location = new Point(156, 6);
            btnThemVaoDon.Name = "btnThemVaoDon";
            btnThemVaoDon.Size = new Size(150, 34);
            btnThemVaoDon.TabIndex = 1;
            btnThemVaoDon.Text = "➕ Thêm vào đơn";
            btnThemVaoDon.UseVisualStyleBackColor = true;
            btnThemVaoDon.Click += btnThemVaoDon_Click;
            // 
            // pnlNhapMa
            // 
            pnlNhapMa.Controls.Add(lblMaSach);
            pnlNhapMa.Controls.Add(txtMaSach);
            pnlNhapMa.Controls.Add(lblTimSach);
            pnlNhapMa.Controls.Add(txtTimSach);
            pnlNhapMa.Dock = DockStyle.Top;
            pnlNhapMa.Location = new Point(6, 24);
            pnlNhapMa.Name = "pnlNhapMa";
            pnlNhapMa.Size = new Size(391, 104);
            pnlNhapMa.TabIndex = 0;
            // 
            // lblMaSach
            // 
            lblMaSach.AutoSize = true;
            lblMaSach.Location = new Point(2, 2);
            lblMaSach.Name = "lblMaSach";
            lblMaSach.Size = new Size(320, 19);
            lblMaSach.Text = "Mã sách / ISBN (gõ hoặc quét rồi Enter):";
            // 
            // txtMaSach
            // 
            txtMaSach.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMaSach.BackColor = Color.LightYellow;
            txtMaSach.CharacterCasing = CharacterCasing.Upper;
            txtMaSach.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            txtMaSach.Location = new Point(2, 24);
            txtMaSach.MaxLength = 20;
            txtMaSach.Name = "txtMaSach";
            txtMaSach.Size = new Size(386, 36);
            txtMaSach.TabIndex = 0;
            txtMaSach.KeyDown += txtMaSach_KeyDown;
            // 
            // lblTimSach
            // 
            lblTimSach.AutoSize = true;
            lblTimSach.Location = new Point(2, 74);
            lblTimSach.Name = "lblTimSach";
            lblTimSach.Size = new Size(112, 19);
            lblTimSach.Text = "Tìm sách (F1):";
            // 
            // txtTimSach
            // 
            txtTimSach.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTimSach.Location = new Point(110, 71);
            txtTimSach.Name = "txtTimSach";
            txtTimSach.Size = new Size(278, 25);
            txtTimSach.TabIndex = 1;
            txtTimSach.KeyDown += txtTimSach_KeyDown;
            txtTimSach.TextChanged += txtTimSach_TextChanged;
            // 
            // grpChiTiet
            // 
            grpChiTiet.Controls.Add(dgvChiTiet);
            grpChiTiet.Controls.Add(pnlDong);
            grpChiTiet.Dock = DockStyle.Fill;
            grpChiTiet.Location = new Point(412, 3);
            grpChiTiet.Name = "grpChiTiet";
            grpChiTiet.Padding = new Padding(6);
            grpChiTiet.Size = new Size(544, 432);
            grpChiTiet.TabIndex = 1;
            grpChiTiet.TabStop = false;
            grpChiTiet.Text = "Hóa đơn đang lập";
            // 
            // dgvChiTiet
            // 
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.AllowUserToResizeRows = false;
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.BackgroundColor = SystemColors.Window;
            dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChiTiet.Columns.AddRange(new DataGridViewColumn[] { colCtStt, colCtMa, colCtTen, colCtSoLuong, colCtDonGia, colCtThanhTien });
            dgvChiTiet.DataSource = bsChiTiet;
            dgvChiTiet.Dock = DockStyle.Fill;
            dgvChiTiet.Location = new Point(6, 24);
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.Size = new Size(532, 356);
            dgvChiTiet.TabIndex = 0;
            dgvChiTiet.CellFormatting += dgvChiTiet_CellFormatting;
            dgvChiTiet.SelectionChanged += dgvChiTiet_SelectionChanged;
            // 
            // colCtStt
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCtStt.DataPropertyName = "Stt";
            colCtStt.DefaultCellStyle = dataGridViewCellStyle3;
            colCtStt.HeaderText = "STT";
            colCtStt.Name = "colCtStt";
            colCtStt.ReadOnly = true;
            colCtStt.Width = 45;
            // 
            // colCtMa
            // 
            colCtMa.DataPropertyName = "MaSach";
            colCtMa.HeaderText = "Mã";
            colCtMa.Name = "colCtMa";
            colCtMa.ReadOnly = true;
            colCtMa.Width = 62;
            // 
            // colCtTen
            // 
            colCtTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCtTen.DataPropertyName = "TenSach";
            colCtTen.HeaderText = "Tên sách";
            colCtTen.Name = "colCtTen";
            colCtTen.ReadOnly = true;
            // 
            // colCtSoLuong
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCtSoLuong.DataPropertyName = "SoLuong";
            colCtSoLuong.DefaultCellStyle = dataGridViewCellStyle4;
            colCtSoLuong.HeaderText = "SL";
            colCtSoLuong.Name = "colCtSoLuong";
            colCtSoLuong.ReadOnly = true;
            colCtSoLuong.Width = 50;
            // 
            // colCtDonGia
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            colCtDonGia.DataPropertyName = "DonGia";
            colCtDonGia.DefaultCellStyle = dataGridViewCellStyle5;
            colCtDonGia.HeaderText = "Đơn giá";
            colCtDonGia.Name = "colCtDonGia";
            colCtDonGia.ReadOnly = true;
            colCtDonGia.Width = 85;
            // 
            // colCtThanhTien
            // 
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            colCtThanhTien.DataPropertyName = "ThanhTien";
            colCtThanhTien.DefaultCellStyle = dataGridViewCellStyle6;
            colCtThanhTien.HeaderText = "Thành tiền";
            colCtThanhTien.Name = "colCtThanhTien";
            colCtThanhTien.ReadOnly = true;
            colCtThanhTien.Width = 100;
            // 
            // pnlDong
            // 
            pnlDong.Controls.Add(lblSoLuongDong);
            pnlDong.Controls.Add(nudSoLuongDong);
            pnlDong.Controls.Add(btnXoaDong);
            pnlDong.Controls.Add(lblSoCuon);
            pnlDong.Dock = DockStyle.Bottom;
            pnlDong.Location = new Point(6, 380);
            pnlDong.Name = "pnlDong";
            pnlDong.Size = new Size(532, 46);
            pnlDong.TabIndex = 1;
            // 
            // lblSoLuongDong
            // 
            lblSoLuongDong.AutoSize = true;
            lblSoLuongDong.Location = new Point(2, 13);
            lblSoLuongDong.Name = "lblSoLuongDong";
            lblSoLuongDong.Size = new Size(152, 19);
            lblSoLuongDong.Text = "SL dòng chọn (+/−):";
            // 
            // nudSoLuongDong
            // 
            nudSoLuongDong.Location = new Point(140, 10);
            nudSoLuongDong.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nudSoLuongDong.Name = "nudSoLuongDong";
            nudSoLuongDong.Size = new Size(70, 25);
            nudSoLuongDong.TabIndex = 0;
            nudSoLuongDong.TextAlign = HorizontalAlignment.Right;
            nudSoLuongDong.ValueChanged += nudSoLuongDong_ValueChanged;
            // 
            // btnXoaDong
            // 
            btnXoaDong.Location = new Point(220, 6);
            btnXoaDong.Name = "btnXoaDong";
            btnXoaDong.Size = new Size(150, 34);
            btnXoaDong.TabIndex = 1;
            btnXoaDong.Text = "🗑 Xóa dòng (Del)";
            btnXoaDong.UseVisualStyleBackColor = true;
            btnXoaDong.Click += btnXoaDong_Click;
            // 
            // lblSoCuon
            // 
            lblSoCuon.AutoSize = true;
            lblSoCuon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSoCuon.Location = new Point(380, 13);
            lblSoCuon.Name = "lblSoCuon";
            lblSoCuon.Size = new Size(48, 19);
            lblSoCuon.Text = "0 cuốn";
            // 
            // pnlKhachQuen
            // 
            pnlKhachQuen.Controls.Add(grpGoiY);
            pnlKhachQuen.Controls.Add(grpHoSoKhach);
            pnlKhachQuen.Dock = DockStyle.Fill;
            pnlKhachQuen.Location = new Point(962, 3);
            pnlKhachQuen.Name = "pnlKhachQuen";
            pnlKhachQuen.Size = new Size(315, 432);
            pnlKhachQuen.TabIndex = 2;
            // 
            // grpGoiY
            // 
            grpGoiY.Controls.Add(dgvGoiY);
            grpGoiY.Controls.Add(lblGoiYHuongDan);
            grpGoiY.Dock = DockStyle.Fill;
            grpGoiY.Location = new Point(0, 176);
            grpGoiY.Name = "grpGoiY";
            grpGoiY.Padding = new Padding(6);
            grpGoiY.Size = new Size(315, 256);
            grpGoiY.TabIndex = 1;
            grpGoiY.TabStop = false;
            grpGoiY.Text = "Thường được mua kèm";
            // 
            // dgvGoiY
            // 
            dgvGoiY.AllowUserToAddRows = false;
            dgvGoiY.AllowUserToDeleteRows = false;
            dgvGoiY.AllowUserToResizeRows = false;
            dgvGoiY.AutoGenerateColumns = false;
            dgvGoiY.BackgroundColor = SystemColors.Window;
            dgvGoiY.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGoiY.Columns.AddRange(new DataGridViewColumn[] { colGyTen, colGyGia });
            dgvGoiY.Dock = DockStyle.Fill;
            dgvGoiY.Location = new Point(6, 24);
            dgvGoiY.MultiSelect = false;
            dgvGoiY.Name = "dgvGoiY";
            dgvGoiY.ReadOnly = true;
            dgvGoiY.RowHeadersVisible = false;
            dgvGoiY.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGoiY.Size = new Size(303, 202);
            dgvGoiY.TabIndex = 0;
            // 
            // colGyTen
            // 
            colGyTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colGyTen.DataPropertyName = "TenSach";
            colGyTen.HeaderText = "Tên sách";
            colGyTen.Name = "colGyTen";
            colGyTen.ReadOnly = true;
            // 
            // colGyGia
            // 
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            colGyGia.DataPropertyName = "GiaBan";
            colGyGia.DefaultCellStyle = dataGridViewCellStyle7;
            colGyGia.HeaderText = "Giá";
            colGyGia.Name = "colGyGia";
            colGyGia.ReadOnly = true;
            colGyGia.Width = 80;
            // 
            // lblGoiYHuongDan
            // 
            lblGoiYHuongDan.Dock = DockStyle.Bottom;
            lblGoiYHuongDan.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblGoiYHuongDan.ForeColor = Color.DimGray;
            lblGoiYHuongDan.Location = new Point(6, 226);
            lblGoiYHuongDan.Name = "lblGoiYHuongDan";
            lblGoiYHuongDan.Size = new Size(303, 24);
            lblGoiYHuongDan.Text = "Nhấp đúp để thêm vào đơn";
            lblGoiYHuongDan.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpHoSoKhach
            // 
            grpHoSoKhach.Controls.Add(lblChuaChonKhach);
            grpHoSoKhach.Controls.Add(lblSoLanMua);
            grpHoSoKhach.Controls.Add(lblTongChiTieu);
            grpHoSoKhach.Controls.Add(lblLanGanNhat);
            grpHoSoKhach.Controls.Add(lblLoaiHayMua);
            grpHoSoKhach.Dock = DockStyle.Top;
            grpHoSoKhach.Location = new Point(0, 0);
            grpHoSoKhach.Name = "grpHoSoKhach";
            grpHoSoKhach.Size = new Size(315, 176);
            grpHoSoKhach.TabIndex = 0;
            grpHoSoKhach.TabStop = false;
            grpHoSoKhach.Text = "Hồ sơ khách quen";
            // 
            // lblChuaChonKhach
            // 
            lblChuaChonKhach.AutoSize = true;
            lblChuaChonKhach.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblChuaChonKhach.ForeColor = Color.DimGray;
            lblChuaChonKhach.Location = new Point(12, 30);
            lblChuaChonKhach.Name = "lblChuaChonKhach";
            lblChuaChonKhach.Size = new Size(120, 19);
            lblChuaChonKhach.Text = "Chưa chọn khách";
            // 
            // lblSoLanMua
            // 
            lblSoLanMua.AutoSize = true;
            lblSoLanMua.Location = new Point(12, 30);
            lblSoLanMua.Name = "lblSoLanMua";
            lblSoLanMua.Size = new Size(88, 19);
            lblSoLanMua.Text = "Số lần mua:";
            lblSoLanMua.Visible = false;
            // 
            // lblTongChiTieu
            // 
            lblTongChiTieu.AutoSize = true;
            lblTongChiTieu.Location = new Point(12, 60);
            lblTongChiTieu.Name = "lblTongChiTieu";
            lblTongChiTieu.Size = new Size(112, 19);
            lblTongChiTieu.Text = "Tổng chi tiêu:";
            lblTongChiTieu.Visible = false;
            // 
            // lblLanGanNhat
            // 
            lblLanGanNhat.AutoSize = true;
            lblLanGanNhat.Location = new Point(12, 90);
            lblLanGanNhat.Name = "lblLanGanNhat";
            lblLanGanNhat.Size = new Size(104, 19);
            lblLanGanNhat.Text = "Lần gần nhất:";
            lblLanGanNhat.Visible = false;
            // 
            // lblLoaiHayMua
            // 
            lblLoaiHayMua.Location = new Point(12, 120);
            lblLoaiHayMua.Name = "lblLoaiHayMua";
            lblLoaiHayMua.Size = new Size(295, 44);
            lblLoaiHayMua.Text = "Hay mua:";
            lblLoaiHayMua.Visible = false;
            // 
            // pnlThanhToan
            // 
            pnlThanhToan.BorderStyle = BorderStyle.FixedSingle;
            pnlThanhToan.Controls.Add(lblTamTinhNhan);
            pnlThanhToan.Controls.Add(lblTamTinh);
            pnlThanhToan.Controls.Add(lblGiamGiaNhan);
            pnlThanhToan.Controls.Add(lblGiamGia);
            pnlThanhToan.Controls.Add(lblTenKM);
            pnlThanhToan.Controls.Add(lblNhacKM);
            pnlThanhToan.Controls.Add(lblTongNhan);
            pnlThanhToan.Controls.Add(lblTongThanhToan);
            pnlThanhToan.Controls.Add(lblTienKhachDuaNhan);
            pnlThanhToan.Controls.Add(nudTienKhachDua);
            pnlThanhToan.Controls.Add(flpMenhGia);
            pnlThanhToan.Controls.Add(lblTienThoiNhan);
            pnlThanhToan.Controls.Add(lblTienThoi);
            pnlThanhToan.Controls.Add(lblGhiChu);
            pnlThanhToan.Controls.Add(txtGhiChu);
            pnlThanhToan.Controls.Add(btnLuu);
            pnlThanhToan.Controls.Add(btnLuuVaIn);
            pnlThanhToan.Controls.Add(btnTreoDon);
            pnlThanhToan.Controls.Add(btnDonTreo);
            pnlThanhToan.Controls.Add(btnLamMoi);
            pnlThanhToan.Dock = DockStyle.Bottom;
            pnlThanhToan.Location = new Point(0, 494);
            pnlThanhToan.Name = "pnlThanhToan";
            pnlThanhToan.Size = new Size(1280, 200);
            pnlThanhToan.TabIndex = 1;
            // 
            // lblTamTinhNhan
            // 
            lblTamTinhNhan.AutoSize = true;
            lblTamTinhNhan.Location = new Point(12, 10);
            lblTamTinhNhan.Name = "lblTamTinhNhan";
            lblTamTinhNhan.Size = new Size(72, 19);
            lblTamTinhNhan.Text = "Tạm tính:";
            // 
            // lblTamTinh
            // 
            lblTamTinh.Location = new Point(130, 10);
            lblTamTinh.Name = "lblTamTinh";
            lblTamTinh.Size = new Size(170, 19);
            lblTamTinh.Text = "0đ";
            lblTamTinh.TextAlign = ContentAlignment.TopRight;
            // 
            // lblGiamGiaNhan
            // 
            lblGiamGiaNhan.AutoSize = true;
            lblGiamGiaNhan.Location = new Point(12, 36);
            lblGiamGiaNhan.Name = "lblGiamGiaNhan";
            lblGiamGiaNhan.Size = new Size(72, 19);
            lblGiamGiaNhan.Text = "Giảm giá:";
            // 
            // lblGiamGia
            // 
            lblGiamGia.ForeColor = Color.ForestGreen;
            lblGiamGia.Location = new Point(130, 36);
            lblGiamGia.Name = "lblGiamGia";
            lblGiamGia.Size = new Size(170, 19);
            lblGiamGia.Text = "0đ";
            lblGiamGia.TextAlign = ContentAlignment.TopRight;
            // 
            // lblTenKM
            // 
            lblTenKM.ForeColor = Color.ForestGreen;
            lblTenKM.Location = new Point(12, 62);
            lblTenKM.Name = "lblTenKM";
            lblTenKM.Size = new Size(440, 19);
            lblTenKM.Text = "Không có khuyến mãi";
            // 
            // lblNhacKM
            // 
            lblNhacKM.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblNhacKM.ForeColor = Color.DarkOrange;
            lblNhacKM.Location = new Point(12, 88);
            lblNhacKM.Name = "lblNhacKM";
            lblNhacKM.Size = new Size(440, 22);
            lblNhacKM.Text = "";
            // 
            // lblTongNhan
            // 
            lblTongNhan.AutoSize = true;
            lblTongNhan.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTongNhan.Location = new Point(12, 140);
            lblTongNhan.Name = "lblTongNhan";
            lblTongNhan.Size = new Size(128, 19);
            lblTongNhan.Text = "TỔNG THANH TOÁN:";
            // 
            // lblTongThanhToan
            // 
            lblTongThanhToan.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTongThanhToan.ForeColor = Color.DarkRed;
            lblTongThanhToan.Location = new Point(170, 124);
            lblTongThanhToan.Name = "lblTongThanhToan";
            lblTongThanhToan.Size = new Size(280, 45);
            lblTongThanhToan.Text = "0đ";
            lblTongThanhToan.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTienKhachDuaNhan
            // 
            lblTienKhachDuaNhan.AutoSize = true;
            lblTienKhachDuaNhan.Location = new Point(480, 10);
            lblTienKhachDuaNhan.Name = "lblTienKhachDuaNhan";
            lblTienKhachDuaNhan.Size = new Size(160, 19);
            lblTienKhachDuaNhan.Text = "Tiền khách đưa (F4):";
            // 
            // nudTienKhachDua
            // 
            nudTienKhachDua.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            nudTienKhachDua.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudTienKhachDua.Location = new Point(480, 34);
            nudTienKhachDua.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudTienKhachDua.Name = "nudTienKhachDua";
            nudTienKhachDua.Size = new Size(220, 32);
            nudTienKhachDua.TabIndex = 0;
            nudTienKhachDua.TextAlign = HorizontalAlignment.Right;
            nudTienKhachDua.ThousandsSeparator = true;
            nudTienKhachDua.KeyUp += nudTienKhachDua_KeyUp;
            nudTienKhachDua.ValueChanged += nudTienKhachDua_ValueChanged;
            // 
            // flpMenhGia
            // 
            flpMenhGia.Location = new Point(476, 74);
            flpMenhGia.Name = "flpMenhGia";
            flpMenhGia.Size = new Size(420, 46);
            flpMenhGia.TabIndex = 1;
            flpMenhGia.WrapContents = false;
            // 
            // lblTienThoiNhan
            // 
            lblTienThoiNhan.AutoSize = true;
            lblTienThoiNhan.Location = new Point(480, 142);
            lblTienThoiNhan.Name = "lblTienThoiNhan";
            lblTienThoiNhan.Size = new Size(80, 19);
            lblTienThoiNhan.Text = "Tiền thối:";
            // 
            // lblTienThoi
            // 
            lblTienThoi.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTienThoi.ForeColor = Color.ForestGreen;
            lblTienThoi.Location = new Point(560, 132);
            lblTienThoi.Name = "lblTienThoi";
            lblTienThoi.Size = new Size(340, 36);
            lblTienThoi.Text = "0đ";
            lblTienThoi.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGhiChu
            // 
            lblGhiChu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(912, 13);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(64, 19);
            lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            txtGhiChu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtGhiChu.Location = new Point(976, 10);
            txtGhiChu.MaxLength = 255;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(290, 25);
            txtGhiChu.TabIndex = 2;
            // 
            // btnLuu
            // 
            btnLuu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLuu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLuu.Location = new Point(912, 46);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(172, 42);
            btnLuu.TabIndex = 3;
            btnLuu.Text = "💾 Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnLuuVaIn
            // 
            btnLuuVaIn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLuuVaIn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLuuVaIn.Location = new Point(1094, 46);
            btnLuuVaIn.Name = "btnLuuVaIn";
            btnLuuVaIn.Size = new Size(172, 42);
            btnLuuVaIn.TabIndex = 4;
            btnLuuVaIn.Text = "🖨 Lưu và In (F9)";
            btnLuuVaIn.UseVisualStyleBackColor = true;
            btnLuuVaIn.Click += btnLuuVaIn_Click;
            // 
            // btnTreoDon
            // 
            btnTreoDon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTreoDon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTreoDon.Location = new Point(912, 96);
            btnTreoDon.Name = "btnTreoDon";
            btnTreoDon.Size = new Size(172, 42);
            btnTreoDon.TabIndex = 5;
            btnTreoDon.Text = "⏸ Treo đơn (F8)";
            btnTreoDon.UseVisualStyleBackColor = true;
            btnTreoDon.Click += btnTreoDon_Click;
            // 
            // btnDonTreo
            // 
            btnDonTreo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDonTreo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDonTreo.Location = new Point(1094, 96);
            btnDonTreo.Name = "btnDonTreo";
            btnDonTreo.Size = new Size(172, 42);
            btnDonTreo.TabIndex = 6;
            btnDonTreo.Text = "📋 Đơn treo (0)";
            btnDonTreo.UseVisualStyleBackColor = true;
            btnDonTreo.Click += btnDonTreo_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLamMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLamMoi.Location = new Point(912, 146);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(354, 40);
            btnLamMoi.TabIndex = 7;
            btnLamMoi.Text = "🧹 Làm mới đơn";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // ssBanHang
            // 
            ssBanHang.ImageScalingSize = new Size(20, 20);
            ssBanHang.Items.AddRange(new ToolStripItem[] { tslPhimTat });
            ssBanHang.Location = new Point(0, 694);
            ssBanHang.Name = "ssBanHang";
            ssBanHang.Size = new Size(1280, 26);
            ssBanHang.TabIndex = 2;
            ssBanHang.Text = "Phím tắt";
            // 
            // tslPhimTat
            // 
            tslPhimTat.Name = "tslPhimTat";
            tslPhimTat.Size = new Size(600, 20);
            tslPhimTat.Text = "F1 Tìm sách | F2 Khách | F4 Tiền khách đưa | F8 Treo | F9 Lưu & In | Del Xóa dòng | +/− Số lượng";
            // 
            // bsChiTiet
            // 
            // 
            // FrmBanHang
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1280, 720);
            Controls.Add(tlpChinh);
            Controls.Add(pnlThanhToan);
            Controls.Add(ssBanHang);
            Controls.Add(pnlThongTin);
            Font = new Font("Segoe UI", 10F);
            KeyPreview = true;
            MinimumSize = new Size(1280, 720);
            Name = "FrmBanHang";
            Text = "Lập hóa đơn";
            FormClosing += FrmBanHang_FormClosing;
            Load += FrmBanHang_Load;
            Shown += FrmBanHang_Shown;
            pnlThongTin.ResumeLayout(false);
            pnlThongTin.PerformLayout();
            tlpChinh.ResumeLayout(false);
            tlpChinh.PerformLayout();
            grpChonSach.ResumeLayout(false);
            grpChonSach.PerformLayout();
            pnlThemSach.ResumeLayout(false);
            pnlThemSach.PerformLayout();
            pnlNhapMa.ResumeLayout(false);
            pnlNhapMa.PerformLayout();
            grpChiTiet.ResumeLayout(false);
            grpChiTiet.PerformLayout();
            pnlDong.ResumeLayout(false);
            pnlDong.PerformLayout();
            pnlKhachQuen.ResumeLayout(false);
            pnlKhachQuen.PerformLayout();
            grpGoiY.ResumeLayout(false);
            grpGoiY.PerformLayout();
            grpHoSoKhach.ResumeLayout(false);
            grpHoSoKhach.PerformLayout();
            pnlThanhToan.ResumeLayout(false);
            pnlThanhToan.PerformLayout();
            flpMenhGia.ResumeLayout(false);
            flpMenhGia.PerformLayout();
            ssBanHang.ResumeLayout(false);
            ssBanHang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSach).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongThem).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongDong).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGoiY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTienKhachDua).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsChiTiet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlThongTin;
        private Label lblMaHD;
        private Label lblGhiChuMaHD;
        private Label lblNgayLap;
        private Label lblThuNgan;
        private Label lblSDTKhach;
        private TextBox txtSDTKhach;
        private Button btnTimKhach;
        private Label lblTenKhach;
        private Button btnKhachLe;
        private TableLayoutPanel tlpChinh;
        private GroupBox grpChonSach;
        private DataGridView dgvSach;
        private DataGridViewTextBoxColumn colSachMa;
        private DataGridViewTextBoxColumn colSachTen;
        private DataGridViewTextBoxColumn colSachGia;
        private DataGridViewTextBoxColumn colSachTon;
        private Panel pnlThemSach;
        private Label lblSoLuongThem;
        private NumericUpDown nudSoLuongThem;
        private Button btnThemVaoDon;
        private Panel pnlNhapMa;
        private Label lblMaSach;
        private TextBox txtMaSach;
        private Label lblTimSach;
        private TextBox txtTimSach;
        private GroupBox grpChiTiet;
        private DataGridView dgvChiTiet;
        private DataGridViewTextBoxColumn colCtStt;
        private DataGridViewTextBoxColumn colCtMa;
        private DataGridViewTextBoxColumn colCtTen;
        private DataGridViewTextBoxColumn colCtSoLuong;
        private DataGridViewTextBoxColumn colCtDonGia;
        private DataGridViewTextBoxColumn colCtThanhTien;
        private Panel pnlDong;
        private Label lblSoLuongDong;
        private NumericUpDown nudSoLuongDong;
        private Button btnXoaDong;
        private Label lblSoCuon;
        private Panel pnlKhachQuen;
        private GroupBox grpGoiY;
        private DataGridView dgvGoiY;
        private DataGridViewTextBoxColumn colGyTen;
        private DataGridViewTextBoxColumn colGyGia;
        private Label lblGoiYHuongDan;
        private GroupBox grpHoSoKhach;
        private Label lblChuaChonKhach;
        private Label lblSoLanMua;
        private Label lblTongChiTieu;
        private Label lblLanGanNhat;
        private Label lblLoaiHayMua;
        private Panel pnlThanhToan;
        private Label lblTamTinhNhan;
        private Label lblTamTinh;
        private Label lblGiamGiaNhan;
        private Label lblGiamGia;
        private Label lblTenKM;
        private Label lblNhacKM;
        private Label lblTongNhan;
        private Label lblTongThanhToan;
        private Label lblTienKhachDuaNhan;
        private NumericUpDown nudTienKhachDua;
        private FlowLayoutPanel flpMenhGia;
        private Label lblTienThoiNhan;
        private Label lblTienThoi;
        private Label lblGhiChu;
        private TextBox txtGhiChu;
        private Button btnLuu;
        private Button btnLuuVaIn;
        private Button btnTreoDon;
        private Button btnDonTreo;
        private Button btnLamMoi;
        private StatusStrip ssBanHang;
        private ToolStripStatusLabel tslPhimTat;
        private BindingSource bsChiTiet;
    }
}
