using System.Collections.Generic;

// container for storing appdata

namespace DAL.Entities
{    
    public class DataContainerEntity
    {
        public List<DoctorEntity> Doctors { get; set; } = new();
        public List<PatientEntity> Patients { get; set; } = new();

        // we will use this id to assing shared ids for doctors and patients in one file
        public int LastId { get; set; } = 0;
    }
}