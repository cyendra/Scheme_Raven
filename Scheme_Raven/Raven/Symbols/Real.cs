using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class Real : Value
    {
        public Real()
        {
            _type = ValueType.Real;
        }
        public Real(double v)
        {
            _type = ValueType.Real;
            Number = v;
        }
        public double Number
        {
            get;
            set;
        }
        public override bool Equal(Value v)
        {
            if (base.Equal(v))
            {
                return ((Real)v).Number == this.Number;
            }
            return false;
        }
        public override string Description()
        {
            return "" + Number;
        }
    }
}
