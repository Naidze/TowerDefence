using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Composite
{
    public class MinionsHolder
    {
        public MinionComponent MinionsList { get; set; }

        public MinionsHolder()
        {

        }

        public MinionsHolder(MinionComponent minionsList)
        {
            this.MinionsList = minionsList;
        }

        public MinionComponent GetMinionsList()
        {
            return MinionsList;
        }

        public void SetMinions(MinionComponent minions)
        {
            MinionsList = minions;
        }
    }
}
