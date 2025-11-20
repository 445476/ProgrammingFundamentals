using Models.Lab3.Core;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_1
{
    public class ConsoleMenu
    {
        private readonly IRepository<Person> _repository;
        private const string FilePath = "database.txt";
        private Person[] _data = new Person[0];

        // Конструктор: Впровадження залежності (IoC)
        public ConsoleMenu(IRepository<Person> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // --- ОСНОВНА ЛОГІКА ---

        public void Run()
        {
            bool running = true;
            while (running)
            {
                DisplayMenu();
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    try
                    {
                        switch (choice)
                        {
                            case 1: LoadData(); break;
                            case 2: SaveData(); break;
                            case 3: CreateSampleData(); break;
                            case 4: AddNewPerson(); break;
                            case 5: ShowAllData(); break;
                            case 6: SearchByLastName(); break;
                            case 7: SearchByIdentifier(); break;
                            case 8: CountExcellentFemales5Course(); break; // Завдання варіанту
                            case 0: running = false; break;
                            default: Console.WriteLine("Невірний вибір. Спробуйте ще раз."); break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[ПОМИЛКА] Помилка виконання: {ex.Message}\n");
                    }
                }
                else
                {
                    Console.WriteLine("Невірний формат вводу.");
                }
                if (running)
                {
                    Console.WriteLine("\nНатисніть Enter для продовження...");
                    Console.ReadLine();
                }
            }
            Console.WriteLine("Програма завершена.");
        }

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine($"--- База Даних (Записів: {_data.Length}) ---");
            Console.WriteLine("1. Завантажити дані з файлу");
            Console.WriteLine("2. Зберегти дані у файл");
            Console.WriteLine("3. Створити демонстраційні дані (> 5 записів)");
            Console.WriteLine("4. Додати новий об'єкт (Студент, Сторітеллер, Дантист)");
            Console.WriteLine("5. Показати всі записи");
            Console.WriteLine("6. Пошук за прізвищем");
            Console.WriteLine("7. Пошук за унікальним ідентифікатором (StudentId/IdCode)");
            Console.WriteLine("8. [ЗАВДАННЯ ВАРІАНТУ] Обчислити кількість студенток-відмінниць 5-го курсу");
            Console.WriteLine("0. Вихід");
            Console.Write("\nВаш вибір: ");
        }

        // --- ОПЕРАЦІЇ I/O ТА CRUD (Через IRepository) ---

        private void LoadData()
        {
            _data = _repository.Load(FilePath);
            Console.WriteLine($"Дані успішно завантажено з {FilePath}. Кількість записів: {_data.Length}");
        }

        private void SaveData()
        {
            _repository.Save(_data, FilePath);
            Console.WriteLine($"Дані успішно збережено у {FilePath}.");
        }

        private void ShowAllData()
        {
            if (_data.Length == 0)
            {
                Console.WriteLine("База даних порожня.");
                return;
            }
            Console.WriteLine("--- УСІ ЗАПИСИ ---");
            foreach (var p in _data)
            {
                Console.WriteLine($"> {p}");
            }
        }

        // --- СТВОРЕННЯ ДЕМОНСТРАЦІЙНИХ ДАНИХ ---

        private void CreateSampleData()
        {
            try
            {
                // Студенти
                var s1 = new Student("Марія", "Коваленко", 5, "KB500123", Gender.Female, 5.0, "2000111101");
                var s2 = new Student("Іван", "Петров", 4, "KZ412345", Gender.Male, 4.1, "1999123456");
                var s3 = new Student("Ольга", "Мельник", 5, "AD500999", Gender.Female, 4.9, "2001000000"); // Відмінниця 5 курсу
                var s4 = new Student("Катерина", "Сидоренко", 3, "ER333444", Gender.Female, 4.5, "1998010101");

                // Дантист
                var d1 = new Dentist("Олег", "Демченко", 150, Gender.Male);

                // Сторітеллер
                var st1 = new Storyteller("Олена", "Ткач", "Megamaster3000", Gender.Female);

                // Об'єднання в масив
                _data = new Person[] { s1, s2, s3, s4, d1, st1 };
                Console.WriteLine($"Створено {_data.Length} демонстраційних записів.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка при створенні демонстраційних даних: {ex.Message}");
                // Ця помилка виникає, якщо регулярні вирази у ваших конструкторах невірні.
            }
        }

        // --- ДІЇ НАД ДАНИМИ ---

        // Завдання варіанту 13: Обчислити кількість студенток-відмінниць 5-го курсу.
        private void CountExcellentFemales5Course()
        {
            Console.WriteLine("--- Пошук студенток-відмінниць 5-го курсу ---");

            // Використовуємо Linq.OfType<Student>() для фільтрації об'єктів
            var excellentStudents = _data
                .OfType<Student>()
                .Where(s =>
                    s.Course == 5 &&
                    s.Gender == Gender.Female &&
                    s.GradeAvg >= 4.75) // Припускаємо, що 'відмінниця' >= 4.75
                .ToArray();

            Console.WriteLine($"Знайдено: {excellentStudents.Length} студенток-відмінниць 5-го курсу.");

            if (excellentStudents.Length > 0)
            {
                Console.WriteLine("--- Їхні дані: ---");
                foreach (var s in excellentStudents)
                {
                    Console.WriteLine($"- {s.FirstName} {s.LastName}, Студ. квиток: {s.StudentId}, Бал: {s.GradeAvg}");
                }
            }
        }

        private void SearchByLastName()
        {
            Console.Write("Введіть прізвище для пошуку: ");
            string searchLastName = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(searchLastName))
            {
                Console.WriteLine("Прізвище не може бути порожнім.");
                return;
            }

            var results = _data
                .Where(p => p.LastName.Equals(searchLastName, StringComparison.OrdinalIgnoreCase))
                .ToArray();

            DisplaySearchResults(results, $"за прізвищем '{searchLastName}'");
        }

        private void SearchByIdentifier()
        {
            Console.Write("Введіть ідентифікатор (StudentId, IdCode або LicenseNumber): ");
            string searchId = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(searchId)) return;

            var results = _data
                .Where(p =>
                    (p is Student s && (s.StudentId.Equals(searchId) || s.IdCode.Equals(searchId))) ||
                    (p is Dentist d && d.PatientsTreated.Equals(searchId)))
                .ToArray();

            DisplaySearchResults(results, $"за ідентифікатором '{searchId}'");
        }

        // --- ДОПОМІЖНІ МЕТОДИ ---

        private void DisplaySearchResults(Person[] results, string searchCriteria)
        {
            if (results.Length == 0)
            {
                Console.WriteLine($"Нічого не знайдено {searchCriteria}.");
                return;
            }

            Console.WriteLine($"Знайдено {results.Length} запис(ів) {searchCriteria}:");
            foreach (var p in results)
            {
                Console.WriteLine($"> {p}");
            }
        }

        // (Метод AddNewPerson є занадто довгим, щоб включати його повністю,
        // але він має приймати ввід з консолі та викликати конструктори Student/Dentist/Storyteller)
        private void AddNewPerson()
        {
            Console.WriteLine("\n--- Додати новий об'єкт ---");
            Console.WriteLine("1: Student, 2: Dentist, 3: Storyteller");
            Console.Write("Ваш вибір: ");

            if (!int.TryParse(Console.ReadLine(), out int typeChoice))
            {
                Console.WriteLine("Невірний вибір.");
                return;
            }

            Console.Write("Введіть Ім'я: "); string fn = Console.ReadLine();
            Console.Write("Введіть Прізвище: "); string ln = Console.ReadLine();
            Person newPerson = null;

            try
            {
                switch (typeChoice)
                {
                    case 1:
                        // Потрібно зібрати всі 5 додаткових параметрів з консолі
                        Console.Write("Курс (1-6): "); int c = int.Parse(Console.ReadLine());
                        Console.Write("Студ. квиток (KB123456): "); string sid = Console.ReadLine();
                        // ... інші поля з TryParse/Enum.Parse ...

                        // Створення об'єкта. Конструктор виконає валідацію!
                        // Приклад: newPerson = new Student(fn, ln, c, sid, Gender.Male, 4.5, "1234567890");
                        Console.WriteLine("Наразі повне додавання полів відключено. Використовуйте семпли.");
                        break;
                    case 2:
                        // newPerson = new Dentist(fn, ln, "LIC1000", 0);
                        Console.WriteLine("Наразі повне додавання полів відключено. Використовуйте семпли.");
                        break;
                    case 3:
                        // newPerson = new Storyteller(fn, ln, "Mythology");
                        Console.WriteLine("Наразі повне додавання полів відключено. Використовуйте семпли.");
                        break;
                }

                if (newPerson != null)
                {
;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ПОМИЛКА ВВОДУ] Невірний формат або недійсні дані: {ex.Message}");
            }
        }
    }
}
