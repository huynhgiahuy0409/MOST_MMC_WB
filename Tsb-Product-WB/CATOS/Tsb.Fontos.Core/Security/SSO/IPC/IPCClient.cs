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
using System.Runtime.Remoting.Channels;
using Tsb.Fontos.Core.Util.File;
using System.IO;
using System.Diagnostics;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Security.SSO.IPC
{
    /// <summary>
    /// This is  class to manage the client side ipc communication.
    /// </summary>
    internal class IPCClient : TsbBaseObject
    {
        #region FIELDS AREA ************************************
        private static ISSOLogin _clientChannel;
        #endregion

        #region INITIALIZE AREA *************************************
        public IPCClient()
        {
            this.ObjectID = "ITM-FT-FTCO-SSO-IPCClient";
        }
        #endregion

        #region METHOD AREA ************************************
        /// <summary>
        /// Gets the singleton ISSOLogin interface instance object.
        /// </summary>
        public static ISSOLogin GetInstance()
        {
            if (_clientChannel == null)
            {
                _clientChannel = CreateClientChannel(SSOConstant.DEFALUT_PGMCODE);
            }

            return _clientChannel;
        }

        /// <summary>
        /// Create client channel about the single sign-on information.
        /// </summary>
        /// <param name="programCode"></param>
        /// <returns></returns>
        private static ISSOLogin CreateClientChannel(string programCode)
        {
            ISSOLogin fontosCommunicator = null;
            try
            {
                fontosCommunicator =
                    (ISSOLogin)Activator.GetObject(
                    typeof(ISSOLogin),
                    IPCUtil.GetIPCUrl(programCode));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fontosCommunicator;
        }

        /// <summary>
        /// Indicates whether the client channel was started.
        /// </summary>
        /// <returns>true if it was started, otherwise false.</returns>
        public static bool IsAlreadyStarted()
        {
            IChannel[] channels = ChannelServices.RegisteredChannels;

            if (channels != null && channels.Count() > 0)
            {
                return true;
            }

            return false;
        }
        #endregion

    }
}
