using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class ErrorValue : Value
    {
        public ErrorValue()
        {
            _type = ValueType.Error;
        }
    }
}
