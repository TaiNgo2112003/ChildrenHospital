using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenHosipital.Models
{
    internal class TemporaryMedicineModel
    {
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice; // Tính tổng tiền
    }
}
