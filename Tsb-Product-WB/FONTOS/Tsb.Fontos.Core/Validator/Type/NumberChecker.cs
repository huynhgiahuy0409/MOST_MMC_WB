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
* 2009.09.27   CHOI 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Validator.Type
{
    /// <summary>
    /// Number type checker class
    /// </summary>
    public class NumberChecker
    {
        /// <summary>
        /// Check a specified string is a positive number or not
        /// </summary>
        /// <param name="strNumber">A number format string to check</param>
        /// <param name="maxValue">Max value</param>
        /// <param name="includeZero">Indicates number can be zero or not</param>
        /// <returns></returns>
        public static bool IsPostiveDecimal(string strNumber, decimal maxValue, bool includeZero)
        {
            bool isPositiveNumber = false;
            decimal dTemp = 0;

            try
            {
                dTemp = decimal.Parse(strNumber);

                if (dTemp == 0)
                {
                    if (includeZero)
                    {
                        isPositiveNumber = true;
                    }
                    else
                    {
                        isPositiveNumber = false;
                    }
                }
                else
                {
                    if (dTemp > 0 && dTemp <= maxValue)
                    {
                        isPositiveNumber = true;
                    }
                }
            }
            catch(Exception)
            {
                isPositiveNumber = false;
            }
            
            return isPositiveNumber;
        }
    }
}
