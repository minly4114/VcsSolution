using System;
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
            try
            {
                var sql = new MySql();
                var result = sql.GetStudent(new StudentSearchObject("Ег;ор", "Петров", null, true, "ИВБО-06-16"));
            } catch(ValidationErrorException ex)
            {
                Assert.AreEqual("Имя", ex.FieldName);
            }
        }
    }
}
