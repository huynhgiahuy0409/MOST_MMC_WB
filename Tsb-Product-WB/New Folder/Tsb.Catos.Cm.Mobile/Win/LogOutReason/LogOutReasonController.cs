using System;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Item.LogOutReason;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.LogOutReason
{
    public class LogOutReasonController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-LogOutReasonController";
        private LogOutReasonInterface _formView;
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string SelectedLogOutReason { get; set; }
        public BaseItemsList<LogOutReasonItem> LogOutReasonItemList { get; set; }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************
        
        public LogOutReasonController()
        {
            ObjectID = OBJECT_ID;
        }

        public LogOutReasonController(LogOutReasonInterface view)
        {
            ObjectID = OBJECT_ID;
            _formView = view;
        }

        public void DoRetrieveData()
        {
            BaseItemsList<ItemListControlItem> itemList = null;

            try
            {
                if (this.LogOutReasonItemList != null)
                {
                    itemList = new BaseItemsList<ItemListControlItem>();
                    foreach (LogOutReasonItem item in this.LogOutReasonItemList)
                    {
                        ItemListControlItem displayItem = new ItemListControlItem();
                        displayItem.Code = item.Code;
                        displayItem.CodeName = item.Description;
                        displayItem.TextValue = item.Description;
                        itemList.Add(displayItem);
                    }
                }

                _formView.SetItemList(itemList);
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
