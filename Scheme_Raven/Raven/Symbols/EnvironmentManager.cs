using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Scheme_Raven.Raven.Symbols
{
    public class EnvCanNotBeNullException : ApplicationException
    {

    }

    public class EnvironmentManager
    {
        public static bool LookupVariableValue(string variable, out Value value, Environment env)
        {
            if (env == null) throw new EnvCanNotBeNullException(); 
            bool findVariable=false;            
            do
            {
                findVariable = env.FindVariable(variable, out value);
                env = env.GetPrevEnv();
            } while (!findVariable && env != null);
            return findVariable;
        }

        public static bool DefineVariable(string variable, Value value, Environment env)
        {
            if (env == null) throw new EnvCanNotBeNullException();
            return env.DefineVariable(variable, value);
        }

        public static bool SetVariableValue(string variable, Value value, Environment env)
        {
            if (env == null) throw new EnvCanNotBeNullException();
            bool findVariable = false;
            do
            {
                findVariable = env.SetVariable(variable, value);
                env = env.GetPrevEnv();
            } while (!findVariable && env != null);
            return findVariable;
        }

        public static Environment SetupEnvironment()
        {
            Environment env = new Environment();
            var map = Primitive.PrimitiveProcedures();
            int sz = map.Count;
            foreach (var item in map)
            {
                env.DefineVariable(item.Key, item.Value);
            }
            return env;
        }

        public static Environment GetSubEnviroment(Environment env)
        {
            Environment sub = new Environment(env);
            return sub;
        }

    }
}
