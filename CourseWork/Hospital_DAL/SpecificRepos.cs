using System;
using System.Collections.Generic;
using System.Linq;
using Hospital_BLL.Interfaces; 
using Hospital_BLL.Models;
using Hospital_BLL.Models.Schedule;

namespace Hospital_DAL.SpecificRepos
{

    public class DoctorRepository : JsonRepository<Doctor>, IDoctorRepository 
    {
        public DoctorRepository() : base("doctors.json") { }

        public IEnumerable<Doctor> FindByName(string name)
        {
            string normalizedName = name.Trim().ToLower();
            return GetAll().Where(d => d.LastName.ToLower().Contains(normalizedName) || d.FirstName.ToLower().Contains(normalizedName));
        }

        public IEnumerable<Doctor> FindBySpecialization(string specialization)
        {
            string normalizedSpecialization = specialization.Trim().ToLower();
            return GetAll().Where(d => d.Specialization.ToLower().Contains(normalizedSpecialization));
        }
    }

    public class PatientRepository : JsonRepository<Patient>, IPatientRepository 
    {
        public PatientRepository() : base("patients.json") { }

        public IEnumerable<Patient> FindByName(string name)
        {
            string normalizedName = name.Trim().ToLower();
            return GetAll().Where(p => p.LastName.ToLower().Contains(normalizedName) || p.FirstName.ToLower().Contains(normalizedName));
        }
    }

    public class AppointmentRepository : JsonRepository<Appointment>, IAppointmentRepository 
    {
        public AppointmentRepository() : base("appointments.json") { }

        public IEnumerable<Appointment> FindByDoctorId(Guid doctorId)
        {
            return GetAll().Where(a => a.DoctorId == doctorId);
        }

        public IEnumerable<Appointment> FindByPatientId(Guid patientId)
        {
            return GetAll().Where(a => a.PatientId == patientId);
        }

        public IEnumerable<Appointment> FindByDateRange(DateTime start, DateTime end)
        {
            return GetAll().Where(a => a.StartTime >= start && a.StartTime <= end);
        }



    }

    public class ScheduleRepository : JsonRepository<Schedule>, IScheduleRepository 
    {
        public ScheduleRepository() : base("schedules.json") { }
        public Schedule GetByDoctorId(Guid doctorId)
        {
            return GetAll().FirstOrDefault(s => s.Id == doctorId);
        }
    }
}