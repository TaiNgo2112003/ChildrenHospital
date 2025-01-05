using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenHospital.Models
{
    internal class DoctorModel
    {
        [Key]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name must be less than 100 characters")]
        public string FullName { get; set; }

        public int? DepartmentID { get; set; }

        [StringLength(20, ErrorMessage = "Phone Number must be less than 20 characters")]
        public string PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "Email must be less than 100 characters")]
        public string Email { get; set; }

        public string Image { get; set; }

        // Navigation property (Foreign Key relationship)
        [ForeignKey("DepartmentID")]
        public virtual DepartmentModel Department { get; set; }
    }
}
