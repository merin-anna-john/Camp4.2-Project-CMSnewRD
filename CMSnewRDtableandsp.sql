USE [ClinicalManagementSystemNewRD_db]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[AppointmentID] [int] IDENTITY(2221,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[DoctorID] [int] NOT NULL,
	[AppointmentDate] [date] NOT NULL,
	[AppointmentTime] [time](7) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Symptoms] [nvarchar](max) NULL,
	[Diagnosis] [nvarchar](max) NULL,
	[Medicine] [nvarchar](max) NULL,
	[LabTests] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[TokenID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AppointmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Availability]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Availability](
	[AvailabilityID] [int] IDENTITY(100,1) NOT NULL,
	[DoctorID] [int] NULL,
	[Date] [date] NULL,
	[TimeSlot] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[AvailabilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[DoctorID] [int] NOT NULL,
	[StaffID] [int] NOT NULL,
	[ConsultationFee] [decimal](10, 2) NOT NULL,
	[Specialization] [varchar](100) NULL,
	[DoctorName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[DoctorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[LoginID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[StaffID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicine]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicine](
	[MedicineID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Dosage] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Manufacturer] [nvarchar](100) NULL,
	[ExpirationDate] [date] NULL,
	[Price] [decimal](10, 2) NULL,
	[QuantityInStock] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MedicineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[PatientID] [int] IDENTITY(1111,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [varchar](10) NULL,
	[BloodGroup] [varchar](3) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Email] [varchar](100) NULL,
	[Address] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientToken]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientToken](
	[TokenID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[DoctorID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TokenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Qualification]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Qualification](
	[QualificationID] [int] NOT NULL,
	[SpecializationID] [int] NULL,
	[QualificationName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QualificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[RoleName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[SpecializationID] [int] NOT NULL,
	[SpecializationName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SpecializationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[StaffID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[StaffName] [varchar](100) NOT NULL,
	[QualificationID] [int] NULL,
	[Gender] [varchar](10) NULL,
	[DateOfBirth] [date] NULL,
	[BloodGroup] [varchar](3) NULL,
	[Address] [varchar](255) NULL,
	[Email] [varchar](100) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[DateOfJoining] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[StaffID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 27-08-2024 14:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[TestID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointment] ON 

INSERT [dbo].[Appointment] ([AppointmentID], [PatientID], [DoctorID], [AppointmentDate], [AppointmentTime], [Status], [Symptoms], [Diagnosis], [Medicine], [LabTests], [Notes], [TokenID]) VALUES (2221, 1111, 3010, CAST(N'2024-08-28' AS Date), CAST(N'01:00:00' AS Time), N'Scheduled', N'Fever', N'Infection', N'Paracetamol', N'Blood Test', N'Take one week rest', 1)
INSERT [dbo].[Appointment] ([AppointmentID], [PatientID], [DoctorID], [AppointmentDate], [AppointmentTime], [Status], [Symptoms], [Diagnosis], [Medicine], [LabTests], [Notes], [TokenID]) VALUES (2222, 1112, 3010, CAST(N'2024-08-29' AS Date), CAST(N'02:00:00' AS Time), N'Scheduled', NULL, NULL, NULL, NULL, NULL, 3)
INSERT [dbo].[Appointment] ([AppointmentID], [PatientID], [DoctorID], [AppointmentDate], [AppointmentTime], [Status], [Symptoms], [Diagnosis], [Medicine], [LabTests], [Notes], [TokenID]) VALUES (2223, 1113, 3011, CAST(N'2024-08-28' AS Date), CAST(N'04:00:00' AS Time), N'Scheduled', NULL, NULL, NULL, NULL, NULL, 5)
INSERT [dbo].[Appointment] ([AppointmentID], [PatientID], [DoctorID], [AppointmentDate], [AppointmentTime], [Status], [Symptoms], [Diagnosis], [Medicine], [LabTests], [Notes], [TokenID]) VALUES (2224, 1115, 3010, CAST(N'2024-08-28' AS Date), CAST(N'14:00:00' AS Time), N'Scheduled', N'', N'', N'', NULL, NULL, 8)
INSERT [dbo].[Appointment] ([AppointmentID], [PatientID], [DoctorID], [AppointmentDate], [AppointmentTime], [Status], [Symptoms], [Diagnosis], [Medicine], [LabTests], [Notes], [TokenID]) VALUES (2225, 1115, 3011, CAST(N'2024-08-29' AS Date), CAST(N'03:00:00' AS Time), N'Scheduled', N'Cold', N'Fever', N'Dolo', N'Blood Test', N'Rest', 10)
INSERT [dbo].[Appointment] ([AppointmentID], [PatientID], [DoctorID], [AppointmentDate], [AppointmentTime], [Status], [Symptoms], [Diagnosis], [Medicine], [LabTests], [Notes], [TokenID]) VALUES (2226, 1116, 3010, CAST(N'2024-08-28' AS Date), CAST(N'06:00:00' AS Time), N'Scheduled', NULL, NULL, NULL, NULL, NULL, 12)
SET IDENTITY_INSERT [dbo].[Appointment] OFF
GO
SET IDENTITY_INSERT [dbo].[Availability] ON 

INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (100, 3010, CAST(N'2024-08-27' AS Date), CAST(N'14:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (101, 3010, CAST(N'2024-08-27' AS Date), CAST(N'15:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (102, 3010, CAST(N'2024-08-27' AS Date), CAST(N'16:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (103, 3010, CAST(N'2024-08-27' AS Date), CAST(N'17:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (104, 3010, CAST(N'2024-08-27' AS Date), CAST(N'18:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (105, 3010, CAST(N'2024-08-27' AS Date), CAST(N'19:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (106, 3010, CAST(N'2024-08-27' AS Date), CAST(N'20:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (107, 3011, CAST(N'2024-08-28' AS Date), CAST(N'09:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (108, 3011, CAST(N'2024-08-28' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (109, 3011, CAST(N'2024-08-28' AS Date), CAST(N'11:00:00' AS Time))
INSERT [dbo].[Availability] ([AvailabilityID], [DoctorID], [Date], [TimeSlot]) VALUES (110, 3011, CAST(N'2024-08-28' AS Date), CAST(N'12:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[Availability] OFF
GO
INSERT [dbo].[Doctor] ([DoctorID], [StaffID], [ConsultationFee], [Specialization], [DoctorName]) VALUES (3010, 3000, CAST(500.00 AS Decimal(10, 2)), N'Dermatology', N'Rajesh')
INSERT [dbo].[Doctor] ([DoctorID], [StaffID], [ConsultationFee], [Specialization], [DoctorName]) VALUES (3011, 3001, CAST(550.00 AS Decimal(10, 2)), N'Cardiology', N'Meenakshi')
GO
INSERT [dbo].[Login] ([LoginID], [RoleID], [Password], [StaffID]) VALUES (1122, 2, N'Loginm@123', 2001)
INSERT [dbo].[Login] ([LoginID], [RoleID], [Password], [StaffID]) VALUES (1123, 2, N'Logins@123', 2002)
INSERT [dbo].[Login] ([LoginID], [RoleID], [Password], [StaffID]) VALUES (3010, 3, N'Loginr@123', 3000)
INSERT [dbo].[Login] ([LoginID], [RoleID], [Password], [StaffID]) VALUES (3011, 3, N'Loginme@123', 3001)
INSERT [dbo].[Login] ([LoginID], [RoleID], [Password], [StaffID]) VALUES (9988, 1, N'Logina@123', 1000)
GO
SET IDENTITY_INSERT [dbo].[Medicine] ON 

INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (1, N'Aspirin', N'500mg', N'Painkiller', N'PharmaCorp', CAST(N'2025-12-31' AS Date), CAST(9.99 AS Decimal(10, 2)), 100)
INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (2, N'Amoxicillin', N'250mg', N'Antibiotic', N'MediPharm', CAST(N'2024-11-30' AS Date), CAST(12.50 AS Decimal(10, 2)), 50)
INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (3, N'Metformin', N'500mg', N'Antidiabetic', N'HealthPlus', CAST(N'2026-01-15' AS Date), CAST(14.75 AS Decimal(10, 2)), 75)
INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (4, N'Ibuprofen', N'200mg', N'Painkiller', N'Wellness Inc.', CAST(N'2024-10-10' AS Date), CAST(8.99 AS Decimal(10, 2)), 120)
INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (5, N'Cetirizine', N'10mg', N'Antihistamine', N'AllergyMed', CAST(N'2025-05-20' AS Date), CAST(7.49 AS Decimal(10, 2)), 200)
INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (6, N'Omeprazole', N'20mg', N'Antacid', N'StomachCare', CAST(N'2025-07-25' AS Date), CAST(11.00 AS Decimal(10, 2)), 60)
INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (7, N'Simvastatin', N'20mg', N'Cholesterol', N'CardioPharm', CAST(N'2026-03-05' AS Date), CAST(19.99 AS Decimal(10, 2)), 40)
INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (8, N'Losartan', N'50mg', N'Antihypertensive', N'HeartHealth', CAST(N'2025-08-15' AS Date), CAST(13.25 AS Decimal(10, 2)), 80)
INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (9, N'Hydrochlorothiazide', N'25mg', N'Diuretic', N'KidneyPlus', CAST(N'2024-09-30' AS Date), CAST(6.50 AS Decimal(10, 2)), 90)
INSERT [dbo].[Medicine] ([MedicineID], [Name], [Dosage], [Type], [Manufacturer], [ExpirationDate], [Price], [QuantityInStock]) VALUES (10, N'Gabapentin', N'300mg', N'Anticonvulsant', N'NeuroCare', CAST(N'2026-02-28' AS Date), CAST(22.50 AS Decimal(10, 2)), 70)
SET IDENTITY_INSERT [dbo].[Medicine] OFF
GO
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([PatientID], [Name], [DateOfBirth], [Gender], [BloodGroup], [PhoneNumber], [Email], [Address]) VALUES (1111, N'Sonu', CAST(N'1990-09-08' AS Date), N'Female', N'A+', N'9087897687', N'sona@gmail.com', N'Trivandrum')
INSERT [dbo].[Patient] ([PatientID], [Name], [DateOfBirth], [Gender], [BloodGroup], [PhoneNumber], [Email], [Address]) VALUES (1112, N'Manoj', CAST(N'1995-08-05' AS Date), N'Male', N'O-', N'8907897896', N'mano@gmail.com', N'Kochi')
INSERT [dbo].[Patient] ([PatientID], [Name], [DateOfBirth], [Gender], [BloodGroup], [PhoneNumber], [Email], [Address]) VALUES (1113, N'Surej', CAST(N'1994-08-07' AS Date), N'Male', N'AB+', N'9087656890', N'surej@gmail.com', N'Kollam')
INSERT [dbo].[Patient] ([PatientID], [Name], [DateOfBirth], [Gender], [BloodGroup], [PhoneNumber], [Email], [Address]) VALUES (1114, N'Goku', CAST(N'1991-09-08' AS Date), N'Male', N'O-', N'8907657890', N'kevin@gmail.com', N'Trivandrum')
INSERT [dbo].[Patient] ([PatientID], [Name], [DateOfBirth], [Gender], [BloodGroup], [PhoneNumber], [Email], [Address]) VALUES (1115, N'Wilson', CAST(N'1990-07-12' AS Date), N'Male', N'A+', N'8907654356', N'wilson@gmail.com', N'Florida')
INSERT [dbo].[Patient] ([PatientID], [Name], [DateOfBirth], [Gender], [BloodGroup], [PhoneNumber], [Email], [Address]) VALUES (1116, N'Gokul', CAST(N'2000-09-08' AS Date), N'Male', N'A+', N'1234567890', N'gokul@gmail.com', N'Trivandrum')
SET IDENTITY_INSERT [dbo].[Patient] OFF
GO
SET IDENTITY_INSERT [dbo].[PatientToken] ON 

INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (1, 1111, 3010)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (2, 1111, 3010)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (3, 1112, 3010)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (4, 1112, 3010)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (5, 1113, 3011)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (6, 1113, 3011)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (7, 1115, 3010)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (8, 1115, 3010)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (9, 1115, 3011)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (10, 1115, 3011)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (11, 1116, 3010)
INSERT [dbo].[PatientToken] ([TokenID], [PatientID], [DoctorID]) VALUES (12, 1116, 3010)
SET IDENTITY_INSERT [dbo].[PatientToken] OFF
GO
INSERT [dbo].[Qualification] ([QualificationID], [SpecializationID], [QualificationName]) VALUES (111, 30, N'Business Communication, Human Resources')
INSERT [dbo].[Qualification] ([QualificationID], [SpecializationID], [QualificationName]) VALUES (112, 30, N'Medical Receptionist Course')
INSERT [dbo].[Qualification] ([QualificationID], [SpecializationID], [QualificationName]) VALUES (113, 10, N'MBBS,MD')
INSERT [dbo].[Qualification] ([QualificationID], [SpecializationID], [QualificationName]) VALUES (114, 20, N'ACLS Certification')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'Receptionist')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (3, N'Doctor')
GO
INSERT [dbo].[Specialization] ([SpecializationID], [SpecializationName]) VALUES (10, N'Dermatology')
INSERT [dbo].[Specialization] ([SpecializationID], [SpecializationName]) VALUES (20, N'Cardiology')
INSERT [dbo].[Specialization] ([SpecializationID], [SpecializationName]) VALUES (30, N'None')
GO
INSERT [dbo].[Staff] ([StaffID], [RoleID], [StaffName], [QualificationID], [Gender], [DateOfBirth], [BloodGroup], [Address], [Email], [PhoneNumber], [DateOfJoining]) VALUES (1000, 1, N'Anu', 111, N'Female', CAST(N'1975-05-05' AS Date), N'O+', N'Trivandrum', N'anu@gmail.com', N'8794328654', CAST(N'2020-10-05' AS Date))
INSERT [dbo].[Staff] ([StaffID], [RoleID], [StaffName], [QualificationID], [Gender], [DateOfBirth], [BloodGroup], [Address], [Email], [PhoneNumber], [DateOfJoining]) VALUES (2001, 2, N'Manju', 112, N'Female', CAST(N'1989-10-13' AS Date), N'A+', N'Trivandrum', N'manju@gmail.com', N'7890654326', CAST(N'2022-11-13' AS Date))
INSERT [dbo].[Staff] ([StaffID], [RoleID], [StaffName], [QualificationID], [Gender], [DateOfBirth], [BloodGroup], [Address], [Email], [PhoneNumber], [DateOfJoining]) VALUES (2002, 2, N'Suresh', 112, N'Male', CAST(N'1990-05-24' AS Date), N'B+', N'Kannur', N'suresh@gmail.com', N'9234519876', CAST(N'2021-12-24' AS Date))
INSERT [dbo].[Staff] ([StaffID], [RoleID], [StaffName], [QualificationID], [Gender], [DateOfBirth], [BloodGroup], [Address], [Email], [PhoneNumber], [DateOfJoining]) VALUES (3000, 3, N'Rajesh', 113, N'Male', CAST(N'1979-11-14' AS Date), N'A+', N'Kochi', N'rajesh@gmail.com', N'8765903452', CAST(N'2020-03-14' AS Date))
INSERT [dbo].[Staff] ([StaffID], [RoleID], [StaffName], [QualificationID], [Gender], [DateOfBirth], [BloodGroup], [Address], [Email], [PhoneNumber], [DateOfJoining]) VALUES (3001, 3, N'Meenakshi', 114, N'Female', CAST(N'1975-11-14' AS Date), N'O-', N'Kochi', N'meenakshi@gmail.com', N'9087654678', CAST(N'2020-04-14' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Test] ON 

INSERT [dbo].[Test] ([TestID], [Name]) VALUES (1, N'Complete Blood Count (CBC)')
INSERT [dbo].[Test] ([TestID], [Name]) VALUES (2, N'Basic Metabolic Panel (BMP)')
INSERT [dbo].[Test] ([TestID], [Name]) VALUES (3, N'Lipid Panel')
INSERT [dbo].[Test] ([TestID], [Name]) VALUES (4, N'Thyroid Function Test')
INSERT [dbo].[Test] ([TestID], [Name]) VALUES (5, N'Hemoglobin A1c')
INSERT [dbo].[Test] ([TestID], [Name]) VALUES (6, N'Urinalysis')
INSERT [dbo].[Test] ([TestID], [Name]) VALUES (7, N'Prostate-Specific Antigen (PSA)')
INSERT [dbo].[Test] ([TestID], [Name]) VALUES (8, N'Vitamin D Level')
INSERT [dbo].[Test] ([TestID], [Name]) VALUES (9, N'Comprehensive Metabolic Panel (CMP)')
INSERT [dbo].[Test] ([TestID], [Name]) VALUES (10, N'Chest X-Ray')
SET IDENTITY_INSERT [dbo].[Test] OFF
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Doctor] ([DoctorID])
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
GO
ALTER TABLE [dbo].[Availability]  WITH CHECK ADD FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Doctor] ([DoctorID])
GO
ALTER TABLE [dbo].[Login]  WITH CHECK ADD  CONSTRAINT [FK__Login__RoleID__46E78A0C] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[Login] CHECK CONSTRAINT [FK__Login__RoleID__46E78A0C]
GO
ALTER TABLE [dbo].[PatientToken]  WITH CHECK ADD FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
GO
ALTER TABLE [dbo].[Qualification]  WITH CHECK ADD FOREIGN KEY([SpecializationID])
REFERENCES [dbo].[Specialization] ([SpecializationID])
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD FOREIGN KEY([QualificationID])
REFERENCES [dbo].[Qualification] ([QualificationID])
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
/****** Object:  StoredProcedure [dbo].[GeneratePatientToken]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[GeneratePatientToken]
    @PatientID INT,
    @DoctorID INT,
    @TokenID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Generate a new token value (simple example, you may use more complex logic)
    DECLARE @TokenValue NVARCHAR(50);
    SET @TokenValue = NEWID(); -- Use GUID or other token generation logic

    -- Insert the new token into the PatientToken table
    INSERT INTO PatientToken (PatientID, DoctorID)
    VALUES (@PatientID, @DoctorID);

    -- Get the newly generated TokenID
    SET @TokenID = SCOPE_IDENTITY(); -- Fetches the last identity value inserted
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_AddAvailability]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[sp_AddAvailability]
    @DoctorID INT,
    @Date DATE,
    @TimeSlot TIME
AS
BEGIN
    INSERT INTO Availability (DoctorID, Date, TimeSlot)
    VALUES (@DoctorID, @Date, @TimeSlot);
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_AddPatient]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[sp_AddPatient]
    @Name NVARCHAR(100),
    @DateOfBirth DATE,
    @Gender NVARCHAR(10),
    @BloodGroup NVARCHAR(5),
    @PhoneNumber NVARCHAR(15),
    @Address NVARCHAR(200),
    @Email NVARCHAR(100),
    @PatientID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Patient (Name, DateOfBirth, Gender, BloodGroup, PhoneNumber, Address, Email)
    OUTPUT INSERTED.PatientID
    VALUES (@Name, @DateOfBirth, @Gender, @BloodGroup, @PhoneNumber, @Address, @Email);

    -- The output parameter @PatientID will hold the inserted PatientID
    SET @PatientID = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteAppointment]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[sp_DeleteAppointment]
    @AppointmentID INT
AS
BEGIN
    -- Delete the appointment record with the specified ID
    DELETE FROM Appointment
    WHERE AppointmentID = @AppointmentID;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllAppointments]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllAppointments]
AS
BEGIN
    -- Select all relevant fields from the Appointments table
    SELECT 
        AppointmentId,
        PatientID,
        DoctorID,
        AppointmentDate,
        AppointmentTime,
        TokenID
    FROM 
        Appointment
    ORDER BY 
        AppointmentDate ASC, AppointmentTime ASC;  -- Optional: Order by date and time
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAppointmentDetails]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetAppointmentDetails]
    @AppointmentID INT
AS
BEGIN
    SELECT *
    FROM Appointments
    WHERE AppointmentID = @AppointmentID;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetConsultationFee]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[sp_GetConsultationFee]
    @DoctorID INT
AS
BEGIN
    SELECT ConsultationFee 
    FROM Doctor 
    WHERE DoctorID = @DoctorID;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDoctorAvailability]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetDoctorAvailability]
    @DoctorID INT = NULL,
    @Date DATE = NULL,
    @Specialty NVARCHAR(50) = NULL
AS
BEGIN
    SELECT a.AvailabilityID, a.DoctorID, a.Date, a.TimeSlot
    FROM Availability a
    INNER JOIN Doctor d ON a.DoctorID = d.DoctorID
    WHERE (@DoctorID IS NULL OR a.DoctorID = @DoctorID)
      AND (@Date IS NULL OR a.Date = @Date)
    ORDER BY a.Date, a.TimeSlot;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLoginIDPassword]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetLoginIDPassword]
    @LoginID NVARCHAR(50),
    @Password NVARCHAR(50),
    @RoleId INT OUTPUT
AS
BEGIN
    SELECT @RoleId = RoleID
    FROM [Login]
    WHERE LoginID = @LoginID
        AND Password = @Password;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPatientByIDorPhoneNumber]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetPatientByIDorPhoneNumber]
    @PatientID NVARCHAR(50),
    @PhoneNumber NVARCHAR(15)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT PatientID, Name, DateOfBirth, PhoneNumber, Address, Gender, BloodGroup, Email
    FROM Patient
    WHERE PatientID = @PatientID OR PhoneNumber = @PhoneNumber;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetScheduledAppointmentsForToday]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetScheduledAppointmentsForToday]
    @DoctorID INT
AS
BEGIN
    SELECT a.*
    FROM Appointment a
    INNER JOIN Doctor d ON a.DoctorID = d.DoctorID
    INNER JOIN Login l ON d.StaffID = l.StaffID
    WHERE a.Status = 'scheduled'
      AND a.AppointmentDate = CAST(GETDATE() AS DATE)
      AND a.DoctorID = @DoctorID;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RescheduleAppointment]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RescheduleAppointment]
    @AppointmentID INT,
    @NewDate DATE = NULL,
    @NewTime TIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Appointment
    SET AppointmentDate = ISNULL(@NewDate, AppointmentDate),
        AppointmentTime = ISNULL(@NewTime, AppointmentTime)
    WHERE AppointmentID = @AppointmentID;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePatientDetails]    Script Date: 27-08-2024 14:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdatePatientDetails]
    @PatientID NVARCHAR(50),
    @Name NVARCHAR(100),
    @DateOfBirth DATE,
    @PhoneNumber NVARCHAR(15),
    @Address NVARCHAR(200),
    @Gender NVARCHAR(10),
    @BloodGroup NVARCHAR(5),
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Patient
    SET Name = @Name,
        DateOfBirth = @DateOfBirth,
        PhoneNumber = @PhoneNumber,
        Address = @Address,
        Gender = @Gender,
        BloodGroup = @BloodGroup,
        Email = @Email
    WHERE PatientID = @PatientID;
END;
GO
