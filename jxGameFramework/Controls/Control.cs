using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using jxGameFramework.Components;
using jxGameFramework.Data;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace jxGameFramework.Controls
{
    /// <summary>
    /// 控件
    /// </summary>
    
    public class XnaKeyEventArgs : EventArgs
    {
        public KeyboardState State { get; set; }
        public XnaKeyEventArgs(KeyboardState state)
        {
            this.State = state;
        }
    }
    public delegate void XnaKeyEventHandler(object sender,XnaKeyEventArgs e);
    public class Control : Sprite
    {
        public event EventHandler Click;
        public event MouseEventHandler MouseMove;
        public event EventHandler MouseLeave;
        public event MouseEventHandler MouseDown;
        public event EventHandler MouseUp;
        public event XnaKeyEventHandler KeyDown;
        public event XnaKeyEventHandler KeyUp;


        protected bool isClicked = false;
        protected bool isMouseDown = false;
        protected bool isKeyDown = false;
        protected bool MouseInRect = false;

        //Sprite _strip;
        Text _content;
        string _toolstrip;
        Font _fnt;

        bool _toolstripinstancecreated = false;

        public string ToolStrip
        {
            get
            {
                return _toolstrip;
            }
            set
            {
                _toolstrip = value;
                
                if(!_toolstripinstancecreated)
                {
                    _fnt = new Font(GraphicsDevice, "msyh.ttc", 12);
                    _content = new Text()
                    {
                        Font = _fnt,
                        Color = Color.White,
                        OriginType = Origins.TopLeft,
                        X = 3,
                        Y = 1,
                        text = _toolstrip,
                        SpriteBatch=this.SpriteBatch,
                        GraphicsDevice=this.GraphicsDevice,
                    };
                    _toolstripinstancecreated = true;
                }
                _content.text = value;
            }
        }

        protected virtual void OnClick(object sender, EventArgs e)
        {
            if (isClicked == false)
            {
                if (Click != null)
                    Click(sender, e);
                isClicked = false;
            }
        }

        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (MouseMove != null)
                MouseMove(sender, e);
        }

        protected virtual void OnMouseLeave(object sender, EventArgs e)
        {
            if (MouseLeave != null)
                MouseLeave(sender, e);
        }

        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (MouseDown != null)
                if (isMouseDown == false)
                {
                    MouseDown(sender, e);
                    isMouseDown = true;
                }
        }
        protected virtual void OnMouseUp(object sender, EventArgs e)
        {
            if (MouseUp != null)
                MouseUp(sender, e);
            isMouseDown = false;
        }
        protected void OnKeyDown(object sender, XnaKeyEventArgs e)
        {
            if ((KeyDown != null) && (isKeyDown == false))
            {
                KeyDown(sender, e);
                isKeyDown = true;
            }
        }
        protected void OnKeyUp(object sender, XnaKeyEventArgs e)
        {
            if (KeyUp != null)
            {
                KeyUp(sender, e);
                isKeyDown = false;
            }

        }
        public virtual void DrawToolStrip(GameTime gameTime)
        {
            if (MouseInRect && _content != null)
            {
                var PosX = Mouse.GetState().X + 5;
                var PosY= Mouse.GetState().Y + 5;
                SpriteBatch.FillRectangle(new Rectangle(PosX, PosY, _content.Width + 7, _content.Height + 2), Color.Black);
                SpriteBatch.DrawRectangle(new Rectangle(PosX, PosY, _content.Width + 7, _content.Height + 2), Color.White, 1f);
                _content.X = PosX + 3;
                _content.Y = PosY;
                _content.Draw(gameTime);
            }
        }
        public virtual void UpdateEvent(GameTime gameTime)
        {
            var mState = Mouse.GetState();

            if (Keyboard.GetState().GetPressedKeys().Count<Microsoft.Xna.Framework.Input.Keys>() != 0)
                OnKeyDown(this, new XnaKeyEventArgs(Keyboard.GetState()));
            else
            {
                OnKeyUp(this, new XnaKeyEventArgs(Keyboard.GetState()));
                isKeyDown = false;
            }
            if (mState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                isClicked = false;
                OnMouseUp(this, EventArgs.Empty);
            }
            if (Texture == null)
                return;

            var tRectangle = new Rectangle(RenderX, RenderY, Width, Height);
            var nPosX = mState.X - RenderX;
            var nPosY = mState.Y - RenderY;
            var mColor = GetPixel(nPosX, nPosY);
            if (tRectangle.Contains(mState.X, mState.Y) && (mColor != Color.Transparent))
            {
                OnMouseMove(this, new MouseEventArgs(MouseButtons.None, 0, nPosX, nPosY, mState.ScrollWheelValue));
                MouseInRect = true;
                if (mState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    OnClick(this, EventArgs.Empty);
                    OnMouseDown(this, new MouseEventArgs(MouseButtons.Left, 0, nPosX, nPosY, mState.ScrollWheelValue));
                    isClicked = true;
                }
            }
            else
            {
                OnMouseLeave(this, EventArgs.Empty);
                MouseInRect = false;
            }
        }
        public override void Update(GameTime gameTime)
        {
            UpdateEvent(gameTime);
            base.Update(gameTime);
        }
    }
}
