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
                cmd.CommandText = StudentsDbRequest(student);
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
        public List<StudentVisit> GetStudentVisits(StudentVisit studentVisit)
        {
            var studentVisits = new List<StudentVisit>();
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = StudentVisitsDbRequest(studentVisit);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    studentVisits.Add(new StudentVisit((int)reader["idpresense"], (string)reader["firstname"], (string)reader["lastname"], (string)reader["pastname"], (string)reader["studentgroup"], (DateTime)reader["datetime"], (string)reader["classroom"], (string)reader["subject"], (bool)reader["presense"]));
                }
            }
            catch (NullReferenceException)
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return studentVisits;
        }

        // Func private
        private MySqlConnection GetDBConnection()
        {
            String connString = "Server=" + host + ";Database=" + dataBase
               + ";port=" + port + ";User Id=" + userName + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
        private String StudentsDbRequest(Student student)
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
        private String StudentVisitsDbRequest(StudentVisit studentVisit)
        {
            string request;
            var presense = studentVisit.Presense ? 1 : 0;
            var datetime = $"{studentVisit.DateTime.Year}" +
                $"-{studentVisit.DateTime.Month}" +
                $"-{studentVisit.DateTime.Day}" +
                $" {studentVisit.DateTime.Hour}" +
                $":{studentVisit.DateTime.Minute}" +
                $":{studentVisit.DateTime.Second}";
            request = $"SELECT * FROM vcsdb.visits " +
                $"WHERE firstname = '{studentVisit.FirstName}' " +
                $"AND lastname = '{studentVisit.LastName}' " +
                $"AND datetime = '{datetime}' " +
                $"AND presense = '{presense}' ";
            if(!studentVisit.Classroom.Equals(null))
            {
                request += $"AND classroom = '{studentVisit.Classroom}' ";
            }
            if(!studentVisit.Group.Equals(null))
            {
                request += $"AND studentgroup = '{studentVisit.Group}' ";
            }
            if(!studentVisit.Id.Equals(null))
            {
                request += $"AND idpresense = '{studentVisit.Id}' ";
            }
            if(!studentVisit.PastName.Equals(null))
            {
                request += $"AND pastname = '{studentVisit.PastName}' ";
            }
            if(!studentVisit.Subject.Equals(null))
            {
                request += $"AND subject = '{studentVisit.Subject}' ";
            }
            request += ";";
            return request;
        }
        private String SubjectsDbRequest()
        {
            string request = "SELECT * FROM vcsdb.subjects ;";
            return request;
        }
        private String ClassroomsDbRequest()
        {
            string request = "SELECT * FROM vcsdb.classrooms;";
            return request;
        }

    }
}
