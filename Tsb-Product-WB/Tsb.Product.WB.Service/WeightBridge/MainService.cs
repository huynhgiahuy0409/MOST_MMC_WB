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
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.Sample;
using Tsb.Product.WB.Common.Param.WeightBridge;
using Tsb.Product.WB.Service.WeightBridge.Dao;

namespace Tsb.Product.WB.Service.WeightBridge
{
    public class MainService : BaseService, IMainService
    {
        #region INITIALIZE AREA *************************************
      
        public MainService()
            : base()
        {
            this.ObjectID = "SVC-PT-PTWB-WB-MainService";
        }
        public IMainDao MainDao { get; set; }


        public BaseResult InquiryWeightInfo(MainParam param)
        {
            BaseResult result = null;

            try
            {
                BaseItemsList<WeightInfoItem> item = this.MainDao.InquiryWeightInfo(param);
                result = BaseResult.CreateOkResult(this.ObjectID, item, param);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }

            return result;
        }
        public void UpdateRemarkWeightBridge(WeightInfoItem item)
        {
            try
            {
                if (item.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.UPDATE)
                {
                    this.MainDao.UpdateWeightBridge(item);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }

            item.OpCode = Tsb.Fontos.Core.Configuration.Common.OpCodes.READ;
            item.BackupItem = item.Clone() as WeightInfoItem;
        }
        public void UpdatePrintCountWeightBridge(WeightInfoItem item)
        {
            try
            {               
                this.MainDao.UpdatePrintCountWeightBridge(item);              
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }

            item.OpCode = Tsb.Fontos.Core.Configuration.Common.OpCodes.READ;
            item.BackupItem = item.Clone() as WeightInfoItem;
        }
        public void upadteStatusCanCelJobWeightBridge(WeightInfoItem item)
        {
            try
            {
                if (item.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.UPDATE)
                {
                    this.MainDao.upadteStatusCanCelJobWeightBridge(item);
                }

            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }
            item.OpCode = Tsb.Fontos.Core.Configuration.Common.OpCodes.READ;
            item.BackupItem = item.Clone() as WeightInfoItem;
        }
        #endregion

    }

}
