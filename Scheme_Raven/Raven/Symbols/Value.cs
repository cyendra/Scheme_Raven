﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class Value
    {
        public enum ValueType
        {
            Integer, Real, String, Quote, Procedure, Construct, Boolean, Error, Non
        }
        public Value() 
        {
            _type = ValueType.Non;
        }
        public virtual bool Equal(Value v)
        {
            return this.Type == v.Type;
        }
        protected ValueType _type;
        public ValueType Type
        {
            get { return _type; }
        }
        public virtual string Description()
        {
            return "没什么";
        }
        public static readonly Value NonValue = new Value();
    }
}
