using ChildrenHospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildrenHosipital.Views
{
    public partial class Appointments : Form
    {
        AppointmentModel currentAppointmentModel;
        public Appointments()
        {
            InitializeComponent();
        }

        private void Appointments_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appointmentDataSet.Appointments' table. You can move, or remove it, as needed.
            this.appointmentsTableAdapter.Fill(this.appointmentDataSet.Appointments);
            this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ Title Bar

        }

        private void guna2DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow != null)
            {
                try
                {
                    DataGridViewRow row = guna2DataGridView1.CurrentRow;
                    currentAppointmentModel = new AppointmentModel
                    {
                        AppointmentID = row.Cells[0].Value != null
                                        ? Convert.ToInt32(row.Cells[0].Value)
                                        : 0,
                        UserID = row.Cells[1].Value != null
                                 ? Convert.ToInt32(row.Cells[1].Value)
                                 : (int?)null,
                        DepartmentID = row.Cells[2].Value != null
                                       ? Convert.ToInt32(row.Cells[2].Value)
                                       : (int?)null,
                        DoctorID = row.Cells[3].Value != null
                                   ? Convert.ToInt32(row.Cells[3].Value)
                                   : (int?)null,
                        FullName = row.Cells[4].Value?.ToString(),
                        PhoneNumber = row.Cells[5].Value?.ToString(),
                        DayAppointment = row.Cells[6].Value != null
                                         ? Convert.ToDateTime(row.Cells[6].Value)
                                         : DateTime.MinValue,
                        Time = row.Cells[7].Value != null
                               ? TimeSpan.Parse(row.Cells[7].Value.ToString())
                               : TimeSpan.Zero,
                        Message = row.Cells[8].Value?.ToString()
                    };

                    // Hiển thị dữ liệu vào các textbox (nếu cần)
                    tbAppointmentID.Text = currentAppointmentModel.AppointmentID.ToString();
                    tbUserID.Text = currentAppointmentModel.UserID.ToString();
                    tbAppointmentID.Text = currentAppointmentModel.AppointmentID.ToString();
                    tbDoctorID.Text = currentAppointmentModel.DoctorID.ToString();
                    tbFullName.Text = currentAppointmentModel.FullName;
                    tbPhoneNumber.Text = currentAppointmentModel.PhoneNumber;
                    tbDayAppointment.Text = currentAppointmentModel.DayAppointment.ToString("yyyy-MM-dd");
                    tbTime.Text = currentAppointmentModel.Time.ToString();
                    tbMess.Text = currentAppointmentModel.Message;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi ánh xạ dữ liệu: {ex.Message}");
                }
            }
            else
            {
                // Xóa sạch các giá trị khi không có dòng nào được chọn
                tbFullName.Text = string.Empty;
                tbPhoneNumber.Text = string.Empty;
                tbDayAppointment.Text = string.Empty;
                tbTime.Text = string.Empty;
                tbMess.Text = string.Empty;
                currentAppointmentModel = null;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (currentAppointmentModel == null)
            {
                MessageBox.Show("Vui lòng chọn một cuộc hẹn để in.");
                return;
            }

            try
            {
                // Chọn nơi lưu file PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    Title = "Lưu file PDF"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Tạo tài liệu PDF
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        var document = new iTextSharp.text.Document();
                        iTextSharp.text.pdf.PdfWriter.GetInstance(document, stream);

                        document.Open();

                        // Thêm tiêu đề
                        var titleFont = iTextSharp.text.FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);
                        var title = new iTextSharp.text.Paragraph("Thông Tin Cuộc Hẹn", titleFont)
                        {
                            Alignment = iTextSharp.text.Element.ALIGN_CENTER
                        };
                        document.Add(title);
                        document.Add(new iTextSharp.text.Paragraph("\n"));

                        // Thêm nội dung
                        var contentFont = iTextSharp.text.FontFactory.GetFont("Arial", 12);
                        document.Add(new iTextSharp.text.Paragraph($"Mã Cuộc Hẹn: {currentAppointmentModel.AppointmentID}", contentFont));
                        document.Add(new iTextSharp.text.Paragraph($"Mã Người Dùng: {currentAppointmentModel.UserID}", contentFont));
                        document.Add(new iTextSharp.text.Paragraph($"Mã Phòng Ban: {currentAppointmentModel.DepartmentID}", contentFont));
                        document.Add(new iTextSharp.text.Paragraph($"Mã Bác Sĩ: {currentAppointmentModel.DoctorID}", contentFont));
                        document.Add(new iTextSharp.text.Paragraph($"Họ Và Tên: {currentAppointmentModel.FullName}", contentFont));
                        document.Add(new iTextSharp.text.Paragraph($"Số Điện Thoại: {currentAppointmentModel.PhoneNumber}", contentFont));
                        document.Add(new iTextSharp.text.Paragraph($"Ngày Hẹn: {currentAppointmentModel.DayAppointment:yyyy-MM-dd}", contentFont));
                        document.Add(new iTextSharp.text.Paragraph($"Thời Gian: {currentAppointmentModel.Time}", contentFont));
                        document.Add(new iTextSharp.text.Paragraph($"Tin Nhắn: {currentAppointmentModel.Message}", contentFont));

                        document.Close();
                    }

                    MessageBox.Show("In thành công! File đã được lưu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in file PDF: {ex.Message}");
            }
        }

    }
}
