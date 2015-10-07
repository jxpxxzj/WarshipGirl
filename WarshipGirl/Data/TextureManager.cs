using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework;
using jxGameFramework.Components;

namespace WarshipGirl.Data
{
    static class TextureManager
    {
        public enum ShipSize
        {
            Small,Medium,Large
        }
        public static GraphicsDevice GraphicsDevice;
        public static Texture2D LoadShipImage(int ShipID,ShipSize size,bool isBroken = false)
        {
            switch (size)
            {
                case ShipSize.Small:
                    return Load64Img(ShipID, isBroken);
                case ShipSize.Medium:
                    return Load128Img(ShipID, isBroken);
                case ShipSize.Large:
                    return Load1024Img(ShipID, isBroken);
                default:
                    return null;
            }
        }
        private static Texture2D Load64Img(int shipid,bool isBroken)
        {
            if(isBroken)
            {
                return Bitmap2Texture2D((System.Drawing.Bitmap)Broken_64.Resource.ResourceManager.GetObject(string.Format("ship64_normal_{0}", shipid)));
            }
            else
            {
                return Bitmap2Texture2D((System.Drawing.Bitmap)Normal_64.Resource.ResourceManager.GetObject(string.Format("ship64_normal_{0}", shipid)));
            }
        }
        private static Texture2D Load128Img(int shipid, bool isBroken)
        {
            if (isBroken)
            {
                return Bitmap2Texture2D((System.Drawing.Bitmap)Broken_128.Resource.ResourceManager.GetObject(string.Format("ship128_normal_{0}", shipid)));
            }
            else
            {
                return Bitmap2Texture2D((System.Drawing.Bitmap)Normal_128.Resource.ResourceManager.GetObject(string.Format("ship128_normal_{0}", shipid)));
            }
        }
        private static Texture2D Load1024Img(int shipid, bool isBroken)
        {
            if (isBroken)
            {
                return Bitmap2Texture2D((System.Drawing.Bitmap)Broken_1024.Resource.ResourceManager.GetObject(string.Format("ship1024_normal_{0}", shipid)));
            }
            else
            {
                return Bitmap2Texture2D((System.Drawing.Bitmap)Normal_1024.Resource.ResourceManager.GetObject(string.Format("ship1024_normal_{0}", shipid)));
            }
        }

        private static Texture2D Bitmap2Texture2D(System.Drawing.Bitmap bitmap)
        {
            var s = new MemoryStream();
            bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Png);
            var t = Sprite.CreateTextureFromStream(s);
            return t;
        }
    }
}
