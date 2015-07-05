using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data.Ship
{
    /// <summary>
    /// 航空母舰
    /// </summary>
    abstract class CV : BaseShip, IFire, IBeginAir
    {
        public void FireAttack(BaseShip ship) { }
        public void BeginAirAttack(BaseShip ship) { }
    }
}
