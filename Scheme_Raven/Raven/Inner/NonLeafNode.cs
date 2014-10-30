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
            childs = new List<Node>();
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
        private List<Node> childs;
    }
}
