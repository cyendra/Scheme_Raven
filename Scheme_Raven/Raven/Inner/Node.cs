using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Symbols;

using Env = Scheme_Raven.Raven.Symbols.Environment;

namespace Scheme_Raven.Raven.Inner
{
    public class Node
    {
        public Node()
        {
            Type = NodeType.Unknown;
        }
        virtual public void Error(string s)
        {

        }
        virtual public bool IsLeaf()
        {
            return false;
        }
        public virtual bool NotLeaf()
        {
            return false;
        }
        public virtual void ShowYourSelf()
        {

        }
        public virtual string Description()
        {
            return "";
        }
        public virtual Value Eval(Env env)
        {
            if (Type == NodeType.Leaf) return visiter.Eval((LeafNode)this, env);
            if (Type == NodeType.NonLeaf) return visiter.Eval((NonLeafNode)this, env);
            return visiter.Eval(this, env);
        }
        protected Visiter visiter = new Visiter();
        public enum NodeType
        {
            Leaf, NonLeaf, Unknown
        };
        public NodeType Type
        {
            get;
            set;
        }
    }
}
