using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Item.BlockBayList;
using Tsb.Catos.Cm.Mobile.Common.Item.YQAssignment;
using Tsb.Catos.Cm.Mobile.Common.Param.YQAssignment;
using Tsb.Catos.Cm.Mobile.Service.YQAssignment;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Observer;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Win.Message;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.BlockBayList
{
    public class BlockBayListController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************
        
        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-BlockBayListController";

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        private BlockBayListInterface FormView { get; set; }
        private IYQAssignmentService YqAssignmentService { get; set; }
        private DataSyncAgent DataSyncAgent { get; set; }

        public BaseDataItem SelectedBlockAreaItem { get; set; }
        public BaseDataItem SelectedBayRowItem { get; set; }
        public BlockItemList BlockItems { get; set; }
        public AreaItemList AreaItems { get; set; }
        public EquipmentItem EquItem { get; set; }
        public BlockBayListViewItem InputValue { get; set; }

        public string CurrentBlockArea { get; set; }
        public string CurrentBayRow { get; set; }

        public BaseItemsList<BayItem> BayItemList { get; set; }
        public BaseItemsList<RowItem> RowItemList { get; set; }

        public bool IsAlwaysShowAll { get; set; }
        public bool IsSortByLandId { get; set; } // added by YoungOk Kim (2020.05.27) - Mantis 106041: [YQ] 블록 정렬 방법 변경
        public bool IsShowAreaOnly { get; set; } // added by YoungOk Kim (2020.10.06) - Mantis 109805: Should not allow use YQ to log in ARMG equipment
        public bool IsFilterFacility { get; set; } // added by kimxyuhwan (2023.05.15) 0148989: YQ 로그인 화면의 블럭 선택 화면 개선 요청
        public bool IsSimpleCommand { get; set; } // added by kimxyuhwan(2023.08.10) [PC18_UP] INV-12-YQ 화면 상의 버튼 추가 및 삭제 (3, GY)

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public BlockBayListController()
        {
            ObjectID = OBJECT_ID;
        }

        public BlockBayListController(BlockBayListInterface view)
        {
            ObjectID = OBJECT_ID;
            FormView = view;
            this.DataSyncAgent = new DataSyncAgent();

            InitService();
        }

        private void InitService()
        {
            try
            {
                YqAssignmentService = BizServiceLocator.GetService<IYQAssignmentService>(ServiceConstant.YQ_ASSIGNMENT_SERVICE);
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

        #endregion INITIALIZATION AREA ****************************************

        #region EVENT HANDLER AREA ********************************************
        #endregion EVENT HANDLER AREA *****************************************

        #region INTERFACE IMPLEMENTATION AREA *********************************
        #endregion INTERFACE IMPLEMENTATION AREA ******************************

        #region BASE IMPLEMENTATION AREA **************************************

        public void NotifySyncAgent()
        {
            this.InputValue = GetInputValue();

            object targetSyncData = this.InputValue as BlockBayListViewItem;

            this.DataSyncAgent.NotifyToSync(DataSyncConstant.SYNC_NOTIFY_BLOCK_BAY_LIST, targetSyncData);
        }

        #endregion BASE IMPLEMENTATION AREA ***********************************

        #region METHOD AREA ***************************************************

        public BaseItemsList<ItemListControlItem> DoRetrieveData(bool isAll)
        {
            BaseItemsList<ItemListControlItem> itemList = null;

            try
            {
                if (isAll)
                {
                    itemList = new BaseItemsList<ItemListControlItem>();

                    if (IsShowAreaOnly == true) // added by YoungOk Kim (2020.10.06) - Mantis 109805: Should not allow use YQ to log in ARMG equipment
                    {
                        // nothing
                    }
                    else
                    {
                        if (BlockItems != null)
                        {
                            List<BlockItem> blockList = null;
                            if (IsSortByLandId == true) // added by YoungOk Kim (2020.05.27) - Mantis 106041: [YQ] 블록 정렬 방법 변경
                            {
                                blockList = BlockItems.OrderBy(p => string.IsNullOrEmpty(p.LaneID) == true ? 2 : 1).ThenBy(p => p.LaneID)
                                                        .ThenBy(p => p.LaneIdx == 0 ? 2 : 1).ThenBy(p => p.LaneIdx)
                                                        .ThenBy(p => p.Name).ToList();
                            }
                            else
                            {
                                blockList = BlockItems.OrderBy(p => p.Name).ToList();
                            }

                            // added by kimxyuhwan(2023.05.15) 0148989: YQ 로그인 화면의 블럭 선택 화면 개선 요청
                            if (IsFilterFacility == true)
                            {
                                if (EquItem != null)
                                {
                                    string equType = EquItem.EquType.Substring(0, 1);
                                    blockList = blockList.Where(p => p.Facility == equType).ToList();
                                }
                            }

                            foreach (var codeItem in blockList)
                            {
                                ItemListControlItem item = new ItemListControlItem();
                                item.Code = codeItem.Name;
                                item.CodeName = codeItem.Name;
                                item.TextValue = codeItem.Name;
                                itemList.Add(item);
                            }
                        }
                    }

                    if (AreaItems != null)
                    {
                        var areaList = AreaItems.OrderBy(p => p.Name).ToList();
                        foreach (var codeItem in areaList)
                        {
                            ItemListControlItem item = new ItemListControlItem();
                            item.Code = codeItem.Name;
                            item.CodeName = codeItem.Name;
                            item.TextValue = codeItem.Name;
                            itemList.Add(item);
                        }
                    }
                }
                else
                {
                    if (EquItem != null)
                    {
                        itemList = GetBlockList(EquItem.Name, EquItem.EquType);
                    }
                    else
                    {
                        MessageManager.Show("MSG_CTMO_00001", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            return itemList;
        }

        public BaseItemsList<ItemListControlItem> GetBlockList(String equNo, String equType)
        {
            BaseItemsList<ItemListControlItem> itemList = null;

            try
            {
                var param = new YQAssignmentParam(this);
                param.EquNo = equNo;
                param.EquType = equType;

                var result = YqAssignmentService.GetYQAssignment(param);
                var yqAssignmentItems = result.ResultObject as BaseItemsList<YQAssignmentItem>;

                if (yqAssignmentItems != null && yqAssignmentItems.Any())
                {
                    itemList = new BaseItemsList<ItemListControlItem>((from item in yqAssignmentItems[0].EquipmentCoverages
                                                                       select new ItemListControlItem()
                                                                       {
                                                                           Code = item.Block,
                                                                           CodeName = item.Block,
                                                                           TextValue = item.Block
                                                                       }).OrderBy(p => p.Code).ToList());
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

            return itemList;
        }

        public BaseItemsList<ItemListControlItem> GetBayList(String block, BayRowType bayRowType)
        {
            BaseItemsList<ItemListControlItem> itemList = null;

            try
            {
                if (IsBlock(block) == false) return null;

                if (bayRowType == BayRowType.ROW)
                {
                    itemList =
                        new BaseItemsList<ItemListControlItem>(
                            RowItemList.Where(item => item.Block.Equals(block)).Select(item => new ItemListControlItem()
                            {
                                Code = item.Key,
                                CodeName = item.Name,
                                TextValue = item.Name
                            }).ToList());
                }
                else if (bayRowType == BayRowType.BAY)
                {
                    itemList =
                       new BaseItemsList<ItemListControlItem>(
                           BayItemList.Where(item => item.Block.Equals(block)).Select(item => new ItemListControlItem()
                           {
                               Code = item.Key,
                               CodeName = item.Name2 + "/" + item.Name4,
                               TextValue = item.DisplayName
                           }).ToList());
                }
                else
                {
                    // TO-DO
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

            return itemList;
        }

        public bool IsBlock(string block)
        {
            if (string.IsNullOrEmpty(block))
            {
                return false;
            }

            return BlockItems.Any(o => o.Name.Equals(block));
        }

        public BlockBayListViewItem GetInputValue()
        {
            BlockBayListViewItem returnValue = null;

            try
            {
                returnValue = new BlockBayListViewItem();
                returnValue.BlockAreaItem = SelectedBlockAreaItem;
                returnValue.BayRowItem = SelectedBayRowItem;

                String blockAreaName = string.Empty;
                if (returnValue.BlockAreaItem is BlockItem)
                {
                    blockAreaName = (returnValue.BlockAreaItem as BlockItem).Name;
                }
                else if (returnValue.BlockAreaItem is AreaItem)
                {
                    blockAreaName = (returnValue.BlockAreaItem as AreaItem).Name;
                }
                returnValue.DisplayBlockAreaName = blockAreaName;

                String bayRowName = string.Empty;
                if (returnValue.BayRowItem is BayItem)
                {
                    bayRowName = (returnValue.BayRowItem as BayItem).Name2 + "/" + (returnValue.BayRowItem as BayItem).Name4; ;
                }
                else if (returnValue.BayRowItem is RowItem)
                {
                    bayRowName = (returnValue.BayRowItem as RowItem).Name;
                }
                returnValue.DisplayBayRowName = bayRowName;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }

            return returnValue;
        }

        #endregion METHOD AREA ************************************************
    }
}
