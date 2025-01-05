using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenHospital.Models
{
    internal class AppointmentModel
    {
        [Key]
        public int AppointmentID { get; set; }

        public int? UserID { get; set; }

        public int? DepartmentID { get; set; }

        public int? DoctorID { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name must be less than 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(20, ErrorMessage = "Phone Number must be less than 20 characters")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Appointment Day is required")]
        [DataType(DataType.Date)]
        public DateTime DayAppointment { get; set; }

        [Required(ErrorMessage = "Time is required")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [StringLength(500, ErrorMessage = "Message must be less than 500 characters")]
        public string Message { get; set; }

        // Navigation properties (Foreign Key relationships)
        [ForeignKey("UserID")]
        public virtual LoginModel User { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual DepartmentModel Department { get; set; }

        [ForeignKey("DoctorID")]
        public virtual DoctorModel Doctor { get; set; }

    }
}
