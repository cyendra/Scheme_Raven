using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Lexer;

namespace Scheme_Raven.Raven.Inner
{
    public class LeafNode : Node
    {
        public LeafNode(Token token)
        {
            tok = token;
        }
        protected Token tok;
    }
}
