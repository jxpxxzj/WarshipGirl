using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data.Ship
{
    /// <summary>
    /// 战列舰
    /// </summary>
    abstract class BB :BaseShip ,IFire,IFireTwice
    {
        public void FireAttack(BaseShip ship)
        { }
        public void FireTwiceAttack(BaseShip ship)
        { }
    }
}
