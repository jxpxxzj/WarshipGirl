using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jxGameFramework.Controls;
using Microsoft.Xna.Framework;

namespace jxGameFramework.Scene
{
    public class BaseScene : Control
    {
        internal bool _hasInitalized = false;
        public event EventHandler Load;
        public event EventHandler Show;
        public event EventHandler Leave;
        public new int Width
        {
            get
            {
                
                return GraphicsDevice.Viewport.Width;
            }
        }
        public new int Height
        {
            get
            {
                return GraphicsDevice.Viewport.Height;
            }
        }
        public override void Initialize()
        {
            if (_hasInitalized)
                return;
            //OnLoad(this,EventArgs.Empty);
            base.Width = GraphicsDevice.Viewport.Width;
            base.Height = GraphicsDevice.Viewport.Height;
            base.Initialize();
            _hasInitalized = true;
            OnLoad(this, EventArgs.Empty);
        }
        public override void Dispose()
        {
            //OnUnload(this, EventArgs.Empty);
            base.Dispose();
        }
        public virtual void OnLoad(object sender, EventArgs e)
        {
            if (Load != null)
                Load(sender, e);
        }
        public virtual void OnShow(object sender,EventArgs e)
        {
            if (Show != null)
                Show(sender, e);
        }
        public virtual void OnLeave(object sender,EventArgs e)
        {
            if (Leave != null)
                Leave(sender, e);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
