using System.Threading;
using TDServer.Hubs;
using TDServer.Models;
using Microsoft.AspNetCore.SignalR;
using TDServer.Factory;
using System.Diagnostics;
using TDServer.Helpers;
using TDServer.Models.Towers;
using TDServer.Facade;
using TDServer.ChainOfResponsibility;
using TDServer.Composite;

namespace TDServer
{
    public class Game
    {
        public MinionsHolder MinionsHolder { get; set; }
        public Player[] players = new Player[GameUtils.PLAYER_COUNT];
        public bool gameStarted = false;
        public int wave;
        public Timer gameLoop;
        public int leftToSpawn;
        public int ticksBeforeSpawn;
        public UnitFactory unitFactory;
        private readonly AbstractLogger logger = AbstractLogger.GetChainOfLoggers();

        public Game(IHubContext<GameHub> hub)
        {
            Hub = hub;
        }

        public IHubContext<GameHub> Hub
        {
            get;
            set;
        }

        public void ChangeName(string connectionId, string name)
        {
            Debug.WriteLine("Changing name " + connectionId + " to: " + name);

            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                if (players[i] != null && players[i].Id == connectionId)
                {
                    players[i].Name = name;
                    logger.LogMessage(LogLevel.DEBUG, "Set player " + (i + 1) + " name to: " + name);
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

        public void SetMinionsHolder(MinionsHolder minionsHolder)
        {
            this.MinionsHolder = minionsHolder;
        }
    }
}