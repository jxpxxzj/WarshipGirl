using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace jxGameFramework.Components
{
    /// <summary>
    /// 游戏组件
    /// </summary>
    public abstract class Component : IComponent,IUpdateable 
    {
        public abstract bool Enabled { get; set; }
        public abstract int UpdateOrder { get; set; }

        public abstract event EventHandler<EventArgs> EnabledChanged;
        public abstract event EventHandler<EventArgs> UpdateOrderChanged;

        public abstract void Dispose();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
    }
    public abstract class DrawableComponent : Component,IDrawable
    {
        public abstract int DrawOrder { get; set; }
        public abstract bool Visible { get; set; }

        public abstract event EventHandler<EventArgs> DrawOrderChanged;
        public abstract event EventHandler<EventArgs> VisibleChanged;

        public abstract void Draw(GameTime gameTime);
    }

}
