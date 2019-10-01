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
    public partial class DatPhong : Form
    {
        KetNoi conn = new KetNoi();
        public DatPhong()
        {
            InitializeComponent();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DatPhong_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có thực sự muốn thoát!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Cancel)
                e.Cancel = true;
        }
        private string phatsinhMa()
        {
            string str = "SELECT COUNT(*) FROM dbo.KHACHHANG";
            if (conn.GetCount(str) < 1)
                return "KH1";
            else
            {
                conn.OpenConnection();
                SqlCommand cmd = new SqlCommand("SELECT Max(MaKhachHang) FROM KHACHHANG", conn.Conn);
                str = cmd.ExecuteScalar().ToString();
                conn.CloseConnection();
                str = str.Substring(2);
                str = "KH" + (Convert.ToInt32(str) + 1).ToString();
                return str;
            }
        }
        private void btnNhanPhong_Click(object sender, EventArgs e)
        {
            
            try
            {
                this.dtpNgayGio.CustomFormat = "MM/dd/yyyy";
                this.dtpNgayGio.Format = DateTimePickerFormat.Custom;
                string MaKH = phatsinhMa();
                string str = "INSERT INTO dbo.KHACHHANG VALUES('" + MaKH + "',N'" + txtHoTen.Text + "','" + txtSoCMND.Text + "','" + txtSDT.Text + "','" + dtpNgayGio.Text + "')";
                conn.Update(str);
                str = "UPDATE dbo.DanhSachPhong SET TinhTrang = 1 WHERE MaPhong = '" + cboPhong.SelectedValue.ToString() + "'";
                conn.Update(str);
                str = "INSERT INTO dbo.ThuePhong VALUES ('" + cboPhong.SelectedValue.ToString() + "','"+ MaKH +"','" + dtpNgayGio.Text + "','" + Convert.ToInt32(numSL.Value) + "')";
                conn.Update(str);
                MessageBox.Show("Đặt phòng thành công!!!");

            }
            catch
            {
                MessageBox.Show("Đặt phòng Thất Bại");
            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item.GetType() == typeof(TextBox))
                    item.ResetText();
            }
        }
        private void loadComboBox_LoaiPhong()
        {
            string str = "SELECT * FROM dbo.LoaiPhong";
            cboLoaiPhong.DataSource = conn.DataTable(str);
            cboLoaiPhong.DisplayMember = "TenLoai";
            cboLoaiPhong.ValueMember = "MaLoai";
        }
        private void loadComboBox_Phong()
        {
            string str = "SELECT * FROM dbo.DanhSachPhong WHERE TinhTrang = 0 AND MaLoai = '"+cboLoaiPhong.SelectedValue.ToString()+"'";
            cboPhong.DataSource = conn.DataTable(str);
            cboPhong.DisplayMember = "MaPhong";
            cboPhong.ValueMember = "MaPhong";
            
        }
        private void DatPhong_Load(object sender, EventArgs e)
        {
            loadComboBox_LoaiPhong();
            loadComboBox_Phong();
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            if (!txtHoTen.Text.Equals(string.Empty) && !txtSoCMND.Text.Equals(string.Empty))
                btnNhanPhong.Enabled = true;
        }

        private void txtSoCMND_TextChanged(object sender, EventArgs e)
        {
            if (!txtHoTen.Text.Equals(string.Empty) && !txtSDT.Text.Equals(string.Empty))
                btnNhanPhong.Enabled = true;
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            if (!txtSDT.Text.Equals(string.Empty) && !txtSoCMND.Text.Equals(string.Empty))
                btnNhanPhong.Enabled = true;
        }

        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadComboBox_Phong();
        }

        private void cboPhong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
