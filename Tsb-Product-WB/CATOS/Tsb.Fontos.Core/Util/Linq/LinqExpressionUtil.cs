#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2009 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE          AUTHOR 		   REVISION    	
* 2010.11.24  Tonny.Kim  1.0  First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Tsb.Fontos.Core.Util.Linq
{
    public class LinqExpressionUtil
    {
        #region INITIALIZE AREA *************************************
        private const string ObjectID = "GNR-FTCO-UTL-LinqExpressionUtil";
        #endregion

        #region METHOD AREA *****************************************
        /// <summary>
        /// Gets Member in Linq Expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetMemberName(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var memberExpression = (MemberExpression)expression;
                    var supername = GetMemberName(memberExpression.Expression);
                    if (String.IsNullOrEmpty(supername)) return memberExpression.Member.Name;
                    return String.Concat(supername, '.', memberExpression.Member.Name);
                case ExpressionType.Call:
                    var callExpression = (MethodCallExpression)expression;
                    return callExpression.Method.Name;
                case ExpressionType.Convert:
                    var unaryExpression = (UnaryExpression)expression;
                    return GetMemberName(unaryExpression.Operand);
                case ExpressionType.Parameter:
                case ExpressionType.Constant: //Change 
                    return String.Empty;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets Member Type in Linq Expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static System.Type GetMemberType(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var memberExpression = (MemberExpression)expression;
                    return memberExpression.Type;
                case ExpressionType.Convert:
                    var unaryExpression = (UnaryExpression)expression;
                    return GetMemberType(unaryExpression.Operand);
                case ExpressionType.Parameter:
                case ExpressionType.Constant: //Change 
                    return null;
                default:
                    return null;
            }
        }
        #endregion
    }
}
