using ChildrenHospital.Controllers;
using ChildrenHospital.Models;
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

namespace ChildrenHosipital.Views
{
    public partial class AddDoctor : Form
    {
        DoctorController doctorController = new DoctorController();

        public AddDoctor()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng DoctorModel từ các trường nhập liệu
            DoctorModel newDoctor = new DoctorModel
            {
                FullName = tbFullName.Text.Trim(),
                DepartmentID = (int)cbDepartment.SelectedValue,
                PhoneNumber = tbPhoneNumber.Text.Trim(),
                Email = tbEmail.Text.Trim(),
                Image = picAvarta.Tag?.ToString() // Lấy đường dẫn ảnh từ Tag của PictureBox
            };

            // Kiểm tra tính hợp lệ của dữ liệu
            try
            {
                // Kiểm tra null hoặc thiếu thông tin
                if (newDoctor == null)
                {
                    throw new ArgumentNullException(nameof(newDoctor), "Đối tượng bác sĩ không được để null.");
                }



                // Gọi hàm Save để lưu bác sĩ vào cơ sở dữ liệu
                bool success = doctorController.Save(newDoctor);

                if (success)
                {
                    MessageBox.Show("Thêm bác sĩ thành công!");
                    // Xóa các trường nhập liệu để chuẩn bị thêm mới
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm bác sĩ. Vui lòng thử lại.");
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Lỗi dữ liệu: {ex.Message}");
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Lỗi xử lý: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}");
            }

        }
        private void ClearFields()
        {
            tbFullName.Text = string.Empty;
            tbPhoneNumber.Text = string.Empty;
            tbEmail.Text = string.Empty;
            cbDepartment.SelectedIndex = -1; // Reset giá trị của ComboBox
            picAvarta.Image = null; // Xóa hình ảnh trong PictureBox
            picAvarta.Tag = null;   // Xóa đường dẫn ảnh
        }


        private void btnSelectPictureFromDv_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Chọn hình ảnh bác sĩ";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    picAvarta.Image = Image.FromFile(selectedFilePath); // Hiển thị ảnh trong PictureBox
                    picAvarta.Tag = selectedFilePath; // Lưu đường dẫn vào Tag để sử dụng sau
                }
            }
        }

        private void AddDoctor_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'departmentDataSet.Departments' table. You can move, or remove it, as needed.
            this.departmentsTableAdapter.Fill(this.departmentDataSet.Departments);

        }
    }
}
