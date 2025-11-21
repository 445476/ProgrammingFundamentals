using Models.Lab3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Storyteller : Person, ITell
    {
        public string Speciality { get; private set; }

        public Storyteller(string firstName, string lastName, string speciality, Gender gender)
            : base(firstName, lastName, gender)
        {
            Speciality = speciality.ToLower();
        }

        public override void Study()
        {
            Speciality = "Storyteller";
            //relearns his speciality
        }

        public override void Cook()
        {
            //just cooks
        }

        public void Tell()
        {
            Speciality = "Master" + Speciality;
            //becomes mastermastermaster...masterStoryteller
        }

        public override string ToPersistenceString()
        {
            //calling base method
            string baseData = base.ToPersistenceString();

            //adding specific fields
            string objData = $"\"speciality\": \"{Speciality}\"\n";

            return baseData + objData;
        }

        public override string ToString()
        {
            return $"Storyteller {FirstName} {LastName}, Speciality={Speciality}";
        }
    }
}

