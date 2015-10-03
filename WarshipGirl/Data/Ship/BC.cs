using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data.Ship
{
    /// <summary>
    /// 战列巡洋舰
    /// </summary>
    abstract class BC : BaseShip, IFire, IFireTwice
    {
        public void FireAttack(BaseShip ship)
        { }
        public void FireTwiceAttack(BaseShip ship)
        { }
    }
}
