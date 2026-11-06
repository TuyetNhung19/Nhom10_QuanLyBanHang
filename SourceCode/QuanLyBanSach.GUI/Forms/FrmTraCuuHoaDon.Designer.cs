// File: QuanLyBanSach.GUI/Forms/FrmTraCuuHoaDon.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmTraCuuHoaDon
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
            components = new System.ComponentModel.Container();
            pnlLoc = new Panel();
            lblMaHD = new Label();
            txtMaHD = new TextBox();
            lblKhach = new Label();
            txtKhach = new TextBox();
            lblThuNgan = new Label();
            cboThuNgan = new ComboBox();
            lblTrangThai = new Label();
            cboTrangThai = new ComboBox();
            lblTuNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            lblDenNgay = new Label();
            dtpDenNgay = new DateTimePicker();
            btnTim = new Button();
            btnLamMoi = new Button();
            lblGhiChuMa = new Label();
            splHoaDon = new SplitContainer();
            dgvHoaDon = new DataGridView();
            colMaHD = new DataGridViewTextBoxColumn();
            colNgayLap = new DataGridViewTextBoxColumn();
            colThuNgan = new DataGridViewTextBoxColumn();
            colKhach = new DataGridViewTextBoxColumn();
            colTong = new DataGridViewTextBoxColumn();
            colKM = new DataGridViewTextBoxColumn();
            colTrangThai = new DataGridViewTextBoxColumn();
            dgvChiTiet = new DataGridView();
            colCtStt = new DataGridViewTextBoxColumn();
            colCtTen = new DataGridViewTextBoxColumn();
            colCtSoLuong = new DataGridViewTextBoxColumn();
            colCtDonGia = new DataGridViewTextBoxColumn();
            colCtThanhTien = new DataGridViewTextBoxColumn();
            grpTongHop = new GroupBox();
            lblNhanTamTinh = new Label();
            lblTamTinh = new Label();
            lblNhanGiamGia = new Label();
            lblGiamGia = new Label();
            lblTenKM = new Label();
            lblNhanTongTT = new Label();
            lblTongTT = new Label();
            lblNhanTienDua = new Label();
            lblTienDua = new Label();
            lblNhanTienThoi = new Label();
            lblTienThoi = new Label();
            lblLyDoHuy = new Label();
            pnlDuoi = new Panel();
            lblTong = new Label();
            btnInLai = new Button();
            btnHuyHoaDon = new Button();
            errTraCuu = new ErrorProvider(components);
            pnlLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splHoaDon).BeginInit();
            splHoaDon.Panel1.SuspendLayout();
            splHoaDon.Panel2.SuspendLayout();
            splHoaDon.SuspendLayout();
            grpTongHop.SuspendLayout();
            pnlDuoi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errTraCuu).BeginInit();
            SuspendLayout();
            // 
            // pnlLoc
            // 
            pnlLoc.Controls.Add(lblMaHD);
            pnlLoc.Controls.Add(txtMaHD);
            pnlLoc.Controls.Add(lblKhach);
            pnlLoc.Controls.Add(txtKhach);
            pnlLoc.Controls.Add(lblThuNgan);
            pnlLoc.Controls.Add(cboThuNgan);
            pnlLoc.Controls.Add(lblTrangThai);
            pnlLoc.Controls.Add(cboTrangThai);
            pnlLoc.Controls.Add(lblTuNgay);
            pnlLoc.Controls.Add(dtpTuNgay);
            pnlLoc.Controls.Add(lblDenNgay);
            pnlLoc.Controls.Add(dtpDenNgay);
            pnlLoc.Controls.Add(btnTim);
            pnlLoc.Controls.Add(btnLamMoi);
            pnlLoc.Controls.Add(lblGhiChuMa);
            pnlLoc.Dock = DockStyle.Top;
            pnlLoc.Location = new Point(0, 0);
            pnlLoc.Name = "pnlLoc";
            pnlLoc.Size = new Size(1200, 92);
            pnlLoc.TabIndex = 0;
            // 
            // lblMaHD
            // 
            lblMaHD.AutoSize = true;
            lblMaHD.Location = new Point(12, 15);
            lblMaHD.Name = "lblMaHD";
            lblMaHD.Size = new Size(48, 19);
            lblMaHD.Text = "Mã HĐ:";
            // 
            // txtMaHD
            // 
            txtMaHD.CharacterCasing = CharacterCasing.Upper;
            txtMaHD.Location = new Point(90, 12);
            txtMaHD.MaxLength = 20;
            txtMaHD.Name = "txtMaHD";
            txtMaHD.Size = new Size(160, 25);
            txtMaHD.TabIndex = 0;
            // 
            // lblKhach
            // 
            lblKhach.AutoSize = true;
            lblKhach.Location = new Point(268, 15);
            lblKhach.Name = "lblKhach";
            lblKhach.Size = new Size(128, 19);
            lblKhach.Text = "Khách (tên/SĐT):";
            // 
            // txtKhach
            // 
            txtKhach.Location = new Point(390, 12);
            txtKhach.MaxLength = 100;
            txtKhach.Name = "txtKhach";
            txtKhach.Size = new Size(190, 25);
            txtKhach.TabIndex = 1;
            // 
            // lblThuNgan
            // 
            lblThuNgan.AutoSize = true;
            lblThuNgan.Location = new Point(598, 15);
            lblThuNgan.Name = "lblThuNgan";
            lblThuNgan.Size = new Size(72, 19);
            lblThuNgan.Text = "Thu ngân:";
            // 
            // cboThuNgan
            // 
            cboThuNgan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThuNgan.FormattingEnabled = true;
            cboThuNgan.Location = new Point(672, 11);
            cboThuNgan.Name = "cboThuNgan";
            cboThuNgan.Size = new Size(200, 25);
            cboThuNgan.TabIndex = 2;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(890, 15);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(88, 19);
            lblTrangThai.Text = "Trạng thái:";
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Items.AddRange(new object[] { "Tất cả", "Hoàn tất", "Đã hủy" });
            cboTrangThai.Location = new Point(972, 11);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(140, 25);
            cboTrangThai.TabIndex = 3;
            // 
            // lblTuNgay
            // 
            lblTuNgay.AutoSize = true;
            lblTuNgay.Location = new Point(12, 55);
            lblTuNgay.Name = "lblTuNgay";
            lblTuNgay.Size = new Size(64, 19);
            lblTuNgay.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(90, 52);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(140, 25);
            dtpTuNgay.TabIndex = 4;
            // 
            // lblDenNgay
            // 
            lblDenNgay.AutoSize = true;
            lblDenNgay.Location = new Point(268, 55);
            lblDenNgay.Name = "lblDenNgay";
            lblDenNgay.Size = new Size(72, 19);
            lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(390, 52);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(140, 25);
            dtpDenNgay.TabIndex = 5;
            // 
            // btnTim
            // 
            btnTim.Location = new Point(598, 48);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(100, 34);
            btnTim.TabIndex = 6;
            btnTim.Text = "🔍 Tìm";
            btnTim.UseVisualStyleBackColor = true;
            btnTim.Click += btnTim_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(706, 48);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 34);
            btnLamMoi.TabIndex = 7;
            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblGhiChuMa
            // 
            lblGhiChuMa.AutoSize = true;
            lblGhiChuMa.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblGhiChuMa.ForeColor = Color.DimGray;
            lblGhiChuMa.Location = new Point(830, 56);
            lblGhiChuMa.Name = "lblGhiChuMa";
            lblGhiChuMa.Size = new Size(240, 15);
            lblGhiChuMa.Text = "Có nhập mã hóa đơn thì bỏ qua khoảng ngày";
            // 
            // splHoaDon
            // 
            splHoaDon.Dock = DockStyle.Fill;
            splHoaDon.Location = new Point(0, 92);
            splHoaDon.Name = "splHoaDon";
            splHoaDon.Orientation = Orientation.Horizontal;
            splHoaDon.Panel1.Controls.Add(dgvHoaDon);
            splHoaDon.Panel2.Controls.Add(dgvChiTiet);
            splHoaDon.Panel2.Controls.Add(grpTongHop);
            splHoaDon.Size = new Size(1200, 558);
            splHoaDon.SplitterDistance = 300;
            splHoaDon.TabIndex = 1;
            // 
            // dgvHoaDon
            // 
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.AllowUserToDeleteRows = false;
            dgvHoaDon.AllowUserToResizeRows = false;
            dgvHoaDon.AutoGenerateColumns = false;
            dgvHoaDon.BackgroundColor = SystemColors.Window;
            dgvHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoaDon.Columns.AddRange(new DataGridViewColumn[] { colMaHD, colNgayLap, colThuNgan, colKhach, colTong, colKM, colTrangThai });
            dgvHoaDon.Dock = DockStyle.Fill;
            dgvHoaDon.Location = new Point(0, 0);
            dgvHoaDon.MultiSelect = false;
            dgvHoaDon.Name = "dgvHoaDon";
            dgvHoaDon.ReadOnly = true;
            dgvHoaDon.RowHeadersVisible = false;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.Size = new Size(1200, 300);
            dgvHoaDon.TabIndex = 0;
            dgvHoaDon.CellDoubleClick += dgvHoaDon_CellDoubleClick;
            dgvHoaDon.CellFormatting += dgvHoaDon_CellFormatting;
            dgvHoaDon.SelectionChanged += dgvHoaDon_SelectionChanged;
            // 
            // colMaHD
            // 
            colMaHD.DataPropertyName = "MaHD";
            colMaHD.HeaderText = "Mã hóa đơn";
            colMaHD.Name = "colMaHD";
            colMaHD.ReadOnly = true;
            colMaHD.Width = 140;
            // 
            // colNgayLap
            // 
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm";
            colNgayLap.DataPropertyName = "NgayLap";
            colNgayLap.DefaultCellStyle = dataGridViewCellStyle1;
            colNgayLap.HeaderText = "Ngày giờ";
            colNgayLap.Name = "colNgayLap";
            colNgayLap.ReadOnly = true;
            colNgayLap.Width = 140;
            // 
            // colThuNgan
            // 
            colThuNgan.DataPropertyName = "TenThuNgan";
            colThuNgan.HeaderText = "Thu ngân";
            colThuNgan.Name = "colThuNgan";
            colThuNgan.ReadOnly = true;
            colThuNgan.Width = 170;
            // 
            // colKhach
            // 
            colKhach.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colKhach.DataPropertyName = "TenKhach";
            colKhach.HeaderText = "Khách hàng";
            colKhach.Name = "colKhach";
            colKhach.ReadOnly = true;
            // 
            // colTong
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            colTong.DataPropertyName = "TongThanhToan";
            colTong.DefaultCellStyle = dataGridViewCellStyle2;
            colTong.HeaderText = "Tổng thanh toán";
            colTong.Name = "colTong";
            colTong.ReadOnly = true;
            colTong.Width = 130;
            // 
            // colKM
            // 
            colKM.DataPropertyName = "TenKM";
            colKM.HeaderText = "Khuyến mãi";
            colKM.Name = "colKM";
            colKM.ReadOnly = true;
            colKM.Width = 200;
            // 
            // colTrangThai
            // 
            colTrangThai.DataPropertyName = "TrangThaiText";
            colTrangThai.HeaderText = "Trạng thái";
            colTrangThai.Name = "colTrangThai";
            colTrangThai.ReadOnly = true;
            colTrangThai.Width = 100;
            // 
            // dgvChiTiet
            // 
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.AllowUserToResizeRows = false;
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.BackgroundColor = SystemColors.Window;
            dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChiTiet.Columns.AddRange(new DataGridViewColumn[] { colCtStt, colCtTen, colCtSoLuong, colCtDonGia, colCtThanhTien });
            dgvChiTiet.Dock = DockStyle.Fill;
            dgvChiTiet.Location = new Point(0, 0);
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.Size = new Size(860, 254);
            dgvChiTiet.TabIndex = 0;
            dgvChiTiet.TabStop = false;
            // 
            // colCtStt
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCtStt.DataPropertyName = "Stt";
            colCtStt.DefaultCellStyle = dataGridViewCellStyle3;
            colCtStt.HeaderText = "STT";
            colCtStt.Name = "colCtStt";
            colCtStt.ReadOnly = true;
            colCtStt.Width = 50;
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
            colCtSoLuong.Width = 60;
            // 
            // colCtDonGia
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            colCtDonGia.DataPropertyName = "DonGia";
            colCtDonGia.DefaultCellStyle = dataGridViewCellStyle5;
            colCtDonGia.HeaderText = "Đơn giá (lúc bán)";
            colCtDonGia.Name = "colCtDonGia";
            colCtDonGia.ReadOnly = true;
            colCtDonGia.Width = 140;
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
            colCtThanhTien.Width = 130;
            // 
            // grpTongHop
            // 
            grpTongHop.Controls.Add(lblNhanTamTinh);
            grpTongHop.Controls.Add(lblTamTinh);
            grpTongHop.Controls.Add(lblNhanGiamGia);
            grpTongHop.Controls.Add(lblGiamGia);
            grpTongHop.Controls.Add(lblTenKM);
            grpTongHop.Controls.Add(lblNhanTongTT);
            grpTongHop.Controls.Add(lblTongTT);
            grpTongHop.Controls.Add(lblNhanTienDua);
            grpTongHop.Controls.Add(lblTienDua);
            grpTongHop.Controls.Add(lblNhanTienThoi);
            grpTongHop.Controls.Add(lblTienThoi);
            grpTongHop.Controls.Add(lblLyDoHuy);
            grpTongHop.Dock = DockStyle.Right;
            grpTongHop.Location = new Point(860, 0);
            grpTongHop.Name = "grpTongHop";
            grpTongHop.Size = new Size(340, 254);
            grpTongHop.TabIndex = 1;
            grpTongHop.TabStop = false;
            grpTongHop.Text = "Tổng hợp";
            // 
            // lblNhanTamTinh
            // 
            lblNhanTamTinh.AutoSize = true;
            lblNhanTamTinh.Location = new Point(12, 30);
            lblNhanTamTinh.Name = "lblNhanTamTinh";
            lblNhanTamTinh.Size = new Size(72, 19);
            lblNhanTamTinh.Text = "Tạm tính:";
            // 
            // lblTamTinh
            // 
            lblTamTinh.Location = new Point(150, 30);
            lblTamTinh.Name = "lblTamTinh";
            lblTamTinh.Size = new Size(176, 19);
            lblTamTinh.TextAlign = ContentAlignment.TopRight;
            // 
            // lblNhanGiamGia
            // 
            lblNhanGiamGia.AutoSize = true;
            lblNhanGiamGia.Location = new Point(12, 56);
            lblNhanGiamGia.Name = "lblNhanGiamGia";
            lblNhanGiamGia.Size = new Size(72, 19);
            lblNhanGiamGia.Text = "Giảm giá:";
            // 
            // lblGiamGia
            // 
            lblGiamGia.Location = new Point(150, 56);
            lblGiamGia.Name = "lblGiamGia";
            lblGiamGia.Size = new Size(176, 19);
            lblGiamGia.TextAlign = ContentAlignment.TopRight;
            // 
            // lblTenKM
            // 
            lblTenKM.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblTenKM.ForeColor = Color.ForestGreen;
            lblTenKM.Location = new Point(12, 82);
            lblTenKM.Name = "lblTenKM";
            lblTenKM.Size = new Size(314, 18);
            // 
            // lblNhanTongTT
            // 
            lblNhanTongTT.AutoSize = true;
            lblNhanTongTT.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNhanTongTT.Location = new Point(12, 104);
            lblNhanTongTT.Name = "lblNhanTongTT";
            lblNhanTongTT.Size = new Size(128, 19);
            lblNhanTongTT.Text = "Tổng thanh toán:";
            // 
            // lblTongTT
            // 
            lblTongTT.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTongTT.Location = new Point(150, 104);
            lblTongTT.Name = "lblTongTT";
            lblTongTT.Size = new Size(176, 19);
            lblTongTT.TextAlign = ContentAlignment.TopRight;
            // 
            // lblNhanTienDua
            // 
            lblNhanTienDua.AutoSize = true;
            lblNhanTienDua.Location = new Point(12, 130);
            lblNhanTienDua.Name = "lblNhanTienDua";
            lblNhanTienDua.Size = new Size(120, 19);
            lblNhanTienDua.Text = "Tiền khách đưa:";
            // 
            // lblTienDua
            // 
            lblTienDua.Location = new Point(150, 130);
            lblTienDua.Name = "lblTienDua";
            lblTienDua.Size = new Size(176, 19);
            lblTienDua.TextAlign = ContentAlignment.TopRight;
            // 
            // lblNhanTienThoi
            // 
            lblNhanTienThoi.AutoSize = true;
            lblNhanTienThoi.Location = new Point(12, 156);
            lblNhanTienThoi.Name = "lblNhanTienThoi";
            lblNhanTienThoi.Size = new Size(80, 19);
            lblNhanTienThoi.Text = "Tiền thối:";
            // 
            // lblTienThoi
            // 
            lblTienThoi.Location = new Point(150, 156);
            lblTienThoi.Name = "lblTienThoi";
            lblTienThoi.Size = new Size(176, 19);
            lblTienThoi.TextAlign = ContentAlignment.TopRight;
            // 
            // lblLyDoHuy
            // 
            lblLyDoHuy.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLyDoHuy.ForeColor = Color.Red;
            lblLyDoHuy.Location = new Point(12, 186);
            lblLyDoHuy.Name = "lblLyDoHuy";
            lblLyDoHuy.Size = new Size(314, 60);
            lblLyDoHuy.UseCompatibleTextRendering = true;
            // 
            // pnlDuoi
            // 
            pnlDuoi.Controls.Add(lblTong);
            pnlDuoi.Controls.Add(btnInLai);
            pnlDuoi.Controls.Add(btnHuyHoaDon);
            pnlDuoi.Dock = DockStyle.Bottom;
            pnlDuoi.Location = new Point(0, 650);
            pnlDuoi.Name = "pnlDuoi";
            pnlDuoi.Size = new Size(1200, 50);
            pnlDuoi.TabIndex = 2;
            // 
            // lblTong
            // 
            lblTong.AutoSize = true;
            lblTong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTong.Location = new Point(12, 15);
            lblTong.Name = "lblTong";
            lblTong.Size = new Size(300, 19);
            lblTong.Text = "0 hóa đơn";
            // 
            // btnInLai
            // 
            btnInLai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnInLai.Location = new Point(900, 8);
            btnInLai.Name = "btnInLai";
            btnInLai.Size = new Size(140, 34);
            btnInLai.TabIndex = 0;
            btnInLai.Text = "🖨 In lại";
            btnInLai.UseVisualStyleBackColor = true;
            btnInLai.Click += btnInLai_Click;
            // 
            // btnHuyHoaDon
            // 
            btnHuyHoaDon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHuyHoaDon.Location = new Point(1048, 8);
            btnHuyHoaDon.Name = "btnHuyHoaDon";
            btnHuyHoaDon.Size = new Size(140, 34);
            btnHuyHoaDon.TabIndex = 1;
            btnHuyHoaDon.Text = "⛔ Hủy hóa đơn";
            btnHuyHoaDon.UseVisualStyleBackColor = true;
            btnHuyHoaDon.Visible = false;
            btnHuyHoaDon.Click += btnHuyHoaDon_Click;
            // 
            // errTraCuu
            // 
            errTraCuu.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errTraCuu.ContainerControl = this;
            // 
            // FrmTraCuuHoaDon
            // 
            AcceptButton = btnTim;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1200, 700);
            Controls.Add(splHoaDon);
            Controls.Add(pnlDuoi);
            Controls.Add(pnlLoc);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1100, 600);
            Name = "FrmTraCuuHoaDon";
            Text = "Tra cứu hóa đơn";
            FormClosed += FrmTraCuuHoaDon_FormClosed;
            Load += FrmTraCuuHoaDon_Load;
            pnlLoc.ResumeLayout(false);
            pnlLoc.PerformLayout();
            splHoaDon.Panel1.ResumeLayout(false);
            splHoaDon.Panel2.ResumeLayout(false);
            splHoaDon.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splHoaDon).EndInit();
            splHoaDon.ResumeLayout(false);
            splHoaDon.PerformLayout();
            grpTongHop.ResumeLayout(false);
            grpTongHop.PerformLayout();
            pnlDuoi.ResumeLayout(false);
            pnlDuoi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ((System.ComponentModel.ISupportInitialize)errTraCuu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlLoc;
        private Label lblMaHD;
        private TextBox txtMaHD;
        private Label lblKhach;
        private TextBox txtKhach;
        private Label lblThuNgan;
        private ComboBox cboThuNgan;
        private Label lblTrangThai;
        private ComboBox cboTrangThai;
        private Label lblTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label lblDenNgay;
        private DateTimePicker dtpDenNgay;
        private Button btnTim;
        private Button btnLamMoi;
        private Label lblGhiChuMa;
        private SplitContainer splHoaDon;
        private DataGridView dgvHoaDon;
        private DataGridViewTextBoxColumn colMaHD;
        private DataGridViewTextBoxColumn colNgayLap;
        private DataGridViewTextBoxColumn colThuNgan;
        private DataGridViewTextBoxColumn colKhach;
        private DataGridViewTextBoxColumn colTong;
        private DataGridViewTextBoxColumn colKM;
        private DataGridViewTextBoxColumn colTrangThai;
        private DataGridView dgvChiTiet;
        private DataGridViewTextBoxColumn colCtStt;
        private DataGridViewTextBoxColumn colCtTen;
        private DataGridViewTextBoxColumn colCtSoLuong;
        private DataGridViewTextBoxColumn colCtDonGia;
        private DataGridViewTextBoxColumn colCtThanhTien;
        private GroupBox grpTongHop;
        private Label lblNhanTamTinh;
        private Label lblTamTinh;
        private Label lblNhanGiamGia;
        private Label lblGiamGia;
        private Label lblTenKM;
        private Label lblNhanTongTT;
        private Label lblTongTT;
        private Label lblNhanTienDua;
        private Label lblTienDua;
        private Label lblNhanTienThoi;
        private Label lblTienThoi;
        private Label lblLyDoHuy;
        private Panel pnlDuoi;
        private Label lblTong;
        private Button btnInLai;
        private Button btnHuyHoaDon;
        private ErrorProvider errTraCuu;
    }
}
