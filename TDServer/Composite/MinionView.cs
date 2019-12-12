using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Enums;

namespace TDServer.Composite
{
    public class MinionView : MinionComponent
    {
        public string Name { get; set; }
        public MinionType MinionType { get; set; }

        public MinionView(string Name)
        {
            this.Name = Name;
        }

        public MinionType GetMinionType()
        {
            return MinionType;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(MinionType);
        }
    }
}
