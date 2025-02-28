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
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Util.Diagnostics.Performance
{
    public class PerformanceHandler : TsbBaseObject
    {
        #region READONLY AREA **********************************
        public readonly string HYPHEN_STRING = PerformanceStore.GetInstance().HYPHEN_STRING;
        #endregion

        #region FIELDS AREA ************************************
        private PerformanceInfo performanceInfo;
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default constructor
        /// </summary>
        public PerformanceHandler()
            : this(null)
        { }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="target">Taget object</param>
        public PerformanceHandler(object target)
            : this(target, PerformanceType.NONE)
        { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="target">Taget Object</param>
        /// <param name="performanceType">Performance Type</param>
        public PerformanceHandler(object target, PerformanceType performanceType)
            : this(target, performanceType, null)
        { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="target">Taget Object</param>
        /// <param name="performanceType">Performance Type</param>
        /// <param name="manualTagetType">Define Type</param>
        public PerformanceHandler(object target, PerformanceType performanceType, System.Type defineTagetType)
            : this(target, performanceType, defineTagetType, null)
        { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="target">Taget Object</param>
        /// <param name="performanceType">Performance Type</param>
        /// <param name="manualTagetType">Define Type</param>
        /// <param name="performanceName">Performance Name</param>
        public PerformanceHandler(object target, PerformanceType performanceType, System.Type defineTagetType, string performanceName)
        {
            this.ObjectID = "GNR-FTCO-UTL-PerformanceHandler";
            this.performanceInfo = new PerformanceInfo();
            this.performanceInfo.PerformanceName = performanceName;
            this.performanceInfo.PerformanceType = performanceType;

            if (defineTagetType != null)
            {
                this.performanceInfo.TargetType = defineTagetType;
            }
            else if (target != null)
            {
                this.performanceInfo.TargetType = target.GetType();
            }

            if (string.IsNullOrEmpty(this.performanceInfo.PerformanceName))
            {
                this.performanceInfo.PerformanceName = this.performanceInfo.TargetType.Name;
            }
        }
        #endregion

        #region METHOD AREA ************************************
        /// <summary>
        /// Start
        /// </summary>
        public void Start(MethodBase method)
        {
            try
            {
                if (DiagnosticsInfo.GetInstance().ShowUsedMemory)
                {
                    performanceInfo.StartCurrentMemoryMB = MachineInfo.GetCurrentUsedMemorySizeMB();
                }

                if (PerformanceKey.GetGuid() == default(Guid))
                {
                    PerformanceKey.SetGuid();
                    this.performanceInfo.IsStartPerformance = true;
                }

                this.performanceInfo.MethodInfo = method;
                this.performanceInfo.StartTime = DateTime.Now;
                this.performanceInfo.Sequence = ++PerformanceKey.Sequence;
                this.performanceInfo.PerformanceName += this.HYPHEN_STRING + PerformanceStore.GetInstance().GetMethodName(performanceInfo);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// End
        /// </summary>
        public void End()
        {
            try
            {
                this.performanceInfo.EndTime = DateTime.Now;

                if (DiagnosticsInfo.GetInstance().ShowUsedMemory)
                {
                    this.performanceInfo.EndCurrentMemoryMB = MachineInfo.GetCurrentUsedMemorySizeMB();
                }

                this.performanceInfo.Accept(new CallStackPerformanceCounter());

                if (this.performanceInfo.IsStartPerformance)
                {
                    PerformanceKey.ClearGuid();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion
    }
}
