using System;
using System.Collections.Generic;
using Tsb.Catos.Cm.Core.Codes;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Codes;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.EquipmentList
{
    public class EquipmentListController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************
        
        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-EquipmentListController";
        private EquipmentListInterface _formView;
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string SelectedEquipmentNo { get; set; }
        public EquipmentItemList YardEquipmentItems { get; set; }

        // added by YoungOk Kim (2019.07.03) - Mantis 90787: [YQ] GC 복수 선택
        public bool UseMultipleSelection { get; set; }
        public bool IsSortEquipmentGroup { get; set; }
        public bool UseSortEquipmentGroup { get; set; }
        public List<string> SelectedEquipmentList { get; set; }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public EquipmentListController()
        {
            ObjectID = OBJECT_ID;
        }

        public EquipmentListController(EquipmentListInterface view)
        {
            ObjectID = OBJECT_ID;
            _formView = view;

            //Initialize Application Code Data Handler
            CodeManager.InitilizeInstance(new CodeGeneralHandler());
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA ***************************************************

        public void DoRetrieveData()
        {
            try
            {
                var itemList = new BaseItemsList<ItemListControlItem>();
                foreach (var equItem in YardEquipmentItems)
                {
                    var item = new ItemListControlItem();
                    item.Code = equItem.Name;
                    item.CodeName = equItem.Name;
                    item.TextValue = equItem.Name;
                    itemList.Add(item);
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

        #endregion METHOD AREA ************************************************
    }
}
