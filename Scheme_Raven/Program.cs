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
            Env env = EnvironmentManager.SetupEnvironment();
            for (; ; )
            {
                var rt = parser.GetNode();
                string s = rt.Description();
                System.Console.WriteLine("收到："+s);
                var rs = rt.Eval(env);
                if (rs.Type == Value.ValueType.Error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("错误：" + rs.Description());
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    System.Console.WriteLine("结果：" + rs.Description());
                }
               
            }
        }
    }
}
