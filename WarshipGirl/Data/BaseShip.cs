using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarshipGirl.Server.OriginalData;

namespace WarshipGirl.Data
{
    public class BaseShip
    {
        public BaseShip()
        { }
        public BaseShip(ships od)
        {
            this.Air=od.DUI_KONG;
            this.Buildable=od.CAN_BUILD;
            if(this.Buildable && (od.BUILD_TIME != "-"))
            {
                this.BuildTime=TimeSpan.Parse(od.BUILD_TIME);
            }
            this.Bullet=od.DAN;
            this.Capacity=od.DA_ZAI;
            this.Description=od.DESC;
            this.Evade=od.SHAN_BI;
            this.FirePower=od.ATTACK;
            this.Fortune=od.XING_YUN;
            this.GettingTalk=od.GETTING_TALK;
            this.Health=od.LIFE;
            this.ID=od.ID;
            this.Level=od.LEVEL;
            this.Name=od.NAME;
            this.No=od.NO;
            this.Oil=od.YOU;
            this.Protect=od.DEFENCE;
            this.Range=RangeParse(od.SHE_CHENG);
            this.RepairIronCost=od.GANG_COST;
            this.RepairOilCost=od.OIL_COST;
            this.RepairTimeCost=od.TIME_COST;
            this.Search=od.ZHEN_CHA;
            this.Speed=od.SU_DU;
            this.Stars=od.STARS;
            this.Submarine=od.DUI_QIAN;
            this.Torpedo=od.ROCKETS;
            this.Type=TypeParse(od.TYPE);
            this.WeddingTalk=od.WEDDING_TALK;
        }
        public static BaseShip CreateFromDB(ships od)
        {
            return new BaseShip(od);
        }
        private ShipType TypeParse(string type)
        {
            ShipType target;
            switch (type)
	        {
                case "战列巡洋舰":target=ShipType.BC; break;
                case "战列舰" :target=ShipType.BB; break;
                case "正规航母" :target=ShipType.CV;break;
                case "轻型航母":target=ShipType.CVL;break;
                case "重巡洋舰":target=ShipType.CA;break;
                case "轻巡洋舰":target=ShipType.CL;break;
                case "驱逐舰":target=ShipType.DD;break;
                case "浅水重炮舰":target=ShipType.BM;break;
                case "潜艇":target=ShipType.SS;break;
                case "水上机母舰":target=ShipType.AV;break;

		        default: throw new NotImplementedException();
	        }
            return target;
        }
        public static string ShipTypeToString(ShipType type)
        {
            switch (type)
            {
                case ShipType.BB: return "战列舰";
                    //break;
                case ShipType.BC: return "战列巡洋舰";
                    //break;
                case ShipType.CA: return  "重巡洋舰" ;
                   // break;
                case ShipType.CL: return "轻巡洋舰"  ;
                    //break;
                case ShipType.CV: return "正规航母" ;
                    //break;
                case ShipType.CVL: return "轻型航母";
                    //break;
                case ShipType.AV: return "水上机母舰";
                    //break;
                case ShipType.DD: return "驱逐舰";
                    //break;
                case ShipType.SS: return "潜艇" ;
                    //break;
                case ShipType.BM: return "浅水重炮舰";
                    //break;
                default:
                    throw new NotImplementedException();
            }
        }
        private FireRange RangeParse(string range)
        {
            FireRange target;
            switch (range)
	        {
                case "长" : target=FireRange.Long;break;
                case "中": target=FireRange.Medium;break;
                case "短": target=FireRange.Short;break;

		        default:throw new NotImplementedException();
	        }
            return target;
        }
        /// <summary>
        /// 火炮射程
        /// </summary>
        public enum FireRange
        {
            Long,Medium,Short
        }
        /// <summary>
        /// 舰船类型
        /// </summary>
        public enum ShipType
        {
            BB,BC,CA,CL,CV,CVL,AV,DD,SS,BM
        }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 稀有度
        /// </summary>
        public int Stars { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public ShipType Type { get; set; }
        /// <summary>
        /// 耐久
        /// </summary>
        public int Health { get; set; }
        /// <summary>
        /// 火力
        /// </summary>
        public int FirePower { get; set; }
        /// <summary>
        /// 搭载
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// 护甲
        /// </summary>
        public int Protect { get; set; }
        /// <summary>
        /// 鱼雷
        /// </summary>
        public int Torpedo { get; set; }
        /// <summary>
        /// 幸运
        /// </summary>
        public int Fortune { get; set; }
        /// <summary>
        /// 闪避
        /// </summary>
        public int Evade { get; set; }
        /// <summary>
        /// 对空
        /// </summary>
        public int Air { get; set; }
        /// <summary>
        /// 射程
        /// </summary>
        public FireRange Range { get; set; }
        /// <summary>
        /// 索敌
        /// </summary>
        public int Search { get; set; }
        /// <summary>
        /// 对潜
        /// </summary>
        public int Submarine { get; set; }
        /// <summary>
        /// 航速
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// 出征油耗
        /// </summary>
        public int Bullet { get; set; }
        /// <summary>
        /// 出征弹耗
        /// </summary>
        public int Oil { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 获得台词
        /// </summary>
        public string GettingTalk { get; set; }
        /// <summary>
        /// 结婚台词
        /// </summary>
        public string WeddingTalk { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 修理油耗
        /// </summary>
        public double RepairOilCost { get; set; }
        /// <summary>
        /// 修理钢耗
        /// </summary>
        public double RepairIronCost { get; set; }
        /// <summary>
        /// 修理时耗
        /// </summary>
        public double RepairTimeCost { get; set; }

        /// <summary>
        /// 可建造
        /// </summary>
        public bool Buildable { get; set; }
        /// <summary>
        /// 建造时间
        /// </summary>
        public TimeSpan BuildTime { get; set; }


    }
}
