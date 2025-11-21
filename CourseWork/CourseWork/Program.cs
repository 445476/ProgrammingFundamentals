using Hospital_BLL.Interfaces;
using Hospital_BLL.Models.Schedule;
using Hospital_DAL.SpecificRepos;
namespace Hospital_PL
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var bootstrapper = new DependencyBootstrapper();

            var appointmentManager = bootstrapper.GetAppointmentManager();
            var doctorRepo = bootstrapper.GetDoctorRepository();
            var patientRepo = bootstrapper.GetPatientRepository();
            var scheduleRepo = bootstrapper.GetScheduleRepository(); 


            var ui = new ConsoleUI(doctorRepo, patientRepo, appointmentManager, scheduleRepo); 
             ui.Start();
        }
    }
}