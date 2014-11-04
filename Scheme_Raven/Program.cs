using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Lex;
using Scheme_Raven.Raven.Parse;
using Scheme_Raven.Raven.Inner;

namespace Scheme_Raven
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            for (; ; )
            {
                var rt = parser.GetNode();
                string s = rt.Description();
                System.Console.WriteLine(s);
            }
        }
    }
}
