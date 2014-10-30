using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Inner
{
    public enum VType
    {
        Nil, Integer, BigInteger, Real, BigDecimal, String, Function, Quote, Construct
    };

    public class CastException : System.ApplicationException
    {
        public static readonly string Msg = "Can not cast to ";
        public CastException() { }
        public CastException(string s) : base(Msg + s) { }
    }

    public class Value
    {
        public Value()
        {
            Type = VType.Nil;
        }

        public VType Type
        {
            get { return Type; }
            set { Type = value; }
        }

        public virtual int GetInteger()
        {
            throw new CastException("Integer");
        }
        public virtual double GetReal()
        {
            throw new CastException("Real");
        }
        public virtual string GetString()
        {
            throw new CastException("String");
        }

    }
}
