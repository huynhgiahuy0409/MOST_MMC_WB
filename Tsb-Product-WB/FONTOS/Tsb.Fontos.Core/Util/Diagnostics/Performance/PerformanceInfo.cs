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
using Tsb.Fontos.Core.Util.Diagnostics.Type;
using System.Reflection;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Util.Diagnostics.Performance
{
    public class PerformanceInfo : TsbBaseObject, IPerformanceInfo
    {
        #region FIELDS AREA ************************************
        private DateTime createTime = default(DateTime);
        #endregion

        #region PROPERTY AREA **********************************
        public bool IsStartPerformance { get; set; }
        public string PerformanceName { get; set; }
        public System.Type TargetType { get; set; }
        public MethodBase MethodInfo { get; set; }
        public DateTime CreateTime { get { return this.createTime; } }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan ElapseTime { get; set; }
        public long StartCurrentMemoryMB { get; set; }
        public long EndCurrentMemoryMB { get; set; }
        public PerformanceType PerformanceType { get; set; }
        public int Sequence { get; set; }
        #endregion

        #region INITIALIZE AREA ********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PerformanceInfo()
            : base()
        {
            this.ObjectID = "GNR-FTCO-UTL-PerformanceInfo";
            this.createTime = DateTime.Now;
            this.Sequence = 0;
        }
        #endregion

        #region IPerformanceAccept IMPLEMENTS AREA *************
        /// <summary>
        /// Accept
        /// </summary>
        /// <param name="performanceCounter"></param>
        public void Accept(IPerfomanceCounter performanceCounter)
        {
            if (performanceCounter != null)
            {
                performanceCounter.SetDelayTime(this);
            }
        }
        #endregion
    }
}