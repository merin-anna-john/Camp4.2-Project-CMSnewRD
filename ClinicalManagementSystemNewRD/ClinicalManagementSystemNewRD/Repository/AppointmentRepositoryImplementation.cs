using ClinicalManagementSystemNewRD.Model;
using SqlServerConnectionLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD.Repository
{
    //Implements the method to execute the stored procedure.
    public class AppointmentRepositoryImplementation : IAppointmentRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

       


        //RECEPTIONIST MODULE
        // 1.1. View all appointments with filters
        #region View all appointments with filters
        public Task<List<Appointment>> GetAppointmentsAsync(DateTime? date = null, string doctorID = null, int? appointmentID = null)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Appointment>> GetAppointmentsAsync(DateTime? date = null, string doctorID = null)
        {
            var appointments = new List<Appointment>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string query = "SELECT * FROM Appointment WHERE (@Date IS NULL OR AppointmentDate = @Date) " +
                                   "AND (@DoctorID IS NULL OR DoctorID = @DoctorID) ";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Date", (object)date ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DoctorID", (object)doctorID ?? DBNull.Value);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var appointment = new Appointment
                                {
                                    PatientID = reader.GetInt32(1),     // Convert to int
                                    DoctorID = reader.GetInt32(2),      // Convert to int
                                    AppointmentDate = reader.GetDateTime(3), // Keep as DateTime

                                };
                                appointments.Add(appointment);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return appointments;
        }
        #endregion

        // 1.2. Schedule Appointment
        #region Schedule Appointment
        public async Task<int> GeneratePatientToken(int doctorId, int patientId)
        {
            int tokenId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand command = new SqlCommand("GeneratePatientToken", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Input parameters
                        command.Parameters.AddWithValue("@DoctorID", doctorId);
                        command.Parameters.AddWithValue("@PatientID", patientId);

                        // Output parameter for TokenID
                        SqlParameter tokenIdParam = new SqlParameter("@TokenID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(tokenIdParam);

                        // Execute the stored procedure
                        await command.ExecuteNonQueryAsync();

                        // Retrieve the output value
                        tokenId = (int)tokenIdParam.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log database errors (consider using a logging framework)
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log general errors
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return tokenId;
        }




        public async Task<bool> InsertAppointmentAsync(Appointment appointment)
        {
            try
            {
                // Generate token for the patient
                int tokenId = await GeneratePatientToken(appointment.DoctorID, appointment.PatientID);

                if (tokenId == 0)
                {
                    Console.WriteLine("Failed to generate token.");
                    return false;
                }

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string query = "INSERT INTO Appointment (PatientID, DoctorID, AppointmentDate, AppointmentTime, TokenID, Status) " +
                               "VALUES (@PatientID, @DoctorID, @AppointmentDate, @AppointmentTime, @TokenID, @Status)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@PatientID", appointment.PatientID);
                        command.Parameters.AddWithValue("@DoctorID", appointment.DoctorID);
                        command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                        command.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);
                        command.Parameters.AddWithValue("@TokenID", tokenId); // Use the generated tokenId
                        command.Parameters.AddWithValue("@Status", appointment.Status); // Add Status parameter

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
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

        public async Task<decimal> GetConsultationFeeAsync(int doctorId, int patientId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string storedProcedure = "sp_GetConsultationFee";

                using (SqlCommand command = new SqlCommand(storedProcedure, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    // If your stored procedure also requires PatientID, you can add it as follows:
                    // command.Parameters.AddWithValue("@PatientID", patientId);

                    object result = await command.ExecuteScalarAsync();
                    if (result != null && decimal.TryParse(result.ToString(), out decimal fee))
                    {
                        return fee;
                    }
                }
            }
            return 0; // Return 0 if not found
        }

        public async Task<bool> PatientExistsAsync(int patientId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT COUNT(1) FROM Patient WHERE PatientID = @PatientID";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@PatientID", patientId);
                        int count = (int)await command.ExecuteScalarAsync();
                        return count > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex.Message}");
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
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT COUNT(1) FROM Doctor WHERE DoctorID = @DoctorID";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        #endregion


        // 1.3. Reschedule Appointment
        #region Reschedule Appointment
        public async Task<Appointment> GetAppointmentDetailsAsync(int appointmentId)
        {
            Appointment appointment = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string query = "SELECT AppointmentID, AppointmentDate, AppointmentTime, PatientID, DoctorID, TokenID " +
                                   "FROM Appointment WHERE AppointmentID = @AppointmentID";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                appointment = new Appointment
                                {
                                    AppointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                                    AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                    AppointmentTime = reader.GetTimeSpan(reader.GetOrdinal("AppointmentTime")),
                                    PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                                    DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                    TokenID = reader.GetInt32(reader.GetOrdinal("TokenID"))
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return appointment;
        }

        public async Task<bool> RescheduleAppointmentAsync(int appointmentId, DateTime? newDate, TimeSpan? newTime)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_RescheduleAppointment", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                        command.Parameters.AddWithValue("@NewDate", (object)newDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@NewTime", (object)newTime ?? DBNull.Value);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
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
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string storedProcedure = "sp_GetConsultationFee";

                using (SqlCommand command = new SqlCommand(storedProcedure, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    // If your stored procedure also requires PatientID, you can add it as follows:
                    // command.Parameters.AddWithValue("@PatientID", patientId);

                    object result = await command.ExecuteScalarAsync();
                    if (result != null && decimal.TryParse(result.ToString(), out decimal fee))
                    {
                        return fee;
                    }
                }
            }
            return 0; // Return 0 if not found
        }

        #endregion

        // 1.4. Cancel Appointment
        #region Cancel Appointment
        public async Task DeleteAppointmentAsync(int appointmentID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string storedProcedure = "sp_DeleteAppointment";

                    using (SqlCommand command = new SqlCommand(storedProcedure, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", appointmentID);

                        await command.ExecuteNonQueryAsync();
                    }
                }
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

        // 2.1.Add Patient
        #region Add Patient
        public async Task<int> AddPatientAsync(Patient patient)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_AddPatient", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Name", patient.Name);
                        command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", patient.Gender);
                        command.Parameters.AddWithValue("@BloodGroup", patient.BloodGroup);
                        command.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", patient.Address);
                        command.Parameters.AddWithValue("@Email", patient.Email);

                        SqlParameter outputIdParam = new SqlParameter("@PatientID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        await command.ExecuteNonQueryAsync();

                        // Retrieve the output parameter value
                        int patientId = (int)command.Parameters["@PatientID"].Value;
                        return patientId;
                    }
                }
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


        // 2.2. Get Patient Details by ID or Phone number
        #region Get Patient Details by ID or Phone number
        public async Task<Patient> GetPatientDetailsAsync(int? patientID, string phoneNumber)
        {
            Patient patient = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_GetPatientByIDorPhoneNumber", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PatientID", patientID.HasValue ? (object)patientID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(phoneNumber) ? (object)DBNull.Value : phoneNumber);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                patient = new Patient
                                {
                                    PatientId = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    DateOfBirth = reader.GetDateTime(2),
                                    PhoneNumber = reader.GetString(3),
                                    Address = reader.GetString(4),
                                    Gender = reader.GetString(5),
                                    BloodGroup = reader.GetString(6),
                                    Email = reader.GetString(7)
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return patient;
        }
        #endregion


        // 2.3. Update Patient Details
        #region Update Patient Details
        public async Task UpdatePatientDetailsAsync(Patient patient)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_UpdatePatientDetails", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PatientID", patient.PatientId);
                        command.Parameters.AddWithValue("@Name", patient.Name);
                        command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);
                        command.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", patient.Address);
                        command.Parameters.AddWithValue("@Gender", patient.Gender);
                        command.Parameters.AddWithValue("@BloodGroup", patient.BloodGroup);
                        command.Parameters.AddWithValue("@Email", patient.Email);

                        await command.ExecuteNonQueryAsync();
                    }
                }
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

        // 3. View Doctor Availability
        #region View Doctor Availability
        public async Task<List<Availability>> GetDoctorAvailabilityAsync(int? doctorId, DateTime? date)
        {
            var availabilities = new List<Availability>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_GetDoctorAvailability", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorID", (object)doctorId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Date", (object)date ?? DBNull.Value);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                availabilities.Add(new Availability
                                {
                                    AvailabilityID = reader.GetInt32(0),
                                    DoctorID = reader.GetInt32(1),
                                    Date = reader.GetDateTime(2),
                                    TimeSlot = reader.GetTimeSpan(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return availabilities;
        }

        public async Task<(string DoctorName, string Specialization)> GetDoctorSpecialtyAsync(int doctorId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT DoctorName, Specialization FROM Doctor WHERE DoctorID = @DoctorID";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string doctorName = reader.IsDBNull(reader.GetOrdinal("DoctorName"))
                                                ? null
                                                : reader.GetString(reader.GetOrdinal("DoctorName"));

                            string specialization = reader.IsDBNull(reader.GetOrdinal("Specialization"))
                                                     ? null
                                                     : reader.GetString(reader.GetOrdinal("Specialization"));

                            return (doctorName, specialization);
                        }
                    }
                }
            }

            // Return null values if doctor not found
            return (null, null);
        }


        #endregion




        //DOCTOR MODULE
        //1.View Appointments
        #region View Appointments
        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            var appointments = new List<Appointment>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("sp_GetAllAppointments", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var appointment = new Appointment
                            {
                                AppointmentId = (int)reader["AppointmentId"],
                                PatientID = (int)reader["PatientID"],
                                DoctorID = (int)reader["DoctorID"],
                                AppointmentDate = (DateTime)reader["AppointmentDate"],
                                AppointmentTime = (TimeSpan)reader["AppointmentTime"],
                                TokenID = (int)reader["TokenID"]
                            };
                            appointments.Add(appointment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (e.g., using a logging framework)
                throw new Exception("Error fetching appointments", ex);
            }

            return appointments;
        }
        #endregion

        //2.Search Patient
        #region Search Patient
        public async Task<Patient> GetPatientByTokenId(int tokenId)
        {
            Patient patient = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT p.PatientId, p.Name FROM Patient p " +
                            "JOIN PatientToken pt ON p.PatientId = pt.PatientId " +
                            "WHERE pt.TokenID = @TokenId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TokenId", tokenId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            patient = new Patient
                            {
                                PatientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                               
                            };
                        }
                    }
                }
            }

            return patient;
        }
        public async Task AddSymptoms(int tokenId, string symptoms)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Appointment SET Symptoms = @Symptoms WHERE TokenID = @TokenId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Symptoms", symptoms);
                    command.Parameters.AddWithValue("@TokenId", tokenId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddDiagnosis(int tokenId, string diagnosis)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Appointment SET Diagnosis = @Diagnosis WHERE TokenID = @TokenId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Diagnosis", diagnosis);
                    command.Parameters.AddWithValue("@TokenId", tokenId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task PrescribeMedicine(int tokenId, string medicine)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Appointment SET Medicine = @Medicine WHERE TokenID = @TokenId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Medicine", medicine);
                    command.Parameters.AddWithValue("@TokenId", tokenId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task OrderLabTest(int tokenId, string labTest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Appointment SET LabTests = @LabTest WHERE TokenID = @TokenId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LabTest", labTest);
                    command.Parameters.AddWithValue("@TokenId", tokenId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddNotes(int tokenId, string notes)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Appointment SET Notes = @Notes WHERE TokenID = @TokenId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Notes", notes);
                    command.Parameters.AddWithValue("@TokenId", tokenId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        #endregion
    }
}
