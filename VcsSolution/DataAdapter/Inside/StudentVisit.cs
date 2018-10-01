using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Inside
{
    public class StudentVisit : IDataObject
    {
        public string FirstName { get; }

        public string LastName { get; }

        public string PastName { get; }

        public string Group { get; }

        public DateTime DateTime { get; }

        public string Classroom { get; }

        public string Subject { get; }

        public bool Presense { get; private set; }

        public StudentVisit(string fName, string lName, string pName, string group, DateTime dt, string classroom, string subject, bool presense)
        {
            FirstName = fName;
            LastName = lName;
            PastName = pName;
            Group = group;
            DateTime = dt;
            Classroom = classroom;
            Presense = presense;
            Subject = subject;
            ValidateData();
        }

        public void SetPresense(bool presense)
        {
            Presense = presense;
        }

        public void ValidateData()
        {
            DataValidator.ValidateFieldTextRequired(FirstName, "Имя");
            DataValidator.ValidateFieldTextRequired(LastName, "Имя");
            DataValidator.ValidateFieldText(PastName, "Имя");
            DataValidator.ValidateFieldTextAdvanced(Group, "Группа");
            DataValidator.ValidateDateTime(DateTime, "Дата занятия");
            DataValidator.ValidateFieldTextAdvanced(Classroom, "Группа");
            DataValidator.ValidateFieldTextAdvanced(Subject, "Предмет");
        }    
    }
}
