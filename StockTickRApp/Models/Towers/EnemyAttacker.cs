﻿using GameClient.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Dictionary<TowerDecorator, int> Upgrades { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int Rate { get; set; }
        public int Price { get; set; }
        public int TicksBeforeShot { get; set; }

    }
}