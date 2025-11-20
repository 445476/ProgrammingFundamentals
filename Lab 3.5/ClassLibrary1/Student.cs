namespace Lab_3_5.Models
{
    public enum Gender { Male, Female }
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Course { get; set; }
        public string StudentId { get; set; }
        public Gender Gender { get; set; }
        public double GradeAvg { get; set; }
        public string Id { get; set; }
    }
}
