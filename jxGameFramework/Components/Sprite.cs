using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using jxGameFramework.Animations;
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
    public enum PositionRelation
    {
        Relative,Absolute
    }
    /// <summary>
    /// 精灵
    /// </summary>
    public class Sprite : Component
    {
        public Sprite Parent { get; set; }

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
        public Color GetPixel(int x,int y)
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

        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }

        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }

        Vector2 _scale = new Vector2(1f, 1f);
        public Vector2 Scale 
        { 
            get
            {
                return _scale;
            }
            set
            {
                _scale=value;
            } 
        }
        public SpriteEffects SpriteEffect { get; set; }
        public float LayerDepth { get; set; }

        public Origins Margin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Color Color { get; set; }

        public List<Animation> AnimList = new List<Animation>();

        private List<Component> CompList = new List<Component>();
        public void AddComponent(Component comp)
        {
            try
            {
                ((Sprite)comp).Parent = this;
            }
            catch (Exception)
            {

            }
            try
            {
                ((Text)comp).Parent = this;
            }
            catch (Exception)
            {

            }
            CompList.Add(comp);
        }

        private int ParentWidth
        {
            get
            {
                if (Parent != null)
                    return Parent.Width;
                else
                    return 0;
            }
        }
        private int ParentHeight
        {
            get
            {
                if (Parent != null)
                    return Parent.Height;
                else
                    return 0;
            }
        }
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
        public int RenderX
        {
            get
            {
                switch (Margin)
                {
                    case Origins.TopLeft: return ParentRenderX + Left;
                    case Origins.TopCenter: return ParentRenderX + (ParentWidth - Width) / 2;
                    case Origins.TopRight: return ParentRenderX + ParentWidth - Right - Width;
                    case Origins.CenterLeft: return ParentRenderX + Left;
                    case Origins.Center: return ParentRenderX + (ParentWidth - Width) / 2;
                    case Origins.CenterRight: return ParentRenderX + ParentWidth - Right - Width;
                    case Origins.BottomLeft: return ParentRenderX + Left;
                    case Origins.BottomCenter: return ParentRenderX + (ParentWidth - Width) / 2;
                    case Origins.BottomRight: return ParentRenderX + ParentWidth - Right - Width;
                    default: return Left;
                }
            }
        }
        public int RenderY
        { 
            get
            {
                switch (Margin)
                {
                    case Origins.TopLeft: return ParentRenderY + Top;
                    case Origins.TopCenter: return ParentRenderY + Top;
                    case Origins.TopRight: return ParentRenderY + Top;
                    case Origins.CenterLeft: return ParentRenderY + (ParentHeight - Height) / 2;
                    case Origins.Center: return ParentRenderY + (ParentHeight - Height) / 2;
                    case Origins.CenterRight: return ParentRenderY + (ParentHeight - Height) / 2;
                    case Origins.BottomLeft: return ParentRenderY + ParentHeight - Bottom - Height;
                    case Origins.BottomCenter: return ParentRenderY + ParentHeight - Bottom - Height;
                    case Origins.BottomRight: return ParentRenderY + ParentHeight - Bottom - Height;                       
                    default: return Left;       
                }
            }
        }

        /// <summary>
        /// 从文件创建Texture
        /// </summary>
        /// <param name="gd">GraphicsDevice</param>
        /// <param name="Path">文件路径</param>
        /// <returns>Texture2D</returns>
        public static Texture2D CreateTextureFromFile(GraphicsDevice gd, string Path)
        {
            var fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
            var t = Texture2D.FromStream(gd, fs);
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

        public override void Draw(GameTime gameTime)
        {
            if(Texture != null)
                SpriteBatch.Draw(Texture, new Rectangle(RenderX,RenderY , (int)(Width*Scale.X), (int)(Height*Scale.Y)),null,Color,Rotation,Origin,SpriteEffect,LayerDepth);
            foreach (Component comp in CompList)
                comp.Draw(gameTime);
        }
        public override void LoadContent()
        {
            foreach (Component comp in CompList)
            {
                comp.GraphicsDevice = this.GraphicsDevice;
                comp.SpriteBatch = this.SpriteBatch;
                comp.LoadContent();
            }
                
        }
        public override void UnloadContent()
        {

        }
        public override void Update(GameTime gameTime)
        {
            for(int j=0;j<CompList.Count;j++)
            {
                CompList[j].Update(gameTime);
            }
            int i=0;
            while(i<AnimList.Count)
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