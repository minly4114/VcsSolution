using System;

namespace DataAdapter.Inside
{
    public class StudentVisitSearchObject : IDataObject
    {

        public Student Student { get; }

        public DateTime DateTime { get; }

        public string Classroom { get; }

        public string Subject { get; }

        public StudentVisitSearchObject(Student student, DateTime dateTime, string classroom, string subject)
        {
            Student = student;
            DateTime = dateTime;
            Classroom = classroom;
            Subject = subject;

            ValidateData();
        }

        public void ValidateData()
        {
            DataValidator.ValidateDateTime(DateTime, "Дата");
            DataValidator.ValidateFieldTextAdvanced(Classroom, "Аудитория");
            DataValidator.ValidateFieldTextAdvanced(Subject, "Предмет");
        }
    }
}
