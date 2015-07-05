using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jxGameFramework.Animations.Curve
{
    public class Elastic : BaseEasing
    {
        public override double EasingCurve(double Progress,bool PlayBack)
        {
            return Math.Pow(1024, Progress - 1) * Math.Sin(Progress * ((2 + 0.5) * Math.PI));
        }
    }
}
