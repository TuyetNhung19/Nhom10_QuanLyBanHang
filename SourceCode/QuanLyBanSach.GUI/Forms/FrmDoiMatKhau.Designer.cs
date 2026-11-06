// File: QuanLyBanSach.GUI/Forms/FrmDoiMatKhau.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmDoiMatKhau
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
            lblTaiKhoan = new Label();
            lblMatKhauCu = new Label();
            txtMatKhauCu = new TextBox();
            lblMatKhauMoi = new Label();
            txtMatKhauMoi = new TextBox();
            lblXacNhan = new Label();
            txtXacNhan = new TextBox();
            chkHienMatKhau = new CheckBox();
            lblGhiChu = new Label();
            btnLuu = new Button();
            btnHuy = new Button();
            errDoiMatKhau = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errDoiMatKhau).BeginInit();
            SuspendLayout();
            // 
            // lblTaiKhoan
            // 
            lblTaiKhoan.AutoSize = true;
            lblTaiKhoan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTaiKhoan.Location = new Point(24, 18);
            lblTaiKhoan.Name = "lblTaiKhoan";
            lblTaiKhoan.Size = new Size(79, 19);
            lblTaiKhoan.TabIndex = 8;
            lblTaiKhoan.Text = "Tài khoản:";
            // 
            // lblMatKhauCu
            // 
            lblMatKhauCu.AutoSize = true;
            lblMatKhauCu.Location = new Point(24, 57);
            lblMatKhauCu.Name = "lblMatKhauCu";
            lblMatKhauCu.Size = new Size(129, 19);
            lblMatKhauCu.TabIndex = 9;
            lblMatKhauCu.Text = "Mật khẩu hiện tại:";
            // 
            // txtMatKhauCu
            // 
            txtMatKhauCu.Location = new Point(176, 54);
            txtMatKhauCu.MaxLength = 100;
            txtMatKhauCu.Name = "txtMatKhauCu";
            txtMatKhauCu.Size = new Size(220, 25);
            txtMatKhauCu.TabIndex = 0;
            txtMatKhauCu.UseSystemPasswordChar = true;
            // 
            // lblMatKhauMoi
            // 
            lblMatKhauMoi.AutoSize = true;
            lblMatKhauMoi.Location = new Point(24, 95);
            lblMatKhauMoi.Name = "lblMatKhauMoi";
            lblMatKhauMoi.Size = new Size(97, 19);
            lblMatKhauMoi.TabIndex = 10;
            lblMatKhauMoi.Text = "Mật khẩu mới:";
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.Location = new Point(176, 92);
            txtMatKhauMoi.MaxLength = 100;
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.Size = new Size(220, 25);
            txtMatKhauMoi.TabIndex = 1;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // lblXacNhan
            // 
            lblXacNhan.AutoSize = true;
            lblXacNhan.Location = new Point(24, 133);
            lblXacNhan.Name = "lblXacNhan";
            lblXacNhan.Size = new Size(141, 19);
            lblXacNhan.TabIndex = 11;
            lblXacNhan.Text = "Nhập lại mật khẩu mới:";
            // 
            // txtXacNhan
            // 
            txtXacNhan.Location = new Point(176, 130);
            txtXacNhan.MaxLength = 100;
            txtXacNhan.Name = "txtXacNhan";
            txtXacNhan.Size = new Size(220, 25);
            txtXacNhan.TabIndex = 2;
            txtXacNhan.UseSystemPasswordChar = true;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Location = new Point(176, 164);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(116, 23);
            chkHienMatKhau.TabIndex = 3;
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblGhiChu.ForeColor = Color.DimGray;
            lblGhiChu.Location = new Point(24, 197);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(293, 15);
            lblGhiChu.TabIndex = 12;
            lblGhiChu.Text = "Mật khẩu mới tối thiểu 6 ký tự và phải khác mật khẩu cũ.";
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(176, 226);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(106, 34);
            btnLuu.TabIndex = 4;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.DialogResult = DialogResult.Cancel;
            btnHuy.Location = new Point(290, 226);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(106, 34);
            btnHuy.TabIndex = 5;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            // 
            // errDoiMatKhau
            // 
            errDoiMatKhau.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errDoiMatKhau.ContainerControl = this;
            // 
            // FrmDoiMatKhau
            // 
            AcceptButton = btnLuu;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnHuy;
            ClientSize = new Size(424, 280);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(lblGhiChu);
            Controls.Add(chkHienMatKhau);
            Controls.Add(txtXacNhan);
            Controls.Add(lblXacNhan);
            Controls.Add(txtMatKhauMoi);
            Controls.Add(lblMatKhauMoi);
            Controls.Add(txtMatKhauCu);
            Controls.Add(lblMatKhauCu);
            Controls.Add(lblTaiKhoan);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmDoiMatKhau";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đổi mật khẩu";
            Load += FrmDoiMatKhau_Load;
            ((System.ComponentModel.ISupportInitialize)errDoiMatKhau).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTaiKhoan;
        private Label lblMatKhauCu;
        private TextBox txtMatKhauCu;
        private Label lblMatKhauMoi;
        private TextBox txtMatKhauMoi;
        private Label lblXacNhan;
        private TextBox txtXacNhan;
        private CheckBox chkHienMatKhau;
        private Label lblGhiChu;
        private Button btnLuu;
        private Button btnHuy;
        private ErrorProvider errDoiMatKhau;
    }
}
