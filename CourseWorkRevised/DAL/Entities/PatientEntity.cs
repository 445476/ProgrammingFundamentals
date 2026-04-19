
using System.Collections.Generic;

//patient entity to keep DAL isolated

namespace DAL.Entities
{
    public class PatientEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Age { get; set; }

        public List<PatientRecordEntity> Records { get; set; } = new();
    }
}