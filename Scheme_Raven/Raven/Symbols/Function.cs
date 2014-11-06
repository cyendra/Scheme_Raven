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
            bodylist = new List<Node>();
        }
        private List<string> paramlist;
        private List<Node> bodylist;
        public List<string> Parameters
        {
            get { return paramlist; }
        }
        public List<Node> Body
        {
            get { return bodylist; }
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
            foreach (var item in Body)
            {
                buf.Append(item.Description()+"\n");
            }
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
                //Console.Write(Parameters.ElementAt(i) + " " + param.At(i).Description());
            }
            //Console.WriteLine();
            Value rs = Value.NonValue;
            foreach (var item in Body)
            {
                rs = item.Eval(subEnv);
                if (rs is ErrorValue) return rs;
            }
            return rs;
        }
    }
}
