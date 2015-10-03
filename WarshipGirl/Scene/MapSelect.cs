using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jxGameFramework.Scene;
using jxGameFramework.Data;
using jxGameFramework.Components;
using jxGameFramework.Controls;
using jxGameFramework.Audio;
using jxGameFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WarshipGirl.Controls;

namespace WarshipGirl.Scene
{
    partial class MapSelect : BaseScene
    {
        Font fnt;
        Font fntsmall;
        Font fntborder;

        List<MapPanel> maps = new List<MapPanel>();
        ScrollPanel spm;
        Texture2D bg;
        Texture2D menu_osu;
        Texture2D top;

        MapPanel selected;
        Button b;
        Button breturn;
        ComboBox combob;

        ComboBox groupcb;
        ComboBox sortcb;

        TabControl tb;

        AudioStream bgstream = new AudioStream(@"Content\battle-event.mp3", true);
        AudioPlayer player;

        public override void Initialize()
        {
            fnt = new Font("msyh.ttc", 25);
            fntsmall = new Font("msyh.ttc", 15);
            fntborder = new Font("msyh.ttc", 15)
            {
                EnableBorder = true,
                BorderColor = Color.Black
            };

            spm = new ScrollPanel()
            {
                Margin = Origins.TopRight,
                Width = 1000,
                Height = Graphics.Instance.GraphicsDevice.Viewport.Height - 80,
                Top = 80,
                Right = 0
            };

            b = new Button()
            {
                Bottom = 10,
                Left = 195,
                Width = 165,
                Height = 40,
                Margin = Origins.BottomLeft,
            };

            breturn = new Button()
            {
                Bottom = 10,
                Left = 10,
                Width = 165,
                Height = 40,
                Margin = Origins.BottomLeft
            };


            combob = new ComboBox()
            {
                Top = 120,
                Left = 10,
                Width = 350,
                Margin = Origins.TopLeft,
            };

            groupcb = new ComboBox()
            {
                Top = 10,
                Right = 300,
                Width = 200,
                Margin = Origins.TopRight,
                FocusColor = Color.CornflowerBlue,
            };

            sortcb = new ComboBox()
            {
                Top = 10,
                Right = 20,
                Width = 200,
                Margin = Origins.TopRight,
                FocusColor = Color.LightGreen,
            };

            tb = new TabControl()
            {
                Top = 54,
                Right = 10,
                Margin = Origins.TopRight,
                Width = Graphics.Instance.GraphicsDevice.Viewport.Width,
                Height = Graphics.Instance.GraphicsDevice.Viewport.Height,
                TabMargin = Origins.TopRight
            };

            bg = Sprite.CreateTextureFromFile(@"Content\menu-background-xmas-6.png");
            menu_osu = Sprite.CreateTextureFromFile(@"Content\010c0951.png");
            top = Sprite.CreateTextureFromFile(@"Content\songselect-top.png");

            ChildSprites.Add(spm);
            ChildSprites.Add(b);
            ChildSprites.Add(breturn);
            ChildSprites.Add(combob);
            ChildSprites.Add(groupcb);
            ChildSprites.Add(sortcb);
            ChildSprites.Add(tb);


            base.Initialize();

            CreateMap();
            p_Click((object)maps[0], null);

            spm.ChildSprites.AddRange(maps.ToArray());

            spm.ScrollSpeed = 150;
            b.Text = "开始颓废";
            breturn.Text = "返回母港";
            breturn.Click += Breturn_Click;

            combob.AddItem("地图信息");
            combob.AddItem("本地排行");
            combob.AddItem("在线排行");
            combob.AddItem("好友排行");
            combob.SelectedItem = "地图信息";
            groupcb.AddItem("不分组");
            groupcb.AddItem("作者");
            groupcb.AddItem("添加日期");
            groupcb.AddItem("长度");
            groupcb.AddItem("海域");
            groupcb.AddItem("地图名称");
            groupcb.AddItem("收藏夹");
            groupcb.AddItem("我做的图");
            groupcb.AddItem("最近玩过的");
            groupcb.SelectedItem = "不分组";

            sortcb.AddItem("海域编号");
            sortcb.AddItem("地图名称");
            sortcb.AddItem("作者");
            sortcb.AddItem("长度");
            sortcb.AddItem("添加日期");
            sortcb.SelectedItem = "海域编号";

            tb.AddTab(Control.Empty(), "不分组");
            tb.AddTab(Control.Empty(), "作者");
            tb.AddTab(Control.Empty(), "最近玩过的");
            tb.AddTab(Control.Empty(), "收藏夹");
            tb.SwitchTab(0);
            this.Load += MapSelect_Load;
            this.Unload += MapSelect_Unload;

            MapSelect_Load(this, EventArgs.Empty);
        }

