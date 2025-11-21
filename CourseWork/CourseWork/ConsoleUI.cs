using System;
using System.Linq;
using System.Collections.Generic;
using Hospital_BLL.Interfaces;
using Hospital_BLL.Models.Medcard;
using Hospital_BLL.Models;
using Hospital_BLL.Models.Schedule;
using Hospital_DAL.SpecificRepos; // Used temporarily for SeedData in PL

namespace Hospital_PL
{
    public class ConsoleUI
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IPatientRepository _patientRepo;
        private readonly AppointmentManager _appointmentManager;
        private readonly IScheduleRepository _scheduleRepo; 

        public ConsoleUI(IDoctorRepository doctorRepo,
                         IPatientRepository patientRepo,
                         AppointmentManager appointmentManager,
                         IScheduleRepository scheduleRepo) 
        {
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
            _appointmentManager = appointmentManager;
            _scheduleRepo = scheduleRepo;
            SeedData();
        }

        public void Start()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=============================================");
                Console.WriteLine("= HOSPITAL REGISTRY SYSTEM =");
                Console.WriteLine("=============================================");
                Console.WriteLine("1. Doctor Menu (CRUD/Schedule)");
                Console.WriteLine("2. Patient Menu (CRUD/Card)");
                Console.WriteLine("3. Appointment Menu (Book/Cancel)");
                Console.WriteLine("0. Exit");
                Console.WriteLine("---------------------------------------------");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": HandleDoctorMenu(); break;
                        case "2": HandlePatientMenu(); break;
                        case "3": HandleAppointmentMenu(); break;
                        case "0": isRunning = false; break;
                        default: Console.WriteLine("Invalid choice. Try again."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nCRITICAL ERROR: {ex.Message}");
                }
                if (isRunning)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
            }
        }


        private void HandleDoctorMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- DOCTOR MENU ---");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. Delete Doctor");
                Console.WriteLine("3. Edit Doctor (Name/Specialty)");
                Console.WriteLine("4. Show Doctor Schedule");
                Console.WriteLine("5. Edit Doctor Schedule (Add/Remove Slots)");
                Console.WriteLine("6. Show All Doctors"); 
                Console.WriteLine("0. Back");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": AddDoctor(); break;
                        case "2": DeleteDoctor(); break;
                        case "3": EditDoctor(); break;
                        case "4": ShowDoctorSchedule(); break;
                        case "5": EditDoctorSchedule(); break;
                        case "6": ShowAllDoctors(); break; 
                        case "0": back = true; break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
                catch (ArgumentException ex) { Console.WriteLine($"\nVALIDATION ERROR: {ex.Message}"); }
                catch (InvalidOperationException ex) { Console.WriteLine($"\nBUSINESS LOGIC ERROR: {ex.Message}"); }
                catch (Exception ex) { Console.WriteLine($"\nERROR: {ex.Message}"); }

                if (!back)
                {
                    Console.WriteLine("\nPress Enter...");
                    Console.ReadLine();
                }
            }
        }

        private void ShowAllDoctors()
        {
            var doctors = _doctorRepo.GetAll().ToList();
            if (!doctors.Any()) { Console.WriteLine("Doctor list is empty."); return; }
            Console.WriteLine("\n--- ALL DOCTORS ---");
            foreach (var d in doctors)
            {
                Console.WriteLine($"ID: {d.Id} | {d.LastName}, {d.FirstName} | Specialization: {d.Specialization}");
            }
        }

        private void AddDoctor()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Specialization: ");
            string spec = Console.ReadLine();

            // Assume gender is not strictly required for this core operation
            var newDoctor = new Doctor(firstName, lastName, false, spec);
            _doctorRepo.Add(newDoctor);
            Console.WriteLine($"✅ Doctor {newDoctor.LastName} added successfully. ID: {newDoctor.Id}");
        }

        private void DeleteDoctor()
        {
            ShowAllDoctors();
            Console.Write("Enter Doctor ID to delete: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id)) { Console.WriteLine("Invalid ID format."); return; }

            _doctorRepo.Delete(id);
            Console.WriteLine($"✅ Doctor with ID {id} successfully deleted.");
        }

        private void EditDoctor()
        {
            ShowAllDoctors();
            Console.Write("Enter Doctor ID to edit: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id)) { Console.WriteLine("Invalid ID format."); return; }

            var doctor = _doctorRepo.GetById(id);
            if (doctor == null) { Console.WriteLine("Doctor not found."); return; }

            // Edit logic
            Console.Write($"Current First Name: {doctor.FirstName}. New (or Enter): ");
            string newFirstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newFirstName)) doctor.FirstName = newFirstName;

            Console.Write($"Current Last Name: {doctor.LastName}. New (or Enter): ");
            string newLastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLastName)) doctor.LastName = newLastName;

            Console.Write($"Current Specialization: {doctor.Specialization}. New (or Enter): ");
            string newSpec = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSpec)) doctor.Specialization = newSpec;

            _doctorRepo.Update(doctor);
            Console.WriteLine($"Doctor's card for {doctor.LastName} updated.");
        }

        private void ShowDoctorSchedule()
        {
            ShowAllDoctors();
            Console.Write("Enter Doctor ID to view schedule: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid doctorId)) return;

            var scheduleRepo = new ScheduleRepository();
            var schedule = scheduleRepo.GetByDoctorId(doctorId);

            Console.WriteLine($"\n--- SCHEDULE FOR DOCTOR {doctorId} ---");

            if (schedule == null || !schedule.Slots.Any())
            {
                Console.WriteLine("No schedule defined for this doctor.");
                return;
            }

            foreach (var slot in schedule.Slots.OrderBy(s => s.StartTime))
            {
                string status = slot.IsBooked ? $"BOOKED (Appt ID: {slot.AppointmentId})" : "AVAILABLE";
                Console.WriteLine($"- {slot.StartTime:yyyy-MM-dd HH:mm} to {slot.EndTime:HH:mm} | Status: {status}");
            }
        }

        private void EditDoctorSchedule()
        {
            ShowAllDoctors();
            Console.Write("Enter Doctor ID to edit schedule: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid doctorId))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            if (_doctorRepo.GetById(doctorId) == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            var schedule = _scheduleRepo.GetByDoctorId(doctorId);
            bool isNewSchedule = (schedule == null);

            if (isNewSchedule)
            {
                schedule = new Schedule(doctorId);
                Console.WriteLine("\nNOTE: Creating a new schedule for this doctor.");
            }

            Console.WriteLine("\n--- EDIT SCHEDULE ---");
            Console.WriteLine("1. Add Slot(s)");
            Console.WriteLine("2. Remove Slot (by Slot ID)");
            Console.WriteLine("0. Back");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            try
            {
                if (choice == "1")
                {
                    AddSlotsToSchedule(schedule);
                }
                else if (choice == "2")
                {
                    RemoveSlotFromSchedule(schedule);
                }
                else if (choice == "0")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    return;
                }

                if (isNewSchedule)
                {
                    _scheduleRepo.Add(schedule);
                    Console.WriteLine("New Schedule created and saved.");
                }
                else
                {
                    _scheduleRepo.Update(schedule);
                    Console.WriteLine("Schedule updated successfully.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n INPUT ERROR: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"BUSINESS LOGIC ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nCRITICAL ERROR: {ex.Message}");
            }
        }

        private void AddSlotsToSchedule(Schedule schedule)
        {
            Console.Write("Enter Slot Start Date/Time (e.g., 2025-12-05 09:00:00): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDateTime))
            {
                Console.WriteLine("Invalid start date/time format.");
                return;
            }

            Console.Write("Enter Slot Duration in minutes (e.g., 30): ");
            if (!int.TryParse(Console.ReadLine(), out int durationMinutes) || durationMinutes <= 0)
            {
                Console.WriteLine("Invalid duration.");
                return;
            }

            TimeSpan slotDuration = TimeSpan.FromMinutes(durationMinutes);

            
            schedule.AddSlot(startDateTime, slotDuration);
            Console.WriteLine("Slot added to memory. Saving will happen next.");
        }

        private void RemoveSlotFromSchedule(Schedule schedule)
        {
            if (!schedule.Slots.Any())
            {
                Console.WriteLine("No slots defined in the current schedule.");
                return;
            }

            Console.WriteLine("\n--- CURRENT SLOTS ---");
            foreach (var slot in schedule.Slots.OrderBy(s => s.StartTime))
            {
                string status = slot.IsBooked ? $"BOOKED (Appt ID: {slot.AppointmentId})" : "AVAILABLE";
                Console.WriteLine($"ID: {slot.Id} | Time: {slot.StartTime:yyyy-MM-dd HH:mm} | Status: {status}");
            }

            Console.Write("Enter Slot ID to remove: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid slotId))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            schedule.RemoveSlot(slotId);
            Console.WriteLine("Slot removed from memory. Saving will happen next.");
        }


        private void HandlePatientMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- PATIENT MENU ---");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Delete Patient");
                Console.WriteLine("3. Edit Patient (Name)");
                Console.WriteLine("4. Show Patient Card");
                Console.WriteLine("5. Add Entry to Medical Card");
                Console.WriteLine("6. Show All Patients"); // <<< ДОДАНО
                Console.WriteLine("0. Back");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": AddPatient(); break;
                        case "2": DeletePatient(); break;
                        case "3": EditPatient(); break;
                        case "4": ShowPatientCard(); break;
                        case "5": AddEntryToPatientCard(); break;
                        case "6": ShowAllPatients(); break; // <<< ВИКЛИК
                        case "0": back = true; break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
                catch (ArgumentException ex) { Console.WriteLine($"\nVALIDATION ERROR: {ex.Message}"); }
                catch (Exception ex) { Console.WriteLine($"\nERROR: {ex.Message}"); }

                if (!back)
                {
                    Console.WriteLine("\nPress Enter...");
                    Console.ReadLine();
                }
            }
        }

        private void ShowAllPatients()
        {
            var patients = _patientRepo.GetAll().ToList();
            if (!patients.Any()) { Console.WriteLine("Patient list is empty."); return; }
            Console.WriteLine("\n--- ALL PATIENTS ---");
            foreach (var p in patients)
            {
                Console.WriteLine($"ID: {p.Id} | {p.LastName}, {p.FirstName} | DOB: {p.DateOfBirth:yyyy-MM-dd}");
            }
        }

        private void AddPatient()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Is Female? (true/false): ");
            bool isFemale = bool.TryParse(Console.ReadLine(), out var result) && result;
            Console.Write("Enter Date of Birth (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dob)) { Console.WriteLine("Invalid Date format."); return; }

            var newPatient = new Patient(firstName, lastName, isFemale, dob);
            _patientRepo.Add(newPatient);
            Console.WriteLine($"✅ Patient {newPatient.LastName} added successfully. ID: {newPatient.Id}");
        }

        private void DeletePatient()
        {
            ShowAllPatients();
            Console.Write("Enter Patient ID to delete: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id)) { Console.WriteLine("Invalid ID format."); return; }

            _patientRepo.Delete(id);
            Console.WriteLine($"✅ Patient with ID {id} successfully deleted.");
        }

        private void EditPatient()
        {
            ShowAllPatients();
            Console.Write("Enter Patient ID to edit: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id)) return;

            var patient = _patientRepo.GetById(id);
            if (patient == null) { Console.WriteLine("Patient not found."); return; }

            Console.Write($"Current First Name: {patient.FirstName}. New (or Enter): ");
            string newFirstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newFirstName)) patient.FirstName = newFirstName;

            Console.Write($"Current Last Name: {patient.LastName}. New (or Enter): ");
            string newLastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLastName)) patient.LastName = newLastName;

            _patientRepo.Update(patient);
            Console.WriteLine($"✅ Patient's card for {patient.LastName} updated.");
        }

        private void ShowPatientCard()
        {
            ShowAllPatients();
            Console.Write("Enter Patient ID to view card: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id)) { Console.WriteLine("Invalid ID format."); return; }

            var patient = _patientRepo.GetById(id);
            if (patient == null) { Console.WriteLine("Patient not found."); return; }

            Console.WriteLine("\n--- PATIENT CARD ---");
            Console.WriteLine($"ID: {patient.Id}");
            Console.WriteLine($"Name: {patient.FirstName} {patient.LastName}");
            Console.WriteLine($"Date of Birth: {patient.DateOfBirth:yyyy-MM-dd}");

            Console.WriteLine("\n--- MEDICAL ENTRIES ---");
            if (patient.MedicalCard.Entries.Any())
            {
                foreach (var entry in patient.MedicalCard.Entries.OrderByDescending(e => e.Date))
                {
                    Console.WriteLine($"- Date: {entry.Date:yyyy-MM-dd HH:mm} | Diagnosis: {entry.Diagnosis}");
                    Console.WriteLine($"  Notes: {entry.Notes}");
                }
            }
            else
            {
                Console.WriteLine("No medical history entries on record.");
            }
        }

        private void AddEntryToPatientCard()
        {
            ShowAllPatients();
            Console.Write("Enter Patient ID to add entry: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id)) return;

            var patient = _patientRepo.GetById(id);
            if (patient == null) { Console.WriteLine("Patient not found."); return; }

            Console.Write("Enter Diagnosis: ");
            string diagnosis = Console.ReadLine();
            Console.Write("Enter Notes/Prescription: ");
            string notes = Console.ReadLine();

            var newEntry = new Entry(Guid.NewGuid(), DateTime.Now, diagnosis, notes);

          
            patient.MedicalCard.AddEntry(newEntry);

  
            _patientRepo.Update(patient);

            Console.WriteLine($"✅ New entry added to {patient.LastName}'s card.");
        }

        private void HandleAppointmentMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- APPOINTMENT MENU ---");
                Console.WriteLine("1. Create New Appointment (Book Appointment)");
                Console.WriteLine("2. Cancel Appointment");
                Console.WriteLine("3. View Appointments by Doctor");
                Console.WriteLine("4. View Appointments by Patient Name");
                Console.WriteLine("0. Back");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": CreateAppointment(); break;
                        case "2": CancelAppointment(); break;
                        case "3": ShowDoctorAppointments(); break;
                        case "4": ShowPatientAppointmentsByName(); break;
                        case "0": back = true; break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nINPUT ERROR: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"BUSINESS LOGIC ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nCRITICAL ERROR: {ex.Message}");
                }

                if (!back)
                {
                    Console.WriteLine("\nPress Enter...");
                    Console.ReadLine();
                }
            }
        }

        private void CreateAppointment()
        {
            ShowAllDoctors();
            Console.Write("Enter Doctor ID: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid doctorId)) return;

            Console.WriteLine("\n--- Available Patients ---");
            _patientRepo.GetAll().ToList().ForEach(p => Console.WriteLine($"ID: {p.Id} | {p.LastName} {p.FirstName}"));
            Console.Write("Enter Patient ID: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid patientId)) return;

            Console.Write("Enter Appointment Date/Time (e.g., 2025-12-01 10:00:00): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
            {
                Console.WriteLine("Invalid date/time format.");
                return;
            }

            TimeSpan duration = new TimeSpan(0, 30, 0); // Assuming 30 minutes duration


            Appointment newApp = _appointmentManager.CreateAppointment(doctorId, patientId, startTime, duration);
            Console.WriteLine($"\nSuccess! Appointment created: {newApp.Id}. Time: {newApp.StartTime}");
        }

        private void CancelAppointment()
        {
            Console.Write("Enter Appointment ID to cancel: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid appId)) return;

            _appointmentManager.CancelAppointment(appId);
            Console.WriteLine($"\nAppointment {appId} successfully cancelled.");
        }

        private void ShowDoctorAppointments()
        {
            
            ShowAllDoctors();
            Console.Write("Enter Doctor ID: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid doctorId)) return;

         
            var appointments = _appointmentManager.GetAppointmentsByDoctor(doctorId);

            Console.WriteLine($"\nAPPOINTMENTS FOR DOCTOR {doctorId}");
            if (appointments.Any())
            {
                foreach (var a in appointments.OrderBy(a => a.StartTime))
                {
                    Console.WriteLine($"[ID: {a.Id}] Time: {a.StartTime} | Patient ID: {a.PatientId} | Status: {a.Status}");
                }
            }
            else
            {
                Console.WriteLine("No appointments found.");
            }
        }

        private void ShowPatientAppointmentsByName()
        {
            Console.Write("Enter Patient Last Name or First Name: ");
            string name = Console.ReadLine();

 
            var appointments = _appointmentManager.GetAppointmentsByPatientName(name);

            Console.WriteLine($"\n--- SEARCH RESULTS FOR '{name}' ---");
            if (appointments.Any())
            {
                foreach (var a in appointments.OrderBy(a => a.StartTime))
                {

                    var doctor = _doctorRepo.GetById(a.DoctorId);
                    string doctorName = doctor != null ? $"{doctor.LastName}, {doctor.FirstName}" : "Unknown Doctor";

                    Console.WriteLine($"[ID: {a.Id}] Time: {a.StartTime} | Doctor: {doctorName} | Status: {a.Status}");
                }
            }
            else
            {
                Console.WriteLine("No appointments found for this patient name.");
            }
        }


        private void SeedData()
        {
            var scheduleRepo = new ScheduleRepository();
            if (!_doctorRepo.GetAll().Any())
            {
                var d1 = new Doctor("John", "Smith", false, "Cardiology");
                var p1 = new Patient("Emma", "Brown", true, new DateTime(1995, 5, 15));

                _doctorRepo.Add(d1);
                _patientRepo.Add(p1);

                // Create Schedule for John Smith
                var s1 = new Schedule(d1.Id);
                s1.AddSlot(new DateTime(2025, 12, 1, 10, 0, 0), new TimeSpan(0, 30, 0));
                scheduleRepo.Add(s1);
            }
        }
    }
}