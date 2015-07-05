using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using jxGameFramework.Components;
using jxGameFramework.Scene;
using WarshipGirl.Scene;
using System.IO;
using System;
using WarshipGirl.Controls;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            this.IsMouseVisible = true;

            //注释这两行以关闭无限fps
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            
            this.Window.Title = "战舰少女 Remix";    
            Content.RootDirectory = "Content";
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
                OriginType = Origins.Center,
                SpriteBatch = spriteBatch,
                GraphicsDevice = this.GraphicsDevice,
                Texture = Texture2D.FromStream(this.GraphicsDevice, harborbg),
                X = 512,
                Y = 384,
                Width = 1024,
                Height = 768
            };
            harbor.LoadContent();

            var factbg = new FileStream(@"Content\factbg.png", FileMode.Open, FileAccess.Read);
            factory = new Factory()
            {
                ParentGame = this,
                Color = Color.White,
                OriginType = Origins.Center,
                SpriteBatch = spriteBatch,
                GraphicsDevice = this.GraphicsDevice,
                Texture = Texture2D.FromStream(this.GraphicsDevice, factbg),
                X = 512,
                Y = 384,
                Width = 1024,
                Height = 768
            };
            factory.LoadContent();

            fpscounter = new FpsCounter()
            {
                GraphicsDevice = this.GraphicsDevice,
                SpriteBatch = spriteBatch,
                Color = Color.White,
                X = 1024,
                Y = 768,
                OriginType = Origins.BottomRight
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            PresentScene.Update(gameTime);
            fpscounter.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            PresentScene.Draw(gameTime);
            fpscounter.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
