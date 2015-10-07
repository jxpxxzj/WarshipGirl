using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jxGameFramework
{
    internal class Graphics
    {
        public SpriteBatch SpriteBatch { get; internal set; }
        public GraphicsDevice GraphicsDevice { get; internal set; }
        public Game Game { get; internal set; }
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
