using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WarshipGirl.Data
{
    class Map
    {
        public string MapIndex { get; set; }
        public string LevelIndex { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public int Points
        {
            get
            {
                return Node.Count;
            }
        }
        public int AnchorPoint
        {
            get
            {
                int s=0;
                foreach (Point p in Node)
                    if (p.Type == PointType.Anchor)
                        s++;
                return s;
            }
        }
        public int ResourcesPoint
        {
            get
            {
                int s = 0;
                foreach (Point p in Node)
                    if (p.Type == PointType.Resources)
                        s++;
                return s;
            }
        }
        public int CanSkipPoint
        {
            get
            {
                int s = 0;
                foreach (Point p in Node)
                    if (p.Type == PointType.CanSkip)
                        s++;
                return s;
            }
        }
        public int Routes
        {
            get
            {
                int s = 0;
                foreach (Point p in Node)
                    s += p.Routes.Count;
                return s;
            }
        }
        int _ways;

        public int Ways
        {
            get
            {
                return _ways;
            }
        }
        public List<Point> Node;
        public List<Fleet> Fleets;

        public Map(string XmlPath)
        {
            var doc = new XmlDocument();
            doc.Load(XmlPath);

            //Basic
            var root = doc.SelectSingleNode("Map");

            LevelIndex = root.Attributes["LevelIndex"].InnerText;
            MapIndex = root.Attributes["MapIndex"].InnerText;
            Title = root.Attributes["Title"].InnerText;
            Description = root.Attributes["Description"].InnerText;
            Creator = root.Attributes["Creator"].InnerText;

            //Points
            var nodes = root.SelectSingleNode("Node");
            Node = new List<Point>();
            foreach (XmlNode x in nodes.ChildNodes)
            {
                //Basic
                var p = new Point();
                p.Type = x.Attributes["Type"] != null ? (PointType)Enum.Parse(typeof(PointType), x.Attributes["Type"].InnerText) : PointType.Normal;
                p.X = x.Attributes["X"] != null ? int.Parse(x.Attributes["X"].InnerText) : 0;
                p.Y = x.Attributes["Y"] != null ? int.Parse(x.Attributes["Y"].InnerText) : 0;
                p.CanSkip = x.Attributes["CanSkip"] != null ? Convert.ToBoolean(int.Parse(x.Attributes["CanSkip"].InnerText)) : false;
                p.Name = x.Attributes["Name"] != null ? x.Attributes["Name"].InnerText : "Start";

                //Routes
                var rx = x.SelectSingleNode("Routes");
                p.Routes = new List<Route>();
                if (rx != null)
                    foreach (XmlNode routex in rx.ChildNodes)
                    {
                        var s = new Route();
                        s.Target = routex.Attributes["Target"].InnerText;
                        s.Star = routex.Attributes["Star"] != null ? int.Parse(routex.Attributes["Star"].InnerText) : 0;
                        p.Routes.Add(s);
                    }

                //Enemies
                var ex = x.SelectSingleNode("Enemies");
                p.Enemies = new List<Fleet>();
                if (ex != null)
                    foreach (XmlNode enemx in ex.ChildNodes)
                    {
                        var e = new Fleet();
                        e.ID = int.Parse(enemx.Attributes["ID"].InnerText);
                        p.Enemies.Add(e);
                    }

                //Items
                var ix = x.SelectSingleNode("Items");
                p.Items = new List<Item>();
                if (ix != null)
                    foreach (XmlNode itemx in ix.ChildNodes)
                    {
                        var i = new Item();
                        i.Type = (ItemType)Enum.Parse(typeof(ItemType), itemx.Attributes["Type"].InnerText);
                        i.Value = int.Parse(itemx.Attributes["Value"].InnerText);
                        p.Items.Add(i);
                    }
                if (ex == null && ix != null)
                    p.Type = PointType.Resources;
                if (ex == null && ix == null)
                    p.Type = PointType.Anchor;
                if (p.CanSkip)
                    p.Type = PointType.CanSkip;

                Node.Add(p);
            }

            //Fleets
            Fleets = new List<Fleet>();
            var fn = root.SelectSingleNode("Fleets");
            foreach (XmlNode x in fn.ChildNodes)
            {
                var f = new Fleet();
                f.ID = int.Parse(x.Attributes["ID"].InnerText);
                f.Ships = new List<BaseShip>();
                foreach (XmlNode sx in x)
                {
                    var s = new BaseShip();
                    s.ID = int.Parse(sx.Attributes["ID"].InnerText);
                    f.Ships.Add(s);
                }
                Fleets.Add(f);
            }
            CheckWays(Node[0]);
        }
        public Point GetPoint(string PointName)
        {
            foreach (Point p in Node)
                if (p.Name == PointName)
                    return p;
            return null;
        }
        private void CheckWays(Point pBegin)
        {
            if (pBegin.CanSkip)
            {
                if (pBegin.Routes.Count != 0)
                    foreach (Route r in pBegin.Routes)
                    {
                        var pTravel = GetPoint(r.Target);
                        CheckWays(pTravel);
                    }
                else
                    _ways++;
            }
            if (pBegin.Routes.Count != 0)
                foreach (Route r in pBegin.Routes)
                {
                    var pTravel = GetPoint(r.Target);
                    CheckWays(pTravel);
                }
            else
                _ways++;
        }
    }
    enum PointType
    {
        Start, Normal, Boss , Anchor, Resources,CanSkip
    }
    class Point
    {
        public PointType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool CanSkip { get; set; }
        public string Name { get; set; }

        public List<Route> Routes;
        public List<Fleet> Enemies;
        public List<Item> Items;
    }
    class Route
    {
        public string Target { get; set; }
        public int Star { get; set; }
    }
    class Enemies
    {
        public List<Fleet> Fleets;
    }
    enum ItemType
    {
        Oil, Ring
    }
    class Item
    {
        public ItemType Type { get; set; }
        public int Value { get; set; }
    }
}
