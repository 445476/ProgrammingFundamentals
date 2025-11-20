using System.Reflection;
using Lab_3_5.Models;

namespace Lab_3._5_DAL
{
    public class FileStudentRepository : IStudentRepository
    {
        private readonly string _path;
        public FileStudentRepository(string path)
        {
            _path = path;
        }


        public List<Student> LoadStudents()
        {
            var list = new List<Student>();
            foreach (var line in File.ReadAllLines(_path))
            {
                var p = line.Split(';');
                list.Add(new Student
                {
                    LastName = p[0],
                    FirstName = p[1],
                    Course = int.Parse(p[2]),
                    StudentId = p[3],
                    Gender = p[4] == "F" ? Gender.Female : Gender.Male,
                    GradeAvg = double.Parse(p[5]),
                    Id = p[6]
                });
            }
            return list;
        }

        public void SaveStudents(List<Student> students)
        {
        }
    }
}
