using BLL.Entities;
using DAL.Entities;

// Mapper for coummunicating between DAL and BLL, converts objecrts to entities and vice versa

namespace BLL.Mappers
{

    public static class EntityMapper
    {
        //doctor to entrity
        public static DoctorEntity ToDal(Doctor d)
        {
            return new DoctorEntity
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Age = d.Age,
                Specialization = d.Specialization,
                Schedule = d.Schedule.ConvertAll(x => new ScheduleItemEntity
                {
                    Date = x.Date,
                    Time = x.Time,
                    IsBooked = x.IsBooked
                })
            };
        }

        // entity to doctor
        public static Doctor ToBll(DoctorEntity d)
        {
            return new Doctor
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Age = d.Age,
                Specialization = d.Specialization,
                Schedule = d.Schedule.ConvertAll(x => new ScheduleItem
                {
                    Date = x.Date,
                    Time = x.Time,
                    IsBooked = x.IsBooked
                })
            };
        }

        // patient to entity
        public static PatientEntity ToDal(Patient p)
        {
            return new PatientEntity
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Records = p.Records.ConvertAll(x => new PatientRecordEntity
                {
                    Date = x.Date,
                    Description = x.Description
                })
            };
        }

        // entity to patient
        public static Patient ToBll(PatientEntity p)
        {
            return new Patient
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Records = p.Records.ConvertAll(x => new PatientRecord
                {
                    Date = x.Date,
                    Description = x.Description
                })
            };
        }
    }
}