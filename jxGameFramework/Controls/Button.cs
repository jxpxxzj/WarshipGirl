using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontTest
{
    public class Button : Control
    {
        Font _fnt;
        Text _title;
        
        public string Text
        {
            get
            {
                return _title.text;
            }
            set
            {
                _title.text = value;
            }
        }

        public override void LoadContent()
        {
            GDIpInterop gdip = new GDIpInterop(this.Width+1, this.Height+1, GraphicsDevice);
            var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.Rectangle(0,0,this.Width,this.Height), System.Drawing.Color.FromArgb(15,115,144),System.Drawing.Color.FromArgb(14,132,166),90f);
            var pen = new System.Drawing.Pen(System.Drawing.Color.Black,3f);
            gdip.g.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));
            gdip.g.DrawRectangle(pen, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));
            this.Texture = gdip.SaveTexture();
            gdip.Dispose();

            _fnt = new Font(GraphicsDevice, "msyh.ttc", 15)
            {
                EnableShadow = true,
                ShadowColor = Color.Black,
                ShadowYOffset = 1,
            };

            _title = new Text()
            {
                Font = _fnt,
                Color = Color.White,
                OriginType = Origins.Center,
                X=this.Width / 2,
                Y=this.Height / 2 - 2,
            };

            AddComponent(_title);

            base.LoadContent();
        }
    }
}
