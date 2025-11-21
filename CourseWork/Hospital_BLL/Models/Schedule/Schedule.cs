using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital_BLL.Models.Schedule
{
    public class Schedule
    {
        [JsonInclude] 
        private Guid _doctorId;
        [JsonInclude] 
        public Guid Id { get; private set; } = Guid.NewGuid();
        [JsonInclude]
        public Guid DoctorId
        {
            get { return _doctorId; }
            private set { _doctorId = value; }
        }
        [JsonInclude] 
        public List<ScheduleSlot> Slots { get; private set; } = new List<ScheduleSlot>();

        public Schedule() { }

        public Schedule(Guid doctorId)
        {
            if (doctorId == Guid.Empty)
                throw new ArgumentException("Doctor ID must be provided when creating a schedule.", nameof(doctorId));

            DoctorId = doctorId;
        }

        public void AddSlot(DateTime startTime, TimeSpan duration)
        {
            var newSlot = new ScheduleSlot(startTime, duration);

            bool overlaps = this.Slots.Any(existingSlot =>
                newSlot.StartTime < existingSlot.EndTime &&
                newSlot.EndTime > existingSlot.StartTime
            );

            if (overlaps)
            {
                throw new InvalidOperationException("New slot time overlaps with an existing slot in the schedule.");
            }

            this.Slots.Add(newSlot);
        }

        public void RemoveSlot(Guid slotId)
        {
            if (slotId == Guid.Empty)
            {
                throw new ArgumentException("Slot ID cannot be empty.", nameof(slotId));
            }

            var slotToRemove = this.Slots.FirstOrDefault(s => s.Id == slotId);

            if (slotToRemove == null)
            {
                throw new ArgumentException($"Schedule slot with ID {slotId} not found in this schedule.", nameof(slotId));
            }

            if (slotToRemove.IsBooked)
            {
                throw new InvalidOperationException(
                    $"Cannot remove slot {slotId}. It is already booked by appointment ID {slotToRemove.AppointmentId}."
                );
            }

            this.Slots.Remove(slotToRemove);
        }
    }
}
