using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Form1 : Form
    {
        string strCon = @"Data Source=DESKTOP-F166USU;Initial Catalog=Deso2;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
    
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnGhi.Enabled = false;
            btnXoa.Enabled = true;
            txtDiaChi.Enabled = false;
            txtGhiChu.Enabled = false;
            txtGioiTinh.Enabled = false;
            txtID.Enabled = false;
            txtQueQuan.Enabled = false;
            txtSinhVien.Enabled = false;
            cboTDHV.Enabled = false;
            dtpNgaySinh.Enabled = false;
            HienThiThongTin();
        }

        private bool CheckTextBox()
        {
            if (txtID.Text == "" || txtSinhVien.Text == "" || cboTDHV.SelectedIndex == -1 || txtGioiTinh.Text == "")
            {
                return false;
            }
            else return true;
        }

        private bool CheckChuanHoa()
        {
            if (txtGioiTinh.Text == "0" || txtGioiTinh.Text == "1")
            {
                return true;
            }
            else return false;
        }

        private void HienThiThongTin()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }

            string query = "select * from tblSinhVien";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();

            adapter.Fill(ds, "sinhvien");

            dgvDanhSach.DataSource = ds.Tables["sinhvien"];
        }

        int tt = 0;

        private void button1_Click(object sender, EventArgs e)
        {

            txtDiaChi.Enabled = true;
            txtGhiChu.Enabled = true;
            txtGioiTinh.Enabled = true;
            txtID.Enabled = true;
            txtQueQuan.Enabled = true;
            txtSinhVien.Enabled = true;
            cboTDHV.Enabled = true;
            dtpNgaySinh.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuyBo.Enabled = true;
            tt = 1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
        
            if (tt == 1)
            {
                if (CheckTextBox() == false)
                {
                    MessageBox.Show("ID,Họ Tên Sinh Viên,Giới Tính,Trình Độ Học Vấn Không Được Để Trống");
                }
                if (CheckChuanHoa() == false)
                {
                    MessageBox.Show("Giới tính chỉ được ghi 0(nam) hoặc 1(nữ)");
                }
                else
                {
                    DataRow row = ds.Tables["sinhvien"].NewRow();
                    row[0] = txtID.Text;
                    row[1] = txtSinhVien.Text;
                    row[2] = dtpNgaySinh.Text;
                    row[3] = txtGioiTinh.Text;
                    row[4] = cboTDHV.Text;
                    row[5] = txtQueQuan.Text;
                    row[6] = txtDiaChi.Text;
                    row[7] = txtGhiChu.Text;


                    ds.Tables["sinhvien"].Rows.Add(row);

                    int kq = adapter.Update(ds.Tables["sinhvien"]);
                    if (kq > 0)
                    {
                        MessageBox.Show("Thêm Dữ Liệu Thành Công");
                        btnGhi.Enabled = false;
                        btnXoa.Enabled = false;
                        txtDiaChi.Enabled = false;
                        txtGhiChu.Enabled = false;
                        txtGioiTinh.Enabled = false;
                        txtID.Enabled = false;
                        txtQueQuan.Enabled = false;
                        txtSinhVien.Enabled = false;
                        cboTDHV.Enabled = false;
                        dtpNgaySinh.Enabled = false;
                        btnThem.Enabled = true;
                        btnSua.Enabled = true;
                    }
                    else MessageBox.Show("Thêm Dữ Liệu Không Thành Công");
                }
            

                
            }
            if (tt == 2)
            {
                if (CheckTextBox() == false)
                {
                    MessageBox.Show("ID,Họ Tên Sinh Viên,Giới Tính,Trình Độ Học Vấn Không Được Để Trống");
                }
                else
                {
                    if (vt == -1)
                    {
                        MessageBox.Show("Chưa Chọn Dữ Liệu");
                        return;
                    }

                    DataRow row = ds.Tables["sinhvien"].Rows[vt];
                    row.BeginEdit();
                    row[0] = txtID.Text;
                    row[1] = txtSinhVien.Text;
                    row[2] = dtpNgaySinh.Text;
                    row[3] = txtGioiTinh.Text;
                    row[4] = cboTDHV.Text;
                    row[5] = txtQueQuan.Text;
                    row[6] = txtDiaChi.Text;
                    row[7] = txtGhiChu.Text;
                    row.EndEdit();

                    int kq = adapter.Update(ds.Tables["sinhvien"]);
                    if (kq > 0)
                    {
                        MessageBox.Show("Sửa Dữ Liệu Thành Công");
                    }
                    else MessageBox.Show("Sửa Dữ Liệu Không Thành Công");
                }
            }
            
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            btnGhi.Enabled = false;
            btnXoa.Enabled = true;
            txtDiaChi.Enabled = false;
            txtGhiChu.Enabled = false;
            txtGioiTinh.Enabled = false;
            txtID.Enabled = false;
            txtQueQuan.Enabled = false;
            txtSinhVien.Enabled = false;
            cboTDHV.Enabled = false;
            dtpNgaySinh.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
        }

        int vt = -1;
        private void btnSua_Click(object sender, EventArgs e)
        {
            txtDiaChi.Enabled = true;
            txtGhiChu.Enabled = true;
            txtGioiTinh.Enabled = true;
            txtID.Enabled = false;
            txtQueQuan.Enabled = true;
            txtSinhVien.Enabled = true;
            cboTDHV.Enabled = true;
            dtpNgaySinh.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuyBo.Enabled = true;
            tt = 2;

            txtID.Focus();



        }

    
        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            if (vt == -1) return;
            DataRow row = ds.Tables["sinhvien"].Rows[vt];
            txtID.Text = row[0].ToString();
            txtSinhVien.Text = row[1].ToString();
            dtpNgaySinh.Text = row[2].ToString();
            txtGioiTinh.Text = row[3].ToString();
            cboTDHV.Text = row[4].ToString();
            txtQueQuan.Text = row[5].ToString();
            txtDiaChi.Text = row[6].ToString();
            txtGhiChu.Text = row[7].ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (vt == -1)
            {
                MessageBox.Show("Chưa Chọn Dữ Liệu");
                return;
            }
            DataRow row = ds.Tables["sinhvien"].Rows[vt];
            row.Delete();

            int kq = adapter.Update(ds.Tables["sinhvien"]);
            if (kq > 0)
            {
                MessageBox.Show("Xoá Thành Công");
            }
            else MessageBox.Show("Xoá Không Thành Công");
        }
    }
}
