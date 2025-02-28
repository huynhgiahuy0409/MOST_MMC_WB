using System;
using System.Collections.Generic;
using Tsb.Catos.Cm.Mobile.Common.Item.MobileContainerDetail;
using Tsb.Catos.Cm.Mobile.Common.Param.MobileContainerDetail;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Service.MobileContainerDetail.Dao
{
    public class MobileContainerDetailDao : SqlMapDaoSupport, IMobileContainerDetailDao
    {
        #region INITIALIZATION AREA *******************************************

        private const string MAP_PACKAGE_STR = "Tsb.Catos.Cm.Mobile.Service.MobileContainerDetail.Map.";

        public MobileContainerDetailDao()
            : base()
        {
            this.ObjectID = "DAO-CT-CTMO-MobileContainerDetailDao";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (SELECT) ******************************************

        public BaseItemsList<ContainerInfoItem> GetContainerList(MobileContainerDetailParam param)
        {
            BaseItemsList<ContainerInfoItem> containerList = null;

            try
            {
                containerList = new BaseItemsList<ContainerInfoItem>(this.QueryForList<ContainerInfoItem>(MAP_PACKAGE_STR + "MobileContainerDetailMap.select-GetContainerList", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return containerList;
        }

        // added by YoungOk Kim (2019.10.16) - Mantis 92189: [Tally] 컨테이너 정보에서 2 time shifting 표시
        public List<string> GetShipPositionList(MobileContainerDetailParam param)
        {
            List<string> shipPositionList = null;

            try
            {
                shipPositionList = new List<string>(this.QueryForList<string>(MAP_PACKAGE_STR + "MobileContainerDetailMap.select-GetShipPosition", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return shipPositionList;
        }
        
        #endregion METHOD AREA (SELECT) ***************************************
    }
}
