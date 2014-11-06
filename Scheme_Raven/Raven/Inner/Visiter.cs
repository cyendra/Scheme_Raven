using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheme_Raven.Raven.Symbols;
using Scheme_Raven.Raven.Lex;
using Env = Scheme_Raven.Raven.Symbols.Environment;

namespace Scheme_Raven.Raven.Inner
{
    public class Visiter
    {
        public Visiter() { }
        public Value Eval(LeafNode rt, Env env)
        {
            //System.Console.WriteLine("LeafNode");
            if (IsBoolean(rt))
            {
                if (IsTrue(rt)) return new BooleanValue(true);
                else return new BooleanValue(false);
            }
            if (IsSelfEvaluating(rt))
            {
                if (rt.GetToken().Type == TokType.String)
                {
                    return new Strings(rt.GetToken().Text);
                }
                if (rt.GetToken().Type == TokType.Value)
                {
                    string snum = rt.GetToken().Text;
                    if (snum.Contains("."))
                    {
                        double v = Double.Parse(snum);
                        return new Real(v);
                    }
                    else
                    {
                        int v = 0;
                        int sz = snum.Length;
                        for (int i = 0; i < sz; i++)
                        {
                            v = v * 10 + snum[i] - '0';
                        }
                        return new Integer(v);
                    }
                }
            }
            if (IsVariable(rt))
            {

                if (IsReserved(rt)) return new ErrorValue("保留字不能求值");
                Value val;
                bool rs = EnvironmentManager.LookupVariableValue(rt.Description(), out val, env);
                if (rs) return val;
                return new ErrorValue("未绑定变量(Unbound variable)");
            }
            return new ErrorValue("未知表达式类型(Unknown expression type)");
        }
        public Value Eval(NonLeafNode rt, Env env)
        {
            if (IsLambda(rt))
            {
                int sz = rt.Size();
                if (sz < 3) return new ErrorValue("函数不完整");
                Function func = new Function();
                NonLeafNode paramsNode = rt.At(1) as NonLeafNode;
                if (paramsNode == null) return new ErrorValue("参数定义语法不正确");
                int paramsNumber = paramsNode.Size();
                for (int i = 0; i < paramsNumber; i++)
                {
                    LeafNode idNode = paramsNode.At(i) as LeafNode;
                    if (idNode == null) return new ErrorValue("不是有效的标识符结构");
                    if (idNode.GetToken().Type != TokType.Identifier) return new ErrorValue("不是有效的标识符");
                    string name = idNode.GetToken().Text;
                    func.Parameters.Add(name);
                }
                func.Body = GetBody(rt);
                func.Env = env;
                return func;
            }
            if (IsIf(rt))
            {
                if (rt.Size() < 3) return new ErrorValue("语句不完整");
                if (rt.Size() > 4) return new ErrorValue("参数过多");
                Node ifPredicate = GetIfPredicate(rt);
                Value rs = ifPredicate.Eval(env);
                bool ok = true;
                if (rs is BooleanValue)
                {
                    if (((BooleanValue)rs).Boolean == false) ok = false;
                }
                if (ok)
                {
                    Node ifConsequent = GetIfConsequent(rt);
                    return ifConsequent.Eval(env);
                }
                else if (rt.Size() == 4)
                {
                    Node ifAlternative = GetIfAlternative(rt);
                    return ifAlternative.Eval(env);
                }
                return Value.NonValue;
            }
            if (IsDefinition(rt))
            {
                if (((NonLeafNode)rt).Size() != 3) return new ErrorValue("设置: 语法错误(bad syntax) 标识符后的表达式太多");
                string varName;
                bool rs = GetDefinitionVariable(rt, out varName);
                if (!rs) return new ErrorValue("语法错误(bad syntax)");
                if (IsReserved(varName)) return new ErrorValue("保留字不能作为名字");
                Value val = GetDefinitionValue(rt, env);
                if (val is ErrorValue) return val;
                env.DefineVariable(varName, val);
                return Value.NonValue;
            }
            if (IsApplication(rt))
            {
                Value procedureName = rt.At(0).Eval(env);
                if (procedureName is ErrorValue) return procedureName;
                Procedure proc = procedureName as Procedure;
                if (proc == null) return new ErrorValue("不是有效的函数名");
                ParametersList param = new ParametersList();
                int sz = rt.Size();
                for (int i = 1; i < sz; i++)
                {
                    Value p = rt.At(i).Eval(env);
                    if (p is ErrorValue) return p;
                    param.Add(p);
                }
                return proc.Run(param);
            }
            return new ErrorValue("未知表达式类型(Unknown expression type)");
        }
        public Value Eval(Node rt, Env env)
        {
            System.Console.WriteLine("Node");
            return new ErrorValue("未知表达式类型(Unknown expression type)");
        }

