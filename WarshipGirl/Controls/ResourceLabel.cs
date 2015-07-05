using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using WarshipGirl.Data;

namespace WarshipGirl.Controls
{
    class ResourceLabel : Control
    {
        public int Value { get; set; }
        public ResourceType Type { get; set; }

        Sprite TypeSprite;
        Text LabelText;
        public bool isBeginning { get; set; }
        public override void LoadContent()
        {
            FileStream bgstream;
            if (isBeginning)
            {
                bgstream = new FileStream(@"Content\rescontbegin.png", FileMode.Open, FileAccess.Read);
            }
            else
            {
                bgstream = new FileStream(@"Content\rescontainer.png", FileMode.Open, FileAccess.Read);
            }

            FileStream restream = new FileStream(string.Format(@"Content\{0}.png", Type.ToString()), FileMode.Open, FileAccess.Read);
            Texture2D restexture = Texture2D.FromStream(this.GraphicsDevice, restream);
            this.Texture = Texture2D.FromStream(this.GraphicsDevice, bgstream);
            this.Width = 134;
            this.Height = 34;
            this.Color = Color.White;
            TypeSprite = new Sprite()
            {
                GraphicsDevice=this.GraphicsDevice,
                SpriteBatch=this.SpriteBatch,
                Texture=restexture,
                Color=Color.White,
                Width=(int)(restexture.Width / 2),
                Height = (int)(restexture.Height / 2),
                X = this.RenderX + this.Width / 16*3,
                Y = this.RenderY + this.Height / 2,
                OriginType = Origins.Center,
            };
            
            LabelText = new Text()
            {
                Font = new Font(this.GraphicsDevice, "msyh.ttc", 20, -15),
                Color = Color.White,
                GraphicsDevice = this.GraphicsDevice,
                SpriteBatch = this.SpriteBatch,
                X = this.RenderX + this.Width / 3,
                Y = this.RenderY + this.Height / 2,
                OriginType = Origins.CenterLeft,
            };
            Value = 61616;
            LabelText.text = Value.ToString();
            this.CompList.Add(TypeSprite);
            this.CompList.Add(LabelText);
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            //LabelText.Draw(gameTime);
        }
    }
}
