using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Lex
{

    public class LexException : System.ApplicationException
    {
        public LexException() { }
        public LexException(int lineNumber, string s) : base("在第" + lineNumber + "行：" + s + "附近，有非法的输入") { }
    }

    public class Lexer
    {
        public Lexer()
        {
            Init();
        }
        public Lexer(Reader read)
        {
            Init(read);
        }

        public void Init()
        {
            Init(new Reader());
        }
        public void Init(Reader read)
        {
            queue = new Queue<Token>();
            lineNumber = 0;
            hasMore = true;
            reader = read;
        }


        public Token Read()
        {
            if (FillQueue())
            {
                Token tok = queue.Dequeue();
                return tok;
            }
            else
            {
                return Token.Eof;
            }
        }

        public Token Peek(int i)
        {
            if (FillQueue(i))
            {
                return queue.ElementAt(i);
            }
            else
            {
                return Token.Eof;
            }
        }

        private int lineNumber;
        private string buf;
        private int pos;
        private char peek;
        private bool hasMore;
        private Reader reader;
        private Queue<Token> queue;

        private bool FillQueue(int i = 0)
        {
            while (i>=queue.Count)
            {
                if (hasMore) ReadLine();
                else return false;
            }
            return true;
        }
        private void ReadLine()
        {
            buf = reader.ReadLine();
            if (!reader.HasMore() || buf == null)
            {
                hasMore = false;
                return;
            }
            lineNumber += 1;
            pos = 0;
            for (peek=' ';;)
            {
                Token tok = Scan();
                if (tok == Token.Eol) break;
                tok.LineNmeber = lineNumber;
                queue.Enqueue(tok);
            }
        }
        private void Readch()
        {
            if (pos < buf.Length)
            {
                peek = buf[pos++];
            }
            else
            {
                peek = '\0';
            }
        }
        private bool Readch(char c)
        {
            Readch();
            if (peek==c)
            {
                peek = ' ';
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsEmptyChar(char c)
        {
            if (c == ' ' || c == '\t' || c == '\0') return true;
            return false;
        }
        private bool IsParentheses(char c)
        {
            if (c == '【' || c == '】') return true;
            return false;
        }
        private Token Scan()
        {
            for (; ; Readch())
            {
                if (peek == ' ' || peek == '\t') continue;
                else if (peek == '\0') return Token.Eol;
                else break;
            }
            Token tok;
            if (IsParentheses(peek))
            {
                tok = new Token(peek);
                peek = ' ';
                if (tok.Text == "【") tok.Type = TokType.LeftParentheses;
                if (tok.Text == "】") tok.Type = TokType.RightParentheses;
                return tok;
            }
            StringBuilder builder = new StringBuilder();
            if (Char.IsDigit(peek))
            {
                do
                {
                    builder.Append(peek);
                    Readch();
                } while (Char.IsDigit(peek));
                if (peek == '.')
                {
                    do
                    {
                        builder.Append(peek);
                        Readch();
                    } while (Char.IsDigit(peek));
                }
                if (IsEmptyChar(peek) || IsParentheses(peek))
                {
                    tok = new Token(builder);
                    tok.Type = TokType.Value;
                    return tok;
                }
                else
                {
                    throw new LexException(lineNumber, builder.ToString());
                }
            }
            if (peek == '『')
            {
                Readch();
                while (peek != '』')
                {
                    builder.Append(peek);
                    Readch();
                };
           
                peek = ' ';
                tok = new Token(builder);
                tok.Type = TokType.String;
                return tok;
            }
            do
            {
                builder.Append(peek);
                Readch();
            } while (!IsEmptyChar(peek) && !IsParentheses(peek));
            tok = new Token(builder.ToString());
            tok.Type = TokType.Identifier;
            return tok;
        }
        
    }
}
