using System;
using System.Linq;
using System.Collections.Generic;
using BLL.Collections;
using BLL.Entities;
using BLL.Exceptions;
using BLL.Mappers;
using DAL.Json;
using DAL.Entities;

// here lies all BL for project. my preciousssss

namespace BLL.Services
{
    public class EntityService
    {
        private readonly JsonDataProvider _provider = new JsonDataProvider("data.json");

        private readonly GenericCollection<Doctor> _doctors = new();
        private readonly GenericCollection<Patient> _patients = new();

        private int _lastId;
        public EntityService()
        {
            _provider = new JsonDataProvider();
        }

        public EntityService(JsonDataProvider provider)
        {
        _provider = provider ?? new JsonDataProvider();
        var data = _provider.Load();

            _lastId = data.LastId;

            // searching docs and adding them to collection
            foreach (var doc in data.Doctors)
            {
                var d = EntityMapper.ToBll(doc);
                _doctors.Add(d.Id, d);
            }

            // same with patients
            foreach (var pat in data.Patients)
            {
                var p = EntityMapper.ToBll(pat);
                _patients.Add(p.Id, p);
            }
        }

        //getting new id of last object
        private int GetNextId() => ++_lastId;

        private void SaveChanges()
        {
            var container = new DataContainerEntity
            {
                LastId = _lastId,
                //converting doctors to entities, getting all of the docs and converting to entities and to list
                Doctors = _doctors.GetAll()
                    .Select(x => EntityMapper.ToDal(x.Value))
                    .ToList(),
                //same with patients
                Patients = _patients.GetAll()
                    .Select(x => EntityMapper.ToDal(x.Value))
                    .ToList()
            };

            _provider.Save(container);
        }

        // bll for doctors
        public int AddDoctor(string first, string last, int age, string specialization)
        {
            var doctor = new Doctor
            {
                Id = GetNextId(),
                FirstName = first,
                LastName = last,
                Age = age,
                Specialization = specialization
            };

            _doctors.Add(doctor.Id, doctor);
            SaveChanges();

            return doctor.Id;
        }

        public void EditDoctor(int id, string first, string last, int age, string specialization)
        {
            var doc = _doctors.Get(id) ?? throw new BlException("Doctor not found.");

            doc.FirstName = first;
            doc.LastName = last;
            doc.Age = age;
            doc.Specialization = specialization;

            SaveChanges();
        }

        public void DeleteDoctor(int id)
        {
            if (!_doctors.Remove(id))
                throw new BlException("Doctor not found.");

            SaveChanges();
        }

        public IEnumerable<Doctor> GetAllDoctors() =>
            _doctors.GetAll().Select(x => x.Value);

        
        //search docs by name or spec
        public IEnumerable<Doctor> SearchDoctors(string term)
        {
            term = term.ToLower();
            return GetAllDoctors().Where(d =>
                d.FirstName.ToLower().Contains(term) ||
                d.LastName.ToLower().Contains(term) ||
                d.Specialization.ToLower().Contains(term));
        }

        //get doctorschedule for certain date
        public IEnumerable<ScheduleItem> GetDoctorSchedule(int id, DateTime date)
        {
            var doc = _doctors.Get(id) ?? throw new BlException("Doctor not found.");
            return doc.Schedule.Where(s => s.Date.Date == date.Date);
        }

        public List<ScheduleItem> GetDoctorScheduleByDate(int doctorId, DateTime date)
        {
            var doctor = _doctors.Get(doctorId); 
            if (doctor == null)
                throw new Exception("Doctor not found.");

            return doctor.Schedule
                         .Where(s => s.Date.Date == date.Date)
                         .ToList();
        }

        //change doctrs schedule. CHANGES ALL THE SCHEDULE
        //TODO: add ability to change specific entry of the schedule
        public void EditDoctorSchedule(int id, List<ScheduleItem> schedule)
        {
            var d = _doctors.Get(id) ?? throw new BlException("Doctor not found.");
            d.Schedule = schedule;
            SaveChanges();
        }

        // bll for patients

        public int AddPatient(string first, string last, int age)
        {
            var pat = new Patient
            {
                Id = GetNextId(),
                FirstName = first,
                LastName = last,
                Age = age
            };

            _patients.Add(pat.Id, pat);
            SaveChanges();

            return pat.Id;
        }

        public void EditPatient(int id, string first, string last, int age)
        {
            var p = _patients.Get(id) ?? throw new BlException("Patient not found.");

            p.FirstName = first;
            p.LastName = last;
            p.Age = age;

            SaveChanges();
        }

        public void DeletePatient(int id)
        {
            if (!_patients.Remove(id))
                throw new BlException("Patient not found.");

            SaveChanges();
        }

        public IEnumerable<Patient> GetAllPatients() =>
            _patients.GetAll().Select(x => x.Value);

        public IEnumerable<Patient> SearchPatients(string term)
        {
            term = term.ToLower();
            return GetAllPatients().Where(p =>
                p.FirstName.ToLower().Contains(term) ||
                p.LastName.ToLower().Contains(term));
        }

        public void AddPatientRecord(int patientId, string description)
        {
            var p = _patients.Get(patientId) ?? throw new BlException("Patient not found.");
            p.Records.Add(new PatientRecord
            {
                Date = DateTime.Now,
                Description = description
            });

            SaveChanges();
        }

        public IEnumerable<PatientRecord> GetPatientRecords(int id)
        {
            var p = _patients.Get(id) ?? throw new BlException("Patient not found.");
            return p.Records;
        }

    // TODO: make easier appointment system, constantly using date and time sucks
        public void BookAppointment(int patientId, int doctorId, DateTime date, TimeSpan time)
        {
            var p = _patients.Get(patientId) ?? throw new BlException("Patient not found.");
            var d = _doctors.Get(doctorId) ?? throw new BlException("Doctor not found.");

            var slot = d.Schedule.FirstOrDefault(s =>
                s.Date.Date == date.Date &&
                s.Time == time);

            if (slot == null)
                throw new BlException("Doctor does not have such a time slot.");

            if (slot.IsBooked)
                throw new BlException("This appointment is already booked.");

            slot.IsBooked = true;

            p.Records.Add(new PatientRecord
            {
                Date = date,
                Description = $"Visit scheduled to Dr. {d.LastName} at {time}"
            });

            SaveChanges();
        }
    }
}