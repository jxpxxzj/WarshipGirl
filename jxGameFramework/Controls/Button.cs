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

namespace jxGameFramework.Controls
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
        Color _hover = new Color(34, 151, 185);
        Color _click = Color.LightGray;
        Color _base = new Color(15, 132, 166);
        public Color BaseColor
        {
            get
            {
                return _base;
            }
            set
            {
                _base = value;
            }
        }
        public Color HoverColor 
        { 
            get
            {
                return _hover;
            }
            set
            {
                _hover = value;
            }
        }
        public Color ClickColor
        {
            get
            {
                return _click;
            }
            set
            {
                _click = value;
            }
        }
        protected override void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Color = _hover ;
            base.OnMouseMove(sender, e);
        }
        protected override void OnMouseLeave(object sender, EventArgs e)
        {
            this.Color = _base;
            base.OnMouseLeave(sender, e);
        }
        protected override void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Color = _click;
            base.OnMouseDown(sender, e);
        }

        public override void LoadContent()
        {
            GDIpInterop gdip = new GDIpInterop(this.Width+1, this.Height+1, GraphicsDevice);
            var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.Rectangle(0,0,this.Width,this.Height), System.Drawing.Color.White,System.Drawing.Color.LightGray,90f);
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
