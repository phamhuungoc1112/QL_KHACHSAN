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
    public partial class QuanLyDichvu : Form
    {
        KetNoi conn = new KetNoi();
        public QuanLyDichvu()
        {
            InitializeComponent();
        }
        public bool ktTrungKhoaChinh(string Key1)
        {
            string strSQL = "SELECT count(*) FROM dbo.LoaiDichVu WHERE MaDV ='" + Key1 + "'";
            if (conn.GetCount(strSQL) > 0)
                return true;
            return false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string key1 = txtMaDV.Text;
            if (ktTrungKhoaChinh(key1))
            {
                MessageBox.Show("Trùng khóa chính");

            }
            else
            {
                try
                {
                    String strSQL = "INSERT into dbo.LoaiDichVu VALUES('" + key1 + "',N'" + txtTenDV.Text + "', '" + txtGia.Text + "',N'" + cboDVT.SelectedItem.ToString() + "')";
                    conn.Update(strSQL);
                    MessageBox.Show("Thêm thành công!!!");
                }
                catch
                {
                    //MessageBox.Show("Thêm Thất Bại");
                }
            }
        }
        private void load_lsvDichVu()
        {
            string str = "SELECT * FROM dbo.LoaiDichVu";
            int i = 0;
            DataTable tb = conn.DataTable(str);
            foreach (DataRow row in tb.Rows)
            {
                lsvDichVu.Items.Add(row["MaDV"].ToString());
                lsvDichVu.Items[i].SubItems.Add(row["TenDV"].ToString());
                lsvDichVu.Items[i].SubItems.Add(row["DonGia"].ToString());
                lsvDichVu.Items[i].SubItems.Add(row["DonViTinh"].ToString());
                i++;
            }
        }
        private void loadComboBox_DonViTinh()
        {
            string[] dv = { "Phần", "Chai", "Lon", "Đĩa", "Bịch", "Lượt" };
            foreach (string s in dv)
            {
                    cboDVT.Items.Add(s);
            }
            cboDVT.SelectedIndex = 0;
            //string str = "SELECT DonViTinh FROM dbo.LoaiDichVu";
            //cboDVT.DataSource = conn.DataTable(str);
            //cboDVT.DisplayMember = "DonViTinh";
            //cboDVT.ValueMember = "DonViTinh";
            //cboDVT.SelectedIndex = 0;
        }
        private void Dichvu_Load(object sender, EventArgs e)
        {
            load_lsvDichVu();
            loadComboBox_DonViTinh();
        }

        private void lsvDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in lsvDichVu.SelectedItems)
            {
                txtMaDV.Text = items.SubItems[0].Text;
                txtTenDV.Text = items.SubItems[1].Text;
                txtGia.Text = items.SubItems[2].Text;
                //cboDVT.SelectedItem = items.SubItems[3].Text;
                cboDVT.Text = items.SubItems[3].Text;
            }
        }

        private void btbXoa_Click(object sender, EventArgs e)
        {

            try
            {
                string str = "DELETE dbo.LoaiDichVu WHERE MaDV = '" + txtMaDV.Text + "'";
                conn.Update(str);
                MessageBox.Show("Xóa thành công!!!");

            }
            catch 
            {
                MessageBox.Show("Dịch vụ này đang được sử dụng");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string strSQL = "UPDATE dbo.LoaiDichVu SET TenDV = N'" + txtTenDV.Text + "', DonGia = '"+ Convert.ToInt32(txtGia.Text) +"', DonViTinh = N'"+cboDVT.Text+"' WHERE MaDV = '" + txtMaDV.Text + "'";
                conn.Update(strSQL);
                MessageBox.Show("Cập nhật thành công!!!");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Cập nhật Thất Bại");
            }
        }

        private void cboDVT_Click(object sender, EventArgs e)
        {
            //string [] dv = {"Phần", "Chai", "Lon", "Đĩa", "Bịch", "Lượt"};
            //foreach (string s in dv)
            //{
            //    cboDVT.Items.Add(s);
            //}
        }

    }
}
