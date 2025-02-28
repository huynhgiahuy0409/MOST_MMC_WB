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

namespace Tsb.Fontos.Core.Security.SSO.IPC
{
    public class IPCUtil
    {
        #region METHOD AREA ************************************
        /// <summary>
        /// Gets IPC Channel Name
        /// </summary>
        /// <param name="pgmCode"></param>
        /// <returns></returns>
        public static string GetIPCChannelName(string pgmCode)
        {
            return SSOConstant.IPC1 + pgmCode;
        }

        /// <summary>
        /// Gets IPC URL
        /// </summary>
        /// <param name="pgmCode"></param>
        /// <returns></returns>
        public static string GetIPCUrl(string pgmCode)
        {
            string url =
                string.Concat(SSOConstant.IPC_BASE_URL, IPCUtil.GetIPCChannelName(pgmCode), SSOConstant.IPC_SEPERATOR, pgmCode);
            return url;
        }

        /// <summary>
        /// Gets IPC URL
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="objectURI"></param>
        /// <returns></returns>
        public static string GetIPCUrl(string channelName, string objectURI)
        {
            string url =
                string.Concat(SSOConstant.IPC_BASE_URL, channelName, SSOConstant.IPC_SEPERATOR, objectURI);
            return url;
        }

        /// <summary>
        /// Gets IPC URL
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="portName"></param>
        /// <param name="objectURI"></param>
        /// <returns></returns>
        public static string GetIPCUrl(string channelName, string portName, string objectURI)
        {
            string url =
                string.Concat(SSOConstant.IPC_BASE_URL, channelName, SSOConstant.IPC_SEPERATOR, portName, SSOConstant.IPC_SEPERATOR, objectURI);
            return url;
        }

        /// <summary>
        /// Gets IPC URL.
        /// </summary>
        /// <param name="pgmCode"></param>
        /// <returns></returns>
        public static string GetIPCServerUrl(string pgmCode)
        {
            string url =
                string.Concat(SSOConstant.IPC_BASE_URL, IPCUtil.GetIPCChannelName(pgmCode));
            return url;
        }
        #endregion

        #region Get SSO Login Object
        /// <summary>
        /// Gets the SSOLogin object from the registered ipc channel.
        /// </summary>
        /// <param name="debugger"></param>
        /// <returns></returns>
        public static SSOLogin GetSSOIPCServerLoginObject(bool debugger)
        {
            if (debugger == true)
            {
                return GetSSOIPCServerLoginObjectWithDebugger();
            }
            else
            {
                return GetSSOIPCServerLoginObjectWithoutDebugger();
            }
        }

        /// <summary>
        /// Gets the SSOLogin object from the registered ipc channel.
        /// Even though a program start as debugging mode, the debugger can access this method.
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerHidden]
        public static SSOLogin GetSSOIPCServerLoginObjectWithoutDebugger()
        {
            SSOLogin ipcServerLoginObj = null;

            try
            {
                System.Type typeofIpcServerLogin = typeof(SSOLogin);
                string processID = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
                string serverObjectURI = Tsb.Fontos.Core.Security.SSO.IPC.IPCUtil.GetIPCUrl(Tsb.Fontos.Core.IPC.IPCConstants.IPC_SERVER_CHANNEL_NAME, SSOConstant.SSOLOGIN_PREFIX_OBJECT_URI + processID);
                ipcServerLoginObj = Activator.GetObject(typeofIpcServerLogin, serverObjectURI) as SSOLogin;
                ipcServerLoginObj.CheckIfAvailable();
            }
            catch (Exception)
            {
                ipcServerLoginObj = null;
            }

            return ipcServerLoginObj;
        }

        /// <summary>
        /// Gets the SSOLogin object from the registered ipc channel.
        /// When a program start as debugging mode, the debugger can access this method.
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public static SSOLogin GetSSOIPCServerLoginObjectWithDebugger()
        {
            SSOLogin ipcServerLoginObj = null;

            try
            {
                System.Type typeofIpcServerLogin = typeof(SSOLogin);
                string processID = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
                string serverObjectURI = Tsb.Fontos.Core.Security.SSO.IPC.IPCUtil.GetIPCUrl(Tsb.Fontos.Core.IPC.IPCConstants.IPC_SERVER_CHANNEL_NAME, SSOConstant.SSOLOGIN_PREFIX_OBJECT_URI + processID);
                ipcServerLoginObj = Activator.GetObject(typeofIpcServerLogin, serverObjectURI) as SSOLogin;
                ipcServerLoginObj.CheckIfAvailable();
            }
            catch (Exception)
            {
                ipcServerLoginObj = null;
            }

            return ipcServerLoginObj;
        }
        #endregion
        
