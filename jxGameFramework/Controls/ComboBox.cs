using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using jxGameFramework.Controls;
using jxGameFramework.Components;
using jxGameFramework.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jxGameFramework.Controls
{
    public enum ItemType
    {
        Item,Separator
    }
    class ComboItem : Control
    {

        Font _fnt;
        Text _title;
        Texture2D _tex;
        Texture2D _septex;
        public string Title
        {
            get
            {
                return _title.text;
            }
            set
            {
                _title.text = value;
            }
        }
        public int ItemID { get; set; }
        public bool LockColor { get; set; }

        public Color FocusColor { get; set; }
        public Color SepColor { get; set; }
        
        public ItemType Type { get; set; }

        int _width;
        int _height;

        public ComboItem(Font fnt,int width,int height,ItemType type)
        {
            _fnt = fnt;
            _width = width;
            _height = height;
            Type = type;
        }
        public override void LoadContent()
        {
            var gdip = new GDIpInterop(_width, _height, GraphicsDevice);
            gdip.g.FillRectangle(System.Drawing.Brushes.White, new System.Drawing.Rectangle(0, 0, _width, _height));
            this.Texture = gdip.SaveTexture();
            gdip.g.Clear(System.Drawing.Color.Transparent);

            var pointa = new System.Drawing.Point[3];
            pointa[0]=new System.Drawing.Point(_width - 25,(int)(_height / 2) -5);
            pointa[1] = new System.Drawing.Point(_width - 10, (int)(_height / 2) - 5);
            pointa[2] = new System.Drawing.Point(_width - 17, (int)(_height / 2) + 5);
            gdip.g.FillPolygon(System.Drawing.Brushes.White, pointa);
            _tex = gdip.SaveTexture();

            this.Width = Texture.Width;
            this.Height = Texture.Height;
            this.Color=Color.Black;
            _title = new Text()
            {
                Font = _fnt,
                Color = Color.White,
                OriginType = Origins.CenterLeft,
                X = 5,
                Y = this.Height /2 - 2,
            };
            AddComponent(_title);
            if (Type == ItemType.Separator)
            {
                this.Color = SepColor;
                _title.X = 20;
                gdip.g.Clear(System.Drawing.Color.Transparent);
                pointa = new System.Drawing.Point[3];
                pointa[0] = new System.Drawing.Point(6, (int)(_height / 2) - 8);
                pointa[1] = new System.Drawing.Point(6, (int)(_height / 2) +8);
                pointa[2] = new System.Drawing.Point(16, (int)(_height / 2));
                gdip.g.FillPolygon(System.Drawing.Brushes.Black, pointa);
                _septex = gdip.SaveTexture();
            }
            gdip.g.Dispose();
            base.LoadContent();
        }
        public void DrawTriangle(GameTime gameTime)
        {
            SpriteBatch.Draw(_tex, new Vector2(this.RenderX, this.RenderY), Color.White); 
        }
        private void DrawSeparator(GameTime gameTime)
        {
            SpriteBatch.Draw(_septex, new Vector2(this.RenderX, this.RenderY), Color.White); 
        }
        protected override void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(!LockColor && Type != ItemType.Separator)
                this.Color = FocusColor;
            base.OnMouseMove(sender, e);
        }
        protected override void OnMouseLeave(object sender, EventArgs e)
        {
            if (!LockColor && Type != ItemType.Separator)
                this.Color = Color.Black;
            base.OnMouseLeave(sender, e);
        }
        public override void Draw(GameTime gameTime)
        {
            if (LockColor)
                this.Color = FocusColor;
            base.Draw(gameTime);
            if (Type == ItemType.Separator)
                DrawSeparator(gameTime);
        }
    }
    public class ComboBox : Control
    {
        List<ComboItem> _comboitem = new List<ComboItem>();
        List<ComboItem> _headinstance = new List<ComboItem>();

        List<object> _objlist = new List<object>();

        ComboItem _presentcbi;
        ComboItem _headcbi;
        object _presentobj;
        Font _fnt;
        public bool Expanded = false;

        Color _focus = Color.DeepPink;
        Color _sep = new Color(34, 153, 187);
        public Color FocusColor
        {
            get
            {
                return _focus;
            }
            set
            {
                _focus = value;
            }
        }
        public Color SeparatorColor
        {
            get
            {
                return _sep;
            }
            set
            {
                _sep = value;
            }
        }

        public object SelectedItem
        {
            get
            {
                return _presentobj;
            }
            set
            {
                int i = 0;
                foreach(object o in _objlist)
                {
                    if (o == value)
                    {
                        SelectItem(i);
                        return;
                    }    
                    i++;
                }
            }
        }

        int _iheight = 30;
        public int ItemHeight
        {
            get
            {
                return _iheight;
            }
            set
            {
                _iheight = value;
            }
        }

        public override void LoadContent()
        {
            _fnt = new Font(GraphicsDevice, "msyh.ttc", 15)
            {
                EnableShadow=true,
                ShadowColor=Color.Black,
                ShadowYOffset =1,
            };
            base.LoadContent();
        }
        public void AddItem(object obj,bool isSeparator = false)
        {
            ItemType type;
            if (isSeparator)
                type = ItemType.Separator;
            else
                type = ItemType.Item;
            var cbi = new ComboItem(_fnt,this.Width,ItemHeight,type)
            {
                Parent=this,
                Margin=Origins.TopLeft,
                GraphicsDevice=this.GraphicsDevice,
                SpriteBatch=this.SpriteBatch,
                FocusColor = FocusColor,
                SepColor = SeparatorColor,
            };
            cbi.LoadContent();
            cbi.ItemID = _objlist.Count;
            cbi.Title = obj.ToString();
            if (type == ItemType.Item)
                cbi.Click += cbi_Click;
            _presentcbi = cbi;
            _presentobj = obj;

            var cbi2 = new ComboItem(_fnt, this.Width,ItemHeight,type)
            {
                Parent = this,
                Margin = Origins.TopLeft,
                GraphicsDevice = this.GraphicsDevice,
                SpriteBatch = this.SpriteBatch,
                FocusColor = FocusColor,
                SepColor=SeparatorColor,
            };
            cbi2.LoadContent();
            cbi2.ItemID = _objlist.Count;
            cbi2.Title = obj.ToString();
            if(type==ItemType.Item)
                cbi2.Click += cbi_Click;

            _headcbi = cbi2;

            _comboitem.Add(cbi);
            _headinstance.Add(cbi2);
            _objlist.Add(obj);
        }
        public void SelectItem(int id)
        {
            _headcbi = _headinstance[id];
            _presentobj = _objlist[id];
        }
        void cbi_Click(object sender, EventArgs e)
        {
            var itm = (ComboItem)sender;
            Expanded = !Expanded;
            if (itm == _headcbi)
            { 
                _headcbi.LockColor = Expanded;
            }   
            else
            {
                _headcbi.LockColor = false;
                _headcbi = _headinstance[itm.ItemID];
                _presentobj = _objlist[itm.ItemID];
            }
        }
        public override void Draw(GameTime gameTime)
        {
            _headcbi.Draw(gameTime);
            _headcbi.DrawTriangle(gameTime);

            if(Expanded)
            {
                foreach(ComboItem i in _comboitem)
                {
                    i.Draw(gameTime);
                }
            }
            
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            _headcbi.Update(gameTime);
            if (Expanded)
                foreach (ComboItem i in _comboitem)
                {
                    i.Top = (i.ItemID + 1) * (ItemHeight -1);
                    i.Update(gameTime);   
                } 
        }
    }
}
