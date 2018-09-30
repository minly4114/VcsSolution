using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Inside
{
    public class Student : IDataObject
    {
        public string FirstName { get;  }

        public string LastName { get; }

        public string PastName { get; }

        /// <summary> true - is Man </summary>
        public bool? Male { get; }

        public string Group { get; }


        public Student(string fName, string lName, string pastName, bool? male, string group)
        {
            FirstName = fName;
            LastName = lName;
            PastName = pastName;
            Male = male;
            Group = group;

            ValidateData();
        }

        /// <summary> Проверяем данного студента в БД </summary>
        public void ValidateData()
        {
            DataValidator.ValidateFieldTextRequired(FirstName, "Имя");
            DataValidator.ValidateFieldTextRequired(LastName, "Фамилия");
            DataValidator.ValidateFieldText(PastName, "Отчество");
            DataValidator.ValidateFieldTextAdvanced(Group, "Группа");
        }
    }
}
