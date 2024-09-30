using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAI2_CAU4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int GetselectedRow(string stk)
        {
            for (int i = 0; i < dgvKhachHang.Rows.Count; i++)
            {
                if (dgvKhachHang.Rows[i].Cells[0].Value.ToString() == stk)
                {
                    return i;
                }
            }
            return -1;
        }
        private void insertUpdate(int selectedRow)
        {
            dgvKhachHang.Rows[selectedRow].Cells[0].Value = selectedRow +1 ;
            dgvKhachHang.Rows[selectedRow].Cells[1].Value = txtSTK.Text ;
            dgvKhachHang.Rows[selectedRow].Cells[2].Value = txtTenKH.Text ;
            dgvKhachHang.Rows[selectedRow].Cells[3].Value = txtDiaChi.Text ;
            dgvKhachHang.Rows[selectedRow].Cells[4].Value = double.Parse(txtSoTien.Text) ;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSTK.Text == "" || txtTenKH.Text == "" || txtDiaChi.Text == "" || txtSoTien.Text == "")
                {
                    throw new Exception("Vui long nhap day du thong tin tai khoan..!!");
                }
                int selectedRow = GetselectedRow(txtSTK.Text);
                if (selectedRow == -1)
                {
                    selectedRow = dgvKhachHang.Rows.Add();
                    insertUpdate(selectedRow);
                    tongTien();
                    MessageBox.Show("Them moi thanh cong!", "thong bao", MessageBoxButtons.OK);
                }
                else
                {
                    insertUpdate(selectedRow);
                    tongTien();
                    MessageBox.Show("cap nhat thanh cong!", "thong bao", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetselectedRow(txtSTK.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Khong tim thay tai khoan can xoa!!");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Ban co muon xoa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        dgvKhachHang.Rows.RemoveAt(selectedRow);
                        tongTien();
                        MessageBox.Show("Xoa thanh cong!", "thong bao", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Loi!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int selectedRow ;

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                selectedRow = int.Parse(row.Cells[0].Value.ToString());
                txtSTK.Text = row.Cells[1].Value.ToString();
                txtTenKH.Text = row.Cells[2].Value.ToString();
                txtDiaChi.Text = row.Cells[3].Value.ToString();
                txtTongTien.Text = row.Cells[4].Value.ToString();
            }
        }

        private void tongTien()
        {
            double tong = 0;
            for (int i = 0; i < dgvKhachHang.Rows.Count; i++)
            {
                tong += double.Parse(dgvKhachHang.Rows[i].Cells[4].Value.ToString());
            }
            txtTongTien.Text = tong.ToString();
        }
    }
}
