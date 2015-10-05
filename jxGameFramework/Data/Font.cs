using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Tao.FreeType;
using System.IO;

namespace jxGameFramework.Data
{
    public class Character
    {
        public Texture2D Texture { get; set; }
        public int OffsetX { get; set; }
        public int OffserY { get; set; }
        public char Char { get; set; }
    }

    public class Font
    {
        private FT_FaceRec face;
        private IntPtr faceptr;
        private GraphicsDevice gd;
        private Character baseCharacter;
        private int yoffset;
        private float lean=0;

        private int descender;
        private int fontheight;
        private int ascender;

        public float Lean
        {
            get
            {
                return this.lean;
            }
            set
            {
                FT_Matrix matrix;
                FT_Vector vect = new FT_Vector { x = 0, y = 0 };
                matrix.xx = (int)0x10000L;
                matrix.xy = (int)(value * 0x10000L);
                matrix.yx = 0;
                matrix.yy = (int)0x10000L;
                FT.FT_Set_Transform(faceptr, ref matrix, ref vect);
                buffer.Clear();
                baseCharacter = CreateChar('i');
                
                this.lean = value;
            }
        }
        public bool EnableBorder { get; set; }
        public Color BorderColor { get; set; }

        public bool EnableShadow { get; set; }
        public int ShadowXOffset { get; set; }
        public int ShadowYOffset { get; set; }
        public Color ShadowColor { get; set; }
        public uint Size { get; private set; }

        private Dictionary<char, Character> buffer = new Dictionary<char, Character>();
        public Font(string font, uint size)
        {
            if (!File.Exists(font))
                throw new FileNotFoundException("Failed to load font file:" + font);
            Size = size;
            if (Graphics.Instance.GraphicsDevice == null)
                throw new NullReferenceException("Graphics Device is null.");
            gd = Graphics.Instance.GraphicsDevice;
            IntPtr libptr;
            int ret = FT.FT_Init_FreeType(out libptr);
            if (ret != 0)
                throw new Exception("Failed to load FreeType library.");

            int retb = FT.FT_New_Face(libptr, font, 0, out faceptr);
            if (retb != 0)
                throw new Exception("Failed to create font face.");

            face = (FT_FaceRec)Marshal.PtrToStructure(faceptr, typeof(FT_FaceRec));
            FT.FT_Set_Char_Size(faceptr, (int)size << 6, (int)size << 6, 96, 96);
            FT.FT_Set_Pixel_Sizes(faceptr, size, size);
            ascender = face.ascender >> 6;
            descender = face.descender >> 6;
            fontheight = ((face.height >> 6) + descender + ascender) / 4;
            yoffset = (int)(Size - ascender);
            baseCharacter = CreateChar('i');
        }

