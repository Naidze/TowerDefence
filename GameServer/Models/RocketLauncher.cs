using System;
namespace GameServer.Models
{
    public class RocketLauncher
    {
        private double damage;
        private double range;
        private int speed;

        public RocketLauncher()
        {

        }

        public RocketLauncher setDamage(double damage)
        {
            this.damage = damage;
            return this;
        }

        public RocketLauncher setRange(double range)
        {
            this.range = range;
            return this;
        }

        public RocketLauncher setSpeed(int speed)
        {
            this.speed = speed;
            return this;
        }
    }
}
