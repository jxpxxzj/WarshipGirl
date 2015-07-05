using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data
{
    class War
    {
        public enum WarResult
        {
            SS,S,A,B,C,D
        }
        public Fleet Fleet1 { get; set; }
        public Fleet Fleet2 { get; set; }

        public War(Fleet fleet1,Fleet fleet2)
        {
            this.Fleet1=fleet1;
            this.Fleet2=fleet2;
        }

        public WarResult Fight()
        {
            return WarResult.SS;
        }
    }
}
