using GameClient.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;
using TDServer.State;
using TDServer.Strategy;

namespace TDServer.Models.Towers
{
    public abstract class EnemyAttacker
    {
        protected static int idCounter = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public ISelectMinion AttackMode { get; set; }
        public Dictionary<string, int> Upgrades { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int Rate { get; set; }
        public int Price { get; set; }
        public TowerAction TowerAction { get; set; }
        public Minion Target { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is EnemyAttacker attacker))
            {
                return false;
            }
            return Id.Equals(attacker.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
