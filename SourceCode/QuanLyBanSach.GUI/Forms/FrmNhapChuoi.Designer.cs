// File: QuanLyBanSach.GUI/Forms/FrmNhapChuoi.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmNhapChuoi
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
            lblNhan = new Label();
            txtGiaTri = new TextBox();
            chkHienKyTu = new CheckBox();
            btnOK = new Button();
            btnHuy = new Button();
            SuspendLayout();
            // 
            // lblNhan
            // 
            lblNhan.Location = new Point(16, 14);
            lblNhan.Name = "lblNhan";
            lblNhan.Size = new Size(380, 40);
            lblNhan.UseCompatibleTextRendering = true;
            lblNhan.TabIndex = 3;
            lblNhan.Text = "Giá trị:";
            // 
            // txtGiaTri
            // 
            txtGiaTri.Location = new Point(16, 58);
            txtGiaTri.MaxLength = 255;
            txtGiaTri.Name = "txtGiaTri";
            txtGiaTri.Size = new Size(380, 25);
            txtGiaTri.TabIndex = 0;
            // 
            // chkHienKyTu
            // 
            chkHienKyTu.AutoSize = true;
            chkHienKyTu.Location = new Point(16, 90);
            chkHienKyTu.Name = "chkHienKyTu";
            chkHienKyTu.Size = new Size(110, 23);
            chkHienKyTu.TabIndex = 1;
            chkHienKyTu.Text = "Hiện ký tự";
            chkHienKyTu.UseVisualStyleBackColor = true;
            chkHienKyTu.Visible = false;
            chkHienKyTu.CheckedChanged += chkHienKyTu_CheckedChanged;
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(184, 124);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(100, 34);
            btnOK.TabIndex = 2;
            btnOK.Text = "Đồng ý";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnHuy
            // 
            btnHuy.DialogResult = DialogResult.Cancel;
            btnHuy.Location = new Point(296, 124);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(100, 34);
            btnHuy.TabIndex = 4;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            // 
            // FrmNhapChuoi
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnHuy;
            ClientSize = new Size(412, 172);
            Controls.Add(lblNhan);
            Controls.Add(txtGiaTri);
            Controls.Add(chkHienKyTu);
            Controls.Add(btnOK);
            Controls.Add(btnHuy);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmNhapChuoi";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nhập dữ liệu";
            Shown += FrmNhapChuoi_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNhan;
        private TextBox txtGiaTri;
        private CheckBox chkHienKyTu;
        private Button btnOK;
        private Button btnHuy;
    }
}
