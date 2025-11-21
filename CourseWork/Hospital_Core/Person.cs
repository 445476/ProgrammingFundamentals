using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hospital_BLL.Models
{
    public abstract class Person 
    {
        private string _firstName;
        private string _lastName;
        
        public Guid Id { get; set; } 
        public string FirstName { 
            get 
            {
                return _firstName; 
            } 
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(FirstName));

                string namePattern = @"^[A-Za-z\-]+$";

                if (!Regex.IsMatch(value, namePattern))
                {
                    throw new ArgumentException("Name contains invalid characters.", nameof(FirstName));
                }
                _firstName = value;
            }
        }
        public string LastName {
            get
            {
                return _lastName;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(LastName));

                string namePattern = @"^[A-Za-z\-]+$";

                if (!Regex.IsMatch(value, namePattern))
                {
                    throw new ArgumentException("Name contains invalid characters.", nameof(LastName));
                }
                _lastName = value;
            }
        }
        public bool IsFemale { get; set; }


        public Person() { }
        protected Person(string firstName, string lastName, bool isFemale)
        {
            FirstName = firstName;
            LastName = lastName;           
            IsFemale = isFemale;
            this.Id = Guid.NewGuid();
        }
        public override string ToString()
        {
            return $"{GetType().Name} {FirstName} {LastName}";
        }
    }
}
