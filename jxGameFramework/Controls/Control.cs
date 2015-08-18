using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using jxGameFramework.Components;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;

namespace jxGameFramework.Controls
{
    /// <summary>
    /// 控件
    /// </summary>
    public class Control : Sprite
    {
        public event EventHandler Click;
        public event MouseEventHandler MouseMove;
        public event EventHandler MouseLeave;
        public event MouseEventHandler MouseDown;
        public event EventHandler MouseUp;
        public event EventHandler KeyDown;
        public event EventHandler KeyUp;


        protected bool isClicked = false;
        protected bool isMouseDown = false;
        protected bool isKeyDown = false;

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
        protected void OnKeyDown(object sender, EventArgs e)
        {
            if ((KeyDown != null) && (isKeyDown == false))
            {
                KeyDown(sender, e);
                isKeyDown = true;
            }
        }
        protected void OnKeyUp(object sender, EventArgs e)
        {
            if (KeyUp != null)
            {
                KeyUp(sender, e);
                isKeyDown = false;
            }

        }

        public virtual void UpdateEvent(GameTime gameTime)
        {
            var mState = Mouse.GetState();

            if (Keyboard.GetState().GetPressedKeys().Count<Microsoft.Xna.Framework.Input.Keys>() != 0)
                OnKeyDown(this, EventArgs.Empty);
            else
            {
                OnKeyUp(this, EventArgs.Empty);
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
                OnMouseMove(this, new MouseEventArgs(MouseButtons.None, 0, mState.X, mState.Y, mState.ScrollWheelValue));
                if (mState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    OnClick(this, EventArgs.Empty);
                    OnMouseDown(this, new MouseEventArgs(MouseButtons.Left, 0, mState.X, mState.Y, mState.ScrollWheelValue));
                    isClicked = true;
                }
            }
            else
            {
                OnMouseLeave(this, EventArgs.Empty);
            }
        }
        public override void Update(GameTime gameTime)
        {
            UpdateEvent(gameTime);
            base.Update(gameTime);
        }
    }
}
