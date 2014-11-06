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
            paramlist = new List<string>();
        }
        private List<string> paramlist;
        public List<string> Parameters
        {
            get { return paramlist; }
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
        public override Value Run(ParametersList param)
        {
            if (param.Size() != Parameters.Count) return new ErrorValue("参数数量不正确啊");
            Environment subEnv = EnvironmentManager.GetSubEnviroment(Env);
            int sz = param.Size();
            for (int i = 0; i < sz; i++)
            {
                subEnv.DefineVariable(Parameters.ElementAt(i), param.At(i));
            }
            return Body.Eval(subEnv);
        }
    }
}
