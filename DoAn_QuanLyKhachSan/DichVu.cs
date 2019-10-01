using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThuVien;

namespace DoAn_QuanLyKhachSan
{
    public partial class DichVu : Form
    {
        KetNoi conn = new KetNoi();
        //private string _massage;
        public DichVu()
        {
            InitializeComponent();
        }
        //truyền dữ liệu
        /*public QuanLyDichVu(String Message)
            : this()
        {
            _massage = Message;
            lbTenPhong.Text = _massage;
        }*/

        private void QuanLyDichVu_Load(object sender, EventArgs e)
        {
            loadComboBox_DichVu();
            //lbMaDV.Visible = false;
        }
        //
        private void loadComboBox_DichVu()
        {
            string str = "SELECT * FROM dbo.LoaiDichVu";
            cboLoaiDichVu.DataSource = conn.DataTable(str);
            cboLoaiDichVu.DisplayMember = "TenDV";
            cboLoaiDichVu.ValueMember = "MaDV";
            cboLoaiDichVu.SelectedIndex = 0;
        }

        private void btnthemdv_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "INSERT INTO DichVu VALUES('"+txtTenPhong.Text+"', '" + cboLoaiDichVu.SelectedValue.ToString() + "', '" + Convert.ToInt32(numsoluongdv.Value).ToString() + "', '" + dtpngaydat.Text + "')";
                conn.Update(str);
                MessageBox.Show("Thành Công");
            }
            catch//(Exception ex)
            {
                try
                {
                    string str = "UPDATE DichVu  SET SoLuong = SoLuong + '" + Convert.ToInt32(numsoluongdv.Value).ToString() + "'";
                    conn.Update(str);
                    MessageBox.Show("Thành Công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //đổ dữ liệu từ combobox sang label
       



    }
}
