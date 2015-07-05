using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Scene;
using WarshipGirl.Controls;
using jxGameFramework.Components;
using jxGameFramework.Audio;
using Microsoft.Xna.Framework;
using WarshipGirl.Data;

namespace WarshipGirl.Scene
{
    partial class Factory :BaseScene
    {
        Button building;
        Button disassembly;
        Button developing;
        Button disintegrate;

        ResourceLabel oilres;
        ResourceLabel bulres;
        ResourceLabel irores;
        ResourceLabel alures;

        Button toharbor;
    }
    partial class Factory : BaseScene
    {
        AudioStream bgstream = new AudioStream(@"Content\factory.mp3", true);
        AudioPlayer player;
        public override void LoadContent()
        {
            building = new Button()
            {                             
                X = 10,
                Y = 10,
                Text = "建造",
                OriginType = Origins.TopLeft
            };
            disassembly = new Button()
            {
                X = 10,
                Y = 80,
                Text = "解体",
                OriginType = Origins.TopLeft
            };
            developing = new Button()
            {                               
                X = 10,
                Y = 150,
                Text = "开发",
                OriginType = Origins.TopLeft
            };
            disintegrate = new Button()
            {                                
                X = 10,
                Y = 220,
                Text = "废弃",
                OriginType = Origins.TopLeft
            };

            toharbor = new Button()
            {                                
                X = 10,
                Y = 768,
                Text = "港口",
                OriginType = Origins.BottomLeft
            };
            toharbor.Click += toharbor_Click;

            oilres = new ResourceLabel()
            {                              
                X = 500,
                Y = 0,
                OriginType = Origins.TopCenter,
                Type = ResourceType.Oil,
                isBeginning = true
            };
            bulres = new ResourceLabel()
            {                               
                X = 625,
                Y = 0,
                OriginType = Origins.TopCenter,
                Type = ResourceType.Bullet
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

            CompList.Add(oilres);
            CompList.Add(bulres);
            CompList.Add(irores);
            CompList.Add(alures);
            CompList.Add(building);
            CompList.Add(disassembly);
            CompList.Add(developing);
            CompList.Add(disintegrate);
            CompList.Add(toharbor);

            this.Load += Factory_Load;
            this.Unload += Factory_Unload;
            base.LoadContent();
        }

        void Factory_Load(object sender, EventArgs e)
        {
            player = new AudioPlayer(bgstream);
            player.Play(true);
        }

        void Factory_Unload(object sender, EventArgs e)
        {
            player.Stop();
        }

        void toharbor_Click(object sender, EventArgs e)
        {
            var game = (Game1)ParentGame;
            game.Navigate(game.harbor);
        }
    }
    partial class Factory : BaseScene
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
