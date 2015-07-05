using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace WarshipGirl.Server.OriginalData
{
    public class ships
    {
        public ships()
        { }
        public ships(object[] itemarray)
        {
            int i=0;
            foreach (FieldInfo info in typeof(ships).GetFields())
            {
                string name = info.Name;
                var field = typeof(ships).GetField(name);
                var value = Convert.ChangeType(itemarray[i], field.FieldType);   
                field.SetValue(this, value);
                i++;
            }
        }
        public int FOCUS;
        public int ZHEN_CHA_MAX;
        public int SHAN_BI;
        public string SKILL;
        public int ID;
        public int DAN;
        public int DUI_KONG;
        public int ATTACK_MAX;
        public int LIFE_MAX;
        public string TYPE;
        public bool CAN_BUILD;
        public string WEDDING_TALK;
        public string NAME;
        public double OIL_COST;
        public int ZHEN_CHA;
        public string GAI_ZAO_USE;
        public string EXP;
        public string SKILL_NAME;
        public bool IS_OPENED;
        public int ATTACK;
        public int YOU;
        public string SKILL_INFO;
        public int LIFE;
        public int DUI_QIAN;
        public int SHAN_BI_MAX;
        public int ITEM_NUM;
        public double TIME_COST;
        public string GETTING_TALK;
        public string PIN_YIN;
        public string SHE_CHENG;
        public string NO;
        public double SU_DU;
        public int DUI_KONG_MAX;
        public int DEFENCE;
        public string GAI_ZAO;
        public int ROCKETS_MAX;
        public string BUILD_TIME;
        public int XING_YUN;
        public string SELL;
        public string LEVEL;
        public string NI_CHENG;
        public int DUI_QIAN_MAX;
        public int STARS;
        public int DA_ZAI;
        public int ROCKETS;
        public string DESC;
        public double GANG_COST;
        public string FEN_BU;
        public string NATION;
        public string WEAPON;
        public int DEFENCE_MAX;

        public string ToSQLValue()
        {
            string s = string.Empty;
            s += "values(";
            foreach (FieldInfo info in typeof(ships).GetFields())
            {
                s += "'" + info.GetValue(this) + "',";
            }
            s=s.Substring(0, s.Length - 1);
            s += ")";
            return s;
        }
    }
}
