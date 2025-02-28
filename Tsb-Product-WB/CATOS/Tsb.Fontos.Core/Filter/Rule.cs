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

namespace Tsb.Fontos.Core.Filter
{
    /// <summary>
    /// 람다식 변환 규칙 정보
    /// left-hand-side  ==> right-hand-side 로의 식 변환을 위한 규칙
    /// p( s1, s2) --> s1.StartsWith( s2 )
    /// </summary>
    public class Rule : TsbBaseObject
    {
        #region FIELD AREA ***************************************
        /// <summary>
        /// left-hand-side 
        /// </summary>
        private LambdaExpression _lhs;
        /// <summary>
        /// right-hand-side 
        /// </summary>
        private LambdaExpression _rhs;
        #endregion

        #region PROPERTY AREA ************************************
        public LambdaExpression Lhs
        {
            get { return _lhs; }
        }
        public LambdaExpression Rhs
        {
            get { return _rhs; }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public Rule(LambdaExpression lhs, LambdaExpression rhs)
        {
            this.ObjectID = "GNR-FTDW-Find-Rule";

            if (lhs == null)
                throw new ArgumentNullException("lhs");
            if (rhs == null)
                throw new ArgumentNullException("rhs");
            //if (lhs.Type != rhs.Type)
            //{
            //    throw new ArgumentException(
            //        string.Format(
            //        "Rhs type {0} is not equal to Lhs type {1}.",
            //        rhs.Type.Name, lhs.Type.Name));
            //}
            _lhs = lhs;
            _rhs = rhs;
        }
        #endregion


        #region CREATE METHOD AREA **************************************
                public static Rule Create<TResult>(Expression<Func<TResult>> lhs, Expression<Func<TResult>> rhs)
        {
            return new Rule(lhs, rhs);
        }
        public static Rule Create<TArg, TResult>(Expression<Func<TArg, TResult>> lhs, Expression<Func<TArg, TResult>> rhs)
        {
            return new Rule(lhs, rhs);
        }
        #endregion
    }
}
