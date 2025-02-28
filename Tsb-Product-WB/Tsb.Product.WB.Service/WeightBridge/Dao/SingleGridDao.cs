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
using Tsb.Product.WB.Common.Param.Sample;

namespace Tsb.Product.WB.Service.WeightBridge.Dao
{
    public class SingleGridDao : SqlMapDaoSupport, ISingleGridDao
    {
        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize SingleGridDao
        /// </summary>
        public SingleGridDao()
            : base()
        {
            this.ObjectID = "DAO-PT-PTWB-SPL-SingleGridDao";
        }
        #endregion

        #region ISingleGridDao Members ******************************
        public BaseItemsList<SingleGridItem> InquirySingleGrid(SingleGridParam param)
        {
            BaseItemsList<SingleGridItem> items = null;

            try
            {
                items = new BaseItemsList<SingleGridItem>(this.QueryForList<SingleGridItem>("Tsb.Product.WB.Service.Sample.Map.SingleGridMap.select-singleGrid", param));
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

        public object CreateSingleGrid(SingleGridItem item)
        {
            object rtnObj = null;

            try
            {
                rtnObj = this.InsertItem("Tsb.Product.WB.Service.Sample.Map.SingleGridMap.insert-singleGrid", item);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return rtnObj;
        }

        /// <summary>
        /// Updates a row into the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SinlgeGridItem</param>
        /// <returns>the number of rows updated</returns>
        public int UpdateSingleGrid(SingleGridItem item)
        {
            int rtnRows = -1;
            try
            {
                rtnRows = this.Update("Tsb.Product.WB.Service.Sample.Map.SingleGridMap.update-singleGrid", item);
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

        /// <summary>
        /// Deletes a row into the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SinlgeGridItem</param>
        /// <returns>the number of rows deleted</returns>
        public int DeleteSingleGrid(SingleGridItem item)
        {
            int rtnRows = -1;
            try
            {
                rtnRows = this.Delete("Tsb.Product.WB.Service.Sample.Map.SingleGridMap.delete-singleGrid", item);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizBaseException), this.ObjectID, "MSG_CTCM_00002", null);
            }
            return rtnRows;
        }
        #endregion
    }
}
