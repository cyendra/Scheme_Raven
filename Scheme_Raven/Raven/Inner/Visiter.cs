using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Symbols;

using Env = Scheme_Raven.Raven.Symbols.Environment;

namespace Scheme_Raven.Raven.Inner
{
    public class Visiter
    {
        public Visiter() { }
        public Value Eval(LeafNode rt, Env env)
        {
            System.Console.WriteLine("LeafNode");
            return Value.NonValue;
        }
        public Value Eval(NonLeafNode rt, Env env)
        {
            System.Console.WriteLine("NonLeafNode");
            return Value.NonValue;
        }
        public Value Eval(Node rt, Env env)
        {
            System.Console.WriteLine("Node");
            return Value.NonValue;
        }
    }

}
