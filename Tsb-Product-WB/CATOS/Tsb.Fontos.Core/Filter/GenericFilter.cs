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
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Filter.Type;

namespace Tsb.Fontos.Core.Filter
{
    public static class GenericFilter<TEntity>
    {
        //private static Dictionary<string, IPropertyGenericFilter<TEntity>> _cache
        //    = new Dictionary<string, IPropertyGenericFilter<TEntity>>();

        #region METHOD AREA **************************************
        /// <summary>
        /// 람다식 생성
        /// </summary>
        /// <param name="propertyName">객체의 속성명</param>
        /// <param name="compareOp">>비교 방법</param>
        /// <param name="value">비교할 값</param>
        /// <returns>TEntity에 해당하는 비교 람다식</returns>
        public static Expression<Func<TEntity, bool>> Create(string propertyName, ConditionalOpType compareOp, object value)
        {
            Expression<Func<TEntity, bool>> expression = null;

            try
            {
                if (string.IsNullOrEmpty(propertyName))
                    //MSG_FTCO_00166 : Null or empty value: '{0}'
                    throw new ArgumentException(MessageBuilder.BuildMessage("MSG_FTCO_00166"
                        , DefaultMessage.NON_REG_WRD + "propertyName"));

                IPropertyGenericFilter<TEntity> propFilter;
                //if (!_cache.TryGetValue(propertyName, out propFilter))
                {
                    PropertyInfo pi = typeof(TEntity).GetProperty(propertyName);
                    if (pi == null)
                        //MSG_FTCO_00169 : Type '{0}' doesn't have property '{1}'.
                        throw new ArgumentException(MessageBuilder.BuildMessage("MSG_FTCO_00169"
                            , DefaultMessage.NON_REG_WRD + typeof(TEntity).Name
                            , DefaultMessage.NON_REG_WRD + propertyName));

                    System.Type[] genTypes = { typeof(TEntity), pi.PropertyType };
                    System.Type filterType = typeof(PropertyGenericFilter<,>).MakeGenericType(genTypes);
                    propFilter = (IPropertyGenericFilter<TEntity>)Activator.CreateInstance(filterType);

                    ParameterExpression p = Expression.Parameter(typeof(TEntity), "e");
                    LambdaExpression propRhs = Expression.Lambda(
                            Expression.MakeMemberAccess(p, pi),
                            p);
                    propFilter.WriteGetter(propRhs);

                    //_cache.Add(propertyName, propFilter);
                }

                SimpleWriter rwr = new SimpleWriter(propFilter.Expression);

                // replace compare op and value
                LambdaExpression opRhs = CompareOperatorDecoder.GetInstance().Decode(propFilter.PropertyType, compareOp);
                rwr.ApplyOnce(new Rule(propFilter.CompareOpLhs, opRhs));
                rwr.ApplyOnce(new Rule(propFilter.ValueLhs, EvaluateOperand.CreateRhs(propFilter.PropertyType, value)));

                expression = (Expression<Func<TEntity, bool>>)rwr.Expression; 
            }
            catch (Exception e)
            {
                throw e;
            }

            return expression;
        }
        #endregion
    }
}
