using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using WarshipGirl.Controls;
using jxGameFramework.Scene;
using jxGameFramework.Audio;
using jxGameFramework.Components;
using jxGameFramework.Animations;
using jxGameFramework.Animations.Curve;
using jxGameFramework.Controls;
using jxGameFramework.Data;
using WarshipGirl.Graphics;
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
        public override void LoadContent()
        {
            if (((Game1)ParentGame).isNightMode)
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
                Color = Color.White,
                Width = 800,
                Height = 800,
                Texture = TextureManager.LoadShipImage(GraphicsDevice,1,TextureManager.ShipSize.Large)
            };
            expbar = new Sprite()
            {
                Margin=Origins.TopLeft,
                Color=Color.White,
                Width=222,
                Height=75,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\expBar.png")
            };
            level = new Label()
            {
                Top = 2,
                Left = 5,
                Color = Color.White,
                Font = new Font(this.GraphicsDevice, "msyh.ttc", 25)
            };

            poss = new Control()
            {
                Right = 133,
                Top = GraphicsDevice.Viewport.Height / 2 - 205,
                Margin = Origins.TopRight,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\Procession.png")
            };
            poss.Click += poss_Click;
            dock = new Control()
            {
                Right = 133,
                Top = GraphicsDevice.Viewport.Height / 2 + 25,
                Margin = Origins.TopRight,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\Dock.png")
            };
            fact = new Control()
            {
                Right = 250,
                Margin = Origins.CenterRight,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\Factory.png")
            };
            fact.Click += fact_Click;
            trans = new Control()
            {
                Right = 15,
                Margin = Origins.CenterRight,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\Transfomation.png")
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
                Type = ResourceType.Bullet
            };
            irores = new ResourceLabel()
            {
                Right = 225,
                Margin = Origins.TopRight,
                Type = ResourceType.Iron
            };
            alures = new ResourceLabel()
            {
                Right = 100,
                Margin = Origins.TopRight,
                Type = ResourceType.Aluminum
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
            AddComponent(hude);
            AddComponent(collection);
            AddComponent(settings);
            AddComponent(poss);
            AddComponent(dock);
            AddComponent(fact);
            AddComponent(trans);
            AddComponent(expbar);
            AddComponent(level);
            AddComponent(oilres);
            AddComponent(bulres);
            AddComponent(irores);
            AddComponent(alures);

            this.Load += Harbor_Load;
            this.Unload += Harbor_Unload;
            base.LoadContent();

            collection.Text = "图鉴";
            settings.Text = "设置";
            level.Text = "Lv.61";
        }

        void poss_Click(object sender, MouseEventArgs e)
        {
            var game = (Game1)ParentGame;
            game.Navigate(game.select);
        }

        void Harbor_Load(object sender, EventArgs e)
        {
            player = new AudioPlayer(bgstream);
            player.Play(true);
        }

        void Harbor_Unload(object sender, EventArgs e)
        {
            player.Stop();
        }
        int h = 10;
        void fact_Click(object sender, EventArgs e)
        {
            //var ship = Server.ServerCommand.GetBuildableRandomShip();
            //var s = new Ship64Img(ship)
            //{
            //    Left = 100,
            //    Top = h,
            //    SpriteBatch = this.SpriteBatch,
            //    GraphicsDevice = this.GraphicsDevice
            //};
            //s.LoadContent();
            //AddComponent(s);
            //h += 75;

            var game = (Game1)ParentGame;
            //var getship = new GetShip(Server.ServerCommand.GetBuildableRandomShip())
            //{
            //    ParentGame = this.ParentGame,
            //    Color = Color.White,
            //    Margin = Origins.Center,
            //    SpriteBatch = this.SpriteBatch,
            //    GraphicsDevice = this.GraphicsDevice,
            //    Width = 1024,
            //    Height = 768,
            //    Parent = this
            //};
            //getship.LoadContent();
            //game.Navigate(getship);

            game.Navigate(game.factory);
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
