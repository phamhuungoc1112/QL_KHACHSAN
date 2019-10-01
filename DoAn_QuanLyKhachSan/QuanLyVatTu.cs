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
using System.Data.SqlClient;
namespace DoAn_QuanLyKhachSan
{
    public partial class QuanLyVatTu : Form
    {
        KetNoi conn = new KetNoi();
        public QuanLyVatTu()
        {
            InitializeComponent();
        }
        public bool ktTrungKhoaChinh(string Key1,string key2)
        {
            string strSQL = "SELECT count(*) FROM dbo.BoTri WHERE MaLoai ='" + Key1 + "' AND MaVatTu ='" + key2 + "'";
            if (conn.GetCount(strSQL) > 0)
                return true;
            return false;
        }
        public bool ktTrungKhoaChinhVT(string Key1)
        {
            string strSQL = "SELECT count(*) FROM dbo.VatTu WHERE MaVatTu ='" + Key1 + "'";
            if (conn.GetCount(strSQL) > 0)
                return true;
            return false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string key1 = cboLoaiPhong.SelectedValue.ToString();
            string key2 = cboVattu.SelectedValue.ToString();
            if (ktTrungKhoaChinh(key1, key2))
            {
                MessageBox.Show("Trùng khóa chính");

            }
            else
            {
                try
                {
                    String strSQL = "INSERT INTO dbo.BoTri VALUES('" + key2 + "','" + key1 + "','" + Convert.ToInt32(numSL.Value) + "')";
                    conn.Update(strSQL);
                    MessageBox.Show("Thêm thành công!!!");
                }
                catch
                {
                    MessageBox.Show("Thêm Thất Bại");
                }
            }
        }
        private void loadComboBox_LoaiPhong()
        {
            string str = "SELECT * FROM dbo.LoaiPhong";
            cboLoaiPhong.DataSource = conn.DataTable(str);
            cboLoaiPhong.DisplayMember = "TenLoai";
            cboLoaiPhong.ValueMember = "MaLoai";
        }
        private void loadComboBox_VatTu()
        {
            string str = "SELECT * FROM dbo.VatTu";
            cboVattu.DataSource = conn.DataTable(str);
            cboVattu.DisplayMember = "TenVatTu";
            cboVattu.ValueMember = "MaVatTu";
        }
        private void QuanLyVatTu_Load(object sender, EventArgs e)
        {
            loadComboBox_LoaiPhong();
            loadComboBox_VatTu();
            load_DRV1();
            load_lsvQLVatTu();
        }
        private void load_DRV1()
        {
            string str = "SELECT TenVatTu, SoLuong FROM dbo.BoTri, dbo.VatTu WHERE VatTu.MaVatTu = BoTri.MaVatTu AND MaLoai = '" + cboLoaiPhong.SelectedValue.ToString() +"'";
            drv1.DataSource = conn.DataTable(str);
            for (int i = 0; i < drv1.Rows.Count - 1; i++)
            {
                drv1.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                load_DRV1();
            }
            catch
            {
                MessageBox.Show("Có biến");
            }
        }

        private void btbXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "DELETE dbo.BoTri WHERE MaLoai = '" + cboLoaiPhong.SelectedValue.ToString() + "' AND MaVatTu = '" + cboVattu.SelectedValue.ToString() + "'";
                conn.Update(str);
                MessageBox.Show("Xóa thành công!!!");
                
            }
            catch
            {
                MessageBox.Show("Xóa thất bại!!!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string strSQL = "UPDATE dbo.BoTri SET SoLuong = '" + Convert.ToInt32(numSL.Value) + "' WHERE MaLoai = '" + cboLoaiPhong.SelectedValue.ToString() + "' AND MaVatTu = '" + cboVattu.SelectedValue.ToString() + "'";
                conn.Update(strSQL);
                MessageBox.Show("Cập nhật thành công!!!");
                
            }
            catch
            {
                MessageBox.Show("Cập nhật Thất Bại");
            }
        }
        private void load_lsvQLVatTu()
        {
            try
            {
                string str = "SELECT * FROM dbo.VatTu";
                int i = 0;
                DataTable tb = conn.DataTable(str);
                foreach (DataRow row in tb.Rows)
                {
                    lsvQLVattu.Items.Add(row["MaVatTu"].ToString());
                    lsvQLVattu.Items[i].SubItems.Add(row["TenVatTu"].ToString());
                    i++;
                }
            }
            catch
            {
                MessageBox.Show("Có biến");
            }
        }

        private void btnThemVT_Click(object sender, EventArgs e)
        {
            if (txtMaVatTu.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Không được để trống mục nào!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtMaVatTu.Focus();
                return;
            }
            string key1 = txtMaVatTu.Text;
            if (ktTrungKhoaChinhVT(key1))
            {
                MessageBox.Show("Trùng khóa chính");

            }
            else
            {
                try
                {
                    String strSQL = "INSERT into dbo.VatTu VALUES('" + key1 + "','" + txtTenVatTu.Text + "')";
                    conn.Update(strSQL);
                    MessageBox.Show("Thêm thành công!!!");
                    txtMaVatTu.Clear();
                    txtTenVatTu.Clear();
                }
                catch
                {
                    MessageBox.Show("Thêm Thất Bại");
                }
            }
        }

        private void btnXoaVT_Click(object sender, EventArgs e)
        {
            if (txtMaVatTu.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Không được để trống mục nào!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtMaVatTu.Focus();
                return;
            }
            string str = "SELECT COUNT(*) FROM dbo.BoTri WHERE MaVatTu = '" + txtMaVatTu.Text + "'";
            if (conn.GetCount(str) > 0)
            {
                MessageBox.Show("Vật tư đang được bố trí tại các phòng!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtMaVatTu.Focus();
                return;
            }
            try
            {
                str = "DELETE dbo.VatTu WHERE MaVatTu = '" + txtMaVatTu.Text + "'";
                conn.Update(str);
                MessageBox.Show("Xóa thành công!!!");
                txtMaVatTu.Clear();
                txtTenVatTu.Clear();
            }
            catch
            {
                MessageBox.Show("Xóa thất bại!!!");
            }
        }

        private void btnSuaVT_Click(object sender, EventArgs e)
        {
            if (txtMaVatTu.Text.Trim().Equals(string.Empty) || txtTenVatTu.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Không được để trống mục nào!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtMaVatTu.Focus();
                return;
            }
            try
            {
                string strSQL = "UPDATE dbo.VatTu SET TenVatTu = '" + txtTenVatTu.Text + "' WHERE MaVatTu = '" + txtMaVatTu.Text + "'";
                conn.Update(strSQL);
                MessageBox.Show("Cập nhật thành công!!!");
                txtMaVatTu.Clear();
                txtTenVatTu.Clear();
            }
            catch
            {
                MessageBox.Show("Cập nhật Thất Bại");
            }
        }

        private void lsvQLVattu_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in lsvQLVattu.SelectedItems)
            {
                txtMaVatTu.Text = items.SubItems[0].Text;
                txtTenVatTu.Text = items.SubItems[1].Text;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

    }
}
