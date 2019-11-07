using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models;

namespace TDServer.Helpers
{
    public class GameUtils
    {
        public const int PLAYER_COUNT = 2;
        public const int TICK_INTERVAL = 15;
        public const int WAVE_INTERVAL = 1000;
        public const int SPAWN_EVERY_X_TICK = 10;
        public const int SHOOT_EVERY_X_TICK = 50;

        public static double CalculateDistance(Position a, Position b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}
