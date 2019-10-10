using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.Builder
{
    interface ITowerBuilder
    {
        TowerBuilder SetRate(int Rate);
        TowerBuilder SetRange(int Range);
        TowerBuilder SetDamage(int Damage);
        Tower GetResult();
    }
}
