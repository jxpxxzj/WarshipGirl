using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using Microsoft.Xna.Framework;

namespace WarshipGirl.Controls
{
    class MapPanel : Control
    {
        public Sprite MapSprite { get; set; }
        Label SeaTitle;
        Label CreatorInfo;
        Label MapTitle;
        Font TextFont;
        Font TextFontsm;
        bool _selected = false;
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
        public string SeaText
        {
            get
            {
                return SeaTitle.Text;
            }
            set
            {
                SeaTitle.Text = value;
            }
        }
        public string CreatorText
        {
            get
            {
                return CreatorInfo.Text;
            }
            set
            {
                CreatorInfo.Text = value;
            }
        }
        public string MapText
        {
            get
            {
                return MapTitle.Text;
            }
            set
            {
                MapTitle.Text = value;
            }
        }
        string _pp;
        public string PreviewPath
        {
            get
            {
                return _pp;
            }
            set
            {
                _pp = value;
                MapSprite.Texture = Sprite.CreateTextureFromFile(value);
            }
        }

        public override void Initialize()
        {
            this.Texture = Sprite.CreateTextureFromFile(@"Content\menu-button-background.png");
            this.Width = Texture.Width;
            this.Height = Texture.Height;

            TextFont = new Font("msyh.ttc", 20);
            TextFontsm = new Font("msyh.ttc", 15);

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
            var temptext = Sprite.CreateTextureFromFile(@"Content\Map_1-1.png");
            float scale = (float)1.0 * 85 / 298;
            MapSprite = new Sprite()
            {
                Texture = temptext,
                Width = (int)(530 * scale),
                Height = (int)(298 * scale) + 1,
                Margin = Origins.CenterLeft,
                Left = 8,
                Color = Color.White,
                Scale = new Vector2(scale, scale),
            };

            ChildSprites.Add(SeaTitle);
            ChildSprites.Add(CreatorInfo);
            ChildSprites.Add(MapTitle);
            ChildSprites.Add(MapSprite);
            base.Initialize();
        }
    }
}
