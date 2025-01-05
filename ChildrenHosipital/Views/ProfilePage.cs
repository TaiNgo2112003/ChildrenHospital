using ChildrenHospital.Controllers;
using ChildrenHospital.Models;
using ChildrenHospital.Views;
using System;
using System.Windows.Forms;
using static ChildrenHospital.Controllers.LoginController;

namespace ChildrenHosipital.Views
{
    public partial class ProfilePage : Form
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoginController login = new LoginController();

            tbUsername.Text = CurrentUser.Username;
            tbFullname.Text = CurrentUser.Fullname;
            tbPassword.Text = CurrentUser.Password;
            tbEmail.Text = CurrentUser.Email;
            tbPhone.Text = CurrentUser.Phone;
            tbAdress.Text = CurrentUser.Address;
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(guna2CheckBox1.Checked)
            {
                tbPassword.PasswordChar = '\0'; // Remove the masking
                guna2CheckBox1.Text = "Show";  // Correct assignment for Text property

            }
            else
            {
                tbPassword.PasswordChar = '*';
                guna2CheckBox1.Text = "Hidde";

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Get updated data from input fields
            LoginModel updatedUser = new LoginModel
            {
                Username = tbUsername.Text, // Assuming you have a TextBox named tbUsername
                FullName = tbFullname.Text,
                Password = tbPassword.Text,
                PhoneNumber = tbPhone.Text,
                Address = tbAdress.Text,
                Email = tbEmail.Text,
            };

            LoginController loginController = new LoginController();
            if (loginController.Update(updatedUser))
            {
                MessageBox.Show("User information updated successfully!");
                // Optionally update CurrentUser to reflect changes
                CurrentUser.Fullname = updatedUser.FullName;
                CurrentUser.Password = updatedUser.Password;
                CurrentUser.Phone = updatedUser.PhoneNumber;
                CurrentUser.Address = updatedUser.Address;
                CurrentUser.Email = updatedUser.Email;
            }
            else
            {
                MessageBox.Show("Failed to update user information.");
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Confirm deletion
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this user?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                LoginController loginController = new LoginController();

                // Assuming you have a TextBox for username
                string usernameToDelete = tbUsername.Text;

                if (string.IsNullOrWhiteSpace(usernameToDelete))
                {
                    MessageBox.Show("Please provide a username to delete.");
                    return;
                }

                LoginModel userToDelete = new LoginModel
                {
                    Username = usernameToDelete
                };

                if (loginController.Delete(userToDelete))
                {
                   

                    MessageBox.Show("User deleted successfully!");
                    this.Close();
                    Login login = new Login();
                    login.ShowDialog();


                }
                else
                {
                    MessageBox.Show("Failed to delete the user.");
                }
            }
        }

    }
}
