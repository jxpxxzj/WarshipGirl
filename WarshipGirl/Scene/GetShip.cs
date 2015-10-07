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
using jxGameFramework.Media;
using Microsoft.Xna.Framework;
using WarshipGirl.Data;
using jxGameFramework.Controls;

namespace WarshipGirl.Scene
{
    partial class GetShip : BaseScene
    {
        //Sprite talking;
        //Sprite image;
        //Label shipname;
        //Label shiptype;
        //Label talk;
    }
    partial class GetShip : BaseScene 
    {
        //public Ship Ship;
        //AudioStream bgstream;
        //AudioPlayer player;
        //public GetShip(Ship target)
        //{
        //    Ship = target;
        //}
        public override void Initialize()
        {
            //TODO: Fit new data sdk
            //bgstream = new AudioStream(@"Content\getship.mp3", true);
            //this.Texture = Sprite.CreateTextureFromFile(string.Format(@"Content\fullColor{0}@2x.png", Ship.Stars));
            //talking = new Sprite()
            //{
            //    Bottom=30,
            //    Margin = Origins.BottomCenter,
            //    Width = 793,
            //    Height = 105,
            //    Color = Color.White,
            //    Texture = Sprite.CreateTextureFromFile(@"Content\gettingtalk.png")
            //};
            //image = new Sprite()
            //{
            //    Margin = Origins.Center,
            //    Color = Color.White,
            //    Width = 900,
            //    Height = 900,
            //    Texture = TextureManager.LoadShipImage(Ship.ID,TextureManager.ShipSize.Large)
            //};

            //shipname = new Label()
            //{
            //    Font = new Font("msyh.ttc", 25)
            //    {
            //        EnableBorder=true,
            //        BorderColor=Color.Black,
            //    },
            //    Color=Color.Cyan,
            //    Text=Ship.Name,
            //    Bottom=90,
            //    Right = 250, 
            //    Margin=Origins.BottomCenter,
            //};
            //shiptype = new Label()
            //{
            //    Font = new Font("msyh.ttc", 20)
            //    {
            //        EnableBorder = true,
            //        BorderColor = Color.Black,
            //    },
            //    Color = Color.Cyan,
            //    Text = BaseShip.ShipTypeToString(Ship.Type),
            //    Bottom = 55,
            //    Right = 250,
            //    Margin=Origins.BottomCenter,
            //};
            //talk = new Label()
            //{
            //    Font = new Font("msyh.ttc", 20)
            //    {
            //        EnableBorder = true,
            //        BorderColor = Color.Black,
            //    },
            //    Color = Color.White,
            //    Text = Ship.GettingTalk,
            //    Bottom = 75,
            //    Margin=Origins.BottomCenter,
            //};

            //ChildSprites.Add(image);
            //ChildSprites.Add(talking);
            //ChildSprites.Add(shipname);
            //ChildSprites.Add(shiptype);
            //ChildSprites.Add(talk);
            //this.Click += GetShip_Click;
            //this.Show += GetShip_Show;
            //this.Leave += GetShip_Leave;
            //base.Initialize();
            //shipname.Origin = new Vector2(shipname.Width / 2, shipname.Height / 2);
            //shiptype.Origin = new Vector2(shiptype.Width / 2, shiptype.Height / 2);
            //talk.Left = talk.Width / 2-160; 
        }

        private void GetShip_Leave(object sender, EventArgs e)
        {
            //player.Stop();
        }

        private void GetShip_Show(object sender, EventArgs e)
        {
            //player = new AudioPlayer(bgstream);
            //player.Play(true);
        }

        void GetShip_Click(object sender, EventArgs e)
        {
            //Game1.Instance.Scenes.Navigate("Harbor");
        }
    }
}
