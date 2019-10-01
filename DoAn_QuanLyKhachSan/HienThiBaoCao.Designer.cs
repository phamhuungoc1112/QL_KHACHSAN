namespace DoAn_QuanLyKhachSan
{
    partial class HienThiBaoCao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.rpHoaDon = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // rpHoaDon
            // 
            this.rpHoaDon.ActiveViewIndex = -1;
            this.rpHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rpHoaDon.Cursor = System.Windows.Forms.Cursors.Default;
            this.rpHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpHoaDon.Location = new System.Drawing.Point(0, 0);
            this.rpHoaDon.Name = "rpHoaDon";
            this.rpHoaDon.Size = new System.Drawing.Size(1269, 749);
            this.rpHoaDon.TabIndex = 0;
            // 
            // HienThiBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 749);
            this.Controls.Add(this.rpHoaDon);
            this.Name = "HienThiBaoCao";
            this.Text = "HienThiBaoCao";
            this.Load += new System.EventHandler(this.HienThiBaoCao_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer rpHoaDon;
    }
}