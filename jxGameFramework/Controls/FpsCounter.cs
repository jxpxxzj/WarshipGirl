using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Data;
using jxGameFramework.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace jxGameFramework.Controls
{
    public class FpsCounter : Control
    {
        int frameRate = 0;
        int frameCounter = 0;
        bool ctrlPressed = false;
        public TimeSpan FrameTime { get; internal set; }
        public TimeSpan BetweenTime { get; internal set; }
        public TimeSpan UpdateTime { get; internal set; }
        TimeSpan elapsedTime = TimeSpan.Zero;

        List<double> timequeue = new List<double>();
        List<double> betweenqueue = new List<double>();
        List<double> updatequeue = new List<double>();
        Rectangle rect;
        Color TransGray = new Color(100, 100, 100, 200);
        Color UpdateGreen = new Color(154, 205, 50, 255);
        Color DrawPurple = new Color(138, 43, 226, 255);
        Font bigger;
        Font smaller;
        Font normal;
        Label fpsvalue;

        public bool EnableFrameTime { get; set; }
        public override void Initialize()
        {
            bigger = new Font(DefaultFontFileName, 20)
            {
                EnableShadow = true,
                ShadowColor = Color.Black,
                ShadowXOffset = 0,
                ShadowYOffset = 1
            };
            normal = new Font(DefaultFontFileName, 12)
            {
                EnableBorder=true,
                BorderColor=Color.Black,
            };
                
            smaller = new Font(DefaultFontFileName, 15)
            { 
                EnableShadow = true,
                ShadowColor=Color.Black,
                ShadowXOffset=0,
                ShadowYOffset=1
            };
            fpsvalue = new Label()
            {
                Color = Color.White,
                Margin = Origins.BottomRight,
                Bottom = 2,
                Right = 2,
                Font = normal
            };
            Controls.Add(fpsvalue);
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            ctrlPressed = Keyboard.GetState().IsKeyDown(Keys.LeftControl);
            if (elapsedTime > TimeSpan.FromMilliseconds(1000))
            {
                elapsedTime -= TimeSpan.FromMilliseconds(1000);
                frameRate = frameCounter;
                frameCounter = 0;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if(EnableFrameTime && Visible)
            {
                rect = new Rectangle(DrawingX, DrawingY, 800, 100);
                int vwidth = this.DrawingX + this.Width;
                int vheight = this.DrawingY + this.Height;
                SpriteBatch.FillRectangle(rect, TransGray);
                int i = 0;
                if (!ctrlPressed)
                {
                    timequeue.Insert(0, FrameTime.TotalMilliseconds);
                    betweenqueue.Insert(0, BetweenTime.TotalMilliseconds);
                    updatequeue.Insert(0, UpdateTime.TotalMilliseconds);
                    if (timequeue.Count > 800)
                        timequeue.RemoveAt(800);
                    if (betweenqueue.Count > 800)
                        betweenqueue.RemoveAt(800);
                    if (updatequeue.Count > 800)
                        updatequeue.RemoveAt(800);
                }
                foreach (double t in timequeue)
                {
                    int drawheight = (int)(100 * (timequeue[i] / 20));
                    int updateheight = (int)(100 * (updatequeue[i] / 20));
                    int betweenheight = (int)(100 * (betweenqueue[i] / 20));
                    int presenty = vheight;

                    int targetlength = updateheight;
                    if (targetlength > 100)
                        targetlength = 100;
                    SpriteBatch.DrawLine(new Vector2(vwidth - i, presenty), new Vector2(vwidth - i, presenty - targetlength), UpdateGreen, 1f);

                    presenty -= targetlength;
                    targetlength = drawheight;
                    if (targetlength > 100 - updateheight)
                        targetlength = 100 - updateheight;

                    SpriteBatch.DrawLine(new Vector2(vwidth - i, presenty), new Vector2(vwidth - i, presenty - targetlength), DrawPurple, 1f);

                    presenty -= targetlength;
                    targetlength = betweenheight;
                    if (targetlength > 100 - updateheight - drawheight)
                        targetlength = 100 - updateheight - drawheight;

                    SpriteBatch.DrawLine(new Vector2(vwidth - i, presenty), new Vector2(vwidth - i, presenty - targetlength), Color.White, 1f);
                    i++;
                }
                if (ctrlPressed)
                {
                    bigger.DrawText(new Vector2(vwidth - 749, vheight - 100), "Update", UpdateGreen);
                    bigger.DrawText(new Vector2(vwidth - 669, vheight - 100), "Draw", DrawPurple);
                    bigger.DrawText(new Vector2(vwidth - 610, vheight - 100), "BetweenFrames", Color.White);
                }
                smaller.DrawText(new Vector2(vwidth - 797, vheight - 100), "20ms", Color.White);
                smaller.DrawText(new Vector2(vwidth - 797, vheight - 20), "0ms", Color.White);
            }     
            frameCounter++;
            string fps = "";
            if (FrameTime != null)
                fps = string.Format("{0}fps", frameRate);
            fpsvalue.Text = fps;
            base.Draw(gameTime);
        }
    }
}
