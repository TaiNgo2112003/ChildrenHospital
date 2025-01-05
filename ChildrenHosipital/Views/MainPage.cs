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
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent(); // Gọi đầu tiên để khởi tạo các thành phần giao diện

            // Gán giá trị của CurrentUser.Username vào TextBox (hoặc hiển thị trên giao diện)
            string currentUserName = CurrentUser.Username ?? "dx"; // Nếu Username null, gán mặc định là "dx"

            // Nếu cần thực hiện thêm hành động, bạn có thể tiếp tục ở đây
            LoginController loginController = new LoginController();
            if(CurrentUser.Username == null)
            {
                this.Close();
            }



        }

        public void loadForrm(object Form)
        {
            if (this.panelMain.Controls.Count > 0)
            {
                this.panelMain.Controls.RemoveAt(0);
                Form f = Form as Form;
                f.TopLevel = false;
                f.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(f);
                this.panelMain.Tag = f;
                f.Show();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            loadForrm(new Appointments());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            loadForrm(new Departments());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            loadForrm(new Medicines());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            loadForrm(new Patients());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            loadForrm(new Services());
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            loadForrm(new Doctors());

        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            loadForrm(new Departments());
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            loadForrm(new Medicines());
        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            loadForrm(new Services());
        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {
            loadForrm(new Patients());
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            loadForrm(new Appointments());
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            loadForrm(new ProfilePage());
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            loadForrm(new SupportForm());
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();  
            login.ShowDialog();
        }
    }
}
