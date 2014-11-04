using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Inner
{
    public class NonLeafNode : Node
    {
        public NonLeafNode()
        {
            Type = NodeType.NonLeaf;
            childs = new List<Node>();
        }
        public override bool NotLeaf()
        {
            return true;
        }
        public void Append(Node nd)
        {
            childs.Add(nd);
        }
        public Node Car()
        {
            return childs.ElementAt(0);
        }
        public Node At(int i)
        {
            return childs.ElementAt(i);
        }
        public override void ShowYourSelf()
        {
            base.ShowYourSelf();
            System.Console.Write("【");
            foreach (Node n in childs)
            {
                n.ShowYourSelf();
                System.Console.Write(" ");
            }
            System.Console.Write("】");
        }
        private List<Node> childs;
        public override string Description()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("【 ");
            foreach (var n in childs)
            {
                buf.Append(n.Description());
                buf.Append(" ");
            }
            buf.Append("】");
            return buf.ToString();
        }
    }
}
