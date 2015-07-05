using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace jxGameFramework.Animations.Curve
{
    public class Sine : BaseEasing
    {
        public override double EasingCurve(double Progress,bool PlayBack)
        {
            if(PlayBack)
                return 0.5 * Math.Sin(2*Math.PI * (Progress - 0.25)) + 0.5;
            else
                return 0.5 * Math.Sin(Math.PI * (Progress - 0.5)) + 0.5;
        }
    }
}
