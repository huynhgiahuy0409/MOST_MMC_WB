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
using System.Text;
using System.Linq.Expressions;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Filter.Item;
using Tsb.Fontos.Core.Filter.Type;

namespace Tsb.Fontos.Core.Filter
{
    public class FilterItemHandler : TsbBaseObject
    {
        #region FIELD AREA ***************************************
        public const string RULE_PARSER_METHOD_NAME = "ParseRules";
        public const string RULE_PARSER_DELEGATE_METHOD_NAME = "RuleParseDelegate";
        /// <summary>
        /// 수행될 filter 정보
        /// </summary>
        IFilterItem _filterItem;
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public FilterItemHandler(IFilterItem filterItem)
        {
            this.ObjectID = "GNR-FTDW-Find-FilterItemHandler";

            _filterItem = filterItem;
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// 입력된 fiter 정보에 해당하는 람다식을 반환한다.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Expression<Func<TEntity, bool>> GetLamdaExpression<TEntity>()
        {
            Expression<Func<TEntity, bool>> expression = null;
            try
            {
                if (_filterItem is FilterItem)
                {
                    FilterItem filterItem = _filterItem as FilterItem;
                    //FilterItem에 해당하는 비교식 생성
                    expression = MakeGerericFilter<TEntity>(filterItem.PropertyName, filterItem.OperatorType, filterItem.Operand);
                }
                else
                {
                    //group filter
                    expression = this.GetLamdaGroupExpression<TEntity>(_filterItem as FilterGroup); 
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return expression;
        }
        /// <summary>
        /// 입력된 fiter에서 FilterGroup에 해당하는 람다식을 반환한다.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="itemGroup"></param>
        /// <returns></returns>
        private Expression<Func<TEntity, bool>> GetLamdaGroupExpression<TEntity>(FilterGroup itemGroup){

            if(itemGroup == null)  return null;
            
            Expression<Func<TEntity, bool>> generic = null;
            Expression<Func<TEntity, bool>> secondGeneric = null;
            FilterItem filterItem = null;
            List<IFilterItem> subList = itemGroup.GetFilterItems();
            
            if(subList == null) return null;

            LogicalOpType logicalOpType = LogicalOpType.AND;
            foreach (IFilterItem item in subList)
	        {
                //compare operations are Supported "String", "Int" and 'boolean"
        	    if(item is FilterGroup)
                {
                    logicalOpType = (item as FilterGroup).LogicalOpType;
                    secondGeneric = GetLamdaGroupExpression<TEntity>(item as FilterGroup);
                }
                else if(item is FilterItem){
                    filterItem = item as FilterItem;
                    logicalOpType = filterItem.LogicalOpType;
                    secondGeneric = MakeGerericFilter<TEntity>(filterItem.PropertyName, filterItem.OperatorType, filterItem.Operand);
                }


                if(generic == null)
                {
                    generic = secondGeneric;
                }else
                {
                    //Logical operators are Supported 'AND' and 'OR'
                    if (logicalOpType == LogicalOpType.AND)
                    {
                        generic = FilterBuilder.GetInstance().AND(generic, secondGeneric);
                    }
                    else if (logicalOpType == LogicalOpType.OR)
                    {
                        generic = FilterBuilder.GetInstance().OR(generic, secondGeneric);
                    }
                }
	        }

            return generic;
        }
        /// <summary>
        /// 비교항목을 람다식으로 반환한다.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="propName"></param>
        /// <param name="opType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> MakeGerericFilter<TEntity>(string propName, ConditionalOpType opType, object value)
        {
            if (string.IsNullOrEmpty(propName) || value == null)
            {
                return null; 
            }
            return GenericFilter<TEntity>.Create(propName, opType, value);
        }


        /// <summary>
        /// 규칙에 대한 구분 분석한 결과를 가져옵니다.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ParseRules<TEntity>(TEntity item)
        {
            bool returnValue = false;
            Expression<Func<TEntity, bool>> expression = null;
            try
            {
                if (_filterItem is FilterItem)
                {
                    FilterItem filterItem = _filterItem as FilterItem;
                    //FilterItem에 해당하는 비교식 생성
                    expression = MakeGerericFilter<TEntity>(filterItem.PropertyName, filterItem.OperatorType, filterItem.Operand);
                }
                else
                {
                    //group filter
                    expression = this.GetLamdaGroupExpression<TEntity>(_filterItem as FilterGroup);
                }

                if (expression != null)
                {
                    returnValue = expression.Compile().Invoke(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return returnValue;
        }


        /// <summary>
        /// 규칙에 대한 구분 분석한 결과를 가져옵니다.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public Func<TEntity, bool> RuleParseDelegate<TEntity>()
        {
            Func<TEntity, bool> returnValue = null;
            Expression<Func<TEntity, bool>> expression = null;
            try
            {
                if (_filterItem is FilterItem)
                {
                    FilterItem filterItem = _filterItem as FilterItem;
                    //FilterItem에 해당하는 비교식 생성
                    expression = MakeGerericFilter<TEntity>(filterItem.PropertyName, filterItem.OperatorType, filterItem.Operand);
                }
                else
                {
                    //group filter
                    expression = this.GetLamdaGroupExpression<TEntity>(_filterItem as FilterGroup);
                }

                if (expression != null)
                {
                    returnValue = expression.Compile();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return returnValue;
        }
        #endregion
    }
}
