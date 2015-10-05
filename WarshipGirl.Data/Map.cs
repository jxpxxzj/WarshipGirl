using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace WarshipGirl.Data
{
    [XmlRoot("Map")]
    public class Map
    {
        [XmlIgnore]
        public string FileName { get; set; }
        public string MapIndex { get; set; }
        public string LevelIndex { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        [XmlArray("Node"), XmlArrayItem("Point")]
        public List<Point> Node { get; set; }
        [XmlArray("Fleets"), XmlArrayItem("Fleet")]
        public List<Fleet> Fleets { get; set; }

        [XmlIgnore]
        public string MapName
        {
            get
            {
                return $"{MapIndex}-{LevelIndex}";
            }
        }
        [XmlIgnore]
        public string PreviewImagePath { get; protected set; }

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
                    Ways++;
            }
            if (pBegin.Routes.Count != 0)
                foreach (Route r in pBegin.Routes)
                {
                    var pTravel = GetPoint(r.Target);
                    CheckWays(pTravel);
                }
            else
                Ways++;
        }
        [XmlIgnore]
        public int Points
        {
            get
            {
                return Node.Count;
            }
        }
        [XmlIgnore]
        public int AnchorPoint
        {
            get
            {
                int s = 0;
                foreach (Point p in Node)
                    if (p.Enemies.Count==0 && p.Items.Count==0 && p.Type != PointType.Start)
                        s++;
                return s;
            }
        }
        [XmlIgnore]
        public int ResourcesPoint
        {
            get
            {
                int s = 0;
                foreach (Point p in Node)
                    if (p.Enemies.Count == 0 && p.Items.Count != 0)
                        s++;
                return s;
            }
        }
        [XmlIgnore]
        public int CanSkipPoint
        {
            get
            {
                int s = 0;
                foreach (Point p in Node)
                    if (p.CanSkip)
                        s++;
                return s;
            }
        }
        [XmlIgnore]
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

        [XmlIgnore]
        public int Ways { get; protected set; }

        public Map(string XmlPath,string ImagePath)
        {
            FileName = XmlPath;
            PreviewImagePath = ImagePath;
            var doc = new XmlDocument();
            doc.Load(XmlPath);

            //Basic
            var root = doc.SelectSingleNode("Map");

            LevelIndex = root.Attributes["LevelIndex"].InnerText;
            MapIndex = root.Attributes["MapIndex"].InnerText;
            Title = root.Attributes["Title"].InnerText;
            Description = root.SelectSingleNode("Description").InnerText;
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
                p.CanSkip = x.Attributes["CanSkip"] != null ? bool.Parse(x.Attributes["CanSkip"].InnerText) : false;
                p.Name = x.Attributes["Name"] != null ? x.Attributes["Name"].InnerText : "Start";

                //Routes
                var rx = x.SelectSingleNode("Routes");
                p.Routes = new List<Route>();
                if (rx != null)
                    foreach (XmlNode routex in rx.ChildNodes)
                    {
                        var s = new Route();
                        s.Target = routex.Attributes["Target"].InnerText;
                        s.Possibility = routex.Attributes["Possibility"] != null ? int.Parse(routex.Attributes["Possibility"].InnerText) : 0;
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
                        i.Type = (ResourceType)Enum.Parse(typeof(ResourceType), itemx.Attributes["Type"].InnerText);
                        i.Value = int.Parse(itemx.Attributes["Value"].InnerText);
                        p.Items.Add(i);
                    }

                Node.Add(p);
            }

            //Fleets
            Fleets = new List<Fleet>();
            var fn = root.SelectSingleNode("Fleets");
            if(fn!=null)
                foreach (XmlNode x in fn.ChildNodes)
                {
                    var f = new Fleet();
                    f.ID = int.Parse(x.Attributes["ID"].InnerText);
                    f.Ships = new List<Ship>();
                    foreach (XmlNode sx in x)
                    {
                        var s = new Ship();
                        s.ID = int.Parse(sx.Attributes["ID"].InnerText);
                        f.Ships.Add(s);
                    }
                    Fleets.Add(f);
                }
            CheckWays(Node[0]);
        }
    }
    public enum PointType
    {
        Start, Normal, Boss,
    }
    public class Point
    {
        [XmlAttribute("Type")]
        public PointType Type { get; set; }
        [XmlAttribute("X")]
        public int X { get; set; }
        [XmlAttribute("Y")]
        public int Y { get; set; }
        [XmlAttribute("CanSkip")]
        public bool CanSkip { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlArray("Routes"), XmlArrayItem("Route")]
        public List<Route> Routes { get; set; }
        [XmlArray("Fleets"), XmlArrayItem("Fleet")]
        public List<Fleet> Enemies { get; set; }
        [XmlArray("Items"), XmlArrayItem("Item")]
        public List<Item> Items { get; set; }
    }
    public class Route
    {
        [XmlAttribute("Target")]
        public string Target { get; set; }
        [XmlAttribute("Possibility")]
        public int Possibility { get; set; }
    }
    public class Item
    {
        [XmlAttribute("Type")]
        public ResourceType Type { get; set; }
        [XmlAttribute("Value")]
        public int Value { get; set; }
    }
    public class Fleet
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }
        [XmlArrayItem("Ship")]
        public List<Ship> Ships { get; set; }
    }
    public class Ship
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }
    }
}
