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
    public class SpriteCollection : DrawableComponent,IEnumerable<Sprite>,IList<Sprite>, ICollection<Sprite>
    {
        Sprite _base;
        List<Sprite> baselist = new List<Sprite>();

        public int Count
        {
            get
            {
                return ((IList<Sprite>)baselist).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<Sprite>)baselist).IsReadOnly;
            }
        }

        public Sprite this[int index]
        {
            get
            {
                return ((IList<Sprite>)baselist)[index];
            }

            set
            {
                ((IList<Sprite>)baselist)[index] = value;
            }
        }

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

        public int IndexOf(Sprite item)
        {
            return ((IList<Sprite>)baselist).IndexOf(item);
        }

        public void Insert(int index, Sprite item)
        {
            ((IList<Sprite>)baselist).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Sprite>)baselist).RemoveAt(index);
        }

        public bool Contains(Sprite item)
        {
            return ((IList<Sprite>)baselist).Contains(item);
        }

        public void CopyTo(Sprite[] array, int arrayIndex)
        {
            ((IList<Sprite>)baselist).CopyTo(array, arrayIndex);
        }

        public bool Remove(Sprite item)
        {
            return ((IList<Sprite>)baselist).Remove(item);
        }
    }
}
