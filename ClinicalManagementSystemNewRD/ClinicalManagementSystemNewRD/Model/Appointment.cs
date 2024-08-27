using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Model
{
    public class Appointment
    {
        // Auto-generated fields
        public int AppointmentId { get; set; } // Ensure this is a property
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int TokenID { get; set; }  // Token ID
        public string Status { get; set; }
    }
}
