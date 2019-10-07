using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Game
    {

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int Wave { get; set; }


    }
}
