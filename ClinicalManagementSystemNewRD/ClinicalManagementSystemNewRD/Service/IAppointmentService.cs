using ClinicalManagementSystemNewRD.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Service
{
    public interface IAppointmentService
    {
        //RECEPTIONIST MODULE


        // 1.1. Add Patient
        Task<int> AddPatientAsync(Patient patient);

        // 1.2. Get Patient Details by ID or Phone number
        Task<Patient> GetPatientDetailsAsync(int? patientID, string phoneNumber);

        // 1.3. Update Patient Details
        Task UpdatePatientDetailsAsync(Patient patient);




        // 2. View Doctor Availability
        Task<List<Availability>> ViewDoctorAvailabilityAsync(int? doctorId, DateTime? date);
        Task<(string DoctorName, string Specialization)> GetDoctorSpecialtyAsync(int doctorId);



        // 3.2. Add Appointment
        Task<bool> ScheduleAppointmentAsync(Appointment appointment);
        Task<decimal> GetConsultationFeeAsync(int doctorId, int patientId);
        Task<bool> PatientExistsAsync(int patientId);
        Task<bool> DoctorExistsAsync(int doctorId);
        Task<int> GeneratePatientToken(int doctorId, int patientId);

        // 3.3. Reschedule Appointment
        Task<Appointment> GetAppointmentDetailsAsync(int appointmentId);
        Task<bool> RescheduleAppointmentAsync(int appointmentId, DateTime? newDate, TimeSpan? newTime);
        Task<decimal> GetConsultationFeeAsyncSame(int doctorId, int patientId);


        // 3.4. Cancel Appointment Async
        Task DeleteAppointmentAsync(int appointmentID);






        //DOCTOR MODULE
        //3.1 View Appointment
        Task<List<Appointment>> GetAllAppointmentsAsync();

        //3.2  Search Patient
        Task<Patient> GetPatientByTokenId(int tokenId);

        Task AddSymptoms(int tokenId, string symptoms);
        Task AddDiagnosis(int tokenId, string diagnosis);
        Task PrescribeMedicine(int tokenId, string medicine);
        Task OrderLabTest(int tokenId, string labTest);
        Task AddNotes(int tokenId, string notes);

    }
}
