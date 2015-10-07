using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace jxGameFramework.Controls
{
    //TODO: buggy
    //TODO: drag / horizontal scrollpanel
    //TODO: scroll bar event
    public class ScrollPanel : Control
    {
        int _maxheight=0;
        Texture2D _scroll;
        float _pos=0f;
        int _wheelvalue;
        public int ScrollSpeed { get; set; } = 75;
        public override void Initialize()
        {
            var gdip = new GDIpInterop(5, 30);
            gdip.g.FillRectangle(System.Drawing.Brushes.White, new System.Drawing.Rectangle(0, 0, 5, 32));
            _scroll = gdip.SaveTexture();
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            foreach(Sprite s in ChildSprites)
            {
                int t = s.Top;
                s.Top = t - (int)(_pos * (_maxheight - Height));
                if(s.Top<=Height && s.Top >=DrawingY-s.Height*2) 
                    s.Draw(gameTime);
                s.Top = t;
            }
            if (_maxheight > this.Height)
            {
                float value = DrawingY + _pos * (this.Height - 32);
                SpriteBatch.FillRectangle(new Rectangle(DrawingX + Width - 5, DrawingY, 5, Height), new Color(128, 128, 128, 200));
                SpriteBatch.Draw(_scroll, new Vector2(DrawingX + this.Width - 5, value), Color.White);
            }
#if DEBUG
            //Graphics.Instance.SpriteBatch.DrawRectangle(new Rectangle(this.DrawingX, this.DrawingY, this.Width, this.Height), Color.Black);
            //Graphics.Instance.SpriteBatch.DrawRectangle(DrawingRect, Color.Red);
#endif
        }
        public override void UpdateEvent(GameTime gameTime)
        {
            base.UpdateEvent(gameTime);
            if (_maxheight > this.Height)
            {
                var mState = Mouse.GetState();
                var rect = new Rectangle(DrawingX, DrawingY, Width, Height);
                if(rect.Contains(mState.Position))
                {
                    var p = mState.ScrollWheelValue;
                    var delta = (float)((1.0f * (p - _wheelvalue)) / Height);
                    var val = -delta * ScrollSpeed;
                    var res = _pos + (val / (_maxheight - Height));
                    if (res > 1)
                        res = 1;
                    if (res < 0)
                        res = 0;
                    _pos = res ;


                    _wheelvalue = p;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (Sprite s in ChildSprites)
            {
                if (_maxheight < s.Top + s.Height)
                    _maxheight = s.Top + s.Height + 5;

                int t = s.Top;
                s.Top = t - (int)(_pos * (_maxheight - Height));
                if (s.Top <= Height && s.Top >= DrawingY - s.Height * 2)
                    s.Update(gameTime);
                s.Top = t;
            }
            UpdateEvent(gameTime);
        }
    }
}