        #region Get SSO Server Communicator Object
        /// <summary>
        /// Gets the SSO IPCServerCommunicator object for the specified processID from the registered ipc channel.
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="debugger"></param>
        /// <returns></returns>
        public static Tsb.Fontos.Core.IPC.IPCServerCommunicator GetSSOIPCServerCommunicatorObject(int processID, bool debugger)
        {
            if (debugger == true)
            {
                return GetSSOIPCServerCommunicatorObjectWithDebugger(processID);
            }
            else
            {
                return GetSSOIPCServerCommunicatorObjectWithoutDebugger(processID);
            }
        }

        /// <summary>
        /// Gets the SSO IPCServerCommunicator object for the specified processID from the registered ipc channel.
        /// Even though a program start as debugging mode, the debugger can access this method.
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerHidden]
        public static Tsb.Fontos.Core.IPC.IPCServerCommunicator GetSSOIPCServerCommunicatorObjectWithoutDebugger(int processID)
        {
            Tsb.Fontos.Core.IPC.IPCServerCommunicator ipcServerCommunicatorObj = null;

            try
            {
                System.Type typeofIpcServerCommunicator = typeof(Tsb.Fontos.Core.IPC.IPCServerCommunicator);
                string serverObjectURI = Tsb.Fontos.Core.Security.SSO.IPC.IPCUtil.GetIPCUrl(Tsb.Fontos.Core.IPC.IPCConstants.IPC_SERVER_CHANNEL_NAME, Tsb.Fontos.Core.IPC.IPCConstants.IPC_SERVER_COMMUNICATOR_OBJECT_PREFIX + (processID.ToString()));
                ipcServerCommunicatorObj = Activator.GetObject(typeofIpcServerCommunicator, serverObjectURI) as Tsb.Fontos.Core.IPC.IPCServerCommunicator;
                ipcServerCommunicatorObj.CheckIfAvailable();
            }
            catch (Exception)
            {
                ipcServerCommunicatorObj = null;
            }

            return ipcServerCommunicatorObj;
        }

        /// <summary>
        /// Gets the SSO IPCServerCommunicator object for the specified processID from the registered ipc channel.
        /// When a program start as debugging mode, the debugger can access this method.
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public static Tsb.Fontos.Core.IPC.IPCServerCommunicator GetSSOIPCServerCommunicatorObjectWithDebugger(int processID)
        {
            Tsb.Fontos.Core.IPC.IPCServerCommunicator ipcServerCommunicatorObj = null;

            try
            {
                System.Type typeofIpcServerCommunicator = typeof(Tsb.Fontos.Core.IPC.IPCServerCommunicator);
                string serverObjectURI = Tsb.Fontos.Core.Security.SSO.IPC.IPCUtil.GetIPCUrl(Tsb.Fontos.Core.IPC.IPCConstants.IPC_SERVER_CHANNEL_NAME, Tsb.Fontos.Core.IPC.IPCConstants.IPC_SERVER_COMMUNICATOR_OBJECT_PREFIX + (processID.ToString()));
                ipcServerCommunicatorObj = Activator.GetObject(typeofIpcServerCommunicator, serverObjectURI) as Tsb.Fontos.Core.IPC.IPCServerCommunicator;
                ipcServerCommunicatorObj.CheckIfAvailable();
            }
            catch (Exception)
            {
                ipcServerCommunicatorObj = null;
            }

            return ipcServerCommunicatorObj;
        }
        #endregion

        #region Get SSO Client Communicator Object
        /// <summary>
        /// Gets the SSO IPCClientCommunicator object for the specified processID from the registered ipc channel.
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="debugger"></param>
        /// <returns></returns>
        public static Tsb.Fontos.Core.IPC.IPCClientCommunicator GetSSOIPCClientCommunicatorObject(int processID, bool debugger)
        {
            if (debugger == true)
            {
                return GetSSOIPCClientCommunicatorObjectWithDebugger(processID);
            }
            else
            {
                return GetSSOIPCClientCommunicatorObjectWithoutDebugger(processID);
            }
        }

        /// <summary>
        /// Gets the SSO IPCClientCommunicator object for the specified processID from the registered ipc channel.
        /// Even though a program start as debugging mode, the debugger can access this method.
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerHidden]
        public static Tsb.Fontos.Core.IPC.IPCClientCommunicator GetSSOIPCClientCommunicatorObjectWithoutDebugger(int processID)
        {
            Tsb.Fontos.Core.IPC.IPCClientCommunicator ipcClientCommunicatorObj = null;

            try
            {
                System.Type typeofIpcClientCommunicator = typeof(Tsb.Fontos.Core.IPC.IPCClientCommunicator);
                string clientObjectURI = Tsb.Fontos.Core.Security.SSO.IPC.IPCUtil.GetIPCUrl(Tsb.Fontos.Core.IPC.IPCConstants.IPC_CLIENT_CHANNEL_NAME + (processID.ToString()), Tsb.Fontos.Core.IPC.IPCConstants.IPC_CLIENT_COMMUNICATOR_OBJECT_PREFIX + (processID.ToString()));
                ipcClientCommunicatorObj = Activator.GetObject(typeofIpcClientCommunicator, clientObjectURI) as Tsb.Fontos.Core.IPC.IPCClientCommunicator;
                ipcClientCommunicatorObj.CheckIfAvailable();
            }
            catch (Exception)
            {
                ipcClientCommunicatorObj = null;
            }

            return ipcClientCommunicatorObj;
        }

        /// <summary>
        /// Gets the SSO IPCClientCommunicator object for the specified processID from the registered ipc channel.
        /// When a program start as debugging mode, the debugger can access this method.
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public static Tsb.Fontos.Core.IPC.IPCClientCommunicator GetSSOIPCClientCommunicatorObjectWithDebugger(int processID)
        {
            Tsb.Fontos.Core.IPC.IPCClientCommunicator ipcClientCommunicatorObj = null;

            try
            {
                System.Type typeofIpcClientCommunicator = typeof(Tsb.Fontos.Core.IPC.IPCClientCommunicator);
                string clientObjectURI = Tsb.Fontos.Core.Security.SSO.IPC.IPCUtil.GetIPCUrl(Tsb.Fontos.Core.IPC.IPCConstants.IPC_CLIENT_CHANNEL_NAME + (processID.ToString()), Tsb.Fontos.Core.IPC.IPCConstants.IPC_CLIENT_COMMUNICATOR_OBJECT_PREFIX + (processID.ToString()));
                ipcClientCommunicatorObj = Activator.GetObject(typeofIpcClientCommunicator, clientObjectURI) as Tsb.Fontos.Core.IPC.IPCClientCommunicator;
                ipcClientCommunicatorObj.CheckIfAvailable();
            }
            catch (Exception)
            {
                ipcClientCommunicatorObj = null;
            }

            return ipcClientCommunicatorObj;
        }
        #endregion

        #region Register SSO Client Communicator Object
        /// <summary>
        /// Registers a ipc channel and registers SSO IPCClientCommunicator object to the ipc channel for the specified processID.
        /// </summary>
        /// <param name="processID"></param>
        public static void RegisterSSOIPCClientCommunicatorObject(int processID)
        {
            try
            {
                BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
                provider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                System.Collections.IDictionary property = new System.Collections.Hashtable();
                property["portName"] = Tsb.Fontos.Core.IPC.IPCConstants.IPC_CLIENT_CHANNEL_NAME + (processID.ToString());

                System.Runtime.Remoting.Channels.IChannel channel = new System.Runtime.Remoting.Channels.Ipc.IpcChannel(property, null, provider);
                if (ChannelServices.GetChannel(channel.ChannelName) == null)
                {
                    System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(channel, false);
                }
                System.Type typeofIPCClientCommunicator = typeof(Tsb.Fontos.Core.IPC.IPCClientCommunicator);
                string clientObjectURI = Tsb.Fontos.Core.IPC.IPCConstants.IPC_CLIENT_COMMUNICATOR_OBJECT_PREFIX + (processID.ToString());
                System.Runtime.Remoting.RemotingConfiguration.RegisterWellKnownServiceType(typeofIPCClientCommunicator, clientObjectURI, System.Runtime.Remoting.WellKnownObjectMode.SingleCall);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
