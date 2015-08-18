using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing.Imaging;
using System.Drawing;

namespace FontTest
{
    class GifFrame
    {
        public Texture2D Image { get; set; }
        public TimeSpan FrameTime { get; set; }
        public GifFrame(Texture2D _img,TimeSpan _span)
        {
            this.Image = _img;
            this.FrameTime = _span;
        }
        public override string ToString()
        {
            return string.Format("FrameTime = {0}ms", FrameTime.TotalMilliseconds.ToString());
        }

    }
    public class GifPlayer : Control
    {
        List<GifFrame> FrameImg = new List<GifFrame>();

        string _imgpath;
        public string ImagePath
        {
            get
            {
                return _imgpath;
            }
            set
            {
                _imgpath = value;
                LoadImg(_imgpath);
            }
        }
        //TimeSpan FrameTime { get; set; }
        
        private void LoadImg(string path)
        {
            var img = Image.FromFile(path);
            var fd = new FrameDimension(img.FrameDimensionsList[0]);
            var count = img.GetFrameCount(fd);
            var ms = new MemoryStream();
            Texture2D texture;
            TimeSpan frametime = new TimeSpan(0,0,0);
            FrameImg.Clear();
            int i;
            for(i=0;i<count;i++)
            {
                for (int j = 0; j < img.PropertyIdList.Length; j++)
                {
                    if ((int)img.PropertyIdList.GetValue(j) == 0x5100)
                    {
                        PropertyItem pItem = (PropertyItem)img.PropertyItems.GetValue(j);
                        byte[] delayByte = new byte[4];
                        delayByte[0] = pItem.Value[i * 4];
                        delayByte[1] = pItem.Value[1 + i * 4];
                        delayByte[2] = pItem.Value[2 + i * 4];
                        delayByte[3] = pItem.Value[3 + i * 4];
                        int delay = BitConverter.ToInt32(delayByte, 0) * 10;
                        frametime = new TimeSpan(0, 0, 0, 0, delay);
                        break;
                    }
                }
                img.SelectActiveFrame(fd, i);
                img.Save(ms, ImageFormat.Png);
                texture = Texture2D.FromStream(GraphicsDevice, ms);
                FrameImg.Add(new GifFrame(texture,frametime));
                ms = new MemoryStream();
            }
            

        }
        private void SetTexture(int frameid)
        {
            this.Texture = FrameImg[frameid].Image;
            this.Height = FrameImg[frameid].Image.Height;
            this.Width = FrameImg[frameid].Image.Width;
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            int frame = (int)((int)(gameTime.TotalGameTime.TotalMilliseconds / FrameImg[FrameImg.Count-1].FrameTime.TotalMilliseconds) % FrameImg.Count);
            SetTexture(frame);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        
    }
}
