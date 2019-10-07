using System;
namespace GameServer.Models
{
    public class RocketTower
    {
        private RocketLauncher gun;
        private int cost;
        private int hitpoints;

        public RocketTower()
        {

        }

        public RocketTower setGun(RocketLauncher gun)
        {
            this.gun = gun;
            return this;
        }

        public RocketTower setCost(int cost)
        {
            this.cost = cost;
            return this;
        }

        public RocketTower setHitpoints(int hitpoints)
        {
            this.hitpoints = hitpoints;
            return this;
        }
    }
}
