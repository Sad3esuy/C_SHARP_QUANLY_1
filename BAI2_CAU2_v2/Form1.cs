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
        public void SetGridViewStyle(DataGridView dgview)
        {
            // Loại bỏ viền của DataGridView để tạo cảm giác nhẹ nhàng hơn
            dgview.BorderStyle = BorderStyle.None;

            // Đặt màu nền khi chọn dòng là màu DarkTurquoise để nổi bật lựa chọn
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;

            // Sử dụng viền đơn giữa các ô, tạo cảm giác thanh thoát và đơn giản
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Đặt màu nền chính của DataGridView là màu trắng, tạo độ tương phản cao với các hàng dữ liệu
            dgview.BackgroundColor = Color.White;

            // Đặt chế độ chọn toàn bộ hàng khi người dùng nhấp vào một ô, cải thiện trải nghiệm sử dụng
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void GenerateAndBindData()
        {
            Random random = new Random();
            for (int i = 1; i <= 10; i++)
            {
                int id = i+10000000;
                string name = "Student " + i;
                string gender = random.Next(0, 2) == 0 ? "Nam" : "Nữ";
                double dtb = Math.Round(random.NextDouble() * 10, 1);
                string faculty = random.Next(0, 2) == 0 ? "CNTT" : "QTKD";
                dgvStudent.Rows.Add(id, name, gender, dtb, faculty);
                DemSV();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetGridViewStyle(dgvStudent);
            GenerateAndBindData();
            cmbKhoa.SelectedIndex = 0;
            cmbXepLoai.SelectedIndex = 0;
            ShowAllRows();
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
            int id;
            bool check1;
            check1 = int.TryParse(txtStudentID.Text, out id);
            if (!check1 || txtStudentID.Text.Length != 8)
            {
                MessageBox.Show("Mã sinh viên phải có 8 chữ số!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            dgvStudent.Rows[themHang].Cells[0].Value = id;
            dgvStudent.Rows[themHang].Cells[1].Value = txtStudentName.Text;
            dgvStudent.Rows[themHang].Cells[2].Value = optNu.Checked ? "Nữ" : "Nam";
            double dtb;
            bool check2;
            check2 = double.TryParse(txtDTB.Text, out dtb);
            if (!check2 || dtb <= 0 || dtb > 10)
            {

                MessageBox.Show("Điểm trung bình phải là số từ 1 đến 10!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            dgvStudent.Rows[themHang].Cells[3].Value = dtb;
            dgvStudent.Rows[themHang].Cells[4].Value = cmbKhoa.Text;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                errorProvider2.Clear();
                errorProvider3.Clear();
                // Kiểm tra các trường dữ liệu rỗng
                if (string.IsNullOrWhiteSpace(txtStudentID.Text))
                {
                    errorProvider1.SetError(txtStudentID, "Mã sinh viên không được để trống và có 8 chữ số!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtStudentName.Text))
                {
                    errorProvider2.SetError(txtStudentName, "Họ tên sinh viên không được để trống");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDTB.Text))
                {
                    errorProvider3.SetError(txtDTB, "Mã sinh viên không được để trống (1->10)");
                    return;
                }
                //if (txtStudentID.Text == "" || txtStudentName.Text == "" || txtDTB.Text == "")
                //{
                //    errorProvider1.SetError(txtStudentID, "Mã sinh viên không được để trống và có 10 chữ số!");
                //    errorProvider2.SetError(txtStudentName, "Họ tên sinh viên không được để trống");
                //    errorProvider3.SetError(txtDTB, "Mã sinh viên không được để trống (1->10)");
                //    //MessageBox.Show("Vui lòng điền đầy đủ thông tin!!", "Thông báo", MessageBoxButtons.OK);
                //    //return;
                //}
                // Kiểm tra các trường dữ liệu rỗng
                int id;
                bool check1;
                check1 = int.TryParse(txtStudentID.Text, out id);
                if (!check1 || txtStudentID.Text.Length != 8)
                {
                    //MessageBox.Show("Mã sinh viên phải có 10 chữ số!", "Thông báo", MessageBoxButtons.OK);
                    errorProvider1.SetError(txtStudentID, "Mã sinh viên không được để trống và có 8 chữ số!");
                    txtStudentID.Clear();
                    return;
                }
                double dtb;
                bool check2;
                check2 = double.TryParse(txtDTB.Text, out dtb);
                if (!check2 || dtb <= 0 || dtb > 10)
                {
                    //MessageBox.Show("Điểm trung bình phải là số từ 1 đến 10!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    errorProvider3.SetError(txtDTB, "Mã sinh viên không được để trống (1->10)");
                    txtDTB.Clear();
                    return; // Ngăn chặn thêm dữ liệu nếu điểm trung bình không hợp lệ
                }
                int laySV = timSV(txtStudentID.Text);
                if (laySV == -1)
                {
                    laySV = dgvStudent.Rows.Add();
                    them(laySV);
                    DemSV();
                    MessageBox.Show("Thêm thành công!!", "Thông báo", MessageBoxButtons.OK);
                    txtStudentID.Clear();
                    txtStudentName.Clear();
                    txtDTB.Clear();
                }
                else
                {
                    MessageBox.Show("Sinh viên đã tồn tại!", "Thông báo", MessageBoxButtons.OK);
                    txtStudentID.Clear();
                    txtStudentName.Clear();
                    txtDTB.Clear();
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
                errorProvider1.Clear();
                errorProvider2.Clear();
                errorProvider3.Clear();
                if (string.IsNullOrWhiteSpace(txtStudentID.Text))
                {
                    errorProvider1.SetError(txtStudentID, "Mã sinh viên không được để trống và có 8 chữ số!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtStudentName.Text))
                {
                    errorProvider2.SetError(txtStudentName, "Họ tên sinh viên không được để trống");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDTB.Text))
                {
                    errorProvider3.SetError(txtDTB, "Mã sinh viên không được để trống (1->10)");
                    return;
                }
                //if (txtStudentID.Text == "" || txtStudentName.Text == "" || txtDTB.Text == "")
                //{
                //    MessageBox.Show("Vui lòng điền đầy đủ thông tin!!", "Thông báo", MessageBoxButtons.OK);
                //    return;
                //}
                int id;
                bool check1;
                check1 = int.TryParse(txtStudentID.Text, out id);
                if (!check1 || txtStudentID.Text.Length != 8)
                {
                    //MessageBox.Show("Mã sinh viên phải có 10 chữ số!", "Thông báo", MessageBoxButtons.OK);
                    errorProvider1.SetError(txtStudentID, "Mã sinh viên không được để trống và có 8 chữ số!");
                    txtStudentID.Clear();
                    return;
                }

                double dtb;
                bool check2;
                check2 = double.TryParse(txtDTB.Text, out dtb);
                if (!check2 || dtb <= 0 || dtb > 10)
                {
                    //MessageBox.Show("Điểm trung bình phải là số từ 1 đến 10!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    errorProvider3.SetError(txtDTB, "Mã sinh viên không được để trống (1->10)");
                    txtDTB.Clear();
                    return; // Ngăn chặn thêm dữ liệu nếu điểm trung bình không hợp lệ
                }
                int laySV = timSV(txtStudentID.Text);
                if (laySV != -1)
                {
                    them(laySV);
                    DemSV();
                    MessageBox.Show("Cập nhật thành công!!", "Thông báo", MessageBoxButtons.OK);
                    txtStudentID.Clear();
                    txtStudentName.Clear();
                    txtDTB.Clear();
                }
                else
                {
                    MessageBox.Show("Sinh viên không tồn tại!", "Thông báo", MessageBoxButtons.OK);
                    txtStudentID.Clear();
                    txtStudentName.Clear();
                    txtDTB.Clear();
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
                        txtStudentID.Clear();
                        txtStudentName.Clear();
                        optNu.Checked = true;
                        txtDTB.Clear();
                        cmbKhoa.SelectedIndex = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Sinh viên không tồn tại!", "Thông báo", MessageBoxButtons.OK);
                    txtStudentID.Clear();
                    txtStudentName.Clear();
                    optNu.Checked = true;
                    txtDTB.Clear();
                    cmbKhoa.SelectedIndex = 0;
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

        private void txtStudentID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép các ký tự số và ký tự điều khiển (như Backspace)
            if (!char.IsDigit(e.KeyChar)&& !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ngăn chặn nhập ký tự không phải số
            }
        }

        private void txtStudentName_KeyPress(object sender, KeyPressEventArgs e)
        {
                // Cho phép chữ cái, dấu cách, dấu có dấu và ký tự điều khiển
         if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
         {
            e.Handled = true; // Ngăn chặn nhập các ký tự không hợp lệ
        }
        }

        private void UpdateDataGrid(List<DataGridViewRow> filteredStudents)
        {
            // Xóa tất cả hàng trong DataGridView
            dgvStudent.Rows.Clear();

            // Thêm lại các hàng đã lọc vào DataGridView
            foreach (var student in filteredStudents)
            {
                // Thêm vào DataGridView
                dgvStudent.Rows.Add(student.Cells[0].Value, student.Cells[1].Value, student.Cells[2].Value, student.Cells[3].Value, student.Cells[4].Value);
            }
        }
        private void cmbXepLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Check if there are rows in the DataGridView
                if (dgvStudent.Rows.Count == 0)
                {
                    MessageBox.Show("Khong ton tai sinh vien nao trong danh sach!!", "Thong bao!", MessageBoxButtons.OK);
                    return;
                }

                if (cmbXepLoai.SelectedIndex == -1)
                {
                    ShowAllRows();
                    return;
                }

                string selectedCategory = cmbXepLoai.SelectedItem.ToString();

                foreach (DataGridViewRow row in dgvStudent.Rows)
                {
                    if (double.TryParse(row.Cells[3].Value?.ToString(), out double dtb))  // Ensure the value is not null
                    {
                        // Show or hide rows based on the selected category and average score (dtb)
                        row.Visible = IsRowMatchingCategory(selectedCategory, dtb);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowAllRows()
        {
            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                row.Visible = true;  // Make all rows visible
            }
        }
        private bool IsRowMatchingCategory(string category, double dtb)
        {
            switch (category)
            {
                case "Xuất Sắc":
                    return dtb >= 9 && dtb <= 10;
                case "Giỏi":
                    return dtb >= 8 && dtb < 9;
                case "Khá":
                    return dtb >= 7 && dtb < 8;
                case "Trung Bình":
                    return dtb >= 5 && dtb < 7;
                case "Yếu":
                    return dtb >= 4 && dtb < 5;
                case "Kém":
                    return dtb < 4;
                default:
                    return false; // Default case: row is hidden if no matching category
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ShowAllRows();
            txtStudentID.Clear();
            txtStudentName.Clear();
            optNu.Checked = true;
            txtDTB.Clear();
            cmbKhoa.SelectedIndex = 0;
        }
    }
}

