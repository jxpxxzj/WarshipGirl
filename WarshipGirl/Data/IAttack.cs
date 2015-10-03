using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarshipGirl.Data;

namespace WarshipGirl.Data
{

    /// <summary>
    /// 航空战
    /// </summary>
    interface IBeginAir 
    {
         void BeginAirAttack(BaseShip ship) ;
    }
    /// <summary>
    /// 先制反潜
    /// </summary>
    interface IBeginAntiSubmarine 
    {
         void BeginAntiSubmarineAttack(BaseShip ship) ;
    }
    /// <summary>
    /// 先制鱼雷
    /// </summary>
    interface IBeginTorpedo 
    {
         void BeginTorpedoAttack(BaseShip ship) ;
    }
    /// <summary>
    /// 首轮炮击
    /// </summary>
    interface IFire 
    {
         void FireAttack(BaseShip ship) ;
    }
    /// <summary>
    /// 次轮炮击
    /// </summary>
    interface IFireTwice 
    {
         void FireTwiceAttack(BaseShip ship) ;
    }
    /// <summary>
    /// 鱼雷战
    /// </summary>
    interface ITorpedo
    {
         void TorpedoAttack(BaseShip ship) ;
    }
}
