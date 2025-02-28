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
* DATE           AUTHOR		       REVISION    	
* 2012.05.03  Tonny.Kim 1.0	    First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Message;

namespace Tsb.Fontos.Core.Util.Diagnostics.Performance
{
    public class PerformanceStore : TsbBaseObject
    {
        #region READONLY AREA **********************************
        public readonly string HYPHEN_STRING = "-";
        #endregion

        #region FIELDS/PROPERTY AREA ***************************
        private Dictionary<string, IList<PerformanceInfo>> _performanceInfosDic;
        /// <summary>
        /// PerformanceStore static reference
        /// </summary>
        private static PerformanceStore _performanceStore = null;
        #endregion

        #region INITIALIZE AREA ********************************
        public PerformanceStore()
            : base()
        {
            this.ObjectID = "GNR-FTCO-UTL-PerformanceStore";
            this._performanceInfosDic = new Dictionary<string, IList<PerformanceInfo>>();
        }

        /// <summary>
        /// Returns a singleton PerformanceStore instance
        /// </summary>
        /// <returns>Performance Store obect reference</returns>
        public static PerformanceStore GetInstance()
        {
            if (_performanceStore == null)
            {
                _performanceStore = new PerformanceStore();
            }

            return _performanceStore;
        }
        #endregion

        #region METHOD AREA ************************************
        /// <summary>
        /// Add Performance Information
        /// </summary>
        /// <param name="performanceKey"></param>
        /// <param name="performanceInfo"></param>
        public void Add(string performanceKey, IList<PerformanceInfo> performanceInfos)
        {
            try
            {
                if (!this._performanceInfosDic.ContainsKey(performanceKey))
                {
                    this._performanceInfosDic.Add(performanceKey, performanceInfos);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }
        }

        /// <summary>
        /// Gets Performance Information
        /// </summary>
        /// <param name="performanceKey"></param>
        /// <returns></returns>
        public IList<PerformanceInfo> Get(string performanceKey)
        {
            try
            {
                if (this._performanceInfosDic.ContainsKey(performanceKey))
                {
                    return this._performanceInfosDic[performanceKey];
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Remove Performance Information
        /// </summary>
        /// <param name="performanceKey"></param>
        public void Remove(string performanceKey)
        {
            try
            {
                if (this._performanceInfosDic.ContainsKey(performanceKey))
                {
                    this._performanceInfosDic.Remove(performanceKey);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="performanceKey"></param>
        /// <returns></returns>
        public IList<KeyValuePair<string, IList<PerformanceInfo>>> GetStartsWith(string performanceName)
        {
            IList<KeyValuePair<string, IList<PerformanceInfo>>> startsWithDics = 
                _performanceInfosDic.Where<KeyValuePair<string, IList<PerformanceInfo>>>(dw =>
                    dw.Value.Where<PerformanceInfo>(w =>
                        w.PerformanceName.Equals(performanceName)).Count() > 0).ToList();

            return startsWithDics;
        }

        /// <summary>
        /// Gets Method Name
        /// </summary>
        /// <param name="performanceInfo"></param>
        /// <returns></returns>
        public string GetMethodName(PerformanceInfo performanceInfo)
        {
            string methodName = string.Empty;

            if (performanceInfo.MethodInfo != null)
            {
                methodName = performanceInfo.MethodInfo.Name;
            }

            return methodName;
        }
        #endregion
    }
}
