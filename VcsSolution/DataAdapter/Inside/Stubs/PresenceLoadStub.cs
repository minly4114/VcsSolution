using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Inside.Stubs
{
    public static class PresenceLoadStub
    {

        public static StudentVisit GetStudentVisit(string firstName, string lastName, string pastName, string group, string classroom, DateTime dt, bool presence)
        {
            return new StudentVisit(firstName, lastName, pastName, group, dt, classroom, presence);
        }
    }
}
