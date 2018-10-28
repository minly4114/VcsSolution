using System;
using System.Collections.Generic;
using System.Text;
using DataAdapter.Inside;
using MySql.Data.MySqlClient;

namespace DataAdapter.Outside
{
    public class MySql
    {

        // Params
        private string host = "37.204.31.67";
        private int port = 3306;
        private string dataBase = "vcsdb";
        private string userName = "root";
        private string password = "1234";
        public MySqlConnection conn;

        // Func public
        public MySql()
        {
            conn = GetDBConnection();
        }

        public List<Student> GetStudent(StudentSearchObject student)
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
            catch (NullReferenceException)
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return students;
        }
        public List<StudentVisit> GetStudentVisits(StudentVisitSearchObject studentVisit)
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
                    studentVisits.Add(new StudentVisit((int)reader["idpresense"], (string)reader["firstname"], (string)reader["lastname"], (string)reader["pastname"], (string)reader["studentgroup"], (DateTime)reader["date"], (string)reader["classroom"], (string)reader["subject"], (bool)reader["presense"]));
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
        public List<string> GetClassrooms()
        {
            var classrooms = new List<string>();
            conn.Open();
            try
            {
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = ClassroomsDbRequest();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    classrooms.Add((string)reader["classroom"]);
                }
            }
            catch(NullReferenceException)
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return classrooms;
        }
        public List<string> GetSubjects()
        {
            var subjects = new List<string>();
            conn.Open();
            try
            {
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = SubjectsDbRequest();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subjects.Add((string)reader["subject"]);
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
            return subjects;
        }
        public List<string> GetStudentGroups()
        {
            var groups = new List<string>();
            conn.Open();
            try
            {
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = GroupDbRequest();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    groups.Add((string)reader["studentgroup"]);
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
            return groups;
        }
        
        // TODO: ЗАМЕНИТЬ НА СОЗДАНИЕ НОВОГО ПОСЕЩЕНИЯ с 0
        public bool SetStudentVisit(StudentVisit studentVisit)
        {
            bool success = true;
            conn.Open();
            try
            {
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = StudentVisitsDBRequestSet(studentVisit);
                var reader = cmd.ExecuteReader();
            }
            catch (NullReferenceException)
            {
                success = false;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return success;
        }

        public bool SetStudentVisitTrue(string studentId, DateTime dt, string pairNumber, string classroom, string subject)
        {
            bool success = true;
            conn.Open();
            try
            {
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = StudentVisitsByIdDBRequestSet(studentId, dt, pairNumber, classroom, subject);
                var qr  = cmd.ExecuteNonQuery();
                if(qr < 1)
                {
                    throw new Exception("Не удалось отметить посещение");
                }
            }
            catch (NullReferenceException)
            {
                success = false;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return success;
        }

        public string GetStudentIdByCardNumber(string cardNumber)
        {
            conn.Open();
            try
            {
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = StudentByCardDbRequest(cardNumber);
                var reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    return reader.GetString(0);
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
            return "";
        }

        // Func private
        private MySqlConnection GetDBConnection()
        {
            String connString = "Server=" + host + ";Database=" + dataBase
               + ";port=" + port + ";User Id=" + userName + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
        private String StudentsDbRequest(StudentSearchObject student)
        {
            string request;
            var male = student.Male ? 1 : 0;
            request = "SELECT * FROM vcsdb.students " +
                    $"WHERE firstname = '{student.FirstName}' " +
                    $"AND lastname = '{student.LastName}' " +
                    $"AND male = '{male}' ";
            if(student.PastName != null)
            {
                request += $"AND pastname = '{student.PastName}' ";
            }
            if(student.Group != null)
            {
                request += $"AND studentgroup = '{student.Group}' ";
            }
            request += ";"; 
            return request;
        }
        private String StudentVisitsDbRequest(StudentVisitSearchObject studentVisit)
        {
            string request;
            var date = studentVisit.DateTime.ToString("yyyy-MM-dd");
            request = $"SELECT * FROM vcsdb.visits " +
                $"WHERE firstname = '{studentVisit.Student.FirstName}' " +
                $"AND lastname = '{studentVisit.Student.LastName}' " +
                $"AND date = '{date}' ";
            if(studentVisit.Classroom != null)
            {
                request += $"AND classroom = '{studentVisit.Classroom}' ";
            }
            if(studentVisit.Student.Group != null)
            {
                request += $"AND studentgroup = '{studentVisit.Student.Group}' ";
            }
            if(studentVisit.Student.PastName != null)
            {
                request += $"AND pastname = '{studentVisit.Student.PastName}' ";
            }
            if(studentVisit.Subject != null)
            {
                request += $"AND subject = '{studentVisit.Subject}' ";
            }
            request += ";";
            return request;
        }
        private string StudentVisitsDBRequestSet(StudentVisit studentVisit)
        {
            string request;
            var date = studentVisit.DateTime.ToString("yyyy-MM-dd");
            request = $"insert into vcsdb.visits (idstudent, date, idclassroom, idclass, presense, pairnumber) " +
                $"value((select idstudent from vcsdb.students where firstname = '{studentVisit.FirstName}' and lastname = '{studentVisit.LastName}' and pastname = '{studentVisit.PastName}' )" +
                $", '{date}'" +
                $", (select idsubject from vcsdb.subjects where subject = '{studentVisit.Subject}')" +
                $", (select idclass from vcsdb.shedule where idsubject = (select idsubject from vcsdb.subjects where subject = '{studentVisit.Subject}') and idclassroom = (select idclassroom from vcsdb.classrooms = '{studentVisit.Classroom}'))" +
                $", '0', '1');";
            //request = $"UPDATE vcsdb.visits " +
            //    $"SET firstname = '{studentVisit.FirstName}' " +
            //    $", lastname = '{studentVisit.LastName}' " +
            //    $", pastname = '{studentVisit.PastName}' " +
            //    $", date = '{date}' " +
            //    $", classroom = '{studentVisit.Classroom}' " +
            //    $", studentgroup = '{studentVisit.Group}' " +
            //    $", subject = '{studentVisit.Subject}' " +
            //    $", presense = '0' " +
            //    $"WHERE idpresense = '{studentVisit.Id}';";
            return request;
        }

        private string StudentVisitsByIdDBRequestSet(string studentId, DateTime dt, string pairNumber, string idclassroom, string idclass)
        {
            string request;
            var date = dt.ToString("yyyy-MM-dd");
            request = $"UPDATE vcsdb.visits " +
                $"SET presense = '1' " +
                $"WHERE studentid = '{studentId}' " +
                $"AND date = '{date}' " +
                $"AND idclassroom = '{idclassroom}' " +
                $"AND idclass = '{idclass}' " +
                $"AND pairnumber = '{pairNumber}';";
            return request;
        }

        private string StudentByCardDbRequest(string studentCard)
        {
            string request;
            request = $"SELECT studentid FROM vcsdb.studentscards " +
                $"WHERE cardnumber = '{studentCard}';";            
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
        private string GroupDbRequest()
        {
            string request = "SELECT * FROM vcsdb.studentgroups;";
            return request;
        }
    }
}
