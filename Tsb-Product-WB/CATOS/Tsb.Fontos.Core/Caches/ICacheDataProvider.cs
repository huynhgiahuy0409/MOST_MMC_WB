#region Interface Definitions
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
* DATE           AUTHOR		REVISION    	
* 2009.09.07    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Codes;
using System.Collections.ObjectModel;
using System.Collections;

namespace Tsb.Fontos.Core.Caches
{
    /// <summary>
    /// Represent Cache Data Provider
    /// </summary>
    public interface ICacheDataProvider
    {
        /// <summary>
        /// Gets Code Data Item List
        /// </summary>
        /// <param name="queryDelegator">Code data query delegator</param>
        /// <returns>Code Data Item List</returns>
        IList<T> GetDataItemList<T>(CodeDataQueryDelegator queryDelegator);


        /// <summary>
        /// Gets Code Data Item List
        /// </summary>
        /// <param name="queryDelegator">Code data query delegator</param>
        /// <param name="doClone">A boolean value indicating whether do clone for a cached data(new copy) or not</param>
        /// <returns>Code Data Item List</returns>
        IList<T> GetDataItemList<T>(CodeDataQueryDelegator queryDelegator, bool doClone);

        /// <summary>
        /// Gets already cached code data item list using cache key. 
        /// </summary>
        /// <param name="cacheKey">Key of Cached Data</param>
        /// <returns>Code Data Item List</returns>
        IList<T> GetDataItemList<T>(string cacheKey);


        /// <summary>
        /// Gets already cached code data item list using cache key. 
        /// </summary>
        /// <param name="cacheKey">Key of Cached Data</param>
        /// <param name="doClone">A boolean value indicating whether do clone for a cached data(new copy) or not</param>
        /// <returns>Code Data Item List</returns>
        IList<T> GetDataItemList<T>(string cacheKey, bool doClone);

        /// <summary>
        /// Fills data to cache
        /// </summary>
        /// <param name="itemList">Code data item List to cache</param>
        void FillDataToCache(object itemListObject);


        /// <summary>
        /// Check whether is cached or not
        /// </summary>
        /// <returns>true if data is cached, otherwise false</returns>
        bool IsCached();
    }
}
