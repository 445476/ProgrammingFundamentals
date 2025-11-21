using Hospital_BLL.Models.Medcard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital_BLL.Models
{
    public class Patient : Person
    {
        [JsonInclude] 
        private DateTime _dob;
        [JsonInclude]
        public DateTime DateOfBirth { get { return _dob;} set
            {
                if (value > DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException(nameof(DateOfBirth), "Date of Birth cannot be in the future.");
                } 
                _dob = value;
            }
        }
        [JsonInclude] 
        public MedicalCard MedicalCard { get; set; }

        public Patient() { }

        public Patient(string firstName, string lastName, bool isFemale, DateTime dob) : base (firstName,lastName, isFemale)
        {
            DateOfBirth = dob;
            MedicalCard = new MedicalCard(this.Id);
        }
        public override string ToString() => $"{LastName} {FirstName} ({DateOfBirth:yyyy-MM-dd})";
    }
}
