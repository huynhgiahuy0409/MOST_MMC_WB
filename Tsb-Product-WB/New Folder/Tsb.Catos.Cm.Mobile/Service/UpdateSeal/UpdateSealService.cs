using System;
using Tsb.Catos.Cm.Mobile.Common.Param.UpdateSeal;
using Tsb.Catos.Cm.Net.Constants;
using Tsb.Catos.Cm.Net.MsgObjects.Common;
using Tsb.Catos.Cm.Net.Objects;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Net.Types;

namespace Tsb.Catos.Cm.Mobile.Service.UpdateSeal
{
    public class UpdateSealService : MessgaeService, IUpdateSealService
    {
        #region CONST & FIELD AREA ********************************************
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************
        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public UpdateSealService()
        {
            ObjectID = "SVC-CT-CTMO-UpdateSealService";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (CUD) *********************************************

        public BaseResult UpdateSeal(UpdateSealParam param)
        {
            BaseResult resultObject = null;
            try
            {
                MsgObjectReceive receiveMsg =
                    SendMessage(CreateMsgC52(param), SyncTypes.Sync, typeof(RstC52)) as MsgObjectReceive;
                RstC52 rstC52 = receiveMsg as RstC52;

                if (receiveMsg is RstC52)
                {
                    if (rstC52.ErrorCode.Equals(MsgConstant.RST_ERR))
                    {
                        throw new TsbBizServiceException(ObjectID, "MSG_CTCM_00000", DefaultMessage.NON_REG_WRD + rstC52.ErrorDescription);
                    }
                    resultObject = BaseResult.CreateOkResult(this.ObjectID, null, param);
                }
                else
                {
                    throw new TsbBizServiceException(this.ObjectID, receiveMsg.MessageID);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }
            return resultObject;
        }

        #endregion METHOD AREA (CUD) ******************************************

        #region METHOD AREA ***************************************************

        private MsgC52 CreateMsgC52(UpdateSealParam param)
        {
            MsgC52 msgC52 = new MsgC52();

            msgC52.EquipmentNo = param.UpdateSealItem.EquipmentNo;
            msgC52.MMode = param.MMode;
            msgC52.CntrNo = param.UpdateSealItem.CntrNo;
            msgC52.JobCd = param.UpdateSealItem.JobCode;
            msgC52.VslCd = param.UpdateSealItem.VslCd;
            msgC52.CallYear = param.UpdateSealItem.CallYear;
            msgC52.CallSeq = param.UpdateSealItem.CallSeq;
            msgC52.SealNo1 = param.UpdateSealItem.SealNo1;
            msgC52.SealNo2 = param.UpdateSealItem.SealNo2;
            msgC52.SealNo3 = param.UpdateSealItem.SealNo3;
            msgC52.SealChk = param.UpdateSealItem.SealChk; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
            msgC52.StaffCd = param.StaffCd;

            return msgC52;
        }

        #endregion METHOD AREA ************************************************
    }
}
