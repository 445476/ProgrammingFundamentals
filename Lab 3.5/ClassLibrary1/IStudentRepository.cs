using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_5.Models
{
    public interface IStudentRepository
    {
        List<Student> LoadStudents();
        void SaveStudents(List<Student> students);

    }
}
