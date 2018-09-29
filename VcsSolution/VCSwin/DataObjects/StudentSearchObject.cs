using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAdapter;

namespace VCSwin.DataObjects
{
    class StudentSearchObject
    {
        public string FirstName;
        public string LastName;
        public string PastName;
        public string Group;

        public StudentSearchObject(string fName, string lName, string PName, string group)
        {
            FirstName = fName;
            LastName = lName;
            PastName = PName;
            Group = group;

            ValidateData();
        }

        public StudentSearchObject()
        {
            ValidateData();
        }

        public StudentSearchObject ValidateData()
        {
            DataValidator.ValidateFieldTextRequired(FirstName, "Имя");
            DataValidator.ValidateFieldTextRequired(LastName, "Фамилия");
            DataValidator.ValidateFieldText(PastName, "Отчество");
            DataValidator.ValidateFieldTextAdvanced(Group, "Группа");
            return this;
        }
    }
}
