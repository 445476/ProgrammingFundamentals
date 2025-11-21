using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hospital_BLL.Managers;
using Hospital_BLL.Models;
using Hospital_Tests.Mocks; 

namespace Hospital_Tests.ManagersTests
{
    [TestClass]
    public class PatientManagerTests
    {
        private PatientManager _manager;
        private PatientRepositoryMock _patientRepo;
        private Guid _patientId;

        [TestInitialize]
        public void Setup()
        {
            _patientRepo = new PatientRepositoryMock();
            _manager = new PatientManager(_patientRepo);
            
            // Створюємо пацієнта для використання в інших тестах
            Patient patient = _manager.CreatePatient("Olha", "Melnyk", false, new DateTime(1995, 5, 15));
            _patientId = patient.Id;
        }

        [TestMethod]
        public void CreatePatient_ValidData_ShouldAddPatientToRepo()
        {
            // ARRANGE: Пацієнт вже створений у Setup, перевіряємо, що він є
            
            // ACT (немає)

            // ASSERT
            Assert.IsNotNull(_patientRepo.GetById(_patientId));
            Assert.AreEqual(1, _patientRepo.GetAll().Count());
        }

        [TestMethod]
        public void DeletePatient_ExistingId_ShouldRemovePatientFromRepo()
        {
            // ARRANGE: Створюємо другого пацієнта для видалення
            Patient patientToDelete = _manager.CreatePatient("Iryna", "Sydorenko", false, new DateTime(1980, 1, 1));
            
            // ACT
            _manager.DeletePatient(patientToDelete.Id);

            // ASSERT
            Assert.IsNull(_patientRepo.GetById(patientToDelete.Id), "Patient should be null after deletion.");
            Assert.AreEqual(1, _patientRepo.GetAll().Count(), "Only the initial patient should remain.");
        }

        [TestMethod]
        public void AddMedicalRecord_ValidData_ShouldAddEntryToPatientCard()
        {
            // ARRANGE
            string diagnosis = "Грип";
            string treatment = "Парацетамол";

            // ACT
            _manager.AddMedicalRecord(_patientId, diagnosis, treatment);

            // ASSERT
            Patient updatedPatient = _patientRepo.GetById(_patientId);
            Assert.AreEqual(1, updatedPatient.MedicalCard.Records.Count(), "Medical card should contain one record.");
            
            MedicalRecord record = updatedPatient.MedicalCard.Records.First();
            Assert.AreEqual(diagnosis, record.Diagnosis);
            Assert.AreEqual(treatment, record.Treatment);
            Assert.IsTrue(record.Date.Date == DateTime.Now.Date, "Record date should be today.");
        }
    }
}