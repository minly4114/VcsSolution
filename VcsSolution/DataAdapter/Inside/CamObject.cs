using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Inside
{
    /// <summary>
    /// Объект взаимодействия
    /// </summary>
    class CamObject
    {
        public string SerializedImage { get; private set; }
        public DateTime Date { get; private set; }

        public CamObject(string SerializedImage)
        {
            // Не забудь поставить текущую дату
            throw new NotImplementedException();
        }
    }
}
