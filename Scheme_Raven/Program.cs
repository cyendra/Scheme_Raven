using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Lexer;

namespace Scheme_Raven
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer lex = new Lexer();
            for (; ; )
            {
                Token tok = lex.Read();
                if (tok == Token.Eof) break;
                Console.WriteLine(tok.Text);
            }
        }
    }
}
