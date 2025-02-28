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
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Util.Diagnostics.Performance;

namespace Tsb.Fontos.Core.Util.Diagnostics.Builder
{
    public class PerformanceDirector : TsbBaseObject
    {
        #region FIEDLS AREA ************************************
        private PerformanceBuilder builder;
        #endregion

        #region INITIALIZE AREA ********************************
        /// <summary>
        /// default constructor
        /// </summary>
        public PerformanceDirector(PerformanceBuilder builder)
            : base()
        {
            this.builder = builder;
        }
        #endregion

        #region METHDO AREA ************************************
        /// <summary>
        /// Get Write String
        /// </summary>
        /// <returns></returns>
        public string GetWriteString(IList<PerformanceInfo> performanceInfos)
        {
            return this.builder.GetWriteString(performanceInfos);
        }
        #endregion
    }
}
