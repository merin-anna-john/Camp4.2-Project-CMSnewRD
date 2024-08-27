using ClinicalManagementSystemNewRD.Model;
using ClinicalManagementSystemNewRD.Repository;
using ClinicalManagementSystemNewRD.Service;
using ConsoleLoginApp2024.Utility;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinicalManagementSystemNewRD
{
    internal class ClinicApp
    {
        // Entry point
        static async Task Main(string[] args)
        {
            IAppointmentService appointmentService = new AppointmentServiceImplementation(new AppointmentRepositoryImplementation());

            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------------------------------------");
                Console.WriteLine("  W E L C O M E   T O    C L I N I C A L     M A N A G E M E N T     S Y S T E M   ");
                Console.WriteLine("-----------------------------------------------------------------------------------");

                int loginID = GetValidLoginID();
                string password = GetValidPassword();

                ILoginService loginService = new LoginServiceImplementation(new LoginRepositoryImplementation());
                int roleId = await loginService.AuthenticationAsync(loginID, password);

                if (roleId >= 1)
                {
                    switch (roleId)
                    {
                        case 1:
                            AdminMenu();
                            break;
                        case 2:
                            ReceptionistMenu(appointmentService);
                            break;
                        case 3:
                            DoctorMenu(appointmentService);
                            break;
                        default:
                            Console.WriteLine("Invalid role: ACCESS DENIED!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid credentials");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static int GetValidLoginID()
        {
            while (true)
            {
                Console.Write("Enter Login ID:");
                string loginIDString = Console.ReadLine();

                if (CustomValidation.IsValidNumber(loginIDString))
                {
                    return Convert.ToInt32(loginIDString);
                }
                else
                {
                    Console.WriteLine("Invalid Login, Try again");
                }
            }
        }

        static string GetValidPassword()
        {
            while (true)
            {
                Console.Write("Enter password:");
                string password = CustomValidation.ReadPassword();

                if (CustomValidation.IsValidPassword(password))
                {
                    return password;
                }
                else
                {
                    Console.WriteLine("Invalid Password, Try again");
                }
            }
        }


        #region Admin Menu
        static void AdminMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Admin Menu");
                Console.WriteLine("1. Add Staff");
                Console.WriteLine("2. Search Staff by ID");
                Console.WriteLine("3. Update Staff by ID");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Call method to add staff
                        break;
                    case "2":
                        // Call method to search staff by ID
                        break;
                    case "3":
                        // Call method to update staff
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong choice! Please enter again...");
                        break;
                }
            }
        }
        #endregion



        #region Receptionist Menu

        static void ReceptionistMenu(IAppointmentService service)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Reception");
                Console.WriteLine("1. Patient Management");
                Console.WriteLine("2. Doctor Schedule");
                Console.WriteLine("3. Manage Appointments");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        PatientManagement(service).Wait();
                        break;
                    case "2":
                        ShowDoctorAvailability(service).Wait();
                        break;
                    case "3":
                        ManageAppointments(service).Wait();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong choice! Please enter again...");
                        break;
                }
            }
        }

        
        #region Patient Management
        static async Task PatientManagement(IAppointmentService service)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Patient Managements");
                Console.WriteLine("1. Add New Patient");
                Console.WriteLine("2. Get Patient Details by ID or Phone number");
                Console.WriteLine("3. Update Patient Details");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await AddPatientAsync(service);
                        break;
                    case "2":
                        await GetPatientByIDorPhNo(service);
                        break;
                    case "3":
                        await UpdatePatientDetails(service);
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong choice! Please enter again...");
                        break;
                }
            }
        }

        #region Update Patient Details
        private static async Task UpdatePatientDetails(IAppointmentService service)
        {
            Console.WriteLine("Enter Patient ID:");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("Invalid Patient ID. Please enter a numeric ID.");
                Console.ReadKey(); // Pause to read the message
                return;
            }

            // Get existing patient details
            Patient patient = await service.GetPatientDetailsAsync(patientId, null);
            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                Console.ReadKey(); // Pause to read the message
                return;
            }

            // Update patient details (allow blank input to keep existing value)
            Console.WriteLine($"Enter Patient Name (Current: {patient.Name}):");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                patient.Name = name;
            }

            Console.WriteLine($"Enter Patient's Date of Birth (YYYY-MM-DD) (Current: {patient.DateOfBirth:yyyy-MM-dd}):");
            string dateOfBirth = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateOfBirth) && DateTime.TryParse(dateOfBirth, out DateTime parsedDate))
            {
                patient.DateOfBirth = parsedDate;
            }

            Console.WriteLine($"Enter Patient Address (Current: {patient.Address}):");
            string address = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(address))
            {
                patient.Address = address;
            }

            Console.WriteLine($"Enter Patient Phone Number (Current: {patient.PhoneNumber}):");
            string phoneNumber = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(phoneNumber) && Regex.IsMatch(phoneNumber, @"^\d{10}$"))
            {
                patient.PhoneNumber = phoneNumber;
            }

            Console.WriteLine($"Enter Patient Gender (Male/Female/Other) (Current: {patient.Gender}):");
            string gender = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(gender))
            {
                patient.Gender = gender;
            }

            Console.WriteLine($"Enter Patient Blood Group (e.g., A+, O-, AB+) (Current: {patient.BloodGroup}):");
            string bloodGroup = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(bloodGroup) && Regex.IsMatch(bloodGroup, @"^(A|B|AB|O)[+-]$"))
            {
                patient.BloodGroup = bloodGroup;
            }

            Console.WriteLine($"Enter Patient Email (Current: {patient.Email}):");
            string email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                patient.Email = email;
            }

            // Call the service to update the patient details
            await service.UpdatePatientDetailsAsync(patient);

            Console.WriteLine("Patient details updated successfully.");
            Console.ReadKey(); // Pause to read the message
        }
        #endregion


        #region GetPatientByIDorPhNo
        private static async Task GetPatientByIDorPhNo(IAppointmentService service)
        {
            // Collect search parameters
            Console.WriteLine("Enter Patient ID (or leave blank):");
            string patientIDInput = Console.ReadLine();
            int? patientID = null;
            if (int.TryParse(patientIDInput, out int id))
            {
                patientID = id;
            }

            Console.WriteLine("Enter Patient Phone Number (or leave blank):");
            string phoneNumber = Console.ReadLine();

            // Ensure at least one of the parameters is provided
            if (!patientID.HasValue && string.IsNullOrWhiteSpace(phoneNumber))
            {
                Console.WriteLine("You must provide either a Patient ID or Phone Number.");
                return;
            }

            // Call the service to get the patient details
            Patient patient = await service.GetPatientDetailsAsync(patientID, phoneNumber);

            if (patient != null)
            {
                Console.WriteLine($"Patient Details:\n" +
                                  $"ID: {patient.PatientId}\n" +
                                  $"Name: {patient.Name}\n" +
                                  $"Date of Birth: {patient.DateOfBirth.ToShortDateString()}\n" +
                                  $"Phone Number: {patient.PhoneNumber}\n" +
                                  $"Address: {patient.Address}\n" +
                                  $"Gender: {patient.Gender}\n" +
                                  $"Blood Group: {patient.BloodGroup}\n" +
                                  $"Email: {patient.Email}");
            }
            else
            {
                Console.WriteLine("No patient found with the provided details.");
            }
            Console.ReadKey();
        }


        #endregion


        #region AddPatient

        private static async Task AddPatientAsync(IAppointmentService service)
        {
            Patient patient = new Patient();

            // Patient Name
            while (true)
            {
                Console.WriteLine("Enter Patient Name:");
                patient.Name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(patient.Name) && Regex.IsMatch(patient.Name, @"^[a-zA-Z\s]+$"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input for Patient Name. Please ensure the name contains only letters and spaces.");
                }
            }


            // Patient Date of Birth
            while (true)
            {
                Console.WriteLine("Enter Patient's Date of Birth (YYYY-MM-DD):");
                string dateOfBirthInput = Console.ReadLine();

                // Check for blank space
                if (string.IsNullOrWhiteSpace(dateOfBirthInput))
                {
                    Console.WriteLine("Date of Birth cannot be blank. Please enter a valid date in YYYY-MM-DD format.");
                    continue;
                }

                // Try to parse the input into a DateTime
                if (DateTime.TryParse(dateOfBirthInput, out DateTime parsedDate))
                {
                    // Check if the parsed date is not today or in the future
                    if (parsedDate < DateTime.Today)
                    {
                        patient.DateOfBirth = parsedDate;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Date of Birth cannot be today or a future date. Please enter a valid past date.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for Date of Birth. Please enter a valid date in YYYY-MM-DD format.");
                }
            }



            // Patient Address
            Console.WriteLine("Enter Patient Address:");
            patient.Address = Console.ReadLine();

            // Patient Phone Number with Validation
            while (true)
            {
                Console.WriteLine("Enter Patient Phone Number:");
                string phoneNumber = Console.ReadLine();

                // Regular expression to validate phone number (e.g., 10 digits)
                if (Regex.IsMatch(phoneNumber, @"^\d{10}$"))
                {
                    patient.PhoneNumber = phoneNumber;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid phone number. Please enter a 10-digit phone number without any special characters or spaces.");
                }
            }

            // Patient Gender
            while (true)
            {
                Console.WriteLine("Enter Patient Gender (Male/Female/Other):");
                string genderInput = Console.ReadLine().Trim();

                if (genderInput.Equals("Male", StringComparison.OrdinalIgnoreCase) ||
                    genderInput.Equals("Female", StringComparison.OrdinalIgnoreCase) ||
                    genderInput.Equals("Other", StringComparison.OrdinalIgnoreCase))
                {
                    patient.Gender = char.ToUpper(genderInput[0]) + genderInput.Substring(1).ToLower(); // Capitalize the first letter
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input for Gender. Please enter 'Male', 'Female', or 'Other'.");
                }
            }

            // Patient Blood Group
            while (true)
            {
                Console.WriteLine("Enter Patient Blood Group (e.g., A+, O-, AB+):");
                string bloodGroup = Console.ReadLine();

                if (Regex.IsMatch(bloodGroup, @"^(A|B|AB|O)[+-]$"))
                {
                    patient.BloodGroup = bloodGroup;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid blood group. Please enter a valid blood group (e.g., A+, O-, AB+).");
                }
            }

            // Patient Email
            while (true)
            {
                Console.WriteLine("Enter Patient Email:");
                string email = Console.ReadLine();

                if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    patient.Email = email;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid email format. Please enter a valid email address.");
                }
            }

            // Call the service to add the patient
            int patientID = await service.AddPatientAsync(patient);

            if (patientID > 0)
            {
                Console.WriteLine($"Patient added successfully with ID: {patientID}");
            }
            else
            {
                Console.WriteLine("Failed to add patient.");
            }
            Console.ReadKey();
        }



        #endregion

        #endregion

        #region Show Doctor Availability
        static async Task ShowDoctorAvailability(IAppointmentService service)
        {
            Console.WriteLine("Enter Doctor ID (or leave blank for all doctors):");
            string doctorIdInput = Console.ReadLine();
            int? doctorId = string.IsNullOrEmpty(doctorIdInput) ? (int?)null : int.Parse(doctorIdInput);

            Console.WriteLine("Enter Date (YYYY-MM-DD) or leave blank for all dates:");
            string dateInput = Console.ReadLine();
            DateTime? date = string.IsNullOrEmpty(dateInput) ? (DateTime?)null : DateTime.Parse(dateInput);

            var availabilities = await service.ViewDoctorAvailabilityAsync(doctorId, date);

            if (availabilities.Any())
            {
                Console.WriteLine("Available Time Slots:");

                // Group by Doctor ID, Specialty, and Date
                var groupedAvailabilities = availabilities
                    .GroupBy(a => new { a.DoctorID, a.Date })
                    .ToList();

                foreach (var group in groupedAvailabilities)
                {
                    // Fetch specialty for the current Doctor ID
                    var (doctorName, specialty) = await service.GetDoctorSpecialtyAsync(group.Key.DoctorID);
                    Console.WriteLine($"Doctor ID: {group.Key.DoctorID},Name: {doctorName},Specialty: {specialty}, Date: {group.Key.Date.ToShortDateString()}");

                    foreach (var availability in group)
                    {
                        Console.WriteLine($"    Time: {availability.TimeSlot}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No availability found for the given criteria.");
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        #endregion

        #region Manage Appointments
        static async Task ManageAppointments(IAppointmentService service)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Manage Appointments");
                Console.WriteLine("1. Schedule New Appointment");
                Console.WriteLine("2. Update/Reschedule Appointment");
                Console.WriteLine("3. Cancel Appointment");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await ScheduleNewAppointment(service);
                        break;
                    case "2":
                        await UpdateOrRescheduleAppointment(service);
                        break;
                    case "3":
                        await CancelAppointment(service);
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong choice! Please enter again...");
                        break;
                }
            }
        }


        #region Schedule New Appointment
        static async Task ScheduleNewAppointment(IAppointmentService service)
        {
            Console.WriteLine("Schedule New Appointment");

            // Ensure PatientID is provided
            int patientId;
            while (true)
            {
                Console.WriteLine("Enter Patient ID:");
                if (int.TryParse(Console.ReadLine(), out patientId) && await service.PatientExistsAsync(patientId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid or non-existent Patient ID. Please enter a valid positive number.");
                }
            }

            // Ensure DoctorID is provided and exists
            int doctorId;
            while (true)
            {
                Console.WriteLine("Enter Doctor ID:");
                if (int.TryParse(Console.ReadLine(), out doctorId) && await service.DoctorExistsAsync(doctorId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid or non-existent Doctor ID. Please enter a valid positive number.");
                }
            }

            // Ensure Date is valid
            DateTime date;
            while (true)
            {
                Console.WriteLine("Enter Date (format YYYY-MM-DD, only tomorrow or the day after):");
                if (DateTime.TryParse(Console.ReadLine(), out date) && date >= DateTime.Today.AddDays(1) && date <= DateTime.Today.AddDays(2))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid date. Please enter a date for tomorrow or the day after in format YYYY-MM-DD.");
                }
            }

            // Ensure Time is valid
            TimeSpan time;
            while (true)
            {
                Console.WriteLine("Enter Time (format HH:mm):");
                if (TimeSpan.TryParse(Console.ReadLine(), out time))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid time. Please enter a time in format HH:mm.");
                }
            }

            // Generate Token
            int tokenID = await service.GeneratePatientToken(doctorId, patientId);

            var appointment = new Appointment
            {
                PatientID = patientId,
                DoctorID = doctorId,
                AppointmentDate = date,
                AppointmentTime = time,
                TokenID = tokenID
            };

            if (await service.ScheduleAppointmentAsync(appointment))
            {
                Console.WriteLine("Appointment scheduled successfully.");
                Console.WriteLine($"Appointment Details:\nPatientID: {appointment.PatientID}\nDoctorID: {appointment.DoctorID}\nAppointmentDate: {appointment.AppointmentDate:yyyy-MM-dd}\nAppointmentTime: {appointment.AppointmentTime}\nTokenID: {appointment.TokenID}");

                // Generate and display the consultation bill
                decimal consultationFee = await service.GetConsultationFeeAsync(doctorId, patientId);
                Console.WriteLine($"Consultation Fee: {consultationFee}");
            }

            else
            {
                Console.WriteLine("Failed to schedule the appointment.");
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }


        #endregion

        #region Update Or Reschedule Appointment
        static async Task UpdateOrRescheduleAppointment(IAppointmentService service)
        {
            Console.WriteLine("Update/Reschedule Appointment");

            // Get Appointment ID
            Console.WriteLine("Enter Appointment ID:");
            if (!int.TryParse(Console.ReadLine(), out int appointmentId))
            {
                Console.WriteLine("Invalid Appointment ID. Please enter a valid number.");
                return;
            }

            // Fetch current appointment details
            Appointment currentAppointment = await service.GetAppointmentDetailsAsync(appointmentId);
            if (currentAppointment == null)
            {
                Console.WriteLine("Appointment not found.");
                Console.ReadKey();
                return;
            }

            // Display current values
            Console.WriteLine($"Current Date: {currentAppointment.AppointmentDate:yyyy-MM-dd}");
            Console.WriteLine($"Current Time: {currentAppointment.AppointmentTime:hh\\:mm}");

            // Get new Date
            Console.WriteLine("Enter new Date (format YYYY-MM-DD) or leave blank to keep the current date:");
            string newDateInput = Console.ReadLine();
            DateTime? newDate = string.IsNullOrWhiteSpace(newDateInput) ? (DateTime?)null : DateTime.Parse(newDateInput);

            // Get new Time
            Console.WriteLine("Enter new Time (format HH:mm) or leave blank to keep the current time:");
            string newTimeInput = Console.ReadLine();
            TimeSpan? newTime = string.IsNullOrWhiteSpace(newTimeInput) ? (TimeSpan?)null : TimeSpan.Parse(newTimeInput);

            // Print new values to be updated
            Console.WriteLine($"New Date: {(newDate.HasValue ? newDate.Value.ToString("yyyy-MM-dd") : "No change")}");
            Console.WriteLine($"New Time: {(newTime.HasValue ? newTime.Value.ToString(@"hh\:mm") : "No change")}");

            // Generate and display the consultation bill
            decimal consultationFee = await service.GetConsultationFeeAsync(currentAppointment.DoctorID, currentAppointment.PatientID);
            Console.WriteLine($"Consultation Fee: {consultationFee:C}");


            // Update appointment
            bool result = await service.RescheduleAppointmentAsync(appointmentId, newDate, newTime);

            if (result)
            {
                Console.WriteLine("Failed to update the appointment.");
            }
            else
            {
                Console.WriteLine("Appointment rescheduled successfully.");
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }


        #endregion

        #region Cancel Appointment
        static async Task CancelAppointment(IAppointmentService service)
        {
            Console.WriteLine("Cancel Appointment");

            Console.WriteLine("Enter Appointment ID:");
            string appointmentIdInput = Console.ReadLine();

            if (string.IsNullOrEmpty(appointmentIdInput))
            {
                Console.WriteLine("Appointment ID is required.");
                return;
            }

            // Convert input to integer and handle potential format errors
            if (!int.TryParse(appointmentIdInput, out int appointmentId))
            {
                Console.WriteLine("Invalid Appointment ID. Please enter a valid number.");
                return;
            }

            try
            {
                // Attempt to delete the appointment
                await service.DeleteAppointmentAsync(appointmentId);
                Console.WriteLine("Appointment cancelled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to cancel the appointment: {ex.Message}");
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        #endregion


        #endregion


        #endregion



        #region DoctorMenu
        public static async Task DoctorMenu(IAppointmentService service)
        {
            bool exit = false;
            

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Doctor Menu");
                Console.WriteLine("1. View Appointments");
                Console.WriteLine("2. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await ViewAllAppointments(service);
                        break;
                    case "2":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong choice! Please enter again...");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }


        // Method to fetch and display scheduled appointments for the logged-in doctor

        private static async Task ViewAllAppointments(IAppointmentService service)
        {
            var appointments = await service.GetAllAppointmentsAsync();

            foreach (var appointment in appointments)
            {
                Console.WriteLine($"Appointment ID: {appointment.AppointmentId}");
                Console.WriteLine($"Patient ID: {appointment.PatientID}");
                Console.WriteLine($"Doctor ID: {appointment.DoctorID}");
                Console.WriteLine($"Date: {appointment.AppointmentDate}");
                Console.WriteLine($"Time: {appointment.AppointmentTime}");
                Console.WriteLine($"Token ID: {appointment.TokenID}");
                Console.WriteLine("--------------------------");
            }

            Console.WriteLine("Enter token ID to search:");
            if (int.TryParse(Console.ReadLine(), out int tokenId))
            {
                try
                {
                    var patient = await service.GetPatientByTokenId(tokenId);

                    if (patient != null)
                    {
                        Console.WriteLine($"PatientID: {patient.PatientId}, Name: {patient.Name}");
                        // Additional patient details can be displayed here
                    }
                    else
                    {
                        Console.WriteLine("No patient found with the given token ID.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while fetching patient details: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid token ID. Please enter a numeric value.");
            }


            // Implementation for adding symptoms
            Console.WriteLine("Enter symptoms:");
            string symptoms = Console.ReadLine();
            // Call service method to save symptoms
            await service.AddSymptoms(tokenId, symptoms);
            Console.WriteLine("Symptoms added successfully.");

            // Implementation for adding diagnosis
            Console.WriteLine("Enter diagnosis:");
            string diagnosis = Console.ReadLine();
            // Call service method to save diagnosis
            await service.AddDiagnosis(tokenId, diagnosis);
            Console.WriteLine("Diagnosis added successfully.");

            // Implementation for prescribing medicine
            Console.WriteLine("Enter medicine:");
            string medicine = Console.ReadLine();
            // Call service method to save medicine prescription
            await service.PrescribeMedicine(tokenId, medicine);
            Console.WriteLine("Medicine prescribed successfully.");

            // Implementation for ordering lab test
            Console.WriteLine("Enter lab test:");
            string labTest = Console.ReadLine();
            // Call service method to order lab test
            await service.OrderLabTest(tokenId, labTest);
            Console.WriteLine("Lab test ordered successfully.");

            // Implementation for adding notes
            Console.WriteLine("Enter notes:");
            string notes = Console.ReadLine();
            // Call service method to save notes
            await service.AddNotes(tokenId, notes);
            Console.WriteLine("Notes added successfully.");
        }
        


        #region HandlePatientActions
        private static async Task HandlePatientActions(IAppointmentService service, int tokenId)
        {
            Console.WriteLine("Select action:");
            Console.WriteLine("1. Add Symptoms");
            Console.WriteLine("2. Add Diagnosis");
            Console.WriteLine("3. Prescribe Medicine");
            Console.WriteLine("4. Order Lab Test");
            Console.WriteLine("5. Add Notes");
            Console.WriteLine("Enter your choice:");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await AddSymptoms(service, tokenId);
                    break;
                case "2":
                    await AddDiagnosis(service, tokenId);
                    break;
                case "3":
                    await PrescribeMedicine(service, tokenId);
                    break;
                case "4":
                    await OrderLabTest(service, tokenId);
                    break;
                case "5":
                    await AddNotes(service, tokenId);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        #endregion

        #region Actions Implementation
        private static async Task AddSymptoms(IAppointmentService service, int tokenId)
        {
            // Implementation for adding symptoms
            Console.WriteLine("Enter symptoms:");
            string symptoms = Console.ReadLine();
            // Call service method to save symptoms
            await service.AddSymptoms(tokenId, symptoms);
            Console.WriteLine("Symptoms added successfully.");
        }

        private static async Task AddDiagnosis(IAppointmentService service, int tokenId)
        {
            // Implementation for adding diagnosis
            Console.WriteLine("Enter diagnosis:");
            string diagnosis = Console.ReadLine();
            // Call service method to save diagnosis
            await service.AddDiagnosis(tokenId, diagnosis);
            Console.WriteLine("Diagnosis added successfully.");
        }

        private static async Task PrescribeMedicine(IAppointmentService service, int tokenId)
        {
            // Implementation for prescribing medicine
            Console.WriteLine("Enter medicine:");
            string medicine = Console.ReadLine();
            // Call service method to save medicine prescription
            await service.PrescribeMedicine(tokenId, medicine);
            Console.WriteLine("Medicine prescribed successfully.");
        }

        private static async Task OrderLabTest(IAppointmentService service, int tokenId)
        {
            // Implementation for ordering lab test
            Console.WriteLine("Enter lab test:");
            string labTest = Console.ReadLine();
            // Call service method to order lab test
            await service.OrderLabTest(tokenId, labTest);
            Console.WriteLine("Lab test ordered successfully.");
        }

        private static async Task AddNotes(IAppointmentService service, int tokenId)
        {
            // Implementation for adding notes
            Console.WriteLine("Enter notes:");
            string notes = Console.ReadLine();
            // Call service method to save notes
            await service.AddNotes(tokenId, notes);
            Console.WriteLine("Notes added successfully.");
        }
        #endregion
         
        #endregion

       


        



















    }

}

