using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;

namespace TDServer.Adapter
{
    public class LizardAdapter : Minion, IMinion
    {
        private readonly Lizard _lizard;

        public LizardAdapter(Lizard lizard, string name) : base(name, 50, 1, 25)
        {
            _lizard = lizard;
        }

        public bool Move()
        {
            return _lizard.Fly();
        }
    }
}
