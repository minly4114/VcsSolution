using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAdapter.Outside;
using DataAdapter.Inside;
using DataAdapter.Exceptions;

namespace Tests
{
    [TestClass]
    public class DataAdapterTests
    {
        /// <summary> Тест обычного получения студента из базы </summary>
        [TestMethod]
        public void MySql_GetStudent()
        {
            var sql = new MySql();
            var result = sql.GetStudent(new StudentSearchObject("Егор", "Петров", null, true, "ИВБО-06-16"));

            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual("Егор", result[0].FirstName);
        }


        /// <summary> Тест - Поле Имя содержит "-" </summary>
        [TestMethod]
        public void MySql_GetStudent_1()
        {
            var sql = new MySql();
            var result = sql.GetStudent(new StudentSearchObject("Егор-Артём", "Петров", null, true, "ИВБО-06-16"));
        }

        /// <summary> Негативный тест - группа не указана </summary>
        [TestMethod]
        public void MySql_GetStudent_Negative()
        {
            var sql = new MySql();
            var result = sql.GetStudent(new StudentSearchObject("Егор", "Петров", null, true, null));

            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual("Егор", result[0].FirstName);
        }

        /// <summary> Негативный тест - Имя с запретными символами </summary>
        [TestMethod]
        public void MySql_GetStudent_Negative_2()
        {
            bool isExeption = false;
            try
            {
                var sql = new MySql();
                var result = sql.GetStudent(new StudentSearchObject("Ег;ор", "Петров", null, true, "ИВБО-06-16"));
            } catch(ValidationErrorException ex)
            {
                Assert.AreEqual("Имя", ex.FieldName);
                isExeption = true;
            }
            Assert.IsTrue(isExeption);
        }

        /// <summary> Негативный тест - Фамилия с запретными символами </summary>
        [TestMethod]
        public void MySql_GetStudent_Negative_3()
        {
            bool isExeption = false;
            try
            {
                var sql = new MySql();
                var result = sql.GetStudent(new StudentSearchObject("Егор", "Пе%тров", null, true, "ИВБО-06-16"));
            }
            catch (ValidationErrorException ex)
            {
                Assert.AreEqual("Фамилия", ex.FieldName);
                isExeption = true;
            }
            Assert.IsTrue(isExeption);
        }

        /// <summary> Негативный тест - Имя с запретными символами </summary>
        [TestMethod]
        public void MySql_GetStudent_Negative_4()
        {
            bool isExeption = false;
            try
            {
                var sql = new MySql();
                var result = sql.GetStudent(new StudentSearchObject("Ег/ор", "Петров", null, true, "ИВБО-06-16"));
            }
            catch (ValidationErrorException ex)
            {
                Assert.AreEqual("Имя", ex.FieldName);
                isExeption = true;
            }
            Assert.IsTrue(isExeption);
        }

        /// <summary> Негативный тест - Фамилия с запретными символами </summary>
        [TestMethod]
        public void MySql_GetStudent_Negative_6()
        {
            bool isExeption = false;
            try
            {
                var sql = new MySql();
                var result = sql.GetStudent(new StudentSearchObject("Егор", "Пе/тров", null, true, "ИВБО-06-16"));
            }
            catch (ValidationErrorException ex)
            {
                Assert.AreEqual("Фамилия", ex.FieldName);
                isExeption = true;
            }
            Assert.IsTrue(isExeption);
        }
        /// <summary> Негативный тест - Фамилия с запретными символами </summary>
        [TestMethod]
        public void MySql_GetStudent_Negative_7()
        {
            bool isExeption = false;
            try
            {
                var sql = new MySql();
                var result = sql.GetStudent(new StudentSearchObject("Егор", "Абдул,Маликов", null, true, "ИВБО-06-16"));
            }
            catch (ValidationErrorException ex)
            {
                Assert.AreEqual("Фамилия", ex.FieldName);
                isExeption = true;
            }
            Assert.IsTrue(isExeption);
        }

        /// <summary> Негативный тест - не указано имя </summary>
        [TestMethod]
        public void MySql_GetStudent_Negative_8()
        {
            bool isExeption = false;
            var sql = new MySql();
            try
            {
                var result = sql.GetStudent(new StudentSearchObject(null, "Петров", null, true, "ИВБО-06-16"));
            } catch(ValidationErrorException ex)
            {
                Assert.AreEqual("Имя", ex.FieldName);
                isExeption = true;
            }
            Assert.IsTrue(isExeption);
        }
        /// <summary> Негативный тест - не указана фамилия </summary>
        [TestMethod]
        public void MySql_GetStudent_Negative_9()
        {
            bool isExeption = false;
            var sql = new MySql();
            try
            {
                var result = sql.GetStudent(new StudentSearchObject("Егор", null, null, true, "ИВБО-06-16"));
            }
            catch (ValidationErrorException ex)
            {
                Assert.AreEqual("Фамилия", ex.FieldName);
                isExeption = true;
            }
            Assert.IsTrue(isExeption);
        }

        [TestMethod]
        public void MySql_GetSubject()
        {
            var sql = new MySql();
            var result = sql.GetSubjects();

            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual("Алгебра и геометрия", result[0]);
        }
        [TestMethod]
        public void mySql_GetClassrooms()
        {
            var sql = new MySql();
            var result = sql.GetClassrooms();

            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual("А-1", result[0]);
        }
       
    }


}
