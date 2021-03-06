﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Lex;

namespace Scheme_Raven.Raven.Inner
{
    public class LeafNode : Node
    {
        public LeafNode(Token token)
        {
            tok = token;
            Type = NodeType.Leaf;
        }
        public override bool IsLeaf()
        {
            return true;
        }
        public override void ShowYourSelf()
        {
            base.ShowYourSelf();
            System.Console.Write(tok.Text);
        }
        protected Token tok;
        public override string Description()
        {
            if (tok.Type == TokType.String) return "『" + tok.Text + "』";
            return tok.Text;
        }
        public Token GetToken()
        {
            return tok;
        }
    }
}
