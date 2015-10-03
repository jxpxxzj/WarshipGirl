using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace jxGameFramework.Components
{
    public interface IComponent : IDisposable
    {
        void Initialize();
    }
}
