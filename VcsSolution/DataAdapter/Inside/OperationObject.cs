using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Inside
{
    /// <summary>
    /// Объект взаимодействия
    /// </summary>
    class OperationObject
    {  
        public int Id { get; private set; }
        public string Hash { get; private set; }
        public enum Statuses { Passed, Aborted }
        public Statuses Status;
    }
}
