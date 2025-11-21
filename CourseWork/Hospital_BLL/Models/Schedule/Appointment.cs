using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital_BLL.Models.Schedule
{
    public class Appointment
    {
        [JsonInclude] 
        public Guid Id { get; private set; } = Guid.NewGuid();
        [JsonInclude] 
        public Guid DoctorId { get; private set; }
        [JsonInclude] 
        public Guid PatientId { get; private set; }
        
        [JsonInclude]
        public DateTime StartTime { get; private set; }
        

        public enum AppointmentStatus
        {
            Scheduled, 
            Cancelled, 
            Completed  
        }

        public AppointmentStatus Status { get; private set; }
        public Appointment() { }
        public Appointment(Guid doctorId, Guid patientId, DateTime startTime, TimeSpan duration)
        {
            if (doctorId == Guid.Empty)
                throw new ArgumentException("Doctor ID must be specified.");
            if (patientId == Guid.Empty)
                throw new ArgumentException("Patient ID must be specified.");
            if (startTime <= DateTime.Now.AddMinutes(5)) // Прийом не може бути в минулому або найближчі хвилини
                throw new ArgumentException("Appointment time must be in the future.");

            DoctorId = doctorId;
            PatientId = patientId;
            StartTime = startTime;

        }

        public void Cancel()
        {
            if (this.Status == AppointmentStatus.Completed)
                throw new InvalidOperationException("Cannot cancel a completed appointment.");

            this.Status = AppointmentStatus.Cancelled;
        }
    }

}
