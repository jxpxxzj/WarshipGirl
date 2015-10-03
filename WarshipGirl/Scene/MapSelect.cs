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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WarshipGirl.Controls;
using WarshipGirl.Data;

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
        Map m = new Map("map.xml");

        public override void LoadContent()
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
                Height = jxGameFramework.Graphics.Instance.GraphicsDevice.Viewport.Height - 80,
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
            breturn.Click += breturn_Click;

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
                TabMargin = Origins.TopRight
            };

            #region "noused"
            //fnt0 = new Font(GraphicsDevice, "simsun.ttc", 20);
            //fnt1 = new Font(GraphicsDevice, "exo20.otf", 20);
            //fnt2 = new Font(GraphicsDevice, "aller.ttf", 20);
            //fnt3 = new Font(GraphicsDevice, "consola.ttf", 20);
            //fnt4 = new Font(GraphicsDevice, "cour.ttf", 20);
            //cb = new CheckBox()
            //{
            //    GraphicsDevice = this.GraphicsDevice,
            //    SpriteBatch = this.spriteBatch,
            //    Top = 100,
            //    Left = 100,
            //    Margin = Origins.TopLeft,
            //};
            //cb.LoadContent();
            //cb.Text = "61616 checkbox";
            //cb.CircleColor = Color.Green;

            //p = new MapPanel()
            //{
            //    Top = 30,
            //    Left = 10,
            //    GraphicsDevice = this.GraphicsDevice,
            //    SpriteBatch = this.spriteBatch,
            //    Margin = Origins.TopLeft,
            //    //Selected=false,
            //};
            //p.LoadContent();
            //p.Selected = true;
            //p.SeaText = "扶桑海域攻略";
            //p.CreatorText = "61616";
            //p.MapText = "深海扶桑基地";
            //p.PreviewPath = @"Content\Map_2-4.png";

            //p2 = new MapPanel()
            //{
            //    Top = 30,
            //    Left = 10,
            //    GraphicsDevice = this.GraphicsDevice,
            //    SpriteBatch = this.spriteBatch,
            //    Margin = Origins.TopLeft,
            //    //Selected=false,
            //};
            //p2.LoadContent();
            //p2.Selected = false;
            //p2.SeaText = "母港周边哨戒";
            //p2.CreatorText = "blblb";
            //p2.MapText = "母港附近海域";
            //p2.PreviewPath = @"Content\Map_1-1.png";

            //gifp = new GifPlayer()
            //{
            //    Top = 300,
            //    Left = 100,
            //    Margin = Origins.TopLeft,
            //    Color=Color.White,
            //    GraphicsDevice = this.GraphicsDevice,
            //    SpriteBatch = this.spriteBatch,
            //};
            //gifp.LoadContent();
            //gifp.ImagePath = @"Content\wtf7.gif";

            //b = new Button()
            //{
            //    Top = 100,
            //    Left = 100,
            //    Width = 200,
            //    Height = 40,
            //    Color=Color.White,
            //    GraphicsDevice = this.GraphicsDevice,
            //    SpriteBatch = this.spriteBatch,
            //    Margin = Origins.TopLeft,
            //};
            //b.LoadContent();
            //b.Text = "A strange button";
            //b.ToolStrip = "A strange Toolstrip 君がいてくれる ここにいてくれる\nThe second line 还是应该留在这里 依然等待倒数计时\nAnother Line いつかこんな日がくることなんて\nMulti line test Oh China(Chinese) Mi Fans ";
            //b.BaseColor = Color.Red;
            //b.HoverColor = Color.Green;
            //b.ClickColor = Color.Yellow;
            //combob = new ComboBox()
            //{
            //    Top = 100,
            //    Left = 100,
            //    Width = 300,
            //    Margin = Origins.TopLeft,
            //    GraphicsDevice = this.GraphicsDevice,
            //    SpriteBatch = this.spriteBatch,
            //};
            //combob.LoadContent();
            //combob.AddItem("标准屏幕",true);
            //combob.AddItem("800x600");
            //combob.AddItem("1024x768");
            //combob.AddItem("宽屏幕", true);
            //combob.AddItem("1024x600");
            //combob.AddItem("1280x720");
            //combob.AddItem("1366x768 (无边框)");

            //combob.SelectedItem = "1024x600";

            //trackb = new TrackBar(300)
            //{
            //    Top=100,
            //    Left=100,
            //    Margin=Origins.TopLeft,
            //    GraphicsDevice=GraphicsDevice,
            //    SpriteBatch=spriteBatch,
            //    MinValue=0,
            //    MaxValue =100,
            //    ToolStripFormat = "{0}jjy",
            //};
            //trackb.LoadContent();
            //tb = new TabControl()
            //{
            //    Top = 0,
            //    Left = 0,
            //    GraphicsDevice = this.GraphicsDevice,
            //    SpriteBatch = this.spriteBatch,
            //    Margin = Origins.TopLeft,
            //    Width = GraphicsDevice.Viewport.Width,
            //    Height=GraphicsDevice.Viewport.Height,
            //};
            //tb.LoadContent();

            //pb = new ProgressBar()
            //{
            //    Top=100,
            //    Left=100,
            //    GraphicsDevice=this.GraphicsDevice,
            //    SpriteBatch=this.spriteBatch,
            //    Margin=Origins.TopLeft,
            //    Width=40,
            //    Height=300,
            //    EdgeWidth=2
            //};
            //pb.LoadContent();
            //pb.Type = ProgressBarTypes.Circle;
            //pb.Value = 62;

            //mi = new MapImage(new Map("map.xml"))
            //{
            //    Top = 100,
            //    Left = 100,
            //    GraphicsDevice = this.GraphicsDevice,
            //    SpriteBatch = this.spriteBatch,
            //    Margin = Origins.TopLeft
            //};
            //mi.LoadContent();



            //            sp = new ScrollPanel()
            //            {
            //                GraphicsDevice = this.GraphicsDevice,
            //                SpriteBatch = this.spriteBatch,
            //                Color = Color.White,
            //                Margin = Origins.TopLeft,
            //                Width = 300,
            //                Height = 300,
            //                Top=100,
            //                Left=100,
            //            };

            //            var lb = new Label()
            //            {
            //                Font = new Font(GraphicsDevice, "msyh.ttc", 15)
            //                {
            //                    EnableBorder = true,
            //                    BorderColor = Color.Black
            //                },
            //                Color = Color.White,
            //                Top = 10,
            //                Left = 10,
            //                Margin = Origins.TopLeft,
            //                GraphicsDevice = this.GraphicsDevice,
            //                SpriteBatch=this.spriteBatch,
            //            };
            //            lb.LoadContent();
            //            lb.Text = @"心の中で描いた地図は 
            //曾近绘于心的地图
            //行き止まりばかり 迷路みたいで 
            //如同迷路了一般 总是处处碰壁
            //きっと誰もが傷つきながら 
            //谁都一样 都是在受伤时
            //心に鍵かけて泣いているんだろう 
            //闭上自己的心扉哭泣着吧
            //いつも強がって 自分を奮い立たせ 
            //一直故作坚强 让自己奋起直追
            //溢れる人ごみの中 遠い空、見上げてた 
            //在这比肩接踵的人群中 仰望那遥远的天空
            //過去の言葉より 未来のノートを開こう 
            //抛弃过去的只言片语 打开那未来的一页吧
            //つまづいた分だけ 少しずつ 強くなれるよ 
            //在失足倒地后 慢慢坚强起来
            //世界でひとつの君という奇跡が 
            //在这世间 有一个被称为「你」的奇迹
            //モノクロの世界を色鮮やかに変えてゆく 
            //让这黑白两色的世界变得五彩斑斓
            //生きていく意味を見出すことが出来たなら 
            //若能找到活下去的意义
            //描く未来は 君から始まる勇気 
            //从中描绘出的未来 便是源自于你的勇气
            //上手く行かずに 臆病になって 
            //无法好好前进 变得胆怯懦弱
            //自分らしささえ 見失っても 
            //就算心中的真实逐渐迷失
            //握り締めてる 熱い想いを 
            //也绝不会放手 绝不会忘怀
            //ずっと忘れずに 歩き続けたい 
            //那份热切的思念 坚持一步步前行
            //夢を追いかけた 少年の日の輝き 
            //追逐着梦想的少年 每一天都闪耀着光辉
            //誰もが抱きしめながら 同じ今を生きてる 
            //谁都会拥抱着 那不变的当下生活下去
            //寂しい夜だって 誰かと繋がってる気持ち 
            //寂寞的夜晚里 也会感到和某人心心相印
            //心のどこかで いつも信じていたいんだ 
            //在心中的某处 一直这样坚信不疑
            //世界でひとつの明日という未来が 
            //在这世间 有一种被称为「明天」的未来
            //小さな涙の種を大きな花へ変える 
            //将小小的泪珠变化为大大的花儿
            //生きていく意味を見出すことが出来たなら 
            //若能说出活下去的意义
            //昨日までの自分を越えられるはず 
            //就一定会超越昨日的自己
            //理想や憧れが大きすぎてつらくなったら 
            //理想与憧憬太过宏大 变得举步维艰之时
            //ありのままの心で君に語りかけよう 
            //就用那毫无掩饰的心向你细细诉说吧
            //回り道のどこかで 道なき道のどこかで 
            //在刻意绕远的道路某处 在没有尽头的道路某处
            //出会えるかもしれない その時は笑顔で… 
            //也许会再次 和那时的笑颜相遇吧····
            //世界でひとつの君という奇跡が消えない希望を 
            //在这世间 有一个被称为「你」的奇迹
            //僕に教えてくれたんだ 
            //带给了我永不消逝的希望
            //生きていく意味は いつでもその胸にあるよ 
            //那活下去的意义 一直都在这心中哦
            //描く未来は もうすぐその先に 
            //描绘的未来 就在不远的前方
            //世界でひとつの君という光が 
            //在这世间 有一道被称为「你」的光芒
            //小さなこの地球のなか 広がる闇を照らす 
            //在这小小的地球上不断扩大 照亮了黑暗
            //繋がる空の下 さあ自分らしく進もう 
            //彼此相连的青空之下 来吧 用自己独有的方式前进
            //始まりの詩贈るよ 歩き出す君へ 
            //将这起始的诗篇 赠与迈出步伐的你";
            //            sp.AddComponent(lb);
            //            sp.LoadContent();

            //            tb.AddTab(spm,"Map");
            //            //tb.AddTab(p2,"p2");
            //            tb.AddTab(gifp, "GifPlayer"); 
            //            tb.AddTab(cb, "CheckBox");
            //            tb.AddTab(b, "Button");
            //            tb.AddTab(combob, "ComboBox");
            //            tb.AddTab(trackb, "Trackbar");
            //            tb.AddTab(pb, "ProgressBar");
            //            tb.AddTab(mi,"Map");
            //            tb.AddTab(sp, "ScrollPanel");

            //            //tb.AddTab(counter, "Counter");

            //            tb.SwitchTab(0);
            #endregion
            
            bg = Sprite.CreateTextureFromFile(@"Content\daywar.png");
            top = Sprite.CreateTextureFromFile(@"Content\songselect-top.png");

            AddComponent(spm);
            AddComponent(b);
            AddComponent(breturn);
            AddComponent(combob);
            AddComponent(groupcb);
            AddComponent(sortcb);
            AddComponent(tb);


            base.LoadContent();

            CreateMap();

            foreach (MapPanel m in maps)
                spm.AddComponent(m);
            spm.ScrollSpeed = 150;
            b.Text = "开始颓废";
            breturn.Text = "返回母港";

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
            //p_Click((object)maps[0], null);
        }

        void breturn_Click(object sender, MouseEventArgs e)
        {
            Game1.Instance.Navigate(Game1.Instance.harbor);
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
            jxGameFramework.Graphics.Instance.SpriteBatch.Draw(bg, new Rectangle(0, 0, bg.Width, bg.Height), Color.White);
            jxGameFramework.Graphics.Instance.SpriteBatch.FillRectangle(new Rectangle(0, 0, 370, 2000), new Color(128, 128, 128, 200));
            spm.Draw(gameTime);
            jxGameFramework.Graphics.Instance.SpriteBatch.Draw(top, new Vector2(0, 0), Color.White);
            if(selected !=null)
            {
                fnt.DrawText(new Vector2(10, 10), string.Format("{0} - {1}", selected.SeaText, selected.MapText), Color.White);
                fntsmall.DrawText(new Vector2(10, 40), string.Format("作者: {0}", selected.CreatorText), Color.White);
                fntsmall.DrawText(new Vector2(10, 60), string.Format("点数: {0} 路径数: {1} 路线数: {2}", m.Points, m.Routes, m.Ways), Color.White);
                fntsmall.DrawText(new Vector2(10, 80), string.Format("迂回点数: {0} 停泊点数: {1} 资源点数: {2}", m.CanSkipPoint, m.AnchorPoint, m.ResourcesPoint), Color.White);
                jxGameFramework.Graphics.Instance.SpriteBatch.Draw(selected.MapSprite.Texture, new Rectangle(12, 180, (int)(selected.MapSprite.Texture.Width * 0.65), (int)(selected.MapSprite.Texture.Height * 0.65)), Color.White);
                fntborder.DrawText(new Vector2(13, 385), "地图描述:", Color.White);
                fntborder.DrawText(new Vector2(13, 405), "这是一款全新突破性质的牙膏", Color.White);
                combob.Draw(gameTime);
                b.Draw(gameTime);
            }
            else
            {
                fnt.DrawText(new Vector2(10, 10), "选择一张地图以查看信息", Color.White);
            }
            breturn.Draw(gameTime);
            tb.Draw(gameTime);
            fnt.DrawText(new Vector2(jxGameFramework.Graphics.Instance.GraphicsDevice.Viewport.Width - 280, 10), "排序", Color.LightGreen);
            fnt.DrawText(new Vector2(jxGameFramework.Graphics.Instance.GraphicsDevice.Viewport.Width - 560, 10), "分组", Color.CornflowerBlue);
            groupcb.Draw(gameTime);
            sortcb.Draw(gameTime);
        }

    }
    partial class MapSelect : BaseScene
    {
        void CreateMap()
        {
            //TODO : database
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
                Top = id * 85+10,
                Right = -200,
                Margin = Origins.TopRight,
            };
            p.LoadContent();
            p.SeaText = st;
            p.CreatorText = "幻萌网络";
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
                p.Right = -200;
            }

            s.Selected = true;
            s.Right = -65;
            selected = s;
        }
    }
}
