﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class String : Value
    {
        public String()
        {
            _type = ValueType.String;
        }
        public string Str
        {
            get;
            set;
        }
        public override bool Equal(Value v)
        {
            if (base.Equal(v))
            {
                return ((String)v).Str == this.Str;
            }
            return false;
        }
        public override string Description()
        {
            return "『" + Str + "』";
        }
    }
}
