//using Hospital_BLL.Models;
//using Hospital_BLL.Models.Schedule;
//using Hospital_DAL.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//
//namespace Hospital_BLL.Interfaces
//{
//    public interface IDoctorRepository : IRepository<Doctor> {
//        IEnumerable<Doctor> FindByName(string name);
//        IEnumerable<Doctor> FindBySpecialization(string specialization);
//    }
//    public interface IPatientRepository : IRepository<Patient> { }
//    public interface IAppointmentRepository : IRepository<Appointment> { }
//
//
//    public interface IScheduleRepository : IRepository<Schedule>
//    {
//        Schedule GetByDoctorId(Guid doctorId);
//    }
//}
