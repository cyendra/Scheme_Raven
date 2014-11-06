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
                //System.Console.WriteLine("收到："+s);
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
                System.Console.WriteLine();
            }
        }
    }
}
/*
 【设置 不科学
   【函数 【参数】
       【如果 【大于 参数 0】
           【加 参数 【不科学 【减 参数 1】】】
           参数】】】
 
 【设置 阶乘
   【函数 【参数】
       【如果 【等于 参数 0】
           1
           【乘 参数 【阶乘 【减 参数 1】】】】】】
 
【设置 斐波那契
    【函数 【参数】
        【如果 【小于等于 参数 1】
             1
            【加 【斐波那契 【减 参数 1】】
                【斐波那契 【减 参数 2】】】】】】
【执行
    【设置 变量 100】
    【设置 倍增
        【函数 【参数】
            【乘 参数 2】】】
    【倍增 变量】】  
*/