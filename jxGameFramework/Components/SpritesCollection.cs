using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using jxGameFramework.Animations;
using System.Collections;

namespace jxGameFramework.Components
{
    public class SpriteCollection : DrawableComponent,IEnumerable<Sprite>
    {
        Sprite _base;
        List<Sprite> baselist = new List<Sprite>();

        public override event EventHandler<EventArgs> DrawOrderChanged;
        public override event EventHandler<EventArgs> VisibleChanged;
        public override event EventHandler<EventArgs> EnabledChanged;
        public override event EventHandler<EventArgs> UpdateOrderChanged;

        //TODO: event
        public override int DrawOrder { get; set; }
        public override bool Visible { get; set; } = true;
        public override bool Enabled { get; set; } = true;
        public override int UpdateOrder { get; set; }

        public SpriteCollection(Sprite basesprite)
        {
            _base = basesprite;
        }
        public void Add(Sprite s)
        {
            s.Parent = _base;
            baselist.Add(s);
        }
        public void AddRange(Sprite[] s)
        {
            foreach(Sprite sp in s)
            {
                Add(sp);
            }
        }
        public void Clear()
        {
            baselist.Clear();
        }
        
        public override void Dispose()
        {
            foreach (Sprite s in baselist)
                s.Dispose();
        }

        public override void Draw(GameTime gameTime)
        {
            if(Visible)
                foreach (Sprite s in baselist)
                    if (s.Visible)
                        s.Draw(gameTime);

        }

        public override void Initialize()
        {
            foreach (Sprite s in baselist)
                s.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(Enabled)
                foreach (Sprite s in baselist)
                    if (s.Enabled)
                        s.Update(gameTime);
        }

        public IEnumerator<Sprite> GetEnumerator()
        {
            return ((IEnumerable<Sprite>)baselist).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Sprite>)baselist).GetEnumerator();
        }
    }
}
