using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Strategy
{
    public interface ISelectMinion
    {
        string Name { get; }
        Minion SelectEnemy(List<Minion> minions);
    }
}
