using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace jxGameFramework.Controls
{
    public class GDIpInterop : IDisposable
    {
        private Bitmap Canvas;
        private GraphicsDevice _gd;
        public System.Drawing.Graphics g { get; protected set; }
        public GDIpInterop(int width,int height)
        {
            Canvas = new Bitmap(width, height);
            g = System.Drawing.Graphics.FromImage(Canvas);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            _gd = Graphics.Instance.GraphicsDevice;
        }
        public Texture2D SaveTexture()
        {
            var s = new MemoryStream();
            Canvas.Save(s, ImageFormat.Png);
            var text = Texture2D.FromStream(_gd, s);
            return text;
        }
        public void Dispose()
        {
            Canvas.Dispose();
            g.Dispose();
        }
    }
}
