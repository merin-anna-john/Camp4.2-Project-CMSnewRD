using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Model
{
    public class Availability
    {
        public int AvailabilityID { get; set; }
        public int DoctorID { get; set; }
        public string Specialty { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeSlot { get; set; }
    }

}
