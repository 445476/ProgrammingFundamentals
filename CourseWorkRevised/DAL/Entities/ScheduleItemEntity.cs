using System;

// item for scheduling

namespace DAL.Entities
{

    public class ScheduleItemEntity
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsBooked { get; set; }
    }
}