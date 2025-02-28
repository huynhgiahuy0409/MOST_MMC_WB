using System;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Item.CommonItemList;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.CommonItemList
{
    public class CommonItemListController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-CommonItemListController";
        
        #endregion CONST & FIELD AREA ********************************************

        #region PROPERTY AREA *************************************************

        public CommonItemListInterface FormView { get; set; }
        public string ViewTitle { get; set; }
        public BaseItemsList<CommonItemListItem> ItemList { get; set; }
        public CommonItemListItem SelectedItem { get; set; }
        public int MaxRowCount { get;set; }
        public int MaxColumnCount { get; set; }
        public BaseItemsList<CommonItemListItem> SelectedItemList { get; set; } // added by YoungOk Kim (2020.05.27) - Mantis 106042: [YQ] 필터에 블록 버튼 추가
        public bool UseMultipleSelection { get; set; } // added by YoungOk Kim (2020.05.27) - Mantis 106042: [YQ] 필터에 블록 버튼 추가

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public CommonItemListController()
        {
            ObjectID = OBJECT_ID;
        }

        public CommonItemListController(CommonItemListInterface view)
        {
            ObjectID = OBJECT_ID;
            FormView = view;
        }

        public void DoRetrieveData()
        {
            BaseItemsList<ItemListControlItem> itemList = null;

            try
            {
                if (this.ItemList != null)
                {
                    itemList = new BaseItemsList<ItemListControlItem>();
                    foreach (CommonItemListItem item in this.ItemList)
                    {
                        ItemListControlItem displayItem = new ItemListControlItem();
                        displayItem.Code = item.Code;
                        displayItem.CodeName = item.CodeName;
                        displayItem.TextValue = item.DisplayValue;
                        displayItem.CodeItem = item;
                        displayItem.CustomStyleName = item.CustomStyleName;
                        itemList.Add(displayItem);
                    }
                }

                FormView.SetItemList(itemList, this.ViewTitle);
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
    }
}
