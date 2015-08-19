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
    public class FpsCounter : Text
    {
        int frameRate = 0;
        int frameCounter = 0;
        bool ctrlPressed = false;
        public TimeSpan FrameTime;
        public TimeSpan BetweenTime;
        public TimeSpan UpdateTime;
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
        int vwidth;
        int vheight;

        public bool EnableFrameTime { get; set; }
        public override void LoadContent()
        {
            bigger = new Font(this.GraphicsDevice, "msyh.ttc", 20)
            {
                EnableShadow = true,
                ShadowColor = Color.Black,
                ShadowXOffset = 0,
                ShadowYOffset = 1
            };
            this.Font = new Font(this.GraphicsDevice, "msyh.ttc", 12)
            {
                EnableBorder=true,
                BorderColor=Color.Black,
            };
                
            smaller = new Font(this.GraphicsDevice, "msyh.ttc", 15)
            { 
                EnableShadow = true,
                ShadowColor=Color.Black,
                ShadowXOffset=0,
                ShadowYOffset=1
            };
            vwidth = GraphicsDevice.Viewport.Width;
            vheight = GraphicsDevice.Viewport.Height;
            rect = new Rectangle(vwidth - 800, vheight - 100, 800, 100);
            base.LoadContent();
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
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
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if(EnableFrameTime)
            {
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
                    bigger.DrawText(SpriteBatch, new Vector2(vwidth - 749, vheight - 100), "Update", UpdateGreen);
                    bigger.DrawText(SpriteBatch, new Vector2(vwidth - 669, vheight - 100), "Draw", DrawPurple);
                    bigger.DrawText(SpriteBatch, new Vector2(vwidth - 610, vheight - 100), "BetweenFrames", Color.White);
                }
                smaller.DrawText(SpriteBatch, new Vector2(vwidth - 797, vheight - 100), "20ms", Color.White);
                smaller.DrawText(SpriteBatch, new Vector2(vwidth - 797, vheight - 20), "0ms", Color.White);
            }     
            frameCounter++;
            string fps = "";
            if (FrameTime != null)
                fps = string.Format("{0}fps", frameRate);
            this.text = fps;
            base.Draw(gameTime);
        }
    }
}
