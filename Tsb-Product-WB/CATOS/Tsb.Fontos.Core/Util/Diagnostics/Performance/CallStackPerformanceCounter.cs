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
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Util.Diagnostics.Type;
using Tsb.Fontos.Core.Logging;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Util.Diagnostics.Builder;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Util.Diagnostics.Performance
{
    public class CallStackPerformanceCounter : TsbBaseObject, IPerfomanceCounter
    {
        #region READONLY AREA **********************************
        private readonly ITsbLog log = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region FIELDS AREA ************************************
        private object syncRoot = new Object();
        private PerformanceDirector performanceDirector = null;
        OutputType outPutType = DiagnosticsInfo.GetInstance().OutPut;
        #endregion

        #region INITIALIZE AREA ********************************
        public CallStackPerformanceCounter()
            : base()
        {
            this.ObjectID = "GNR-FTCO-UTL-CallStackPerformanceCounter";
            this.performanceDirector = new PerformanceDirector(new PerformanceGeneralWriteBuilder());
        }
        #endregion

        #region IPerformanceCounter IMPLEMENTS AREA ************
        /// <summary>
        /// Calculate Delay Time
        /// </summary>
        /// <param name="performanceInfo"></param>
        public void SetDelayTime(PerformanceInfo performanceInfo)
        {
            try
            {
                lock (syncRoot)
                {
                    performanceInfo.ElapseTime = performanceInfo.EndTime - performanceInfo.StartTime;
                    string performanceKey = PerformanceKey.GetGuid().ToString();

                    if (this.HasSkipType(performanceInfo.TargetType.Name))
                    {
                        return;
                    }

                    IList<PerformanceInfo> performanceInfos = PerformanceStore.GetInstance().Get(performanceKey);
                    PerformanceInfo clonePerformanceInfo = performanceInfo.Clone() as PerformanceInfo;

                    if (performanceInfos == null)
                    {
                        performanceInfos = new List<PerformanceInfo>();
                        performanceInfos.Add(clonePerformanceInfo);
                        PerformanceStore.GetInstance().Add(performanceKey, performanceInfos);
                    }
                    else if (performanceInfos != null)
                    {
                        performanceInfos.Add(clonePerformanceInfo);
                    }

                    if (clonePerformanceInfo.IsStartPerformance)
                    {
                        this.WriteLog(performanceInfos);
                        this.RemovePerformanceStore(clonePerformanceInfo.PerformanceName);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Write Log
        /// </summary>
        /// <param name="performanceInfos"></param>
        private void WriteLog(IList<PerformanceInfo> performanceInfos)
        {
            try
            {
                string writeLogString = this.performanceDirector.GetWriteString(performanceInfos);
                this.WriteOutputLog(writeLogString);
            }
            catch (Exception ex)
            {
                this.WriteOutputLog(ex.StackTrace);
            }
        }

        /// <summary>
        /// Whether Taget Type exists
        /// </summary>
        /// <param name="tagetType"></param>
        /// <returns></returns>
        private bool HasSkipType(string tagetType)
        {
            if (DiagnosticsInfo.GetInstance().SkipTargetTypeNames != null)
            {
                IEnumerable<string> exists = DiagnosticsInfo.GetInstance().SkipTargetTypeNames.Where<string>(w => w.Equals(tagetType));

                if (exists.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region METHOD(RemovePerformanceStroe) AREA ************
        /// <summary>
        /// Remove Performance Store
        /// </summary>
        /// <param name="performanceName"></param>
        private void RemovePerformanceStore(string performanceName)
        {
            List<KeyValuePair<string, IList<PerformanceInfo>>> performanceInfosLists = 
                PerformanceStore.GetInstance().GetStartsWith(performanceName).ToList();

            try
            {
                if (performanceInfosLists.Count() > DiagnosticsInfo.GetInstance().MaxSavedPerformanceCount)
                {
                    // Remove old Data
                    IEnumerable<string> removeKeys =
                        from performanceInfos in performanceInfosLists
                        orderby performanceInfos.Value[0].CreateTime ascending
                        select performanceInfos.Key;

                    if (removeKeys != null && removeKeys.First() != null)
                    {
                        string removeKey = removeKeys.First();
                        PerformanceStore.GetInstance().Remove(removeKey);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion

        #region METHOD(Output LOG) AREA ************************
        /// <summary>
        /// Write Output Log
        /// </summary>
        /// <param name="writeLog"></param>
        public void WriteOutputLog(string writeLog)
        {
            if (this.outPutType == OutputType.LOG)
            {
                log.Info(writeLog);
            }
            else
            {
                Console.WriteLine(writeLog);
            }
        }
        #endregion
    }
}
