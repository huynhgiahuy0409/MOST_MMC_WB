#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2013 TOTAL SOFT BANK LIMITED. All Rights
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
* 2013.11.25   Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Security.SSO.IPC;

namespace Tsb.Fontos.Core.Security.SSO
{
    public class SSOServerProxy
    {
        #region CONSTRUCTOR AREA *********************************
        public SSOServerProxy()
        {
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Start up the server module.
        /// </summary>
        public void StartUp()
        {
            IPCServer.GetInstance().StartUp();
        }

        /// <summary>
        /// Shut down the server side module.
        /// </summary>
        public void Shutdown()
        {
            IPCServer.GetInstance().Shutdown();
        }
        #endregion
    }
}