        private FT_GlyphSlotRec GetCharBitmap(uint c)
        {

            uint index = FT.FT_Get_Char_Index(faceptr, c);

            int ret = FT.FT_Load_Glyph(faceptr, index, FT.FT_LOAD_DEFAULT);

            FT_GlyphSlotRec glyph_rec = (FT_GlyphSlotRec)Marshal.PtrToStructure(face.glyph, typeof(FT_GlyphSlotRec));
            int retb = FT.FT_Render_Glyph(ref glyph_rec, FT_Render_Mode.FT_RENDER_MODE_NORMAL);
            return glyph_rec;
        }
        private Character CreateChar(char c)
        {
            Character ch = new Character();
            try
            {
                if (buffer.ContainsKey(c))
                {
                    return buffer[c];
                }
                if (c == '\r' || c == '\n')
                {
                    Texture2D texture = new Texture2D(gd, baseCharacter.Texture.Width, 1);
                    ch.Texture = texture;
                    ch.Char = c;
                    buffer.Add(c, ch);
                    return ch;
                }

                if (c != ' ' && c != '　')
                {
                    var tt = GetCharBitmap(Convert.ToUInt32(c));
                    var charoffsety = (ascender) - tt.bitmap_top;
                    var charoffsetx = tt.bitmap_left;

                    byte[] bmp = new byte[tt.bitmap.rows * tt.bitmap.width];
                    Marshal.Copy(tt.bitmap.buffer, bmp, 0, bmp.Length);

                    Color[] colordata = new Color[tt.bitmap.rows * tt.bitmap.width];
                    for (int i = 0; i < colordata.Length; i++)
                    {
                        colordata[i] = new Color(255, 255, 255, bmp[i]);
                    }

                    Texture2D texture = new Texture2D(gd, tt.bitmap.width, tt.bitmap.rows);
                    texture.SetData<Color>(colordata);
                    PreMultiplyAlphas(texture);
                    ch.Texture = texture;
                    ch.OffserY = charoffsety;
                    ch.OffsetX = charoffsetx;
                    ch.Char = c;
                    buffer.Add(c, ch);
                }
                else
                {
                    Texture2D texture = new Texture2D(gd, baseCharacter.Texture.Width, 1);
                    ch.Texture = texture;
                    buffer.Add(c, ch);
                }
            }
            catch (Exception)
            {
                Texture2D texture = new Texture2D(gd, baseCharacter.Texture.Width, 1);
                ch.Texture = texture;
                buffer.Add(c, ch);
            }

            return ch;
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

        private Character[] CreateStringTexture(string s)
        {
            var temp = new Character[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                temp[i] = CreateChar(s[i]);
            }
            return temp;
        }
        public Vector2 MeasureString(string s)
        {
            string[] lines = s.Split('\n');
            int maxx = 0;
            int maxy = lines.Length * ((int)Size + 2);
            foreach(string ln in lines)
            {
                var text = CreateStringTexture(ln);
                int presentx = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    presentx += (1 + (int)text[i].Texture.Width + text[i].OffsetX);
                }
                if (presentx > maxx)
                    maxx = presentx;
            }
            return new Vector2(maxx, maxy+1);
        }
        private void DrawText(SpriteBatch sb, Vector2 pos, Character[] text, Color color)
        {
            int presentx = (int)pos.X;
            int presenty = (int)pos.Y + yoffset;
            for (int i = 0; i < text.Length; i++)
            {
                bool draw = true;
                if (presentx > gd.Viewport.Width || presenty > gd.Viewport.Height || presentx < 0 || presenty < yoffset - Size)
                    draw = false;
                if(text[i].Char == '\n')
                {
                    presenty += (int)Size+2;
                    presentx = (int)pos.X;
                    continue;
                }
                if (text[i].Char == '\r')
                    continue;
                if(draw)
                {
                    if (EnableBorder)
                    {
                        if (draw)
                        {
                        }
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX - 1, presenty + text[i].OffserY - 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX - 1, presenty + text[i].OffserY, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX - 1, presenty + text[i].OffserY + 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);

                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX, presenty + text[i].OffserY - 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX, presenty + text[i].OffserY + 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);

                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX + 1, presenty + text[i].OffserY - 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX + 1, presenty + text[i].OffserY, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX + 1, presenty + text[i].OffserY + 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                    }
                    if (EnableShadow)
                    {
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX + ShadowXOffset, presenty + text[i].OffserY + ShadowYOffset, text[i].Texture.Width, text[i].Texture.Height), ShadowColor);
                    }
                    sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX, presenty + text[i].OffserY, text[i].Texture.Width, text[i].Texture.Height), color);
                }       
                presentx += (1 + (int)text[i].Texture.Width + text[i].OffsetX);
            }
        }
        //TODO: overflow
        private void DrawText(SpriteBatch sb, Vector2 pos, Rectangle rect,Character[] text,Color color)
        {
            int presentx = (int)pos.X;
            int presenty = (int)pos.Y + yoffset;
            for (int i = 0; i < text.Length; i++)
            {
                bool draw = true;
                if (presentx > rect.Width +rect.X || presenty > rect.Height + rect.Y - Size  || presentx < rect.X || presenty < yoffset - Size +rect.Y)
                    draw = false;
                if (text[i].Char == '\n')
                {
                    presenty += (int)Size + 2;
                    presentx = (int)rect.X;
                    continue;
                }
                if (text[i].Char == '\r')
                    continue;
                if (draw)
                {
                    if (EnableBorder)
                    {
                        if (draw)
                        {
                        }
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX - 1, presenty + text[i].OffserY - 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX - 1, presenty + text[i].OffserY, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX - 1, presenty + text[i].OffserY + 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);

                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX, presenty + text[i].OffserY - 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX, presenty + text[i].OffserY + 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);

                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX + 1, presenty + text[i].OffserY - 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX + 1, presenty + text[i].OffserY, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX + 1, presenty + text[i].OffserY + 1, text[i].Texture.Width, text[i].Texture.Height), BorderColor);
                    }
                    if (EnableShadow)
                    {
                        sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX + ShadowXOffset, presenty + text[i].OffserY + ShadowYOffset, text[i].Texture.Width, text[i].Texture.Height), ShadowColor);
                    }
                    sb.Draw(text[i].Texture, new Rectangle(presentx + text[i].OffsetX, presenty + text[i].OffserY, text[i].Texture.Width, text[i].Texture.Height), color);
                }
                presentx += (1 + (int)text[i].Texture.Width + text[i].OffsetX);
            }
        }
        public void DrawText(Vector2 pos, string text, Color color)
        {
            var tt = CreateStringTexture(text);
            DrawText(Graphics.Instance.SpriteBatch, pos, tt, color);
        }
        public void DrawText(Vector2 pos, Rectangle rect,string text,Color color)
        {
            var tt = CreateStringTexture(text);
            DrawText(Graphics.Instance.SpriteBatch, pos, rect, tt, color);
        }
    }
}
