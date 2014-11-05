using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Inner;

namespace Scheme_Raven.Raven.Symbols
{
    public class Function : Procedure
    {
        public Function()
        {
        }
        
        public Queue<string> Parameters
        {
            get;
            set;
        }
        public Node Body
        {
            get;
            set;
        }
        public Environment Env { get; set; }
        public override string Description()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("过程：" + Name + "\n参数：");
            buf.Append("【 ");
            foreach (var s in Parameters)
            {
                buf.Append(s);
                buf.Append(" ");
            }
            buf.Append("】\n执行：");
            buf.Append(Body.Description());
            return buf.ToString();
        }
    }
}
