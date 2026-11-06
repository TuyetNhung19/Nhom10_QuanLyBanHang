// File: QuanLyBanSach.GUI/Forms/FrmSachSapHet.Designer.cs
#nullable disable
namespace QuanLyBanSach.GUI.Forms
{
    partial class FrmSachSapHet
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
            dgvSapHet = new DataGridView();
            colMaSach = new DataGridViewTextBoxColumn();
            colTenSach = new DataGridViewTextBoxColumn();
            colLoai = new DataGridViewTextBoxColumn();
            colTon = new DataGridViewTextBoxColumn();
            colToiThieu = new DataGridViewTextBoxColumn();
            pnlDuoi = new Panel();
            lblSoLuong = new Label();
            btnIn = new Button();
            btnDong = new Button();
            pnlDuoi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSapHet).BeginInit();
            SuspendLayout();
            // 
            // dgvSapHet
            // 
            dgvSapHet.AllowUserToAddRows = false;
            dgvSapHet.AllowUserToDeleteRows = false;
            dgvSapHet.AllowUserToResizeRows = false;
            dgvSapHet.AutoGenerateColumns = false;
            dgvSapHet.BackgroundColor = SystemColors.Window;
            dgvSapHet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSapHet.Columns.AddRange(new DataGridViewColumn[] { colMaSach, colTenSach, colLoai, colTon, colToiThieu });
            dgvSapHet.Dock = DockStyle.Fill;
            dgvSapHet.Location = new Point(0, 0);
            dgvSapHet.MultiSelect = false;
            dgvSapHet.Name = "dgvSapHet";
            dgvSapHet.ReadOnly = true;
            dgvSapHet.RowHeadersVisible = false;
            dgvSapHet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSapHet.Size = new Size(760, 400);
            dgvSapHet.TabIndex = 0;
            // 
            // colMaSach
            // 
            colMaSach.DataPropertyName = "MaSach";
            colMaSach.HeaderText = "Mã sách";
            colMaSach.Name = "colMaSach";
            colMaSach.ReadOnly = true;
            colMaSach.Width = 80;
            // 
            // colTenSach
            // 
            colTenSach.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenSach.DataPropertyName = "TenSach";
            colTenSach.HeaderText = "Tên sách";
            colTenSach.Name = "colTenSach";
            colTenSach.ReadOnly = true;
            // 
            // colLoai
            // 
            colLoai.DataPropertyName = "TenLoai";
            colLoai.HeaderText = "Loại sách";
            colLoai.Name = "colLoai";
            colLoai.ReadOnly = true;
            colLoai.Width = 150;
            // 
            // colTon
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTon.DataPropertyName = "SoLuongTon";
            colTon.DefaultCellStyle = dataGridViewCellStyle1;
            colTon.HeaderText = "Tồn";
            colTon.Name = "colTon";
            colTon.ReadOnly = true;
            colTon.Width = 60;
            // 
            // colToiThieu
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            colToiThieu.DataPropertyName = "TonToiThieu";
            colToiThieu.DefaultCellStyle = dataGridViewCellStyle2;
            colToiThieu.HeaderText = "Tối thiểu";
            colToiThieu.Name = "colToiThieu";
            colToiThieu.ReadOnly = true;
            colToiThieu.Width = 80;
            // 
            // pnlDuoi
            // 
            pnlDuoi.Controls.Add(lblSoLuong);
            pnlDuoi.Controls.Add(btnIn);
            pnlDuoi.Controls.Add(btnDong);
            pnlDuoi.Dock = DockStyle.Bottom;
            pnlDuoi.Location = new Point(0, 400);
            pnlDuoi.Name = "pnlDuoi";
            pnlDuoi.Size = new Size(760, 50);
            pnlDuoi.TabIndex = 1;
            // 
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSoLuong.Location = new Point(12, 15);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(100, 19);
            lblSoLuong.Text = "0 sách";
            // 
            // btnIn
            // 
            btnIn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnIn.Location = new Point(500, 8);
            btnIn.Name = "btnIn";
            btnIn.Size = new Size(150, 34);
            btnIn.TabIndex = 0;
            btnIn.Text = "🖨 In danh sách";
            btnIn.UseVisualStyleBackColor = true;
            btnIn.Click += btnIn_Click;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.Location = new Point(656, 8);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(92, 34);
            btnDong.TabIndex = 1;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            btnDong.Click += btnDong_Click;
            // 
            // FrmSachSapHet
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnDong;
            ClientSize = new Size(760, 450);
            Controls.Add(dgvSapHet);
            Controls.Add(pnlDuoi);
            Font = new Font("Segoe UI", 10F);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(600, 360);
            Name = "FrmSachSapHet";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sách sắp hết (tồn ≤ tồn tối thiểu)";
            Load += FrmSachSapHet_Load;
            pnlDuoi.ResumeLayout(false);
            pnlDuoi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSapHet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvSapHet;
        private DataGridViewTextBoxColumn colMaSach;
        private DataGridViewTextBoxColumn colTenSach;
        private DataGridViewTextBoxColumn colLoai;
        private DataGridViewTextBoxColumn colTon;
        private DataGridViewTextBoxColumn colToiThieu;
        private Panel pnlDuoi;
        private Label lblSoLuong;
        private Button btnIn;
        private Button btnDong;
    }
}
