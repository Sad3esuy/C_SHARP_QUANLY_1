using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAI2_CAU2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbFaculty.SelectedIndex = 0;
        }
        private int GetselectedRow(string studentID)
        {
            for (int i = 0; i < dgvStudent.Rows.Count; i++)
            {
                if (dgvStudent.Rows[i].Cells[0].Value.ToString() == studentID)
                {
                    return i;
                }
            }
            return -1;
        }

        private void insertUpdate(int selectedRow)
        {
            dgvStudent.Rows[selectedRow].Cells[0].Value = txtStudentID.Text;
            dgvStudent.Rows[selectedRow].Cells[1].Value = txtFullName.Text;
            dgvStudent.Rows[selectedRow].Cells[2].Value = optFemale.Checked ? "Nu":"Nam";
            dgvStudent.Rows[selectedRow].Cells[3].Value = double.Parse(txtAverageScore.Text).ToString();
            dgvStudent.Rows[selectedRow].Cells[4].Value = cmbFaculty.Text;

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtStudentID.Text == "" || txtFullName.Text == "" || txtAverageScore.Text == "")
                {
                    throw new Exception("Vui long nhap day du thong tin sinh vien..!!");
                }
                int selectedRow = GetselectedRow(txtStudentID.Text);
                if(selectedRow == -1)
                {
                    selectedRow = dgvStudent.Rows.Add();
                    insertUpdate(selectedRow);
                    demSoLuong();
                    MessageBox.Show("Them moi thanh cong!","thong bao",MessageBoxButtons.OK);
                }
                else
                {
                    insertUpdate(selectedRow);
                    demSoLuong();
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
                int selectedRow = GetselectedRow(txtStudentID.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Khong tim thay MSSV can xoa!!");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Ban co muon xoa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        dgvStudent.Rows.RemoveAt(selectedRow);
                        demSoLuong();
                        MessageBox.Show("Xoa thanh cong!", "thong bao", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Loi!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void demSoLuong()
        {
            int demSvNam = 0;
            int demSvNu = 0;
            for (int i = 0; i < dgvStudent.Rows.Count; i++)
            {
                if (dgvStudent.Rows[i].Cells[2].Value.ToString() == "Nam")
                    demSvNam++;
                else
                    demSvNu++;
            }
            txtDemNam.Text = demSvNam.ToString();
            txtDemNu.Text = demSvNu.ToString();
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    txtStudentID.Text = row.Cells[0].Value.ToString();
                    txtFullName.Text = row.Cells[1].Value.ToString();
                    if (row.Cells[2].Value.ToString() == "Nam")
                    {
                        optMale.Checked = true;
                    }
                    else
                    {
                        optFemale.Checked = true;
                    }
                    txtAverageScore.Text = row.Cells[3].Value.ToString();
                    cmbFaculty.Text = row.Cells[4].Value.ToString();
                }
        }
    }
}
