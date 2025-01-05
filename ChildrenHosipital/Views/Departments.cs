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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ChildrenHosipital.Views
{
    public partial class Departments : Form
    {
        private DepartmentModel currentDepartment;
        private DepartmentController departmentController;
        public Departments()
        {
            departmentController = new DepartmentController();

            InitializeComponent();
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'departmentDataSet.Departments' table. You can move, or remove it, as needed.
            this.departmentsTableAdapter.Fill(this.departmentDataSet.Departments);
            this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ Title Bar

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                DataGridViewRow row = guna2DataGridView1.CurrentRow;
                currentDepartment = new DepartmentModel
                {
                    DepartmentID = int.Parse(row.Cells[0].Value?.ToString() ?? "0"),
                    DepartmentName = row.Cells[1].Value?.ToString(),
                    Details = row.Cells[2].Value?.ToString(),
                    Image = row.Cells[1].Value?.ToString(),
                };
                tbDepartmentId.Text = currentDepartment.DepartmentID.ToString();
                tbDepartmentName.Text = currentDepartment.DepartmentName.ToString();
                tbDetail.Text = currentDepartment.Details.ToString();
                picAvata.ImageLocation = currentDepartment.Image;

            }
            else
            {
                tbDepartmentId.Text = string.Empty;
                tbDepartmentName.Text = string.Empty;
                tbDetail.Text = string.Empty;
                picAvata.ImageLocation = string.Empty;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var department = new DepartmentModel
                {
                    DepartmentName = txtDepartmentName.Text,
                    Details = txtDetails.Text,
                    Image = pictureDepartment.ImageLocation
                };

                if (departmentController.isValidate(department))
                {
                if (departmentController.Save(department))
                    {
                        MessageBox.Show("Thêm phòng ban thành công!");
                        this.departmentsTableAdapter.Fill(this.departmentDataSet.Departments);

                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm phòng ban.");
                    }
                }

                    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPicture_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var department = new DepartmentModel
                {
                    DepartmentID = int.TryParse(tbDepartmentId.Text, out var id) ? id : 0,
                    DepartmentName = tbDepartmentName.Text,
                    Details = tbDetail.Text,
                    Image = picAvata.ImageLocation
                };

                if (departmentController.isValidate(department))
                {
                    if (departmentController.Update(department))
                    {
                        MessageBox.Show("Cập nhật phòng ban thành công!");

                        ClearFields();

                        this.departmentsTableAdapter.Fill(this.departmentDataSet.Departments);

                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật phòng ban.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var department = new DepartmentModel
                {
                    DepartmentID = int.TryParse(tbDepartmentId.Text, out var id) ? id : 0
                };

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phòng ban này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (departmentController.Delete(department))
                    {
                        MessageBox.Show("Xóa phòng ban thành công!");
                        ClearFields();
                        this.departmentsTableAdapter.Fill(this.departmentDataSet.Departments);

                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa phòng ban.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPicture_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureDepartment.ImageLocation = openFileDialog.FileName;
                }
            }
        }

        private void ClearFields()
        {
            tbDepartmentId.Text = string.Empty;
            tbDepartmentName.Text = string.Empty;
            tbDetail.Text = string.Empty;
            picAvata.ImageLocation = string.Empty;
        }

    }
}
