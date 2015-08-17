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
        Font bigger;
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
            this.Font = new Font(this.GraphicsDevice, "msyh.ttc", 15)
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
                    int betweenheight = (int)(100 * (betweenqueue[i] / 20));
                    int updateheight = (int)(100 * (updatequeue[i] / 20));

                    SpriteBatch.DrawLine(new Vector2(vwidth - i, vheight), new Vector2(vwidth - i, vheight - updateheight), Color.LightGreen, 1f);
                    SpriteBatch.DrawLine(new Vector2(vwidth - i, vheight - updateheight), new Vector2(vwidth - i, vheight - updateheight - drawheight), Color.MediumPurple, 1f);
                    SpriteBatch.DrawLine(new Vector2(vwidth - i, vheight - updateheight - drawheight), new Vector2(vwidth - i, vheight - updateheight - drawheight - betweenheight), Color.White, 1f);
                    i++;
                }
                if (ctrlPressed)
                {
                    bigger.DrawText(SpriteBatch, new Vector2(vwidth - 749, vheight - 100), "Update", Color.LightGreen);
                    bigger.DrawText(SpriteBatch, new Vector2(vwidth - 669, vheight - 100), "Draw", Color.MediumPurple);
                    bigger.DrawText(SpriteBatch, new Vector2(vwidth - 610, vheight - 100), "BetweenFrames", Color.White);
                }
                Font.DrawText(SpriteBatch, new Vector2(vwidth - 798, vheight - 100), "20ms", Color.White);
                Font.DrawText(SpriteBatch, new Vector2(vwidth - 798, vheight - 20), "0ms", Color.White);
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
