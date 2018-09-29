using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Inside.Stubs
{
    public static class StudentStub
    {
        public static Student GetStudent()
        {
            return new Student("FirstName", "LastName", true, "ИВБО-06-16");
        }
    }
}