        private void Breturn_Click(object sender, MouseEventArgs e)
        {
            Game1.Instance.Scenes.Navigate("Harbor");
        }

        void MapSelect_Unload(object sender, EventArgs e)
        {
            player.Stop();
        }

        void MapSelect_Load(object sender, EventArgs e)
        {
            player = new AudioPlayer(bgstream);
            player.Play(true);
        }
        public override void Draw(GameTime gameTime)
        {
            Graphics.Instance.SpriteBatch.Draw(bg, new Rectangle(0, 0, bg.Width, bg.Height), Color.White);
            Graphics.Instance.SpriteBatch.FillRectangle(new Rectangle(0, 0, 370, 650), new Color(128, 128, 128, 200));
            spm.Draw(gameTime);
            Graphics.Instance.SpriteBatch.Draw(top, new Vector2(0, 0), Color.White);
            fnt.DrawText(new Vector2(10, 10), string.Format("{0} - {1}", selected.SeaText, selected.MapText), Color.White);
            fntsmall.DrawText(new Vector2(10, 40), string.Format("作者: {0}", selected.CreatorText), Color.White);
            //fntsmall.DrawText(new Vector2(10, 60), string.Format("点数: {0} 路径数: {1} 路线数: {2}", m.Points, m.Routes, m.Ways), Color.White);
            //fntsmall.DrawText(new Vector2(10, 80), string.Format("迂回点数: {0} 停泊点数: {1} 资源点数: {2}", m.CanSkipPoint, m.AnchorPoint, m.ResourcesPoint), Color.White);
            Graphics.Instance.SpriteBatch.Draw(selected.MapSprite.Texture, new Rectangle(12, 180, (int)(selected.MapSprite.Texture.Width * 0.65), (int)(selected.MapSprite.Texture.Height * 0.65)), Color.White);
            fntborder.DrawText(new Vector2(13, 385), "地图描述:", Color.White);
            fntborder.DrawText(new Vector2(13, 405), "这是一款全新突破性质的牙膏", Color.White);
            b.Draw(gameTime);
            breturn.Draw(gameTime);
            tb.Draw(gameTime);
            combob.Draw(gameTime);
            fnt.DrawText(new Vector2(Graphics.Instance.GraphicsDevice.Viewport.Width - 280, 10), "排序", Color.LightGreen);
            fnt.DrawText(new Vector2(Graphics.Instance.GraphicsDevice.Viewport.Width - 560, 10), "分组", Color.CornflowerBlue);
            groupcb.Draw(gameTime);
            sortcb.Draw(gameTime);
        }

    }
    partial class MapSelect : BaseScene
    {
        void CreateMap()
        {
            string mapstr = @"1-1 母港附近海域
1-2 东北防线海域
1-3 仁州附近海域
1-4 深海仁州基地
2-1 扶桑西部海域
2-2 扶桑西南海域
2-3 扶桑南部海域
2-4 深海扶桑基地
3-1 母港南部海域
3-2 东南群岛(I)
3-3 东南群岛(II)
3-4 星洲海峡
4-1 克拉代夫东部海域
4-2 克拉代夫西部海域
4-3 泪之扉附近海域
4-4 泪之扉防线
5-1 塞浦路斯附近海域
5-2 克里特附近海域
5-3 马耳他附近海域
5-4 直布罗陀东部海域
5-5 直布罗陀要塞
6-1 洛里昂南部海域
6-2 英吉利海峡";
            string[] levelstr = mapstr.Split('\n');
            for (int i = 0; i < levelstr.Length; i++)
            {
                levelstr[i] = levelstr[i].Substring(0, levelstr[i].Length - 1);
                maps.Add(CreateMapPanel(levelstr[i], i));
            }

        }
        MapPanel CreateMapPanel(string lstr, int id)
        {
            string[] t = lstr.Split(' ');
            string st = t[0];
            string lt = t[1];

            var p = new MapPanel()
            {
                Top = id * 85 + 10,
                Right = -150,
                Margin = Origins.TopRight,
            };
            p.Initialize();
            p.SeaText = st;
            p.CreatorText = "61616";
            p.MapText = lt;
            p.Selected = false;
            p.Click += p_Click;
            p.PreviewPath = string.Format(@"Maps/Map_{0}.png", st);
            return p;
        }

        void p_Click(object sender, MouseEventArgs e)
        {
            var s = (MapPanel)sender;
            foreach (MapPanel p in maps)
            {
                p.Selected = false;
                p.Right = -150;
            }

            s.Selected = true;
            s.Right = -15;
            selected = s;
        }
    }
}
