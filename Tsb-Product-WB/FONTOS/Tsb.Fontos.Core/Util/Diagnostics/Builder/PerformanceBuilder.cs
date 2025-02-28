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
* 2012.05.09  Tonny.Kim 1.0	    First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Util.Diagnostics.Type;
using Tsb.Fontos.Core.Util.Diagnostics.Performance;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Util.Diagnostics.Builder
{
    public abstract class PerformanceBuilder
    {
        #region READONLY AREA **********************************
        private int MAX_DATETIME_LENGTH = 12;
        #endregion

        #region FIDELDS AREA ***********************************
        private ResultFormatType timeFormatType = DiagnosticsInfo.GetInstance().PerformanceTimeFormat;
        private PerformanceResultFormatType resultFormatType = DiagnosticsInfo.GetInstance().PerformanceResultFormat;
        private OutputType outPutType = DiagnosticsInfo.GetInstance().OutPut;
        private PerformanceLevel logLevel = DiagnosticsInfo.GetInstance().LogLevel;
        #endregion

        #region READONLY AREA **********************************
        public readonly string HYPHEN_STRING = PerformanceStore.GetInstance().HYPHEN_STRING;
        public readonly string START_STRING = "DIAGNOSTICS RESULT";
        public readonly string END_STRING = "End DIAGNOSTICS RESULT";
        public readonly string MILLISECONDS_RESULT_FORMAT_STRING = "ms";
        public readonly string MEMORY_SIZE_MB_STRING = "MB";
        public readonly string SECONDS_RESULT_FORMAT_STRING = "s";
        public readonly string TAB = "    ";
        public readonly string SUB_TAB_STRING = " -> ";
        public readonly string ARROW_BLANK_STRING = " -> ";
        #endregion

        #region INITIALIZE AREA ********************************
        /// <summary>
        /// default constructor
        /// </summary>
        public PerformanceBuilder()
            : base()
        { }
        #endregion

        #region METHOD(abstract) AREA **************************
        protected abstract void MakeStartString(PerformanceInfo performanceInfo);
        protected abstract void MakeContentsString(PerformanceInfo performanceInfo, string prevStringWithContents);
        protected abstract void MakeEndString(PerformanceInfo performanceInfo);
        protected abstract string GetPrevStringWithContents(PerformanceInfo performanceInfo);
        protected abstract string GetResult();
        #endregion

        #region GetWriteString AREA ****************************
        /// <summary>
        /// Gets Write String
        /// </summary>
        /// <param name="performanceInfos"></param>
        /// <returns></returns>
        public string GetWriteString(IList<PerformanceInfo> performanceInfos)
        {
            try
            {
                PerformanceInfo startPerformanceInfo = null;
                string writeLog = string.Empty;

                IList<PerformanceInfo> startPerformanceInfos =
                                    performanceInfos.Where<PerformanceInfo>(w => w.IsStartPerformance).ToList();

                // Start Log
                if (startPerformanceInfos.Count() == 1)
                {
                    startPerformanceInfo = startPerformanceInfos[0];
                    this.MakeStartString(startPerformanceInfo);
                }

                // List Log
                IList<PerformanceInfo> orderByPerformanceInfos =
                    performanceInfos.OrderBy(o => o.Sequence).Where<PerformanceInfo>(w => !this.IsSkipLogLevel(logLevel, w)).ToList();
                IList<string> prevStringWithContentsList = this.PrevStringWithContents(orderByPerformanceInfos);
                int maxLengthPrevStringWithContents = this.GetMaxLengthPrevStringWithContents(prevStringWithContentsList);

                for (int i = 0; i < orderByPerformanceInfos.Count; i++)
                {
                    PerformanceInfo performanceInfo = orderByPerformanceInfos[i];
                    string prevStringWithContents = StringUtil.PadSpaceLeft(prevStringWithContentsList[i], maxLengthPrevStringWithContents);
                    this.MakeContentsString(performanceInfo, prevStringWithContents);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return this.GetResult();
        }
        #endregion

        #region METHOD(Previous Performance) AREA **************
        /// <summary>
        /// Gets maximum length previous string with contents
        /// </summary>
        /// <param name="prevStringWithContentsList"></param>
        /// <returns></returns>
        private int GetMaxLengthPrevStringWithContents(IList<string> prevStringWithContentsList)
        {
            int maxLength = default(int);

            try
            {
                foreach (string prevStringWithContents in prevStringWithContentsList)
                {
                    int stringLength = prevStringWithContents.Length;

                    if (stringLength > maxLength)
                    {
                        maxLength = stringLength;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return maxLength;
        }

        /// <summary>
        /// Previous string with contents
        /// </summary>
        /// <param name="performanceInfos"></param>
        /// <returns></returns>
        private IList<string> PrevStringWithContents(IList<PerformanceInfo> performanceInfos)
        {
            IList<string> returnStrings = new List<string>();

            try
            {
                foreach (PerformanceInfo performanceInfo in performanceInfos)
                {
                    string prevStringWithContents = this.GetPrevStringWithContents(performanceInfo);
                    returnStrings.Add(prevStringWithContents);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return returnStrings;
        }
        #endregion

        #region METHOD(Log Level) AREA *************************
        /// <summary>
        /// Whether Skp log level
        /// </summary>
        /// <returns></returns>
        protected bool IsSkipLogLevel(PerformanceLevel performanceLevel, PerformanceInfo performanceInfo)
        {
            bool isSkip = false;

            try
            {
                if (performanceLevel != PerformanceLevel.All)
                {
                    if (performanceInfo.PerformanceType == PerformanceType.MENU_TOOLBAR)
                    {
                        isSkip = true;
                    }
                    else if (performanceInfo.PerformanceType.GetHashCode() > performanceLevel.GetHashCode())
                    {
                        isSkip = true;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return isSkip;
        }
        #endregion

        #region METHOD(Display LOG) AREA ***********************
        /// <summary>
        /// write log string
        /// </summary>
        /// <param name="count">tab count</param>
        /// <param name="logString">log string</param>
        protected string WriteLogStringContents(string logString, PerformanceType performanceType)
        {
            string tabString = this.TAB;

            if (performanceType == PerformanceType.DAO)
            {
                tabString += this.TAB;
            }

            return tabString;
        }

        /// <summary>
        /// Gets Display Taget Type Name
        /// </summary>
        /// <param name="Type">Target Type Name</param>
        /// <returns>Name</returns>
        protected string GetDisplayTargetTypeName(System.Type type)
        {
            string returnDisplayTargetTypeName = string.Empty;

            switch (this.resultFormatType)
            {
                case PerformanceResultFormatType.FullName:
                    returnDisplayTargetTypeName = type.FullName;
                    break;
                case PerformanceResultFormatType.ShortName:
                    returnDisplayTargetTypeName = type.Name;
                    break;
                default:
                    returnDisplayTargetTypeName = string.Empty;
                    break;
            }

            return returnDisplayTargetTypeName;
        }

        /// <summary>
        /// Gets Display Time
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        protected string GetDisplayTime(TimeSpan timeSpan)
        {
            string returnDisplayTime = string.Empty;

            switch (this.timeFormatType)
            {
                case ResultFormatType.TICK:
                    returnDisplayTime = string.Format("{0:d}", timeSpan.Ticks);
                    break;
                case ResultFormatType.MILLI_SECOND:
                    returnDisplayTime = string.Format("{0:f} " + this.MILLISECONDS_RESULT_FORMAT_STRING, timeSpan.TotalMilliseconds);
                    break;
                case ResultFormatType.DATETIME:
                    returnDisplayTime = string.Format("{0:hh:mm:ss}", timeSpan.ToString()).Substring(0, MAX_DATETIME_LENGTH);
                    break;
                default:
                    returnDisplayTime = string.Empty;
                    break;
            }

            return returnDisplayTime;
        }
        #endregion
    }
}
