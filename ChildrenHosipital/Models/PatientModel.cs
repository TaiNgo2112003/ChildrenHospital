using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenHospital.Models
{
    internal class PatientModel
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username must be less than 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password must be less than 255 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name must be less than 100 characters")]
        public string FullName { get; set; }

        [StringLength(100, ErrorMessage = "Email must be less than 100 characters")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Phone Number must be less than 20 characters")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(20, ErrorMessage = "Role must be less than 20 characters")]
        public string Role { get; set; }
    }
}
