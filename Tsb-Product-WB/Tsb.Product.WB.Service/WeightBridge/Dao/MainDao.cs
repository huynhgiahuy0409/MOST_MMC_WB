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
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.Sample;
using Tsb.Product.WB.Common.Param.WeightBridge;

namespace Tsb.Product.WB.Service.WeightBridge.Dao
{
    public class MainDao : SqlMapDaoSupport, IMainDao
    {
        #region INITIALIZE AREA *************************************
        public MainDao()
            : base()
        {
            this.ObjectID = "DAO-PT-PTWB-WB-MainDao";
        }
        #endregion

        #region IMainDao Members ******************************
        public BaseItemsList<WeightInfoItem> InquiryWeightInfo(MainParam param)
        {
            BaseItemsList<WeightInfoItem> items = null;
            try
            {
                items = new BaseItemsList<WeightInfoItem>(this.QueryForList<WeightInfoItem>("Tsb.Product.WB.Service.WeightBridge.Map.MainMap.select-weightInfo", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizBaseException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return items;
        }
        public int UpdateWeightBridge(WeightInfoItem item)
        {
            int rtnRows = -1;
            try
            {
                rtnRows = this.Update("Tsb.Product.WB.Service.WeightBridge.Map.MainMap.update-remarkWeightInfo", item);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizBaseException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return rtnRows;
        }

        public int UpdatePrintCountWeightBridge(WeightInfoItem item)
        {
            int rtnRows = -1;
            try
            {
                rtnRows = this.Update("Tsb.Product.WB.Service.WeightBridge.Map.MainMap.update-printCount", item);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizBaseException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return rtnRows;
        }

        public int upadteStatusCanCelJobWeightBridge(WeightInfoItem item)
        {
            int result = -1;
            try
            {
                result = this.Update("Tsb.Product.WB.Service.WeightBridge.Map.MainMap.update-statusCanCelJobWeightBridge", item);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizBaseException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return result;
        }
        #endregion

    }
}
