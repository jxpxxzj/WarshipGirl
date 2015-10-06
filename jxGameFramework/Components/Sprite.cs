using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using jxGameFramework.Animations;
using jxGameFramework.Data;
using System.Windows;
using System.IO;

namespace jxGameFramework.Components
{
    /// <summary>
    /// 定位方式
    /// </summary>
    public enum Origins
    {
        TopLeft,
        TopCenter,
        TopRight,

        CenterLeft,
        Center,
        CenterRight,

        BottomLeft,
        BottomCenter,
        BottomRight,

        Custom,
    }
    /// <summary>
    /// 精灵
    /// </summary>
    public class Sprite : DrawableComponent
    {
        public Sprite()
        {
            ChildSprites = new SpriteCollection(this);
        }
        public Sprite Parent { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public override bool Visible { get; set; } = true;
        public override bool Enabled { get; set; } = true;
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public float LayerDepth { get; set; }
        public Origins Margin { get; set; } = Origins.TopLeft;
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; } = Color.White;
        public List<Animation> AnimList = new List<Animation>();
        public SpriteCollection ChildSprites { get; set; }
        public override int DrawOrder { get; set; }
        public override int UpdateOrder { get; set; }
        public override event EventHandler<EventArgs> DrawOrderChanged;
        public override event EventHandler<EventArgs> VisibleChanged;
        public override event EventHandler<EventArgs> EnabledChanged;
        public override event EventHandler<EventArgs> UpdateOrderChanged;

