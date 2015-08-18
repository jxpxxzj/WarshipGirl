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

namespace WarshipGirl
{
    internal class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BaseScene PresentScene;

        internal Harbor harbor;
        //internal GetShip getship;
        internal Factory factory;

        FpsCounter fpscounter;
        internal bool isNightMode;

        Stopwatch watch = new Stopwatch(); //draw
        Stopwatch watch2 = new Stopwatch(); //between
        Stopwatch watch3 = new Stopwatch(); //update

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 600;
            this.IsMouseVisible = true;

            //注释这两行以关闭无限fps
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;

            //后台限制fps为30
            this.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 1000 / 30);
            this.Deactivated += Game1_Deactivated;
            this.Activated += Game1_Activated;
            
            this.Window.Title = "战舰少女 Remix";    
            Content.RootDirectory = "Content";
        }

        void Game1_Activated(object sender, System.EventArgs e)
        {
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
        }

        void Game1_Deactivated(object sender, System.EventArgs e)
        {
            graphics.SynchronizeWithVerticalRetrace = true;
            this.IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {
            graphics.ApplyChanges();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            isNightMode = false;
            FileStream harborbg;
            if(isNightMode)
                harborbg = new FileStream(@"Content\dark_harbor.png", FileMode.Open, FileAccess.Read);
            else
                harborbg = new FileStream(@"Content\day_harbor.png", FileMode.Open, FileAccess.Read);

            harbor = new Harbor()
            {
                ParentGame=this,
                Color = Color.White,
                //Margin = Origins.Center,
                SpriteBatch = spriteBatch,
                GraphicsDevice = this.GraphicsDevice,
                Texture = Texture2D.FromStream(this.GraphicsDevice, harborbg),
                Width = GraphicsDevice.Viewport.Width,
                Height = GraphicsDevice.Viewport.Height
            };
            harbor.LoadContent();

            var factbg = new FileStream(@"Content\factbg.png", FileMode.Open, FileAccess.Read);
            factory = new Factory()
            {
                ParentGame = this,
                Color = Color.White,
                //Margin = Origins.Center,
                SpriteBatch = spriteBatch,
                GraphicsDevice = this.GraphicsDevice,
                Texture = Texture2D.FromStream(this.GraphicsDevice, factbg),
                Width = GraphicsDevice.Viewport.Width,
                Height = GraphicsDevice.Viewport.Height
            };
            factory.LoadContent();

            fpscounter = new FpsCounter()
            {
                GraphicsDevice = this.GraphicsDevice,
                SpriteBatch = spriteBatch,
                Color = Color.White,
                X = GraphicsDevice.Viewport.Width,
                Y = GraphicsDevice.Viewport.Height,
                OriginType = Origins.BottomRight,
                EnableFrameTime=true,
            };
            fpscounter.LoadContent();

            Navigate(harbor);
            base.LoadContent();
        }
        public void Navigate(BaseScene scene)
        {
            if(PresentScene!=null)
            {
                PresentScene.OnUnload(this, EventArgs.Empty);
            }
            PresentScene = scene;
            PresentScene.OnLoad(this, EventArgs.Empty);
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            watch2.Stop();
            fpscounter.BetweenTime = watch2.Elapsed;
            watch3.Restart();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(this.IsActive)
                PresentScene.Update(gameTime);
            fpscounter.Update(gameTime);
            base.Update(gameTime);
            watch3.Stop();
            fpscounter.UpdateTime = watch3.Elapsed;
        }

        
        protected override void Draw(GameTime gameTime)
        {
            
            watch.Restart();

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            PresentScene.Draw(gameTime);
            fpscounter.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);

            watch.Stop();
            fpscounter.FrameTime = watch.Elapsed;
            
            
            watch2.Restart();
        }
    }
}
