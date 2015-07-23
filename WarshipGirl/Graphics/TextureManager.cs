using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework;

namespace WarshipGirl.Graphics
{
    static class TextureManager
    {
        public enum ShipSize
        {
            Small,Medium,Large
        }
        public static Texture2D LoadShipImage(GraphicsDevice gd,int ShipID,ShipSize size,bool isBroken = false)
        {
            switch (size)
            {
                case ShipSize.Small:
                    return Load64Img(gd, ShipID, isBroken);
                case ShipSize.Medium:
                    return Load128Img(gd, ShipID, isBroken);
                case ShipSize.Large:
                    return Load1024Img(gd, ShipID, isBroken);
                default:
                    return null;
            }
        }
        private static Texture2D Load64Img(GraphicsDevice gd,int shipid,bool isBroken)
        {
            if(isBroken)
            {
                return Bitmap2Texture2D(gd,(System.Drawing.Bitmap)Broken_64.Resource.ResourceManager.GetObject(string.Format("ship64_normal_{0}", shipid)));
            }
            else
            {
                return Bitmap2Texture2D(gd, (System.Drawing.Bitmap)Normal_64.Resource.ResourceManager.GetObject(string.Format("ship64_normal_{0}", shipid)));
            }
        }
        private static Texture2D Load128Img(GraphicsDevice gd, int shipid, bool isBroken)
        {
            if (isBroken)
            {
                return Bitmap2Texture2D(gd, (System.Drawing.Bitmap)Broken_128.Resource.ResourceManager.GetObject(string.Format("ship128_normal_{0}", shipid)));
            }
            else
            {
                return Bitmap2Texture2D(gd, (System.Drawing.Bitmap)Normal_128.Resource.ResourceManager.GetObject(string.Format("ship128_normal_{0}", shipid)));
            }
        }
        private static Texture2D Load1024Img(GraphicsDevice gd, int shipid, bool isBroken)
        {
            if (isBroken)
            {
                return Bitmap2Texture2D(gd, (System.Drawing.Bitmap)Broken_1024.Resource.ResourceManager.GetObject(string.Format("ship1024_normal_{0}", shipid)));
            }
            else
            {
                return Bitmap2Texture2D(gd, (System.Drawing.Bitmap)Normal_1024.Resource.ResourceManager.GetObject(string.Format("ship1024_normal_{0}", shipid)));
            }
        }

        private static Texture2D Bitmap2Texture2D(GraphicsDevice gd,System.Drawing.Bitmap bitmap)
        {
            var s = new MemoryStream();
            bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Png);
            var t = Texture2D.FromStream(gd,s);
            PreMultiplyAlphas(t);
            return t;
        }
        private static void PreMultiplyAlphas(Texture2D ret)
        {
            Byte4[] data = new Byte4[ret.Width * ret.Height];
            ret.GetData<Byte4>(data);
            for (int i = 0; i < data.Length; i++)
            {
                Vector4 vec = data[i].ToVector4();
                float alpha = vec.W / 255.0f;
                int a = (int)(vec.W);
                int r = (int)(alpha * vec.X);
                int g = (int)(alpha * vec.Y);
                int b = (int)(alpha * vec.Z);
                uint packed = (uint)(
                    (a << 24) +
                    (b << 16) +
                    (g << 8) +
                    r
                    );

                data[i].PackedValue = packed;
            }
            ret.SetData<Byte4>(data);
        }
           
    }
}
