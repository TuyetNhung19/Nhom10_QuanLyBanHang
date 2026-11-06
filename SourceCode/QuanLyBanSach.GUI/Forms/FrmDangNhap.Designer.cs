// File: QuanLyBanSach.GUI/Forms/FrmDangNhap.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmDangNhap
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
            pnlTieuDe = new Panel();
            pnlVachDo = new Panel();
            lblSlogan = new Label();
            lblLogo = new Label();
            lblTenDangNhap = new Label();
            txtTenDangNhap = new TextBox();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            chkHienMatKhau = new CheckBox();
            lblLoi = new Label();
            btnDangNhap = new Button();
            btnThoat = new Button();
            errDangNhap = new ErrorProvider(components);
            pnlTieuDe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errDangNhap).BeginInit();
            SuspendLayout();
            // 
            // pnlTieuDe
            // 
            pnlTieuDe.BackColor = Color.FromArgb(14, 72, 116);
            pnlTieuDe.Controls.Add(pnlVachDo);
            pnlTieuDe.Controls.Add(lblSlogan);
            pnlTieuDe.Controls.Add(lblLogo);
            pnlTieuDe.Dock = DockStyle.Top;
            pnlTieuDe.Location = new Point(0, 0);
            pnlTieuDe.Margin = new Padding(4);
            pnlTieuDe.Name = "pnlTieuDe";
            pnlTieuDe.Size = new Size(530, 115);
            pnlTieuDe.TabIndex = 7;
            // 
            // pnlVachDo
            // 
            pnlVachDo.BackColor = Color.FromArgb(209, 58, 61);
            pnlVachDo.Dock = DockStyle.Bottom;
            pnlVachDo.Location = new Point(0, 110);
            pnlVachDo.Margin = new Padding(4);
            pnlVachDo.Name = "pnlVachDo";
            pnlVachDo.Size = new Size(530, 5);
            pnlVachDo.TabIndex = 2;
            // 
            // lblSlogan
            // 
            lblSlogan.AutoSize = true;
            lblSlogan.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblSlogan.ForeColor = Color.WhiteSmoke;
            lblSlogan.Location = new Point(32, 75);
            lblSlogan.Margin = new Padding(4, 0, 4, 0);
            lblSlogan.Name = "lblSlogan";
            lblSlogan.Size = new Size(260, 21);
            lblSlogan.TabIndex = 1;
            lblSlogan.Text = "Nhanh chóng, đúng giá, hiểu khách";
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(25, 18);
            lblLogo.Margin = new Padding(4, 0, 4, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(220, 46);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "📚 BookSPT";
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Location = new Point(38, 144);
            lblTenDangNhap.Margin = new Padding(4, 0, 4, 0);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(128, 23);
            lblTenDangNhap.TabIndex = 8;
            lblTenDangNhap.Text = "Tên đăng nhập:";
            lblTenDangNhap.Click += lblTenDangNhap_Click;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(188, 140);
            txtTenDangNhap.Margin = new Padding(4);
            txtTenDangNhap.MaxLength = 50;
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(299, 30);
            txtTenDangNhap.TabIndex = 0;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(38, 194);
            lblMatKhau.Margin = new Padding(4, 0, 4, 0);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(86, 23);
            lblMatKhau.TabIndex = 9;
            lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(188, 190);
            txtMatKhau.Margin = new Padding(4);
            txtMatKhau.MaxLength = 100;
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(299, 30);
            txtMatKhau.TabIndex = 1;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Location = new Point(188, 232);
            chkHienMatKhau.Margin = new Padding(4);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(144, 27);
            chkHienMatKhau.TabIndex = 2;
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;
            // 
            // lblLoi
            // 
            lblLoi.ForeColor = Color.Red;
            lblLoi.Location = new Point(38, 270);
            lblLoi.Margin = new Padding(4, 0, 4, 0);
            lblLoi.Name = "lblLoi";
            lblLoi.Size = new Size(450, 50);
            lblLoi.TabIndex = 10;
            // 
            // btnDangNhap
            // 
            btnDangNhap.Location = new Point(188, 328);
            btnDangNhap.Margin = new Padding(4);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(148, 42);
            btnDangNhap.TabIndex = 3;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = true;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // btnThoat
            // 
            btnThoat.DialogResult = DialogResult.Cancel;
            btnThoat.Location = new Point(342, 328);
            btnThoat.Margin = new Padding(4);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(145, 42);
            btnThoat.TabIndex = 4;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            // 
            // errDangNhap
            // 
            errDangNhap.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errDangNhap.ContainerControl = this;
            // 
            // FrmDangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnThoat;
            ClientSize = new Size(530, 398);
            Controls.Add(btnThoat);
            Controls.Add(btnDangNhap);
            Controls.Add(lblLoi);
            Controls.Add(chkHienMatKhau);
            Controls.Add(txtMatKhau);
            Controls.Add(lblMatKhau);
            Controls.Add(txtTenDangNhap);
            Controls.Add(lblTenDangNhap);
            Controls.Add(pnlTieuDe);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BookSPT – Đăng nhập";
            Shown += FrmDangNhap_Shown;
            pnlTieuDe.ResumeLayout(false);
            pnlTieuDe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errDangNhap).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTieuDe;
        private Panel pnlVachDo;
        private Label lblLogo;
        private Label lblSlogan;
        private Label lblTenDangNhap;
        private TextBox txtTenDangNhap;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private CheckBox chkHienMatKhau;
        private Label lblLoi;
        private Button btnDangNhap;
        private Button btnThoat;
        private ErrorProvider errDangNhap;
    }
}
