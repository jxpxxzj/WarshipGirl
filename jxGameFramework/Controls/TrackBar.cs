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
        public Color LineColor { get; set; } = DefaultFocusColor;
        public Color CircleColor { get; set; } = DefaultFocusColor;

        public TrackBar(int width)
        {
            this.Width = width;
        }

        public int MinValue { get; set; } = 0;
        public int MaxValue { get; set; } = 100;
     
        public int Value { get; set; }
        public string ToolStripFormat { get; set; } = "{0}";
        double _value = 0;
        public override void Initialize()
        {
            var gdip = new GDIpInterop(this.Width, 16);
            gdip.g.DrawLine(System.Drawing.Pens.White, new System.Drawing.Point(0, 7), new System.Drawing.Point(this.Width, 7));
            gdip.g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(10, 255, 255, 255)), new System.Drawing.Rectangle(0, 0, this.Width, 15));
            this.Texture = gdip.SaveTexture();
            this.Color = LineColor;
            this.Height = 16;
            gdip.g.Clear(System.Drawing.Color.Transparent);
            gdip.g.FillEllipse(System.Drawing.Brushes.White, new System.Drawing.Rectangle(0, 0, 15, 15));
            _scrollcircle= gdip.SaveTexture();
            gdip.g.Dispose();
            this.MouseMove += TrackBar_MouseMove;
            this.MouseDown += TrackBar_MouseDown;
            this.KeyDown += TrackBar_KeyDown;
            Value = MinValue;
            base.Initialize();
        }

        void TrackBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (MouseInRect)
            {
                if(e.State.GetPressedKeys()[0] == Microsoft.Xna.Framework.Input.Keys.Right && Value < (MaxValue-1))
                    Value++;
                if (e.State.GetPressedKeys()[0] == Microsoft.Xna.Framework.Input.Keys.Left && Value > MinValue)
                    Value--;
                _value = (double)(Value - MinValue) / (MaxValue - MinValue); 
                this.ToolStrip = string.Format(ToolStripFormat, Value);
            }
            OnKeyDown(sender, e);
        }

        void TrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(sender, e);
        }

        void TrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if(isMouseDown)
            {
                this._value = (double) e.State.X / this.Width;
                Value = MinValue + (int)((MaxValue - MinValue) * _value);
                this.ToolStrip = string.Format(ToolStripFormat,Value);
            }
            OnMouseMove(sender, e);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Graphics.Instance.SpriteBatch.Draw(_scrollcircle, new Vector2(DrawingX + (int)(this.Width * _value) - 7, DrawingY), CircleColor);
            DrawToolStrip(gameTime);
        }
        
    }
}
