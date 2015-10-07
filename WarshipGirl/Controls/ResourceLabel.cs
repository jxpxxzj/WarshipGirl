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
        Label LabelText;
        public bool isBeginning { get; set; }
        public override void Initialize()
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
            Texture2D restexture = Texture2D.FromStream(GraphicsDevice, restream);
            this.Texture = Texture2D.FromStream(GraphicsDevice, bgstream);
            this.Width = 134;
            this.Height = 34;
            this.Color = Color.White;
            TypeSprite = new Sprite()
            {
                Texture=restexture,
                Color=Color.White,
                Width=(int)(restexture.Width / 2),
                Height = (int)(restexture.Height / 2),
                Left = 12,
                Top = 8,
                Margin=Origins.TopLeft,
                Parent=this,
            };
            
            LabelText = new Label()
            {
                Font = new Font("msyh.ttc", 20),
                Color = Color.White,
                Left = this.Width / 3,
                Margin = Origins.CenterLeft,
            };
            Value = 61616;
            LabelText.Text = Value.ToString();
            ChildSprites.Add(TypeSprite);
            ChildSprites.Add(LabelText);
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            //LabelText.Draw(gameTime);
        }
    }
}
