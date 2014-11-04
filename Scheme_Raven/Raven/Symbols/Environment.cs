using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class Environment
    {
        public Environment()
        {
            prev = null;
            map = new Dictionary<string, Value>();
        }

        private Dictionary<string, Value> map;
        private Environment prev;

        public bool FindVariable(string variable, out Value value)
        {
            return map.TryGetValue(variable,out value);
        }

        public bool DefineVariable(string variable, Value value)
        {
            if (map.ContainsKey(variable)) return false;
            map.Add(variable, value);
            return true;
        }

        public bool SetVariable(string variable, Value value)
        {
            if (!map.ContainsKey(variable)) return false;
            map[variable] = value;
            return true;
        }

        public Environment GetPrevEnv()
        {
            return prev;
        }

    }
}
