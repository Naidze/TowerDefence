using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Decorator;
using TDServer.Enums;
using TDServer.Helpers;
using TDServer.Iterator;
using TDServer.Memento;
using TDServer.Models;
using TDServer.Models.Minions;
using TDServer.Models.Towers;
using TDServer.Strategy;
using TDServer.Template;

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
            EnemyAttacker attacker = new HighRateConveyor().BuildTower(new Position(x, y));

            if (player.Money < tower.Price)
            {
                return;
            }

            player.Caretaker.Add(player.Originator.SaveState(tower.Price, ObjectCopier.Clone(player.Towers)));
            player.Money -= tower.Price;
            int towerCount = player.Towers.Count;
            player.Towers[towerCount] = tower;
        }

        public void TowersAction()
        {
            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                ITowerIterator iterator = _game.players[i].Towers.CreateIterator();
                for (EnemyAttacker tower = iterator.First(); !iterator.IsDone; tower = iterator.Next())
                {
                    tower.TowerAction.Action(tower, _game.players[i]);
                }
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
                    tower.AttackMode = new SelectClosestMinion();
                    return;
                case AttackMode.FURTHEST:
                    tower.AttackMode = new SelectFurthestMinion();
                    return;
                case AttackMode.WEAKEST:
                    tower.AttackMode = new SelectWeakestMinion();
                    return;
                case AttackMode.STRONGEST:
                    tower.AttackMode = new SelectStrongestMinion();
                    return;
                default:
                    return;
            }
        }

        public void UpgradeTower(string name, string towerId, string type)
        {
            Player player = _game.GetPlayer(name);
            if (player == null)
            {
                return;
            }

            int id = int.Parse(towerId);
            EnemyAttacker tower = GetTower(player, id);
            if (tower == null)
            {
                return;
            }

            switch (type)
            {
                case "damage":
                    UpdateTower(player, id, new HighDamage(tower));
                    return;
                case "rate":
                    UpdateTower(player, id, new HighRate(tower));
                    return;
                case "range":
                    UpdateTower(player, id, new LongRange(tower));
                    return;
                default:
                    return;
            }

        }

        public void SellTower(string name, string towerId)
        {
            Player player = _game.GetPlayer(name);
            if (player == null)
            {
                return;
            }

            int id = int.Parse(towerId);
            EnemyAttacker tower = GetTower(player, id);
            if (tower == null)
            {
                return;
            }

            player.Towers.Remove(tower);
        }

        public void UndoTower(string name)
        {
            Player player = _game.GetPlayer(name);
            if (player == null)
            {
                return;
            }

            if (player.Caretaker.Size() == 0)
            {
                return;
            }

            PlayerMemento memento = player.Caretaker.Restore();
            player.Originator.RestoreState(memento);
        }

        private EnemyAttacker GetTower(Player player, int towerId)
        {
            ITowerIterator iterator = player.Towers.CreateIterator();
            for (EnemyAttacker tower = iterator.First(); !iterator.IsDone; tower = iterator.Next())
            {
                if (tower.Id == towerId)
                {
                    return tower;
                }
            }
            return null;
        }

        private void UpdateTower(Player player, int towerId, EnemyAttacker attacker)
        {
            for (int i = 0; i < player.Towers.Count; i++)
            {
                if (player.Towers[i].Id == towerId)
                {
                    player.Towers[i] = attacker;
                }
            }
        }
    }
}
