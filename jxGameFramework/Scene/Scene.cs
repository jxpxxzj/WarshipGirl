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
        public Game ParentGame;
        public override void LoadContent()
        {
            //OnLoad(this,EventArgs.Empty);
            base.LoadContent();
        }
        public override void UnloadContent()
        {
            //OnUnload(this, EventArgs.Empty);
            base.UnloadContent();
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
