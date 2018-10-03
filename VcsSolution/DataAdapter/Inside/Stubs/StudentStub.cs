namespace DataAdapter.Inside.Stubs
{
    public static class StudentStub
    {
        public static Student GetStudent()
        {
            return new Student(-1, "FirstName", "LastName", "PastName", true, "ИВБО-06-16");
        }

        public static Student GetStudent(string firstName, string lastName, string pastName)
        {
            return new Student(-1, firstName, lastName, pastName, true, "ИВБО-06-16");
        }
    }
}
