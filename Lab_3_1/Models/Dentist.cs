using Models.Lab3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Dentist : Person, ITreat
    {
        public int PatientsTreated { get; private set; }


        public Dentist(string firstName, string lastName, int patientsTreated, Gender gender)
            : base(firstName, lastName, gender)
        {
            PatientsTreated = patientsTreated;
        }

        public override void Study()
        {
            // dentist upps his qualification
        }

        public override void Cook()
        {
            //lets assume dentist just cooks
        }

        public void Treat()
        {
            PatientsTreated++;
        }

        public override string ToPersistenceString()
        {
            //calling base method
            string baseData = base.ToPersistenceString();

            //adding specific fields
            string objData = $"\"patientsTreated\": \"{PatientsTreated}\"\n"; 
                               
            return baseData + objData;
        }
        public override string ToString()
        {
            return $"Dentist {FirstName} {LastName}, Patients treated: {PatientsTreated}";
        }
    }
}
