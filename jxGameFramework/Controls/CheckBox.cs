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
    public class CheckBox : Control
    {
        Control _checkcircle;
        Label _title;
        Font _fnt;

        Texture2D full;
        Texture2D empty;

        public Color CircleColor { get; set; } = Color.DeepPink;
  
        public string Text
        {
            get
            {
                return _title.Text;
            }
            set
            {
                _title.Text = value;
                this.Width = _title.Width + full.Width;
            }
        }
        bool _checked = false;
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                if(_checked)
                {
                    _checkcircle.Texture = full;
                }
                else
                {
                    _checkcircle.Texture = empty;
                }
            }
        }

        public override void Initialize()
        {
            GDIpInterop gdip = new GDIpInterop(20, 20);
            gdip.g.FillEllipse(System.Drawing.Brushes.White, 2, 2, 16, 16);
            full = gdip.SaveTexture();

            gdip.g.Clear(System.Drawing.Color.Transparent);
            System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Color.White, 2);
            gdip.g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(10,255,255,255)), 2, 2, 16, 16);
            gdip.g.DrawEllipse(p, 2, 2, 16, 16);
            empty = gdip.SaveTexture();

            _fnt = new Font("msyh.ttc", 15)
            {
                EnableShadow = true,
                ShadowColor = Color.Black,
                ShadowYOffset = 1,
            };
            _checkcircle = new Control()
            {
                Texture = empty,
                Margin=Origins.CenterLeft,
                Left = 0,
                Color=CircleColor
            };
            _checkcircle.Width = _checkcircle.Texture.Width;
            _checkcircle.Height = _checkcircle.Texture.Height;
            _checkcircle.Click += CheckBox_Click;

            _title = new Label()
            {
                Font = _fnt,
                Color = Color.White,
                Margin = Origins.CenterLeft,
                Left = 22,
            };
            ChildSprites.Add(_title);
            ChildSprites.Add(_checkcircle);
            this.Width = full.Width;
            this.Height = full.Height;
            base.Initialize();
        }

        void CheckBox_Click(object sender, EventArgs e)
        {
            this.Checked = !this.Checked;
        }        
    }
}
