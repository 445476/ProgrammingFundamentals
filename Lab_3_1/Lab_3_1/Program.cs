using IOLib;
using Lab_3_1;
using Models.Lab3.Core;
using Models;

static void Main(string[] args)
{
    // 1. Впровадження залежності (Dependency Injection)
    // Створюємо конкретну реалізацію (FileRepository)
    // Змінна оголошена як інтерфейс (IRepository<Person>),
    // що забезпечує слабке зв'язування.
    IRepository<Person> repository = new FileRepository();

    // 2. Створення UI-класу
    // Передаємо реалізацію в ConsoleMenu через інтерфейс.
    // ConsoleMenu не знає, чи працює він з файлами, базою даних, чи чимось іншим.
    ConsoleMenu menu = new ConsoleMenu(repository);

    Console.Title = "ЛР №3: Файловий I/O та ООП (Варіант 13)";
    Console.WriteLine("Лабораторна робота №3: Файловий I/O та ООП.");

    // 3. Запуск основного циклу програми
    menu.DisplayMenu();
    menu.Run();
}