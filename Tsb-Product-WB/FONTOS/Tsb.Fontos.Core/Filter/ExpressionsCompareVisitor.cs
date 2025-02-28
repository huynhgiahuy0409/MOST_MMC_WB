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

namespace Tsb.Fontos.Core.Filter
{
    /// <summary>
    /// Compares two expressions up to renaming of lambda parameters. 
    /// Types are not compared (only in lambda param definitions). 
    /// </summary>
    public abstract class ExpressionsCompareVisitor : TsbBaseObject
    {
        #region FIELD AREA ***************************************
        // Maps Expr1.Param -> Expr2.Param
        private Dictionary<ParameterExpression, ParameterExpression> _paramDict;
        #endregion

        #region CONSTRUCTOR AREA *********************************
        protected ExpressionsCompareVisitor()
        {
            this.ObjectID = "GNR-FTDW-Find-ExpressionsCompareVisitor";

            _paramDict = new Dictionary<ParameterExpression, ParameterExpression>();
        }
        #endregion

        #region METHOD AREA **************************************
        protected bool IsInternalParameter(ParameterExpression p2)
        {
            return _paramDict.Values.Contains(p2);
        }

        //protected IDictionary<ParameterExpression, ParameterExpression> VarMapping {
        //    get { return _paramDict; }
        //}

        //protected virtual bool ExpressionsEqual( Expression exp, Expression exp2 ) {
        //    _paramDict = new Dictionary<ParameterExpression, ParameterExpression>();
        //    return this.Visit( exp, exp2 );
        //}

        //protected virtual bool ExpressionsEqual( Expression exp, Expression exp2,
        //    IEnumerable<ParameterExpression> boundVars ) {
        //    _paramDict = boundVars.ToDictionary( x => x, x => x );
        //    return this.Visit( exp, exp2 );
        //}
        #endregion

        #region VISIT METHOD AREA **************************************
        protected virtual bool Visit(Expression exp, Expression exp2)
        {
            if (exp2 == null)
                return exp == null;
            if (exp == null)
                return false;

            if (exp.NodeType != exp2.NodeType)
            {
                return false;
            }

            switch (exp.NodeType)
            {
                ////(-a) 같은 산술 부정 연산입니다. 현재 위치에서 a 개체가 수정되어서는 안 됩니다. 
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
                case ExpressionType.TypeAs: return this.VisitUnary((UnaryExpression)exp, (UnaryExpression)exp2);
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
                case ExpressionType.ExclusiveOr:
                    return this.VisitBinary((BinaryExpression)exp, (BinaryExpression)exp2);

                //C#의 obj is SampleType 또는 Visual Basic의 TypeOf obj is SampleType 같은 형식 테스트입니다.
                case ExpressionType.TypeIs:
                    return this.VisitTypeIs((TypeBinaryExpression)exp, (TypeBinaryExpression)exp2);

                //C#의 a > b ? a : b 또는 Visual Basic의 If(a > b, a, b) 같은 조건부 연산입니다.
                case ExpressionType.Conditional:
                    return this.VisitConditional((ConditionalExpression)exp, (ConditionalExpression)exp2);

                //상수 값입니다.
                case ExpressionType.Constant:
                    return this.VisitConstant((ConstantExpression)exp, (ConstantExpression)exp2);

                //식의 컨텍스트에 정의된 매개 변수 또는 변수에 대한 참조입니다. 자세한 내용은 ParameterExpression를 참조하십시오. 
                case ExpressionType.Parameter:
                    return this.VisitParameter((ParameterExpression)exp, (ParameterExpression)exp2);

                //obj.SampleProperty와 같이 필드 또는 속성에서 읽는 연산입니다.
                case ExpressionType.MemberAccess:
                    return this.VisitMemberAccess((MemberExpression)exp, (MemberExpression)exp2);

                //obj.sampleMethod() 식에서와 같은 메서드 호출입니다.
                case ExpressionType.Call:
                    return this.VisitMethodCall((MethodCallExpression)exp, (MethodCallExpression)exp2);

                //C#의 a => a + a 또는 Visual Basic의 Function(a) a + a 같은 람다 식입니다.
                case ExpressionType.Lambda:
                    return this.VisitLambda((LambdaExpression)exp, (LambdaExpression)exp2);

                //new SampleType()과 같이 생성자를 호출하여 새 개체를 만드는 연산입니다.
                case ExpressionType.New:
                    return this.VisitNew((NewExpression)exp, (NewExpression)exp2);

                //C#의 new SampleType[]{a, b, c} 또는 Visual Basic의 New SampleType(){a, b, c}과 같이 새 1차원 배열을 만들고 요소 목록을 사용하여 초기화하는 연산입니다.
                case ExpressionType.NewArrayInit:
                //C#의 new SampleType[dim1, dim2] 또는 Visual Basic의 New SampleType(dim1, dim2)과 같이 각 차원의 경계가 지정된 새 배열을 만드는 연산입니다.
                case ExpressionType.NewArrayBounds:
                    return this.VisitNewArray((NewArrayExpression)exp, (NewArrayExpression)exp2);

                //sampleDelegate.Invoke() 같은 대리자 또는 람다 식을 호출하는 연산입니다.
                case ExpressionType.Invoke:
                    return this.VisitInvocation((InvocationExpression)exp, (InvocationExpression)exp2);

                //C#의 new Point { X = 1, Y = 2 } 또는 Visual Basic의 New Point With {.X = 1, .Y = 2}와 같이 새 개체를 만들고 하나 이상의 멤버를 초기화하는 연산입니다.
                case ExpressionType.MemberInit:
                    return this.VisitMemberInit((MemberInitExpression)exp, (MemberInitExpression)exp2);

                //C#의 new List<SampleType>(){ a, b, c } 또는 Visual Basic의 Dim sampleList = { a, b, c }와 같이 새 IEnumerable 개체를 만들고 요소 목록을 사용하여 초기화하는 연산입니다.
                case ExpressionType.ListInit:
                    return this.VisitListInit((ListInitExpression)exp, (ListInitExpression)exp2);
                default:
                    //MSG_FTCO_00164 : Unhandled expression type: '{0}'
                    throw new Exception(MessageBuilder.BuildMessage("MSG_FTCO_00164"
                        , DefaultMessage.NON_REG_WRD + exp.NodeType));
                //throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
            }
        }

