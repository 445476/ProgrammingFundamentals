using System.Collections.Generic;
using DAL.Entities;
using DAL.Json;
using DAL;

//mock DP to isolate tests from DAL

namespace Tests
{
     public static class TestMocks
    {
        public static JsonDataProvider GetMockJsonProvider()
        {
            return new JsonDataProviderMock();
        }

        // Internal mock provider class
        private class JsonDataProviderMock : JsonDataProvider
        {
            public List<DoctorEntity> Doctors { get; set; } = new List<DoctorEntity>
            {
                new DoctorEntity { Id = 1, FirstName = "John", LastName = "Doe", Age = 40, Specialization = "Cardiology" },
                new DoctorEntity { Id = 2, FirstName = "Alice", LastName = "Smith", Age = 35, Specialization = "Neurology" },
                new DoctorEntity { Id = 3, FirstName = "Bob", LastName = "Brown", Age = 50, Specialization = "Dermatology" }
            };

            public List<PatientEntity> Patients { get; set; } = new List<PatientEntity>
            {
                new PatientEntity { Id = 1, FirstName = "Patient1", LastName = "One", Age = 25 },
                new PatientEntity { Id = 2, FirstName = "Patient2", LastName = "Two", Age = 30 },
                new PatientEntity { Id = 3, FirstName = "Patient3", LastName = "Three", Age = 28 }
            };

            // Returns a DTO that matches what EntityService expects
            public override object Load()
            {
                return new
                {
                    LastId = 3,
                    Doctors = this.Doctors,
                    Patients = this.Patients
                };
            }

            // Save methods simply store data in memory
            public void SaveDoctors(List<DoctorEntity> doctors) => Doctors = doctors;
            public void SavePatients(List<PatientEntity> patients) => Patients = patients;
        }
    }
}
