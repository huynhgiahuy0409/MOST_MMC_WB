#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2009 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE            AUTHOR	  	 REVISION    	
* 2009.09.07       CHOI 1.0	   First release.
* 2012.03.09  Tonny.Kim 1.1    Add IsCachedStartWith, RemoveDataToCacheStartsWith
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Caches;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Codes.Type;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Exceptions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;
using System.Collections;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Codes
{

    /// <summary>
    /// Code data Cache Provider class
    /// </summary>
    public class CodeCacheDataProvider : TsbBaseObject, ICacheDataProvider
    {
        #region FIELD AREA *************************************

        private static object syncRoot = new Object();

        #endregion


        #region PROPERTY AREA **********************************

        /// <summary>
        /// Gets or Sets CodeDataParam Object reference
        /// </summary>
        public CodeDataParam CodeParam { get; set; }

        /// <summary>
        /// Gets CacheKey string
        /// </summary>
        public string CacheKey
        {
            get
            {
                return this.MakeCodeTypeKey();
            }

        }

        #endregion


        #region INITIALIZATION AREA ****************************

        /// <summary>
        /// Initialize Instance using a specified CodeDataParam Object
        /// </summary>
        /// <param name="param">CodeDataParam Object</param>
        public CodeCacheDataProvider(CodeDataParam param)
            : base()
        {
            this.ObjectID = "GNR-FTCO-COD-CodeCacheDataProvider";
            this.CodeParam = param;
        }

        #endregion


        #region METHOD AREA (GET DATA ITEM LIST USING DELEGATOR)

        /// <summary>
        /// Gets Code Data Item List that is cloned data from cached data
        /// </summary>
        /// <param name="queryDelegator">Code data query delegator</param>
        /// <returns>Code Data Item List</returns>
        public IList<T> GetDataItemList<T>(CodeDataQueryDelegator queryDelegator)
        {
            return this.GetDataItemList<T>(queryDelegator, true);
        }

        /// <summary>
        /// Gets Code Data Item List that is cloned data from cached data
        /// </summary>
        /// <param name="queryDelegator">Code data query delegator</param>
        /// <param name="doClone">A boolean value indicating whether do clone for a cached data(new copy) or not</param>
        /// <returns>Code Data Item List</returns>
        public IList<T> GetDataItemList<T>(CodeDataQueryDelegator queryDelegator, bool doClone)
        {
            IList<T> rtnList = null;
            //IList<T> cachedList = null;
            IList cachedList = null;
            string cacheKey = null;
            int count = 0;
            ICloneable item = null;

            try
            {
                cacheKey = this.MakeCodeTypeKey();

                lock (CodeCacheDataProvider.syncRoot)
                {
                    if (CacheManager.ContainsKey(cacheKey) == false)
                    {
                        this.FillDataToCache(queryDelegator(this.CodeParam));
                    }
                }

                if (doClone)
                {
                    cachedList = CacheManager.Get(cacheKey) as IList;
                    count = cachedList.Count;
                    rtnList = new List<T>(count);

                    for (int i = 0; i < count; i++)
                    {
                        item = cachedList[i] as ICloneable;
                        rtnList.Add((T)item.Clone());
                    }
                }
                else
                {
                    rtnList = CacheManager.Get(cacheKey) as IList<T>;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return rtnList;

        }
        #endregion


        #region METHOD AREA (GET DATA ITEM LIST USING CACHE KEY)

        /// <summary>
        /// Gets Code Data Item List that is cloned data from cached data using cache key. 
        /// </summary>
        /// <param name="cacheKey">Key of Cached Data</param>
        /// <returns>Code Data Item List</returns>
        public IList<T> GetDataItemList<T>(string cacheKey)
        {
            return this.GetDataItemList<T>(cacheKey, true);
        }

        /// <summary>
        /// Gets already cached code data item list using cache key. 
        /// </summary>
        /// <param name="cacheKey">Key of Cached Data</param>
        /// <param name="doClone">A boolean value indicating whether do clone for a cached data(new copy) or not</param>
        /// <returns>Code Data Item List</returns>
        public IList<T> GetDataItemList<T>(string cacheKey, bool doClone)
        {
            IList<T> rtnList = null;
            IList<T> cachedList = null;

            try
            {
                if (CacheManager.ContainsKey(cacheKey) == false)
                {
                    //MSG:Cached data cannot be found using a specified cache key.[Key={0}]
                    throw new TsbSysBaseException(this.ObjectID, "MSG_FTCO_00062", DefaultMessage.NON_REG_WRD + this.CodeParam.Type);
                }

                if (doClone)
                {
                    cachedList = CacheManager.Get(cacheKey) as IList<T>;
                    rtnList = new List<T>(cachedList.Count);

                    foreach (T item in cachedList)
                    {
                        T clonedItem = (T)(item as ICloneable).Clone();
                        rtnList.Add(clonedItem);
                    }
                }
                else
                {
                    rtnList = CacheManager.Get(cacheKey) as IList<T>;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return rtnList;
        }

        #endregion


        #region METHOD AREA (FILL DATA TO CACHE)****************
        /// <summary>
        /// Fills Code data to cache
        /// </summary>
        /// <param name="itemList">Code data item List to cache</param>
        public void FillDataToCache(object itemListObject)
        {
            try
            {
                CacheManager.Insert(this.MakeCodeTypeKey(), itemListObject, CacheConfigurator.GENERAL_CODE_TIME_TO_LIVE, CacheConfigurator.GENERAL_CODE_DO_SLIDING_EXPIRE);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }
        #endregion

        #region METHOD AREA (IS CHACHED CHECK)******************
        /// <summary>
        /// Check whether is cached or not
        /// </summary>
        /// <returns>true if data is cached, otherwise false</returns>
        public bool IsCached()
        {
            string cacheKey = null;
            bool isCached = false;

            try
            {
                cacheKey = this.MakeCodeTypeKey();

                lock (CodeCacheDataProvider.syncRoot)
                {
                    isCached = CacheManager.ContainsKey(cacheKey);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return isCached;
        }

        /// <summary>
        /// Check whether is cached or not
        /// </summary>
        /// <param name="value">cache key starts with string</param>
        /// <returns>true if data is cached, otherwise false</returns>
        public bool IsCachedStartWith(string value)
        {
            string cacheKey = null;
            bool isCached = false;

            try
            {
                cacheKey = this.MakeCodeTypeKey(value);

                lock (CodeCacheDataProvider.syncRoot)
                {
                    isCached = CacheManager.ContainsStartsWith(cacheKey);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return isCached;
        }
        #endregion

        #region METHOD AREA (Remove Data to Cache) *************
        /// <summary>
        /// Remove Code data to cache
        /// </summary>
        /// <param name="itemList">Code data item List to cache</param>
        public bool RemoveDataToCache()
        {
            try
            {
                lock (CodeCacheDataProvider.syncRoot)
                {
                    string cacheKey = this.MakeCodeTypeKey();

                    if (CacheManager.ContainsKey(cacheKey))
                    {
                        CacheManager.Remove(cacheKey);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return false;
        }

        /// <summary>
        /// Remove Code data to cache
        /// </summary>
        /// <param name="value">cache key starts with string</param>
        public bool RemoveDataToCacheStartsWith(string value)
        {
            try
            {
                lock (CodeCacheDataProvider.syncRoot)
                {
                    string cacheKey = this.MakeCodeTypeKey(value);

                    if (CacheManager.ContainsStartsWith(cacheKey))
                    {
                        CacheManager.RemoveStartsWith(cacheKey);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return false;
        }
        #endregion

        #region METHOD AREA (MAKE KEY)**************************
        /// <summary>
        /// Returns Code Type Key
        /// </summary>
        /// <returns>Cache Code Type key string</returns>
        private string MakeCodeTypeKey()
        {
            return this.MakeCodeTypeKey(this.CodeParam.Key);
        }

        /// <summary>
        /// Returns Code Type Key
        /// </summary>
        /// <returns>Cache Code Type key string</returns>
        private string MakeCodeTypeKey(string value)
        {
            return StringUtil.CombineDot(this.CodeParam.CodeGroupType.ToString(), value);
        }
        #endregion

    }
}
