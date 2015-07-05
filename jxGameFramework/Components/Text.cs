using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using jxGameFramework.Data;

namespace jxGameFramework.Components
{
    public class Text : Component
    {
        private string _text;
        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                if(Font!=null)
                {
                    var vect = Font.MeasureString(value);
                    Width = (int)vect.X;
                    Height = (int)vect.Y;
                } 
            }
        }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Color Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int RenderX { get; set; }
        public int RenderY { get; set; }
        public Origins OriginType { get; set; }
        public Vector2 CustomOriginPoint { get; set; }
        public Font Font { get; set; }
        public override void Draw(GameTime gameTime)
        {
            switch (OriginType)
            {
                case Origins.TopLeft: RenderX = X; RenderY = Y;
                    break;
                case Origins.TopCenter: RenderX = X - Width - 2; RenderY = Y;
                    break;
                case Origins.TopRight: RenderX = X - Width; RenderY = Y;
                    break;
                case Origins.CenterLeft: RenderX = X; RenderY = Y - Height / 2;
                    break;
                case Origins.Center: RenderX = X - Width / 2; RenderY = Y - Height / 2;
                    break;
                case Origins.CenterRight: RenderX = X - Width; RenderY = Y - Height / 2;
                    break;
                case Origins.BottomLeft: RenderX = X; RenderY = Y - Height;
                    break;
                case Origins.BottomCenter: RenderX = X - Width / 2; RenderY = Y - Height;
                    break;
                case Origins.BottomRight: RenderX = X - Width; RenderY = Y - Height;
                    break;
                case Origins.Custom: RenderX = X - Width * (int)CustomOriginPoint.X; RenderY = Y - Height * (int)CustomOriginPoint.Y;
                    break;
                default:
                    break;
            }
            Font.DrawText(SpriteBatch, new Vector2(RenderX,RenderY), text, Color);
        }
        public override void LoadContent()
        {
         
        }
        public override void UnloadContent()
        {

        }
        public override void Update(GameTime gameTime)
        {

        }
    }
}
