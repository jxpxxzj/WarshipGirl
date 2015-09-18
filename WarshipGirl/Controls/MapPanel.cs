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
        bool _selected=false;
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
                MapSprite.Texture = Sprite.CreateTextureFromFile(GraphicsDevice, value);
            }
        }

        public override void LoadContent()
        {
            this.Texture=Sprite.CreateTextureFromFile(GraphicsDevice,@"Content\menu-button-background.png");
            this.Width = Texture.Width;
            this.Height=Texture.Height;

            TextFont = new Font(GraphicsDevice,"msyh.ttc",20);
            TextFontsm = new Font(GraphicsDevice, "msyh.ttc", 15);

            SeaTitle = new Label()
            {
                Font = TextFont,
                Color = Color.Black,
                Margin=Origins.TopLeft,
                Left = 138,
                Top = 9,
            };
            CreatorInfo = new Label()
            {
                Font = TextFontsm,
                Color = Color.Black,
                Margin=Origins.TopLeft,
                Left = 138,
                Top = 32,
            };
            MapTitle = new Label()
            {
                Font = TextFontsm,
                Color = Color.Black,
                Margin=Origins.TopLeft,
                Left = 138,
                Top = 50,
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
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
