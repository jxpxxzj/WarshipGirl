using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data
{
    class BaseEquipment
    {
        /// <summary>
        /// 装备类型
        /// </summary>
        public enum EquipmentType
        {
            Cannon,Aircraft,Submarine,Others
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 稀有度
        /// </summary>
        public int Star { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public EquipmentType Type { get; set; }
        /// <summary>
        /// 火力
        /// </summary>
        public int Fire { get; set; }
        /// <summary>
        /// 射程
        /// </summary>
        public int Range { get; set; }
        /// <summary>
        /// 对空
        /// </summary>
        public int Air { get; set; }
        /// <summary>
        /// 对潜
        /// </summary>
        public int Submarine { get; set;  }
        /// <summary>
        /// 命中
        /// </summary>
        public int Accuracy { get; set; }
        /// <summary>
        /// 装甲
        /// </summary>
        public int Protect { get; set; }
        /// <summary>
        /// 闪避
        /// </summary>
        public int Evade { get; set; }
        /// <summary>
        /// 幸运
        /// </summary>
        public int Fortune { get; set; }
        /// <summary>
        /// 索敌
        /// </summary>
        public int Searching { get; set; }

    }
}
