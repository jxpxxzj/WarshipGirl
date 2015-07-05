using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jxGameFramework.Animations.Curve
{
    public abstract class BaseEasing
    {
        public abstract double EasingCurve(double Progress,bool PlayBack);
    }
}
