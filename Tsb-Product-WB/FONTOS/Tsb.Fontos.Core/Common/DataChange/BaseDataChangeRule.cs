#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2010 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR 		REVISION    	
* 2010.04.28  Tonny Kim 1.0   First release.
*
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using System.ComponentModel;
using Tsb.Fontos.Core.Common.Param;
using Tsb.Fontos.Core.Caches;
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Exceptions.System;
using System.Diagnostics;
using Tsb.Fontos.Core.Logging;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Common.DataChange
{
    public abstract class BaseDataChangeRule : TsbBaseObject
    {
        #region PROPERTY AREA ****************************
        private readonly ITsbLog log = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BaseDataChangeRule Next { get; set; }
        public Enum RuleType { get; set; }
        private BaseDataChangeParam DataChangeParam { get; set; }
        public string[] PropertyNames { get; set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initilize instance
        /// </summary>
        public BaseDataChangeRule()
            : base()
        {
            this.ObjectID = "GNR-FTCO-CMN-BaseDataChangeRule";
        }
        #endregion

        #region METHOD AREA (OPERATOR OVERLOADING)*******************
        /// <summary>
        /// operator overloading to make chain of BaseDataChangeRule
        /// </summary>
        /// <param name="leftValidator">left side DataType</param>
        /// <param name="rightValidator">right side DataType</param>
        /// <returns>chain of validators</returns>
        public static BaseDataChangeRule operator +(BaseDataChangeRule leftDataChangeRule, BaseDataChangeRule rightDataChangeRule)
        {
            BaseDataChangeRule last = leftDataChangeRule;

            while (last.Next != null)
            {
                last = last.Next;
            }

            last.Next = rightDataChangeRule;

            return leftDataChangeRule;
        }
        #endregion

        #region METHOD(Chain of Responsibility) AREA ****************
        /// <summary>
        /// Gets DataChangeRule
        /// </summary>
        /// <param name="typeParam"></param>
        /// <returns></returns>
        public object GetDataChange(BaseDataChangeParam dataChangeParam)
        {
            try
            {
                this.DataChangeParam = dataChangeParam;

                if (this.MatchDataChangeRule(dataChangeParam))
                {
                    return this.ApplyDataChangeRule(dataChangeParam);
                }
                else if (this.Next != null)
                {
                    return this.Next.GetDataChange(dataChangeParam);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }

            return null;
        }

        /// <summary>
        /// Match Data change
        /// </summary>
        /// <param name="typeParam"></param>
        /// <returns></returns>
        private bool MatchDataChangeRule(BaseDataChangeParam dataChangeParam)
        {
            if (this.RuleType.Equals(dataChangeParam.RuleType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object GetAllDataChange(BaseDataChangeParam dataChangeParam)
        {
            object returnObj = this.ApplyDataChangeRule(dataChangeParam);

            try
            {
                if (this.Next != null)
                {
                    returnObj = this.Next.GetAllDataChange(dataChangeParam);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }

            return returnObj;
        }
        #endregion

        #region Abstract METHOD/PROPERTY AREA ***********************
        protected abstract object ApplyDataChangeRule(BaseDataChangeParam param);
        #endregion

        #region METHOD(SetPropertyValueByName) AREA *****************
        /// <summary>
        /// Sets Property Value By Name
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        public void SetPropertyValueByName(object targetObject, string propName, object value)
        {
            try
            {
                if (PropertyUtil.IsValidPropertyName(targetObject, propName))
                {
                    PropertyUtil.SetPropertyValueByName(targetObject, propName, value);
                }
            }
            catch (TsbSysTypeException ex)
            {
                log.Debug(ex);
            }
        }
        #endregion

        #region METHOD(SetEmptyValueWithProperty) AREA **************
        /// <summary>
        /// Sets empty value with property
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="propertyNames"></param>
        public void SetEmptyValueWithProperty(object targetObject, string[] propertyNames)
        {
            try
            {
                if (targetObject == null || propertyNames == null) return;

                foreach (string propertyName in propertyNames)
                {
                    this.SetPropertyValueByName(targetObject, propertyName, string.Empty);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }

        }
        #endregion
    }
}
