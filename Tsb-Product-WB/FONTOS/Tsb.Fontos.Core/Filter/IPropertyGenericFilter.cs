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
    public interface IPropertyGenericFilter<TEntity>
    {
        /// <summary>
        /// 비교 람다식
        /// </summary>
        Expression<Func<TEntity, bool>> Expression { get; }
        /// <summary>
        /// 비교 람다식 생성
        /// </summary>
        /// <param name="rhs"></param>
        void WriteGetter(LambdaExpression rhs);
        /// <summary>
        /// 비교 표현식
        /// </summary>
        LambdaExpression CompareOpLhs { get; }
        /// <summary>
        /// Value 표현식
        /// </summary>
        LambdaExpression ValueLhs { get; }
        /// <summary>
        /// 비교 속성 타입
        /// </summary>
        System.Type PropertyType { get; }
    }
}