        #region 是XX吗？

        private Node GetBody(NonLeafNode exp)
        {
            return exp.At(2);
        }

        private bool IsTrue(LeafNode b)
        {
            if (b.GetToken().Text == "真") return true;
            return false;
        }

        private bool IsBoolean(LeafNode b)
        {
            if (b.GetToken().Text == "真" || b.GetToken().Text == "假") return true;
            return false;
        }

        private bool IsReserved(LeafNode exp)
        {
            string name = exp.GetToken().Text;
            if (Visiter.ReservedSet.Contains(name)) return true;
            return false;
        }
        private bool IsReserved(string name)
        {
            if (Visiter.ReservedSet.Contains(name)) return true;
            return false;
        }

        private static HashSet<string> ReservedSet = new HashSet<string> { "设置", "如果", "函数", "条件", "引用", "真", "假" };

        private bool GetDefinitionVariable(NonLeafNode exp,out string name)
        {
            if (IsDefineFunction(exp))
            {
                var funcName = ((NonLeafNode)(exp.At(1))).At(0);
                if (funcName.NotLeaf())
                {
                    name = "";
                    return false;
                }
                else
                {
                    var tok = ((LeafNode)funcName).GetToken();
                    name = tok.Text;
                    if (tok.Type != TokType.Identifier) return false;
                }
            }
            else
            {
                var tok = ((LeafNode)exp.At(1)).GetToken();
                name = tok.Text;
                if (tok.Type != TokType.Identifier) return false;
            }
            return true;
        }
        private Value GetDefinitionValue(NonLeafNode exp, Env env)
        {
            if (IsDefineFunction(exp))
            {
                return new ErrorValue("还没写！");
            }
            else
            {
                return exp.At(2).Eval(env);
            }
        }

        // 是define的函数吗
        private bool IsDefineFunction(NonLeafNode exp)
        {
            return exp.At(1).NotLeaf();
        }

        //是自求值表达式 数和字符串 吗
        private bool IsSelfEvaluating(Node exp)
        {
            if (exp.IsLeaf())
            {
                var tok = ((LeafNode)exp).GetToken();
                if (tok.Type == TokType.Value || tok.Type == TokType.String) return true;
            }
            return false;
        }

        //是变量吗
        private bool IsVariable(Node exp)
        {
            if (exp.IsLeaf())
            {
                var tok = ((LeafNode)exp).GetToken();
                if (tok.Type == TokType.Identifier) return true;
            }
            return false;
        }

        //表的开始是某个符号吗
        private bool TaggedListIs(Node exp, string tag)
        {
            if (exp.NotLeaf())
            {
                if (((NonLeafNode)exp).Size() > 0)
                {
                    Node id = ((NonLeafNode)exp).Car();
                    if (id.IsLeaf())
                    {
                        var tok = ((LeafNode)id).GetToken();
                        if (tok.Type == TokType.Identifier && tok.Text == tag) return true;
                    }
                }
            }
            return false;
        }

        //是引用吗
        private bool IsQuoted(Node exp)
        {
            return TaggedListIs(exp, "引用");
        }

        //是赋值吗
        private bool IsAssignment(Node exp)
        {
            return TaggedListIs(exp, "赋值");
        }

        //取得变量名

        //取得值

        //是定义吗
        private bool IsDefinition(Node exp)
        {
            return TaggedListIs(exp, "设置");
        }

        //取得定义的名字

        //定义的内容

        //是lambda表达式吗
        private bool IsLambda(Node exp)
        {
            return TaggedListIs(exp, "函数");
        }

        //参数们

        //函数体

        //构造一个函数

        //是如果吗
        private bool IsIf(Node exp)
        {
            return TaggedListIs(exp, "如果");
        }

        //条件
        private Node GetIfPredicate(NonLeafNode exp)
        {
            return exp.At(1);

        }
        //满足条件
        private Node GetIfConsequent(NonLeafNode exp)
        {
            return exp.At(2);
        }
        //不满足条件
        private Node GetIfAlternative(NonLeafNode exp)
        {
            return exp.At(3);
        }

        //是序列吗
        private bool IsBegin(Node exp)
        {
            return TaggedListIs(exp, "执行");
        }

        //是过程吗
        private bool IsApplication(Node exp)
        {
            return exp.NotLeaf() && ((NonLeafNode)exp).Size() > 0;
        }


        #endregion

    }

}
