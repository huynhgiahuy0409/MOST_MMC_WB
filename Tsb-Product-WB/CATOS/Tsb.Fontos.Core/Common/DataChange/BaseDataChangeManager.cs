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
* 2011.07.19  Tonny Kim 1.1   Delete CacheManager
*
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Caches;
using Tsb.Fontos.Core.Common.Param;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Common.DataChange
{
    public abstract class BaseDataChangeManager : TsbBaseObject
    {
        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initilize instance
        /// </summary>
        public BaseDataChangeManager()
            : base()
        {
            this.ObjectID = "GNR-FTCO-CMN-BaseDataChangeManager";
        }
        #endregion

        #region METHOD(SetDataChange) AREA **************************
        /// <summary>
        /// Sets DataChange
        /// </summary>
        /// <param name="ruleType"></param>
        /// <param name="targetObject"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public object SetDataChange(System.Enum ruleType, object targetObject, params string[] propertyNames)
        {
            return this.SetDataChange(ruleType, targetObject, true, propertyNames);
        }

        /// <summary>
        /// Sets DataChange
        /// </summary>
        /// <param name="ruleType"></param>
        /// <param name="targetObject"></param>
        /// <param name="throwException"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public object SetDataChange(System.Enum ruleType, object targetObject, bool throwException, params string[] propertyNames)
        {
            BaseDataChangeParam dataChangeParam = new BaseDataChangeParam();

            try
            {
                dataChangeParam.RuleType = ruleType;
                dataChangeParam.TargetObject = targetObject;
                dataChangeParam.PropertyNames = propertyNames;
                dataChangeParam.ThrowException = throwException;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return SetDataChange(dataChangeParam);
        }
        #endregion

        #region abstract METHOD AREA ********************************
        public abstract object SetDataChange(BaseDataChangeParam dataChangeParam);
        public abstract object SetAllDataChange(BaseDataChangeParam dataChangeParam, BaseDataChangeRule dataChangeRuleChain);
        #endregion
    }
}
