using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheme_Raven.Raven.Symbols
{
    public class Primitive : Procedure
    {
        public Primitive()
        {
        }
        public Primitive(string s)
        {
            Name = s;
        }
        public override string Description()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("基本过程：" + Name + "\n");
            return buf.ToString();
        }

        private Value ProcNumberCal(ParametersList param, char cal)
        {
            bool RealNumber = false;

            int sz = param.Size();
            if (sz == 0)
            {
                if (cal == '*' || cal == '/')
                {
                    return new Integer(1);
                }
                return new Integer(0);
            }

            for (int i = 0; i < sz; i++)
            {
                Value item = param.At(i);
                ValueType itemType = item.Type;
                if (itemType != ValueType.Integer && itemType != ValueType.Real)
                {
                    return new ErrorValue("参数不是有效的数字");
                }
                if (itemType == ValueType.Real)
                {
                    RealNumber = true;
                }
            }

            if (RealNumber)
            {
                //Console.WriteLine("REAL");
                double rv = 0.0;
                Value item = param.At(0);
                ValueType itemType = item.Type;
                if (itemType == ValueType.Integer)
                {
                    rv = (double)((Integer)item).Number;
                }
                else if (itemType == ValueType.Real)
                {
                    rv = (double)((Real)item).Number;
                }
                for (int i = 1; i < sz; i++)
                {
                    item = param.At(i);
                    itemType = item.Type;
                    if (itemType == ValueType.Integer)
                    {
                        if (cal == '+') rv += ((Integer)item).Number;
                        if (cal == '-') rv -= ((Integer)item).Number;
                        if (cal == '*') rv *= ((Integer)item).Number;
                        if (cal == '/') rv /= ((Integer)item).Number;
                    }
                    if (itemType == ValueType.Real)
                    {
                        if (cal == '+') rv += ((Real)item).Number;
                        if (cal == '-') rv -= ((Real)item).Number;
                        if (cal == '*') rv *= ((Real)item).Number;
                        if (cal == '/') rv /= ((Real)item).Number;
                    }
                    //Console.WriteLine(rv);
                }
                return new Real(rv);
            }
            else
            {
                int iv = ((Integer)param.At(0)).Number;
                for (int i = 1; i < sz; i++)
                {
                    Value item = param.At(i);
                    if (cal == '+') iv += ((Integer)item).Number;
                    if (cal == '-') iv -= ((Integer)item).Number;
                    if (cal == '*') iv *= ((Integer)item).Number;
                    if (cal == '/') iv /= ((Integer)item).Number;
                }
                return new Integer(iv);
            }
        }

        public override Value Run(ParametersList param)
        {
            if (Name == "加")
            {
                return ProcNumberCal(param, '+');
            }
            if (Name == "减")
            {
                return ProcNumberCal(param, '-');
            }
            if (Name == "乘")
            {
                return ProcNumberCal(param, '*');
            }
            if (Name == "除")
            {
                return ProcNumberCal(param, '/');
            }
            return new ErrorValue("不是有效的基础库函数");
        }
        public static List<string> PrimitiveProceduresNameList = new List<string> { "加", "减", "乘", "除" };
        public static Dictionary<string,Primitive> PrimitiveProcedures()
        {
            Dictionary<string, Primitive> map = new Dictionary<string, Primitive>();
            foreach (var item in PrimitiveProceduresNameList)
            {
                map.Add(item, new Primitive(item));
            }            
            return map;
        }

    }
}
