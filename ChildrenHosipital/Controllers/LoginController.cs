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
    internal class LoginController : IController<LoginModel>
    {

        public bool checkLogin(string username, string password)
        {
            using (var connection = DbConnection_.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Role, Email, Fullname, PhoneNumber, Address FROM DoctorAccount WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Đăng nhập thành công
                                CurrentUser.Username = username;
                                CurrentUser.Role = reader["Role"].ToString();
                                CurrentUser.Email = reader["Email"].ToString();
                                CurrentUser.Fullname = reader["Fullname"].ToString();
                                CurrentUser.Password = password;
                                CurrentUser.Phone = reader["PhoneNumber"].ToString();
                                CurrentUser.Address = reader["Address"].ToString();


                                return true;
                            }
                            else
                            {
                                // Sai thông tin đăng nhập
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }
        public static class CurrentUser
        {
            public static int Id { get; set; }//
            public static string Username { get; set; }//
            public static string Fullname { get; set; }//
            public static string Password { get; set; }//
            public static string Phone { get; set; }//
            public static string Address { get; set; }//

            public static string Role { get; set; }//
            public static string Email { get; set; }//

        }
        public bool Update(LoginModel entity)
        {
            using (var connection = DbConnection_.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = @"
                UPDATE DoctorAccount
                SET 
                    Fullname = @Fullname,
                    Password = @Password,
                    PhoneNumber = @Phone,
                    Address = @Address,
                    Email = @Email
                WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Fullname", entity.FullName);
                        command.Parameters.AddWithValue("@Password", entity.Password);
                        command.Parameters.AddWithValue("@Phone", entity.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", entity.Address);
                        command.Parameters.AddWithValue("@Email", entity.Email);
                        command.Parameters.AddWithValue("@Username", entity.Username);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Returns true if update was successful
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating user: {ex.Message}");
                    return false;
                }
            }
        }

        public bool Save(LoginModel entity)
        {
            throw new NotImplementedException();
        }
        public bool Delete(LoginModel entity)
        {
            using (var connection = DbConnection_.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = @"DELETE FROM DoctorAccount WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter
                        command.Parameters.AddWithValue("@Username", entity.Username);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Returns true if delete was successful
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}");
                    return false;
                }
            }
        }


        public bool isValidate(LoginModel entity)
        {
            throw new NotImplementedException();
        }






    }
}
