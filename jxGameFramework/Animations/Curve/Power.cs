using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace jxGameFramework.Animations.Curve
{
    public class Power : BaseEasing
    {
        public double PowerValue=1;
        public override double EasingCurve(double Progress,bool PlayBack)
        {
            if(PlayBack)
            {
                double k = Math.Pow(2, PowerValue);
                if (Progress <= 0.5)
                    return k*Math.Pow(Progress, PowerValue);
                else
                    return k*Math.Pow((1-Progress), PowerValue);
            }

            else
                return Math.Pow(Progress, PowerValue);
            
        }
    }
}