        protected virtual bool VisitBinding(MemberBinding binding, MemberBinding binding2)
        {
            if (binding.Member != binding2.Member)
                return false;

            switch (binding.BindingType)
            {
                //멤버를 식의 값으로 초기화하는 동작을 나타내는 바인딩입니다.
                case MemberBindingType.Assignment:
                    return this.VisitMemberAssignment((MemberAssignment)binding, (MemberAssignment)binding2);
                //멤버의 멤버를 재귀적으로 초기화하는 동작을 나타내는 바인딩입니다.
                case MemberBindingType.MemberBinding:
                    return this.VisitMemberMemberBinding((MemberMemberBinding)binding, (MemberMemberBinding)binding2);
                //요소 목록을 사용하여 IList 또는 ICollection<T> 형식의 멤버를 초기화하는 동작을 나타내는 바인딩입니다.
                case MemberBindingType.ListBinding:
                    return this.VisitMemberListBinding((MemberListBinding)binding, (MemberListBinding)binding2);
                default:
                    //MSG_FTCO_00165 : Unhandled binding type '{0}'
                    throw new Exception(MessageBuilder.BuildMessage("MSG_FTCO_00165"
                        , DefaultMessage.NON_REG_WRD + binding.BindingType));
                //throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
            }
        }

        protected virtual bool VisitElementInitializer(ElementInit initializer, ElementInit initializer2)
        {
            return initializer.AddMethod == initializer2.AddMethod
                && this.VisitExpressionList(initializer.Arguments, initializer2.Arguments);
        }

        protected virtual bool VisitUnary(UnaryExpression u, UnaryExpression u2)
        {
            return u.Method == u2.Method
                && u.IsLifted == u2.IsLifted
                && u.IsLiftedToNull == u2.IsLiftedToNull
                && this.Visit(u.Operand, u2.Operand);
        }

        protected virtual bool VisitBinary(BinaryExpression b, BinaryExpression b2)
        {
            return b.Method == b2.Method
                && b.IsLifted == b2.IsLifted
                && b.IsLiftedToNull == b2.IsLiftedToNull
                && this.Visit(b.Left, b2.Left)
                && this.Visit(b.Right, b2.Right)
                && this.Visit(b.Conversion, b2.Conversion);
        }

        protected virtual bool VisitTypeIs(TypeBinaryExpression b, TypeBinaryExpression b2)
        {
            return b.TypeOperand == b2.TypeOperand
                && this.Visit(b.Expression, b2.Expression);
        }

        protected virtual bool VisitConstant(ConstantExpression c, ConstantExpression c2)
        {
            /* c.Type == c2.Type && */
            return object.Equals(c.Value, c2.Value);
        }

        protected virtual bool VisitConditional(ConditionalExpression c, ConditionalExpression c2)
        {
            return this.Visit(c.Test, c2.Test)
                && this.Visit(c.IfTrue, c2.IfTrue)
                && this.Visit(c.IfFalse, c2.IfFalse);
        }

        protected virtual bool VisitParameter(ParameterExpression p, ParameterExpression p2)
        {
            // if two sub-trees of the same expressions are compared. they may have equal 
            //  parameters (first disjunct). 
            return p == p2 || p2 == _paramDict[p];
        }

