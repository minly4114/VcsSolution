using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Inside
{
    public class Student : IDataObject
    {
        public int Id { get; }

        public string FirstName { get;  }

        public string LastName { get; }

        public string PastName { get; }

        /// <summary> true - is Man </summary>
        public bool Male { get; }

        public string Group { get; }


		/// <summary></summary>
		/// <param name="id">Если нет -1</param>
		/// <param name="fName"></param>
		/// <param name="lName"></param>
		/// <param name="pastName"></param>
		/// <param name="male"></param>
		/// <param name="group"></param>
		public Student(int id, string fName, string lName, string pastName, bool male, string group)
        {
                Id = id;
                FirstName = fName;
                LastName = lName;
                PastName = pastName;
                Male = male;
                Group = group;

                ValidateData();                
        }

        public void ValidateData()
        {
            DataValidator.ValidateFieldTextRequired(FirstName, "Имя");
            DataValidator.ValidateFieldTextRequired(LastName, "Фамилия");
            DataValidator.ValidateFieldText(PastName, "Отчество");
            DataValidator.ValidateFieldTextAdvanced(Group, "Группа");
        }
    }
}
