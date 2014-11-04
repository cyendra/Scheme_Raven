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
        public ErrorValue(string s)
        {
            _type = ValueType.Error;
            errorMsg = s;
        }
        public override string Description()
        {
            return errorMsg;
        }
        private string errorMsg;
    }
}
