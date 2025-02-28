#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2015 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* 
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		    REVISION    	
* 2019.06.15     Lim JC 1.0	    First release.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.IPC
{
    public enum IPCMessageTypes
    {
        COMPLETE_MENU_INITIALIZE,
        COMPLETE_APPDATA_INITIALIZE,
        ACTIVATED_APP,
        NET_CONNECTED,
        NET_DISCONNECTED,
        SET_LAST_ACCESSTIME
    }
}
