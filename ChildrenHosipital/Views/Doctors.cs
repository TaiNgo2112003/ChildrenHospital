using ChildrenHospital.Controllers;
using ChildrenHospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildrenHosipital.Views
{
    public partial class Doctors : Form
    {
        private DoctorModel currentDoctor;
        DoctorController doctorController = new DoctorController();

        public Doctors()
        {
            InitializeComponent();
        }

        private void Doctors_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'departmentDataSet.Departments' table. You can move, or remove it, as needed.
            this.departmentsTableAdapter.Fill(this.departmentDataSet.Departments);
            // TODO: This line of code loads data into the 'appointmentDataSet.Appointments' table. You can move, or remove it, as needed.
            this.appointmentsTableAdapter.Fill(this.appointmentDataSet.Appointments);
            // TODO: This line of code loads data into the 'hospitalDataSet.Doctors' table. You can move, or remove it, as needed.
            this.doctorsTableAdapter.Fill(this.hospitalDataSet.Doctors);
            this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ Title Bar

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có bác sĩ được chọn
            if (currentDoctor != null)
            {
                // Lấy dữ liệu từ các trường nhập liệu và ComboBox
                currentDoctor.FullName = tbFullName.Text;
                currentDoctor.PhoneNumber = tbPhoneNumber.Text;
                currentDoctor.Email = tbEmail.Text;
                currentDoctor.DepartmentID = (int)cbDepartment.SelectedValue;  // Lấy DepartmentID từ ComboBox
                currentDoctor.Image = picAvarta.ImageLocation;  // Nếu có cột Avatar
                // Kiểm tra tính hợp lệ của dữ liệu
                if (!doctorController.isValidate(currentDoctor))
                {
                    // Nếu dữ liệu không hợp lệ, hiển thị thông báo và không thực hiện cập nhật
                    MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.");
                    return;
                }

                // Gọi hàm Update để cập nhật bác sĩ vào cơ sở dữ liệu
                bool success = doctorController.Update(currentDoctor);

                if (success)
                {
                    MessageBox.Show("Cập nhật thông tin bác sĩ thành công!");
                    // Tải lại dữ liệu lên DataGridView nếu cần
                    this.doctorsTableAdapter.Fill(this.hospitalDataSet.Doctors);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin bác sĩ.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bác sĩ để chỉnh sửa.");
            }
        }


        private void guna2DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có dòng được chọn
            if (guna2DataGridView1.CurrentRow != null)
            {
                DataGridViewRow row = guna2DataGridView1.CurrentRow;

                // Lưu dữ liệu từ DataGridView vào model
                currentDoctor = new DoctorModel
                {
                    DoctorID = int.Parse(row.Cells[0].Value?.ToString() ?? "0"),
                    FullName = row.Cells[1].Value?.ToString(),
                    DepartmentID = Convert.ToInt32(row.Cells[2].Value ?? 0), // Convert sang int
                    PhoneNumber = row.Cells[3].Value?.ToString(),
                    Email = row.Cells[4].Value?.ToString(),
                    Image = row.Cells[5].Value?.ToString() // Nếu có cột Avatar
                };

                // Hiển thị dữ liệu từ model lên các field
                tbDoctorId.Text = currentDoctor.DoctorID.ToString();
                tbFullName.Text = currentDoctor.FullName;
               // cbDepartment.SelectedValue = currentDoctor.Department;
                tbPhoneNumber.Text = currentDoctor.PhoneNumber;
                tbEmail.Text = currentDoctor.Email;
                picAvarta.ImageLocation = currentDoctor.Image; // Hiển thị ảnh (nếu có)
            }
            else
            {
                // Nếu không có dòng nào được chọn, xóa nội dung các field
                tbDoctorId.Text = string.Empty;
                tbFullName.Text = string.Empty;
                cbDepartment.Text = string.Empty;
                tbPhoneNumber.Text = string.Empty;
                tbEmail.Text = string.Empty;
                picAvarta.ImageLocation = null;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có bác sĩ được chọn trong DataGridView
            if (currentDoctor != null)
            {
                // Hiển thị hộp thoại xác nhận
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa bác sĩ này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    // Gọi hàm Delete để xóa bác sĩ khỏi cơ sở dữ liệu
                    bool success = doctorController.Delete(currentDoctor);

                    if (success)
                    {
                        MessageBox.Show("Xóa bác sĩ thành công!");
                        // Tải lại dữ liệu lên DataGridView nếu cần
                        this.doctorsTableAdapter.Fill(this.hospitalDataSet.Doctors);
                        // Clear các trường thông tin
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa bác sĩ.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bác sĩ để xóa.");
            }
        }

        // Hàm xóa các trường thông tin sau khi xóa thành công
        private void ClearFields()
        {
            tbDoctorId.Text = string.Empty;
            tbFullName.Text = string.Empty;
            tbPhoneNumber.Text = string.Empty;
            tbEmail.Text = string.Empty;
            cbDepartment.SelectedIndex = -1; // Chọn lại giá trị mặc định cho ComboBox
            picAvarta.Image = null;  // Xóa hình ảnh nếu có
        }

        private void tbDoctorId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddDoctor add = new AddDoctor();    

            add.ShowDialog();
        }
    }
}
