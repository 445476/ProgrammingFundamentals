using Hospital_BLL.Interfaces;
using Hospital_BLL.Models.Schedule;
using Hospital_DAL.SpecificRepos; 

public class DependencyBootstrapper
{
    private readonly IDoctorRepository _doctorRepo = new DoctorRepository();
    private readonly IPatientRepository _patientRepo = new PatientRepository();
    private readonly IScheduleRepository _scheduleRepo = new ScheduleRepository();
    private readonly IAppointmentRepository _appointmentRepo = new AppointmentRepository();

    public AppointmentManager GetAppointmentManager()
    {
        return new AppointmentManager(_scheduleRepo, _appointmentRepo, _doctorRepo, _patientRepo);
    }
    public IDoctorRepository GetDoctorRepository() => _doctorRepo;
    public IPatientRepository GetPatientRepository() => _patientRepo;
    public IScheduleRepository GetScheduleRepository() => _scheduleRepo;
}