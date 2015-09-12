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
    public class TrackBar : Control
    {
        Texture2D _scrollcircle;

        Color _line = Color.DeepPink;
        Color _circle = Color.DeepPink;
        public Color LineColor
        {
            get
            {
                return _line;
            }
            set
            {
                _line = value;
            }
        }
        public Color CircleColor
        {
            get
            {
                return _circle;
            }
            set
            {
                _circle = value;
            }
        }

        public TrackBar(int width)
        {
            this.Width = width;
        }

        int _min = 0;
        int _max = 100;

        public int MinValue
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
            }
        }

        public int MaxValue
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value + 1;
            }
        }
        int _uservalue;
        public int Value
        {
            get
            {
                return _uservalue;
            }
            set
            {
                _uservalue = value;
            }
        }
        string _format = "{0}";
        public string ToolStripFormat
        {
            get
            {
                return _format;
            }
            set
            {
                _format = value;
            }
        }
        double _value = 0;
        public override void LoadContent()
        {
            var gdip = new GDIpInterop(this.Width, 16, GraphicsDevice);
            gdip.g.DrawLine(System.Drawing.Pens.White, new System.Drawing.Point(0, 7), new System.Drawing.Point(this.Width, 7));
            gdip.g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(10, 255, 255, 255)), new System.Drawing.Rectangle(0, 0, this.Width, 15));
            this.Texture = gdip.SaveTexture();
            this.Color = _line;
            this.Height = 16;
            gdip.g.Clear(System.Drawing.Color.Transparent);
            gdip.g.FillEllipse(System.Drawing.Brushes.White, new System.Drawing.Rectangle(0, 0, 15, 15));
            _scrollcircle= gdip.SaveTexture();
            gdip.g.Dispose();
            this.MouseMove += TrackBar_MouseMove;
            this.MouseDown += TrackBar_MouseDown;
            this.KeyDown += TrackBar_KeyDown;
            _uservalue = MinValue;
            base.LoadContent();
        }

        void TrackBar_KeyDown(object sender, XnaKeyEventArgs e)
        {
            if (MouseInRect)
            {
                if(e.State.GetPressedKeys()[0] == Microsoft.Xna.Framework.Input.Keys.Right && Value < (MaxValue-1))
                    Value++;
                if (e.State.GetPressedKeys()[0] == Microsoft.Xna.Framework.Input.Keys.Left && Value > MinValue)
                    Value--;
                _value = (double)(Value - MinValue) / (MaxValue - MinValue); 
                this.ToolStrip = string.Format(ToolStripFormat, _uservalue);
            }      
        }

        void TrackBar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
        }

        void TrackBar_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(isMouseDown)
            {
                this._value = (double) e.X / this.Width;
                _uservalue = MinValue + (int)((MaxValue - MinValue) * _value);
                this.ToolStrip = string.Format(ToolStripFormat,_uservalue);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.Draw(_scrollcircle, new Vector2(RenderX + (int)(this.Width * _value) - 7, RenderY), _circle);
            DrawToolStrip(gameTime);
        }
        
    }
}
