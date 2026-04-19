using System;

namespace BLL.Entities
{
    public class ScheduleItem
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsBooked { get; set; }
    }
}