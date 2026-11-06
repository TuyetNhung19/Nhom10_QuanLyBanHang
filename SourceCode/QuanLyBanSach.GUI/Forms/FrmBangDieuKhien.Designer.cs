// File: QuanLyBanSach.GUI/Forms/FrmBangDieuKhien.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmBangDieuKhien
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
            ucBangDieuKhien = new QuanLyBanSach.GUI.UserControls.UcBangDieuKhien();
            SuspendLayout();
            // 
            // ucBangDieuKhien
            // 
            ucBangDieuKhien.BackColor = Color.White;
            ucBangDieuKhien.Dock = DockStyle.Fill;
            ucBangDieuKhien.Font = new Font("Segoe UI", 10F);
            ucBangDieuKhien.Location = new Point(0, 0);
            ucBangDieuKhien.Name = "ucBangDieuKhien";
            ucBangDieuKhien.Size = new Size(1100, 640);
            ucBangDieuKhien.TabIndex = 0;
            // 
            // FrmBangDieuKhien
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1100, 640);
            Controls.Add(ucBangDieuKhien);
            Font = new Font("Segoe UI", 10F);
            KeyPreview = true;
            MinimumSize = new Size(900, 560);
            Name = "FrmBangDieuKhien";
            Text = "Bảng điều khiển";
            KeyDown += FrmBangDieuKhien_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private QuanLyBanSach.GUI.UserControls.UcBangDieuKhien ucBangDieuKhien;
    }
}
