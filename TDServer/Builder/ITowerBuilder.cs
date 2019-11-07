using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models;
using TDServer.Models.Towers;

namespace TDServer.Builder
{
    interface ITowerBuilder
    {
        TowerBuilder SetName(string name);
        TowerBuilder SetRate(int rate);
        TowerBuilder SetRange(int range);
        TowerBuilder SetDamage(int damage);
        TowerBuilder SetPrice(int price);
        Tower GetResult();
    }
}
