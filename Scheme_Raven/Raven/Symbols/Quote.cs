using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Inner;

namespace Scheme_Raven.Raven.Symbols
{
    public class Quote : Value
    {
        public Quote()
        {
            _type = ValueType.Quote;
        }
        public Node Root
        {
            get;
            set;
        }
        public override string Description()
        {
            return "「" + Root.Description();
        }
    }
}
