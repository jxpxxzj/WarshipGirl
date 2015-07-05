using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data
{
    interface IEquip1
    {
         BaseEquipment Equip1 { get; set; }
    }
    interface IEquip2 :IEquip1
    {
         BaseEquipment Equip2 { get; set; }
    }
    interface IEquip3 : IEquip2
    {
         BaseEquipment Equip3 { get; set; }
    }
    interface IEquip4 :IEquip3
    {
         BaseEquipment Equio4 { get; set; }
    }
}
