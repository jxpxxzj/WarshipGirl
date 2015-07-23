using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Scene;
using jxGameFramework.Data;
using jxGameFramework.Components;
using jxGameFramework.Audio;
using Microsoft.Xna.Framework;
using WarshipGirl.Data;

namespace WarshipGirl.Scene
{
    partial class GetShip : BaseScene
    {
        Sprite talking;
        Sprite image;
        Text shipname;
        Text shiptype;
        Text talk;
    }
    partial class GetShip : BaseScene 
    {
        public BaseShip Ship;
        AudioStream bgstream;
        AudioPlayer player;
        public GetShip(BaseShip target)
        {
            Ship = target;
        }
        public override void LoadContent()
        {
            bgstream = new AudioStream(@"Content\getship.mp3", true);
            this.Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, string.Format(@"Content\fullColor{0}@2x.png", Ship.Stars));
            talking = new Sprite()
            {
                Bottom=15,
                Margin = Origins.BottomCenter,
                Width = 793,
                Height = 105,
                Color = Color.White,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\gettingtalk.png")
            };
            image = new Sprite()
            {
                Margin = Origins.Center,
                Color = Color.White,
                Width = 900,
                Height = 900,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, string.Format(@"F:\shipwar\assets\bin\Pic\ship1024_normal_{0}.png",Ship.ID)),
            };

            shipname = new Text()
            {
                Font = new Font(this.GraphicsDevice,"msyh.ttc", 25, -8)
                {
                    EnableBorder=true,
                    BorderColor=Color.Black,
                },
                Color=Color.Cyan,
                text=Ship.Name,
                X=260,
                Y=675,
                OriginType=Origins.Center,
            };
            shiptype = new Text()
            {
                Font = new Font(this.GraphicsDevice, "msyh.ttc", 20, -10)
                {
                    EnableBorder = true,
                    BorderColor = Color.Black,
                },
                Color = Color.Cyan,
                text = BaseShip.ShipTypeToString(Ship.Type),
                X = 260,
                Y = 710,
                OriginType = Origins.Center,
            };
            talk = new Text()
            {
                Font = new Font(this.GraphicsDevice, "msyh.ttc", 20, -10)
                {
                    EnableBorder = true,
                    BorderColor = Color.Black,
                },
                Color = Color.White,
                text = Ship.GettingTalk,
                X = 375,
                Y = 702,
                OriginType = Origins.BottomLeft,
            };

            AddComponent(image);
            AddComponent(talking);
            AddComponent(shipname);
            AddComponent(shiptype);
            AddComponent(talk);
            this.Click += GetShip_Click;
            this.Load += GetShip_Load;
            this.Unload += GetShip_Unload;
            base.LoadContent();
        }

        void GetShip_Unload(object sender, EventArgs e)
        {
            player.Stop();
        }

        void GetShip_Load(object sender, EventArgs e)
        {
            player = new AudioPlayer(bgstream);
            player.Play(true);
        }

        void GetShip_Click(object sender, EventArgs e)
        {
            var game = (Game1)ParentGame;
            game.Navigate(game.harbor);
        }

    }
}
