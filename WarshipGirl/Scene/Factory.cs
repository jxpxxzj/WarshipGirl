using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Scene;
using WarshipGirl.Controls;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Media;
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
        public override void Initialize()
        {
            building = new Button()
            {                             
                Left = 10,
                Top = 10,
                Width = 100,
                Height = 50,
                Margin = Origins.TopLeft
            };
            disassembly = new Button()
            {
                Left = 10,
                Top = 80,
                Width = 100,
                Height = 50,
                Margin = Origins.TopLeft
            };
            developing = new Button()
            {                               
                Left = 10,
                Top = 150,
                Width = 100,
                Height = 50,
                Margin = Origins.TopLeft
            };
            disintegrate = new Button()
            {                                
                Left = 10,
                Top = 220,
                Width = 100,
                Height = 50,
                Margin = Origins.TopLeft
            };

            toharbor = new Button()
            {                                
                Left = 10,
                Bottom = 10,
                Width = 100,
                Height = 50,
                Margin = Origins.BottomLeft
            };
            toharbor.Click += toharbor_Click;



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
            
            ChildSprites.Add(oilres);
            ChildSprites.Add(bulres);
            ChildSprites.Add(irores);
            ChildSprites.Add(alures);
            ChildSprites.Add(building);
            ChildSprites.Add(disassembly);
            ChildSprites.Add(developing);
            ChildSprites.Add(disintegrate);
            ChildSprites.Add(toharbor);

            this.Show += Factory_Show;
            this.Leave += Factory_Leave;
            base.Initialize();
            building.Text = "建造";
            disassembly.Text = "解体";
            developing.Text = "开发";
            disintegrate.Text = "废弃";
            toharbor.Text = "港口";
        }

        private void Factory_Leave(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void Factory_Show(object sender, EventArgs e)
        {
            player = new AudioPlayer(bgstream);
            player.Play(true);
        }

        void toharbor_Click(object sender, EventArgs e)
        {
            Game1.Instance.Scenes.Navigate("Harbor");
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
