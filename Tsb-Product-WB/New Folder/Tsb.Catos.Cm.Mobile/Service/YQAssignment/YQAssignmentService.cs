using System;
using Tsb.Catos.Cm.Mobile.Common.Item.YQAssignment;
using Tsb.Catos.Cm.Mobile.Common.Param.YQAssignment;
using Tsb.Catos.Cm.Mobile.Service.YQAssignment.Dao;
using Tsb.Catos.Cm.Net.MsgObjects.Common;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Net.Objects;

namespace Tsb.Catos.Cm.Mobile.Service.YQAssignment
{
    public class YQAssignmentService : MessgaeService, IYQAssignmentService
    {
        #region CONST & FIELD AREA ********************************************
        
        private IYQAssignmentDao _yqAssignmentDao;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public IYQAssignmentDao YQAssignmentDao
        {
            get { return _yqAssignmentDao; }
            set { _yqAssignmentDao = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public YQAssignmentService()
        {
            ObjectID = "SVC-CT-CTMO-YQAssignmentService";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (SELECT) ******************************************

        public BaseResult GetYQAssignment(YQAssignmentParam param)
        {
            BaseResult resultObject = null;
            try
            {
                //< modified by YoungOk Kim (2019.05.22) - Mantis 90900: YQ did not log in but show in TM
                //BaseItemsList<YQAssignmentItem> yqAssignmentItems = null;
                //string mMode = C3ITInfConstant.MSG_MODE_YQ;

                //IBaseMsgObjectSend msgC42 = GenerateMsgC42(mMode, param.EquNo);
                //MsgObjectReceive receiveMsg = SendMessage(msgC42, SyncTypes.Sync, typeof(RstC42)) as MsgObjectReceive;
                //RstC42 rstC42 = receiveMsg as RstC42;
                //if (receiveMsg is RstC42)
                //{
                //    if (rstC42 != null)
                //    {
                //        if (rstC42.ErrorCode.Contains(C3ITInfConstant.MSG_MODE_ERROR))
                //        {
                //            throw new TsbBizServiceException(ObjectID, rstC42.ErrorDescription);
                //        }
                //        else
                //        {
                //            yqAssignmentItems = new BaseItemsList<YQAssignmentItem>();
                //            foreach (RstC42Sub rstC42Sub in rstC42.SubMessages)
                //            {
                //                if (string.IsNullOrEmpty(rstC42Sub.Data1) == false)
                //                {
                //                    YQAssignmentItem yqAssignmentItem = new YQAssignmentItem();
                //                    yqAssignmentItem.EquNo = rstC42Sub.Data1;
                //                    BaseItemsList<EquipmentCoverageItem> equCoverages = new BaseItemsList<EquipmentCoverageItem>();
                //                    string lastPosition = "";
                //                    foreach (string token in rstC42Sub.Data2.Split('+'))
                //                    {
                //                        if (token.StartsWith("#"))
                //                        {
                //                            yqAssignmentItem.WorkAssign = int.Parse(token.Substring(1, token.Length - 1));
                //                        }
                //                        else if (token.StartsWith("$"))
                //                        {
                //                            EquipmentCoverageItem equCoverageItem = new EquipmentCoverageItem();
                //                            equCoverageItem.EquNo = rstC42Sub.Data1;
                //                            string[] coverage = token.Substring(1, token.Length - 1).Split('-');
                //                            equCoverageItem.Block = coverage[0];
                //                            if (coverage.Length == 3)
                //                            {
                //                                equCoverageItem.FromBayIndex = int.Parse(coverage[1]);
                //                                equCoverageItem.ToBayIndex = int.Parse(coverage[2]);
                //                            }
                //                            else if (coverage.Length == 4)
                //                            {
                //                                equCoverageItem.FromBayIndex = int.Parse(coverage[1]);
                //                                equCoverageItem.ToBayIndex = int.Parse(coverage[2]);
                //                                equCoverageItem.AssignType = coverage[3];
                //                            }

                //                            if (!String.IsNullOrEmpty(coverage[0]))
                //                            {
                //                                equCoverages.Add(equCoverageItem);
                //                            }
                //                        }
                //                        else if (token.StartsWith("%"))
                //                        {
                //                            lastPosition = token.Substring(1, token.Length - 1);
                //                        }
                //                        yqAssignmentItem.EquipmentCoverages = equCoverages;
                //                        yqAssignmentItem.LastPosition = lastPosition;
                //                    }
                //                    yqAssignmentItems.Add(yqAssignmentItem);
                //                }
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    throw new TsbBizServiceException(this.ObjectID, receiveMsg.MessageID);
                //}
                BaseItemsList<YQAssignmentItem> yqAssignmentItems = new BaseItemsList<YQAssignmentItem>();

                BaseItemsList<EquipmentCoverageItem> equCoverages = YQAssignmentDao.GetYQCoverageList(param);
                
                YQAssignmentItem yqAssignmentItem = new YQAssignmentItem();
                yqAssignmentItem.EquNo = param.EquNo;
                yqAssignmentItem.WorkAssign = 0;
                yqAssignmentItem.EquipmentCoverages = equCoverages;

                yqAssignmentItems.Add(yqAssignmentItem);
                //>

                if (yqAssignmentItems != null)
                {
                    resultObject = BaseResult.CreateOkResult(this.ObjectID, yqAssignmentItems, param);
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

        #endregion METHOD AREA (SELECT) ***************************************

        #region METHOD AREA ***************************************************

        private IBaseMsgObjectSend GenerateMsgC42(string mMode, string equNo)
        {
            MsgC42 msgC42 = new MsgC42();
            msgC42.MMode = mMode;
            //msgC42.EquipmentNo = "^M01";
            msgC42.EquipmentNo = equNo;
            return msgC42;
        }

        #endregion METHOD AREA ************************************************
    }
}
