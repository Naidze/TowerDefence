using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Composite
{
    public class MinionViewGroup : MinionComponent
    {
        public ArrayList MinionComponents { get; set;}

        public string GroupName { get; set; }

        public MinionViewGroup(String groupName)
        {
            this.GroupName = groupName;
            MinionComponents = new ArrayList();
        }

        public string GetGroupName()
        {
            return GroupName;
        }

        public override void Add(MinionComponent minionComponent)
        {
            MinionComponents.Add(minionComponent);
        }


        public override void Remove(MinionComponent minionComponent)
        {
            MinionComponents.Remove(minionComponent);
        }

        public new MinionComponent GetComponent(int componentIndex)
        {
            return (MinionComponent)MinionComponents[componentIndex];
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(GroupName);
            foreach (MinionComponent minion in MinionComponents)
            {
                minion.DisplayInfo();
            }
        }

    }
}
