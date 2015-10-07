using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using WarshipGirl.Controls;
using jxGameFramework.Scene;
using jxGameFramework.Media;
using jxGameFramework.Components;
using jxGameFramework.Animations;
using jxGameFramework.Animations.Curve;
using jxGameFramework.Controls;
using jxGameFramework.Data;
using WarshipGirl.Data;

namespace WarshipGirl.Scene
{
    partial class Harbor:BaseScene
    {
        Control poss;
        Control dock;
        Control fact;
        Control trans;
        ResourceLabel oilres;
        ResourceLabel bulres;
        ResourceLabel irores;
        ResourceLabel alures;

        Button collection;
        Button settings;

        Sprite hude;
        Sprite expbar;

        Label level;

    }
    partial class Harbor : BaseScene
    {
        AudioStream bgstream;
        AudioPlayer player;
        public override void Initialize()
        {
            if (((Game1)Parent).isNightMode)
                bgstream = new AudioStream(@"Content\port-night.mp3",true);
            else
                bgstream = new AudioStream(@"Content\port-day.mp3",true);

            collection = new Button()
            {
                Left = 10,
                Bottom=10,
                Width =120,
                Height =40,
                Margin=Origins.BottomLeft
            };
            settings = new Button()
            {
                Left = 140,
                Bottom = 10,
                Width = 120,
                Height = 40,
                Margin = Origins.BottomLeft
            };
            hude = new Sprite()
            {
                Left =-100,
                Margin = Origins.CenterLeft,
                Width = 800,
                Height = 800,
                Texture = TextureManager.LoadShipImage(8009,TextureManager.ShipSize.Large)
            };
            expbar = new Sprite()
            {
                Margin=Origins.TopLeft,
                Width=222,
                Height=75,
                Texture = Sprite.CreateTextureFromFile(@"Content\expBar.png")
            };
            level = new Label()
            {
                Top = 2,
                Left = 5,
                Color = Color.White,
                Font = new Font("msyh.ttc", 25)
            };

            poss = new Control()
            {
                Right = 133,
                Bottom = 120,
                Margin = Origins.CenterRight,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(@"Content\Procession.png")
            };
            poss.Click += poss_Click;
            dock = new Control()
            {
                Right = 133,
                Top = 120,
                Margin = Origins.CenterRight,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(@"Content\Dock.png")
            };
            fact = new Control()
            {
                Right = 250,
                Margin = Origins.CenterRight,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(@"Content\Factory.png")
            };
            fact.Click += fact_Click;
            trans = new Control()
            {
                Right = 15,
                Margin = Origins.CenterRight,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(@"Content\Transfomation.png")
            };

            oilres = new ResourceLabel()
            {
                Right = 475,
                Margin = Origins.TopRight,
                Type = ResourceType.Oil,
                isBeginning = true
            };
            bulres = new ResourceLabel()
            {
                Right = 350,
                Margin = Origins.TopRight,
                Type = ResourceType.Steel
            };
            irores = new ResourceLabel()
            {
                Right = 225,
                Margin = Origins.TopRight,
                Type = ResourceType.Ammo
            };
            alures = new ResourceLabel()
            {
                Right = 100,
                Margin = Origins.TopRight,
                Type = ResourceType.Aluminium
            };
            
            //flashanim = new Animation()
            //{
            //    BeginTime = new GameTime(new TimeSpan(0, 0, 0,0, 1921), new TimeSpan(1)),
            //    Duration = new TimeSpan(0, 0, 0,0,1000),
            //    ReturnToBeginning=true,
            //    PlayBack=true,
            //    BeginValue = 255,
            //    EndValue = 200,
            //    TargetProperty = typeof(Sprite).GetProperty("ColorA"),
            //    TargetSprite = testbtn,
            //    LoopMode = LoopMode.Forever,
            //    EasingFunction = new Power()
            //};
            ChildSprites.Add(hude);
            ChildSprites.Add(collection);
            ChildSprites.Add(settings);
            ChildSprites.Add(poss);
            ChildSprites.Add(dock);
            ChildSprites.Add(fact);
            ChildSprites.Add(trans);
            ChildSprites.Add(expbar);
            ChildSprites.Add(level);
            ChildSprites.Add(oilres);
            ChildSprites.Add(bulres);
            ChildSprites.Add(irores);
            ChildSprites.Add(alures);

            this.Show += Harbor_Show;
            this.Leave += Harbor_Leave;
            base.Initialize();

            collection.Text = "图鉴";
            settings.Text = "设置";
            level.Text = "Lv.61";
        }

        private void Harbor_Leave(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void Harbor_Show(object sender, EventArgs e)
        {
            player = new AudioPlayer(bgstream);
            player.Play(true);
        }

        void poss_Click(object sender, MouseEventArgs e)
        {
            Game1.Instance.Scenes.Navigate("Select");
        }

        int h = 10;
        bool r = false;
        void fact_Click(object sender, EventArgs e)
        {
            //var ship = Server.ServerCommand.GetBuildableRandomShip();
            //var s = new Ship64Img(ship,r)
            //{
            //    Left = 100,
            //    Top = h,
            //};
            //s.LoadContent();
            //AddComponent(s);
            //h += 75;
            //r = !r;

            //var getship = new GetShip(Server.ServerCommand.GetBuildableRandomShip())
            //{
            //    Color = Color.White,
            //    Margin = Origins.Center,
            //    Width = Width,
            //    Height = Height,
            //    Parent = this
            //};
            //getship.LoadContent();
            //Game1.Instance.Navigate(getship);

            Game1.Instance.Scenes.Navigate("Factory");
        }
    }
    partial class Harbor: BaseScene
    {
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
