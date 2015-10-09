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
    public class Component : IGameComponent,IUpdateable 
    {
        private bool _enabled = true;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                OnEnabledChanged(this, EventArgs.Empty);
            }
        }
        //TODO: implemention.
        public int UpdateOrder
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public Game Game
        {
            get
            {
                return Graphics.Instance.Game;
            }
        }
        public bool Initialized { get; protected set; }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public virtual void Dispose() { }
        public virtual void Initialize()
        {
            Initialized = true;
        }
        public virtual void Update(GameTime gameTime) { }
        protected virtual void OnEnabledChanged(object sender,EventArgs e)
        {
            if (EnabledChanged != null)
                EnabledChanged(sender, e);
        }
        protected virtual void OnUpdateOrderChanged(object sender,EventArgs e)
        {
            if (UpdateOrderChanged != null)
                UpdateOrderChanged(sender, e);
        }
    }
    public class DrawableComponent : Component,IDrawable
    {
        private bool _visible = true;

        //TODO: implemention.
        public int DrawOrder
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public bool Visible
        {
            get
            {
                return _visible;   
            }
            set
            {
                _visible = value;
                OnvisibleChanged(this, EventArgs.Empty);

            }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public virtual void Draw(GameTime gameTime) { }
        public SpriteBatch SpriteBatch
        {
            get
            {
                return Graphics.Instance.SpriteBatch;
            }
        }
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return Graphics.Instance.GraphicsDevice;
            }
        }

        protected virtual void OnvisibleChanged(object sender, EventArgs e)
        {
            if (VisibleChanged != null)
                VisibleChanged(sender, e);
        }
    }

}
