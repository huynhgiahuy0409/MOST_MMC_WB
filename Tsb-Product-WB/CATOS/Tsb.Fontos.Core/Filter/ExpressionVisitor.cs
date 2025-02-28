#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2011 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
* 
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		REVISION    	
* 2011.05.06    Jindols 1.0	First release.
*
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Filter
{
    public abstract class ExpressionVisitor : TsbBaseObject
    {
        #region CONSTRUCTOR AREA *********************************
        protected ExpressionVisitor()
        {
            this.ObjectID = "GNR-FTDW-Find-ExpressionVisitor";
        }
        #endregion

        #region VISIT METHOD AREA **************************************
        protected virtual Expression Visit(Expression exp)
        {
            if (exp == null) return exp;
            switch (exp.NodeType)
            {
                //(-a) 같은 산술 부정 연산입니다. 현재 위치에서 a 개체가 수정되어서는 안 됩니다. 
                case ExpressionType.Negate:
                //(-a)와 같이 오버플로를 검사하는 산술 부정 연산입니다. 현재 위치에서 a 개체가 수정되어서는 안 됩니다. 
                case ExpressionType.NegateChecked:
                //비트 보수 또는 논리 부정 연산입니다. C#에서는 정수 형식의 (~a) 및 부울 값의 (!a)와 동일하고, Visual Basic에서는 (Not a)와 동일합니다. 현재 위치에서 a 개체가 수정되어서는 안 됩니다. 
                case ExpressionType.Not:
                //C#의 (SampleType)obj 또는 Visual Basic의 CType(obj, SampleType)과 같은 캐스팅 또는 변환 연산입니다. 숫자 변환의 경우 변환된 값이 대상 형식에 비해 너무 크면 예외가 throw되지 않습니다. 
                case ExpressionType.Convert:
                //C#의 (SampleType)obj 또는 Visual Basic의 CType(obj, SampleType)과 같은 캐스팅 또는 변환 연산입니다. 숫자 변환의 경우 변환된 값이 대상 형식에 맞지 않으면 예외가 throw됩니다. 
                case ExpressionType.ConvertChecked:
                //array.Length 같은 1차원 배열의 길이를 가져오는 연산입니다.
                case ExpressionType.ArrayLength:
                //Expression 형식의 상수 값이 있는 식입니다. Quote 노드에는 표현된 식의 컨텍스트에 정의된 매개 변수에 대한 참조가 포함될 수 있습니다. 
                case ExpressionType.Quote:
                //C#의 (obj as SampleType) 또는 Visual Basic의 TryCast(obj, SampleType)와 같이 변환에 실패하면 null이 제공되는 boxing 변환이나 명시적 참조입니다.
                case ExpressionType.TypeAs: return this.VisitUnary((UnaryExpression)exp);

                //a + b와 같이 숫자 피연산자에 대해 오버플로를 검사하지 않는 더하기 연산입니다.
                case ExpressionType.Add:
                //(a + b)와 같이 숫자 피연산자에 대해 오버플로를 검사하는 더하기 연산입니다.
                case ExpressionType.AddChecked:
                //(a - b)와 같이 숫자 피연산자에 대해 오버플로를 검사하지 않는 빼기 연산입니다.
                case ExpressionType.Subtract:
                //(a - b)와 같이 숫자 피연산자에 대해 오버플로를 검사하는 산술 빼기 연산입니다. 
                case ExpressionType.SubtractChecked:
                //(a * b)와 같이 숫자 피연산자에 대해 오버플로를 검사하지 않는 곱하기 연산입니다.
                case ExpressionType.Multiply:
                //(a * b)와 같이 숫자 피연산자에 대해 오버플로를 검사하는 곱하기 연산입니다.
                case ExpressionType.MultiplyChecked:
                //(a / b)와 같이 숫자 피연산자에 대한 나누기 연산입니다.
                case ExpressionType.Divide:
                //C#의 (a % b) 또는 Visual Basic의 (a Mod b) 같은 산술 나머지 연산입니다.
                case ExpressionType.Modulo:
                //C#의 (a & b) 및 Visual Basic의 (a And b) 같은 비트 또는 논리 AND 연산입니다.
                case ExpressionType.And:
                //첫 번째 피연산자가 true로 계산되는 경우에만 두 번째 피연산자를 계산하는 조건부 AND 연산입니다. C#의 (a && b)와 Visual Basic의 (a AndAlso b)에 해당합니다. 
                case ExpressionType.AndAlso:
                //C#의 (a | b) 또는 Visual Basic의 (a Or b) 같은 비트 또는 논리 OR 연산입니다.
                case ExpressionType.Or:
                //C#의 (a || b) 또는 Visual Basic의 (a OrElse b) 같은 단락(short circuit) 조건부 OR 연산입니다.
                case ExpressionType.OrElse:
                //(a < b) 같은 "보다 큼" 비교입니다.
                case ExpressionType.LessThan:
                //(a <= b) 같은 "보다 작거나 같음" 비교입니다.
                case ExpressionType.LessThanOrEqual:
                //(a > b) 같은 "보다 큼" 비교입니다.
                case ExpressionType.GreaterThan:
                //(a >= b) 같은 "보다 크거나 같음" 비교입니다. 
                case ExpressionType.GreaterThanOrEqual:
                //C#의 (a == b) 또는 Visual Basic의 (a = b) 같은 같음 비교를 나타내는 노드입니다.
                case ExpressionType.Equal:
                //#의 (a != b) 또는 Visual Basic의 (a <> b) 같은 다름 비교입니다.
                case ExpressionType.NotEqual:
                //C#의 (a ?? b) 또는 Visual Basic의 If(a, b) 같은 null 결합 연산을 나타내는 노드입니다.
                case ExpressionType.Coalesce:
                //C#의 array[index] 또는 Visual Basic의 array(index) 같은 1차원 배열의 인덱싱 연산입니다.
                case ExpressionType.ArrayIndex:
                //a >> b) 같은 비트 오른쪽 시프트 연산입니다.
                case ExpressionType.RightShift:
                //(a << b) 같은 비트 왼쪽 시프트 연산입니다.
                case ExpressionType.LeftShift:
                //C#의 (a ^ b) 또는 Visual Basic의 (a Xor b) 같은 비트 또는 논리 XOR 연산입니다.
                case ExpressionType.ExclusiveOr: return this.VisitBinary((BinaryExpression)exp);

                //C#의 obj is SampleType 또는 Visual Basic의 TypeOf obj is SampleType 같은 형식 테스트입니다.
                case ExpressionType.TypeIs: return this.VisitTypeIs((TypeBinaryExpression)exp);

                //C#의 a > b ? a : b 또는 Visual Basic의 If(a > b, a, b) 같은 조건부 연산입니다.
                case ExpressionType.Conditional: return this.VisitConditional((ConditionalExpression)exp);

                //상수 값입니다.
                case ExpressionType.Constant: return this.VisitConstant((ConstantExpression)exp);

                //식의 컨텍스트에 정의된 매개 변수 또는 변수에 대한 참조입니다. 자세한 내용은 ParameterExpression를 참조하십시오. 
                case ExpressionType.Parameter: return this.VisitParameter((ParameterExpression)exp);

                //obj.SampleProperty와 같이 필드 또는 속성에서 읽는 연산입니다.
                case ExpressionType.MemberAccess: return this.VisitMemberAccess((MemberExpression)exp);

                //obj.sampleMethod() 식에서와 같은 메서드 호출입니다.
                case ExpressionType.Call: return this.VisitMethodCall((MethodCallExpression)exp);

                //C#의 a => a + a 또는 Visual Basic의 Function(a) a + a 같은 람다 식입니다.
                case ExpressionType.Lambda: return this.VisitLambda((LambdaExpression)exp);

                //new SampleType()과 같이 생성자를 호출하여 새 개체를 만드는 연산입니다.
                case ExpressionType.New: return this.VisitNew((NewExpression)exp);

                //C#의 new SampleType[]{a, b, c} 또는 Visual Basic의 New SampleType(){a, b, c}과 같이 새 1차원 배열을 만들고 요소 목록을 사용하여 초기화하는 연산입니다.
                case ExpressionType.NewArrayInit:
                
                case ExpressionType.NewArrayBounds: return this.VisitNewArray((NewArrayExpression)exp);

                //sampleDelegate.Invoke() 같은 대리자 또는 람다 식을 호출하는 연산입니다.
                case ExpressionType.Invoke: return this.VisitInvocation((InvocationExpression)exp);

                //C#의 new Point { X = 1, Y = 2 } 또는 Visual Basic의 New Point With {.X = 1, .Y = 2}와 같이 새 개체를 만들고 하나 이상의 멤버를 초기화하는 연산입니다.
                case ExpressionType.MemberInit: return this.VisitMemberInit((MemberInitExpression)exp);

                //C#의 new List<SampleType>(){ a, b, c } 또는 Visual Basic의 Dim sampleList = { a, b, c }와 같이 새 IEnumerable 개체를 만들고 요소 목록을 사용하여 초기화하는 연산입니다.
                case ExpressionType.ListInit: return this.VisitListInit((ListInitExpression)exp);

                default:
                    //MSG_FTCO_00164 : Unhandled expression type: '{0}'
                    throw new Exception(MessageBuilder.BuildMessage("MSG_FTCO_00164"
                        , DefaultMessage.NON_REG_WRD + exp.NodeType));
                    //throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
            }
        }

        protected virtual MemberBinding VisitBinding(MemberBinding binding)
        {
            switch (binding.BindingType)
            {
                //멤버를 식의 값으로 초기화하는 동작을 나타내는 바인딩입니다.
                case MemberBindingType.Assignment:
                    return this.VisitMemberAssignment((MemberAssignment)binding);
                //멤버의 멤버를 재귀적으로 초기화하는 동작을 나타내는 바인딩입니다.
                case MemberBindingType.MemberBinding:
                    return this.VisitMemberMemberBinding((MemberMemberBinding)binding);
                //요소 목록을 사용하여 IList 또는 ICollection<T> 형식의 멤버를 초기화하는 동작을 나타내는 바인딩입니다.
                case MemberBindingType.ListBinding:
                    return this.VisitMemberListBinding((MemberListBinding)binding);
                default:
                    //MSG_FTCO_00165 : Unhandled binding type '{0}'
                    throw new Exception(MessageBuilder.BuildMessage("MSG_FTCO_00165"
                        , DefaultMessage.NON_REG_WRD + binding.BindingType));
                    //throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
            }
        }

        /// <summary>
        /// 지정된 IEnumerable<T>을 두 번째 인수로 사용하여 ElementInit를 만듭니다.
        /// </summary>
        /// <param name="initializer"></param>
        /// <returns></returns>
        protected virtual ElementInit VisitElementInitializer(ElementInit initializer)
        {
            ReadOnlyCollection<Expression> arguments = this.VisitExpressionList(initializer.Arguments);
            if (arguments != initializer.Arguments)
            {
                return Expression.ElementInit(initializer.AddMethod, arguments);
            }
            return initializer;
        }

        /// <summary>
        /// 단항 연산자가 있는 식을 만듭니다.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        protected virtual Expression VisitUnary(UnaryExpression u)
        {
            Expression operand = this.Visit(u.Operand);
            if (operand != u.Operand)
            {
                return Expression.MakeUnary(u.NodeType, operand, u.Type, u.Method);
            }
            return u;
        }
        /// <summary>
        /// 결합 연산자가 있는 식을 만듭니다.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        protected virtual Expression VisitBinary(BinaryExpression b)
        {
            Expression left = this.Visit(b.Left);
            Expression right = this.Visit(b.Right);
            Expression conversion = this.Visit(b.Conversion);

            try
            {
                if (left != b.Left || right != b.Right || conversion != b.Conversion)
                {
                    if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
                        //결합 연산을 나타내는 BinaryExpression을 만듭니다.
                        return Expression.Coalesce(left, right, conversion as LambdaExpression);
                    else
                        //지정된 왼쪽 및 오른쪽 피연산자를 사용하고 적절한 팩터리 메서드를 호출하여 BinaryExpression을 만듭니다.
                        return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return b;
        }
        /// <summary>
        /// TypeBinaryExpression을 만듭니다.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        protected virtual Expression VisitTypeIs(TypeBinaryExpression b)
        {
            Expression expr = this.Visit(b.Expression);
            if (expr != b.Expression)
            {
                return Expression.TypeIs(expr, b.TypeOperand);
            }
            return b;
        }
        /// <summary>
        /// 상수 값을 만듭니다.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        protected virtual Expression VisitConstant(ConstantExpression c)
        {
            return c;
        }
        /// <summary>
        /// 조건문을 나타내는 ConditionalExpression을 만듭니다.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        protected virtual Expression VisitConditional(ConditionalExpression c)
        {
            Expression test = this.Visit(c.Test);
            Expression ifTrue = this.Visit(c.IfTrue);
            Expression ifFalse = this.Visit(c.IfFalse);
            if (test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse)
            {
                return Expression.Condition(test, ifTrue, ifFalse);
            }
            return c;
        }
        /// <summary>
        /// 매개 변수 식을 만듭니다.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected virtual Expression VisitParameter(ParameterExpression p)
        {
            return p;
        }
        /// <summary>
        /// 필드 또는 속성 액세스를 나타내는 MemberExpression을 만듭니다.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected virtual Expression VisitMemberAccess(MemberExpression m)
        {
            Expression exp = this.Visit(m.Expression);
            if (exp != m.Expression)
            {
                return Expression.MakeMemberAccess(exp, m.Member);
            }
            return m;
        }
        /// <summary>
        /// 인수를 받는 메서드에 대한 호출을 나타내는 MethodCallExpression을 만듭니다.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected virtual Expression VisitMethodCall(MethodCallExpression m)
        {
            Expression obj = this.Visit(m.Object);
            IEnumerable<Expression> args = this.VisitExpressionList(m.Arguments);
            if (obj != m.Object || args != m.Arguments)
            {
                return Expression.Call(obj, m.Method, args);
            }
            return m;
        }

        protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
        {
            List<Expression> list = null;

            try
            {
                for (int i = 0, n = original.Count; i < n; i++)
                {
                    Expression p = this.Visit(original[i]);
                    if (list != null)
                    {
                        list.Add(p);
                    }
                    else if (p != original[i])
                    {
                        list = new List<Expression>(n);
                        for (int j = 0; j < i; j++)
                        {
                            list.Add(original[j]);
                        }
                        list.Add(p);
                    }
                }
                if (list != null)
                {
                    return list.AsReadOnly();
                }
            }catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return original;
        }

        /// <summary>
        /// 멤버를 식의 값으로 초기화하는 동작을 나타내는 바인딩
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
        {
            Expression e = this.Visit(assignment.Expression);

            try
            {
                if (e != assignment.Expression)
                {
                    return Expression.Bind(assignment.Member, e);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return assignment;
        }

        /// <summary>
        /// 멤버의 멤버를 재귀적으로 초기화하는 동작을 나타내는 바인딩
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
        {
            IEnumerable<MemberBinding> bindings = this.VisitBindingList(binding.Bindings);
            if (bindings != binding.Bindings)
            {
                return Expression.MemberBind(binding.Member, bindings);
            }
            return binding;
        }
        /// <summary>
        /// 요소 목록을 사용하여 IList 또는 ICollection<T> 형식의 멤버를 초기화하는 동작을 나타내는 바인딩
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
        {
            IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(binding.Initializers);
            if (initializers != binding.Initializers)
            {
                return Expression.ListBind(binding.Member, initializers);
            }
            return binding;
        }

        protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
        {
            List<MemberBinding> list = null;

            try
            {
                for (int i = 0, n = original.Count; i < n; i++)
                {
                    MemberBinding b = this.VisitBinding(original[i]);
                    if (list != null)
                    {
                        list.Add(b);
                    }
                    else if (b != original[i])
                    {
                        list = new List<MemberBinding>(n);
                        for (int j = 0; j < i; j++)
                        {
                            list.Add(original[j]);
                        }
                        list.Add(b);
                    }
                }
                if (list != null)
                    return list;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return original;
        }

        protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
        {            
            List<ElementInit> list = null;

            try
            {
                for (int i = 0, n = original.Count; i < n; i++)
                {
                    ElementInit init = this.VisitElementInitializer(original[i]);
                    if (list != null)
                    {
                        list.Add(init);
                    }
                    else if (init != original[i])
                    {
                        list = new List<ElementInit>(n);
                        for (int j = 0; j < i; j++)
                        {
                            list.Add(original[j]);
                        }
                        list.Add(init);
                    }
                }
                if (list != null)
                    return list;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return original;
        }
        /// <summary>
        /// 먼저 대리자 형식을 생성하여 LambdaExpression을 만듭니다. 
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        protected virtual Expression VisitLambda(LambdaExpression lambda)
        {
            Expression body = this.Visit(lambda.Body);
            if (body != lambda.Body)
            {
                return Expression.Lambda(lambda.Type, body, lambda.Parameters);
            }
            return lambda;
        }
        /// <summary>
        /// NewExpression을 만듭니다. 
        /// </summary>
        /// <param name="nex"></param>
        /// <returns></returns>
        protected virtual NewExpression VisitNew(NewExpression nex)
        {
            IEnumerable<Expression> args = this.VisitExpressionList(nex.Arguments);
            if (args != nex.Arguments)
            {
                if (nex.Members != null)
                    //지정된 인수를 사용하여 지정된 생성자를 호출하는 동작을 나타내는 NewExpression을 만듭니다. 생성자에서 초기화되는 필드에 액세스하는 멤버가 지정됩니다. 
                    return Expression.New(nex.Constructor, args, nex.Members);
                else
                    //지정된 인수를 사용하여 지정된 생성자를 호출하는 동작을 나타내는 NewExpression을 만듭니다.
                    return Expression.New(nex.Constructor, args);
            }
            return nex;
        }
        /// <summary>
        /// 새 개체를 만들고 개체의 속성을 초기화하는 식을 만듭니다. 
        /// </summary>
        /// <param name="init"></param>
        /// <returns></returns>
        protected virtual Expression VisitMemberInit(MemberInitExpression init)
        {
            NewExpression n = this.VisitNew(init.NewExpression);
            IEnumerable<MemberBinding> bindings = this.VisitBindingList(init.Bindings);
            if (n != init.NewExpression || bindings != init.Bindings)
            {
                return Expression.MemberInit(n, bindings);
            }
            return init;
        }
        /// <summary>
        /// C#의 new List<SampleType>(){ a, b, c } 또는 Visual Basic의 Dim sampleList = { a, b, c }와 같이 새 IEnumerable 개체를 만들고 요소 목록을 사용하여 초기화하는 연산입니다.
        /// </summary>
        /// <param name="init"></param>
        /// <returns></returns>
        protected virtual Expression VisitListInit(ListInitExpression init)
        {
            NewExpression n = this.VisitNew(init.NewExpression);
            IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(init.Initializers);
            if (n != init.NewExpression || initializers != init.Initializers)
            {
                return Expression.ListInit(n, initializers);
            }
            return init;
        }
        /// <summary>
        /// C#의 new SampleType[dim1, dim2] 또는 Visual Basic의 New SampleType(dim1, dim2)과 같이 각 차원의 경계가 지정된 새 배열을 만드는 연산입니다.
        /// </summary>
        /// <param name="na"></param>
        /// <returns></returns>
        protected virtual Expression VisitNewArray(NewArrayExpression na)
        {
            IEnumerable<Expression> exprs = this.VisitExpressionList(na.Expressions);
            if (exprs != na.Expressions)
            {
                if (na.NodeType == ExpressionType.NewArrayInit)
                {
                    //1차원 배열을 만들고 요소 목록으로 초기화하는 동작을 나타내는 NewArrayExpression을 만듭니다.
                    return Expression.NewArrayInit(na.Type.GetElementType(), exprs);
                }
                else
                {
                    //지정된 차수의 배열을 만드는 동작을 나타내는 NewArrayExpression을 만듭니다.
                    return Expression.NewArrayBounds(na.Type.GetElementType(), exprs);
                }
            }
            return na;
        }
        /// <summary>
        /// sampleDelegate.Invoke() 같은 대리자 또는 람다 식을 호출하는 연산입니다.
        /// </summary>
        /// <param name="iv"></param>
        /// <returns></returns>
        protected virtual Expression VisitInvocation(InvocationExpression iv)
        {
            IEnumerable<Expression> args = this.VisitExpressionList(iv.Arguments);
            Expression expr = this.Visit(iv.Expression);
            if (args != iv.Arguments || expr != iv.Expression)
            {
                //인수 식 목록에 대리자 또는 람다 식을 적용하는 InvocationExpression을 만듭니다.
                return Expression.Invoke(expr, args);
            }
            return iv;
        }
        #endregion

        
    }
}
