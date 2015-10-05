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
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (AutoSize)
                {
                    var v = Font.MeasureString(value);
                    this.Width = (int)v.X;
                    this.Height = (int)v.Y;
                }
                _text = value;

            }
        }
        public bool AutoSize { get; set; } = true;
        public Font Font { get; set; } = new Font(DefaultFontFileName,14);
        public uint Size
        {
            get
            {
                return Font.Size;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if(Text!=null)
                Font.DrawText(new Vector2(DrawingX,DrawingY),DestRect,Text, Color);
            base.Draw(gameTime);
        }
    }
}
