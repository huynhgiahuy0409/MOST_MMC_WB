using System;
using System.Collections.Generic;
using System.Linq;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Mobile.Common.Item.MobileContainerDetail;
using Tsb.Catos.Cm.Mobile.Common.Param.MobileContainerDetail;
using Tsb.Catos.Cm.Mobile.Service.MobileContainerDetail.Dao;
using Tsb.Catos.Cm.Net.Constants;
using Tsb.Catos.Cm.Net.MsgObjects.Quay;
using Tsb.Catos.Cm.Net.MsgObjects.Rail;
using Tsb.Catos.Cm.Net.MsgObjects.Yard;
using Tsb.Catos.Cm.Net.Objects;
using Tsb.Catos.Cm.Net.Util.Type;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Net.Types;

namespace Tsb.Catos.Cm.Mobile.Service.MobileContainerDetail
{
    public class MobileContainerDetailService : MessgaeService, IMobileContainerDetailService
    {
        #region CONST & FIELD AREA ********************************************

        private IMobileContainerDetailDao _mobileContainerDetailDao;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public IMobileContainerDetailDao MobileContainerDetailDao
        {
            get { return this._mobileContainerDetailDao; }
            set { this._mobileContainerDetailDao = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public MobileContainerDetailService()
        {
            ObjectID = "SVC-CT-CTMO-MobileContainerDetailService";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (SELECT) ******************************************

        public BaseResult GetYardContainerDetailItem(MobileContainerDetailParam param)
        {
            BaseResult resultObject = null;
            try
            {
                MsgObjectReceive receiveMsg =
                    SendMessage(CreateMsgY23(param), SyncTypes.Sync, typeof(RstY23)) as MsgObjectReceive;
                RstY23 rstY23 = receiveMsg as RstY23;

                if (receiveMsg is RstY23)
                {
                    if (rstY23.ErrorCode.Equals(MsgConstant.RST_ERR))
                    {
                        throw new TsbBizServiceException(ObjectID, "MSG_CTCM_00000", DefaultMessage.NON_REG_WRD + rstY23.ErrorDescription);
                    }
                    resultObject = BaseResult.CreateOkResult(this.ObjectID, GenerateDetailItem(rstY23), param);
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

        public BaseResult GetQuayContainerDetailItem(MobileContainerDetailParam param)
        {
            BaseResult resultObject = null;
            try
            {
                MsgObjectReceive receiveMsg =
                    SendMessage(CreateMsgQ23(param), SyncTypes.Sync, typeof(RstQ23)) as MsgObjectReceive;
                RstQ23 rstQ23 = receiveMsg as RstQ23;

                if (receiveMsg is RstQ23)
                {
                    if (rstQ23.ErrorCode.Equals(MsgConstant.RST_ERR))
                    {
                        throw new TsbBizServiceException(ObjectID, "MSG_CTCM_00000", DefaultMessage.NON_REG_WRD + rstQ23.ErrorDescription);
                    }
                    resultObject = BaseResult.CreateOkResult(this.ObjectID, GenerateDetailItem(rstQ23), param);
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

        // added by YoungOk Kim (2019.10.15) - Mantis 92188: [Tally] 컨테이너 정보에서 컨테이너 번호 뒷자리 4자리로 조회
        public BaseResult GetContainerList(MobileContainerDetailParam param)
        {
            BaseResult resultObject = null;
            try
            {
                BaseItemsList<ContainerInfoItem> containerList = MobileContainerDetailDao.GetContainerList(param);
                resultObject = BaseResult.CreateOkResult(ObjectID, containerList, param);
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

        // added by YoungOk Kim (2019.10.16) - Mantis 92189: [Tally] 컨테이너 정보에서 2 time shifting 표시
        public BaseResult GetShipPosition(MobileContainerDetailParam param)
        {
            BaseResult resultObject = null;
            try
            {
                string shipPosition = string.Empty;
                List<string> shipPositionList = MobileContainerDetailDao.GetShipPositionList(param);
                if (shipPositionList != null && shipPositionList.Any())
                {
                    shipPosition = shipPositionList[0] as string;
                }

                resultObject = BaseResult.CreateOkResult(ObjectID, shipPosition, param);
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

        #endregion METHOD AREA (SELECT) ***************************************

        #region METHOD AREA ***************************************************

        private MsgY23 CreateMsgY23(MobileContainerDetailParam param)
        {
            MsgY23 msgY23 = new MsgY23();

            msgY23.YardEquipment = param.EquipmentNo;
            msgY23.MMode = param.MMode;
            msgY23.CntrNo = param.ContainerNo;

            return msgY23;
        }

        public MobileContainerDetailItem GenerateDetailItem(RstY23 rstY23)
        {
            MobileContainerDetailItem item = new MobileContainerDetailItem();

            RstY11 rstY11 = rstY23.RstY11;
            item.CntrNo = rstY11.ContainerNo;
            item.VslCd = rstY11.VesselCode;
            item.VslNm = rstY11.VesselName;
            item.CallYear = rstY11.CallYear;
            item.CallSeq = rstY11.CallSeq;
            item.Vvd = string.IsNullOrEmpty(rstY11.VesselCode) ? string.Empty : rstY11.VesselCode + "-" + rstY11.CallYear + "-" + rstY11.CallSeq;
            item.ShipPosition = GetShipPosition(rstY11.YardJobCode, rstY11.CfSBaySRowSTier);
            item.JobCode = rstY11.YardJobCode;
            item.Block = rstY11.Block;
            item.Bay = rstY11.Bay;
            item.Row = rstY11.Row;
            item.Tier = rstY11.Tier;
            item.SzTp = rstY11.SzTp;
            item.SzTp2 = rstY11.SzTp2;
            item.IMDG = rstY11.IMDG;
            item.UNNo = rstY11.UNNO;
            item.Weight = rstY11.Weight;
            item.Pod = rstY11.POD;
            item.PtnrCode = rstY11.PartnerCode;
            item.FE = rstY11.FE;
            item.CargoType = rstY11.CargoType;
            item.Owner = rstY11.Owner;
            item.SealNo1 = rstY11.SealNo1;
            item.SealNo2 = rstY11.SealNo2;
            item.Remark = rstY11.Remark;
            item.UserVoy = rstY11.UserVoyage;
            item.DamageCondition = rstY11.DamageCondition;
            item.ContainerCondition = rstY11.ContainerCondition;
            item.InspectCheck = rstY11.InspectCheck;
            item.TerminalHold = rstY11.THoldCheck;
            item.CustomsHold = rstY11.CHoldCheck;
            item.SetTemp = rstY11.SetTemp;
            item.Fdest = rstY11.FDEST;
            item.Owner = rstY11.Owner;
            item.PuPlanDate2 = rstY11.PuPlanDate2; // added by YoungOk Kim (2020.04.08) - Mantis 105287: Show Special Color for Reservation Contaioners: TM /YQ/C3IT part
            item.TimeSlotNo2 = rstY11.TimeSlotNo2; // added by YoungOk Kim (2020.04.08) - Mantis 105287: Show Special Color for Reservation Contaioners: TM /YQ/C3IT part

            item.BundleList = GetBundleList(rstY23.BundleList);
            item.OverDimension = GetOverDimensionInfo(rstY11.OvHeight, rstY11.OvFore, rstY11.OvAfter, rstY11.OvStbd, rstY11.OvPort);
            item.SealChk = rstY11.SealCheck; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
            item.TimeSlotDate2 = rstY11.TimeSlotDate2; // added by JH.Tak (2022.09.05) WHL_UP OM-K-009

            // added by Ron (2023.08.24) [ADP-RT] 0151467: [Container Information] Add field Train Voyage
            if (rstY11.TransType.Equals("R"))
            {
                item.TrainVoyage = rstY11.TrainVoyage;
            }
            else if (rstY11.TransType2.Equals("R"))
            {
                item.TrainVoyage = rstY11.TrainVoyage2;
            }
            else
            {
                item.TrainVoyage = string.Empty;
            }

            return item;
        }

        private string GetShipPosition(string jobCode, string sBaySRowSTier)
        {
            string returnValue = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(sBaySRowSTier)) { return returnValue; }

                StrToken token = new StrToken(sBaySRowSTier, C3ITInfConstant.CHAR_DTL_PLUS);
                if (jobCode.Equals(CTBizConstant.YardJobType.LIFT_FOR_DISCHARGING))
                {
                    returnValue = string.IsNullOrEmpty(token.getToken(3)) ? string.Empty : token.getToken(3) + "-" + token.getToken(4) + "-" + token.getToken(5);

                }
                else
                {
                    returnValue = string.IsNullOrEmpty(token.getToken(0)) ? string.Empty : token.getToken(0) + "-" + token.getToken(1) + "-" + token.getToken(2);
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

            return returnValue;
        }

        private string GetBundleList(string bundleList)
        {
            string returnValue = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(bundleList)) return returnValue;

                StrToken token = new StrToken(bundleList, C3ITInfConstant.CHAR_DTL_PLUS);

                if (token != null)
                {
                    foreach (string temp in token.GetList())
                    {
                        StrToken token2 = new StrToken(temp, C3ITInfConstant.CHAR_DTL_CARET);
                        returnValue += token2.getToken(0) + " ";
                    }
                }
                //returnValue = string.IsNullOrEmpty(token.getToken(1)) ? token.getToken(0) : token.getToken(0) + "-" + token.getToken(1);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }

            return returnValue;
        }

        private MsgQ23 CreateMsgQ23(MobileContainerDetailParam param)
        {
            MsgQ23 msgQ23 = new MsgQ23();

            msgQ23.QcNo = param.EquipmentNo;
            msgQ23.MMode = param.MMode;
            msgQ23.CntrNo = param.ContainerNo;
            msgQ23.VslCd = param.VesselCode;
            msgQ23.CallYear = param.CallYear;
            msgQ23.CallSeq = param.CallSequence;

            return msgQ23;
        }

        public MobileContainerDetailItem GenerateDetailItem(RstQ23 rstQ23)
        {
            MobileContainerDetailItem item = new MobileContainerDetailItem();

            RstQ11 rstQ11 = rstQ23.RstQ11;
            item.CntrNo = rstQ11.ContainerNo;
            item.VslCd = rstQ11.VesselCode;
            item.CallYear = rstQ11.CallYear;
            item.CallSeq = rstQ11.CallSequence;
            item.Vvd = string.IsNullOrEmpty(rstQ11.VesselCode) ? string.Empty : rstQ11.VesselCode + "-" + rstQ11.CallYear + "-" + rstQ11.CallSequence;
            item.ShipPosition = rstQ11.SBay + "-" + rstQ11.SRow + "-" + rstQ11.STier + "/" + rstQ11.SHD;
            item.JobCode = rstQ11.QuayJobCode;
            item.Block = rstQ11.Block;
            item.Bay = rstQ11.Bay;
            item.Row = rstQ11.Row;
            item.Tier = rstQ11.Tier;
            item.SzTp = rstQ11.SizeType;
            item.SzTp2 = rstQ11.SizeType2;
            item.IMDG = rstQ11.Imdg;
            item.UNNo = rstQ11.Unno;
            item.Weight = rstQ11.GrsWgt;
            item.Pod = rstQ11.Pod1;
            item.PtnrCode = rstQ11.PatnerCode;
            item.FE = rstQ11.FE;
            item.CargoType = rstQ11.CargoType;
            item.Owner = rstQ11.Owner;
            item.SealNo1 = rstQ11.SealNo1;
            item.SealNo2 = rstQ11.SealNo2;
            item.Remark = rstQ11.Remark;
            item.BundleList = GetBundleList(rstQ23.BundleList);
            item.UserVoy = rstQ11.UserVoyage;
            item.SetTemp = rstQ11.SetTemp;
            item.Fdest = rstQ11.Fdest;
            item.OverDimension = GetOverDimensionInfo(rstQ11.OverHeight, rstQ11.OverFore, rstQ11.OverAft, rstQ11.OverStbd, rstQ11.OverPort);
            item.InspectCheck = rstQ11.InspectCheck;
            item.TerminalHold = rstQ11.TholdCheck;
            item.CustomsHold = rstQ11.CholdCheck;
            item.DamageCondition = rstQ11.DamageCondition;
            item.SealChk = rstQ11.SealCheck; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

            return item;
        }

        private string GetOverDimensionInfo(string ovHeight, string ovFore, string ovAfter, string ovStbd, string ovPort)
        {
            string returnValue = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(ovHeight) == false && Convert.ToInt32(ovHeight) > 0)
                {
                    returnValue += "H = " + ovHeight + ",";
                }

                if (string.IsNullOrEmpty(ovFore) == false && Convert.ToInt32(ovFore) > 0)
                {
                    returnValue += "F = " + ovFore + ",";
                }

                if (string.IsNullOrEmpty(ovAfter) == false && Convert.ToInt32(ovAfter) > 0)
                {
                    returnValue += "A = " + ovAfter + ",";
                }

                if (string.IsNullOrEmpty(ovStbd) == false && Convert.ToInt32(ovStbd) > 0)
                {
                    returnValue += "S = " + ovStbd + ",";
                }

                if (string.IsNullOrEmpty(ovPort) == false && Convert.ToInt32(ovPort) > 0)
                {
                    returnValue += "P = " + ovPort + ",";
                }

                if (string.IsNullOrEmpty(returnValue) == false)
                {
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
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

            return returnValue;
        }

        #endregion METHOD AREA ************************************************
    }
}
