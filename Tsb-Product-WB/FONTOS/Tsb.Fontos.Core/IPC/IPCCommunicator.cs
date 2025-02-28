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
using Tsb.Fontos.Core.Objects;
using System.Reflection;
using System.Diagnostics;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.IPC
{
    public delegate void IPCMessageEventHandler(object message, int processID, params object[] args);

    [Serializable]
    public class IPCCommunicator : TsbBaseObject
    {
        public IPCCommunicator()
        {
            this.ObjectID = "ITM-FT-FTCO-IPC-IPCCommunicator";
        }

        public event IPCMessageEventHandler MessageSend;

        /// <summary>
        /// Sends the message for the specified messageType through ipc communication.  
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="processID"></param>
        public void SendMessage(IPCMessageTypes messageType, int processID)
        {
            SendMessageArgs(messageType, processID);
        }

        /// <summary>
        /// Sends the message for the specified messageType through ipc communication. 
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="processID"></param>
        /// <param name="args"></param>
        public void SendMessageArgs(IPCMessageTypes messageType, int processID, params object[] args)
        {
            if (MessageSend != null)
            {
                GeneralLogger.Info("The process[" + processID + "] sends the message \"" + messageType.ToString() + "\".");
                MessageSend(messageType, processID, args);
            }
        }

        /// <summary>
        /// Invokes the method with specified targetType, methodInfo, parameters that is received through ipc communication.
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="methodInfo"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object InvokeMethodWithTargetType(Type targetType, MethodInfo methodInfo, params object[] parameters)
        {
            object target = null;
            if (methodInfo.IsStatic == false)
            {
                target = Activator.CreateInstance(targetType);
            }
            GeneralLogger.Info("The process[" + Process.GetCurrentProcess().Id.ToString() + "] has been received the method(" + methodInfo.Name + ") call request.");
            return methodInfo.Invoke(target, parameters);
        }

        /// <summary>
        /// Invokes the method with specified target, methodInfo, parameters that is received through ipc communication.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="methodInfo"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object InvokeMethodWithTargetObject(object target, MethodInfo methodInfo, params object[] parameters)
        {
            if (methodInfo.IsStatic == true)
            {
                target = null;
            }
            GeneralLogger.Info("The process[" + Process.GetCurrentProcess().Id.ToString() + "] has been received the method(" + methodInfo.Name + ") call request.");
            return methodInfo.Invoke(target, parameters);
        }

        public object GetPropertyValue(object target, PropertyInfo propertyInfo)
        {
            GeneralLogger.Info("The process[" + Process.GetCurrentProcess().Id.ToString() + "] has been received the getting property(" + propertyInfo.Name + ") request.");
            return propertyInfo.GetValue(target);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void CheckIfAvailable()
        {
        }
    }
}
