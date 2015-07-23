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
                Left = 10,
                Top = 10,
                Text = "建造",
                Margin = Origins.TopLeft
            };
            disassembly = new Button()
            {
                Left = 10,
                Top = 80,
                Text = "解体",
                Margin = Origins.TopLeft
            };
            developing = new Button()
            {                               
                Left = 10,
                Top = 150,
                Text = "开发",
                Margin = Origins.TopLeft
            };
            disintegrate = new Button()
            {                                
                Left = 10,
                Top = 220,
                Text = "废弃",
                Margin = Origins.TopLeft
            };

            toharbor = new Button()
            {                                
                Left = 10,
                Text = "港口",
                Margin = Origins.BottomLeft
            };
            toharbor.Click += toharbor_Click;



            oilres = new ResourceLabel()
            {
                Left = 400,
                Margin = Origins.TopLeft,
                Type = ResourceType.Oil,
                isBeginning = true
            };
            bulres = new ResourceLabel()
            {
                Left = 525,
                Margin = Origins.TopLeft,
                Type = ResourceType.Bullet
            };
            irores = new ResourceLabel()
            {
                Left = 650,
                Margin = Origins.TopLeft,
                Type = ResourceType.Iron
            };
            alures = new ResourceLabel()
            {
                Left = 775,
                Margin = Origins.TopLeft,
                Type = ResourceType.Aluminum
            };
            
            AddComponent(oilres);
            AddComponent(bulres);
            AddComponent(irores);
            AddComponent(alures);
            AddComponent(building);
            AddComponent(disassembly);
            AddComponent(developing);
            AddComponent(disintegrate);
            AddComponent(toharbor);

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
