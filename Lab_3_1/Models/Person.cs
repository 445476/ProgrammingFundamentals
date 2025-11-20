using System.Text.RegularExpressions;

namespace Models
{
    public enum Gender { Male, Female }

    namespace Lab3.Core
    {
        public abstract class Person : IStudy, ICook, IPersistable
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public Gender Gender { get; set; }

            protected Person() { }

            protected Person(string firstName, string lastName, Gender gender)
            {
                string namePattern = @"^[A-Za-zА\-]+$";

                // check for null
                FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
                LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
                Gender = gender;


                // name can be only letters
                if (!Regex.IsMatch(firstName, namePattern))
                {
                    throw new ArgumentException(nameof(firstName));
                }
                if (!Regex.IsMatch(lastName, namePattern))
                {
                    throw new ArgumentException(nameof(lastName));
                }


                
            }

            public void Cook() { }
            public void Study() { }

            //base method for universal reading of objects
            public virtual string ToPersistenceString()
            {
                return $"\"firstname\": \"{FirstName}\",\n" +
                       $"\"lastname\": \"{LastName}\",\n" +
                       $"\"gender\": \"{Gender}\",\n";
            }
            public override string ToString()
            {
                return $"{GetType().Name} {FirstName} {LastName}";
            }
        }
    }
}
