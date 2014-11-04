using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Lex;
using Scheme_Raven.Raven.Parse;
using Scheme_Raven.Raven.Inner;
using Scheme_Raven.Raven.Symbols;

using Env = Scheme_Raven.Raven.Symbols.Environment;

namespace Scheme_Raven
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            Env env = new Env();
            for (; ; )
            {
                var rt = parser.GetNode();
                string s = rt.Description();
                System.Console.WriteLine("收到了："+s);
                var rs = rt.Eval(env);
                System.Console.WriteLine("结果是：" + rs.Description());
            }
        }
    }
}
