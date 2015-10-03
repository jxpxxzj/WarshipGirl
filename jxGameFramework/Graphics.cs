using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jxGameFramework
{
    public class Graphics
    {
        //private static SpriteBatch _spritebatch;
        //private static GraphicsDevice _graphics;
        //public static SpriteBatch SpriteBatch
        //{
        //    get
        //    {
        //        return _spritebatch;
        //    }
        //    set
        //    {
        //        _spritebatch = value;
        //    }
        //}
        //public static GraphicsDevice GraphicsDevice
        //{
        //    get
        //    {
        //        return _graphics;
        //    }
        //    set
        //    {
        //        _graphics = value;
        //    }
        //}
        public SpriteBatch SpriteBatch{get;set;}
        public GraphicsDevice GraphicsDevice{get;set;}
        private static Graphics mInstanse;
        public static Graphics Instance
        {
            get
            {
                if (mInstanse == null)
                    mInstanse = new Graphics();
                return mInstanse;
            }
        }
    }
}
