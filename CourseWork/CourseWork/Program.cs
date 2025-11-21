using Hospital_BLL.Interfaces;
using Hospital_BLL.Models.Schedule;
using Hospital_DAL.SpecificRepos;
namespace Hospital_PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ... (Створення bootstrapper)
            var bootstrapper = new DependencyBootstrapper();

            // Отримуємо всі необхідні залежності
            var appointmentManager = bootstrapper.GetAppointmentManager();
            var doctorRepo = bootstrapper.GetDoctorRepository();
            var patientRepo = bootstrapper.GetPatientRepository();
            var scheduleRepo = bootstrapper.GetScheduleRepository(); // <<< НОВЕ: Отримання IScheduleRepository


            try
{
                // Використовуйте Guid'и, які ви знаєте, що існують у Doctor/Patient Repos, 
                // або які створюються у вашому SeedData.
                Guid existingDoctorId = new Guid("0bc78eb7-1bcd-4c05-9427-d65493f991db");
                Guid existingPatientId = new Guid("47209950-e654-4ed4-b018-3dbb781db831");

                var allAppointments = appointmentManager.GetAll().ToList();
                bool createdTestAppointment = false;

                // --- ЛОГІКА СТВОРЕННЯ, ЯКЩО НЕМАЄ ЖОДНОГО ЗАПИСУ ---
                if (!allAppointments.Any())
                {
                    Console.WriteLine("\n[DEBUG] Жодних Appointments не знайдено. Створення тестових...");
                    TimeSpan dsadasdas = TimeSpan.Zero;
                    // Перевіряємо, чи існують Doctor та Patient у сховищі
                    if (doctorRepo.GetById(existingDoctorId) != null && patientRepo.GetById(existingPatientId) != null)
                    {
                        // Створюємо новий об'єкт запису BLL (Appointment)
                        var newApp = new Hospital_BLL.Models.Schedule.Appointment(
                            existingDoctorId,
                            existingPatientId,
                            DateTime.Now.AddDays(5).AddHours(10), // Прийом через 5 днів о 10:00
                            dsadasdas
                        );

                        // Оскільки ми не викликаємо AppointmentManager.CreateAppointment (щоб уникнути перевірки слотів),
                        // ми додаємо запис напряму через DAL (тільки для цілей дебагу DAL).
                        appointmentManager.CreateAppointment(existingDoctorId,
                            existingPatientId,
                            DateTime.Now.AddDays(5).AddHours(10), // Прийом через 5 днів о 10:00
                            dsadasdas);
                        allAppointments.Add(newApp);
                        createdTestAppointment = true;
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Неможливо створити тестовий Appointment: Лікар або Пацієнт не існують у сховищі.");
                    }
                }
                // ---------------------------------------------------------------------

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n--- ТЕСТ DAL (APPOINTMENTS) ---");
                Console.WriteLine($"Завантажено всього Appointments: {allAppointments.Count}");

                if (allAppointments.Any())
                {
                    var firstApp = allAppointments.First();
                    Console.WriteLine($"  ID Першого запису: {firstApp.AppointmentId}");
                    Console.WriteLine($"  Час: {firstApp.StartTime}");
                    Console.WriteLine($"  Статус: {firstApp.Status}");
                }
                if (createdTestAppointment)
                {
                    Console.WriteLine("✅ Тестовий Appointment створено і збережено вперше.");
                }
                Console.ForegroundColor = ConsoleColor.White;

            }
catch (Exception ex)
{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n--- КРИТИЧНА ПОМИЛКА DAL (APPOINTMENT) ---");
                Console.WriteLine($"Помилка при читанні/запису Appointments: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            // Запустіть програму. Якщо виведеться "НІ" або "Помилка", є проблема.

            // 3. Запуск UI (тепер передаємо 4 залежності)
            //var ui = new ConsoleUI(doctorRepo, patientRepo, appointmentManager, _scheduleRepo); // <<< НОВЕ: Передача
            // ui.Start();
        }
    }
}