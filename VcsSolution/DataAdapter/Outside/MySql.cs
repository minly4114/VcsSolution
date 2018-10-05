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
        private string host = "127.0.0.1";
        private int port = 3306;
        private string dataBase = "vcsdb";
        private string userName = "root";
        private string password = "1234";
        public MySqlConnection conn;

        public string GetObject(int from, int objectId)
        {
            throw new NotImplementedException();
        }
        public string SendObject(int to, int objectId, object obj)
        {
            throw new NotImplementedException();
        }

        // Func public
        public MySql()
        {
            conn = GetDBConnection();
        }
        public List<Student> GetStudent(Student student)
        {
            var students = new List<Student>();
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = StudentDBrequest(student);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student((int)reader["idstudent"], (string)reader["firstname"], (string)reader["lastname"], (string)reader["pastname"],(bool) reader["male"], (string)reader["studentgroup"]));
                }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return students;
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
            request = "SELECT * FROM vcsdb.students " +
                    $"WHERE firstname = '{student.FirstName}' " +
                    $"AND lastname = '{student.LastName}' " +
                    $"AND male = '{male}' ";
            if(!student.PastName.Equals(null))
            {
                request += $"AND pastname = '{student.PastName}' ";
            }
            if(!student.Group.Equals(null))
            {
                request += $"AND studentgroup = '{student.Group}' ";
            }
            if(!student.Id.Equals(null))
            {
                request += $"AND idstudent = '{student.Id}' ";
            }
            request += ";"; 
            return request;
        }

    }
}
