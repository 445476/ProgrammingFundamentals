using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hospital_BLL.Managers;
using Hospital_BLL.Models;
using Hospital_Tests.Mocks; 

namespace Hospital_Tests.ManagersTests
{
    [TestClass]
    public class DoctorManagerTests
    {
        private DoctorManager _manager;
        private DoctorRepositoryMock _doctorRepo;

        [TestInitialize]
        public void Setup()
        {
            _doctorRepo = new DoctorRepositoryMock();
            _manager = new DoctorManager(_doctorRepo, new ScheduleRepositoryMock());
        }

        [TestMethod]
        public void CreateDoctor_ValidData_ShouldAddDoctorToRepo()
        {
            // ARRANGE
            string firstName = "Іван";
            string lastName = "Коваленко";
            string specialization = "Хірург";

            // ACT
            Doctor newDoctor = _manager.CreateDoctor(firstName, lastName, true, specialization);

            // ASSERT
            Assert.IsNotNull(newDoctor);
            Assert.AreEqual(lastName, newDoctor.LastName);
            Assert.AreEqual(1, _doctorRepo.GetAll().Count(), "Repo should contain exactly one doctor.");
        }

        [TestMethod]
        public void DeleteDoctor_ExistingId_ShouldRemoveDoctorFromRepo()
        {
            // ARRANGE: Створення лікаря для видалення
            Doctor doctor = _manager.CreateDoctor("Петро", "Іванов", true, "Терапевт");
            Guid doctorId = doctor.Id;

            // ACT
            _manager.DeleteDoctor(doctorId);

            // ASSERT
            Assert.IsNull(_doctorRepo.GetById(doctorId), "Doctor should be null after deletion.");
            Assert.AreEqual(0, _doctorRepo.GetAll().Count(), "Repo should contain zero doctors.");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteDoctor_NonExistingId_ShouldThrowException()
        {
            // ACT: Спроба видалення неіснуючого ID
            _manager.DeleteDoctor(Guid.NewGuid());

            // ASSERT: Очікуємо ArgumentException
        }
    }
}