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
* DATE           AUTHOR		REVISION    	
* 2009.09.05    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Caches.Types;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Caches
{
    /// <summary>
    /// Cache Configuration for the Cache instance.
    /// </summary>
    public class CacheConfigurator : TsbBaseObject
    {

        /// <summary>
        /// Default cache item priority.
        /// </summary>
        public static readonly CachePriorityTypes DEFAULT_CACHE_PRIORITY = CachePriorityTypes.Normal;


        /// <summary>
        /// Default flag indicating if sliding expiration is enabled.
        /// </summary>
        public static readonly bool DEFAULT_DO_SLIDING_EXPIRE = false;


        /// <summary>
        /// Default timespne to keep item in cache.
        /// </summary>
        public static readonly TimeSpan DEFAULT_TIME_TO_LIVE = new TimeSpan(0, 20, 0);


        /// <summary>
        /// General code default flag indicating if sliding expiration is enabled.
        /// </summary>
        public static readonly bool GENERAL_CODE_DO_SLIDING_EXPIRE = true;


        /// <summary>
        /// General code default timespne to keep item in cache.
        /// </summary>
        public static readonly TimeSpan GENERAL_CODE_TIME_TO_LIVE = new TimeSpan(0, 20, 0);


        /// <summary>
        /// Default Constructor
        /// </summary>
        public CacheConfigurator() : base()
        {
            this.ObjectID = "GNR_FTCO_CCH_CacheConfigurator";
        }
    }
}
