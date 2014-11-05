using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class ParametersList
    {
        public ParametersList()
        {
            list = new List<Value>();
        }
        private List<Value> list;
        public void Add(Value v)
        {
            list.Add(v);
        }
        public void Clear()
        {
            list.Clear();
        }
        public void Remove(int i)
        {
            list.RemoveAt(i);
        }
        public Value At(int i)
        {
            return list.ElementAt(i);
        }
        public int Size()
        {
            return list.Count;
        }
    }
}
