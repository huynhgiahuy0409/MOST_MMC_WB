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
namespace Tsb.Fontos.Core.Filter
{
    public static class EvaluateOperand
    {
        #region METHOD AREA **************************************
        public static LambdaExpression CreateRhs(System.Type type, object value)
        {
            //Value 속성이 지정된 값으로 설정된 ConstantExpression을 만듭니다. 
            ConstantExpression cex = Expression.Constant(value);
            LambdaExpression rhs;

            if (type != cex.Type)
            {
                //형식 변환 연산을 나타내는 UnaryExpression을 만들고 이값을 LambdaExpression을 만듭니다.
                rhs = Expression.Lambda(Expression.Convert(cex, type));
            }
            else
            {
                //먼저 대리자 형식을 생성하여 LambdaExpression을 만듭니다.
                rhs = Expression.Lambda(cex);
            }
            
            return rhs;
        }

        //public static Expression<Func<VType>> CreateRhs<VType>(VType value)
        //{
        //    ConstantExpression cex = Expression.Constant(value);
        //    return (Expression<Func<VType>>)Expression.Lambda(cex);
        //}
        #endregion

    }
}
