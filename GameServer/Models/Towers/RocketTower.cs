using System;
namespace GameServer.Models
{
    public class RocketTower : Tower
    {
        public RocketLauncher Gun { get; set; }
        public int Cost { get; set; }
        public int HitPoints { get; set; }

        public RocketTower()
        {

        }

        public RocketTower SetGun(RocketLauncher Gun)
        {
            this.Gun = Gun;
            return this;
        }

        public RocketTower SetCost(int Cost)
        {
            this.Cost = Cost;
            return this;
        }

        public RocketTower SetHitPoints(int HitPoints)
        {
            this.HitPoints = HitPoints;
            return this;
        }
    }
}
