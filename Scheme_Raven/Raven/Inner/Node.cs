using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Inner
{
    public class Node
    {
        public Node()
        {

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
    }
}
