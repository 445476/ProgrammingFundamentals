using System.Collections.Generic;

namespace BLL.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Age { get; set; }
        public string Specialization { get; set; } = "";
        public List<ScheduleItem> Schedule { get; set; } = new();
    }
}