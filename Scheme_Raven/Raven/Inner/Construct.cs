using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Inner
{
    public class Construct<T>
    {
        private T first;
        private T second;

        public Construct()
        {
            first = default(T);
            second = default(T);
        }
        public Construct(T x)
        {
            first = x;
            second = default(T);
        }
        public Construct(T x, T y)
        {
            first = x;
            second = y;
        }

        public T Car()
        {
            return first;
        }
        public T Cdr()
        {
            return second;
        }

        public void SetCar(T x)
        {
            first = x;
        }
        public void SetCdr(T x)
        {
            second = x;
        }
    }
}
