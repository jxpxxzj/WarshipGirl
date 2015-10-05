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
using WarshipGirl.Data;
using System.IO;

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

            tb.AddTab(Control.Empty, "不分组");
            tb.AddTab(Control.Empty, "作者");
            tb.AddTab(Control.Empty, "最近玩过的");
            tb.AddTab(Control.Empty, "收藏夹");
            tb.SwitchTab(0);
            this.Show += MapSelect_Show;
            this.Leave += MapSelect_Leave;
        }

        private void MapSelect_Leave(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void MapSelect_Show(object sender, EventArgs e)
        {
            player = new AudioPlayer(bgstream);
            player.Play(true);
        }

        private void Breturn_Click(object sender, MouseEventArgs e)
        {
            Game1.Instance.Scenes.Navigate("Harbor");
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Instance.SpriteBatch.Draw(bg, new Rectangle(0, 0, bg.Width, bg.Height), Color.White);
            Graphics.Instance.SpriteBatch.FillRectangle(new Rectangle(0, 0, 370, 650), new Color(128, 128, 128, 200));
            spm.Draw(gameTime);
            Graphics.Instance.SpriteBatch.Draw(top, new Vector2(0, 0), Color.White);
            fnt.DrawText(new Vector2(10, 10), string.Format("{0} - {1}",selected.Map.MapName,selected.Map.Title), Color.White);
            fntsmall.DrawText(new Vector2(10, 40), string.Format("作者: {0}", selected.Map.Creator), Color.White);
            fntsmall.DrawText(new Vector2(10, 60), string.Format("点数: {0} 路径数: {1} 路线数: {2}", selected.Map.Points, selected.Map.Routes, selected.Map.Ways), Color.White);
            fntsmall.DrawText(new Vector2(10, 80), string.Format("迂回点数: {0} 停泊点数: {1} 资源点数: {2}", selected.Map.CanSkipPoint, selected.Map.AnchorPoint, selected.Map.ResourcesPoint), Color.White);
            Graphics.Instance.SpriteBatch.Draw(selected.MapSprite.Texture, new Rectangle(12, 180, (int)(selected.MapSprite.Texture.Width * 0.65), (int)(selected.MapSprite.Texture.Height * 0.65)), Color.White);
            fntborder.DrawText(new Vector2(13, 385), "地图描述:", Color.White);
            fntborder.DrawText(new Vector2(13, 405), selected.Map.Description, Color.White);
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
            FileInfo[] files = new DirectoryInfo("Maps").GetFiles();
            var res = from name in files
                      where name.Extension == ".xml"
                      select name;
            var res2 = from name in files
                       where name.Extension == ".png"
                       select name;
            var flist = res.ToList();
            var ilist = res2.ToList();
            for (int i = 0; i < flist.Count; i++)
            {
               maps.Add(CreateMapPanel(new Map(flist[i].FullName,ilist[i].FullName), i));
            }

        }
        MapPanel CreateMapPanel(Map m,int id)
        {
            var p = new MapPanel(m)
            {
                Top = id * 85 + 10,
                Right = -150,
                Margin = Origins.TopRight,
            };
            p.Initialize();
            p.Selected = false;
            p.Click += p_Click;
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
