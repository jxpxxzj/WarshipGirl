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
    //TODO: overflow
    public class ScrollPanel : Control
    {
        int _maxheight=0;
        Texture2D _scroll;
        float _pos=0f;
        int _wheelvalue;
        int _speed=50;
        public int ScrollSpeed
        { 
            get
            { 
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }
        public override void AddComponent(Sprite comp)
        {
            base.AddComponent(comp);
            if (_maxheight + Height < comp.Top + comp.Height)
                _maxheight = comp.Top + comp.Height - Height + 5;
        }
        public override void LoadContent()
        {
            var gdip = new GDIpInterop(5, 30, GraphicsDevice);
            gdip.g.FillRectangle(System.Drawing.Brushes.White, new System.Drawing.Rectangle(0, 0, 5, 32));
            _scroll = gdip.SaveTexture();
            //base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            foreach(Sprite s in CompList)
            {
                int t = s.Top;
                s.Top = t - (int)(_pos * _maxheight);
                if(s.Top<=Height && s.Top >=RenderY-s.Height*2) 
                    s.Draw(gameTime);
                s.Top = t;
            }
            //SpriteBatch.DrawRectangle(new Rectangle(RenderX, RenderY, Width, Height), Color.Black);
            if (_maxheight > this.Height)
            {
                float value = RenderY + _pos * (this.Height - 32);
                SpriteBatch.FillRectangle(new Rectangle(RenderX + Width - 5, RenderY, 5, Height), new Color(128, 128, 128, 200));
                SpriteBatch.Draw(_scroll, new Vector2(RenderX + this.Width - 5, value), Color.White);
            }
        }
        public override void UpdateEvent(GameTime gameTime)
        {
            base.UpdateEvent(gameTime);
            var mState = Mouse.GetState();
            var rect = new Rectangle(RenderX, RenderY, Width, Height);
            if(rect.Contains(mState.Position))
            {
                var p = mState.ScrollWheelValue;
                var delta = (float)((1.0f * (p - _wheelvalue)) / Height);
                var val = -delta * _speed;
                var res = _pos + (val / _maxheight);
                if (res > 1)
                    res = 1;
                if (res < 0)
                    res = 0;
                _pos = res;


                _wheelvalue = p;
            }

        }
        public override void Update(GameTime gameTime)
        {
            foreach (Sprite s in CompList)
            {
                int t = s.Top;
                s.Top = t - (int)(_pos * _maxheight);
                if (s.Top <= (Top + Height) && s.Top >= (this.Top - s.Height))
                    s.Update(gameTime);
                s.Top = t;
            }
            UpdateEvent(gameTime);
        }
    }
}
