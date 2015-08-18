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
        protected bool MouseInRect = false;

        Sprite _strip;
        Text _content;
        string _toolstrip;
        Font _fnt;

        public string ToolStrip
        {
            get
            {
                return _toolstrip;
            }
            set
            {
                _toolstrip = value;
                _fnt = new Font(GraphicsDevice, "msyh.ttc", 12);
                _content = new Text()
                {
                    Font = _fnt,
                    Color = Color.White,
                    OriginType = Origins.TopLeft,
                    X = 3,
                    Y = 1,
                    text=_toolstrip,
                };
                var gdip = new GDIpInterop(_content.Width + 8, _content.Height + 3, GraphicsDevice);
                gdip.g.FillRectangle(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(0, 0, _content.Width+7, _content.Height+2));
                gdip.g.DrawRectangle(System.Drawing.Pens.White, new System.Drawing.Rectangle(0, 0, _content.Width+7, _content.Height+2));

                _strip = new Sprite()
                {
                    GraphicsDevice = this.GraphicsDevice,
                    SpriteBatch = this.SpriteBatch,
                    Width = _content.Width+8,
                    Height = _content.Height+3,
                    Color = Color.White,
                    Texture=gdip.SaveTexture(),
                };
                gdip.Dispose();

                _strip.AddComponent(_content);
                _strip.LayerDepth = 1f;
                _strip.LoadContent();
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
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (MouseInRect && _content != null)
            {
                _strip.Top = Mouse.GetState().Y + 5;
                _strip.Left = Mouse.GetState().X + 5;
                _strip.Draw(gameTime);
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
                MouseInRect = true;
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
