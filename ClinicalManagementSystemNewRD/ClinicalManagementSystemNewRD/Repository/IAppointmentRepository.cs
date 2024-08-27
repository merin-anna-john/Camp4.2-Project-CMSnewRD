using ClinicalManagementSystemNewRD.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAppointmentRepository
{
    //RECEPTIONIST MODULE


    // 1.1. Add Patient
    Task<int> AddPatientAsync(Patient patient);

    // 1.2. Get Patient Details by ID or Phone number
    Task<Patient> GetPatientDetailsAsync(int? patientID, string phoneNumber);

    // 1.3. Update Patient Details
    Task UpdatePatientDetailsAsync(Patient patient);



    // 2. View Doctor Availability
    Task<List<Availability>> GetDoctorAvailabilityAsync(int? doctorId, DateTime? date);
    Task<(string DoctorName, string Specialization)> GetDoctorSpecialtyAsync(int doctorId);


    // 3.1. Add Appointment
    Task<bool> InsertAppointmentAsync(Appointment appointment);
    Task<decimal> GetConsultationFeeAsync(int doctorId, int patientId);
    Task<bool> PatientExistsAsync(int patientId);
    Task<bool> DoctorExistsAsync(int doctorId);
    Task<int> GeneratePatientToken(int doctorId, int patientId);

    // 3.2. Reschedule Appointment
    Task<Appointment> GetAppointmentDetailsAsync(int appointmentId);
    Task<bool> RescheduleAppointmentAsync(int appointmentId, DateTime? newDate, TimeSpan? newTime);
    Task<decimal> GetConsultationFeeAsyncSame(int doctorId, int patientId);


    // 3.3. Cancel Appointment Async
    Task DeleteAppointmentAsync(int appointmentID);





    //DOCTOR MODULE
    //1.View Appointment
    Task<List<Appointment>> GetAllAppointmentsAsync();


    //2.Search Patients
    Task<Patient> GetPatientByTokenId(int tokenId);
    Task AddSymptoms(int tokenId, string symptoms);
    Task AddDiagnosis(int tokenId, string diagnosis);
    Task PrescribeMedicine(int tokenId, string medicine);
    Task OrderLabTest(int tokenId, string labTest);
    Task AddNotes(int tokenId, string notes);
}
