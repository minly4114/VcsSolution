using System;
using System.Collections.Generic;
using System.Text;

namespace DataAdapter.Outside
{
    interface OutsideAdapter
    {
        string GetObject(int from, int objectId);
        string SendObject(int to, int objectId, object obj);
    }
}
