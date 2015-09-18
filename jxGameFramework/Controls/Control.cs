using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using jxGameFramework.Components;
using jxGameFramework.Data;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace jxGameFramework.Controls
{
    /// <summary>
    /// 控件
    /// </summary>
    
    public class KeyEventArgs : EventArgs
    {
        public KeyboardState State { get; set; }
        public KeyEventArgs(KeyboardState state)
        {
            this.State = state;
        }
    }
    public class MouseEventArgs : EventArgs
    {
        public MouseState State { get; set; }
        public MouseEventArgs (MouseState state)
        {
            this.State = state;
        }
    }
    public delegate void KeyEventHandler(object sender,KeyEventArgs e);
    public delegate void MouseEventHandler(object sender,MouseEventArgs e);
    public class Control : Sprite
    {
        public event MouseEventHandler Click;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseLeave;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseEnter;
        public event MouseEventHandler MouseUp;
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;


        protected bool isClicked = false;
        protected bool isMouseDown = false;
        protected bool isKeyDown = false;
        protected bool MouseInRect = false;
        protected bool isEnter = false;
        protected bool isLeave = true;

        Label _content;
        string _toolstrip;
        Font _fnt;

        bool _toolstripinstancecreated = false;
        public static Control Empty(GraphicsDevice gd, SpriteBatch sb)
        {
            var s = new Control()
            {
                Top = 0,
                Left = 0,
                Width = 1,
                Height = 1,
                GraphicsDevice = gd,
                SpriteBatch = sb,
                Color = Color.White,
                Margin = Origins.TopLeft,
            };
            return s;
        }
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
                    _content = new Label()
                    {
                        Font = _fnt,
                        Color = Color.White,
                        Margin = Origins.TopLeft,
                        Left = 3,
                        Top = 1,
                        SpriteBatch=this.SpriteBatch,
                        GraphicsDevice=this.GraphicsDevice,
                    };
                    _toolstripinstancecreated = true;
                }
                _content.Text = value;
            }
        }

        protected virtual void OnClick(object sender, MouseEventArgs e)
        {
            if (!isClicked)
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
            MouseInRect = true;
        }

        protected virtual void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if(!isLeave)
            {
                if (MouseLeave != null)
                    MouseLeave(sender, e);
                isLeave = true;
                isEnter = false;
                MouseInRect = false;
            }
        }

        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!isMouseDown)
            {
                if (MouseDown!=null)
                {
                    MouseDown(sender, e);
                }
                isMouseDown = true;
            }
        }
        protected virtual void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (MouseUp != null)
                MouseUp(sender, e);
            isMouseDown = false;
        }
        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(!isKeyDown)
            {
                if ((KeyDown != null))
                    KeyDown(sender, e);
                isKeyDown = true;
            }

        }
        protected void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (KeyUp != null)
                KeyUp(sender, e);
            isKeyDown = false;

        }
        protected void OnMouseEnter(object sender,MouseEventArgs e)
        {
            if (e.State.LeftButton == ButtonState.Released && e.State.RightButton == ButtonState.Released)
            {
                if (!isEnter)
                {
                    if (MouseEnter != null)
                        MouseEnter(sender, e);
                    isEnter = true;
                    isLeave = false;

                }
            }
        }

        protected virtual void DrawToolStrip(GameTime gameTime)
        {
            if (MouseInRect && _content != null)
            {
                var PosX = Mouse.GetState().X + 5;
                var PosY= Mouse.GetState().Y + 5;
                SpriteBatch.FillRectangle(new Rectangle(PosX, PosY, _content.Width + 7, _content.Height + 2), Color.Black);
                SpriteBatch.DrawRectangle(new Rectangle(PosX, PosY, _content.Width + 7, _content.Height + 2), Color.White, 1f);
                _content.Left = PosX + 3;
                _content.Top = PosY;
                _content.Draw(gameTime);
            }
        }

        public virtual void UpdateEvent(GameTime gameTime)
        {
            var _mState = Mouse.GetState();
            
            var tRectangle = new Rectangle(RenderX, RenderY, Width, Height);
            var nPosX = _mState.X - RenderX;
            var nPosY = _mState.Y - RenderY;

            var mState = new MouseState(nPosX, nPosY, _mState.ScrollWheelValue, _mState.LeftButton, _mState.MiddleButton, _mState.RightButton, _mState.XButton1, _mState.XButton2);
            
            var args = new MouseEventArgs(mState);
            
            if (Keyboard.GetState().GetPressedKeys().Count<Microsoft.Xna.Framework.Input.Keys>() != 0)
                OnKeyDown(this, new KeyEventArgs(Keyboard.GetState()));
            else
            {
                OnKeyUp(this, new KeyEventArgs(Keyboard.GetState()));
            }

            if (Texture == null)
                return;
            var mColor = GetPixel(nPosX, nPosY);
            if (tRectangle.Contains(_mState.Position) & mColor != Color.Transparent)
            {
                OnMouseEnter(this, args);
                if (isEnter)
                {
                    OnMouseMove(this, args);
                    if (mState.LeftButton == ButtonState.Pressed || mState.RightButton == ButtonState.Pressed)
                    {
                        OnMouseDown(this, args);
                    }
                    if (isMouseDown && (mState.LeftButton == ButtonState.Released && mState.RightButton == ButtonState.Released))
                    {
                        OnMouseUp(this, args);
                        OnClick(this, args);
                    }
                }
            }
            else
            {
                if (isEnter)
                {
                    OnMouseUp(this, args);
                    OnMouseLeave(this, args);           
                }
                
            }
        }
        public override void Update(GameTime gameTime)
        {
            UpdateEvent(gameTime);
            base.Update(gameTime);
        }
    }
}
