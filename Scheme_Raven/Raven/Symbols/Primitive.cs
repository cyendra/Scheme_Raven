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

        private Value ProcBoolCal(ParametersList param, string cal)
        {
            if (param.Size() < 2) return new ErrorValue("至少需要两个参数");
            bool RealNumber = false;
            int sz = param.Size();
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
                bool res = true;
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
                    double num = 0;
                    if (itemType == ValueType.Integer) num = ((Integer)item).Number;
                    if (itemType == ValueType.Real) num = ((Real)item).Number;
                    //Console.WriteLine(rv);
                    if (cal == "大于")
                    {
                        if (rv <= num)
                        {
                            res = false;
                            break;
                        }
                    }
                    if (cal == "等于")
                    {
                        if (rv != num)
                        {
                            res = false;
                            break;
                        }
                    }
                    if (cal == "小于")
                    {
                        if (rv >= num)
                        {
                            res = false;
                            break;
                        }
                    }
                    if (cal == "大于等于")
                    {
                        if (rv < num)
                        {
                            res = false;
                            break;
                        }
                    }
                    if (cal == "小于等于")
                    {
                        if (rv > num)
                        {
                            res = false;
                            break;
                        }
                    }
                    rv = num;
                }

                return new BooleanValue(res);
            }
            else
            {
                int iv = ((Integer)param.At(0)).Number;
                bool res = true;
                for (int i = 1; i < sz; i++)
                {
                    Value item = param.At(i);
                    int num = ((Integer)item).Number;
                    if (cal == "大于")
                    {
                        if (iv <= num)
                        {
                            res = false;
                            break;
                        }
                    }
                    if (cal == "等于")
                    {
                        if (iv != num)
                        {
                            res = false;
                            break;
                        }
                    }
                    if (cal == "小于")
                    {
                        if (iv >= num)
                        {
                            res = false;
                            break;
                        }
                    }
                    if (cal == "大于等于")
                    {
                        if (iv < num)
                        {
                            res = false;
                            break;
                        }
                    }
                    if (cal == "小于等于")
                    {
                        if (iv > num)
                        {
                            res = false;
                            break;
                        }
                    }
                    iv = num;
                }
                return new BooleanValue(res);
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
            if (Name == "大于")
            {
                return ProcBoolCal(param, "大于");
            }
            if (Name == "小于")
            {
                return ProcBoolCal(param, "小于");
            }
            if (Name == "等于")
            {
                return ProcBoolCal(param, "等于");
            }
            if (Name == "大于等于")
            {
                return ProcBoolCal(param, "大于等于");
            }
            if (Name == "小于等于")
            {
                return ProcBoolCal(param, "小于等于");
            }
            if (Name == "构造")
            {
                return ProcCons(param);
            }
            if (Name == "取首")
            {
                return ProcGetFirst(param);
            }
            if (Name == "取尾")
            {
                return ProcGetSecond(param);
            }
            if (Name == "列表")
            {
                return ProcList(param);
            }
            return new ErrorValue("不是有效的基础库函数");
        }

        private Value ProcCons(ParametersList param)
        {
            if (param.Size() != 2) return new ErrorValue("构造需要两个参数");
            Value fst = param.At(0);
            Value sec = param.At(1);
            Construct cons = new Construct(fst, sec);
            return cons;
        }

        private Value ProcGetFirst(ParametersList param)
        {
            if (param.Size() != 1) return new ErrorValue("需要一个参数");
            Construct cons = param.At(0) as Construct;
            if (cons == null) return new ErrorValue("参数不是一个对");
            return cons.Car();
        }
        private Value ProcGetSecond(ParametersList param)
        {
            if (param.Size() != 1) return new ErrorValue("需要一个参数");
            Construct cons = param.At(0) as Construct;
            if (cons == null) return new ErrorValue("参数不是一个对");
            return cons.Cdr();
        }
        private Value ProcList(ParametersList param)
        {
            int sz = param.Size();
            if (sz == 0) return Value.NonValue;
            Construct list = new Construct(param.At(sz - 1));
            for (int i = sz - 2; i >= 0; i--)
            {
                list = new Construct(param.At(i), list);
            }
            return list;
        }

        public static List<string> PrimitiveProceduresNameList = new List<string> { "加", "减", "乘", "除","大于","等于","小于","大于等于","小于等于", "构造", "取首", "取尾", "列表" };
        public static Dictionary<string, Primitive> PrimitiveProcedures()
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
