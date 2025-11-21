using Hospital_BLL.Models;
using Hospital_BLL.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_BLL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(Guid id);
    }

    public interface IScheduleRepository : IRepository<Schedule>
    {
        Schedule GetByDoctorId(Guid doctorId);
    }

    public interface IDoctorRepository : IRepository<Doctor>
    {
        IEnumerable<Doctor> FindByName(string name);
        
        IEnumerable<Doctor> FindBySpecialization(string specialization);
    }

    public interface IPatientRepository : IRepository<Patient>
    {
        IEnumerable<Patient> FindByName(string name);
    }

    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IEnumerable<Appointment> FindByDoctorId(Guid doctorId);
        IEnumerable<Appointment> FindByPatientId(Guid patientId);
        IEnumerable<Appointment> FindByDateRange(DateTime start, DateTime end);
        
    }
}

