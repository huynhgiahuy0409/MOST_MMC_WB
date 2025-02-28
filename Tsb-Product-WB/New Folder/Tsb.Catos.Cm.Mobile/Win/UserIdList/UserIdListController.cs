using System;
using System.Linq;
using Tsb.Catos.Cm.Core.Codes;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Item.Staff;
using Tsb.Catos.Cm.Mobile.Common.Param.Staff;
using Tsb.Catos.Cm.Mobile.Service.UserIdList;
using Tsb.Fontos.Core.Codes;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.UserIdList
{
    public class UserIdListController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************
        
        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-UserIdListController";
        private UserIdListInterface _formView;
        private IUserIdListService _userIdListService;
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string SelectedUserID { get; set; }
        public string SelectedUserName { get; set; }
        public string UserGroup { get; set; } // added by YoungOk Kim (2020.01.13) - Mantis 104821: [QC] User ID 클릭 시 전체 사용자가 조회되는 문제

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public UserIdListController()
        {
            ObjectID = OBJECT_ID;
        }

        public UserIdListController(UserIdListInterface view)
        {
            ObjectID = OBJECT_ID;
            _formView = view;
            InitService();

            //Initialize Application Code Data Handler
            CodeManager.InitilizeInstance(new CodeGeneralHandler());
        }

        private void InitService()
        {
            try
            {
                _userIdListService = BizServiceLocator.GetService<IUserIdListService>(ServiceConstant.USER_ID_LIST_SERVICE);
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
            }
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA ***************************************************

        public void DoRetrieveData()
        {
            try
            {
                var itemList = new BaseItemsList<ItemListControlItem>();

                GetStaffParam param = new GetStaffParam(this);
                param.UserGroup = UserGroup;

                var driverIdList = _userIdListService.GetUserIdList(param);
                BaseItemsList<StaffItem> userIdList = driverIdList.ResultObject as BaseItemsList<StaffItem>;

                if (userIdList != null && userIdList.Any())
                {
                    foreach (var staffItem in userIdList)
                    {
                        var item = new ItemListControlItem();
                        item.Code = staffItem.StaffCode;
                        item.CodeName = staffItem.StaffCode;
                        item.TextValue = staffItem.StaffCode + Environment.NewLine + staffItem.LocalName;
                        itemList.Add(item);
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

        #endregion METHOD AREA ************************************************
    }
}
