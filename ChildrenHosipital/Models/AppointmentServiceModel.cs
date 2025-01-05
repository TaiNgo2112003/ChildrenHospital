using ChildrenHospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenHosipital.Models
{
    internal class AppointmentServiceModel
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Appointment")]
        public int AppointmentID { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Service")]
        public int ServiceID { get; set; }

        // Navigation properties
        public virtual AppointmentModel Appointment { get; set; }

        public virtual ServiceModel Service { get; set; }

    }
}
