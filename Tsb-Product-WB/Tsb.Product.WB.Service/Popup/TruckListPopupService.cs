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
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Product.WB.Common.Item.Popup;
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.Popup;
using Tsb.Product.WB.Common.Param.Sample;
using Tsb.Product.WB.Service.Popup.Dao;


namespace Tsb.Product.WB.Service.Popup
{
    public class TruckListPopupService : BaseService, ITruckListPopupService
    {
        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initializes a new instance of the SingleGridService class.
        /// </summary>
        public TruckListPopupService()
            : base()
        {
            this.ObjectID = "SVC-PT-PTWB-POP-TruckListPopupService";
        }

        /// <summary>
        /// Gets or sets truckListPopupDao.
        /// </summary>
        public ITruckListPopupDao truckListPopupDao { get; set; }
        #endregion

        #region ITruckListPopupService Members **************************
        /// <summary>
        /// Inquire the ITruckListPopupServiced's table with the specified item.
        /// </summary>
        /// <param name="item">ITruckListPopupService</param>
        /// <returns>the rows inquired</returns>
        public BaseResult InquiryLorryList(TruckListPopupParam param)
        {
            BaseResult resultObject = null;
            try
            {
                //BaseItemsList<TruckListPopupItem> items = this.truckListPopupDao.InquiryLorryList(param); 
                BaseItemsList<WeightInfoItem> items = this.truckListPopupDao.InquiryLorryList(param);
                resultObject = BaseResult.CreateOkResult(this.ObjectID, items, param);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }
            return resultObject;
        }
        #endregion

    }
}
