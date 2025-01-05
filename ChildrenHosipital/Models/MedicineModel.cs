using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace ChildrenHospital.Models
{
    internal class MedicineModel
    {
        [Key]
        public int MedicineID { get; set; } // Unique identifier for the medicine

        [Required]
        [MaxLength(255)]
        public string MedicineName { get; set; } // Name of the medicine

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } // Category of the medicine (e.g., Antibiotic, Vitamin)

        [MaxLength(255)]
        public string Manufacturer { get; set; } // Name of the manufacturer

        [Required]
        public DateTime ExpiryDate { get; set; } // Expiration date of the medicine

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; } // Available quantity in stock

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Unit price must be a non-negative value.")]
        public decimal UnitPrice { get; set; } // Price per unit

        public string Description { get; set; } // Additional description of the medicine

        public DateTime AddedDate { get; set; } = DateTime.Now; // Date when the record was added

        public bool IsActive { get; set; } = true; // Indicates if the medicine is active
    }
}

