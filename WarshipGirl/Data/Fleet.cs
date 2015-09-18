using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data
{
    class Fleet
    {
        public int ID { get; set; }
        public List<BaseShip> Ships = new List<BaseShip>();

        public string Name { get; set; }
    }
}
