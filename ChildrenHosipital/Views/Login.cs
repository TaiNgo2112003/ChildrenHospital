using ChildrenHosipital.Views;
using ChildrenHospital.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChildrenHospital.Controllers.LoginController;

namespace ChildrenHospital.Views
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
           
        }


        private void LRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void lForgotP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void LRegister_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                tbPassword.PasswordChar = '\0'; // Remove the masking
            }
            else
            {
                tbPassword.PasswordChar = '*'; // Mask the password with '*'

            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = tbUsername.Text.Trim();
            string password = tbPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Xin hãy nhập UserName", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Xin hãy nhập Password!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoginController loginController = new LoginController();
            bool isLoginSuccessful = loginController.checkLogin(username, password);

            if (isLoginSuccessful)
            {
                MessageBox.Show($"Đăng nhập thành công! Chào mừng, {CurrentUser.Username}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Điều hướng đến giao diện chính
                MainPage mainForm = new MainPage();
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
