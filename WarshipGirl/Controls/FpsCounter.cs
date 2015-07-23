using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using jxGameFramework.Components;
using jxGameFramework.Data;

namespace WarshipGirl.Controls
{
    public class FpsCounter : Text
    {
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;   
        public override void LoadContent()
        {
            this.Font = new Font(this.GraphicsDevice, "msyh.ttc", 15)
            {
                EnableBorder = true,
                BorderColor = Color.Black,
                EnableShadow = false
            };
            base.LoadContent();
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

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
            frameCounter++;
            string fps = string.Format("{0}fps", frameRate);
            this.text = fps;
            base.Draw(gameTime);
        }
    }
}
