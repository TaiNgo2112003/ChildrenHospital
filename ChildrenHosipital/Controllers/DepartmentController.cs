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
    internal class DepartmentController : IController<DepartmentModel>
    {
        public bool Delete(DepartmentModel entity)
        {
            try
            {
                using (var connection = DbConnection_.GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM Departments WHERE DepartmentID = @DepartmentID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DepartmentID", entity.DepartmentID);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xóa phòng ban: {ex.Message}");
                return false;
            }
        }

        public bool isValidate(DepartmentModel entity)
        {
            if (string.IsNullOrWhiteSpace(entity.DepartmentName))
            {
                Console.WriteLine("Tên phòng ban không được để trống.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(entity.Details))
            {
                Console.WriteLine("Chi tiết không được để trống.");
                return false;
            }

            return true;
        }

        public bool Save(DepartmentModel entity)
        {
            try
            {
                using (var connection = DbConnection_.GetConnection())
                {
                    connection.Open();

                    string query = @"
                INSERT INTO Departments (DepartmentName, Details, Image)
                VALUES (@DepartmentName, @Details, @Image)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@DepartmentName", entity.DepartmentName);
                        command.Parameters.AddWithValue("@Details", entity.Details);
                        command.Parameters.AddWithValue("@Image", entity.Image ?? string.Empty);

                        int rowsAffected = command.ExecuteNonQuery();

                        // Kiểm tra số hàng bị ảnh hưởng
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("SQL executed but no rows were affected.");
                            return false;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log lỗi SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Log lỗi chung
                Console.WriteLine($"General Error: {ex.Message}");
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
            return false; // Trả về false khi có lỗi
        }


        public bool Update(DepartmentModel entity)
        {
            try
            {
                using (var connection = DbConnection_.GetConnection())
                {
                    connection.Open();
                    string query = @"
                UPDATE Departments 
                SET DepartmentName = @DepartmentName, Details = @Details, Image = @Image
                WHERE DepartmentID = @DepartmentID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DepartmentID", entity.DepartmentID);
                        command.Parameters.AddWithValue("@DepartmentName", entity.DepartmentName);
                        command.Parameters.AddWithValue("@Details", entity.Details);
                        command.Parameters.AddWithValue("@Image", entity.Image);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật phòng ban: {ex.Message}");
                return false;
            }
        }

    }
}
