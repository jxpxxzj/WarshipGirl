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
        Sprite MapSprite;
        Text SeaTitle;
        Text CreatorInfo;
        Text MapTitle;
        Font TextFont;
        Font TextFontsm;
        bool _selected;
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
                if(SeaTitle!=null)
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
                return SeaTitle.text;
            }
            set
            {
                SeaTitle.text = value;
            }
        }
        public string CreatorText
        {
            get
            {
                return CreatorInfo.text;
            }
            set
            {
                CreatorInfo.text = value;
            }
        }
        public string MapText
        {
            get
            {
                return MapTitle.text;
            }
            set
            {
                MapTitle.text = value;
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
                MapSprite.Texture = Sprite.CreateTextureFromFile(GraphicsDevice, value);
            }
        }

        public override void LoadContent()
        {
            this.Texture=Sprite.CreateTextureFromFile(GraphicsDevice,@"Content\menu-button-background.png");
            this.Width = Texture.Width;
            this.Height=Texture.Height;
            this.Color=Color.White;

            TextFont = new Font(GraphicsDevice,"msyh.ttc",20);
            TextFontsm = new Font(GraphicsDevice, "msyh.ttc", 15);

            SeaTitle = new Text()
            {
                Font = TextFont,
                Color = Color.Black,
                X = 138,
                Y = 9,
            };
            CreatorInfo = new Text()
            {
                Font = TextFontsm,
                Color = Color.Black,
                X = 138,
                Y = 32,
            };
            MapTitle = new Text()
            {
                Font = TextFontsm,
                Color = Color.Black,
                X = 138,
                Y = 50,
            };
            var temptext = Sprite.CreateTextureFromFile(GraphicsDevice, @"Content\Map_1-1.png");
            MapSprite=new Sprite()
            {
                Texture=temptext,
                Width=(int)(((double)Texture.Height / temptext.Height*0.85) * temptext.Width*0.85),
                Height=(int)(Texture.Height*0.85),
                Margin=Origins.CenterLeft,
                Left = 0,
                Color=Color.White,
            };

            AddComponent(SeaTitle);
            AddComponent(CreatorInfo);
            AddComponent(MapTitle);
            AddComponent(MapSprite);
            base.LoadContent();
        }
    }
}
