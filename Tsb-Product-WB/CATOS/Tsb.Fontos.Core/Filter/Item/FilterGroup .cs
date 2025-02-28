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
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Util.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Filter.Type;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Filter.Item
{
    public class FilterGroup : TsbBaseObject, IFilterItem
    {
        #region FIELD AREA ***************************************
        private List<IFilterItem> _list;
        private LogicalOpType _logicalOpType;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// 비교 연산자 그룹 간의 논리연산 방법
        /// </summary>
        public LogicalOpType LogicalOpType
        {
            get { return _logicalOpType; }
            //set { _logicalOpType = value; }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public FilterGroup(LogicalOpType logicalOpType)
        {
            base.ObjectID = "GNR-FTDW-Find-FilterItemGroup";
            _logicalOpType = logicalOpType;
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// 조건식을 등록한다.
        /// </summary>
        /// <param name="fiterItem"></param>
        /// <returns></returns>
        public bool AddFilter(IFilterItem fiterItem)
        {
            try
            {
                this.CreateFilterList();

                _list.Add(fiterItem);
            }
            catch (Exception)
            {

                return false;
            }

            return true;
            
        }
        /// <summary>
        /// 조건식을 등록한다.
        /// </summary>
        /// <typeparam name="TEntity">비교할 객체</typeparam>
        /// <param name="propertyName">객체의 속성명</param>
        /// <param name="opType">비교 방법</param>
        /// <param name="value">비교할 값</param>
        /// <param name="logicalOpType">
        /// 비교 연산자 간의 논리연산 방법
        /// 앞에 등록된 비교 연산자와 논리 연산을 수행한다.
        /// </param>
        /// <returns></returns>
        public bool AddFilter<TEntity>(string propertyName, ConditionalOpType opType, object value, LogicalOpType logicalOpType)
        {
            try
            {
                if (string.IsNullOrEmpty(propertyName) == true || value == null)
                {
                    return false;
                }

                PropertyInfo pi = typeof(TEntity).GetProperty(propertyName);
                if (pi == null)
                {
                    //MSG_FTCO_00169 : Type '{0}' doesn't have property '{1}'.
                    throw new ArgumentException(MessageBuilder.BuildMessage("MSG_FTCO_00169"
                        , DefaultMessage.NON_REG_WRD + typeof(TEntity).Name
                        , DefaultMessage.NON_REG_WRD + propertyName));
                }

                if (pi.PropertyType != value.GetType())
                {
                    try
                    {
                        value = Convert.ChangeType(value, System.Type.GetTypeCode(pi.PropertyType));
                    }
                    catch (Exception e)
                    {
                        GeneralLogger.Error(e);
                        return false;
                    }
                }

                this.CreateFilterList();

                _list.Add(new FilterItem(propertyName, opType, value, logicalOpType));
            }
            catch (Exception e)
            {
                GeneralLogger.Error(e);
                return false;
            }

            return true;            
        }
        /// <summary>
        /// 조건식을 등록한다.
        /// </summary>
        /// <typeparam name="TEntity">>비교할 객체</typeparam>
        /// <param name="expression">비교할 객체의 속성</param>
        /// <param name="opType">비교 방법</param>
        /// <param name="value">>비교할 값</param>
        /// <param name="logicalOpType">
        /// 비교 연산자 간의 논리연산 방법
        /// 앞에 등록된 비교 연산자와 논리 연산을 수행한다.
        /// </param>
        /// <returns></returns>
        public bool AddFilter<TEntity>(Expression<Func<TEntity, object>> expression, ConditionalOpType opType, object value, LogicalOpType logicalOpType)
        {
            if (expression == null || value == null) return false;
            
            try
            {
                string propertyName = LinqExpressionUtil.GetMemberName(expression.Body);
                System.Type propertyType = LinqExpressionUtil.GetMemberType(expression.Body);
                TypeCode valueTypeCode = Convert.GetTypeCode(value);

                TypeCode propertyTypeCode = System.Type.GetTypeCode(propertyType);


                if (valueTypeCode != propertyTypeCode)
                {
                    try
                    {
                        value = Convert.ChangeType(value, propertyTypeCode);
                    }
                    catch (Exception e)
                    {
                        GeneralLogger.Error(e);
                        return false;
                    }
                }

                this.AddFilter<TEntity>(propertyName, opType, value, logicalOpType);
            }
            catch (Exception e)
            {
                GeneralLogger.Error(e);

                return false;
            }

            return true;
        }

        /// <summary>
        /// 조건식을 등록한다.
        /// </summary>
        /// <typeparam name="TEntity">비교할 객체</typeparam>
        /// <param name="propertyName">객체의 속성명</param>
        /// <param name="opType">비교 방법</param>
        /// <param name="value">비교할 값</param>
        /// <param name="logicalOpType">
        /// 비교 연산자 간의 논리연산 방법
        /// 앞에 등록된 비교 연산자와 논리 연산을 수행한다.
        /// </param>
        /// <returns></returns>
        public bool AddFilter(System.Type itemType, string propertyName, ConditionalOpType opType, object value, LogicalOpType logicalOpType)
        {
            try
            {
                if (string.IsNullOrEmpty(propertyName) == true || value == null)
                {
                    return false;
                }

                PropertyInfo pi = itemType.GetProperty(propertyName);
                if (pi == null)
                {
                    //MSG_FTCO_00169 : Type '{0}' doesn't have property '{1}'.
                    throw new ArgumentException(MessageBuilder.BuildMessage("MSG_FTCO_00169"
                        , DefaultMessage.NON_REG_WRD + itemType.Name
                        , DefaultMessage.NON_REG_WRD + propertyName));
                }

                if (pi.PropertyType != value.GetType())
                {
                    try
                    {
                        if (pi.PropertyType.BaseType == typeof(Enum))
                        {
                            value = Enum.Parse(pi.PropertyType, value.ToString());
                        }
                        else
                        {
                            value = Convert.ChangeType(value, System.Type.GetTypeCode(pi.PropertyType));
                        }
                    }
                    catch (Exception e)
                    {
                        GeneralLogger.Error(e);
                        return false;
                    }
                }

                this.CreateFilterList();

                _list.Add(new FilterItem(propertyName, opType, value, logicalOpType));

            }
            catch (Exception e)
            {
                GeneralLogger.Error(e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 등록된 조건식을 반환한다.
        /// </summary>
        /// <returns></returns>
        public List<IFilterItem> GetFilterItems()
        {
            return _list;
        }

        private void CreateFilterList()
        {
            if (_list == null)
            {
                _list = new List<IFilterItem>();
            }
        }
        #endregion


        

    }
}
