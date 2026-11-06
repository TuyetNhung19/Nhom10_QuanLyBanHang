// File: QuanLyBanSach.GUI/Forms/FrmBaoCaoCuoiNgay.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmBaoCaoCuoiNgay
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
            grpLoc = new GroupBox();
            lblNgay = new Label();
            dtpNgay = new DateTimePicker();
            lblThuNgan = new Label();
            cboThuNgan = new ComboBox();
            btnXem = new Button();
            btnIn = new Button();
            grpTomTat = new GroupBox();
            lblNhanSoHoaDon = new Label();
            lblSoHoaDon = new Label();
            lblNhanSoHoaDonHuy = new Label();
            lblSoHoaDonHuy = new Label();
            lblNhanTongTamTinh = new Label();
            lblTongTamTinh = new Label();
            lblNhanTongGiamGia = new Label();
            lblTongGiamGia = new Label();
            lblNhanDoanhThuThuc = new Label();
            lblDoanhThuThuc = new Label();
            lblNhanTienMat = new Label();
            lblTienMat = new Label();
            lblGhiChu = new Label();
            errBaoCao = new ErrorProvider(components);
            grpLoc.SuspendLayout();
            grpTomTat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errBaoCao).BeginInit();
            SuspendLayout();
            // 
            // grpLoc
            // 
            grpLoc.Controls.Add(lblNgay);
            grpLoc.Controls.Add(dtpNgay);
            grpLoc.Controls.Add(lblThuNgan);
            grpLoc.Controls.Add(cboThuNgan);
            grpLoc.Controls.Add(btnXem);
            grpLoc.Controls.Add(btnIn);
            grpLoc.Location = new Point(12, 12);
            grpLoc.Name = "grpLoc";
            grpLoc.Size = new Size(616, 116);
            grpLoc.TabIndex = 0;
            grpLoc.TabStop = false;
            grpLoc.Text = "Chọn báo cáo";
            // 
            // lblNgay
            // 
            lblNgay.AutoSize = true;
            lblNgay.Location = new Point(16, 34);
            lblNgay.Name = "lblNgay";
            lblNgay.Size = new Size(40, 19);
            lblNgay.Text = "Ngày:";
            // 
            // dtpNgay
            // 
            dtpNgay.CustomFormat = "dd/MM/yyyy";
            dtpNgay.Format = DateTimePickerFormat.Custom;
            dtpNgay.Location = new Point(100, 30);
            dtpNgay.Name = "dtpNgay";
            dtpNgay.Size = new Size(130, 25);
            dtpNgay.TabIndex = 0;
            dtpNgay.ValueChanged += boLoc_Changed;
            // 
            // lblThuNgan
            // 
            lblThuNgan.AutoSize = true;
            lblThuNgan.Location = new Point(16, 74);
            lblThuNgan.Name = "lblThuNgan";
            lblThuNgan.Size = new Size(72, 19);
            lblThuNgan.Text = "Thu ngân:";
            // 
            // cboThuNgan
            // 
            cboThuNgan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThuNgan.FormattingEnabled = true;
            cboThuNgan.Location = new Point(100, 70);
            cboThuNgan.Name = "cboThuNgan";
            cboThuNgan.Size = new Size(250, 25);
            cboThuNgan.TabIndex = 1;
            cboThuNgan.SelectionChangeCommitted += boLoc_Changed;
            // 
            // btnXem
            // 
            btnXem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXem.Location = new Point(380, 26);
            btnXem.Name = "btnXem";
            btnXem.Size = new Size(220, 34);
            btnXem.TabIndex = 2;
            btnXem.Text = "👁 Xem trước";
            btnXem.UseVisualStyleBackColor = true;
            btnXem.Click += btnXem_Click;
            // 
            // btnIn
            // 
            btnIn.Location = new Point(380, 66);
            btnIn.Name = "btnIn";
            btnIn.Size = new Size(220, 34);
            btnIn.TabIndex = 3;
            btnIn.Text = "🖨 In báo cáo";
            btnIn.UseVisualStyleBackColor = true;
            btnIn.Click += btnIn_Click;
            // 
            // grpTomTat
            // 
            grpTomTat.Controls.Add(lblNhanSoHoaDon);
            grpTomTat.Controls.Add(lblSoHoaDon);
            grpTomTat.Controls.Add(lblNhanSoHoaDonHuy);
            grpTomTat.Controls.Add(lblSoHoaDonHuy);
            grpTomTat.Controls.Add(lblNhanTongTamTinh);
            grpTomTat.Controls.Add(lblTongTamTinh);
            grpTomTat.Controls.Add(lblNhanTongGiamGia);
            grpTomTat.Controls.Add(lblTongGiamGia);
            grpTomTat.Controls.Add(lblNhanDoanhThuThuc);
            grpTomTat.Controls.Add(lblDoanhThuThuc);
            grpTomTat.Controls.Add(lblNhanTienMat);
            grpTomTat.Controls.Add(lblTienMat);
            grpTomTat.Location = new Point(12, 136);
            grpTomTat.Name = "grpTomTat";
            grpTomTat.Size = new Size(616, 212);
            grpTomTat.TabIndex = 1;
            grpTomTat.TabStop = false;
            grpTomTat.Text = "Tóm tắt";
            // 
            // lblNhanSoHoaDon
            // 
            lblNhanSoHoaDon.AutoSize = true;
            lblNhanSoHoaDon.Location = new Point(20, 30);
            lblNhanSoHoaDon.Name = "lblNhanSoHoaDon";
            lblNhanSoHoaDon.Size = new Size(160, 19);
            lblNhanSoHoaDon.Text = "Số hóa đơn hoàn tất:";
            // 
            // lblSoHoaDon
            // 
            lblSoHoaDon.Location = new Point(220, 30);
            lblSoHoaDon.Name = "lblSoHoaDon";
            lblSoHoaDon.Size = new Size(200, 19);
            lblSoHoaDon.Text = "–";
            lblSoHoaDon.TextAlign = ContentAlignment.TopRight;
            // 
            // lblNhanSoHoaDonHuy
            // 
            lblNhanSoHoaDonHuy.AutoSize = true;
            lblNhanSoHoaDonHuy.Location = new Point(20, 58);
            lblNhanSoHoaDonHuy.Name = "lblNhanSoHoaDonHuy";
            lblNhanSoHoaDonHuy.Size = new Size(144, 19);
            lblNhanSoHoaDonHuy.Text = "Số hóa đơn đã hủy:";
            // 
            // lblSoHoaDonHuy
            // 
            lblSoHoaDonHuy.Location = new Point(220, 58);
            lblSoHoaDonHuy.Name = "lblSoHoaDonHuy";
            lblSoHoaDonHuy.Size = new Size(200, 19);
            lblSoHoaDonHuy.Text = "–";
            lblSoHoaDonHuy.TextAlign = ContentAlignment.TopRight;
            // 
            // lblNhanTongTamTinh
            // 
            lblNhanTongTamTinh.AutoSize = true;
            lblNhanTongTamTinh.Location = new Point(20, 86);
            lblNhanTongTamTinh.Name = "lblNhanTongTamTinh";
            lblNhanTongTamTinh.Size = new Size(112, 19);
            lblNhanTongTamTinh.Text = "Tổng tạm tính:";
            // 
            // lblTongTamTinh
            // 
            lblTongTamTinh.Location = new Point(220, 86);
            lblTongTamTinh.Name = "lblTongTamTinh";
            lblTongTamTinh.Size = new Size(200, 19);
            lblTongTamTinh.Text = "–";
            lblTongTamTinh.TextAlign = ContentAlignment.TopRight;
            // 
            // lblNhanTongGiamGia
            // 
            lblNhanTongGiamGia.AutoSize = true;
            lblNhanTongGiamGia.Location = new Point(20, 114);
            lblNhanTongGiamGia.Name = "lblNhanTongGiamGia";
            lblNhanTongGiamGia.Size = new Size(112, 19);
            lblNhanTongGiamGia.Text = "Tổng giảm giá:";
            // 
            // lblTongGiamGia
            // 
            lblTongGiamGia.Location = new Point(220, 114);
            lblTongGiamGia.Name = "lblTongGiamGia";
            lblTongGiamGia.Size = new Size(200, 19);
            lblTongGiamGia.Text = "–";
            lblTongGiamGia.TextAlign = ContentAlignment.TopRight;
            // 
            // lblNhanDoanhThuThuc
            // 
            lblNhanDoanhThuThuc.AutoSize = true;
            lblNhanDoanhThuThuc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNhanDoanhThuThuc.Location = new Point(20, 142);
            lblNhanDoanhThuThuc.Name = "lblNhanDoanhThuThuc";
            lblNhanDoanhThuThuc.Size = new Size(120, 19);
            lblNhanDoanhThuThuc.Text = "Doanh thu thực:";
            // 
            // lblDoanhThuThuc
            // 
            lblDoanhThuThuc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDoanhThuThuc.Location = new Point(220, 142);
            lblDoanhThuThuc.Name = "lblDoanhThuThuc";
            lblDoanhThuThuc.Size = new Size(200, 19);
            lblDoanhThuThuc.Text = "–";
            lblDoanhThuThuc.TextAlign = ContentAlignment.TopRight;
            // 
            // lblNhanTienMat
            // 
            lblNhanTienMat.AutoSize = true;
            lblNhanTienMat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNhanTienMat.Location = new Point(20, 170);
            lblNhanTienMat.Name = "lblNhanTienMat";
            lblNhanTienMat.Size = new Size(136, 19);
            lblNhanTienMat.Text = "Tiền mặt phải có:";
            // 
            // lblTienMat
            // 
            lblTienMat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTienMat.Location = new Point(220, 170);
            lblTienMat.Name = "lblTienMat";
            lblTienMat.Size = new Size(200, 19);
            lblTienMat.Text = "–";
            lblTienMat.TextAlign = ContentAlignment.TopRight;
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblGhiChu.ForeColor = Color.DimGray;
            lblGhiChu.Location = new Point(12, 356);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(400, 15);
            lblGhiChu.Text = "Báo cáo tổng hợp, không thay thế đối soát ca. Hóa đơn hủy không tính vào doanh thu.";
            // 
            // errBaoCao
            // 
            errBaoCao.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errBaoCao.ContainerControl = this;
            // 
            // FrmBaoCaoCuoiNgay
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(640, 384);
            Controls.Add(grpLoc);
            Controls.Add(grpTomTat);
            Controls.Add(lblGhiChu);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmBaoCaoCuoiNgay";
            Text = "Báo cáo cuối ngày";
            Load += FrmBaoCaoCuoiNgay_Load;
            grpLoc.ResumeLayout(false);
            grpLoc.PerformLayout();
            grpTomTat.ResumeLayout(false);
            grpTomTat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errBaoCao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpLoc;
        private Label lblNgay;
        private DateTimePicker dtpNgay;
        private Label lblThuNgan;
        private ComboBox cboThuNgan;
        private Button btnXem;
        private Button btnIn;
        private GroupBox grpTomTat;
        private Label lblNhanSoHoaDon;
        private Label lblSoHoaDon;
        private Label lblNhanSoHoaDonHuy;
        private Label lblSoHoaDonHuy;
        private Label lblNhanTongTamTinh;
        private Label lblTongTamTinh;
        private Label lblNhanTongGiamGia;
        private Label lblTongGiamGia;
        private Label lblNhanDoanhThuThuc;
        private Label lblDoanhThuThuc;
        private Label lblNhanTienMat;
        private Label lblTienMat;
        private Label lblGhiChu;
        private ErrorProvider errBaoCao;
    }
}
