using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using jxGameFramework.Animations;
using System.Windows;
using System.IO;

namespace jxGameFramework.Components
{
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
    public class Sprite : Component
    {
        public Texture2D Texture { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ColorR { get; set; }
        public int ColorG { get; set; }
        public int ColorB { get; set; }
        public int ColorA { get; set; }

        public Color Color
        {
            get
            {
                return new Color(ColorR, ColorG, ColorB, ColorA);
            }
            set
            {
                ColorR = value.R;
                ColorB = value.B;
                ColorG = value.G;
                ColorA = value.A;
            }
        }
        public List<Animation> AnimList = new List<Animation>();
        public List<Component> CompList = new List<Component>();

        public Origins OriginType { get; set; }
        public Vector2 CustomOriginPoint { get; set; }

        public int RenderX
        {
            get
            {
                switch (OriginType)
                {
                    case Origins.TopLeft: return X;
                    case Origins.TopCenter: return X - Width - 2;
                    case Origins.TopRight: return X - Width;
                    case Origins.CenterLeft: return X; 
                    case Origins.Center: return X - Width / 2; 
                    case Origins.BottomLeft: return X;
                    case Origins.BottomCenter: return X - Width / 2; 
                    case Origins.BottomRight: return X - Width;
                    case Origins.Custom: return X - Width * (int)CustomOriginPoint.X;
                    default: return X;
                }
            }
        }
        public int RenderY
        { 
            get
            {
                switch (OriginType)
                {
                    case Origins.TopLeft: return Y;                        
                    case Origins.TopCenter: return Y;                       
                    case Origins.TopRight: return Y;                        
                    case Origins.CenterLeft: return Y - Height / 2;                       
                    case Origins.Center: return Y - Height / 2;                        
                    case Origins.CenterRight: return Y - Height / 2;                        
                    case Origins.BottomLeft: return Y - Height;                        
                    case Origins.BottomCenter:return Y - Height;                        
                    case Origins.BottomRight: return Y - Height;                       
                    case Origins.Custom: return Y - Height * (int)CustomOriginPoint.Y;
                    default: return Y;       
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Texture, new Rectangle(RenderX,RenderY , Width, Height),Color);
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
            foreach (Component comp in CompList)
                comp.Update(gameTime);
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

        public static Texture2D CreateTextureFromFile(GraphicsDevice gd,string Path)
        {
            var fs = new FileStream(Path, FileMode.Open,FileAccess.Read);
            var t = Texture2D.FromStream(gd,fs);
            return t;
        }
    }
}
