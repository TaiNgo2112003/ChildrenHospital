using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ChildrenHosipital.Views
{
    public partial class SupportForm : Form
    {
        public SupportForm()
        {
            InitializeComponent();
        }

        private void sendEmailButton_Click(object sender, EventArgs e)
        {
            try
            {
                string recipientEmail = emailTB.Text;
                string emailContent = contentRTB.Text;

                // Kiểm tra xem email và nội dung có trống không
                if (string.IsNullOrEmpty(recipientEmail) || string.IsNullOrEmpty(emailContent))
                {
                    MessageBox.Show("Vui lòng nhập email và nội dung", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo email
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("taingo2112003@gmail.com");
                mail.To.Add(recipientEmail);
                mail.Subject = "Hỗ trợ báo lỗi";
                mail.Body = emailContent;

                // Cấu hình máy chủ SMTP
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("taingo2112003@gmail.com", "pqhu ford ugva mzrx");
                smtpServer.EnableSsl = true;
                bool success = IsValidEmail(emailTB.Text);
                if(success)
                {
                    // Gửi email
                    smtpServer.Send(mail);
                    MessageBox.Show("Email đã được gửi thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Email gửi thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi gửi email:\n{ex.Message}", "Lỗi gửi email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
