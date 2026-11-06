// File: QuanLyBanSach.GUI/Forms/FrmThongKe.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmThongKe
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
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            components = new System.ComponentModel.Container();
            pnlLoc = new Panel();
            lblTuNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            lblDenNgay = new Label();
            dtpDenNgay = new DateTimePicker();
            btnHomNay = new Button();
            btn7Ngay = new Button();
            btnThangNay = new Button();
            btnXem = new Button();
            btnXuatCsv = new Button();
            tabThongKe = new TabControl();
            tabDoanhThu = new TabPage();
            splDoanhThu = new SplitContainer();
            dgvDoanhThu = new DataGridView();
            colDtNhan = new DataGridViewTextBoxColumn();
            colDtSoHoaDon = new DataGridViewTextBoxColumn();
            colDtTamTinh = new DataGridViewTextBoxColumn();
            colDtGiamGia = new DataGridViewTextBoxColumn();
            colDtDoanhThu = new DataGridViewTextBoxColumn();
            bdcDoanhThu = new QuanLyBanSach.GUI.UserControls.BieuDoCot();
            lblTongDoanhThu = new Label();
            pnlDoanhThuTren = new Panel();
            rdoTheoNgay = new RadioButton();
            rdoTheoThang = new RadioButton();
            tabSachBanChay = new TabPage();
            splBanChay = new SplitContainer();
            dgvBanChay = new DataGridView();
            colBcMaSach = new DataGridViewTextBoxColumn();
            colBcTenSach = new DataGridViewTextBoxColumn();
            colBcSoLuong = new DataGridViewTextBoxColumn();
            colBcDoanhThu = new DataGridViewTextBoxColumn();
            bdcBanChay = new QuanLyBanSach.GUI.UserControls.BieuDoCot();
            pnlBanChayTren = new Panel();
            lblTop = new Label();
            nudTop = new NumericUpDown();
            rdoTheoSoLuong = new RadioButton();
            rdoTheoDoanhThu = new RadioButton();
            tabSachSapHet = new TabPage();
            dgvSapHet = new DataGridView();
            colShMaSach = new DataGridViewTextBoxColumn();
            colShTenSach = new DataGridViewTextBoxColumn();
            colShLoai = new DataGridViewTextBoxColumn();
            colShTon = new DataGridViewTextBoxColumn();
            colShToiThieu = new DataGridViewTextBoxColumn();
            pnlSapHetTren = new Panel();
            lblLoaiSach = new Label();
            cboLoaiSach = new ComboBox();
            btnInSapHet = new Button();
            tabThuNgan = new TabPage();
            splThuNgan = new SplitContainer();
            dgvThuNgan = new DataGridView();
            colTnHoTen = new DataGridViewTextBoxColumn();
            colTnSoHoaDon = new DataGridViewTextBoxColumn();
            colTnDoanhThu = new DataGridViewTextBoxColumn();
            bdcThuNgan = new QuanLyBanSach.GUI.UserControls.BieuDoCot();
            tabKhuyenMai = new TabPage();
            dgvKhuyenMai = new DataGridView();
            colKmTen = new DataGridViewTextBoxColumn();
            colKmSoHoaDon = new DataGridViewTextBoxColumn();
            colKmTongGiam = new DataGridViewTextBoxColumn();
            colKmDoanhThu = new DataGridViewTextBoxColumn();
            lblGhiChuKM = new Label();
            errThongKe = new ErrorProvider(components);
            pnlLoc.SuspendLayout();
            tabThongKe.SuspendLayout();
            tabDoanhThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splDoanhThu).BeginInit();
            splDoanhThu.Panel1.SuspendLayout();
            splDoanhThu.Panel2.SuspendLayout();
            splDoanhThu.SuspendLayout();
            pnlDoanhThuTren.SuspendLayout();
            tabSachBanChay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splBanChay).BeginInit();
            splBanChay.Panel1.SuspendLayout();
            splBanChay.Panel2.SuspendLayout();
            splBanChay.SuspendLayout();
            pnlBanChayTren.SuspendLayout();
            tabSachSapHet.SuspendLayout();
            pnlSapHetTren.SuspendLayout();
            tabThuNgan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splThuNgan).BeginInit();
            splThuNgan.Panel1.SuspendLayout();
            splThuNgan.Panel2.SuspendLayout();
            splThuNgan.SuspendLayout();
            tabKhuyenMai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDoanhThu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBanChay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSapHet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvThuNgan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhuyenMai).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errThongKe).BeginInit();
            SuspendLayout();
            // 
            // pnlLoc
            // 
            pnlLoc.Controls.Add(lblTuNgay);
            pnlLoc.Controls.Add(dtpTuNgay);
            pnlLoc.Controls.Add(lblDenNgay);
            pnlLoc.Controls.Add(dtpDenNgay);
            pnlLoc.Controls.Add(btnHomNay);
            pnlLoc.Controls.Add(btn7Ngay);
            pnlLoc.Controls.Add(btnThangNay);
            pnlLoc.Controls.Add(btnXem);
            pnlLoc.Controls.Add(btnXuatCsv);
            pnlLoc.Dock = DockStyle.Top;
            pnlLoc.Location = new Point(0, 0);
            pnlLoc.Name = "pnlLoc";
            pnlLoc.Size = new Size(1200, 58);
            pnlLoc.TabIndex = 0;
            // 
            // lblTuNgay
            // 
            lblTuNgay.AutoSize = true;
            lblTuNgay.Location = new Point(12, 18);
            lblTuNgay.Name = "lblTuNgay";
            lblTuNgay.Size = new Size(64, 19);
            lblTuNgay.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(80, 15);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(130, 25);
            dtpTuNgay.TabIndex = 0;
            // 
            // lblDenNgay
            // 
            lblDenNgay.AutoSize = true;
            lblDenNgay.Location = new Point(224, 18);
            lblDenNgay.Name = "lblDenNgay";
            lblDenNgay.Size = new Size(72, 19);
            lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(300, 15);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(130, 25);
            dtpDenNgay.TabIndex = 1;
            // 
            // btnHomNay
            // 
            btnHomNay.Location = new Point(446, 11);
            btnHomNay.Name = "btnHomNay";
            btnHomNay.Size = new Size(90, 34);
            btnHomNay.TabIndex = 2;
            btnHomNay.Text = "Hôm nay";
            btnHomNay.UseVisualStyleBackColor = true;
            btnHomNay.Click += btnHomNay_Click;
            // 
            // btn7Ngay
            // 
            btn7Ngay.Location = new Point(542, 11);
            btn7Ngay.Name = "btn7Ngay";
            btn7Ngay.Size = new Size(80, 34);
            btn7Ngay.TabIndex = 3;
            btn7Ngay.Text = "7 ngày";
            btn7Ngay.UseVisualStyleBackColor = true;
            btn7Ngay.Click += btn7Ngay_Click;
            // 
            // btnThangNay
            // 
            btnThangNay.Location = new Point(628, 11);
            btnThangNay.Name = "btnThangNay";
            btnThangNay.Size = new Size(100, 34);
            btnThangNay.TabIndex = 4;
            btnThangNay.Text = "Tháng này";
            btnThangNay.UseVisualStyleBackColor = true;
            btnThangNay.Click += btnThangNay_Click;
            // 
            // btnXem
            // 
            btnXem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXem.Location = new Point(744, 11);
            btnXem.Name = "btnXem";
            btnXem.Size = new Size(100, 34);
            btnXem.TabIndex = 5;
            btnXem.Text = "📊 Xem";
            btnXem.UseVisualStyleBackColor = true;
            btnXem.Click += btnXem_Click;
            // 
            // btnXuatCsv
            // 
            btnXuatCsv.Location = new Point(850, 11);
            btnXuatCsv.Name = "btnXuatCsv";
            btnXuatCsv.Size = new Size(120, 34);
            btnXuatCsv.TabIndex = 6;
            btnXuatCsv.Text = "💾 Xuất CSV";
            btnXuatCsv.UseVisualStyleBackColor = true;
            btnXuatCsv.Click += btnXuatCsv_Click;
            // 
            // tabThongKe
            // 
            tabThongKe.Controls.Add(tabDoanhThu);
            tabThongKe.Controls.Add(tabSachBanChay);
            tabThongKe.Controls.Add(tabSachSapHet);
            tabThongKe.Controls.Add(tabThuNgan);
            tabThongKe.Controls.Add(tabKhuyenMai);
            tabThongKe.Dock = DockStyle.Fill;
            tabThongKe.Location = new Point(0, 58);
            tabThongKe.Name = "tabThongKe";
            tabThongKe.SelectedIndex = 0;
            tabThongKe.Size = new Size(1200, 642);
            tabThongKe.TabIndex = 1;
            // 
            // tabDoanhThu
            // 
            tabDoanhThu.Controls.Add(splDoanhThu);
            tabDoanhThu.Controls.Add(lblTongDoanhThu);
            tabDoanhThu.Controls.Add(pnlDoanhThuTren);
            tabDoanhThu.Location = new Point(4, 28);
            tabDoanhThu.Name = "tabDoanhThu";
            tabDoanhThu.Padding = new Padding(3);
            tabDoanhThu.Size = new Size(1192, 610);
            tabDoanhThu.TabIndex = 0;
            tabDoanhThu.Text = "Doanh thu";
            tabDoanhThu.UseVisualStyleBackColor = true;
            // 
            // splDoanhThu
            // 
            splDoanhThu.Dock = DockStyle.Fill;
            splDoanhThu.Location = new Point(0, 0);
            splDoanhThu.Name = "splDoanhThu";
            splDoanhThu.Panel1.Controls.Add(dgvDoanhThu);
            splDoanhThu.Panel2.Controls.Add(bdcDoanhThu);
            splDoanhThu.Size = new Size(1186, 536);
            splDoanhThu.SplitterDistance = 560;
            splDoanhThu.TabIndex = 0;
            // 
            // dgvDoanhThu
            // 
            dgvDoanhThu.AllowUserToAddRows = false;
            dgvDoanhThu.AllowUserToDeleteRows = false;
            dgvDoanhThu.AllowUserToResizeRows = false;
            dgvDoanhThu.AutoGenerateColumns = false;
            dgvDoanhThu.BackgroundColor = SystemColors.Window;
            dgvDoanhThu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDoanhThu.Columns.AddRange(new DataGridViewColumn[] { colDtNhan, colDtSoHoaDon, colDtTamTinh, colDtGiamGia, colDtDoanhThu });
            dgvDoanhThu.Dock = DockStyle.Fill;
            dgvDoanhThu.Location = new Point(0, 0);
            dgvDoanhThu.MultiSelect = false;
            dgvDoanhThu.Name = "dgvDoanhThu";
            dgvDoanhThu.ReadOnly = true;
            dgvDoanhThu.RowHeadersVisible = false;
            dgvDoanhThu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoanhThu.Size = new Size(560, 536);
            dgvDoanhThu.TabIndex = 0;
            // 
            // colDtNhan
            // 
            colDtNhan.DataPropertyName = "NhanNgay";
            colDtNhan.HeaderText = "Ngày";
            colDtNhan.Name = "colDtNhan";
            colDtNhan.ReadOnly = true;
            colDtNhan.Width = 90;
            // 
            // colDtSoHoaDon
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDtSoHoaDon.DataPropertyName = "SoHoaDon";
            colDtSoHoaDon.DefaultCellStyle = dataGridViewCellStyle1;
            colDtSoHoaDon.HeaderText = "Số HĐ";
            colDtSoHoaDon.Name = "colDtSoHoaDon";
            colDtSoHoaDon.ReadOnly = true;
            colDtSoHoaDon.Width = 80;
            // 
            // colDtTamTinh
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            colDtTamTinh.DataPropertyName = "TongTamTinh";
            colDtTamTinh.DefaultCellStyle = dataGridViewCellStyle2;
            colDtTamTinh.HeaderText = "Tạm tính";
            colDtTamTinh.Name = "colDtTamTinh";
            colDtTamTinh.ReadOnly = true;
            colDtTamTinh.Width = 120;
            // 
            // colDtGiamGia
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            colDtGiamGia.DataPropertyName = "TongGiamGia";
            colDtGiamGia.DefaultCellStyle = dataGridViewCellStyle3;
            colDtGiamGia.HeaderText = "Giảm giá";
            colDtGiamGia.Name = "colDtGiamGia";
            colDtGiamGia.ReadOnly = true;
            colDtGiamGia.Width = 110;
            // 
            // colDtDoanhThu
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            colDtDoanhThu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDtDoanhThu.DataPropertyName = "DoanhThu";
            colDtDoanhThu.DefaultCellStyle = dataGridViewCellStyle4;
            colDtDoanhThu.HeaderText = "Doanh thu";
            colDtDoanhThu.Name = "colDtDoanhThu";
            colDtDoanhThu.ReadOnly = true;
            // 
            // bdcDoanhThu
            // 
            bdcDoanhThu.BackColor = Color.White;
            bdcDoanhThu.Dock = DockStyle.Fill;
            bdcDoanhThu.DonViTruc = "nghìn đồng";
            bdcDoanhThu.HeSoTruc = new decimal(new int[] { 1000, 0, 0, 0 });
            bdcDoanhThu.LaTien = true;
            bdcDoanhThu.Location = new Point(0, 0);
            bdcDoanhThu.Name = "bdcDoanhThu";
            bdcDoanhThu.Size = new Size(622, 536);
            bdcDoanhThu.TabIndex = 0;
            bdcDoanhThu.TieuDe = "Doanh thu";
            // 
            // lblTongDoanhThu
            // 
            lblTongDoanhThu.BackColor = Color.FromArgb(223, 230, 239);
            lblTongDoanhThu.Dock = DockStyle.Bottom;
            lblTongDoanhThu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTongDoanhThu.Location = new Point(3, 575);
            lblTongDoanhThu.Name = "lblTongDoanhThu";
            lblTongDoanhThu.Padding = new Padding(8, 0, 0, 0);
            lblTongDoanhThu.Size = new Size(1186, 32);
            lblTongDoanhThu.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlDoanhThuTren
            // 
            pnlDoanhThuTren.Controls.Add(rdoTheoNgay);
            pnlDoanhThuTren.Controls.Add(rdoTheoThang);
            pnlDoanhThuTren.Dock = DockStyle.Top;
            pnlDoanhThuTren.Location = new Point(3, 3);
            pnlDoanhThuTren.Name = "pnlDoanhThuTren";
            pnlDoanhThuTren.Size = new Size(1186, 36);
            pnlDoanhThuTren.TabIndex = 1;
            // 
            // rdoTheoNgay
            // 
            rdoTheoNgay.AutoSize = true;
            rdoTheoNgay.Checked = true;
            rdoTheoNgay.Location = new Point(8, 6);
            rdoTheoNgay.Name = "rdoTheoNgay";
            rdoTheoNgay.Size = new Size(96, 23);
            rdoTheoNgay.TabIndex = 0;
            rdoTheoNgay.TabStop = true;
            rdoTheoNgay.Text = "Theo ngày";
            rdoTheoNgay.UseVisualStyleBackColor = true;
            rdoTheoNgay.CheckedChanged += rdoKieuDoanhThu_CheckedChanged;
            // 
            // rdoTheoThang
            // 
            rdoTheoThang.AutoSize = true;
            rdoTheoThang.Location = new Point(130, 6);
            rdoTheoThang.Name = "rdoTheoThang";
            rdoTheoThang.Size = new Size(104, 23);
            rdoTheoThang.TabIndex = 1;
            rdoTheoThang.Text = "Theo tháng";
            rdoTheoThang.UseVisualStyleBackColor = true;
            rdoTheoThang.CheckedChanged += rdoKieuDoanhThu_CheckedChanged;
            // 
            // tabSachBanChay
            // 
            tabSachBanChay.Controls.Add(splBanChay);
            tabSachBanChay.Controls.Add(pnlBanChayTren);
            tabSachBanChay.Location = new Point(4, 28);
            tabSachBanChay.Name = "tabSachBanChay";
            tabSachBanChay.Padding = new Padding(3);
            tabSachBanChay.Size = new Size(1192, 610);
            tabSachBanChay.TabIndex = 1;
            tabSachBanChay.Text = "Sách bán chạy";
            tabSachBanChay.UseVisualStyleBackColor = true;
            // 
            // splBanChay
            // 
            splBanChay.Dock = DockStyle.Fill;
            splBanChay.Location = new Point(0, 0);
            splBanChay.Name = "splBanChay";
            splBanChay.Panel1.Controls.Add(dgvBanChay);
            splBanChay.Panel2.Controls.Add(bdcBanChay);
            splBanChay.Size = new Size(1186, 568);
            splBanChay.SplitterDistance = 560;
            splBanChay.TabIndex = 0;
            // 
            // dgvBanChay
            // 
            dgvBanChay.AllowUserToAddRows = false;
            dgvBanChay.AllowUserToDeleteRows = false;
            dgvBanChay.AllowUserToResizeRows = false;
            dgvBanChay.AutoGenerateColumns = false;
            dgvBanChay.BackgroundColor = SystemColors.Window;
            dgvBanChay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBanChay.Columns.AddRange(new DataGridViewColumn[] { colBcMaSach, colBcTenSach, colBcSoLuong, colBcDoanhThu });
            dgvBanChay.Dock = DockStyle.Fill;
            dgvBanChay.Location = new Point(0, 0);
            dgvBanChay.MultiSelect = false;
            dgvBanChay.Name = "dgvBanChay";
            dgvBanChay.ReadOnly = true;
            dgvBanChay.RowHeadersVisible = false;
            dgvBanChay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBanChay.Size = new Size(560, 568);
            dgvBanChay.TabIndex = 0;
            // 
            // colBcMaSach
            // 
            colBcMaSach.DataPropertyName = "MaSach";
            colBcMaSach.HeaderText = "Mã sách";
            colBcMaSach.Name = "colBcMaSach";
            colBcMaSach.ReadOnly = true;
            colBcMaSach.Width = 80;
            // 
            // colBcTenSach
            // 
            colBcTenSach.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colBcTenSach.DataPropertyName = "TenSach";
            colBcTenSach.HeaderText = "Tên sách";
            colBcTenSach.Name = "colBcTenSach";
            colBcTenSach.ReadOnly = true;
            // 
            // colBcSoLuong
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            colBcSoLuong.DataPropertyName = "SoLuongBan";
            colBcSoLuong.DefaultCellStyle = dataGridViewCellStyle5;
            colBcSoLuong.HeaderText = "SL bán";
            colBcSoLuong.Name = "colBcSoLuong";
            colBcSoLuong.ReadOnly = true;
            colBcSoLuong.Width = 80;
            // 
            // colBcDoanhThu
            // 
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            colBcDoanhThu.DataPropertyName = "DoanhThu";
            colBcDoanhThu.DefaultCellStyle = dataGridViewCellStyle6;
            colBcDoanhThu.HeaderText = "Doanh thu";
            colBcDoanhThu.Name = "colBcDoanhThu";
            colBcDoanhThu.ReadOnly = true;
            colBcDoanhThu.Width = 120;
            // 
            // bdcBanChay
            // 
            bdcBanChay.BackColor = Color.White;
            bdcBanChay.Dock = DockStyle.Fill;
            bdcBanChay.DonViTruc = "cuốn";
            bdcBanChay.HeSoTruc = new decimal(new int[] { 1, 0, 0, 0 });
            bdcBanChay.LaTien = false;
            bdcBanChay.Location = new Point(0, 0);
            bdcBanChay.Name = "bdcBanChay";
            bdcBanChay.Size = new Size(622, 568);
            bdcBanChay.TabIndex = 0;
            bdcBanChay.TieuDe = "Sách bán chạy";
            // 
            // pnlBanChayTren
            // 
            pnlBanChayTren.Controls.Add(lblTop);
            pnlBanChayTren.Controls.Add(nudTop);
            pnlBanChayTren.Controls.Add(rdoTheoSoLuong);
            pnlBanChayTren.Controls.Add(rdoTheoDoanhThu);
            pnlBanChayTren.Dock = DockStyle.Top;
            pnlBanChayTren.Location = new Point(3, 3);
            pnlBanChayTren.Name = "pnlBanChayTren";
            pnlBanChayTren.Size = new Size(1186, 36);
            pnlBanChayTren.TabIndex = 1;
            // 
            // lblTop
            // 
            lblTop.AutoSize = true;
            lblTop.Location = new Point(8, 8);
            lblTop.Name = "lblTop";
            lblTop.Size = new Size(32, 19);
            lblTop.Text = "Top:";
            // 
            // nudTop
            // 
            nudTop.Location = new Point(50, 5);
            nudTop.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudTop.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTop.Name = "nudTop";
            nudTop.Size = new Size(64, 25);
            nudTop.TabIndex = 0;
            nudTop.TextAlign = HorizontalAlignment.Right;
            nudTop.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudTop.ValueChanged += nudTop_ValueChanged;
            // 
            // rdoTheoSoLuong
            // 
            rdoTheoSoLuong.AutoSize = true;
            rdoTheoSoLuong.Checked = true;
            rdoTheoSoLuong.Location = new Point(140, 6);
            rdoTheoSoLuong.Name = "rdoTheoSoLuong";
            rdoTheoSoLuong.Size = new Size(128, 23);
            rdoTheoSoLuong.TabIndex = 1;
            rdoTheoSoLuong.TabStop = true;
            rdoTheoSoLuong.Text = "Theo số lượng";
            rdoTheoSoLuong.UseVisualStyleBackColor = true;
            rdoTheoSoLuong.CheckedChanged += rdoKieuBanChay_CheckedChanged;
            // 
            // rdoTheoDoanhThu
            // 
            rdoTheoDoanhThu.AutoSize = true;
            rdoTheoDoanhThu.Location = new Point(280, 6);
            rdoTheoDoanhThu.Name = "rdoTheoDoanhThu";
            rdoTheoDoanhThu.Size = new Size(136, 23);
            rdoTheoDoanhThu.TabIndex = 2;
            rdoTheoDoanhThu.Text = "Theo doanh thu";
            rdoTheoDoanhThu.UseVisualStyleBackColor = true;
            rdoTheoDoanhThu.CheckedChanged += rdoKieuBanChay_CheckedChanged;
            // 
            // tabSachSapHet
            // 
            tabSachSapHet.Controls.Add(dgvSapHet);
            tabSachSapHet.Controls.Add(pnlSapHetTren);
            tabSachSapHet.Location = new Point(4, 28);
            tabSachSapHet.Name = "tabSachSapHet";
            tabSachSapHet.Padding = new Padding(3);
            tabSachSapHet.Size = new Size(1192, 610);
            tabSachSapHet.TabIndex = 2;
            tabSachSapHet.Text = "Sách sắp hết";
            tabSachSapHet.UseVisualStyleBackColor = true;
            // 
            // dgvSapHet
            // 
            dgvSapHet.AllowUserToAddRows = false;
            dgvSapHet.AllowUserToDeleteRows = false;
            dgvSapHet.AllowUserToResizeRows = false;
            dgvSapHet.AutoGenerateColumns = false;
            dgvSapHet.BackgroundColor = SystemColors.Window;
            dgvSapHet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSapHet.Columns.AddRange(new DataGridViewColumn[] { colShMaSach, colShTenSach, colShLoai, colShTon, colShToiThieu });
            dgvSapHet.Dock = DockStyle.Fill;
            dgvSapHet.Location = new Point(3, 39);
            dgvSapHet.MultiSelect = false;
            dgvSapHet.Name = "dgvSapHet";
            dgvSapHet.ReadOnly = true;
            dgvSapHet.RowHeadersVisible = false;
            dgvSapHet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSapHet.Size = new Size(1186, 568);
            dgvSapHet.TabIndex = 0;
            // 
            // colShMaSach
            // 
            colShMaSach.DataPropertyName = "MaSach";
            colShMaSach.HeaderText = "Mã sách";
            colShMaSach.Name = "colShMaSach";
            colShMaSach.ReadOnly = true;
            colShMaSach.Width = 90;
            // 
            // colShTenSach
            // 
            colShTenSach.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colShTenSach.DataPropertyName = "TenSach";
            colShTenSach.HeaderText = "Tên sách";
            colShTenSach.Name = "colShTenSach";
            colShTenSach.ReadOnly = true;
            // 
            // colShLoai
            // 
            colShLoai.DataPropertyName = "TenLoai";
            colShLoai.HeaderText = "Loại sách";
            colShLoai.Name = "colShLoai";
            colShLoai.ReadOnly = true;
            colShLoai.Width = 200;
            // 
            // colShTon
            // 
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            colShTon.DataPropertyName = "SoLuongTon";
            colShTon.DefaultCellStyle = dataGridViewCellStyle7;
            colShTon.HeaderText = "Tồn";
            colShTon.Name = "colShTon";
            colShTon.ReadOnly = true;
            colShTon.Width = 80;
            // 
            // colShToiThieu
            // 
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
            colShToiThieu.DataPropertyName = "TonToiThieu";
            colShToiThieu.DefaultCellStyle = dataGridViewCellStyle8;
            colShToiThieu.HeaderText = "Tối thiểu";
            colShToiThieu.Name = "colShToiThieu";
            colShToiThieu.ReadOnly = true;
            colShToiThieu.Width = 90;
            // 
            // pnlSapHetTren
            // 
            pnlSapHetTren.Controls.Add(lblLoaiSach);
            pnlSapHetTren.Controls.Add(cboLoaiSach);
            pnlSapHetTren.Controls.Add(btnInSapHet);
            pnlSapHetTren.Dock = DockStyle.Top;
            pnlSapHetTren.Location = new Point(3, 3);
            pnlSapHetTren.Name = "pnlSapHetTren";
            pnlSapHetTren.Size = new Size(1186, 36);
            pnlSapHetTren.TabIndex = 1;
            // 
            // lblLoaiSach
            // 
            lblLoaiSach.AutoSize = true;
            lblLoaiSach.Location = new Point(8, 8);
            lblLoaiSach.Name = "lblLoaiSach";
            lblLoaiSach.Size = new Size(80, 19);
            lblLoaiSach.Text = "Loại sách:";
            // 
            // cboLoaiSach
            // 
            cboLoaiSach.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiSach.FormattingEnabled = true;
            cboLoaiSach.Location = new Point(90, 5);
            cboLoaiSach.Name = "cboLoaiSach";
            cboLoaiSach.Size = new Size(240, 25);
            cboLoaiSach.TabIndex = 0;
            cboLoaiSach.SelectionChangeCommitted += cboLoaiSach_SelectionChangeCommitted;
            // 
            // btnInSapHet
            // 
            btnInSapHet.Location = new Point(346, 1);
            btnInSapHet.Name = "btnInSapHet";
            btnInSapHet.Size = new Size(150, 34);
            btnInSapHet.TabIndex = 1;
            btnInSapHet.Text = "🖨 In danh sách";
            btnInSapHet.UseVisualStyleBackColor = true;
            btnInSapHet.Click += btnInSapHet_Click;
            // 
            // tabThuNgan
            // 
            tabThuNgan.Controls.Add(splThuNgan);
            tabThuNgan.Location = new Point(4, 28);
            tabThuNgan.Name = "tabThuNgan";
            tabThuNgan.Padding = new Padding(3);
            tabThuNgan.Size = new Size(1192, 610);
            tabThuNgan.TabIndex = 3;
            tabThuNgan.Text = "Theo thu ngân";
            tabThuNgan.UseVisualStyleBackColor = true;
            // 
            // splThuNgan
            // 
            splThuNgan.Dock = DockStyle.Fill;
            splThuNgan.Location = new Point(0, 0);
            splThuNgan.Name = "splThuNgan";
            splThuNgan.Panel1.Controls.Add(dgvThuNgan);
            splThuNgan.Panel2.Controls.Add(bdcThuNgan);
            splThuNgan.Size = new Size(1186, 604);
            splThuNgan.SplitterDistance = 560;
            splThuNgan.TabIndex = 0;
            // 
            // dgvThuNgan
            // 
            dgvThuNgan.AllowUserToAddRows = false;
            dgvThuNgan.AllowUserToDeleteRows = false;
            dgvThuNgan.AllowUserToResizeRows = false;
            dgvThuNgan.AutoGenerateColumns = false;
            dgvThuNgan.BackgroundColor = SystemColors.Window;
            dgvThuNgan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvThuNgan.Columns.AddRange(new DataGridViewColumn[] { colTnHoTen, colTnSoHoaDon, colTnDoanhThu });
            dgvThuNgan.Dock = DockStyle.Fill;
            dgvThuNgan.Location = new Point(0, 0);
            dgvThuNgan.MultiSelect = false;
            dgvThuNgan.Name = "dgvThuNgan";
            dgvThuNgan.ReadOnly = true;
            dgvThuNgan.RowHeadersVisible = false;
            dgvThuNgan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThuNgan.Size = new Size(560, 604);
            dgvThuNgan.TabIndex = 0;
            // 
            // colTnHoTen
            // 
            colTnHoTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTnHoTen.DataPropertyName = "HoTen";
            colTnHoTen.HeaderText = "Thu ngân";
            colTnHoTen.Name = "colTnHoTen";
            colTnHoTen.ReadOnly = true;
            // 
            // colTnSoHoaDon
            // 
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTnSoHoaDon.DataPropertyName = "SoHoaDon";
            colTnSoHoaDon.DefaultCellStyle = dataGridViewCellStyle9;
            colTnSoHoaDon.HeaderText = "Số HĐ";
            colTnSoHoaDon.Name = "colTnSoHoaDon";
            colTnSoHoaDon.ReadOnly = true;
            colTnSoHoaDon.Width = 80;
            // 
            // colTnDoanhThu
            // 
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N0";
            colTnDoanhThu.DataPropertyName = "DoanhThu";
            colTnDoanhThu.DefaultCellStyle = dataGridViewCellStyle10;
            colTnDoanhThu.HeaderText = "Doanh thu";
            colTnDoanhThu.Name = "colTnDoanhThu";
            colTnDoanhThu.ReadOnly = true;
            colTnDoanhThu.Width = 140;
            // 
            // bdcThuNgan
            // 
            bdcThuNgan.BackColor = Color.White;
            bdcThuNgan.Dock = DockStyle.Fill;
            bdcThuNgan.DonViTruc = "nghìn đồng";
            bdcThuNgan.HeSoTruc = new decimal(new int[] { 1000, 0, 0, 0 });
            bdcThuNgan.LaTien = true;
            bdcThuNgan.Location = new Point(0, 0);
            bdcThuNgan.Name = "bdcThuNgan";
            bdcThuNgan.Size = new Size(622, 604);
            bdcThuNgan.TabIndex = 0;
            bdcThuNgan.TieuDe = "Doanh thu theo thu ngân";
            // 
            // tabKhuyenMai
            // 
            tabKhuyenMai.Controls.Add(dgvKhuyenMai);
            tabKhuyenMai.Controls.Add(lblGhiChuKM);
            tabKhuyenMai.Location = new Point(4, 28);
            tabKhuyenMai.Name = "tabKhuyenMai";
            tabKhuyenMai.Padding = new Padding(3);
            tabKhuyenMai.Size = new Size(1192, 610);
            tabKhuyenMai.TabIndex = 4;
            tabKhuyenMai.Text = "Hiệu quả khuyến mãi";
            tabKhuyenMai.UseVisualStyleBackColor = true;
            // 
            // dgvKhuyenMai
            // 
            dgvKhuyenMai.AllowUserToAddRows = false;
            dgvKhuyenMai.AllowUserToDeleteRows = false;
            dgvKhuyenMai.AllowUserToResizeRows = false;
            dgvKhuyenMai.AutoGenerateColumns = false;
            dgvKhuyenMai.BackgroundColor = SystemColors.Window;
            dgvKhuyenMai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhuyenMai.Columns.AddRange(new DataGridViewColumn[] { colKmTen, colKmSoHoaDon, colKmTongGiam, colKmDoanhThu });
            dgvKhuyenMai.Dock = DockStyle.Fill;
            dgvKhuyenMai.Location = new Point(3, 3);
            dgvKhuyenMai.MultiSelect = false;
            dgvKhuyenMai.Name = "dgvKhuyenMai";
            dgvKhuyenMai.ReadOnly = true;
            dgvKhuyenMai.RowHeadersVisible = false;
            dgvKhuyenMai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhuyenMai.Size = new Size(1186, 572);
            dgvKhuyenMai.TabIndex = 0;
            // 
            // colKmTen
            // 
            colKmTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colKmTen.DataPropertyName = "TenKM";
            colKmTen.HeaderText = "Khuyến mãi";
            colKmTen.Name = "colKmTen";
            colKmTen.ReadOnly = true;
            // 
            // colKmSoHoaDon
            // 
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight;
            colKmSoHoaDon.DataPropertyName = "SoHoaDonApDung";
            colKmSoHoaDon.DefaultCellStyle = dataGridViewCellStyle11;
            colKmSoHoaDon.HeaderText = "Số HĐ áp dụng";
            colKmSoHoaDon.Name = "colKmSoHoaDon";
            colKmSoHoaDon.ReadOnly = true;
            colKmSoHoaDon.Width = 120;
            // 
            // colKmTongGiam
            // 
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N0";
            colKmTongGiam.DataPropertyName = "TongTienGiam";
            colKmTongGiam.DefaultCellStyle = dataGridViewCellStyle12;
            colKmTongGiam.HeaderText = "Tổng tiền giảm";
            colKmTongGiam.Name = "colKmTongGiam";
            colKmTongGiam.ReadOnly = true;
            colKmTongGiam.Width = 140;
            // 
            // colKmDoanhThu
            // 
            dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N0";
            colKmDoanhThu.DataPropertyName = "DoanhThu";
            colKmDoanhThu.DefaultCellStyle = dataGridViewCellStyle13;
            colKmDoanhThu.HeaderText = "Doanh thu các HĐ";
            colKmDoanhThu.Name = "colKmDoanhThu";
            colKmDoanhThu.ReadOnly = true;
            colKmDoanhThu.Width = 150;
            // 
            // lblGhiChuKM
            // 
            lblGhiChuKM.Dock = DockStyle.Bottom;
            lblGhiChuKM.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblGhiChuKM.ForeColor = Color.DimGray;
            lblGhiChuKM.Location = new Point(3, 575);
            lblGhiChuKM.Name = "lblGhiChuKM";
            lblGhiChuKM.Size = new Size(1186, 32);
            lblGhiChuKM.Text = "Chỉ tính hóa đơn hoàn tất có áp dụng khuyến mãi; hóa đơn đã hủy không được tính (TK-03).";
            lblGhiChuKM.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // errThongKe
            // 
            errThongKe.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errThongKe.ContainerControl = this;
            // 
            // FrmThongKe
            // 
            AcceptButton = btnXem;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1200, 700);
            Controls.Add(tabThongKe);
            Controls.Add(pnlLoc);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1000, 600);
            Name = "FrmThongKe";
            Text = "Thống kê doanh thu";
            Load += FrmThongKe_Load;
            pnlLoc.ResumeLayout(false);
            pnlLoc.PerformLayout();
            tabThongKe.ResumeLayout(false);
            tabThongKe.PerformLayout();
            tabDoanhThu.ResumeLayout(false);
            tabDoanhThu.PerformLayout();
            splDoanhThu.Panel1.ResumeLayout(false);
            splDoanhThu.Panel2.ResumeLayout(false);
            splDoanhThu.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splDoanhThu).EndInit();
            splDoanhThu.ResumeLayout(false);
            splDoanhThu.PerformLayout();
            pnlDoanhThuTren.ResumeLayout(false);
            pnlDoanhThuTren.PerformLayout();
            tabSachBanChay.ResumeLayout(false);
            tabSachBanChay.PerformLayout();
            splBanChay.Panel1.ResumeLayout(false);
            splBanChay.Panel2.ResumeLayout(false);
            splBanChay.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splBanChay).EndInit();
            splBanChay.ResumeLayout(false);
            splBanChay.PerformLayout();
            pnlBanChayTren.ResumeLayout(false);
            pnlBanChayTren.PerformLayout();
            tabSachSapHet.ResumeLayout(false);
            tabSachSapHet.PerformLayout();
            pnlSapHetTren.ResumeLayout(false);
            pnlSapHetTren.PerformLayout();
            tabThuNgan.ResumeLayout(false);
            tabThuNgan.PerformLayout();
            splThuNgan.Panel1.ResumeLayout(false);
            splThuNgan.Panel2.ResumeLayout(false);
            splThuNgan.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splThuNgan).EndInit();
            splThuNgan.ResumeLayout(false);
            splThuNgan.PerformLayout();
            tabKhuyenMai.ResumeLayout(false);
            tabKhuyenMai.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDoanhThu).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBanChay).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSapHet).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvThuNgan).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhuyenMai).EndInit();
            ((System.ComponentModel.ISupportInitialize)errThongKe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlLoc;
        private Label lblTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label lblDenNgay;
        private DateTimePicker dtpDenNgay;
        private Button btnHomNay;
        private Button btn7Ngay;
        private Button btnThangNay;
        private Button btnXem;
        private Button btnXuatCsv;
        private TabControl tabThongKe;
        private TabPage tabDoanhThu;
        private SplitContainer splDoanhThu;
        private DataGridView dgvDoanhThu;
        private DataGridViewTextBoxColumn colDtNhan;
        private DataGridViewTextBoxColumn colDtSoHoaDon;
        private DataGridViewTextBoxColumn colDtTamTinh;
        private DataGridViewTextBoxColumn colDtGiamGia;
        private DataGridViewTextBoxColumn colDtDoanhThu;
        private QuanLyBanSach.GUI.UserControls.BieuDoCot bdcDoanhThu;
        private Label lblTongDoanhThu;
        private Panel pnlDoanhThuTren;
        private RadioButton rdoTheoNgay;
        private RadioButton rdoTheoThang;
        private TabPage tabSachBanChay;
        private SplitContainer splBanChay;
        private DataGridView dgvBanChay;
        private DataGridViewTextBoxColumn colBcMaSach;
        private DataGridViewTextBoxColumn colBcTenSach;
        private DataGridViewTextBoxColumn colBcSoLuong;
        private DataGridViewTextBoxColumn colBcDoanhThu;
        private QuanLyBanSach.GUI.UserControls.BieuDoCot bdcBanChay;
        private Panel pnlBanChayTren;
        private Label lblTop;
        private NumericUpDown nudTop;
        private RadioButton rdoTheoSoLuong;
        private RadioButton rdoTheoDoanhThu;
        private TabPage tabSachSapHet;
        private DataGridView dgvSapHet;
        private DataGridViewTextBoxColumn colShMaSach;
        private DataGridViewTextBoxColumn colShTenSach;
        private DataGridViewTextBoxColumn colShLoai;
        private DataGridViewTextBoxColumn colShTon;
        private DataGridViewTextBoxColumn colShToiThieu;
        private Panel pnlSapHetTren;
        private Label lblLoaiSach;
        private ComboBox cboLoaiSach;
        private Button btnInSapHet;
        private TabPage tabThuNgan;
        private SplitContainer splThuNgan;
        private DataGridView dgvThuNgan;
        private DataGridViewTextBoxColumn colTnHoTen;
        private DataGridViewTextBoxColumn colTnSoHoaDon;
        private DataGridViewTextBoxColumn colTnDoanhThu;
        private QuanLyBanSach.GUI.UserControls.BieuDoCot bdcThuNgan;
        private TabPage tabKhuyenMai;
        private DataGridView dgvKhuyenMai;
        private DataGridViewTextBoxColumn colKmTen;
        private DataGridViewTextBoxColumn colKmSoHoaDon;
        private DataGridViewTextBoxColumn colKmTongGiam;
        private DataGridViewTextBoxColumn colKmDoanhThu;
        private Label lblGhiChuKM;
        private ErrorProvider errThongKe;
    }
}
