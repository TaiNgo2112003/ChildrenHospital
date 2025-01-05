using ChildrenHospital.Db;
using ChildrenHospital.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenHospital.Controllers
{
    internal class RegisterController : IController<LoginModel>
    {
        public bool isValidate(LoginModel entity)
        {
            if (string.IsNullOrEmpty(entity.Username) ||
                          string.IsNullOrEmpty(entity.Password) ||
                          string.IsNullOrEmpty(entity.Email) ||
                          string.IsNullOrEmpty(entity.FullName))
            {
                return false; // Dữ liệu không hợp lệ
            }

            // Có thể thêm kiểm tra khác (vd: định dạng email)
            if (!entity.Email.Contains("@"))
            {
                return false; // Email không hợp lệ
            }

            return true; // Dữ liệu hợp lệ
        }

        public bool Save(LoginModel entity)
        {
            try
            {
                using (var connection = DbConnection_.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO DoctorAccount (Username, Password, Email, FullName, PhoneNumber, Address, Role)
                        VALUES (@Username, @Password, @Email, @FullName, @PhoneNumber, @Address, @Role)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", entity.Username);
                        command.Parameters.AddWithValue("@Password", entity.Password);
                        command.Parameters.AddWithValue("@Email", entity.Email);
                        command.Parameters.AddWithValue("@FullName", entity.FullName);
                        command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", entity.Address);
                        command.Parameters.AddWithValue("@Role", entity.Role);

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
   

        public bool Delete(LoginModel entity)
        {
            throw new NotImplementedException();
        }
        public bool Update(LoginModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
