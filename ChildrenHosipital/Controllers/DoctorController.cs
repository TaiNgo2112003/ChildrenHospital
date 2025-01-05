using ChildrenHospital.Db;
using ChildrenHospital.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChildrenHospital.Controllers
{
    internal class DoctorController : IController<DoctorModel>
    {
        public bool Delete(DoctorModel entity)
        {
            try
            {
                using (var connection = DbConnection_.GetConnection())
                {
                    connection.Open();

                    string query = "DELETE FROM Doctors WHERE DoctorID = @DoctorID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorID", entity.DoctorID);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu xóa thành công
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }


        public bool isValidate(DoctorModel entity)
        {
            if (string.IsNullOrWhiteSpace(entity.FullName))
            {
                Console.WriteLine("FullName không được để trống.");
                return false;
            }

            if (entity.DepartmentID <= 0)
            {
                Console.WriteLine("DepartmentID không hợp lệ.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(entity.Email) || !Regex.IsMatch(entity.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.WriteLine("Email không hợp lệ.");
                return false;
            }

            if (entity.PhoneNumber.Length != 10 || !Regex.IsMatch(entity.PhoneNumber, @"^\d{10}$"))
            {
                Console.WriteLine("Số điện thoại không hợp lệ.");
                return false;
            }

            return true; // Dữ liệu hợp lệ
        }


        public bool Save(DoctorModel entity)
        {
            try
            {
                using (var connection = DbConnection_.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Doctors (FullName, DepartmentID, PhoneNumber, Email, Image)
                                     VALUES (@FullName, @DepartmentID, @PhoneNumber, @Email, @Image)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", entity.FullName);
                        command.Parameters.AddWithValue("@DepartmentID", entity.DepartmentID);
                        command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", entity.Email);
                        command.Parameters.AddWithValue("@Image", entity.Image);


                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu thêm thành công
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool Update(DoctorModel entity)
        {
            try
            {
                using (var connection = DbConnection_.GetConnection())
                {
                    connection.Open();

                    string query = @"
                UPDATE Doctors
                SET FullName = @FullName, 
                    DepartmentID = @DepartmentID, 
                    PhoneNumber = @PhoneNumber, 
                    Email = @Email, 
                    Image = @Image
                WHERE DoctorID = @DoctorID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", entity.FullName);
                        command.Parameters.AddWithValue("@DepartmentID", entity.DepartmentID);
                        command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", entity.Email);
                        command.Parameters.AddWithValue("@Image", entity.Image);
                        command.Parameters.AddWithValue("@DoctorID", entity.DoctorID);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu cập nhật thành công
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

    }
}
