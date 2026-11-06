// File: QuanLyBanSach.GUI/Forms/FrmDonTreo.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmDonTreo
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
            splDonTreo = new SplitContainer();
            dgvDonTreo = new DataGridView();
            colGioTreo = new DataGridViewTextBoxColumn();
            colKhach = new DataGridViewTextBoxColumn();
            colSoCuon = new DataGridViewTextBoxColumn();
            colTamTinh = new DataGridViewTextBoxColumn();
            dgvXemChiTiet = new DataGridView();
            colCtTen = new DataGridViewTextBoxColumn();
            colCtSoLuong = new DataGridViewTextBoxColumn();
            colCtDonGia = new DataGridViewTextBoxColumn();
            colCtThanhTien = new DataGridViewTextBoxColumn();
            lblXemChiTiet = new Label();
            pnlNut = new Panel();
            lblSoDon = new Label();
            lblChuThich = new Label();
            btnGoiLai = new Button();
            btnHuyDon = new Button();
            btnDong = new Button();
            ((System.ComponentModel.ISupportInitialize)splDonTreo).BeginInit();
            splDonTreo.Panel1.SuspendLayout();
            splDonTreo.Panel2.SuspendLayout();
            splDonTreo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDonTreo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvXemChiTiet).BeginInit();
            pnlNut.SuspendLayout();
            SuspendLayout();
            // 
            // splDonTreo
            // 
            splDonTreo.Dock = DockStyle.Fill;
            splDonTreo.Location = new Point(0, 0);
            splDonTreo.Margin = new Padding(4, 4, 4, 4);
            splDonTreo.Name = "splDonTreo";
            splDonTreo.Orientation = Orientation.Horizontal;
            // 
            // splDonTreo.Panel1
            // 
            splDonTreo.Panel1.Controls.Add(dgvDonTreo);
            // 
            // splDonTreo.Panel2
            // 
            splDonTreo.Panel2.Controls.Add(dgvXemChiTiet);
            splDonTreo.Panel2.Controls.Add(lblXemChiTiet);
            splDonTreo.Size = new Size(975, 570);
            splDonTreo.SplitterDistance = 275;
            splDonTreo.SplitterWidth = 5;
            splDonTreo.TabIndex = 0;
            // 
            // dgvDonTreo
            // 
            dgvDonTreo.AllowUserToAddRows = false;
            dgvDonTreo.AllowUserToDeleteRows = false;
            dgvDonTreo.AllowUserToResizeRows = false;
            dgvDonTreo.AutoGenerateColumns = false;
            dgvDonTreo.BackgroundColor = SystemColors.Window;
            dgvDonTreo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDonTreo.Columns.AddRange(new DataGridViewColumn[] { colGioTreo, colKhach, colSoCuon, colTamTinh });
            dgvDonTreo.Dock = DockStyle.Fill;
            dgvDonTreo.Location = new Point(0, 0);
            dgvDonTreo.Margin = new Padding(4, 4, 4, 4);
            dgvDonTreo.MultiSelect = false;
            dgvDonTreo.Name = "dgvDonTreo";
            dgvDonTreo.ReadOnly = true;
            dgvDonTreo.RowHeadersVisible = false;
            dgvDonTreo.RowHeadersWidth = 51;
            dgvDonTreo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonTreo.Size = new Size(975, 275);
            dgvDonTreo.TabIndex = 0;
            dgvDonTreo.CellDoubleClick += dgvDonTreo_CellDoubleClick;
            dgvDonTreo.CellFormatting += dgvDonTreo_CellFormatting;
            dgvDonTreo.SelectionChanged += dgvDonTreo_SelectionChanged;
            // 
            // colGioTreo
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colGioTreo.DataPropertyName = "GioTreoText";
            colGioTreo.DefaultCellStyle = dataGridViewCellStyle1;
            colGioTreo.HeaderText = "Giờ treo";
            colGioTreo.MinimumWidth = 6;
            colGioTreo.Name = "colGioTreo";
            colGioTreo.ReadOnly = true;
            colGioTreo.Width = 100;
            // 
            // colKhach
            // 
            colKhach.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colKhach.DataPropertyName = "TenKhachHienThi";
            colKhach.HeaderText = "Khách hàng";
            colKhach.MinimumWidth = 6;
            colKhach.Name = "colKhach";
            colKhach.ReadOnly = true;
            // 
            // colSoCuon
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoCuon.DataPropertyName = "SoCuon";
            colSoCuon.DefaultCellStyle = dataGridViewCellStyle2;
            colSoCuon.HeaderText = "Số cuốn";
            colSoCuon.MinimumWidth = 6;
            colSoCuon.Name = "colSoCuon";
            colSoCuon.ReadOnly = true;
            colSoCuon.Width = 100;
            // 
            // colTamTinh
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            colTamTinh.DataPropertyName = "TamTinh";
            colTamTinh.DefaultCellStyle = dataGridViewCellStyle3;
            colTamTinh.HeaderText = "Tạm tính";
            colTamTinh.MinimumWidth = 6;
            colTamTinh.Name = "colTamTinh";
            colTamTinh.ReadOnly = true;
            colTamTinh.Width = 150;
            // 
            // dgvXemChiTiet
            // 
            dgvXemChiTiet.AllowUserToAddRows = false;
            dgvXemChiTiet.AllowUserToDeleteRows = false;
            dgvXemChiTiet.AllowUserToResizeRows = false;
            dgvXemChiTiet.AutoGenerateColumns = false;
            dgvXemChiTiet.BackgroundColor = SystemColors.Window;
            dgvXemChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvXemChiTiet.Columns.AddRange(new DataGridViewColumn[] { colCtTen, colCtSoLuong, colCtDonGia, colCtThanhTien });
            dgvXemChiTiet.Dock = DockStyle.Fill;
            dgvXemChiTiet.Location = new Point(0, 35);
            dgvXemChiTiet.Margin = new Padding(4, 4, 4, 4);
            dgvXemChiTiet.MultiSelect = false;
            dgvXemChiTiet.Name = "dgvXemChiTiet";
            dgvXemChiTiet.ReadOnly = true;
            dgvXemChiTiet.RowHeadersVisible = false;
            dgvXemChiTiet.RowHeadersWidth = 51;
            dgvXemChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvXemChiTiet.Size = new Size(975, 255);
            dgvXemChiTiet.TabIndex = 1;
            dgvXemChiTiet.TabStop = false;
            // 
            // colCtTen
            // 
            colCtTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCtTen.DataPropertyName = "TenSach";
            colCtTen.HeaderText = "Tên sách";
            colCtTen.MinimumWidth = 6;
            colCtTen.Name = "colCtTen";
            colCtTen.ReadOnly = true;
            // 
            // colCtSoLuong
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCtSoLuong.DataPropertyName = "SoLuong";
            colCtSoLuong.DefaultCellStyle = dataGridViewCellStyle4;
            colCtSoLuong.HeaderText = "SL";
            colCtSoLuong.MinimumWidth = 6;
            colCtSoLuong.Name = "colCtSoLuong";
            colCtSoLuong.ReadOnly = true;
            colCtSoLuong.Width = 70;
            // 
            // colCtDonGia
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            colCtDonGia.DataPropertyName = "DonGia";
            colCtDonGia.DefaultCellStyle = dataGridViewCellStyle5;
            colCtDonGia.HeaderText = "Đơn giá";
            colCtDonGia.MinimumWidth = 6;
            colCtDonGia.Name = "colCtDonGia";
            colCtDonGia.ReadOnly = true;
            colCtDonGia.Width = 120;
            // 
            // colCtThanhTien
            // 
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            colCtThanhTien.DataPropertyName = "ThanhTien";
            colCtThanhTien.DefaultCellStyle = dataGridViewCellStyle6;
            colCtThanhTien.HeaderText = "Thành tiền";
            colCtThanhTien.MinimumWidth = 6;
            colCtThanhTien.Name = "colCtThanhTien";
            colCtThanhTien.ReadOnly = true;
            colCtThanhTien.Width = 130;
            // 
            // lblXemChiTiet
            // 
            lblXemChiTiet.Dock = DockStyle.Top;
            lblXemChiTiet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblXemChiTiet.Location = new Point(0, 0);
            lblXemChiTiet.Margin = new Padding(4, 0, 4, 0);
            lblXemChiTiet.Name = "lblXemChiTiet";
            lblXemChiTiet.Padding = new Padding(8, 0, 0, 0);
            lblXemChiTiet.Size = new Size(975, 35);
            lblXemChiTiet.TabIndex = 0;
            lblXemChiTiet.Text = "Chi tiết đơn";
            lblXemChiTiet.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlNut
            // 
            pnlNut.Controls.Add(lblSoDon);
            pnlNut.Controls.Add(lblChuThich);
            pnlNut.Controls.Add(btnGoiLai);
            pnlNut.Controls.Add(btnHuyDon);
            pnlNut.Controls.Add(btnDong);
            pnlNut.Dock = DockStyle.Bottom;
            pnlNut.Location = new Point(0, 570);
            pnlNut.Margin = new Padding(4, 4, 4, 4);
            pnlNut.Name = "pnlNut";
            pnlNut.Size = new Size(975, 70);
            pnlNut.TabIndex = 1;
            // 
            // lblSoDon
            // 
            lblSoDon.AutoSize = true;
            lblSoDon.Location = new Point(15, 24);
            lblSoDon.Margin = new Padding(4, 0, 4, 0);
            lblSoDon.Name = "lblSoDon";
            lblSoDon.Size = new Size(106, 23);
            lblSoDon.TabIndex = 0;
            lblSoDon.Text = "0/5 đơn treo";
            // 
            // lblChuThich
            // 
            lblChuThich.AutoSize = true;
            lblChuThich.BackColor = Color.LightYellow;
            lblChuThich.Location = new Point(162, 24);
            lblChuThich.Margin = new Padding(4, 0, 4, 0);
            lblChuThich.Name = "lblChuThich";
            lblChuThich.Padding = new Padding(5, 0, 5, 0);
            lblChuThich.Size = new Size(151, 23);
            lblChuThich.TabIndex = 1;
            lblChuThich.Text = "Treo quá 30 phút";
            // 
            // btnGoiLai
            // 
            btnGoiLai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGoiLai.Location = new Point(482, 14);
            btnGoiLai.Margin = new Padding(4, 4, 4, 4);
            btnGoiLai.Name = "btnGoiLai";
            btnGoiLai.Size = new Size(188, 42);
            btnGoiLai.TabIndex = 0;
            btnGoiLai.Text = "↩ Gọi lại (Enter)";
            btnGoiLai.UseVisualStyleBackColor = true;
            btnGoiLai.Click += btnGoiLai_Click;
            // 
            // btnHuyDon
            // 
            btnHuyDon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHuyDon.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHuyDon.ForeColor = Color.DarkRed;
            btnHuyDon.Location = new Point(678, 14);
            btnHuyDon.Margin = new Padding(4, 4, 4, 4);
            btnHuyDon.Name = "btnHuyDon";
            btnHuyDon.Size = new Size(138, 42);
            btnHuyDon.TabIndex = 1;
            btnHuyDon.Text = "🗑 Hủy đơn";
            btnHuyDon.UseVisualStyleBackColor = true;
            btnHuyDon.Click += btnHuyDon_Click;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.Location = new Point(822, 14);
            btnDong.Margin = new Padding(4, 4, 4, 4);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(138, 42);
            btnDong.TabIndex = 2;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // FrmDonTreo
            // 
            AcceptButton = btnGoiLai;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnDong;
            ClientSize = new Size(975, 640);
            Controls.Add(splDonTreo);
            Controls.Add(pnlNut);
            Margin = new Padding(4, 4, 4, 4);
            MinimizeBox = false;
            MinimumSize = new Size(796, 513);
            Name = "FrmDonTreo";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đơn treo";
            Load += FrmDonTreo_Load;
            splDonTreo.Panel1.ResumeLayout(false);
            splDonTreo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splDonTreo).EndInit();
            splDonTreo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDonTreo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvXemChiTiet).EndInit();
            pnlNut.ResumeLayout(false);
            pnlNut.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splDonTreo;
        private DataGridView dgvDonTreo;
        private DataGridViewTextBoxColumn colGioTreo;
        private DataGridViewTextBoxColumn colKhach;
        private DataGridViewTextBoxColumn colSoCuon;
        private DataGridViewTextBoxColumn colTamTinh;
        private DataGridView dgvXemChiTiet;
        private Label lblXemChiTiet;
        private DataGridViewTextBoxColumn colCtTen;
        private DataGridViewTextBoxColumn colCtSoLuong;
        private DataGridViewTextBoxColumn colCtDonGia;
        private DataGridViewTextBoxColumn colCtThanhTien;
        private Panel pnlNut;
        private Label lblSoDon;
        private Label lblChuThich;
        private Button btnGoiLai;
        private Button btnHuyDon;
        private Button btnDong;
    }
}
