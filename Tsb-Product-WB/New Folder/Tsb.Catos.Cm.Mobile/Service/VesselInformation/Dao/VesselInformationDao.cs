using System;
using Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation;
using Tsb.Catos.Cm.Mobile.Common.Param.VesselInformation;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Service.VesselInformation.Dao
{
    public class VesselInformationDao : SqlMapDaoSupport, IVesselInformationDao
    {
        #region CONST & FIELD AREA ********************************************

        private const string MAP_PACKAGE_STR = "Tsb.Catos.Cm.Mobile.Service.VesselInformation.Map.";
        
        #endregion CONST & FIELD AREA *****************************************

        #region INITIALIZATION AREA *******************************************

        public VesselInformationDao()
            : base()
        {
            this.ObjectID = "DAO-CT-CTMO-VesselInformationDao";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (SELECT) ******************************************
        
        public BaseItemsList<BerthPlanItem> GetVesselScheduleList()
        {
            BaseItemsList<BerthPlanItem> itemList = null;

            try
            {
                itemList = new BaseItemsList<BerthPlanItem>(this.QueryForList<BerthPlanItem>(MAP_PACKAGE_STR + "VesselInformationMap.select-vesselScheduleList", null));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }

            return itemList;
        }

        public BaseItemsList<HatchInfoItem> GetHatchItemList(VesselInformationParam param)
        {
            BaseItemsList<HatchInfoItem> hatchItemList = null;
            try
            {
                hatchItemList = new BaseItemsList<HatchInfoItem>(this.QueryForList<HatchInfoItem>(MAP_PACKAGE_STR + "VesselInformationMap.select-hatchItemList", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return hatchItemList;
        }

        //added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        public BaseItemsList<ActivatedHatchInfoItem> GetActivatedHatchItemList(ActivatedVesselInformationParam param)
        {
            BaseItemsList<ActivatedHatchInfoItem> activatedHatchItemList = null;
            try
            {
                activatedHatchItemList = new BaseItemsList<ActivatedHatchInfoItem>(this.QueryForList<ActivatedHatchInfoItem>(MAP_PACKAGE_STR + "VesselInformationMap.select-ativatedHatchItemList", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return activatedHatchItemList;
        }

        public BaseItemsList<VesselBayItem> GetBayItemList(VesselInformationParam param)
        {
            BaseItemsList<VesselBayItem> bayItemList = null;
            try
            {
                bayItemList = new BaseItemsList<VesselBayItem>(this.QueryForList<VesselBayItem>(MAP_PACKAGE_STR + "VesselInformationMap.select-bayList", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return bayItemList;
        }

        public BerthPlanItem GetVesselSchedule(ActivatedVesselInformationParam param)
        {
            BerthPlanItem item = null;

            try
            {
                item = this.QueryForObject<BerthPlanItem>(MAP_PACKAGE_STR + "VesselInformationMap.select-vesselSchedule", param);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }

            return item;
        }
        #endregion METHOD AREA (SELECT) ***************************************
    }
}
