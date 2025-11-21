using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hospital_BLL.Managers;
using Hospital_BLL.Models.Schedule;
using Hospital_Tests.Mocks; 

namespace Hospital_Tests.ManagersTests
{
    [TestClass]
    public class ScheduleManagerTests
    {
        private ScheduleManager _manager;
        private ScheduleRepositoryMock _scheduleRepo;
        private DoctorRepositoryMock _doctorRepo;
        private Guid _doctorId;
        private DateTime _dayToAdd = new DateTime(2026, 12, 10); // Майбутній день
        private TimeSpan _slotDuration = TimeSpan.FromMinutes(20);

        [TestInitialize]
        public void Setup()
        {
            _scheduleRepo = new ScheduleRepositoryMock();
            _doctorRepo = new DoctorRepositoryMock();
            
            // DoctorManager залежить від DoctorRepository, ScheduleManager - ні, 
            // але ми створюємо ID лікаря, щоб симулювати його наявність
            _doctorRepo.Add(new Hospital_BLL.Models.Doctor("Тест", "Лікар", true, "Окуліст"));
            _doctorId = _doctorRepo.GetAll().First().Id;
            
            _manager = new ScheduleManager(_scheduleRepo);
        }

        [TestMethod]
        public void AddWorkingDay_NewDoctor_ShouldCreateScheduleAndAddSlots()
        {
            // ACT
            _manager.AddWorkingDay(_doctorId, _dayToAdd, TimeSpan.FromHours(9), TimeSpan.FromHours(17), _slotDuration);

            // ASSERT
            Schedule schedule = _scheduleRepo.GetByDoctorId(_doctorId);
            Assert.IsNotNull(schedule, "Schedule object should be created.");
            
            int expectedSlots = (int)((TimeSpan.FromHours(17) - TimeSpan.FromHours(9)).TotalMinutes / _slotDuration.TotalMinutes);
            
            Assert.AreEqual(expectedSlots, schedule.Slots.Count, "Incorrect number of slots created.");
            Assert.IsTrue(schedule.Slots.All(s => s.StartTime.Date == _dayToAdd.Date), "All slots must be on the specified day.");
        }
        
        [TestMethod]
        public void DeleteWorkingDay_ExistingDay_ShouldRemoveAllSlotsForThatDay()
        {
            // ARRANGE: Додаємо робочий день
            _manager.AddWorkingDay(_doctorId, _dayToAdd, TimeSpan.FromHours(9), TimeSpan.FromHours(12), _slotDuration);
            Assert.IsTrue(_scheduleRepo.GetByDoctorId(_doctorId).Slots.Count > 0);

            // ACT: Видалення робочого дня
            _manager.DeleteWorkingDay(_doctorId, _dayToAdd);

            // ASSERT
            Schedule schedule = _scheduleRepo.GetByDoctorId(_doctorId);
            Assert.AreEqual(0, schedule.Slots.Count, "All slots for the specified day should be removed.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddWorkingDay_OverlappingDay_ShouldThrowException()
        {
            // ARRANGE: Додаємо день вперше
            _manager.AddWorkingDay(_doctorId, _dayToAdd, TimeSpan.FromHours(9), TimeSpan.FromHours(17), _slotDuration);

            // ACT: Спроба додати той самий день ще раз
            _manager.AddWorkingDay(_doctorId, _dayToAdd, TimeSpan.FromHours(9), TimeSpan.FromHours(17), _slotDuration);
            
            // ASSERT: Очікуємо InvalidOperationException (запобігання дублюванню)
        }
    }
}