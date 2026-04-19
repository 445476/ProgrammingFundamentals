using System;
using BLL.Services;

//main menu was done in 3 parts to increase readability

namespace PL
{
    public class MainMenu
    {
        private readonly EntityService _service;

        public MainMenu(EntityService service)
        {
            _service = service;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("1. Doctors");
                Console.WriteLine("2. Patients");
                Console.WriteLine("3. Search");
                Console.WriteLine("0. Exit");

                int choice = Helper.ReadInt("Choose option: ");

                switch (choice)
                {
                    case 1:
                        new DoctorMenu(_service).Show();
                        break;
                    case 2:
                        new PatientMenu(_service).Show();
                        break;
                    case 3:
                        ShowSearchMenu();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowSearchMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Search doctor");
                Console.WriteLine("1. Search doctor");
                Console.WriteLine("2. Search patient");
                Console.WriteLine("0. Back");

                int choice = Helper.ReadInt("Choose: ");

                switch (choice)
                {
                    case 1:
                        string docTerm = Helper.ReadString("Enter name or specialization: ");
                        var docList = _service.SearchDoctors(docTerm);
                        foreach (var d in docList)
                            Console.WriteLine($"{d.Id}: {d.FirstName} {d.LastName} ({d.Specialization})");
                        Console.ReadKey();
                        break;

                    case 2:
                        string patTerm = Helper.ReadString("Enter name: ");
                        var patList = _service.SearchPatients(patTerm);
                        foreach (var p in patList)
                            Console.WriteLine($"{p.Id}: {p.FirstName} {p.LastName}");
                        Console.ReadKey();
                        break;

                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}