using System.Collections.Generic;


//doctor entity to keep dal isolated


namespace DAL.Entities
{
    public class DoctorEntity
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Age { get; set; }
        public string Specialization { get; set; } = "";

        public List<ScheduleItemEntity> Schedule { get; set; } = new();
    }
}
