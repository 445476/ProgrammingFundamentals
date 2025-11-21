using Hospital_BLL.Interfaces;
using Hospital_BLL.Models;
using Hospital_BLL.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital_BLL.Models.Schedule
{
    public class AppointmentManager
    {
        [JsonInclude] 
        private readonly IScheduleRepository _scheduleRepository;
        [JsonInclude] 
        private readonly IAppointmentRepository _appointmentRepository;
        [JsonInclude] 
        private readonly IDoctorRepository _doctorRepository;
        [JsonInclude] 
        private readonly IPatientRepository _patientRepository;

        //creating dependencies
        public AppointmentManager(IScheduleRepository scheduleRepo,
                                  IAppointmentRepository appointmentRepo,
                                  IDoctorRepository doctorRepo,
                                  IPatientRepository patientRepo)
        {
            _scheduleRepository = scheduleRepo ?? throw new ArgumentNullException(nameof(scheduleRepo));
            _appointmentRepository = appointmentRepo ?? throw new ArgumentNullException(nameof(appointmentRepo));
            _doctorRepository = doctorRepo ?? throw new ArgumentNullException(nameof(doctorRepo));
            _patientRepository = patientRepo ?? throw new ArgumentNullException(nameof(patientRepo));
        }

        public IEnumerable<Appointment> GetAll()
        {
            // BLL делегує запит GetAll() до IAppointmentRepository (DAL)
            return _appointmentRepository.GetAll();
        }
        public Appointment CreateAppointment(Guid doctorId, Guid patientId, DateTime startTime, TimeSpan duration)
        {
            // checking if entities exist
            if (_doctorRepository.GetById(doctorId) == null)
                throw new ArgumentException($"Doctor with ID {doctorId} not found.", nameof(doctorId));
            if (_patientRepository.GetById(patientId) == null)
                throw new ArgumentException($"Patient with ID {patientId} not found.", nameof(patientId));

            // booking slot
            Schedule doctorSchedule = _scheduleRepository.GetByDoctorId(doctorId);
            if (doctorSchedule == null)
                throw new InvalidOperationException($"No schedule found for Doctor ID {doctorId}.");

            ScheduleSlot slotToBook = doctorSchedule.Slots.FirstOrDefault(s =>
                s.StartTime == startTime &&
                s.EndTime == startTime.Add(duration) &&
                !s.IsBooked
            );

            if (slotToBook == null)
            {
                throw new InvalidOperationException("Requested time slot is unavailable or duration mismatch.");
            }

            var newAppointment = new Appointment(doctorId, patientId, startTime, duration);
            slotToBook.Book(newAppointment.Id);

            _appointmentRepository.Add(newAppointment);
            _scheduleRepository.Update(doctorSchedule);

            return newAppointment;
        }

        public void CancelAppointment(Guid appointmentId)
        {
            Appointment appointmentToCancel = _appointmentRepository.GetById(appointmentId);
            if (appointmentToCancel == null)
                throw new ArgumentException($"Appointment with ID {appointmentId} not found.", nameof(appointmentId));

            appointmentToCancel.Cancel();

            Schedule doctorSchedule = _scheduleRepository.GetByDoctorId(appointmentToCancel.DoctorId);

            if (doctorSchedule != null)
            {
                ScheduleSlot bookedSlot = doctorSchedule.Slots.FirstOrDefault(s =>
                    s.AppointmentId == appointmentToCancel.Id
                );

                if (bookedSlot != null)
                {
                    bookedSlot.CancelBooking();
                    _scheduleRepository.Update(doctorSchedule);
                }
            }

            _appointmentRepository.Update(appointmentToCancel);
        }

        //searches

        public IEnumerable<Doctor> GetDoctorsByName(string name)
        {
            return _doctorRepository.FindByName(name);
        }

        public IEnumerable<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            return _doctorRepository.FindBySpecialization(specialization);
        }

        public IEnumerable<Appointment> GetAppointmentsByPatientName(string patientName)
        {
            IEnumerable<Patient> matchingPatients = _patientRepository.FindByName(patientName);

            if (!matchingPatients.Any())
            {
                return Enumerable.Empty<Appointment>();
            }

            var patientIds = matchingPatients.Select(p => p.Id).ToList();

            var results = new List<Appointment>();
            foreach (var id in patientIds)
            {
                results.AddRange(_appointmentRepository.FindByPatientId(id));
            }

            return results.Distinct();
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctor(Guid doctorId)
        {
            if (_doctorRepository.GetById(doctorId) == null)
                throw new ArgumentException($"Doctor with ID {doctorId} not found.", nameof(doctorId));
            return _appointmentRepository.FindByDoctorId(doctorId);
        }
    }
}