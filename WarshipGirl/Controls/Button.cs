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

namespace WarshipGirl.Controls
{
    class Button : Control
    {
        public string Text { get; set; }
        Text ButtonText;
        public override void LoadContent()
        {
            var buttonbg = new FileStream(@"Content\btnCommon_1.png", FileMode.Open, FileAccess.Read);
            this.Texture = Texture2D.FromStream(this.GraphicsDevice,buttonbg);
            this.Width = 143;
            this.Height = 70;
            this.Color = Color.White;
            ButtonText = new Text()
            {
                Font = new Font(this.GraphicsDevice, "msyh.ttc", 25, -12)
                {
                    BorderColor = Color.Black,
                    EnableBorder = true
                },
                Color = Color.White,
                GraphicsDevice = this.GraphicsDevice,
                SpriteBatch = this.SpriteBatch,
                X = this.RenderX + this.Width / 2,
                Y = this.RenderY + this.Height / 2,
                OriginType = Origins.Center,
            };
            ButtonText.text = Text;
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            ButtonText.Draw(gameTime);
        }
    }
}
