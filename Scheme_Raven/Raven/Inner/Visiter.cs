using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Symbols;

namespace Scheme_Raven.Raven.Inner
{
    public class Visiter
    {
        public Visiter() { }
        public Value Eval(LeafNode rt)
        {
            return Value.NonValue;
        }
        public Value Eval(NonLeafNode rt)
        {
            return Value.NonValue;
        }
    }

}
