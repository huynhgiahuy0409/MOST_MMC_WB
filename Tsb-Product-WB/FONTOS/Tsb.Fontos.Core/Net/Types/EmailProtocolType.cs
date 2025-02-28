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
* 2009.12.03    JACK 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Tsb.Fontos.Core.Net.Types
{
    /// <summary>
    /// Enumerated type used to represent supported e-mail mechanisms 
    /// </summary>
    public enum EmailProtocolType
    {
        SimpleMAPI,
        SMTP
    };
}
