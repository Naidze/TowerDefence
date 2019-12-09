using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Iterator
{
    public interface ITowerCollection
    {
        ITowerIterator CreateIterator();
    }
}
