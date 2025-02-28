using System;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Core.Resources;

namespace Tsb.Catos.Cm.Mobile.Win.YtCPosList
{
    public class YtCPosListController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-YtCPosListController";
        private readonly string NONE_YT = "[NONE]";
        private YtCPosListInterface _formView;
        private BaseItemsList<EquipmentItem> _ytList = null;

        public readonly string CHASSIS_POSITION_FORE = "F";
        public readonly string CHASSIS_POSITION_AFTER = "A";
        public readonly string CHASSIS_POSITION_MIDDLE = "M";

        private string _ytNo = string.Empty;
        private string _cPos = string.Empty;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public BaseItemsList<EquipmentItem> YtList // added by jaeok (2016.09.20) Mantis 54601: RMRC-011- "NONE" YT selection unavailable when completing job in RMRC
        {
            set
            {
                _ytList = value;

                EquipmentItem noneEqu = new EquipmentItem(); // add 'NONE'
                noneEqu.Name = string.Empty;
                _ytList.Insert(0, noneEqu);
            }
            get { return _ytList; }
        }

        public BaseItemsList<EquipmentItem> YardTrucksInQueue { get; set; }
        public string SelectedCntrNo { get; set; }
        public string SelectedJobCode { get; set; }

        public string YtNo 
        {
            get { return _ytNo; }
            set 
            { 
                _ytNo = value;
                SelectedYtNo = _ytNo;
            }
        }
        public string CPos
        {
            get { return _cPos; }
            set
            {
                _cPos = value;
                SelectedCPos = _cPos;
            }
        }
        public string SelectedYtNo { get; set; }
        public string SelectedCPos { get; set; }

        //added by bkkang(2020.01.09)
        public BaseItemsList<EquipmentItem> YardTruckAll { get; set; }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public YtCPosListController()
        {
            ObjectID = OBJECT_ID;
        }

        public YtCPosListController(YtCPosListInterface view)
        {
            ObjectID = OBJECT_ID;
            _formView = view;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA ***************************************************

        public void DoRetrieveData()
        {
            BaseItemsList<ItemListControlItem> itemList = null;

            try
            {
                _ytList = this.YardTrucksInQueue;

                if (_ytList != null)
                {
                    itemList = new BaseItemsList<ItemListControlItem>();
                    foreach (EquipmentItem item in _ytList)
                    {
                        ItemListControlItem displayItem = new ItemListControlItem();
                        displayItem.Code = item.Name;
                        displayItem.CodeName = item.Name;

                        // modified by jaeok (2016.09.20) Mantis 54601: RMRC-011- "NONE" YT selection unavailable when completing job in RMRC
                        //displayItem.TextValue = item.Name;
                        if (item.Name.Length == 0)
                        {
                            displayItem.TextValue = NONE_YT;
                        }
                        else
                        {
                            displayItem.TextValue = item.Name;
                        }

                        // added by YoungOk Kim (2019.12.27) - Mantis 104219: [YQ] 단작업을 위한 기능 추가 수동 배차 시 상차된 YT는 빨간색 표시
                        if (string.IsNullOrEmpty(item.Remark) == false && item.Remark.StartsWith(CTBizConstant.YesNo.YES) == true)
                        {
                            displayItem.CustomStyleName = "biz_active_btn_list_for_ytList";
                        }

                        itemList.Add(displayItem);
                    }
                    _formView.SetYtItemList(itemList);

                    DoRetrieveCPosData(SelectedCPos);
                    _formView.SetSelectedJobInfo(SelectedCntrNo, SelectedJobCode, SelectedYtNo, SelectedCPos);
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
        }

        public void DoRetrieveCPosData(string cPos)
        {
            BaseItemsList<ItemListControlItem> itemList = new BaseItemsList<ItemListControlItem>();

            try
            {
                if (cPos.Equals(CHASSIS_POSITION_MIDDLE) == true)
                {
                    ItemListControlItem displayItem = new ItemListControlItem();
                    displayItem.Code = CHASSIS_POSITION_MIDDLE;
                    displayItem.CodeName = CHASSIS_POSITION_MIDDLE;
                    displayItem.TextValue = ResourceFactory.GetResource().GetLabel("WRD_CTYQ_CPOS_MIDDLE");;
                    itemList.Add(displayItem);
                }
                else
                {
                    ItemListControlItem displayItem = new ItemListControlItem();
                    displayItem.Code = CHASSIS_POSITION_FORE;
                    displayItem.CodeName = CHASSIS_POSITION_FORE;
                    displayItem.TextValue = ResourceFactory.GetResource().GetLabel("WRD_CTYQ_CPOS_FORE");;
                    itemList.Add(displayItem);

                    displayItem = new ItemListControlItem();
                    displayItem.Code = CHASSIS_POSITION_AFTER;
                    displayItem.CodeName = CHASSIS_POSITION_AFTER;
                    displayItem.TextValue = ResourceFactory.GetResource().GetLabel("WRD_CTYQ_CPOS_AFTER");;
                    itemList.Add(displayItem);
                }

                _formView.SetCPosItemList(itemList);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        public void setSelectedYt(string code)
        {
            try
            {
                if (_ytList == null)
                {
                    return;
                }

                this.SelectedYtNo = _ytList.GetItem(code).Name;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }


        public void setSelectedCPos(string code)
        {
            try
            {
                this.SelectedCPos = code;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        #endregion METHOD AREA ************************************************
    }
}
