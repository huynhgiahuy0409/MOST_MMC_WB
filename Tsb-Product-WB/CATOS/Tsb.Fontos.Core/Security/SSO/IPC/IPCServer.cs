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
using Tsb.Fontos.Core.Objects;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting;

namespace Tsb.Fontos.Core.Security.SSO.IPC
{
    /// <summary>
    /// This is class to manage the server side ipc communication.
    /// </summary>
    internal class IPCServer : TsbBaseObject
    {
        #region FIELDS AREA ************************************
        private static IPCServer _serverCommunicator;
        private IChannel _channel;
        #endregion

        #region INITIALIZE AREA *************************************
        public IPCServer()
        {
            this.ObjectID = "ITM-FT-FTCO-SSO-IPCServer";
        }

        /// <summary>
        /// Gets the singleton IPCServer interface instance object.
        /// </summary>
        public static IPCServer GetInstance()
        {
            if (_serverCommunicator == null)
            {
                _serverCommunicator = new IPCServer();
            }

            return _serverCommunicator;
        }
        #endregion

        #region METHOD AREA ************************************
        /// <summary>
        /// Start up the server side channel.
        /// </summary>
        public void StartUp()
        {
            try
            {
                _channel = ChannelServices.GetChannel(IPCUtil.GetIPCChannelName(SSOConstant.DEFALUT_PGMCODE));

                if (_channel == null)
                {
                    _channel = new IpcChannel(IPCUtil.GetIPCChannelName(SSOConstant.DEFALUT_PGMCODE));
                    ChannelServices.RegisterChannel(_channel, false);
                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(SSOLogin),
                        SSOConstant.DEFALUT_PGMCODE, WellKnownObjectMode.Singleton);
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        /// <summary>
        /// Shut down the server side channel.
        /// </summary>
        public void Shutdown()
        {
            try
            {
                if (_channel != null)
                {
                    ChannelServices.UnregisterChannel(_channel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private bool IsCheckSSOChannel()
        //{
        //    IChannel channel = ChannelServices.GetChannel("ipc");
        //    IpcChannel ipcChannel = channel as IpcChannel;
        //    if (ipcChannel.ChannelData != null)
        //    {
        //        ChannelDataStore channelDataStore = ipcChannel.ChannelData as ChannelDataStore;

        //        if (channelDataStore.ChannelUris != null)
        //        {
        //            string targetUri = IPCUtil.GetIPCServerUrl(SSOConstant.DEFALUT_PGMCODE);
        //            int length = channelDataStore.ChannelUris.Length;
        //            for (int i = 0; i < length; i++)
        //            {
        //                string uri = channelDataStore.ChannelUris[i];

        //                if (targetUri == uri)
        //                {
        //                }
        //            }
        //        }
        //    }

        //    return false;
        //}
        #endregion
    }
}
