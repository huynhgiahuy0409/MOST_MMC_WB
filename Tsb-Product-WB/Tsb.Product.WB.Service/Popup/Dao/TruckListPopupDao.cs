/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2012 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		REVISION    	
* 2011.01.25  Tonny.Kim 1.0	First release.
* 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Product.WB.Common.Item.Popup;
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.Popup;
using Tsb.Product.WB.Common.Param.Sample;

namespace Tsb.Product.WB.Service.Popup.Dao
{
    public class TruckListPopupDao : SqlMapDaoSupport, ITruckListPopupDao
    {
        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize SingleGridDao
        /// </summary>
        public TruckListPopupDao()
            : base()
        {
            this.ObjectID = "DAO-PT-PTWB-SPL-TruckListPopupDao";
        }
        #endregion

        #region ITruckListPopupDao Members ******************************
        /// <summary>
        /// Inquire the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SinlgeGridItem</param>
        /// <returns>the rows inquired</returns>
        //public BaseItemsList<TruckListPopupItem> InquiryLorryList(TruckListPopupParam param)
        public BaseItemsList<WeightInfoItem> InquiryLorryList(TruckListPopupParam param)
        {
            //BaseItemsList<TruckListPopupItem> items = null; 
            BaseItemsList<WeightInfoItem> items = null;
            try
            {
                //items = new BaseItemsList<TruckListPopupItem>(this.QueryForList<TruckListPopupItem>("Tsb.Product.WB.Service.Popup.Map.TruckListPopupMap.select-LorryList", param));
                items = new BaseItemsList<WeightInfoItem>(this.QueryForList<WeightInfoItem>("Tsb.Product.WB.Service.Popup.Map.TruckListPopupMap.select-LorryList", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return items;
        }

        
        #endregion
    }
}
