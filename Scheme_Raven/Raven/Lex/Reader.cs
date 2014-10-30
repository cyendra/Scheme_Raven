using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Lex
{
    public class Reader
    {
        protected bool Eof;
        public Reader()
        {
            Eof = false;
        }
        public virtual bool HasMore()
        {
            return !Eof;
        }
        public virtual string ReadLine()
        {
            string s = Console.ReadLine();
            if (s == null)
            {
                Eof = true;
            }
            return s;
        }
    }
}
