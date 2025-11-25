using System;
using System.Collections.Generic;
using BLL.Services;
using BLL.Entities;

namespace PL
{
    public class DoctorMenu
    {
        private readonly EntityService _service;

        public DoctorMenu(EntityService service)
        {
            _service = service;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== DOCTOR MENU ====");
                Console.WriteLine("1. Add doctor");
                Console.WriteLine("2. Edit doctor");
                Console.WriteLine("3. Delete doctor");
                Console.WriteLine("4. View all doctors");
                Console.WriteLine("5. Edit schedule");
                Console.WriteLine("6. View schedule by date");
                Console.WriteLine("0. Back");

                int choice = Helper.ReadInt("Choose: ");

                switch (choice)
                {
                    case 1: AddDoctor(); break;
                    case 2: EditDoctor(); break;
                    case 3: DeleteDoctor(); break;
                    case 4: ShowDoctors(); break;
                    case 5: EditSchedule(); break;
                    case 6: ViewScheduleByDate(); break;
                    case 0: return;
                    default: Console.WriteLine("Invalid option."); Console.ReadKey(); break;
                }
            }
        }

        private void AddDoctor()
        {
            string f = Helper.ReadString("First name: ");
            string l = Helper.ReadString("Last name: ");
            int age = Helper.ReadInt("Age: ");
            string spec = Helper.ReadString("Specialization: ");

            int id = _service.AddDoctor(f, l, age, spec);
            Console.WriteLine($"Doctor added with ID: {id}");
            Console.ReadKey();
        }

        private void EditDoctor()
        {
            int id = Helper.ReadInt("Doctor ID: ");
            string f = Helper.ReadString("New first name: ");
            string l = Helper.ReadString("New last name: ");
            int age = Helper.ReadInt("New age: ");
            string spec = Helper.ReadString("New specialization: ");

            try
            {
                _service.EditDoctor(id, f, l, age, spec);
                Console.WriteLine("Doctor updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private void DeleteDoctor()
        {
            int id = Helper.ReadInt("Doctor ID: ");
            try
            {
                _service.DeleteDoctor(id);
                Console.WriteLine("Doctor deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private void ShowDoctors()
        {
            var list = _service.GetAllDoctors();
            foreach (var d in list)
                Console.WriteLine($"{d.Id}: {d.FirstName} {d.LastName} — {d.Specialization}");
            Console.ReadKey();
        }

        private void EditSchedule()
        {
            int id = Helper.ReadInt("Doctor ID: ");
            var newSchedule = new List<ScheduleItem>();

            Console.WriteLine("Enter schedule items. Leave date empty to finish.");
            while (true)
            {
                Console.Write("Date (yyyy-mm-dd or empty): ");
                string? s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s)) break;

                if (!DateTime.TryParse(s, out var date))
                {
                    Console.WriteLine("Invalid date.");
                    continue;
                }

                var time = Helper.ReadTime("Time: ");
                newSchedule.Add(new ScheduleItem { Date = date, Time = time, IsBooked = false });
            }

            try
            {
                _service.EditDoctorSchedule(id, newSchedule);
                Console.WriteLine("Schedule updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
        private void ViewScheduleByDate()
        {
            int id = Helper.ReadInt("Doctor ID: ");
            DateTime date = Helper.ReadDate("Date: ");

            try
            {
                var schedule = _service.GetDoctorScheduleByDate(id, date);
                if (schedule.Count == 0)
                    Console.WriteLine("No schedule for this day.");
                else
                {
                    Console.WriteLine($"Schedule for {date:yyyy-MM-dd}:");
                    foreach (var s in schedule)
                        Console.WriteLine($"{s.Time:hh\\:mm} - {(s.IsBooked ? "Booked" : "Available")}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}