using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;

namespace TDServer.Command
{
    public class Right : IMoveCommand
    {
        readonly Minion _minion;

        public Right(Minion minion)
        {
            _minion = minion;
        }

        public int Execute()
        {
            return _minion.MoveRight();
        }

        public int Undo()
        {
            return _minion.MoveLeft();
        }
    }
}
