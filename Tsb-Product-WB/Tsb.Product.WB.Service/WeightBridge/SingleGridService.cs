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
using Tsb.Product.WB.Common.Param.Sample;
using Tsb.Product.WB.Service.WeightBridge;
using Tsb.Product.WB.Service.WeightBridge.Dao;

namespace Tsb.Product.WB.Service.WeightBridge
{
    public class SingleGridService : BaseService, ISingleGridService
    {
        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initializes a new instance of the SingleGridService class.
        /// </summary>
        public SingleGridService()
            : base()
        {
            this.ObjectID = "SVC-PT-PTWB-SPL-SingleGridService";
        }

        /// <summary>
        /// Gets or sets SingleGridDao.
        /// </summary>
        public ISingleGridDao SingleGridDao { get; set; }
        #endregion

        #region ISingleGridService Members **************************
        /// <summary>
        /// Inquire the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SinlgeGridItem</param>
        /// <returns>the rows inquired</returns>
        public BaseResult InquirySingleGrid(SingleGridParam param)
        {
            BaseResult resultObject = null;
            try
            {
                BaseItemsList<SingleGridItem> items = this.SingleGridDao.InquirySingleGrid(param);

                //=========================================
                // Code to make the processing speed slowed.
                foreach (SingleGridItem item in items)
                {
                    System.Threading.Thread.Sleep(10);
                }
                //=========================================

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

        /// <summary>
        /// Insert or Update a row into the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SingleGridItem</param>
        public void ProcessSingleGrid(SingleGridItem item)
        {
            try
            {
                if (item.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.UPDATE)
                {
                    this.SingleGridDao.UpdateSingleGrid(item);
                }
                else if (item.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.CREATE)
                {
                    this.SingleGridDao.CreateSingleGrid(item);
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
            item.BackupItem = item.Clone() as SingleGridItem;
        }

        /// <summary>
        /// Deletes a row into the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SinlgeGridItem</param>
        public void DeleteSingleGrid(SingleGridItem item)
        {
            try
            {
                this.SingleGridDao.DeleteSingleGrid(item);     // Delete Master Data.
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }
        }
        #endregion
    }
}
