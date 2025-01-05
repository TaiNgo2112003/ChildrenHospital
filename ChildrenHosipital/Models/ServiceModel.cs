using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenHospital.Models
{
    internal class ServiceModel
    {
        [Key]
        public int ServiceID { get; set; }

        [Required(ErrorMessage = "Service Name is required")]
        [StringLength(100, ErrorMessage = "Service Name must be less than 100 characters")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
    }
}
