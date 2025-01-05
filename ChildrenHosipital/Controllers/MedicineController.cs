using ChildrenHospital.Db;
using ChildrenHospital.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildrenHospital.Controllers
{
    internal class MedicineController : IController<MedicineModel>
    {
        public bool Delete(MedicineModel entity)
        {
            throw new NotImplementedException();
        }

        public bool isValidate(MedicineModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Save(MedicineModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(MedicineModel entity)
        {
            try
            {
                using (var connection = DbConnection_.GetConnection())
                {
                    connection.Open();

                    // Truy vấn cập nhật số lượng thuốc
                    string query = @"
                UPDATE Medicines
                SET Quantity = @Quantity
                WHERE MedicineID = @MedicineID;
            ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Gán tham số cho truy vấn
                        command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                        command.Parameters.AddWithValue("@MedicineID", entity.MedicineID);

                        // Thực thi truy vấn
                        int rowsAffected = command.ExecuteNonQuery();

                        // Kiểm tra nếu cập nhật thành công
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Thuốc không tồn tại hoặc không thể cập nhật.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thuốc: {ex.Message}");
                return false;
            }
        }


    }
}
