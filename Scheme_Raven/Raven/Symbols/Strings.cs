using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class Strings : Value
    {
        public Strings()
        {
            _type = ValueType.String;
        }
        public Strings(string s)
        {
            _type = ValueType.String;
            Str = s;
        }
        public string Str
        {
            get;
            set;
        }
        public override bool Equal(Value v)
        {
            if (base.Equal(v))
            {
                return ((Strings)v).Str == this.Str;
            }
            return false;
        }
        public override string Description()
        {
            return "『" + Str + "』";
        }
    }
}
