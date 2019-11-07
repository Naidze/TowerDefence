using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Decorator;
using TDServer.Enums;
using TDServer.Helpers;
using TDServer.Models;
using TDServer.Models.Minions;
using TDServer.Models.Towers;
using TDServer.Strategy;

namespace TDServer.Facade
{
    public class TowerManager
    {
        private readonly Game _game;

        public TowerManager(Game game)
        {
            _game = game;
        }

        public void PlaceTower(string name, string towerName, int x, int y)
        {
            Player player = _game.GetPlayer(name);
            if (player == null)
            {
                return;
            }

            Enum.TryParse(towerName.ToUpper(), out TowerType type);
            Tower tower = _game.unitFactory.CreateTower(type, new Position(x, y));
            EnemyAttacker attacker = new HighDamage(new HighRate(new LongRange(tower)));

            if (player.Money < tower.Price)
            {
                return;
            }
            player.Money -= attacker.Price;
            player.Towers.Add(attacker);
        }

        public void FireTowers()
        {
            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                foreach (EnemyAttacker tower in _game.players[i].Towers)
                {
                    if (tower.TicksBeforeShot-- > 0)
                    {
                        continue;
                    }

                    Minion minion = tower.AttackMode.SelectEnemy(_game.players[i].Minions);
                    if (minion == null)
                    {
                        continue;
                    }
                    DamageMinion(_game.players[i], tower, minion);
                }
            }
        }

        private void DamageMinion(Player player, EnemyAttacker tower, Minion minion)
        {
            tower.TicksBeforeShot = GameUtils.SHOOT_EVERY_X_TICK / tower.Rate;
            minion.Health -= tower.Damage;
            if (minion.Health <= 0)
            {
                player.Money += minion.Reward;
                player.Minions.Remove(minion);
            }
        }

        public void ChangeAttackMode(string name, string towerId, string mode)
        {
            Player player = _game.GetPlayer(name);
            if (player == null)
            {
                return;
            }

            EnemyAttacker tower = GetTower(player, int.Parse(towerId));
            if (tower == null)
            {
                return;
            }

            Enum.TryParse(mode.ToUpper(), out AttackMode attackMode);
            switch (attackMode)
            {
                case AttackMode.CLOSEST:
                    tower.AttackMode = new SelectClosestMinion(tower);
                    return;
                case AttackMode.FURTHEST:
                    tower.AttackMode = new SelectFurthestMinion(tower);
                    return;
                case AttackMode.WEAKEST:
                    tower.AttackMode = new SelectWeakestMinion(tower);
                    return;
                case AttackMode.STRONGEST:
                    tower.AttackMode = new SelectStrongestMinion(tower);
                    return;
                default:
                    return;
            }
        }

        private EnemyAttacker GetTower(Player player, int towerId)
        {
            foreach (EnemyAttacker tower in player.Towers)
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
