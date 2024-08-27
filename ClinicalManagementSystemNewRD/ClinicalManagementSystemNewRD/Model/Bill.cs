using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Model
{
    public class Bill
    {
        // Properties
        public int BillID { get; set; }              // Unique identifier for the bill
        public int AppointmentID { get; set; }       // Associated Appointment ID
        public int PatientID { get; set; }           // Associated Patient ID
        public DateTime BillDate { get; set; }       // Date of the bill generation
        public decimal ConsultationFee { get; set; } // Fee for the consultation
        public decimal GST { get; set; }             // GST on the bill
        public decimal GrandTotal { get; set; }      // Total amount after adding GST

        // Constructor to initialize the Bill
        public Bill(int appointmentId, int patientId, decimal consultationFee, DateTime billDate)
        {
            AppointmentID = appointmentId;
            PatientID = patientId;
            ConsultationFee = consultationFee;
            BillDate = billDate;
            GST = CalculateGST(consultationFee);
            GrandTotal = consultationFee + GST;
        }

        // Method to calculate GST
        private decimal CalculateGST(decimal consultationFee)
        {
            // Assume GST is 18% of ConsultationFee
            const decimal gstRate = 0.18m;
            return gstRate * consultationFee;
        }
    }

}
