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
            System.Console.WriteLine("LeafNode");
         
            if (IsVariable(rt))
            {
                Value val;
                bool rs = EnvironmentManager.LookupVariableValue(rt.Description(), out val, env);
                if (rs) return val;
                
            }
            return Value.NonValue;
        }
        public Value Eval(NonLeafNode rt, Env env)
        {
            System.Console.WriteLine("NonLeafNode");
            return Value.NonValue;
        }
        public Value Eval(Node rt, Env env)
        {
            System.Console.WriteLine("Node");
            return Value.NonValue;
        }

        #region 是XX吗？

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

        //满足条件

        //不满足条件

        //是序列吗
        private bool IsBegin(Node exp)
        {
            return TaggedListIs(exp, "执行");
        }

        //是过程吗
        private bool IsApplication(Node exp)
        {
            return exp.NotLeaf();
        }


        #endregion

    }

}
