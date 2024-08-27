using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Model
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public int StaffID { get; set; }
        public decimal ConsultationFee { get; set; }
        public int? TokenCount { get; set; } // Use nullable int if the column can be NULL
        public string Specialty { get; set; }
        public string DoctorName { get; set; }

    }
}
