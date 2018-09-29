using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Inside
{
    public class Student
    {
        public string FirstName { get;  }

        public string LastName { get; }

        public string PastName { get; }

        /// <summary> true - is Man </summary>
        public bool? Male { get; }

        public string Group { get; }


        public Student(string fName, string lName, bool? male, string group)
        {
            FirstName = fName;
            LastName = lName;
            male = Male;
            group = Group;

            ValidateData();
        }

        /// <summary> Проверяем данного студента в БД </summary>
        private void ValidateData()
        {

        }
    }
}
