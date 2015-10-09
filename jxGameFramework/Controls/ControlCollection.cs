using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using jxGameFramework.Animations;
using System.Collections;
using jxGameFramework.Components;

namespace jxGameFramework.Controls
{
    public class ControlEventArgs : EventArgs
    {
        public Control Control { get; private set; }
        public ControlEventArgs(Control c)
        {
            Control = c;
        }
    }
    public class ControlCollection : DrawableComponent,IEnumerable<Control>,IList<Control>, ICollection<Control>
    {
        Control _base;
        List<Control> baselist = new List<Control>();
        public event EventHandler<ControlEventArgs> ControlAdded;
        public event EventHandler<ControlEventArgs> ControlRemoved;
        protected void OnControlAdded(object sender, ControlEventArgs e)
        {
            if (ControlAdded != null)
                ControlAdded(sender, e);
        }
        protected void OnControlRemoved(object sender,ControlEventArgs e)
        {
            if (ControlRemoved != null)
                ControlRemoved(sender, e);
        }
        public int Count
        {
            get
            {
                return ((IList<Control>)baselist).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<Control>)baselist).IsReadOnly;
            }
        }

        public Control this[int index]
        {
            get
            {
                return ((IList<Control>)baselist)[index];
            }

            set
            {
                ((IList<Control>)baselist)[index] = value;
            }
        }

        public ControlCollection(Control basec)
        {
            _base = basec;
        }
        public void Add(Control s)
        {
            s.Parent = _base;
            baselist.Add(s);
            OnControlAdded(this, new ControlEventArgs(s));
        }
        public void AddRange(Control[] s)
        {
            foreach(Control sp in s)
                Add(sp);
        }
        public void Clear()
        {
            baselist.Clear();
        }
        
        public override void Dispose()
        {
            foreach (Control s in baselist)
                s.Dispose();
        }

        public override void Draw(GameTime gameTime)
        {
            if(Visible)
                foreach (Control s in baselist)
                    if (s.Visible)
                        s.Draw(gameTime);

        }

        public override void Initialize()
        {
            foreach (Control s in baselist)
                s.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(Enabled)
            {
                foreach (Control s in baselist)
                    if (s.Enabled)
                        s.Update(gameTime);
                UpdateEvent();
            }
        }
        public void UpdateEvent()
        {
            Control c;
            Control findControl = null;
            foreach(Control s in baselist)
            {
                c = s.GetEventControl();
                if(c!=null)
                    findControl = c;
            }
            if (MouseControl != findControl)
            {
                if(MouseControl!=null)
                    MouseControl.EnableEvent = false;
                MouseControl = findControl;
            }
            if (MouseControl != null)
                MouseControl.EnableEvent = true;
        }
        internal Control MouseControl;
        public IEnumerator<Control> GetEnumerator()
        {
            return ((IEnumerable<Control>)baselist).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Control>)baselist).GetEnumerator();
        }

        public int IndexOf(Control item)
        {
            return ((IList<Control>)baselist).IndexOf(item);
        }

        public void Insert(int index, Control item)
        {
            ((IList<Control>)baselist).Insert(index, item);
            item.Parent = _base;
            OnControlAdded(this,new ControlEventArgs(item));
        }

        public void RemoveAt(int index)
        {
            var target = baselist[index];
            ((IList<Control>)baselist).RemoveAt(index);
            OnControlRemoved(this, new ControlEventArgs(target));
        }

        public bool Contains(Control item)
        {
            return ((IList<Control>)baselist).Contains(item);
        }

        public void CopyTo(Control[] array, int arrayIndex)
        {
            ((IList<Control>)baselist).CopyTo(array, arrayIndex);
        }

        public bool Remove(Control item)
        {
            var result = ((IList<Control>)baselist).Remove(item);
            OnControlRemoved(this, new ControlEventArgs(item));
            return result;
        }
    }
}
