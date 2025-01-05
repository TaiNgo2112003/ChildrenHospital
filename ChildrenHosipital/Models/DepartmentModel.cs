using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenHospital.Models
{
    internal class DepartmentModel
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(100, ErrorMessage = "Department Name must be less than 100 characters")]
        public string DepartmentName { get; set; }

        [StringLength(500, ErrorMessage = "Details must be less than 500 characters")]
        public string Details { get; set; }

        [StringLength(50, ErrorMessage = "Image Path must be less than 50 characters")]
        public string Image { get; set; }
    }
}
