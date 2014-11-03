using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class Construct : Value
    {
        public Construct()
        {
            _type = ValueType.Construct;
        }
        private Value first;
        private Value second;

        public Construct()
        {
            first = default(Value);
            second = default(Value);
        }
        public Construct(Value x)
        {
            first = x;
            second = default(Value);
        }
        public Construct(Value x, Value y)
        {
            first = x;
            second = y;
        }

        public Value Car()
        {
            return first;
        }
        public Value Cdr()
        {
            return second;
        }

        public void SetCar(Value x)
        {
            first = x;
        }
        public void SetCdr(Value x)
        {
            second = x;
        }

        public override string Description()
        {
            return "(" + first.Description() + " " + second.Description() + ")";
        }

    }
}
