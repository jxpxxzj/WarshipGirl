using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data
{
    public enum ResourceType
    {
        /// <summary>
        /// 钻石
        /// </summary>
        Gold = 1,
        /// <summary>
        /// 油
        /// </summary>
        Oil = 2,
        /// <summary>
        /// 钢
        /// </summary>
        Ammo = 3,
        /// <summary>
        /// 弹
        /// </summary>
        Steel = 4,
        BuildMaterial = 5,
        Exp = 6,
        ShipEnemy = 7,
        ShipExp = 8,
        /// <summary>
        /// 铝
        /// </summary>
        Aluminium = 9,
        /// <summary>
        /// 驱逐?
        /// </summary>
        SmallShip = 111,
        /// <summary>
        /// 轻巡?
        /// </summary>
        MediumShip = 112,
        /// <summary>
        /// 战列?
        /// </summary>
        BigShip = 113,
        /// <summary>
        /// 潜艇
        /// </summary>
        Submarine = 114,
        Equipment = 121,
        MainQuest = 131,
        NormalQuest = 132,
        WeekQuest = 133,
        /// <summary>
        /// 快速建造
        /// </summary>
        FastBuild = 141,
        /// <summary>
        /// 快速修理
        /// </summary>
        FastRepair = 541,
        BuildShipItem = 241,
        BuildEquipItem = 741,
        /// <summary>
        /// 誓约之戒
        /// </summary>
        LoveRing = 88841,
        /// <summary>
        /// 损管
        /// </summary>
        DamageManager = 66641
    }
}
