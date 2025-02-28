#region Interface Definitions
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
* 2010.02.04    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using Tsb.Fontos.Core.Security.Authentication;
using Tsb.Fontos.Core.Security.Encryption;

namespace Tsb.Fontos.Core.Configuration
{
    /// <summary>
    /// Represent ConfigInitAdapter
    /// </summary>
    public interface IConfigInitAdapter
    {
        #region PROPERTY AREA **********************************
        IBaseEncrypter PasswordEncrypter { get; set; }
        #endregion

        #region METHOD AREA ************************************
        /// <summary>
        /// Initialize Configuration
        /// </summary>
        /// <returns>IConfigInfo implementer object reference</returns>
        IConfigInfo InitConfig();
        #endregion
    }
}
