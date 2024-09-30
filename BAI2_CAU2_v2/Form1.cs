using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAI2_CAU2_v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbKhoa.SelectedIndex = 0;
        }

        private int timSV(string txtStudentID)
        {
            for (int i = 0; i < dgvStudent.Rows.Count; i++)
            {
                if (dgvStudent.Rows[i].Cells[0].Value != null && dgvStudent.Rows[i].Cells[0].Value.ToString() == txtStudentID)
                {
                    return i;
                }
            }
            return -1;
        }

        private void them(int themHang)
        {
            dgvStudent.Rows[themHang].Cells[0].Value = txtStudentID.Text;
            dgvStudent.Rows[themHang].Cells[1].Value = txtStudentName.Text;
            dgvStudent.Rows[themHang].Cells[2].Value = optNu.Checked ? "Nữ" : "Nam";
            double dtb;
            if (double.TryParse(txtDTB.Text, out dtb))
            {
                dgvStudent.Rows[themHang].Cells[3].Value = dtb;
            }
            else
            {
                MessageBox.Show("Điểm trung bình phải là số!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            dgvStudent.Rows[themHang].Cells[4].Value = cmbKhoa.Text;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStudentID.Text == "" || txtStudentName.Text == "" || txtDTB.Text == "")
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                double dtb;
                if (!double.TryParse(txtDTB.Text, out dtb))
                {
                    MessageBox.Show("Điểm trung bình phải là số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Ngăn chặn thêm dữ liệu nếu điểm trung bình không hợp lệ
                }
                int laySV = timSV(txtStudentID.Text);
                if (laySV == -1)
                {
                    laySV = dgvStudent.Rows.Add();
                    them(laySV);
                    DemSV();
                    MessageBox.Show("Thêm thành công!!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Sinh viên đã tồn tại!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm thất bại!!\n" + ex.Message, "Thông báo", MessageBoxButtons.OKCancel);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStudentID.Text == "" || txtStudentName.Text == "" || txtDTB.Text == "")
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                double dtb;
                if (!double.TryParse(txtDTB.Text, out dtb))
                {
                    MessageBox.Show("Điểm trung bình phải là số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Ngăn chặn thêm dữ liệu nếu điểm trung bình không hợp lệ
                }
                int laySV = timSV(txtStudentID.Text);
                if (laySV != -1)
                {
                    them(laySV);
                    DemSV();
                    MessageBox.Show("Cập nhật thành công!!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Sinh viên không tồn tại!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại!!\n" + ex.Message, "Thông báo", MessageBoxButtons.OKCancel);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int laySV = timSV(txtStudentID.Text);
                if (laySV != -1) // Kiểm tra sinh viên tồn tại
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa ???", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        dgvStudent.Rows.RemoveAt(laySV);
                        DemSV();
                        MessageBox.Show("Xóa thành công!!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Sinh viên không tồn tại!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];

                // Kiểm tra xem từng ô có giá trị không trước khi truy cập và chuyển đổi thành chuỗi
                txtStudentID.Text = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
                txtStudentName.Text = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : string.Empty;

                if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() == "Nam")
                {
                    optNam.Checked = true;
                }
                else
                {
                    optNu.Checked = true;
                }

                txtDTB.Text = row.Cells[3].Value != null ? row.Cells[3].Value.ToString() : string.Empty;
                cmbKhoa.Text = row.Cells[4].Value != null ? row.Cells[4].Value.ToString() : string.Empty;
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận với nút Yes/No
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kiểm tra xem người dùng có chọn Yes không
            if (dialogResult == DialogResult.Yes)
            {
                // Thoát ứng dụng
                Application.Exit();
            }
        }

        private void DemSV()
        {
            int demNam = 0;
            int demNu = 0;
            for (int i = 0; i < dgvStudent.Rows.Count; i++)
            {
                if (dgvStudent.Rows[i].Cells[2].Value.ToString() == "Nam")
                    demNam++;
                else
                    demNu++;
            }
            txtTongNam.Text = demNam.ToString();
            txtTongNu.Text = demNu.ToString();
        }
    }
}

