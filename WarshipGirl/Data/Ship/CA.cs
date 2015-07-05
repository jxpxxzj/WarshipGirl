using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data.Ship
{
    /// <summary>
    /// 重巡洋舰
    /// </summary>
    abstract class CA : BaseShip, IFire, ITorpedo
    {
        public void FireAttack(BaseShip ship) { }
        public void TorpedoAttack(BaseShip ship) { }

    }
}
