using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Lex;
using Scheme_Raven.Raven.Inner;

namespace Scheme_Raven.Raven.Parse
{
    public class Parser
    {
        public Parser() 
        {
            lex = new Lexer();
        }
        public void Error(Token tok, string s)
        {

        }
        public void Match(TokType type)
        {
            Token tok = lex.Read();
            if (tok.Type != type) Error(tok, "syntax error"); 
        }
        public void Match(string s)
        {
            Token tok = lex.Read();
            if (tok.Text != s) Error(tok, "syntax error");
        }

        public Node GetNode()
        {
            Token look = lex.Peek(0);
            if (look.Type == TokType.LeftParentheses) 
            {
                Match(TokType.LeftParentheses);
                Node rs = GetBlock();
                Match(TokType.RightParentheses);
                return rs;
            }
            else
            {
                Node rs = new LeafNode(look);
                lex.Read();
                return rs;
            }
        }
        public Node GetBlock()
        {
            Token look = lex.Peek(0);
            NonLeafNode tree = new NonLeafNode();
            while (look.Type != TokType.RightParentheses) 
            {
                Node rs = GetNode();
                tree.Append(rs);
            }
            return tree;
        }
        private Lexer lex;
    }
}
