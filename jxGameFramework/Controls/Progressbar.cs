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
    public enum ProgressBarTypes
    {
        HorizontalLine,
        VerticalLine,
        Circle
    }
    public class ProgressBar : Control
    {
        public int Value
        {
            get
            {
                return (int)((_max-_min + 1) * fvalue);
            }
            set
            {
                fvalue = value / (_max - _min + 1.0f);
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
                _max=value;
            }
        }
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

        int _max = 100;
        int _min = 1;

        public ProgressBarTypes Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                if(value==ProgressBarTypes.Circle)
                {
                    var radius = Math.Min(this.Width ,this.Height);
                    var gdip = new GDIpInterop(radius + EdgeWidth*2, radius + EdgeWidth*2, GraphicsDevice);
                    gdip.g.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.FromArgb(200, EdgeColor.R, EdgeColor.G, EdgeColor.B), EdgeWidth), new System.Drawing.Rectangle(EdgeWidth, EdgeWidth, radius, radius));

                    _cirquebg = gdip.SaveTexture();
                    gdip.Dispose();
                }

            }
        }

        ProgressBarTypes _type = ProgressBarTypes.HorizontalLine;
        Texture2D _cirquebg;
        public Color FillColor
        {
            get
            {
                return _fill;
            }
            set
            {
                _fill = value;
            }
        }
       
        public Color EdgeColor
        {
            get
            {
                return _edge;
            }
            set
            {
                _edge = value;
            }
        }

        Color _fill = Color.DeepPink;
        Color _edge = Color.DeepPink;

        public int EdgeWidth { get; set; }

        float fvalue=0.5f;

        public override void Draw(GameTime gameTime)
        {
            Rectangle rect;
            Rectangle progrect;
            switch (Type)
            {
                case ProgressBarTypes.HorizontalLine:
                     rect = new Rectangle(this.RenderX, this.RenderY, this.Width, this.Height);
                     progrect = new Rectangle(this.RenderX, this.RenderY, (int)(this.Width * fvalue), this.Height);
                     SpriteBatch.FillRectangle(progrect, FillColor);
                     SpriteBatch.DrawRectangle(rect, EdgeColor, EdgeWidth);
                     break;
                case ProgressBarTypes.VerticalLine:
                    rect = new Rectangle(this.RenderX, this.RenderY, this.Width, this.Height);
                    progrect = new Rectangle(this.RenderX, this.RenderY, this.Width, (int)(this.Height * fvalue));
                    SpriteBatch.FillRectangle(progrect, FillColor);
                    SpriteBatch.DrawRectangle(rect, EdgeColor, EdgeWidth);
                    break;
                case ProgressBarTypes.Circle:
                    var radius = Math.Min(Width, Height) / 2;
                    SpriteBatch.DrawArc(new Vector2(RenderX + radius, RenderY + radius), radius - EdgeWidth, 512, MathHelper.Pi + MathHelper.PiOver2, (float)(MathHelper.Pi * fvalue * 2), FillColor, radius - EdgeWidth);
                    SpriteBatch.Draw(_cirquebg,new Rectangle(RenderX,RenderY,radius *2 ,radius * 2),Color.White);
                    break;
                default:
                    break;
            }        
            base.Draw(gameTime);
        }
    }
}

