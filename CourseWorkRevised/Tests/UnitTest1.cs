using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Services;
using Xunit;
using Tests;

namespace Tests
{
    public class EntityServiceMockTests
    {
        private EntityService _service;

        public EntityServiceMockTests()
        {
            var mockProvider = TestMocks.GetMockJsonProvider();
            _service = new EntityService(mockProvider); 
        }

        [Fact]
        public void AddDoctor_Should_AddDoctor()
        {
            int id = _service.AddDoctor("Greg", "House", 50, "Diagnostics");

            var doctor = _service.GetAllDoctors().FirstOrDefault(d => d.Id == id);

            Assert.NotNull(doctor);
            Assert.Equal("Greg", doctor.FirstName);
            Assert.Equal("House", doctor.LastName);
            Assert.Equal("Diagnostics", doctor.Specialization);
        }

        [Fact]
        public void EditDoctor_Should_UpdateDoctor()
        {
            int id = _service.AddDoctor("Jane", "Smith", 35, "Neurology");

            _service.EditDoctor(id, "Janet", "Smithers", 36, "Pediatrics");

            var doctor = _service.GetAllDoctors().First(d => d.Id == id);

            Assert.Equal("Janet", doctor.FirstName);
            Assert.Equal("Smithers", doctor.LastName);
            Assert.Equal(36, doctor.Age);
            Assert.Equal("Pediatrics", doctor.Specialization);
        }

        [Fact]
        public void DeleteDoctor_Should_RemoveDoctor()
        {
            int id = _service.AddDoctor("Alice", "Brown", 50, "Dermatology");

            _service.DeleteDoctor(id);

            var doctor = _service.GetAllDoctors().FirstOrDefault(d => d.Id == id);
            Assert.Null(doctor);
        }

        [Fact]
        public void AddPatient_Should_AddPatient()
        {
            int id = _service.AddPatient("Bob", "Johnson", 28);

            var patient = _service.GetAllPatients().FirstOrDefault(p => p.Id == id);
            Assert.NotNull(patient);
            Assert.Equal("Bob", patient.FirstName);
            Assert.Equal("Johnson", patient.LastName);
            Assert.Equal(28, patient.Age);
        }

        [Fact]
        public void AddRecord_Should_AddRecordToPatient()
        {
            int patientId = 1;
            string description = "Testing";

            _service.AddPatientRecord(patientId, description);

            var records = _service.GetPatientRecords(patientId).ToList();

            Assert.Single(records);
            Assert.Equal(description, records[0].Description);
        }

        [Fact]
        public void GetDoctorScheduleByDate_Should_ReturnCorrectSchedule()
        {
            int doctorId = 1;

            var schedule = new List<ScheduleItem>
            {
                new ScheduleItem { Date = DateTime.Today, Time = new TimeSpan(10,0,0), IsBooked = false },
                new ScheduleItem { Date = DateTime.Today.AddDays(1), Time = new TimeSpan(11,0,0), IsBooked = false }
            };

            _service.EditDoctorSchedule(doctorId, schedule);

            var result = _service.GetDoctorScheduleByDate(doctorId, DateTime.Today).ToList();

            Assert.Single(result);
            Assert.Equal(new TimeSpan(10,0,0), result[0].Time);
        }

        [Fact]
        public void BookAppointment_Should_MarkSlotAsBooked()
        {
            int doctorId = 1;
            int patientId = 1;

            var schedule = new List<ScheduleItem>
            {
                new ScheduleItem { Date = DateTime.Today, Time = new TimeSpan(9,0,0), IsBooked = false }
            };
            _service.EditDoctorSchedule(doctorId, schedule);

            _service.BookAppointment(patientId, doctorId, DateTime.Today, new TimeSpan(9,0,0));

            var doctorSchedule = _service.GetDoctorScheduleByDate(doctorId, DateTime.Today);
            Assert.True(doctorSchedule[0].IsBooked);
        }

        [Fact]
        public void SearchDoctors_Should_FindCorrectResults()
        {
            var result = _service.SearchDoctors("Alice").ToList();
            Assert.Single(result);
            Assert.Equal("Alice", result[0].FirstName);

            var specResult = _service.SearchDoctors("Dermatology").ToList();
            Assert.Single(specResult);
            Assert.Equal("Bob", specResult[0].FirstName);
        }

        [Fact]
        public void SearchPatients_Should_FindCorrectResults()
        {
            var result = _service.SearchPatients("Two").ToList();
            Assert.Single(result);
            Assert.Equal("Patient2", result[0].FirstName);
        }

        [Fact]
        public void GetAllDoctors_Should_ReturnAllDoctors()
        {
            var doctors = _service.GetAllDoctors();
            Assert.True(doctors.Count() >= 3); 
        }

        [Fact]
        public void GetAllPatients_Should_ReturnAllPatients()
        {
            var patients = _service.GetAllPatients();
            Assert.True(patients.Count() >= 3);
        }
    }
}