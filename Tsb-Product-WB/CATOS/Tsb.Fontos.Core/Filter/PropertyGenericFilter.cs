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
    /// 속성에 대한 람다식 표현 정보
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class PropertyGenericFilter<TEntity, TValue> : TsbBaseObject, IPropertyGenericFilter<TEntity>
    {
        #region FIELD AREA ***************************************
        private Expression<Func<TEntity, bool>> _expr;
        private Expression<Func<TEntity, TValue>> _propertyLhs;
        private Expression<Func<TValue, TValue, bool>> _compareOpLhs;
        private Expression<Func<TValue>> _valueLhs;
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public PropertyGenericFilter()
        {
            this.ObjectID = "GNR-FTDW-Find-PropertyGenericFilter";

            Func<TValue, TValue, bool> op = null;
            Func<TEntity, TValue> prop = null;
            TValue value = default(TValue);

            _compareOpLhs = (x, y) => op(x, y);
            _propertyLhs = e => prop(e);
            _valueLhs = () => value;
            _expr = e => op(prop(e), value);
        }
        #endregion


        #region IPropertyGenericBinFilter<TEntity> Members *********************************
        public Expression<Func<TEntity, bool>> Expression
        {
            get { return _expr; }
        }

        public void WriteGetter(LambdaExpression rhs)
        {
            _expr = SimpleWriter.ApplyOnce(_expr,
                Rule.Create(_propertyLhs, (Expression<Func<TEntity, TValue>>)rhs));
        }

        public LambdaExpression CompareOpLhs
        {
            get { return _compareOpLhs; }
        }

        public LambdaExpression ValueLhs
        {
            get { return _valueLhs; }
        }
        public System.Type PropertyType
        {
            get { return typeof(TValue); }
        }

        #endregion
    }
}
