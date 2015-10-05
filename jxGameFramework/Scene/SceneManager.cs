using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Components;
using Microsoft.Xna.Framework;

namespace jxGameFramework.Scene
{
    public class SceneManager : DrawableComponent
    {
        private Dictionary<string, BaseScene> _scenedict = new Dictionary<string, BaseScene>();
        private Game _basegame;
        public BaseScene PresentScene;
        public SceneManager(Game _base)
        {
            _basegame = _base;
        }
        //TODO: event
        public override int DrawOrder { get; set; }
        public override bool Visible { get; set; } = true;
        public override bool Enabled { get; set; } = true;
        public override int UpdateOrder { get; set; }
        public override event EventHandler<EventArgs> DrawOrderChanged;
        public override event EventHandler<EventArgs> VisibleChanged;
        public override event EventHandler<EventArgs> EnabledChanged;
        public override event EventHandler<EventArgs> UpdateOrderChanged;
        public void Add(string key,BaseScene scene)
        {
            scene.Parent = _basegame;
            scene.Initialize();
            _scenedict.Add(key, scene);
        }
        public void Navigate(string key)
        {
            if (PresentScene != null)
            {
                PresentScene.OnLeave(this, EventArgs.Empty);
            }
            PresentScene = _scenedict[key];
            PresentScene.OnShow(this, EventArgs.Empty);
        }
        public override void Draw(GameTime gameTime)
        {
            if (PresentScene != null)
                PresentScene.Draw(gameTime);
        }
        public override void Dispose()
        {
            
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if(PresentScene!=null)
                PresentScene.Update(gameTime);
        }      
    }
}
