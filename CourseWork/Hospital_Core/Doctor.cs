using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hospital_BLL.Models
{
    public class Doctor : Person
    {
        private string _specialization;

        private const string SpecRegEx = @"^[A-Za-z\- ]+$";
        public string Specialization
        {
            get => _specialization;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Specialization), "Specialization cannot be empty.");

                if (!Regex.IsMatch(value, SpecRegEx))
                {
                    throw new ArgumentException("Specialization contains invalid characters.", nameof(Specialization));
                }
                _specialization = value; 
            }
        }

        public Doctor() { }

        public Doctor(string firstName, string lastName, bool isFemale, string specialization) : base(firstName,lastName, isFemale) 
        {
            Specialization = specialization;
        }

        public override string ToString() => $"{LastName} {FirstName} — {Specialization}";
    }
}
