using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using jxGameFramework;
using jxGameFramework.Scene;
using jxGameFramework.Components;
using jxGameFramework.Controls;
using System.Diagnostics;

namespace jxGameFramework
{
    internal class BaseGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Game g;
        public BaseGame(Game game)
        {
            g = game;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            this.IsMouseVisible=true;
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            this.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 1000 / 30);
            this.Activated += BaseGame_Activated;
            this.Deactivated += BaseGame_Deactivated;
            this.Exiting += BaseGame_Exiting;
        }
        void BaseGame_Exiting(object sender, EventArgs e)
        {
            g.OnExisting();
        }

        void BaseGame_Deactivated(object sender, EventArgs e)
        {
            graphics.SynchronizeWithVerticalRetrace = true;
            this.IsFixedTimeStep = true;
            g.OnDeactivated();
        }

        void BaseGame_Activated(object sender, EventArgs e)
        {
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            g.OnActivated();
        }
        protected override void Initialize()
        {       
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Graphics.Instance.SpriteBatch = spriteBatch;
            Graphics.Instance.GraphicsDevice = GraphicsDevice;
            g.OnInitalize();
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            g.OnUpdate(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            g.OnDraw(gameTime);
            base.Draw(gameTime);
        }
        protected override void UnloadContent()
        {
            g.OnDispose();
            base.UnloadContent();
        }
    }

    public class Game : Control
    {
        Stopwatch watch = new Stopwatch();
        Stopwatch watch2 = new Stopwatch();
        Stopwatch watch3 = new Stopwatch();
        protected FpsCounter FpsCounter;
        internal BaseGame baseGame;

        public void Run()
        {
            baseGame = new BaseGame(this);
            baseGame.Run();
        }
        public void Exit()
        {
            baseGame.Exit();
        }
        public Microsoft.Xna.Framework.GameWindow Window
        {
            get
            {
                return baseGame.Window;
            }
        }
        internal virtual void OnInitalize()
        {
            FpsCounter = new FpsCounter()
            {
                Color = Color.White,
                Margin = Origins.BottomRight,
                Width = 800,
                Height = 100,
            };
            FpsCounter.Initialize();
#if DEBUG
            FpsCounter.EnableFrameTime = true;
#endif
            this.Width = Graphics.Instance.GraphicsDevice.Viewport.Width;
            this.Height = Graphics.Instance.GraphicsDevice.Viewport.Height;
            this.Initialize();
        }

        internal virtual void OnDraw(GameTime gameTime)
        {
            watch.Restart();
            Graphics.Instance.GraphicsDevice.Clear(Color.CornflowerBlue);
            Graphics.Instance.SpriteBatch.Begin();
            this.Draw(gameTime);
            FpsCounter.Draw(gameTime);
            Graphics.Instance.SpriteBatch.End();
            watch.Stop();
            FpsCounter.FrameTime = watch.Elapsed;
            watch2.Restart();
        }

        internal virtual void OnUpdate(GameTime gameTime)
        {
            watch2.Stop();
            FpsCounter.BetweenTime = watch2.Elapsed;
            watch3.Restart();
            if(this.baseGame.IsActive)
            {
                this.Update(gameTime);
            }
            FpsCounter.Update(gameTime);
            watch3.Stop();
            FpsCounter.UpdateTime = watch3.Elapsed;
        }
        internal virtual void OnDispose()
        {
            Dispose();
        }
        internal virtual void OnActivated()
        {
            Activated();
        }
        internal virtual void OnDeactivated()
        {
            Deactivated();
        }
        internal virtual void OnExisting()
        {
            Existing();
        }


        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Dispose()
        {
            base.Dispose();
        }

        protected virtual void Activated()
        {

        }
        protected virtual void Deactivated()
        {

        }
        protected virtual void Existing()
        {

        }
    }
}
