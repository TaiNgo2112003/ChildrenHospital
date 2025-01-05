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

namespace ChildrenHospital.Views
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng LoginModel từ dữ liệu nhập vào
            LoginModel account = new LoginModel
            {
                Username = tbUsername.Text.Trim(),
                Password = tbPassword.Text.Trim(),
                Email = tbEmail.Text.Trim(),
                FullName = tbFullname.Text.Trim(),
                PhoneNumber = tbPhoneNumber.Text.Trim(),
                Address = tbAddress.Text.Trim(),
                Role = "User"
            };

            RegisterController registerController = new RegisterController();

            // Kiểm tra tính hợp lệ
            if (!registerController.isValidate(account))
            {
                MessageBox.Show("Thông tin không hợp lệ. Vui lòng kiểm tra lại.");
                return;
            }

            // Thực hiện lưu vào cơ sở dữ liệu
            bool isSuccess = registerController.Save(account);

            if (isSuccess)
            {
                MessageBox.Show("Đăng ký thành công!");
                // Có thể reset form hoặc chuyển sang màn hình đăng nhập
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Vui lòng thử lại sau.");
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();    
            Login login = new Login();
            login.ShowDialog();
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            // Tạo đối tượng LoginModel từ dữ liệu nhập vào
            LoginModel account = new LoginModel
            {
                Username = tbUsername.Text.Trim(),
                Password = tbPassword.Text.Trim(),
                Email = tbEmail.Text.Trim(),
                FullName = tbFullname.Text.Trim(),
                PhoneNumber = tbPhoneNumber.Text.Trim(),
                Address = tbAddress.Text.Trim(),
                Role = "User"
            };

            RegisterController registerController = new RegisterController();

            // Kiểm tra tính hợp lệ
            if (!registerController.isValidate(account))
            {
                MessageBox.Show("Thông tin không hợp lệ. Vui lòng kiểm tra lại.");
                return;
            }

            // Thực hiện lưu vào cơ sở dữ liệu
            bool isSuccess = registerController.Save(account);

            if (isSuccess)
            {
                MessageBox.Show("Đăng ký thành công!");
                // Có thể reset form hoặc chuyển sang màn hình đăng nhập
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Vui lòng thử lại sau.");
            }
        }
    }
}
