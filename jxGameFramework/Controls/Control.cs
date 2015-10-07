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
        public MouseEventArgs(MouseState state)
        {
            this.State = state;
        }
    }
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
    //TODO: focus
    public class Control : Sprite
    {
        public static string DefaultFontFileName { get; set; } = "msyh.ttc";
        public static Color DefaultFocusColor { get; set; } = Color.DeepPink;

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
        public new static Control Empty
        {
            get
            {
                var s = new Control()
                {
                    Top = 0,
                    Left = 0,
                    Width = 1,
                    Height = 1,
                    Color = Color.White,
                    Margin = Origins.TopLeft,
                };
                return s;
            }
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

                if (!_toolstripinstancecreated)
                {
                    _fnt = new Font(DefaultFontFileName,12);
                    _content = new Label()
                    {
                        Font = _fnt,
                        Color = Color.White,
                        Margin = Origins.TopLeft,
                        Left = 3,
                        Top = 1,
                    };
                    _toolstripinstancecreated = true;
                }
                _content.Text = value;
            }
        }

        protected virtual void OnClick(object sender, MouseEventArgs e)
        {
            if (Click != null)
                Click(sender, e);
        }

        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (MouseMove != null)
                MouseMove(sender, e);
        }
        protected virtual void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (MouseLeave != null)
                MouseLeave(sender, e);
        }

        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (MouseDown != null)
                MouseDown(sender, e);
        }
        protected virtual void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (MouseUp != null)
                MouseUp(sender, e);
        }
        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (KeyDown != null)
                KeyDown(sender, e);
        }
        protected void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (KeyUp != null)
                KeyUp(sender, e);
        }
        protected void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (MouseEnter != null)
                MouseEnter(sender, e);
        }

        protected virtual void DrawToolStrip(GameTime gameTime)
        {
            if (MouseInRect && _content != null)
            {
                var PosX = Mouse.GetState().X + 5;
                var PosY = Mouse.GetState().Y + 5;
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

            var tRectangle = DestRect;
            var nPosX = _mState.X - DrawingX;
            var nPosY = _mState.Y - DrawingY;

            var mState = new MouseState(nPosX, nPosY, _mState.ScrollWheelValue, _mState.LeftButton, _mState.MiddleButton, _mState.RightButton, _mState.XButton1, _mState.XButton2);

            var args = new MouseEventArgs(mState);

            if (Keyboard.GetState().GetPressedKeys().Count() != 0)
            {
                if (!isKeyDown)
                {
                    OnKeyDown(this, new KeyEventArgs(Keyboard.GetState()));
                    isKeyDown = true;
                }
            }
            else
            {
                OnKeyUp(this, new KeyEventArgs(Keyboard.GetState()));
                isKeyDown = false;
            }
            if (Texture == null)
                return;
            var mColor = GetPixel(nPosX, nPosY);
            if (tRectangle.Contains(_mState.Position) & mColor != Color.Transparent)
            {
                if (mState.LeftButton == ButtonState.Released && mState.RightButton == ButtonState.Released)
                {
                    if (!isEnter)
                    {
                        OnMouseEnter(this, args);
                        isEnter = true;
                        isLeave = false;

                    }
                }
                if (isEnter)
                {
                    OnMouseMove(this, args);
                    MouseInRect = true;
                    if (mState.LeftButton == ButtonState.Pressed || mState.RightButton == ButtonState.Pressed)
                    {
                        if (!isMouseDown)
                        {
                            OnMouseDown(this, args);
                            isMouseDown = true;
                        }
                    }
                    if (isMouseDown && (mState.LeftButton == ButtonState.Released && mState.RightButton == ButtonState.Released))
                    {
                        OnMouseUp(this, args);
                        isMouseDown = false;
                        if (!isClicked)
                        {
                            OnClick(this, args);
                            isClicked = false;
                        }
                    }
                }
            }
            else
            {
                if (isEnter)
                {
                    OnMouseUp(this, args);
                    isMouseDown = false;
                    if (!isLeave)
                    {
                        OnMouseLeave(this, args);
                        isLeave = true;
                        isEnter = false;
                        MouseInRect = false;
                    }
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (Game.isActive)
                UpdateEvent(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