        protected virtual bool VisitMemberAccess(MemberExpression m, MemberExpression m2)
        {
            return m.Member == m2.Member
                && this.Visit(m.Expression, m2.Expression);
        }

        protected virtual bool VisitMethodCall(MethodCallExpression m, MethodCallExpression m2)
        {
            return m.Method == m2.Method
                && this.Visit(m.Object, m2.Object)
                && this.VisitExpressionList(m.Arguments, m2.Arguments);
        }

        protected virtual bool VisitExpressionList(ReadOnlyCollection<Expression> original, ReadOnlyCollection<Expression> original2)
        {
            if (original.Count != original2.Count)
            {
                return false;
            }

            for (int i = 0, n = original.Count; i < n; i++)
            {
                if (!this.Visit(original[i], original2[i]))
                    return false;
            }
            return true;
        }

        protected virtual bool VisitMemberAssignment(MemberAssignment assignment, MemberAssignment assignment2)
        {
            return assignment.Member == assignment2.Member
             && this.Visit(assignment.Expression, assignment2.Expression);
        }

        protected virtual bool VisitMemberMemberBinding(MemberMemberBinding binding, MemberMemberBinding binding2)
        {
            return binding.Member == binding2.Member
                && this.VisitBindingList(binding.Bindings, binding2.Bindings);
        }

        protected virtual bool VisitMemberListBinding(MemberListBinding binding, MemberListBinding binding2)
        {
            return binding.Member == binding2.Member
                && this.VisitElementInitializerList(binding.Initializers, binding2.Initializers);
        }

        protected virtual bool VisitBindingList(ReadOnlyCollection<MemberBinding> original, ReadOnlyCollection<MemberBinding> original2)
        {
            if (original.Count != original2.Count)
                return false;

            for (int i = 0, n = original.Count; i < n; i++)
            {
                if (!this.VisitBinding(original[i], original2[i]))
                    return false;
            }
            return true;
        }

        protected virtual bool VisitElementInitializerList(ReadOnlyCollection<ElementInit> original, ReadOnlyCollection<ElementInit> original2)
        {
            if (original.Count != original2.Count)
                return false;
            for (int i = 0, n = original.Count; i < n; i++)
            {
                if (!this.VisitElementInitializer(original[i], original2[i]))
                    return false;
            }
            return true;
        }

        protected virtual bool VisitLambda(LambdaExpression lambda, LambdaExpression lambda2)
        {
            bool ret = this.VisitParameterList(lambda.Parameters, lambda2.Parameters);
            if (ret)
                ret = this.Visit(lambda.Body, lambda2.Body);
            // if VisitParameterList() failed, not all params are in the dict
            foreach (var p in lambda.Parameters)
            {
                if (_paramDict.ContainsKey(p))
                    _paramDict.Remove(p);
            }
            return ret;
        }

        protected virtual bool VisitParameterList(ReadOnlyCollection<ParameterExpression> original, ReadOnlyCollection<ParameterExpression> original2)
        {
            if (original.Count != original2.Count)
                return false;
            for (int i = 0, n = original.Count; i < n; i++)
            {
                if (original[i].Type != original2[i].Type)
                    return false;
                _paramDict.Add(original[i], original2[i]);
            }
            return true;
        }

        protected virtual bool VisitNew(NewExpression nex, NewExpression nex2)
        {
            return nex.Constructor == nex2.Constructor
                && this.VisitMembersOfNew(nex.Members, nex2.Members)
                && this.VisitExpressionList(nex.Arguments, nex2.Arguments);
        }

        protected virtual bool VisitMembersOfNew(ReadOnlyCollection<MemberInfo> original, ReadOnlyCollection<MemberInfo> original2)
        {
            if (original.Count != original2.Count)
                return false;
            for (int i = 0, n = original.Count; i < n; i++)
            {
                if (original[i] != original2[i])
                    return false;
            }
            return true;
        }

        protected virtual bool VisitMemberInit(MemberInitExpression init, MemberInitExpression init2)
        {
            return this.VisitNew(init.NewExpression, init2.NewExpression)
                && this.VisitBindingList(init.Bindings, init2.Bindings);
        }

        protected virtual bool VisitListInit(ListInitExpression init, ListInitExpression init2)
        {
            return this.VisitNew(init.NewExpression, init2.NewExpression)
                && this.VisitElementInitializerList(init.Initializers, init2.Initializers);
        }

        protected virtual bool VisitNewArray(NewArrayExpression na, NewArrayExpression na2)
        {
            return na.Type == na2.Type
                && this.VisitExpressionList(na.Expressions, na2.Expressions);
        }

        protected virtual bool VisitInvocation(InvocationExpression iv, InvocationExpression iv2)
        {
            return this.VisitExpressionList(iv.Arguments, iv2.Arguments)
                && this.Visit(iv.Expression, iv2.Expression);
        }
        #endregion

    }
}
