using System;
using System.Collections.Generic;
using BLL.Services;
using BLL.Entities;

namespace PL
{
    public class PatientMenu
    {
        private readonly EntityService _service;

        public PatientMenu(EntityService service)
        {
            _service = service;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== PATIENT MENU ====");
                Console.WriteLine("1. Add patient");
                Console.WriteLine("2. Edit patient");
                Console.WriteLine("3. Delete patient");
                Console.WriteLine("4. View all patients");
                Console.WriteLine("5. View patient card");
                Console.WriteLine("6. Book appointment");
                Console.WriteLine("7. Add record to patient card");
                Console.WriteLine("0. Back");

                int choice = Helper.ReadInt("Choose: ");

                switch (choice)
                {
                    case 1: AddPatient(); break;
                    case 2: EditPatient(); break;
                    case 3: DeletePatient(); break;
                    case 4: ShowPatients(); break;
                    case 5: ShowPatientCard(); break;
                    case 6: BookAppointment(); break;
                    case 7: AddPatientRecord(); break;
                    case 0: return;
                    default: Console.WriteLine("Invalid option."); Console.ReadKey(); break;
                }
            }
        }

        private void AddPatient()
        {
            string f = Helper.ReadString("First name: ");
            string l = Helper.ReadString("Last name: ");
            int age = Helper.ReadInt("Age: ");

            int id = _service.AddPatient(f, l, age);
            Console.WriteLine($"Patient added with ID: {id}");
            Console.ReadKey();
        }

        private void EditPatient()
        {
            int id = Helper.ReadInt("Patient ID: ");
            string f = Helper.ReadString("New first name: ");
            string l = Helper.ReadString("New last name: ");
            int age = Helper.ReadInt("New age: ");

            try
            {
                _service.EditPatient(id, f, l, age);
                Console.WriteLine("Patient updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private void DeletePatient()
        {
            int id = Helper.ReadInt("Patient ID: ");
            try
            {
                _service.DeletePatient(id);
                Console.WriteLine("Patient deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private void ShowPatients()
        {
            var list = _service.GetAllPatients();
            foreach (var p in list)
                Console.WriteLine($"{p.Id}: {p.FirstName} {p.LastName}");
            Console.ReadKey();
        }

        private void ShowPatientCard()
        {
            int id = Helper.ReadInt("Patient ID: ");
            try
            {
                var records = _service.GetPatientRecords(id);
                Console.WriteLine("==== Patient Card ====");
                foreach (var r in records)
                    Console.WriteLine($"{r.Date:yyyy-MM-dd}: {r.Description}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private void BookAppointment()
        {
            int patientId = Helper.ReadInt("Patient ID: ");
            int doctorId = Helper.ReadInt("Doctor ID: ");
            DateTime date = Helper.ReadDate("Date: ");
            TimeSpan time = Helper.ReadTime("Time: ");

            try
            {
                _service.BookAppointment(patientId, doctorId, date, time);
                Console.WriteLine("Appointment booked.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private void AddPatientRecord()
        {
            int patientId = Helper.ReadInt("Patient ID: ");
            string description = Helper.ReadString("Description: ");

            try
            {
                _service.AddPatientRecord(patientId, description);
                Console.WriteLine("Record added to patient card.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
