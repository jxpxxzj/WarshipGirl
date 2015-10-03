using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using WarshipGirl.Data;
using System.Reflection;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WarshipGirl.Graphics;

namespace WarshipGirl.Controls
{
    class Ship64Img : Control
    {
        public BaseShip Ship { get; set; }
        public bool isReverse { get; set; }

        Sprite bg;
        Sprite padding;
        Sprite border;
        Sprite icon;
        Sprite img;
        SpriteEffects se;
        
        public Ship64Img(BaseShip target,bool reverse)
        {
            Ship = target;
            isReverse = reverse;
            if (isReverse)
                se = SpriteEffects.FlipHorizontally;
            else
                se = SpriteEffects.None;
        }
        public override void LoadContent()
        {
            this.Width = 215;
            this.Height = 72;
            bg = new Sprite()
            {
                Width=196,
                Height=66,
                Left=16,
                Top=4,
                Margin=Origins.TopLeft,
                Texture=Sprite.CreateTextureFromFile(string.Format(@"Content\bg_64_{0}.png",Ship.Stars)),
            };
            border = new Sprite()
            {
                Width=202,
                Height=72,
                Left=14,
                Margin=Origins.TopLeft,
                Texture=Sprite.CreateTextureFromFile(@"Content\border_64_gold.png"),
            };

            padding = new Sprite()
            {
                Width=30,
                Height=70,
                Top = 1,
                Margin=Origins.TopLeft,
                Texture=Sprite.CreateTextureFromFile(@"Content\padding_64_gold.png"),
                SpriteEffect=se,
            };
            icon = new Sprite()
            {
                Width=50,
                Height=50,
                Left=22,
                Margin = Origins.CenterLeft,
                Texture=Sprite.CreateTextureFromFile(string.Format(@"Content\icon_{0}.png",Ship.Type.ToString())),
            };
            img = new Sprite()
            {
                Width = 248,
                Height = 64,
                Margin = Origins.Center,
                Texture = TextureManager.LoadShipImage(Ship.ID,TextureManager.ShipSize.Small),
            };
            AddComponent(bg);
            AddComponent(img);
            AddComponent(border);
            AddComponent(padding);
            AddComponent(icon);
            if(isReverse)
            {
                border.SpriteEffect = se;
                border.Margin = Origins.TopRight;
                border.Right = 14;

                padding.SpriteEffect = se;
                padding.Margin = Origins.TopRight;

                icon.Margin = Origins.CenterRight;
                icon.Right = 22;

                bg.Margin = Origins.TopRight;
                bg.Right = 16;
               
            }
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
