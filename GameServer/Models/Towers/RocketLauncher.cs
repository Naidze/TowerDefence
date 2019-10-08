using System;
namespace GameServer.Models
{
    public class RocketLauncher
    {
        public double Damage { get; set; }
        public double Range { get; set; }
        public int Speed { get; set; }

        public RocketLauncher()
        {

        }

        public RocketLauncher SetDamage(double Damage)
        {
            this.Damage = Damage;
            return this;
        }

        public RocketLauncher SetRange(double Range)
        {
            this.Range = Range;
            return this;
        }

        public RocketLauncher SetSpeed(int Speed)
        {
            this.Speed = Speed;
            return this;
        }
    }
}
