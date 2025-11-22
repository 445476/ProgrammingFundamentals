using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hospital_BLL.Interfaces;
using Hospital_BLL.Models;
using Hospital_BLL.Models.Schedule;

namespace Hospital_Tests.Mocks
{
    public class TestRepository<T> : IRepository<T> where T : class
    {
        protected readonly List<T> Store = new List<T>();

        protected Guid GetItemId(T item)
        {
            return (Guid)item.GetType().GetProperty("Id")?.GetValue(item);
        }

        public T GetById(Guid id) => Store.FirstOrDefault(item => GetItemId(item) == id);

        public IEnumerable<T> GetAll() => Store;

        public void Add(T item)
        {
            if (Store.Any(i => GetItemId(i) == GetItemId(item)))
            {
                throw new InvalidOperationException("Item already exists.");
            }
            Store.Add(item);
        }

        public void Update(T item)
        {
            var id = GetItemId(item);
            var index = Store.FindIndex(i => GetItemId(i) == id);
            if (index != -1)
            {
                Store[index] = item;
            }
        }

        public void Delete(Guid id)
        {
            int initialCount = Store.Count;
            Store.RemoveAll(item => GetItemId(item) == id);

            if (Store.Count == initialCount)
            {
                throw new ArgumentException($"Item with ID {id} not found for deletion.");
            }
        }
    }

    public class DoctorRepositoryMock : TestRepository<Doctor>, IDoctorRepository
    {
        public IEnumerable<Doctor> FindByName(string name)
        {
            var normalizedName = name.Trim().ToLower();
            return Store.Where(d =>
                d.LastName.ToLower().Contains(normalizedName) ||
                d.FirstName.ToLower().Contains(normalizedName)
            );
        }

        public IEnumerable<Doctor> FindBySpecialization(string specialization)
        {
            var normalizedSpec = specialization.Trim().ToLower();
            return Store.Where(d => d.Specialization.ToLower().Contains(normalizedSpec));
        }
    }

    public class PatientRepositoryMock : TestRepository<Patient>, IPatientRepository
    {
        public IEnumerable<Patient> FindByName(string name)
        {
            var normalizedName = name.Trim().ToLower();
            return Store.Where(p =>
                p.LastName.ToLower().Contains(normalizedName) ||
                p.FirstName.ToLower().Contains(normalizedName)
            );
        }
    }

    public class ScheduleRepositoryMock : TestRepository<Schedule>, IScheduleRepository
    {
        public Schedule GetByDoctorId(Guid doctorId)
        {
            return Store.FirstOrDefault(s => s.DoctorId == doctorId);
        }
    }

    public class AppointmentRepositoryMock : TestRepository<Appointment>, IAppointmentRepository
    {
        public IEnumerable<Appointment> FindByDoctorId(Guid doctorId)
        {
            return Store.Where(a => a.DoctorId == doctorId);
        }

        public IEnumerable<Appointment> FindByPatientId(Guid patientId)
        {
            return Store.Where(a => a.PatientId == patientId);
        }

        public IEnumerable<Appointment> FindByDateRange(DateTime start, DateTime end)
        {
            return Store.Where(a => a.StartTime >= start && a.StartTime <= end);
        }
    }
}