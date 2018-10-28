using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAdapter.Outside;

namespace ARDIS
{
    public class ArdisWorker
    {
        MySql sql = new MySql();

        public void Send(string cardNumber, string pairNumber, string classroom, string subject)
        {
            try
            {
                string s = sql.GetStudentIdByCardNumber(cardNumber);
                if(s.Length < 1)
                {
                    throw new Exception("Студент не найден в базе");
                }
                if(!sql.SetStudentVisitTrue(s, DateTime.Now, pairNumber, classroom, subject))
                {
                    throw new Exception("Неизвестная ошибка отметки посещения");
                }          
            }
            catch
            {

                throw;
            }
        }
    }
}
