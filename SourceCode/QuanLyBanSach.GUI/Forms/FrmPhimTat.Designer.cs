// File: QuanLyBanSach.GUI/Forms/FrmPhimTat.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmPhimTat
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
            lblTieuDe = new Label();
            dgvPhimTat = new DataGridView();
            colPhim = new DataGridViewTextBoxColumn();
            colChucNang = new DataGridViewTextBoxColumn();
            colPhamVi = new DataGridViewTextBoxColumn();
            btnDong = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPhimTat).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTieuDe.Location = new Point(16, 14);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(165, 21);
            lblTieuDe.TabIndex = 2;
            lblTieuDe.Text = "Bảng phím tắt BookSPT";
            // 
            // dgvPhimTat
            // 
            dgvPhimTat.AllowUserToAddRows = false;
            dgvPhimTat.AllowUserToDeleteRows = false;
            dgvPhimTat.AllowUserToResizeRows = false;
            dgvPhimTat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPhimTat.AutoGenerateColumns = false;
            dgvPhimTat.BackgroundColor = SystemColors.Window;
            dgvPhimTat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhimTat.Columns.AddRange(new DataGridViewColumn[] { colPhim, colChucNang, colPhamVi });
            dgvPhimTat.Location = new Point(16, 46);
            dgvPhimTat.MultiSelect = false;
            dgvPhimTat.Name = "dgvPhimTat";
            dgvPhimTat.ReadOnly = true;
            dgvPhimTat.RowHeadersVisible = false;
            dgvPhimTat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhimTat.Size = new Size(712, 420);
            dgvPhimTat.TabIndex = 1;
            // 
            // colPhim
            // 
            colPhim.DataPropertyName = "Phim";
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            colPhim.DefaultCellStyle = dataGridViewCellStyle1;
            colPhim.HeaderText = "Phím";
            colPhim.Name = "colPhim";
            colPhim.ReadOnly = true;
            colPhim.Width = 120;
            // 
            // colChucNang
            // 
            colChucNang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colChucNang.DataPropertyName = "ChucNang";
            colChucNang.HeaderText = "Chức năng";
            colChucNang.Name = "colChucNang";
            colChucNang.ReadOnly = true;
            // 
            // colPhamVi
            // 
            colPhamVi.DataPropertyName = "PhamVi";
            colPhamVi.HeaderText = "Phạm vi";
            colPhamVi.Name = "colPhamVi";
            colPhamVi.ReadOnly = true;
            colPhamVi.Width = 150;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.Location = new Point(622, 478);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(106, 34);
            btnDong.TabIndex = 0;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // FrmPhimTat
            // 
            AcceptButton = btnDong;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnDong;
            ClientSize = new Size(744, 526);
            Controls.Add(btnDong);
            Controls.Add(dgvPhimTat);
            Controls.Add(lblTieuDe);
            Font = new Font("Segoe UI", 10F);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(560, 400);
            Name = "FrmPhimTat";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Phím tắt";
            Load += FrmPhimTat_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPhimTat).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private DataGridView dgvPhimTat;
        private DataGridViewTextBoxColumn colPhim;
        private DataGridViewTextBoxColumn colChucNang;
        private DataGridViewTextBoxColumn colPhamVi;
        private Button btnDong;
    }
}
