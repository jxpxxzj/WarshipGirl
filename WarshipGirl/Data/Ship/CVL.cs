using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data.Ship
{
    /// <summary>
    /// 轻型航空母舰
    /// </summary>
    abstract class CVL : BaseShip, IBeginAir, IFire, IBeginAntiSubmarine
    {
        public void FireAttack(BaseShip ship) { }
        public void BeginAirAttack(BaseShip ship) { }
        public void BeginAntiSubmarineAttack(BaseShip ship) { }
    }
}
