using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using jxGameFramework.Scene;
using jxGameFramework.Data;
using jxGameFramework.Components;
using jxGameFramework.Audio;
using Microsoft.Xna.Framework;
using WarshipGirl.Data;
using WarshipGirl.Graphics;
using jxGameFramework.Controls;

namespace WarshipGirl.Scene
{
    partial class GetShip : BaseScene
    {
        Sprite talking;
        Sprite image;
        Label shipname;
        Label shiptype;
        Label talk;
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
                Texture = TextureManager.LoadShipImage(GraphicsDevice,Ship.ID,TextureManager.ShipSize.Large)
            };

            shipname = new Label()
            {
                Font = new Font(this.GraphicsDevice,"msyh.ttc", 25)
                {
                    EnableBorder=true,
                    BorderColor=Color.Black,
                },
                Color=Color.Cyan,
                Text=Ship.Name,
                Top=260,
                Width=675,
            };
            shiptype = new Label()
            {
                Font = new Font(this.GraphicsDevice, "msyh.ttc", 20)
                {
                    EnableBorder = true,
                    BorderColor = Color.Black,
                },
                Color = Color.Cyan,
                Text = BaseShip.ShipTypeToString(Ship.Type),
                Top = 260,
                Left = 710,
            };
            talk = new Label()
            {
                Font = new Font(this.GraphicsDevice, "msyh.ttc", 20)
                {
                    EnableBorder = true,
                    BorderColor = Color.Black,
                },
                Color = Color.White,
                Text = Ship.GettingTalk,
                Top = 375,
                Left = 702,
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
