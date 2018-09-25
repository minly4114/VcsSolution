using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Inside
{
    /// <summary>
    /// Объект взаимодействия
    /// </summary>
    class Visitor
    {
        public int Id { get; private set; }
        public string Hash { get; private set; }
        public DateTime Date { get; private set; }
        public int CamNumber { get; private set; }
        public enum Tags { Checked, Unchecked }
        public Tags Tag;

        public Visitor(string hash, int camNumber, string localPhotoPath, Tags tag = Tags.Unchecked)
        {
            // Не забудь поставить дату текущую и присвоить Id из БД
            throw new NotImplementedException();
        }
    }
}
