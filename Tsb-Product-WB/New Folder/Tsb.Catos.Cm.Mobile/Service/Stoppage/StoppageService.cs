using System;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Item.Stoppage;
using Tsb.Catos.Cm.Mobile.Common.Param;
using Tsb.Catos.Cm.Mobile.Common.Param.Stoppage;
using Tsb.Catos.Cm.Mobile.Service.Stoppage.Dao;
using Tsb.Catos.Cm.Net.Constants;
using Tsb.Catos.Cm.Net.MsgObjects.Common;
using Tsb.Catos.Cm.Net.Objects;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Net.Objects;
using Tsb.Fontos.Net.Types;

namespace Tsb.Catos.Cm.Mobile.Service.Stoppage
{
    public class StoppageService : MessgaeService, IStoppageService
    {
        #region CONST & FIELD AREA ********************************************

        private IStoppageDao _stoppageDao;

        #endregion CONST & FIELD AREA *****************************************

        #region INITIALIZATION AREA *******************************************

        public StoppageService()
        {
            ObjectID = "SVC-CT-CTMO-StoppageService";
        }

        public IStoppageDao StoppageDao
        {
            get { return _stoppageDao; }
            set { _stoppageDao = value; }
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (SELECT) ******************************************

        public BaseResult GetEquipmentStoppageReasonAll(StoppageReasonParam param)
        {
            BaseResult resultObject = null;
            try
            {
                StoppageReasonItemList stoppageReasonItemList =
                    StoppageDao.GetEquipmentStoppageReasonAll(param);
                resultObject = BaseResult.CreateOkResult(ObjectID, stoppageReasonItemList, param);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
            return resultObject;
        }

        public BaseResult GetEquipmentStoppageReason(StoppageReasonParam param)
        {
            BaseResult resultObject = null;
            try
            {
                StoppageReasonItemList stoppageReasonItemList = StoppageDao.GetEquipmentStoppageReason(param);
                resultObject = BaseResult.CreateOkResult(ObjectID, stoppageReasonItemList, param);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
            return resultObject;
        }

        #endregion METHOD AREA (SELECT) ***************************************

        #region METHOD AREA (CUD) *********************************************

        public BaseResult UpdateStoppageInfoOfEquipment(CudServiceParam param)
        {
            BaseResult resultObject = null;
            try
            {
                StoppageItem item = param.ParamDataItem as StoppageItem;

                IBaseMsgObjectSend msgC23 = GenerateMessageC23(item, true);
                MsgObjectReceive receiveMsg =
                    SendMessage(msgC23, SyncTypes.Sync, typeof(RstC23), typeof(RstC02)) as MsgObjectReceive;
                if (receiveMsg is RstC23)
                {
                    RstC23 rstC23 = receiveMsg as RstC23;
                    if (rstC23.ErrorCode.Equals(MsgConstant.MMODE_ER))
                    {
                        throw new TsbBizBaseException(ObjectID, "MSG_CTTM_UTIL-00013", DefaultMessage.NON_REG_WRD + rstC23.ErrorDescription);
                    }
                }
                if (receiveMsg is RstC02)
                {
                    RstC02 rstC02 = receiveMsg as RstC02;
                    if (item != null)
                    {
                        item.Status = rstC02.EquipmentStatus;
                        item.Active = rstC02.Pause.Equals(CTBizConstant.YesNo.YES) ? false : true;

                        resultObject = BaseResult.CreateOkResult(ObjectID, item, param);
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
            return resultObject;
        }

        #endregion METHOD AREA (CUD) ******************************************

        #region METHOD AREA ***************************************************

        public MsgObjectSend GenerateMessageC23(StoppageItem stoppageItem, bool pauseYn)
        {
            MsgC23 msgC23 = new MsgC23();
            try
            {
                msgC23.EquipmentNo = stoppageItem.EquipmentNo;
                if (pauseYn)
                {
                    msgC23.Pause = stoppageItem.Active ? CTBizConstant.YesNo.NO : CTBizConstant.YesNo.YES;
                }
                msgC23.Remark = stoppageItem.Remark;
                msgC23.VesselCode = stoppageItem.VesselCode;
                msgC23.CallYear = stoppageItem.CallYear;
                msgC23.CallSequence = stoppageItem.CallSeq;
                msgC23.EquipmentStatus = stoppageItem.Status;
                msgC23.StaffCode_driverCode = stoppageItem.StaffCd;

                if (stoppageItem.ResumeATime == null)
                {
                    msgC23.StoppageCode = stoppageItem.StopReason;

                    string stopPTime = GetDay(stoppageItem.StopPTime, MsgConstant.C3IT_DATE_TIME_PATTERN);
                    string stopSTime = GetDay(stoppageItem.StopSTime, MsgConstant.C3IT_DATE_TIME_PATTERN);
                    string resumePTime = GetDay(stoppageItem.ResumePTime, MsgConstant.C3IT_DATE_TIME_PATTERN);
                    msgC23.TransTime_predictTime = stopSTime + C3ITInfConstant.CHAR_DTL_PLUS +
                                                   resumePTime + C3ITInfConstant.CHAR_DTL_PLUS +
                                                   stopPTime;
                }

                else
                {
                    msgC23.StoppageCode = MOCommonConstants.EQU_STOPPAGE_RESUME;
                    string resumeATime = GetDay(stoppageItem.ResumeATime, MsgConstant.C3IT_DATE_TIME_PATTERN);
                    msgC23.TransTime_predictTime = resumeATime + C3ITInfConstant.CHAR_DTL_PLUS + "";
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
            return msgC23;
        }

        private string GetDay(DateTime? dateTime, string pattern)
        {
            return dateTime == null ? string.Empty : ((DateTime)dateTime).ToString(pattern);
        }

        #endregion METHOD AREA ************************************************
    }
}
