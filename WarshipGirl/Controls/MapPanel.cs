using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using Microsoft.Xna.Framework;
using WarshipGirl.Data;

namespace WarshipGirl.Controls
{
    class MapPanel : Control
    {
        public Control MapSprite { get; set; }

        public Map Map { get; private set; }
        Label SeaTitle;
        Label CreatorInfo;
        Label MapTitle;
        Font TextFont;
        Font TextFontsm;
        bool _selected = false;
        public MapPanel(Map m)
        {
            this.Map = m;
        }
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                var transcolor = new Color(0, 0, 0, 20);
                if (SeaTitle != null)
                {
                    if (_selected)
                    {
                        SeaTitle.Color = Color.Black;
                        CreatorInfo.Color = Color.Black;
                        MapTitle.Color = Color.Black;
                        this.Color = Color.White;
                    }
                    else
                    {
                        SeaTitle.Color = transcolor;
                        CreatorInfo.Color = transcolor;
                        MapTitle.Color = Color.White;
                        this.Color = Color.CornflowerBlue;
                    }
                }
            }
        }
        public override void Initialize()
        {
            this.Texture = Sprite.CreateTextureFromFile(@"Content\menu-button-background.png");
            this.Width = Texture.Width;
            this.Height = Texture.Height;

            TextFont = new Font(DefaultFontFileName, 20);
            TextFontsm = new Font(DefaultFontFileName, 15);

            SeaTitle = new Label()
            {
                Font = TextFont,
                Color = Color.Black,
                Left = 163,
                Top = 9,
            };
            CreatorInfo = new Label()
            {
                Font = TextFontsm,
                Color = Color.Black,
                Left = 163,
                Top = 32,
            };
            MapTitle = new Label()
            {
                Font = TextFontsm,
                Color = Color.Black,
                Left = 163,
                Top = 50,
            };
            float scale = (float)1.0 * 85 / 298;
            MapSprite = new Control()
            {
                Texture=Sprite.CreateTextureFromFile(Map.PreviewImagePath),
                Width = (int)(530 * scale),
                Height = (int)(298 * scale) + 1,
                Margin = Origins.CenterLeft,
                Left = 8,
                Color = Color.White,
            };

            Controls.Add(SeaTitle);
            Controls.Add(CreatorInfo);
            Controls.Add(MapTitle);
            Controls.Add(MapSprite);
            base.Initialize();
            SeaTitle.Text = Map.MapName;
            CreatorInfo.Text = Map.Creator;
            MapTitle.Text = Map.Title;

        }
    }
}
