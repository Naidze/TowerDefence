using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Composite
{
    public abstract class MinionComponent
    {
        public virtual void Add(MinionComponent minionComponent) { }
        public virtual void Remove(MinionComponent minionComponent) { }

        public MinionComponent GetComponent() { throw new NotSupportedException(); }

        abstract public void DisplayInfo();

    }
}
