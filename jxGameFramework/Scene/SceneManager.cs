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
    public class SceneManager : DrawableComponent ,IDictionary<string,BaseScene> , ICollection<KeyValuePair<string,BaseScene>> , IEnumerable<KeyValuePair<string,BaseScene>>
    {
        private Dictionary<string, BaseScene> _scenedict = new Dictionary<string, BaseScene>();
        private Game _basegame;
        public BaseScene PresentScene;
        public SceneManager(Game _base)
        {
            _basegame = _base;
        }

        public ICollection<string> Keys
        {
            get
            {
                return ((IDictionary<string, BaseScene>)_scenedict).Keys;
            }
        }

        public ICollection<BaseScene> Values
        {
            get
            {
                return ((IDictionary<string, BaseScene>)_scenedict).Values;
            }
        }

        public int Count
        {
            get
            {
                return ((IDictionary<string, BaseScene>)_scenedict).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IDictionary<string, BaseScene>)_scenedict).IsReadOnly;
            }
        }

        public BaseScene this[string key]
        {
            get
            {
                return ((IDictionary<string, BaseScene>)_scenedict)[key];
            }

            set
            {
                ((IDictionary<string, BaseScene>)_scenedict)[key] = value;
            }
        }

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

        public bool ContainsKey(string key)
        {
            return ((IDictionary<string, BaseScene>)_scenedict).ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return ((IDictionary<string, BaseScene>)_scenedict).Remove(key);
        }

        public bool TryGetValue(string key, out BaseScene value)
        {
            return ((IDictionary<string, BaseScene>)_scenedict).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<string, BaseScene> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            ((IDictionary<string, BaseScene>)_scenedict).Clear();
        }

        public bool Contains(KeyValuePair<string, BaseScene> item)
        {
            return ((IDictionary<string, BaseScene>)_scenedict).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, BaseScene>[] array, int arrayIndex)
        {
            ((IDictionary<string, BaseScene>)_scenedict).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, BaseScene> item)
        {
            return ((IDictionary<string, BaseScene>)_scenedict).Remove(item);
        }

        public IEnumerator<KeyValuePair<string, BaseScene>> GetEnumerator()
        {
            return ((IDictionary<string, BaseScene>)_scenedict).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, BaseScene>)_scenedict).GetEnumerator();
        }
    }
}
