using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ThuVien;
namespace DoAn_QuanLyKhachSan
{
    public partial class HienThiBaoCao : Form
    {
        
        public HienThiBaoCao()
        {
            InitializeComponent();
        }

        private void HienThiBaoCao_Load(object sender, EventArgs e)
        {
            KetNoi conn = new KetNoi();
            DataTable dt = new DataTable();
            dt = conn.DataTable("SELECT * FROM dbo.HoaDon");
            rpHD hd = new rpHD();
            hd.SetDataSource(dt);
            rpHoaDon.ReportSource = hd;
            rpHoaDon.DisplayToolbar = true;
           
        }
    }
}
