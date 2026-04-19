
using System.Collections.Generic;

namespace BLL.Entities
{
    /// <summary>
    /// Business logic layer patient model.
    /// </summary>
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Age { get; set; }

        public List<PatientRecord> Records { get; set; } = new();
    }
}