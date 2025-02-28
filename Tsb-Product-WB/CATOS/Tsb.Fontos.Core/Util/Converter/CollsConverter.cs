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
* 2009.08.06   CHOI 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Util.Converter
{
    /// <summary>
    /// Collection Converter Utility class
    /// </summary>
    public class CollsConverter
    {
        /// <summary>
        /// Converts IEnumarable collection to List
        /// </summary>
        /// <param name="iEnumarable">IEnumarable collection to convert</param>
        /// <returns>List collection</returns>
        public static List<T> IEnumToList<T>(IEnumerable<T> iEnumarable)
        {
            return new List<T>(iEnumarable);
        }

        /// <summary>
        /// Converts IEnumarable collection to BaseItemList
        /// </summary>
        /// <param name="iEnumarable">IEnumarable collection to convert</param>
        /// <returns>List collection</returns>
		//public static BaseItemList<T> IEnumToBaseItemList<T>(IEnumerable<T> iEnumarable)
		//{
		//    return new BaseItemList<T>(new List<T>(iEnumarable));
		//}

    }
}
