using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data.Ship
{
    /// <summary>
    /// 驱逐舰
    /// </summary>
    abstract class DD : BaseShip, IFire, ITorpedo, IBeginAntiSubmarine
    {
        public void FireAttack(BaseShip ship) { }
        public void TorpedoAttack(BaseShip ship) { }
        public void BeginAntiSubmarineAttack(BaseShip ship) { }
    }
}
