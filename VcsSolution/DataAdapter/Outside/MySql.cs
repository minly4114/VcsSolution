using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Outside
{
    class MySql : OutsideAdapter
    {
        public string GetObject(int from, int objectId)
        {
            //Test change
            throw new NotImplementedException();
        }
        public string SendObject(int to, int objectId, object obj)
        {
            throw new NotImplementedException();
        }
    }
}
