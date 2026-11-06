// File: QuanLyBanSach.GUI/Forms/FrmInHoaDon.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmInHoaDon
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
            pnlTren = new Panel();
            lblKhoGiay = new Label();
            cboKhoGiay = new ComboBox();
            lblThongTin = new Label();
            btnIn = new Button();
            btnDong = new Button();
            ppcXemTruoc = new PrintPreviewControl();
            pnlTren.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTren
            // 
            pnlTren.Controls.Add(lblKhoGiay);
            pnlTren.Controls.Add(cboKhoGiay);
            pnlTren.Controls.Add(lblThongTin);
            pnlTren.Controls.Add(btnIn);
            pnlTren.Controls.Add(btnDong);
            pnlTren.Dock = DockStyle.Top;
            pnlTren.Location = new Point(0, 0);
            pnlTren.Name = "pnlTren";
            pnlTren.Size = new Size(720, 52);
            pnlTren.TabIndex = 0;
            // 
            // lblKhoGiay
            // 
            lblKhoGiay.AutoSize = true;
            lblKhoGiay.Location = new Point(12, 16);
            lblKhoGiay.Name = "lblKhoGiay";
            lblKhoGiay.Size = new Size(72, 19);
            lblKhoGiay.Text = "Khổ giấy:";
            // 
            // cboKhoGiay
            // 
            cboKhoGiay.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoGiay.FormattingEnabled = true;
            cboKhoGiay.Items.AddRange(new object[] { "K80 (giấy nhiệt 80 mm)", "A5" });
            cboKhoGiay.Location = new Point(84, 12);
            cboKhoGiay.Name = "cboKhoGiay";
            cboKhoGiay.Size = new Size(190, 25);
            cboKhoGiay.TabIndex = 0;
            cboKhoGiay.SelectedIndexChanged += cboKhoGiay_SelectedIndexChanged;
            // 
            // lblThongTin
            // 
            lblThongTin.AutoSize = true;
            lblThongTin.Location = new Point(286, 16);
            lblThongTin.Name = "lblThongTin";
            lblThongTin.Size = new Size(120, 19);
            lblThongTin.Text = "";
            // 
            // btnIn
            // 
            btnIn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnIn.Location = new Point(486, 9);
            btnIn.Name = "btnIn";
            btnIn.Size = new Size(110, 34);
            btnIn.TabIndex = 1;
            btnIn.Text = "🖨 In...";
            btnIn.UseVisualStyleBackColor = true;
            btnIn.Click += btnIn_Click;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.Location = new Point(602, 9);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(106, 34);
            btnDong.TabIndex = 2;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // ppcXemTruoc
            // 
            ppcXemTruoc.AutoZoom = true;
            ppcXemTruoc.BackColor = SystemColors.ControlDark;
            ppcXemTruoc.Dock = DockStyle.Fill;
            ppcXemTruoc.Location = new Point(0, 52);
            ppcXemTruoc.Name = "ppcXemTruoc";
            ppcXemTruoc.Size = new Size(720, 748);
            ppcXemTruoc.TabIndex = 1;
            // 
            // FrmInHoaDon
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnDong;
            ClientSize = new Size(720, 800);
            Controls.Add(ppcXemTruoc);
            Controls.Add(pnlTren);
            Font = new Font("Segoe UI", 10F);
            MinimizeBox = false;
            MinimumSize = new Size(560, 500);
            Name = "FrmInHoaDon";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "In hóa đơn";
            FormClosed += FrmInHoaDon_FormClosed;
            Load += FrmInHoaDon_Load;
            pnlTren.ResumeLayout(false);
            pnlTren.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTren;
        private Label lblKhoGiay;
        private ComboBox cboKhoGiay;
        private Label lblThongTin;
        private Button btnIn;
        private Button btnDong;
        private PrintPreviewControl ppcXemTruoc;
    }
}
