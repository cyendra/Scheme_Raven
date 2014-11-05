using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class BooleanValue : Value
    {
        public BooleanValue()
        {
            _type = ValueType.Boolean;
        }
        public BooleanValue(bool b)
        {
            _type = ValueType.Boolean;
            Boolean = b;
        }
        public bool Boolean { get; set; }
        public override string Description()
        {
            if (Boolean) return "真";
            else return "假";
        }
    }
}
