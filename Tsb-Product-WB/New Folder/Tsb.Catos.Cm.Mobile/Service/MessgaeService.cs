using System;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Net.Application.C3IT;
using Tsb.Catos.Cm.Net.Constants;
using Tsb.Catos.Cm.Net.MsgObjects.Common;
using Tsb.Catos.Cm.Net.Objects;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Logging;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Net.Factory;
using Tsb.Fontos.Net.Handler;
using Tsb.Fontos.Net.Objects;
using Tsb.Fontos.Net.Types;

namespace Tsb.Catos.Cm.Mobile.Service
{
    public class MessgaeService : BaseService
    {
        #region CONST & FIELD AREA ********************************************

        protected readonly ITsbLog Logger = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IInterfaceHandler _c3InterfaceHandler;

        private static object lockObject = new object();

        #endregion CONST & FIELD AREA *****************************************

        #region INITIALIZATION AREA *******************************************

        public MessgaeService()
            : base()
        {
            this.ObjectID = "SVC-CT-CTMO-MessgaeService";
            this.ObjectType = Tsb.Fontos.Core.Objects.ObjectType.SERVICE;
            this.ClassName = this.GetType().FullName;
            this.Name = this.GetType().Name;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA ***************************************************

        protected IBaseMsgObject SendMessage(IBaseMsgObjectSend msgObjectSend, SyncTypes syncType)
        {
            IBaseMsgObject baseMsgObject = null;

            try
            {
                baseMsgObject = SendMessage(msgObjectSend, syncType, null);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }

            return baseMsgObject;
        }

        protected IBaseMsgObject SendMessage(IBaseMsgObjectSend msgObjectSend, SyncTypes syncType, params Type[] msgObjectReturnType)
        {
            IBaseMsgObject baseMsgObject = null;
            try
            {
                lock (lockObject)
                {
                    if (_c3InterfaceHandler == null)
                    {
                        _c3InterfaceHandler = InfHandlerFactory.CreateInfHandler(CTInterfaceSpec.INF_TARGET_NAME_C3IT_SERVICE);
                    }

                    if (NetConnectionStatusHandler.Instance.IsConnectedC3IT)
                    {
                        baseMsgObject = _c3InterfaceHandler.SendMessage(msgObjectSend, syncType, msgObjectReturnType) as MsgObjectReceive;
                    }
                    else
                    {
                        if (C3ITApplication.LoginMsg != null)
                        {
                            throw new TsbBaseException(this.ObjectID, "MSG_CTMO_00004");
                        }
                        else 
                        {
                            bool isReconnected = Reconnect();
                            if (isReconnected == false)
                            {
                                throw new TsbBaseException(this.ObjectID, "MSG_CTMO_00004");
                            }
                            else
                            {
                                baseMsgObject = _c3InterfaceHandler.SendMessage(msgObjectSend, syncType, msgObjectReturnType) as MsgObjectReceive;
                            }
                        }
                    }
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }

            return baseMsgObject;
        }

        private bool Reconnect()
        {
            bool isSuccess = false;
            try
            {
                lock (lockObject)
                {
                    if (_c3InterfaceHandler == null)
                    {
                        _c3InterfaceHandler = InfHandlerFactory.CreateInfHandler(CTInterfaceSpec.INF_TARGET_NAME_C3IT_SERVICE);
                    }

                    IBaseMsgObject baseMsgObject = _c3InterfaceHandler.SendMessage(new MsgAK(), SyncTypes.ASync, null) as MsgObjectReceive;
                    if (baseMsgObject != null) 
                    {
                        isSuccess = true;
                    }                    
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex.Message);
            }

            return isSuccess;
        }

        #endregion METHOD AREA ************************************************
    }
}
