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
* 2009.06.29    CHOI 1.0	First release.
* 
*/
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using System.Runtime.Remoting;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using System.Threading;
using System.Windows.Forms;
using Tsb.Fontos.Core.Message;

namespace Tsb.Fontos.Core.Service
{
    /// <summary>
    /// Dynamic Proxy class using .NET Remoting
    /// </summary>
    public class DynamicProxy : RealProxy, ITsbDynamicProxy, IRemotingTypeInfo, ITsbBaseObject
    {
        private object _proxyTarget;
        private InvocationDelegate _invocationHandler;
        private string _objectID;

        #region ITsbDynamicProxy Members

        /// <summary>
        /// The object to proxy
        /// </summary>
        public object ProxyTarget
        {
            get
            {
                return _proxyTarget;
            }
            set
            {
                _proxyTarget = value;
            }
        }

        /// <summary>
        /// Delegator of invocation handling
        /// </summary>
        public InvocationDelegate InvocationHandler
        {
            get
            {
                return _invocationHandler;
            }
            set
            {
                _invocationHandler = value;
            }
        }

        #endregion

        #region IRemotingTypeInfo Members

        /// <summary>
        /// Checks whether the proxy that represents the specified object type can be cast to the type 
        /// </summary>
        /// <param name="fromType">The type to cast to</param>
        /// <param name="o">The object for which to check casting</param>
        /// <returns>true if cast will succeed; otherwise, false.</returns>
        public bool CanCastTo(Type fromType, object o)
        {
            return true;
        }

        /// <summary>
        /// Not supported. Gets or sets the fully qualified type name of the server object 
        /// </summary>
        public string TypeName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ITsbBaseObject Members

        public string ObjectID
        {
            get
            {
                return this._objectID;
            }
            set
            {
                this._objectID = value;
            }
        }

        public ObjectType ObjectType
        {
            get { return ObjectType.DEFAULT; }
        }

        #endregion

        /// <summary>
		/// Default Constructor
		/// </summary>
		/// <param name="proxyTarget">The object to proxy</param>
        /// <param name="invocationHandler">Delegator of invocation handling</param>
        public DynamicProxy(object proxyTarget, InvocationDelegate invocationHandler)
            : base(typeof(ITsbDynamicProxy))
        {
			this._proxyTarget = proxyTarget;
			this._invocationHandler = invocationHandler;
		}


        /// <summary>
        /// CreateObjRef() isn't supported.
        /// </summary>
        /// <param name="type">The Type of the object that the new ObjRef will reference.</param>
        /// <returns>Information required to generate a proxy.</returns>
        public override ObjRef CreateObjRef(Type type)
        {
            return new ObjRef();
        }


        ///<summary>
        /// The reflective method for invoking methods
        /// </summary>
        /// <param name="msg">Communication data</param>
        /// <returns>Returnning Message</returns>
        public override IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage msg)
        {
            object returnedObj = null;
            ReturnMessage toRetunMessage = null;

            try
            {
                IMethodCallMessage methodMessage = new MethodCallMessageWrapper(msg as IMethodCallMessage);
                returnedObj = InvocationHandler(_proxyTarget, methodMessage.MethodBase, methodMessage.Args);
                toRetunMessage = new ReturnMessage(returnedObj, methodMessage.Args, methodMessage.ArgCount, methodMessage.LogicalCallContext, methodMessage);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00001", DefaultMessage.NON_REG_WRD+_proxyTarget.ToString());
            }
            return toRetunMessage;

        }
       
    }
}
