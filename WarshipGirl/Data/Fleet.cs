using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data
{
    class Fleet
    {
        public List<BaseShip> Ship = new List<BaseShip>();

        public string Name { get; set; }
    }
}
