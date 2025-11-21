using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital_BLL.Models.Schedule
{
    public class ScheduleSlot 
    {
        [JsonInclude] 
        public Guid Id { get; private set; } = Guid.NewGuid();
        [JsonInclude] 
        public DateTime StartTime { get; private set; }
        [JsonInclude] 
        public DateTime EndTime { get; private set; }

        // important flag
        [JsonInclude] 
        public bool IsBooked { get; private set; }

        // reference to appointment, empty if no appointment
        [JsonInclude] 
        public Guid? AppointmentId { get; private set; }

        public ScheduleSlot() { }
        public ScheduleSlot(DateTime startTime, TimeSpan duration)
        {
            if (duration.TotalMinutes <= 0)
                throw new ArgumentException("Slot duration must be positive.", nameof(duration));

            if (startTime < DateTime.Now.AddMinutes(-1))
                throw new ArgumentException("Slot cannot be scheduled in the past.", nameof(startTime));

            StartTime = startTime;
            EndTime = startTime.Add(duration);
            IsBooked = false;
            AppointmentId = null;
        }



        // booking
        public void Book(Guid appointmentId)
        {
            if (IsBooked)
                throw new InvalidOperationException("This slot is already booked.");

            if (appointmentId == Guid.Empty)
                throw new ArgumentException("Appointment ID cannot be empty when booking.", nameof(appointmentId));

            IsBooked = true;
            AppointmentId = appointmentId;
        }

        // canselling booking
        public void CancelBooking()
        {
            IsBooked = false;
            AppointmentId = null;
        }
    }
}
