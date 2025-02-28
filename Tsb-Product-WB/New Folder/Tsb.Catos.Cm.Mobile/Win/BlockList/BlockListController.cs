using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Item.BlockList;
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

namespace Tsb.Catos.Cm.Mobile.Win.BlockList
{
    public class BlockListController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************
        
        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-BlockListController";

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        private BlockListInterface FormView { get; set; }
        private IYQAssignmentService YqAssignmentService { get; set; }
        private DataSyncAgent DataSyncAgent { get; set; }

        public BaseDataItem SelectedBlockAreaItem { get; set; }
        public BlockItemList BlockItems { get; set; }
        public EquipmentItem EquItem { get; set; }
        public BlockListViewItem InputValue { get; set; }

        public string CurrentBlockArea { get; set; }

        public bool IsAlwaysShowAll { get; set; }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public BlockListController()
        {
            ObjectID = OBJECT_ID;
        }

        public BlockListController(BlockListInterface view)
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

            object targetSyncData = this.InputValue as BlockListViewItem;

            this.DataSyncAgent.NotifyToSync(DataSyncConstant.SYNC_NOTIFY_BLOCK_LIST, targetSyncData);
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

                    if (BlockItems != null)
                    {
                        List<BlockItem> blockList = null;
                        //blockList = BlockItems.OrderBy(p => p.Name).ToList();
                        blockList = BlockItems.OrderBy(g => g.Name.Substring(1, 1)).ThenBy(g => g.Name.Substring(0, 1)).ToList();

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

        public bool IsBlock(string block)
        {
            if (string.IsNullOrEmpty(block))
            {
                return false;
            }

            return BlockItems.Any(o => o.Name.Equals(block));
        }

        public BlockListViewItem GetInputValue()
        {
            BlockListViewItem returnValue = null;

            try
            {
                returnValue = new BlockListViewItem();
                returnValue.BlockAreaItem = SelectedBlockAreaItem;

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
