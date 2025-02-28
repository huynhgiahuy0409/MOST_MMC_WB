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

namespace Tsb.Fontos.Core.Util.Diagnostics.Performance
{
    public class PerformanceKey : TsbBaseObject
    {
        #region FIELDS/PROPERTY AREA ***************************
        /// <summary>
        /// Represents a globally unique identifier(GUID)
        /// </summary>
        private static Guid guid;

        /// <summary>
        /// Sequence
        /// </summary>
        public static int Sequence { get; set; }
        #endregion

        #region INITIALIZE AREA ********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PerformanceKey()
            : base()
        {
            this.ObjectID = "GNR-FTCO-UTL-PerformanceOption";
        }

        /// <summary>
        /// static Constructor
        /// </summary>
        static PerformanceKey()
        {
            Sequence = 0;
        }
        #endregion

        #region METHOD AREA ************************************
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Guid GetGuid()
        {
            return guid;
        }

        /// <summary>
        /// Sets GUID
        /// </summary>
        public static void SetGuid()
        {
            guid = System.Guid.NewGuid();
        }

        /// <summary>
        /// Clear GUID
        /// </summary>
        public static void ClearGuid()
        {
            guid = default(Guid);
            Sequence = 0;
        }
        #endregion
    }
}
