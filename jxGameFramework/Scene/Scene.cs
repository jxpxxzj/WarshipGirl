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
        public event EventHandler Load;
        public event EventHandler Unload;
        public new int Width
        {
            get
            {
                
                return Graphics.Instance.GraphicsDevice.Viewport.Width;
            }
        }
        public new int Height
        {
            get
            {
                
                return Graphics.Instance.GraphicsDevice.Viewport.Height;
            }
        }
        public override void Initialize()
        {
            //OnLoad(this,EventArgs.Empty);
            base.Width = Graphics.Instance.GraphicsDevice.Viewport.Width;
            base.Height = Graphics.Instance.GraphicsDevice.Viewport.Height;
            base.Initialize();
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

        public virtual void OnUnload(object sender, EventArgs e)
        {
            if (Unload != null)
                Unload(sender, e);
        }
    }
}
