using System;
using System.Collections.Generic;
using System.Text;
using DataAdapter.Inside;
using MySql.Data.MySqlClient;

namespace DataAdapter.Outside
{
    public class MySql : OutsideAdapter
    {

        // Params
        private string host;
        private int port;
        private string dataBase;
        private string userName;
        private string password;

        public string GetObject(int from, int objectId)
        {
            throw new NotImplementedException();
        }
        public string SendObject(int to, int objectId, object obj)
        {
            throw new NotImplementedException();
        }

        // Func public
        public MySql(string host, int port, string dataBase, string userName, string password)
        {
            this.host = host;
            this.port = port;
            this.dataBase = dataBase;
            this.userName = userName;
            this.password = password;
        }
        public List<Student> GetStudent(Student student)
        {
            MySqlConnection conn = GetDBConnection();
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = StudentDBrequest(student);
                var reader = cmd.ExecuteReader();
            }
            catch(NullReferenceException)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            //#TODO
                return new List<Student>();
        }

        // Func private
        private MySqlConnection GetDBConnection()
        {
            String connString = "Server=" + host + ";Database=" + dataBase
               + ";port=" + port + ";User Id=" + userName + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
        private String StudentDBrequest(Student student)
        {
            string request;
            var male = student.Male ? 1 : 0;

            if(student.PastName.Equals(null)
                &&student.Group.Equals(null)
                &&student.Id.Equals(0))
            {
                request = $"SELECT * FROM vcsdb.visits " +
                    $"WHERE firstname = {student.FirstName} " +
                    $"AND lastname = {student.LastName}" +
                    $"AND male = {male};";
            }
            else if(student.PastName.Equals(null)
                &&student.Id.Equals(0))
            {
                request = $"SELECT * FROM vcsdb.visits " +
                    $"WHERE firstname = {student.FirstName} " +
                    $"AND lastname = {student.LastName} " +
                    $"AND male = {male}" +
                    $"AND group = {student.Group};";
            }
            else if(student.Group.Equals(null)&&
                student.Id.Equals(0))
            {
                request = $"SELECT * FROM vcsdb.visits " +
                    $"WHERE firstname = {student.FirstName} " +
                    $"AND lastname = {student.LastName} " +
                    $"AND male = {male}" +
                    $"AND pastname = {student.PastName};";
            }
            else if(student.PastName.Equals(null)&&
                student.Group.Equals(null))
            {
                request = $"SELECT * FROM vcsdb.visits " +
                    $"WHERE firstname = {student.FirstName} " +
                    $"AND lastname = {student.LastName} " +
                    $"AND male = {male}" +
                    $"AND idstudent = {student.Id};";
            }
            else if(student.PastName.Equals(null))
            {
                request = $"SELECT * FROM vcsdb.visits " +
                    $"WHERE firstname = {student.FirstName} " +
                    $"AND lastname = {student.LastName} " +
                    $"AND male = {male}" +
                    $"AND group = {student.Group} " +
                    $"AND idstudent = {student.Id};";
            }
            else if(student.Group.Equals(null))
            {
                request = $"SELECT * FROM vcsdb.visits " +
                    $"WHERE firstname = {student.FirstName} " +
                    $"AND lastname = {student.LastName} " +
                    $"AND male = {male}" +
                    $"AND pastname = {student.PastName} " +
                    $"AND idstudent = {student.Id};";
            }
            else if (student.Id.Equals(0))
            {
                request = $"SELECT * FROM vcsdb.visits " +
                    $"WHERE firstname = {student.FirstName} " +
                    $"AND lastname = {student.LastName} " +
                    $"AND male = {male}" +
                    $"AND group = {student.Group} " +
                    $"AND pastname = {student.PastName};";
            }
            else
            {
                request = $"SELECT * FROM vcsdb.visits " +
                    $"WHERE firstname = {student.FirstName} " +
                    $"AND lastname = {student.LastName} " +
                    $"AND male = {male}" +
                    $"AND group = {student.Group}" +
                    $"AND pastname = {student.PastName}" +
                    $"AND idstudent = {student.Id};";
            }
            return request;
        }

    }
}
