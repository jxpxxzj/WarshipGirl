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
    public enum FrameLimit
    {
        Unlimited,
        VSync,
        Custom,
    }
    internal class BaseGame : Microsoft.Xna.Framework.Game
    {
        internal GraphicsDeviceManager graphics;
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
            graphics.PreferMultiSampling = true;
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
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
            g.OnDeactivated();
        }

        void BaseGame_Activated(object sender, EventArgs e)
        {
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
    //TODO: dialog, messagebox
    public class Game : Control
    {
        Stopwatch watch = new Stopwatch();
        Stopwatch watch2 = new Stopwatch();
        Stopwatch watch3 = new Stopwatch();
        protected FpsCounter FpsCounter { get; set; }
        internal BaseGame baseGame;
        int _framelimitcount = 120;
        FrameLimit _fl = FrameLimit.Custom;

        public int FrameLimitCount
        {
            get
            {
                return _framelimitcount;
            }
            set
            {
                _framelimitcount = value;
                if(FrameLimter == FrameLimit.Custom)
                    SetFrameLimit(value);
            }
        }
        
        public FrameLimit FrameLimter
        {
            get
            {
                return _fl;
            }
            set
            {
                _fl = value;
                switch (_fl)
                {
                    case FrameLimit.Unlimited:
                        SetFrameLimit(-1);
                        break;
                    case FrameLimit.VSync:
                        SetFrameLimit(-2);
                        break;
                    case FrameLimit.Custom:
                        SetFrameLimit(FrameLimitCount);
                        break;
                    default:
                        break;
                }
            }
        }
        public bool isFullScreen
        {
            get
            {
                return baseGame.graphics.IsFullScreen;
            }
            set
            {
                baseGame.graphics.IsFullScreen = value;
            }
        }
        public bool isActive
        {
            get
            {
                return baseGame.IsActive;
            }
        }
        public bool isMouseVisible
        {
            get
            {
                return baseGame.IsMouseVisible;
            }
            set
            {
                baseGame.IsMouseVisible = value;
            }
        }
        public Vector2 Resolution
        {
            get
            {
                return new Vector2(baseGame.graphics.PreferredBackBufferWidth, baseGame.graphics.PreferredBackBufferHeight);
            }
            set
            {
                baseGame.graphics.PreferredBackBufferWidth = (int)value.X;
                baseGame.graphics.PreferredBackBufferHeight = (int)value.Y;
            }
        }
        public void ToggleFullScreen()
        {
            baseGame.graphics.ToggleFullScreen();
        }
        public Game()
        {
            Graphics.Instance.Game = this;
        }
        public SceneManager Scenes { get; set; }

        public void Run()
        {
            Scenes = new SceneManager(this);
            baseGame = new BaseGame(this);
            
            SetFrameLimit(-1);
            baseGame.Run();
        }
        private void SetFrameLimit(int fps,bool remember = true)
        {
            if(remember)
                _framelimitcount = fps;
            if (fps==-1)
            {
                baseGame.graphics.SynchronizeWithVerticalRetrace = false;
                baseGame.IsFixedTimeStep = false;
            }
            else
            {
                if(fps==-2)
                {
                    baseGame.graphics.SynchronizeWithVerticalRetrace = true;
                    baseGame.IsFixedTimeStep = true;
                    baseGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 1000 / 60);
                }
                else
                {
                    baseGame.graphics.SynchronizeWithVerticalRetrace = false;
                    baseGame.IsFixedTimeStep = true;
                    baseGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 1000 / fps);
                }
            }
            baseGame.graphics.ApplyChanges();
        }
        public void Exit()
        {
            baseGame.Exit();
        }
        public GameWindow Window
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
                Margin = Origins.BottomRight,
                Width = 800,
                Height = 100,
            };
            FpsCounter.Initialize();
#if DEBUG
            FpsCounter.EnableFrameTime = true;
#endif
            this.Width = GraphicsDevice.Viewport.Width;
            this.Height = GraphicsDevice.Viewport.Height;
            this.Initialize();
        }

        internal virtual void OnDraw(GameTime gameTime)
        {
            watch.Restart();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();
            Scenes.Draw(gameTime);
            this.Draw(gameTime);  
            FpsCounter.Draw(gameTime);
            SpriteBatch.End();
            watch.Stop();
            FpsCounter.FrameTime = watch.Elapsed;
            watch2.Restart();
        }

        internal virtual void OnUpdate(GameTime gameTime)
        {
            watch2.Stop();
            FpsCounter.BetweenTime = watch2.Elapsed;
            watch3.Restart();
            Scenes.Update(gameTime);
            this.Update(gameTime);
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
            SetFrameLimit(FrameLimitCount);
            Activated();
        }
        internal virtual void OnDeactivated()
        {
            SetFrameLimit(30,false);
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
