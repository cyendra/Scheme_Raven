using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class Procedure : Value
    {
        public Procedure()
        {
            _type = ValueType.Procedure;
        }
        public string Name { get; set; }
        public override string Description()
        {
            return "过程基类";
        }
        public virtual Value Run(ParametersList param)
        {
            return new ErrorValue("过程基类不能运行");
        }
    }
}
