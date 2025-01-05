using ChildrenHosipital.Models;
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
    public partial class Medicines : Form
    {
        private List<TemporaryMedicineModel> temporaryMedicines = new List<TemporaryMedicineModel>();

        private MedicineModel currentmedicines;

        private MedicineController medicineController = new MedicineController();

        public Medicines()
        {
            medicineController = new MedicineController();
            InitializeComponent();
        }

        private void Medicines_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'medicinesDataSet.Medicines' table. You can move, or remove it, as needed.
            this.medicinesTableAdapter.Fill(this.medicinesDataSet.Medicines);
            this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ Title Bar
            //Row Of List Medicines To Printf Bill
            dataGridViewList.Columns.Add("MedicineID", "Medicine ID");
            dataGridViewList.Columns.Add("MedicineName", "Medicine Name");
            dataGridViewList.Columns.Add("Quantity", "Quantity");
            dataGridViewList.Columns.Add("UnitPrice", "Unit Price");
            dataGridViewList.Columns.Add("TotalPrice", "Total Price");


        }

        private void guna2DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                try
                {
                    DataGridViewRow row = guna2DataGridView1.CurrentRow;
                    currentmedicines = new MedicineModel
                    {
                        MedicineID = row.Cells[0].Value != null
                                     ? Convert.ToInt32(row.Cells[0].Value)
                                     : 0,
                        MedicineName = row.Cells[1].Value?.ToString(),
                        Category = row.Cells[2].Value?.ToString(),
                        Manufacturer = row.Cells[3].Value?.ToString(),
                        ExpiryDate = row.Cells[4].Value != null
                                     ? Convert.ToDateTime(row.Cells[4].Value)
                                     : DateTime.MinValue,
                        Quantity = row.Cells[5].Value != null
                                   ? Convert.ToInt32(row.Cells[5].Value)
                                   : 0,
                        UnitPrice = row.Cells[6].Value != null
                                    ? Convert.ToDecimal(row.Cells[6].Value)
                                    : 0,
                        Description = row.Cells[7].Value?.ToString(),
                        AddedDate = row.Cells[8].Value != null
                                    ? Convert.ToDateTime(row.Cells[8].Value)
                                    : DateTime.MinValue,
                        IsActive = row.Cells[9].Value != null
                                   ? Convert.ToBoolean(row.Cells[9].Value)
                                   : false
                    };

                    // Binding to TextBoxes and other UI elements
                   // tbMedicineID.Text = currentMedicineModel.MedicineID.ToString();
                    tbMedicineName.Text = currentmedicines.MedicineName;
                    tbCategory.Text = currentmedicines.Category;
                    tbManufacturer.Text = currentmedicines.Manufacturer;
                    tbExpriryDate.Text = currentmedicines.ExpiryDate.ToString("yyyy-MM-dd");
                    tbQuantity.Text = currentmedicines.Quantity.ToString();
                    tbUnitPrice.Text = currentmedicines.UnitPrice.ToString("F2");
                    tbDescription.Text = currentmedicines.Description;
                    tbAddedDate.Text = currentmedicines.AddedDate.ToString("yyyy-MM-dd");
                    checkIsActivity.Checked = currentmedicines.IsActive;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error mapping data: {ex.Message}");
                }
            }
            else
            {
                // Clear fields when no row is selected
               // tbMedicineID.Text = string.Empty;
                tbMedicineName.Text = string.Empty;
                tbCategory.Text = string.Empty;
                tbManufacturer.Text = string.Empty;
                tbExpriryDate.Text = string.Empty;
                tbQuantity.Text = string.Empty;
                tbUnitPrice.Text = string.Empty;
                tbDescription.Text = string.Empty;
                tbAddedDate.Text = string.Empty;
                checkIsActivity .Checked = false;

                currentmedicines = null;
            }
        }

        private void tbAddToList_Click(object sender, EventArgs e)
        {
            if (currentmedicines != null)
            {
                try
                {
                    // Kiểm tra số lượng nhập vào
                    if (int.TryParse(tbQuantityMedicine.Text, out int quantityToAdd) && quantityToAdd > 0)
                    {
                        if (quantityToAdd > currentmedicines.Quantity)
                        {
                            MessageBox.Show("Số lượng yêu cầu vượt quá số lượng hiện có trong kho.");
                            return;
                        }
                       
                        // Tạo đối tượng tạm thời
                        var tempMedicine = new TemporaryMedicineModel
                        {
                            MedicineID = currentmedicines.MedicineID,
                            MedicineName = currentmedicines.MedicineName,
                            Quantity = quantityToAdd,
                            UnitPrice = currentmedicines.UnitPrice
                        };

                        // Thêm vào danh sách tạm
                        temporaryMedicines.Add(tempMedicine);

                        // Cập nhật số lượng thuốc trong kho
                        currentmedicines.Quantity -= quantityToAdd;
                        tbQuantity.Text = currentmedicines.Quantity.ToString();

                        // Cập nhật DataGridView
                        UpdateDataGridViewList();
                        if (medicineController.Update(currentmedicines))
                        {
                            MessageBox.Show("Cập nhật số lượng thành công!");
                            this.medicinesTableAdapter.Fill(this.medicinesDataSet.Medicines);

                        }
                        else
                        {
                            MessageBox.Show("Không thể cập nhật số lượng thuốc trong cơ sở dữ liệu.");
                        }
                        // Thông báo thành công
                        MessageBox.Show($"Thêm thuốc '{tempMedicine.MedicineName}' vào danh sách thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập số lượng hợp lệ.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm vào danh sách: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thuốc trước khi thêm.");
            }
        }
        private void UpdateDataGridViewList()
        {
            // Xóa sạch các hàng hiện tại
            dataGridViewList.Rows.Clear();

            // Thêm từng mục trong danh sách tạm
            foreach (var item in temporaryMedicines)
            {
                dataGridViewList.Rows.Add(
                    item.MedicineID,
                    item.MedicineName,
                    item.Quantity,
                    item.UnitPrice.ToString("F2"),
                    item.TotalPrice.ToString("F2")
                );
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