        Texture2D _texture;
        Color[] _texturepixel;
        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
                _texturepixel = new Color[value.Width * value.Height];
                value.GetData<Color>(_texturepixel);
            }
        }
        private int ParentWidth
        {
            get
            {
                if (Parent != null)
                    return Parent.Width;
                else
                    return Graphics.Instance.GraphicsDevice.Viewport.Width;
            }
        }
        private int ParentHeight
        {
            get
            {
                if (Parent != null)
                    return Parent.Height;
                else
                    return Graphics.Instance.GraphicsDevice.Viewport.Height;
            }
        }
        private int ParentDrawingX
        {
            get
            {
                if (Parent != null)
                    return Parent.DrawingX;
                else
                    return 0;
            }
        }
        private int ParentDrawingY
        {
            get
            {
                if (Parent != null)
                    return Parent.DrawingY;
                else
                    return 0;
            }
        }
        public int DrawingX
        {
            get
            {
                return ParentDrawingX + X;
            }
        }
        public int DrawingY
        {
            get
            {
                return ParentDrawingY + Y;
            }
        }
        private int DestX
        {
            get
            {
                if (DrawingX < ParentDrawingX)
                    return ParentDrawingX;
                else
                    return DrawingX;
            }
        }
        private int DestY
        {
            get
            {
                if (DrawingY < ParentDrawingY)
                    return ParentDrawingY;
                else
                    return DrawingY;
            }
        }
        public int X
        {
            get
            {
                switch (Margin)
                {
                    case Origins.TopLeft: return Left;
                    case Origins.TopCenter: return (ParentWidth - Width) / 2 - Right + Left;
                    case Origins.TopRight: return ParentWidth - Right - Width;
                    case Origins.CenterLeft: return Left;
                    case Origins.Center: return (ParentWidth - Width) / 2 - Right + Left;
                    case Origins.CenterRight: return ParentWidth - Right - Width;
                    case Origins.BottomLeft: return Left;
                    case Origins.BottomCenter: return (ParentWidth - Width) / 2 - Right + Left;
                    case Origins.BottomRight: return ParentWidth - Right - Width;
                    default: return Left; 
                }
            }
        }
        public int Y
        {
            get
            {
                switch (Margin)
                {
                    case Origins.TopLeft: return Top;
                    case Origins.TopCenter: return Top;
                    case Origins.TopRight: return Top;
                    case Origins.CenterLeft: return (ParentHeight - Height) / 2 - Bottom + Top;
                    case Origins.Center: return (ParentHeight - Height) / 2 - Bottom + Top;
                    case Origins.CenterRight: return (ParentHeight - Height) / 2 - Bottom + Top;
                    case Origins.BottomLeft: return ParentHeight - Bottom - Height;
                    case Origins.BottomCenter: return ParentHeight - Bottom - Height;
                    case Origins.BottomRight: return ParentHeight - Bottom - Height;
                    default: return Top;
                }
            }
        }
        internal Rectangle DestRect
        {
            get
            {
                return new Rectangle(DestX,DestY, SourceRect.Width,SourceRect.Height);
            }
        }
        internal Rectangle TextureRect
        {
            get
            {
                return new Rectangle(DrawingX, DrawingY, Width, Height);
            }
        }
        internal Rectangle ParentRect
        {
            get
            {
                return new Rectangle(ParentDrawingX, ParentDrawingY, ParentWidth, ParentHeight);
            }
        }
        internal Rectangle SourceRect
        {
            get
            {
                int x = 0;
                int y = 0;
                int width = Width;
                int height = Height;
                if (DrawingX < ParentDrawingX)
                {
                    x = ParentDrawingX - DrawingX;
                    width = Width - x;
                }
                if (DrawingY < ParentDrawingY)
                {
                    y = ParentDrawingY - DrawingY;
                    height = Height - y;
                }

                if (DrawingX + Width > ParentWidth + ParentDrawingX)
                    width = ParentWidth - X;
                if (DrawingY + Height > ParentHeight + ParentDrawingY)
                    height = ParentHeight - Y;

                if (DrawingX < ParentDrawingX && DrawingX + Width > ParentWidth + ParentDrawingX)
                    width = ParentWidth;
                if (DrawingY < ParentDrawingY && DrawingY + Height > ParentHeight + ParentDrawingY)
                    height = ParentHeight;
                return new Rectangle(x, y, width, height);
            }
        }
        public static Sprite Empty
        {
            get
            {
                var s = new Sprite()
                {
                    Top = 0,
                    Left = 0,
                    Width = 1,
                    Height = 1,
                    Color = Color.White,
                    Margin = Origins.TopLeft,
                };
                return s;
            }
        }

        /// <summary>
        /// 从文件创建Texture
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns>Texture2D</returns>
        public static Texture2D CreateTextureFromFile(string Path)
        {
            var fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
            var t = Texture2D.FromStream(Graphics.Instance.GraphicsDevice, fs);
            PreMultiplyAlphas(t);
            return t;
        }
        private static void PreMultiplyAlphas(Texture2D ret)
        {
            Byte4[] data = new Byte4[ret.Width * ret.Height];
            ret.GetData<Byte4>(data);
            for (int i = 0; i < data.Length; i++)
            {
                Vector4 vec = data[i].ToVector4();
                float alpha = vec.W / 255.0f;
                int a = (int)(vec.W);
                int r = (int)(alpha * vec.X);
                int g = (int)(alpha * vec.Y);
                int b = (int)(alpha * vec.Z);
                uint packed = (uint)(
                    (a << 24) +
                    (b << 16) +
                    (g << 8) +
                    r
                    );

                data[i].PackedValue = packed;
            }
            ret.SetData<Byte4>(data);
        }
        public Color GetPixel(int x, int y)
        {
            int id = (y - 1) * Texture.Width + x - 1;
            if (id >= 0 && id < _texturepixel.Length)
                return _texturepixel[id];
            else
                return Color.Transparent;
        }
        public Color GetPixel(Vector2 pos)
        {
            return GetPixel((int)pos.X, (int)pos.Y);
        }
        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                if (Texture != null)
                {
                    Graphics.Instance.SpriteBatch.DrawArea(Texture, new Vector2(DestX,DestY), SourceRect, new Vector2(Width, Height), Color, Rotation, Origin, SpriteEffect, LayerDepth);
                }
                ChildSprites.Draw(gameTime);
#if DEBUG
                //
                //Graphics.Instance.SpriteBatch.DrawRectangle(DestRect, Color.Black);
                //Graphics.Instance.SpriteBatch.DrawRectangle(TextureRect, Color.Red);
                //dbg.DrawText(new Vector2(TextureRect.X, TextureRect.Y),string.Format("TextureRect,Type={0}", this.GetType().ToString()),Color.Red);
                //dbg.DrawText(new Vector2(DestRect.X, DestRect.Y), string.Format("DestRect,Type={0},X={1},Y={2},Width={3},Height={4}", this.GetType().ToString(),DestRect.X,DestRect.Y,DestRect.Width,DestRect.Height), Color.Black);
#endif
            }
        }
        public override void Initialize()
        {
            //dbg = new Font("msyh.ttc", 12);
            ChildSprites.Initialize();
        }
        public override void Dispose()
        {
            ChildSprites.Dispose();
        }
        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                ChildSprites.Update(gameTime);
            }
            int i = 0;
            while (i < AnimList.Count)
            {
                if (AnimList[i].Finished)
                    AnimList.Remove(AnimList[i]);
                else
                {
                    AnimList[i].Update(gameTime);
                    i++;
                }
            }
        }
    }
}