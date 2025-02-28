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
using Tsb.Fontos.Core.Util.Diagnostics.Performance;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Util.Diagnostics.Type;
using Tsb.Fontos.Core.Util.Type;

namespace Tsb.Fontos.Core.Util.Diagnostics.Builder
{
    public class PerformanceGeneralWriteBuilder : PerformanceBuilder
    {
        #region FIELDS AREA ************************************
        private StringBuilder buffer = new StringBuilder();
        #endregion

        #region INITIALIZE AREA ********************************
        /// <summary>
        /// default constructor
        /// </summary>
        public PerformanceGeneralWriteBuilder()
            : base()
        { }
        #endregion

        #region PerformanceBuilder IMPLEMENTS AREA *************
        /// <summary>
        /// Make Write String
        /// </summary>
        protected override void MakeStartString(PerformanceInfo performanceInfo)
        {
            string writeLog = base.START_STRING + " (" + performanceInfo.PerformanceName + ") : "
                + base.GetDisplayTime(performanceInfo.ElapseTime);
            buffer.AppendLine(writeLog);
        }

        /// <summary>
        /// Make Contents String
        /// </summary>
        protected override void MakeContentsString(PerformanceInfo performanceInfo, string prevStringWithContents)
        {
            string writeLog = prevStringWithContents
                + "Source Target <" + base.GetDisplayTargetTypeName(performanceInfo.TargetType) + base.HYPHEN_STRING
                + PerformanceStore.GetInstance().GetMethodName(performanceInfo) + "> : "
                + base.GetDisplayTime(performanceInfo.ElapseTime);
            buffer.AppendLine(base.TAB + writeLog);
        }

        /// <summary>
        /// Gets previous string with contents
        /// </summary>
        /// <param name="performanceInfo"></param>
        /// <param name="prevPerformanceInfo"></param>
        /// <returns></returns>
        protected override string GetPrevStringWithContents(PerformanceInfo performanceInfo)
        {
            string returnString = string.Empty;

            if (performanceInfo != null)
            {
                returnString += "[" + performanceInfo.PerformanceType + "] ";
            }

            return returnString;
        }

        /// <summary>
        /// Make End String
        /// </summary>
        protected override void MakeEndString(PerformanceInfo performanceInfo)
        {
            string writeLog = base.END_STRING + " (" + performanceInfo.PerformanceName + ")";
            buffer.AppendLine(writeLog);
        }

        /// <summary>
        /// Gets Result
        /// </summary>
        /// <returns></returns>
        protected override string GetResult()
        {
            return buffer.ToString().TrimEnd();
        }
        #endregion
    }
}
