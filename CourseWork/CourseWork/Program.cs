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


            

            // Запустіть програму. Якщо виведеться "НІ" або "Помилка", є проблема.

            // 3. Запуск UI (тепер передаємо 4 залежності)
            var ui = new ConsoleUI(doctorRepo, patientRepo, appointmentManager, scheduleRepo); // <<< НОВЕ: Передача
             ui.Start();
        }
    }
}