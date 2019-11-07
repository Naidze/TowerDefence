using System.Threading;
using TDServer.Hubs;
using TDServer.Models;
using Microsoft.AspNetCore.SignalR;
using TDServer.Factory;
using System.Diagnostics;
using TDServer.Helpers;
using TDServer.Models.Towers;

namespace TDServer
{
    public class Game
    {

        public static readonly Path[] map = new Path[] {
            new Path(0, 220),
            new Path(100, 220),
            new Path(100, 100),
            new Path(220, 100),
            new Path(220, 260),
            new Path(380, 260),
            new Path(380, 180),
            new Path(590, 180)
        };

        public Player[] players = new Player[GameUtils.PLAYER_COUNT];

        public Game(IHubContext<GameHub> hub)
        {
            Hub = hub;
        }

        public IHubContext<GameHub> Hub
        {
            get;
            set;
        }

        public UnitFactory unitFactory;

        public bool gameStarted = false;
        public int wave;
        public Timer gameLoop;
        public int leftToSpawn;
        public int ticksBeforeSpawn;

        public void ChangeName(string connectionId, string name)
        {
            Debug.WriteLine("Changing name " + connectionId + " to: " + name);

            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                if (players[i] != null && players[i].Id == connectionId)
                {
                    players[i].Name = name;
                    Logger.GetInstance().Info("Set player " + (i + 1) + " name to: " + name);
                    break;
                }
            }
        }

        public Player GetPlayer(string name)
        {
            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                if (players[i].Name == name)
                {
                    return players[i];
                }
            }
            return null;
        }

        public void ChangeAttackMode(string name, int towerId, string mode)
        {
            Player player = GetPlayer(name);
            if (player == null)
            {
                return;
            }

            Tower tower = GetTower(player, towerId);
            if (tower == null)
            {
                return;
            }

        }

        private Tower GetTower(Player player, int towerId)
        {
            foreach (Tower tower in player.Towers)
            {
                if (tower.Id == towerId)
                {
                    return tower;
                }
            }
            return null;
        }
    }

}