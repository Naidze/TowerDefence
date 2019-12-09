using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.Iterator
{
    public interface ITowerIterator
    {
        EnemyAttacker First();
        EnemyAttacker Next();
        bool IsDone { get; }
        EnemyAttacker CurrentTower { get; }
    }
}
