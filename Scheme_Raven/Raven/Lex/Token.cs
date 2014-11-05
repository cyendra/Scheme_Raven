using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Lex
{
    public enum TokType
    {
        Value, String, LeftParentheses, RightParentheses, Identifier
    }
    public class Token
    {
        public Token()
        {
            Text = "";
        }
        public Token(char c)
        {
            Text = "" + c;
        }
        public Token(string s)
        {
            Text = s;
        }
        public Token(StringBuilder builder)
        {
            Text = builder.ToString();
        }
        public string Text
        {
            get {
                //if (type == TokType.String) return "『" + text + "』";
                return text;
            }
            set { text = value; }
        }
        public int LineNmeber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }
        private TokType type;
        public TokType Type
        {
            get { return type; }
            set { type = value; }
        }
        private string text;
        private int lineNumber;
        public static readonly Token Eof = new Token();
        public static readonly Token Eol = new Token();
    }
}
