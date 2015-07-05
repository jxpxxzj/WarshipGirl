using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipGirl.Data.Map
{
    class BaseMap
    {
        public string Name { get; set; }
        public int MapIndex { get; set; }
        public int LevelIndex { get; set; }
        public NodeMap Route { get; set; }

    }
    class Node
    {
        public List<Node> OutNode = new List<Node>();

        public string Name { get; set; }
        public int Possibility { get; set; }
        public List<Fleet> Enemy { get; set; }
        //public abstract bool Available(Fleet fleet);
        public Node(string name)
        {
            this.Name = name;
        }

    }
    class NodeMap
    {
        public List<Node> Nodes = new List<Node>();
        public Node PresentNode { get; set; }
        public Fleet fleet { get; set; }
        private int[] fillchar(int[] target,int num,int left,int count)
        {
            for(int i=left;i<left+count;i++)
            {
                target[i] = num;
            }
            return target;

        }
        public void MoveToNext()
        {
            //foreach(Node n in PresentNode.OutNode)
            //{
            //    if(n.Available(fleet))
            //    {
            //        PresentNode = n;
            //        return;
            //    }
            //}
            int maxp = 0;
            Random rand = new Random();
            int[] posbarr = new int[10];
            int pos = 0;
            int nodeindex = 1;
            foreach(Node n in PresentNode.OutNode)
            {
                maxp += n.Possibility;
                fillchar(posbarr,nodeindex, pos,n.Possibility);
                pos += n.Possibility;
                nodeindex++;
            }        
            int target = rand.Next(maxp);
            PresentNode = PresentNode.OutNode[posbarr[target]-1];
        }
    }
}
