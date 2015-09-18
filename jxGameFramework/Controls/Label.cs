using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using Microsoft.Xna.Framework;

namespace jxGameFramework.Controls
{
    public class Label : Control
    {
        string _text="Label";
        Font _font;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                var v = _font.MeasureString(value);
                this.Width = (int)v.X;
                this.Height = (int)v.Y;
                _text = value;
            }
        }
        
        public Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            if(Text!=null)
                _font.DrawText(SpriteBatch, new Vector2(RenderX, RenderY), Text, Color);
            base.Draw(gameTime);
        }
    }
}
