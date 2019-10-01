using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ThuVien;
namespace DoAn_QuanLyKhachSan
{
    public partial class QuanLyPhong : Form
    {
        KetNoi conn = new KetNoi();
        public QuanLyPhong()
        {
            InitializeComponent();
        }
        //LOAIPHONG
        private void btnthemloaiphong_Click(object sender, EventArgs e)
        {
            if (btnthemloaiphong.ImageIndex == 1)
            {
                btnthemloaiphong.Text = "  Hủy";
                btnthemloaiphong.ImageIndex = 5;
                txtMaLoai.Enabled = true;
                txtGia.Enabled = true;
                numSoNguoi.Enabled = true;
                txtLoaiPhong.Visible = true;
                lbLoaiPhong.Visible = true;
                btnLuuLoaiPhong.Enabled = true;
            }
            else
            {
                btnthemloaiphong.Text = " Thêm";
                btnthemloaiphong.ImageIndex = 1;
                txtMaLoai.Enabled = false;
                txtGia.Enabled = false;
                numSoNguoi.Enabled = false;
                txtLoaiPhong.Visible = false;
                lbLoaiPhong.Visible = false;
                btnLuuLoaiPhong.Enabled = false;
            }
        }
        //PHONG
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.ImageIndex == 1)//khi nhấm thêm thì nút thêm trở thành "Hủy"
            {
                btnThem.Text = "  Hủy";
                btnThem.ImageIndex = 5;
                txtPhong.Enabled = true;
                cmbTenLoaiPhong.Enabled = true;
                txtPhong.Focus();
                btnLuuGia.Enabled = true;
            }
            else
            {
                btnThem.Text = " Thêm";
                btnThem.ImageIndex = 1;
                txtPhong.Text = "";
                txtPhong.Enabled = false;
                cmbTenLoaiPhong.Enabled = false;
                btnLuuGia.Enabled = false;
            }
        }

        private void Phong_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn thực sự muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //PHONG
        private void loadComboBox_LoaiPhong()
        {
            string str = "SELECT * FROM dbo.LoaiPhong";
            cmbTenLoaiPhong.DataSource = conn.DataTable(str);
            cmbTenLoaiPhong.DisplayMember = "TenLoai";
            cmbTenLoaiPhong.ValueMember = "MaLoai";
            cmbTenLoaiPhong.SelectedIndex = 0;
        }

        private void Phong_Load(object sender, EventArgs e)
        {
           
            loadComboBox_LoaiPhong();
            load_dgvphong();
         
            //LOAIPHONG
            load_ListView();
        }
        //PHONG
        private void load_dgvphong()
        {
            string str = "SELECT MaPhong, TenLoai, SoLuong, DonGia FROM DanhSachPhong, LoaiPhong WHERE LoaiPhong.MaLoai = DanhSachPhong.MaLoai ";
            dgvphong.DataSource = conn.DataTable(str);
        }
        
        //PHONG
        private void btnLuuGia_Click(object sender, EventArgs e)
        {
            try
            {
                string str1 = "INSERT INTO dbo.DanhSachPhong VALUES('" + txtPhong.Text + "', '"+ 0 +"', '" + cmbTenLoaiPhong.SelectedValue.ToString() + "')";
                conn.Update(str1);
                MessageBox.Show("Thêm Thành Công");
               
            }
            catch
            {
                MessageBox.Show("Thêm Thất bại");
            }
        }
        //PHONG
        private void btbXoa_Click(object sender, EventArgs e)
        {
            try
            {
                dgvphong.Rows.RemoveAt(dgvphong.CurrentRow.Index);
                Update();
                string upstr = "DELETE dbo.DanhSachPhong WHERE MaPhong = '" + txtPhong.Text.Trim() + "'";
                conn.Update(upstr);
                MessageBox.Show("Xóa Thành công");
            }
            catch //(Exception ex)
            {
                MessageBox.Show("Không xóa được");
            }
        }
        //PHONG
        private void dgvphong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txtPhong.Text = Convert.ToString(dgvphong.CurrentRow.Cells[0].Value);
                cmbTenLoaiPhong.Text = Convert.ToString(dgvphong.CurrentRow.Cells[1].Value);
                txtPhong.Enabled = true;
                cmbTenLoaiPhong.Enabled = true;
                btbXoa.Enabled = true;
            }
        }
       
        //LOAIPHONG
        private void btnLuuLoaiPhong_Click(object sender, EventArgs e)
        {
            //thêm dữ liệu vào trên lưới listview
            try
            {
                ListViewItem item = new ListViewItem();
                item.Text = txtMaLoai.Text;
                item.SubItems.Add(txtGia.Text);
                item.SubItems.Add(Convert.ToInt32(numSoNguoi.Value).ToString());
                lsvLoaiPhong.Items.Add(item);
            }
            catch
            {
                MessageBox.Show("hãy thử lại");
            }
            //lưu dữ liệu vào database
            try
            {
                string strlp = "INSERT INTO LoaiPhong(MaLoai, TenLoai, DonGia, SoLuong) VALUES('" + txtMaLoai.Text + "', N'" + txtLoaiPhong.Text + "', '" + txtGia.Text + "','" + Convert.ToInt32(numSoNguoi.Value).ToString() + "')";
                conn.Update(strlp);
                MessageBox.Show("Thêm Thành Công");
            }
            catch
            {
                MessageBox.Show("Phòng Đã Tồn Tại");
            }
        }
       
        //LOAIPHONG
        private void load_ListView()
        {
            string strlv = "SELECT MaLoai, DonGia, SoLuong FROM LoaiPhong";
            int i = 0;
            DataTable tb = conn.DataTable(strlv);
            foreach (DataRow row in tb.Rows)
            {
                lsvLoaiPhong.Items.Add(row["MaLoai"].ToString());
                lsvLoaiPhong.Items[i].SubItems.Add(row["DonGia"].ToString());
                lsvLoaiPhong.Items[i].SubItems.Add(row["SoLuong"].ToString());
                i++;
            }
        }
        //LOAIPHONG
        //load dữ liệu từ listview sang combobox
        private void lsvLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in lsvLoaiPhong.SelectedItems)
            {
                txtMaLoai.Text = items.SubItems[0].Text;
                txtGia.Text = items.SubItems[1].Text;
                numSoNguoi.Text = items.SubItems[2].Text;
                txtMaLoai.Enabled = true;
                txtGia.Enabled = true;
                numSoNguoi.Enabled = true;
                btnxoaloaiphong.Enabled = true;
            }
        }
        //LOAIPHONG
        private void btnxoaloaiphong_Click(object sender, EventArgs e)
        {
            try
            {
                Update();
                string str = "DELETE dbo.LoaiPhong WHERE MaLoai = '" + txtMaLoai.Text.Trim() + "'";
                conn.Update(str);
                lsvLoaiPhong.Items.RemoveAt(lsvLoaiPhong.SelectedItems[0].Index);
                MessageBox.Show("Xóa thành công!!!");

            }
            catch
            {
                MessageBox.Show("Xóa thất bại!!!");
            }
        }
        //gọi form con
        private void btnQLVT_Click(object sender, EventArgs e)
        {
            VatTuTrongPhong vt = new VatTuTrongPhong(txtPhong.Text, cmbTenLoaiPhong.Text, cmbTenLoaiPhong.SelectedValue.ToString());
            vt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DichVu dv = new DichVu();
            dv.Show();
        }
        //Lay don gia cua loai phong
        private string lay_DonGia()
        {
            string str = "SELECT DonGia FROm dbo.LoaiPhong WHERE MaLoai = '" + cmbTenLoaiPhong.SelectedValue.ToString() + "'";
            conn.OpenConnection();
            SqlCommand cmd = new SqlCommand(str, conn.Conn);
            str = cmd.ExecuteScalar().ToString();
            conn.CloseConnection();
            return str;
        }
        //Lay so luong nguoi cua loai phong
        private string lay_SoLuong()
        {
            string str = "SELECT SoLuong FROm dbo.LoaiPhong WHERE MaLoai = '" + cmbTenLoaiPhong.SelectedValue.ToString() + "'";
            conn.OpenConnection();
            SqlCommand cmd = new SqlCommand(str, conn.Conn);
            str = cmd.ExecuteScalar().ToString();
            conn.CloseConnection();
            return str;
        }

        private void txtLoaiPhong_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
