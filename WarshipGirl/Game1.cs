using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using jxGameFramework.Components;
using jxGameFramework.Scene;
using WarshipGirl.Scene;
using System.IO;
using System;
using System.Diagnostics;
using WarshipGirl.Controls;
using jxGameFramework.Controls;
using jxGameFramework.Data;
using jxGameFramework;

namespace WarshipGirl
{
    internal class Game1 : jxGameFramework.Game
    {
        private static Game1 mInstance;

        private Harbor harbor;
        //internal GetShip getship;
        private Factory factory;
        private MapSelect select;

        internal bool isNightMode;

        string _globalmsg = "";
        TimeSpan _msgTime;
        Stopwatch _msgwatch = new Stopwatch();
        Font _msgfont;
        public static Game1 Instance
        {
            get
            {
                if(mInstance ==null)
                    mInstance = new Game1();
                return mInstance;
            }

        }
        public void ShowMessage(string Content, TimeSpan Time)
        {
            _globalmsg = Content;
            _msgTime = Time;
            _msgwatch.Restart();
        }

        public override void Initialize()
        {
            this.Window.Title = "战舰少女 Remix";    
            isNightMode = true;
            FileStream harborbg;
            if(isNightMode)
                harborbg = new FileStream(@"Content\dark_harbor.png", FileMode.Open, FileAccess.Read);
            else
                harborbg = new FileStream(@"Content\day_harbor.png", FileMode.Open, FileAccess.Read);
            _msgfont = new Font("msyh.ttc", 20)
            {
                EnableBorder = true,
                BorderColor = Color.Black
            };
            harbor = new Harbor()
            {
                Texture = Texture2D.FromStream(jxGameFramework.Graphics.Instance.GraphicsDevice, harborbg),
            };

            var factbg = new FileStream(@"Content\factbg.png", FileMode.Open, FileAccess.Read);
            factory = new Factory()
            {
                Texture = Texture2D.FromStream(jxGameFramework.Graphics.Instance.GraphicsDevice, factbg),
            };

            select = new MapSelect();
            FpsCounter.KeyDown += counter_KeyDown;
            Scenes.Add("Harbor", harbor);
            Scenes.Add("Factory", factory);
            Scenes.Add("Select", select);

            base.Initialize();
            Scenes.Navigate("Harbor");
            
        }
        void counter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.State.IsKeyDown(Keys.F1))
            {
                FpsCounter.EnableFrameTime = !FpsCounter.EnableFrameTime;
                if (FpsCounter.EnableFrameTime)
                    ShowMessage("Frame times are now visible.", new TimeSpan(0, 0, 1));
                else
                    ShowMessage("Frame times are now hidden.", new TimeSpan(0, 0, 1));
            }
            if (e.State.IsKeyDown(Keys.F2))
            {
                FpsCounter.Visible = !FpsCounter.Visible;
                if (FpsCounter.Visible)
                    ShowMessage("Fps counter are now visible.", new TimeSpan(0, 0, 1));
                else
                    ShowMessage("Fps counter are now hidden.", new TimeSpan(0, 0, 1));
            }

        }
        public override void Update(GameTime gameTime)
        {
            if (_msgwatch.ElapsedMilliseconds > _msgTime.TotalMilliseconds)
            {
                _globalmsg = "";
                _msgwatch.Stop();
            }       
        }

        
        public override void Draw(GameTime gameTime)
        {
            if (_globalmsg != "")
            {
                jxGameFramework.Graphics.Instance.SpriteBatch.FillRectangle(new Rectangle(0, jxGameFramework.Graphics.Instance.GraphicsDevice.Viewport.Height / 2 - 20, jxGameFramework.Graphics.Instance.GraphicsDevice.Viewport.Width, 40), new Color(0, 0, 0, 128));
                Vector2 size = _msgfont.MeasureString(_globalmsg);
                _msgfont.DrawText(new Vector2((jxGameFramework.Graphics.Instance.GraphicsDevice.Viewport.Width - size.X) / 2, (jxGameFramework.Graphics.Instance.GraphicsDevice.Viewport.Height - size.Y) / 2), _globalmsg, Color.White);
            }
        }
    }
}
