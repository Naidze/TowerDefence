using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.Iterator
{
    public interface ITowerIterator
    {
        Tower First();
        Tower Next();
        bool IsDone { get; }
        Tower CurrentTower { get; }
    }
}
