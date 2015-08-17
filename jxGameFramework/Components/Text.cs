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
    /// <summary>
    /// 文本
    /// </summary>
    public class Text : Component
    {
        private string _text;
        /// <summary>
        /// 文字
        /// </summary>
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
        public Sprite Parent { get; set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Color Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int RenderX { get; set; }
        public int RenderY { get; set; }
        private int ParentRenderX
        {
            get
            {
                if (Parent != null)
                    return Parent.RenderX;
                else
                    return 0;
            }
        }
        private int ParentRenderY
        {
            get
            {
                if (Parent != null)
                    return Parent.RenderY;
                else
                    return 0;
            }
        }
        public Origins OriginType { get; set; }
        public Vector2 CustomOriginPoint { get; set; }
        public Font Font { get; set; }
        public override void Draw(GameTime gameTime)
        {
            switch (OriginType)
            {
                case Origins.TopLeft: RenderX = ParentRenderX + X; RenderY = ParentRenderY + Y;
                    break;
                case Origins.TopCenter: RenderX = ParentRenderX + X - Width / 2; RenderY = ParentRenderY + Y;
                    break;
                case Origins.TopRight: RenderX = ParentRenderX + X - Width; RenderY = ParentRenderY + Y;
                    break;
                case Origins.CenterLeft: RenderX = ParentRenderX + X; RenderY = ParentRenderY + Y - Height / 2;
                    break;
                case Origins.Center: RenderX = ParentRenderX + X - Width / 2; RenderY = ParentRenderY + Y - Height / 2;
                    break;
                case Origins.CenterRight: RenderX = ParentRenderX + X - Width; RenderY = ParentRenderY + Y - Height / 2;
                    break;
                case Origins.BottomLeft: RenderX = ParentRenderX + X; RenderY = ParentRenderY + Y - Height;
                    break;
                case Origins.BottomCenter: RenderX = ParentRenderX + X - Width / 2; RenderY = ParentRenderY + Y - Height;
                    break;
                case Origins.BottomRight: RenderX = ParentRenderX + X - Width; RenderY = ParentRenderY + Y - Height;
                    break;
                case Origins.Custom: RenderX = ParentRenderX + X - Width * (int)CustomOriginPoint.X; RenderY = ParentRenderY + Y - Height * (int)CustomOriginPoint.Y;
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
