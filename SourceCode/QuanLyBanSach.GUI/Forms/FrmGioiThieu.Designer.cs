// File: QuanLyBanSach.GUI/Forms/FrmGioiThieu.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmGioiThieu
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
            lblTenSanPham = new Label();
            lblSlogan = new Label();
            lblDeTai = new Label();
            lblHocPhan = new Label();
            lblKhoa = new Label();
            lblGVHD = new Label();
            grpThanhVien = new GroupBox();
            dgvThanhVien = new DataGridView();
            colSTT = new DataGridViewTextBoxColumn();
            colMSSV = new DataGridViewTextBoxColumn();
            colHoTen = new DataGridViewTextBoxColumn();
            colNhiemVu = new DataGridViewTextBoxColumn();
            btnDong = new Button();
            grpThanhVien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvThanhVien).BeginInit();
            SuspendLayout();
            // 
            // lblTenSanPham
            // 
            lblTenSanPham.AutoSize = true;
            lblTenSanPham.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTenSanPham.ForeColor = Color.FromArgb(14, 72, 116);
            lblTenSanPham.Location = new Point(25, 18);
            lblTenSanPham.Margin = new Padding(4, 0, 4, 0);
            lblTenSanPham.Name = "lblTenSanPham";
            lblTenSanPham.Size = new Size(193, 41);
            lblTenSanPham.TabIndex = 0;
            lblTenSanPham.Text = "📚 BookSPT";
            lblTenSanPham.Click += lblTenSanPham_Click;
            // 
            // lblSlogan
            // 
            lblSlogan.AutoSize = true;
            lblSlogan.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblSlogan.Location = new Point(30, 65);
            lblSlogan.Margin = new Padding(4, 0, 4, 0);
            lblSlogan.Name = "lblSlogan";
            lblSlogan.Size = new Size(268, 23);
            lblSlogan.TabIndex = 1;
            lblSlogan.Text = "Nhanh chóng, đúng giá, hiểu khách";
            // 
            // lblDeTai
            // 
            lblDeTai.AutoSize = true;
            lblDeTai.Location = new Point(30, 108);
            lblDeTai.Margin = new Padding(4, 0, 4, 0);
            lblDeTai.Name = "lblDeTai";
            lblDeTai.Size = new Size(448, 23);
            lblDeTai.TabIndex = 2;
            lblDeTai.Text = "Đề tài 7: Quản lý bán hàng – Áp dụng cho cửa hàng sách";
            // 
            // lblHocPhan
            // 
            lblHocPhan.AutoSize = true;
            lblHocPhan.Location = new Point(30, 140);
            lblHocPhan.Margin = new Padding(4, 0, 4, 0);
            lblHocPhan.Name = "lblHocPhan";
            lblHocPhan.Size = new Size(420, 23);
            lblHocPhan.TabIndex = 3;
            lblHocPhan.Text = "Đồ án học phần COMP1019 – Lập trình trên Windows";
            // 
            // lblKhoa
            // 
            lblKhoa.AutoSize = true;
            lblKhoa.Location = new Point(30, 172);
            lblKhoa.Margin = new Padding(4, 0, 4, 0);
            lblKhoa.Name = "lblKhoa";
            lblKhoa.Size = new Size(556, 23);
            lblKhoa.TabIndex = 4;
            lblKhoa.Text = "Khoa Công nghệ Thông tin – Trường Đại học Sư phạm TP. Hồ Chí Minh";
            // 
            // lblGVHD
            // 
            lblGVHD.AutoSize = true;
            lblGVHD.Location = new Point(30, 205);
            lblGVHD.Margin = new Padding(4, 0, 4, 0);
            lblGVHD.Name = "lblGVHD";
            lblGVHD.Size = new Size(343, 23);
            lblGVHD.TabIndex = 5;
            lblGVHD.Text = "Giảng viên hướng dẫn: ThS. Lê Thanh Thoại";
            // 
            // grpThanhVien
            // 
            grpThanhVien.Controls.Add(dgvThanhVien);
            grpThanhVien.Location = new Point(25, 248);
            grpThanhVien.Margin = new Padding(4);
            grpThanhVien.Name = "grpThanhVien";
            grpThanhVien.Padding = new Padding(10);
            grpThanhVien.Size = new Size(730, 238);
            grpThanhVien.TabIndex = 6;
            grpThanhVien.TabStop = false;
            grpThanhVien.Text = "Thành viên nhóm";
            // 
            // dgvThanhVien
            // 
            dgvThanhVien.AllowUserToAddRows = false;
            dgvThanhVien.AllowUserToDeleteRows = false;
            dgvThanhVien.AutoGenerateColumns = false;
            dgvThanhVien.BackgroundColor = SystemColors.Window;
            dgvThanhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvThanhVien.Columns.AddRange(new DataGridViewColumn[] { colSTT, colMSSV, colHoTen, colNhiemVu });
            dgvThanhVien.Dock = DockStyle.Fill;
            dgvThanhVien.Location = new Point(10, 33);
            dgvThanhVien.Margin = new Padding(4);
            dgvThanhVien.MultiSelect = false;
            dgvThanhVien.Name = "dgvThanhVien";
            dgvThanhVien.ReadOnly = true;
            dgvThanhVien.RowHeadersVisible = false;
            dgvThanhVien.RowHeadersWidth = 51;
            dgvThanhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThanhVien.Size = new Size(710, 195);
            dgvThanhVien.TabIndex = 0;
            // 
            // colSTT
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSTT.DataPropertyName = "STT";
            colSTT.DefaultCellStyle = dataGridViewCellStyle1;
            colSTT.HeaderText = "STT";
            colSTT.MinimumWidth = 6;
            colSTT.Name = "colSTT";
            colSTT.ReadOnly = true;
            colSTT.Width = 55;
            // 
            // colMSSV
            // 
            colMSSV.DataPropertyName = "MSSV";
            colMSSV.HeaderText = "MSSV";
            colMSSV.MinimumWidth = 6;
            colMSSV.Name = "colMSSV";
            colMSSV.ReadOnly = true;
            colMSSV.Width = 140;
            // 
            // colHoTen
            // 
            colHoTen.DataPropertyName = "HoTen";
            colHoTen.HeaderText = "Họ tên";
            colHoTen.MinimumWidth = 6;
            colHoTen.Name = "colHoTen";
            colHoTen.ReadOnly = true;
            colHoTen.Width = 200;
            // 
            // colNhiemVu
            // 
            colNhiemVu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNhiemVu.DataPropertyName = "NhiemVu";
            colNhiemVu.HeaderText = "Nhiệm vụ";
            colNhiemVu.MinimumWidth = 6;
            colNhiemVu.Name = "colNhiemVu";
            colNhiemVu.ReadOnly = true;
            // 
            // btnDong
            // 
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.Location = new Point(622, 500);
            btnDong.Margin = new Padding(4);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(132, 42);
            btnDong.TabIndex = 0;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // FrmGioiThieu
            // 
            AcceptButton = btnDong;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnDong;
            ClientSize = new Size(780, 562);
            Controls.Add(btnDong);
            Controls.Add(grpThanhVien);
            Controls.Add(lblGVHD);
            Controls.Add(lblKhoa);
            Controls.Add(lblHocPhan);
            Controls.Add(lblDeTai);
            Controls.Add(lblSlogan);
            Controls.Add(lblTenSanPham);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmGioiThieu";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Giới thiệu BookSPT";
            Load += FrmGioiThieu_Load;
            grpThanhVien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvThanhVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTenSanPham;
        private Label lblSlogan;
        private Label lblDeTai;
        private Label lblHocPhan;
        private Label lblKhoa;
        private Label lblGVHD;
        private GroupBox grpThanhVien;
        private DataGridView dgvThanhVien;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colMSSV;
        private DataGridViewTextBoxColumn colHoTen;
        private DataGridViewTextBoxColumn colNhiemVu;
        private Button btnDong;
    }
}
