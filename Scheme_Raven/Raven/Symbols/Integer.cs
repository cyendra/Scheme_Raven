using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class Integer : Value
    {
        public Integer()
        {
            _type = ValueType.Integer;
        }
        public int Number
        {
            get;
            set;
        }
        public override bool Equal(Value v)
        {
            if (base.Equal(v))
            {
                return ((Integer)v).Number == this.Number;
            }
            return false;
        }
        public override string Description()
        {
            return "" + Number;
        }
    }
}
