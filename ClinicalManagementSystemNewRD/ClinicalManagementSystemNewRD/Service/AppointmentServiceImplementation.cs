using ClinicalManagementSystemNewRD.Model;
using ClinicalManagementSystemNewRD.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Service
{
    //Calls the repository method and manages exceptions.
    public class AppointmentServiceImplementation : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentServiceImplementation(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        //RECEPTIONIST MODULE


        // 1.1. Add Patient
        #region Add Patient
        public async Task<int> AddPatientAsync(Patient patient)
        {
            try
            {
                // Call the repository method and return its result
                int patientID = await _appointmentRepository.AddPatientAsync(patient);
                return patientID;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return -1; // Indicate an error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return -1; // Indicate an error
            }
        }
        #endregion

        // 1.2. Get Patient Details by ID or Phone number
        #region Get Patient Details by ID or Phone number
        public async Task<Patient> GetPatientDetailsAsync(int? patientID, string phoneNumber)
        {
            try
            {
                return await _appointmentRepository.GetPatientDetailsAsync(patientID, phoneNumber);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return null; // Indicate failure
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Indicate failure
            }
        }
        #endregion

        // 1.3. Update Patient Details
        #region Update Patient Details
        public async Task UpdatePatientDetailsAsync(Patient patient)
        {
            try
            {
                await _appointmentRepository.UpdatePatientDetailsAsync(patient);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ReadKey();
            }
        }
        #endregion



        // 2. View Doctor Availability
        #region View Doctor Availability
        public async Task<List<Availability>> ViewDoctorAvailabilityAsync(int? doctorId, DateTime? date)
        {
            try
            {
                return await _appointmentRepository.GetDoctorAvailabilityAsync(doctorId, date);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return new List<Availability>(); // Return an empty list on error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Availability>(); // Return an empty list on error
            }
        }

        public async Task<(string DoctorName, string Specialization)> GetDoctorSpecialtyAsync(int doctorId)
        {
            try
            {
                return await _appointmentRepository.GetDoctorSpecialtyAsync(doctorId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return (null, null);
            }
        }



        #endregion



        // 3.1. Schedule Appointment
        #region Schedule Appointment

        public async Task<bool> ScheduleAppointmentAsync(Appointment appointment)
        {
            try
            {
                // Set the status to "Scheduled"
                appointment.Status = "Scheduled";

                // Call repository method to insert the appointment
                return await _appointmentRepository.InsertAppointmentAsync(appointment);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false; // Return false in case of an error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Return false in case of an error
            }
        }

        public async Task<decimal> GetConsultationFeeAsync(int doctorId, int patientId)
        {
            try
            {
                return await _appointmentRepository.GetConsultationFeeAsync(doctorId, patientId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw; // Optionally rethrow the exception or return a default value
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Optionally rethrow the exception or return a default value
            }
        }

        public async Task<bool> PatientExistsAsync(int patientId)
        {
            try
            {
                return await _appointmentRepository.PatientExistsAsync(patientId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DoctorExistsAsync(int doctorId)
        {
            try
            {
                return await _appointmentRepository.DoctorExistsAsync(doctorId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking DoctorID: {ex.Message}");
                return false;
            }
        }

        public async Task<int> GeneratePatientToken(int doctorId, int patientId)
        {
            return await _appointmentRepository.GeneratePatientToken(doctorId, patientId);
        }
        #endregion

        // 3.2. Reschedule Appointment
        #region  Reschedule Appointment
        public async Task<Appointment> GetAppointmentDetailsAsync(int appointmentId)
        {
            try
            {
                return await _appointmentRepository.GetAppointmentDetailsAsync(appointmentId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> RescheduleAppointmentAsync(int appointmentId, DateTime? newDate, TimeSpan? newTime)
        {
            try
            {
                return await _appointmentRepository.RescheduleAppointmentAsync(appointmentId, newDate, newTime);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Number} - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return false;
            }
        }

        public async Task<decimal> GetConsultationFeeAsyncSame(int doctorId, int patientId)
        {
            try
            {
                return await _appointmentRepository.GetConsultationFeeAsync(doctorId, patientId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw; // Optionally rethrow the exception or return a default value
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Optionally rethrow the exception or return a default value
            }
        }

        #endregion

        // 3.3. Cancel Appointment
        #region Cancel Appointment
        public async Task DeleteAppointmentAsync(int appointmentID)
        {
            try
            {
                await _appointmentRepository.DeleteAppointmentAsync(appointmentID);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        #endregion


        //DOCTOR MODULE

        //3.1 View Todays scheduled appointments according to particular doctor id
        #region View Todays scheduled appointments according to particular doctor id
        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }


        #endregion

        //3.2  Search Patient
        #region Search Patient
        public async Task<Patient> GetPatientByTokenId(int tokenId)
        {
            return await _appointmentRepository.GetPatientByTokenId(tokenId);
        }
        public async Task AddSymptoms(int tokenId, string symptoms)
        {
            await _appointmentRepository.AddSymptoms(tokenId, symptoms);
        }

        public async Task AddDiagnosis(int tokenId, string diagnosis)
        {
            await _appointmentRepository.AddDiagnosis(tokenId, diagnosis);
        }

        public async Task PrescribeMedicine(int tokenId, string medicine)
        {
            await _appointmentRepository.PrescribeMedicine(tokenId, medicine);
        }

        public async Task OrderLabTest(int tokenId, string labTest)
        {
            await _appointmentRepository.OrderLabTest(tokenId, labTest);
        }

        public async Task AddNotes(int tokenId, string notes)
        {
            await _appointmentRepository.AddNotes(tokenId, notes);
        }


        #endregion


    }
}
