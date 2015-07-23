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
        Sprite bg;
        Sprite padding;
        Sprite border;
        Sprite icon;
        Sprite img;
        public Ship64Img(BaseShip target)
        {
            Ship = target;
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
                Texture=Sprite.CreateTextureFromFile(this.GraphicsDevice,string.Format(@"Content\bg_64_{0}.png",Ship.Stars)),
                Color=Color.White
            };
            border = new Sprite()
            {
                Width=202,
                Height=72,
                Left=14,
                Margin=Origins.TopLeft,
                Texture=Sprite.CreateTextureFromFile(this.GraphicsDevice,@"Content\border_64_gold.png"),
                Color=Color.White
            };
            padding = new Sprite()
            {
                Width=30,
                Height=70,
                Top = 1,
                Margin=Origins.TopLeft,
                Texture=Sprite.CreateTextureFromFile(this.GraphicsDevice,@"Content\padding_64_gold.png"),
                Color=Color.White
            };

            icon = new Sprite()
            {
                Width=50,
                Height=50,
                Left=22,
                Margin = Origins.CenterLeft,
                Texture=Sprite.CreateTextureFromFile(this.GraphicsDevice,string.Format(@"Content\icon_{0}.png",Ship.Type.ToString())),
                Color=Color.White
            };
            img = new Sprite()
            {
                Width = 248,
                Height = 64,
                Margin = Origins.Center,
                Texture = TextureManager.LoadShipImage(GraphicsDevice,Ship.ID,TextureManager.ShipSize.Small),//Sprite.CreateTextureFromFile(this.GraphicsDevice, string.Format(@"F:\shipwar\assets\bin\Pic\ship64_normal_{0}.png", Ship.ID)),
                Color = Color.White
            };
            img.Width = (int)(img.Width * 0.9);
            img.Height = (int)(img.Height * 0.9);
            AddComponent(bg);
            AddComponent(img);
            AddComponent(border);
            AddComponent(padding);
            AddComponent(icon);
            base.LoadContent();
        }
    }
}
