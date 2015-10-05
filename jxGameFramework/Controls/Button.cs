using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jxGameFramework.Controls
{
    public class Button : Control
    {
        Font _fnt;
        Label _title;
        
        public string Text
        {
            get
            {
                return _title.Text;
            }
            set
            {
                _title.Text = value;
            }
        }
        public Color BaseColor { get; set; } = new Color(15, 132, 166);
        public Color HoverColor { get; set; } = new Color(34, 151, 185);
        public Color ClickColor { get; set; } = Color.LightGray;
        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if(!isMouseDown)
                this.Color = HoverColor;
            base.OnMouseMove(sender, e);
        }
        protected override void OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.Color = BaseColor;
            base.OnMouseLeave(sender, e);
        }
        protected override void OnMouseDown(object sender, MouseEventArgs e)
        {
            this.Color = ClickColor;
            base.OnMouseDown(sender, e);
        }

        public override void Initialize()
        {
            GDIpInterop gdip = new GDIpInterop(this.Width+1, this.Height+1);
            var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.Rectangle(0,0,this.Width,this.Height), System.Drawing.Color.White,System.Drawing.Color.LightGray,90f);
            var pen = new System.Drawing.Pen(System.Drawing.Color.Black,1f);
            gdip.g.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));
            gdip.g.DrawRectangle(pen, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));
            this.Texture = gdip.SaveTexture();
            gdip.Dispose();

            _fnt = new Font(DefaultFontFileName, 18)
            {
                EnableShadow = true,
                ShadowColor = Color.Black,
                ShadowYOffset = 1,
            };

            _title = new Label()
            {
                Font = _fnt,
                Color = Color.White,
                Margin = Origins.Center,
            };

            ChildSprites.Add(_title);
            this.Color = BaseColor;
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            DrawToolStrip(gameTime);
        }
    }
}
