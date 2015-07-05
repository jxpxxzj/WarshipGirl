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

        Text level;

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
                X = 10,
                Y = 768,
                Text="图鉴",
                OriginType=Origins.BottomLeft
            };
            settings = new Button()
            {
                X = 140,
                Y = 768,
                Text = "设置",
                OriginType = Origins.BottomLeft
            };
            hude = new Sprite()
            {
                X =280,
                Y = 384,
                OriginType = Origins.Center,
                Color = Color.White,
                Width = 800,
                Height = 800,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice,@"Content\ship1024_normal_1.png"),
            };
            expbar = new Sprite()
            {
                X=0,
                Y=0,
                OriginType=Origins.TopLeft,
                Color=Color.White,
                Width=222,
                Height=75,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\expBar.png")
            };
            level = new Text()
            {
                X = 8,
                Y = 2,
                text = "Lv.61",
                Color = Color.White,
                OriginType = Origins.TopLeft,
                Font = new Font(this.GraphicsDevice, "msyh.ttc", 25,-8)
            };

            poss = new Control()
            {
                X = 768,
                Y = 192,
                OriginType = Origins.Center,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\Procession.png")
            };
            dock = new Control()
            {
                X = 768,
                Y = 440,
                OriginType = Origins.Center,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\Dock.png")
            };
            fact = new Control()
            {
                X = 650,
                Y = 315,
                OriginType = Origins.Center,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\Factory.png")
            };
            fact.Click += fact_Click;
            trans = new Control()
            {
                X = 885,
                Y = 315,
                OriginType = Origins.Center,
                Color = Color.White,
                Width = 185,
                Height = 182,
                Texture = Sprite.CreateTextureFromFile(this.GraphicsDevice, @"Content\Transfomation.png")
            };

            oilres = new ResourceLabel()
            {
                X = 500,
                Y = 0,
                OriginType = Origins.TopCenter,
                Type = ResourceType.Oil,
                isBeginning=true
            };
            bulres = new ResourceLabel()
            {
                X = 625,
                Y = 0,
                OriginType = Origins.TopCenter,
                Type=ResourceType.Bullet
            };
            irores = new ResourceLabel()
            {
                X = 750,
                Y = 0,
                OriginType = Origins.TopCenter,
                Type = ResourceType.Iron
            };
            alures = new ResourceLabel()
            {
                X = 875,
                Y = 0,
                OriginType = Origins.TopCenter,
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
            this.CompList.Add(hude);
            this.CompList.Add(collection);
            this.CompList.Add(settings);
            this.CompList.Add(poss);
            this.CompList.Add(dock);
            this.CompList.Add(fact);
            this.CompList.Add(trans);
            this.CompList.Add(expbar);
            this.CompList.Add(level);
            this.CompList.Add(oilres);
            this.CompList.Add(bulres);
            this.CompList.Add(irores);
            this.CompList.Add(alures);

            this.Load += Harbor_Load;
            this.Unload += Harbor_Unload;
            base.LoadContent();
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

        void fact_Click(object sender, EventArgs e)
        {
            var game = (Game1)ParentGame;
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
