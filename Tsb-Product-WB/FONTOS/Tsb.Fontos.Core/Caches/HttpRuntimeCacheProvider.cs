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
* DATE           AUTHOR		     REVISION    	
* 2009.09.05       CHOI 1.0	   First release.
* 2012.03.09  Tonny.Kim 1.1    Add ContainsStartsWith, RemoveStartsWith
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;
using System.Collections;
using Tsb.Fontos.Core.Caches.Types;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Caches
{
    /// <summary>
    /// Cache privider using HttpRuntime Cache
    /// </summary>
    public class HttpRuntimeCacheProvider : TsbBaseObject, ICacheProvider
    {
        #region FIELD AREA *************************************
        private Cache _cache = null;
        CacheItemRemovedCallback _onRemoveDelegator = null;
        #endregion


        #region PROPERTY AREA **********************************

        /// <summary>
        /// Gets the cache keys list
        /// </summary>
        public List<string> Keys
        {
            get
            {
                List<string> keys = null;

                keys = new List<string>();
                foreach (DictionaryEntry entry in this._cache)
                {
                    keys.Add(Convert.ToString(entry.Key));
                }

                return keys;
            }
        }

        #endregion


        #region INITIALIZATION AREA ****************************

        /// <summary>
        /// Initialize instance
        /// </summary>
        public HttpRuntimeCacheProvider()
            : base()
        {
            try
            {
                this.ObjectID = "GNR_FTCO_CCH_HttpRuntimeCacheProvider";
                _cache = HttpRuntime.Cache;
                _onRemoveDelegator = new CacheItemRemovedCallback(this.ItemRemovedCallback);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        #endregion


        #region INSTNACE METHOD AREA (CALL BACK)****************

        /// <summary>
        /// Cached Item Removed Callback Handler Mehtod
        /// </summary>
        /// <param name="key">The key that is removed from the cache.</param>
        /// <param name="value">The Object item associated with the key removed from the cache.</param>
        /// <param name="reason">The reason the item was removed from the cache, as specified by the CacheItemRemovedReason enumeration.</param>
        public void ItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            return;
        }

        #endregion


        #region INSTNACE METHOD AREA (GET) *********************

        /// <summary>
        /// Retrieves an item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        public object Get(string key)
        {
            return this._cache.Get(key);
        }


        /// <summary>
        /// Retrieves an item from the cache of the specified type.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        public T Get<T>(string key)
        {
            T rtnObject = default(T);

            rtnObject = (T)this._cache.Get(key);

            return rtnObject;
        }

        #endregion


        #region INSTNACE METHOD AREA (REMOVE) ******************

        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache item to remove.</param>
        public void Remove(string key)
        {
            try
            {
                this._cache.Remove(key);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return;
        }

        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache key to remove.</param>
        public void RemoveStartsWith(string value)
        {
            try
            {
                foreach (DictionaryEntry entry in this._cache)
                {
                    string key = Convert.ToString(entry.Key);
                    if (key.StartsWith(value))
                    {
                        this._cache.Remove(key);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }


        /// <summary>
        /// Removes collection of items from the cache.
        /// </summary>
        /// <param name="keys">The list collection of cache key</param>
        public void RemoveAll(List<string> keys)
        {
            foreach (string key in keys)
            {
                this.Remove(key);
            }
            return;
        }


        /// <summary>
        /// Removes all items from the cache.
        /// </summary>
        public void Clear()
        {
            foreach (string key in this.Keys)
            {
                this.Remove(key);
            }
            return;
        }
        #endregion


        #region INSTNACE METHOD AREA (INSERT) ******************
        /// <summary>
        /// Inserts an item into the cache.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        public void Insert(string key, object value)
        {
            this.Insert(key, value, CacheConfigurator.DEFAULT_TIME_TO_LIVE, CacheConfigurator.DEFAULT_DO_SLIDING_EXPIRE, CacheConfigurator.DEFAULT_CACHE_PRIORITY);
        }


        /// <summary>
        /// Inserts an item into the cache.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        /// <param name="timeToLive">The interval between the time the inserted object was last accessed and the time at which that object expires</param>
        /// <param name="doSlidingExpiration">Specifies whether cached data will be sliding expiration or not.</param>
        public void Insert(string key, object value, TimeSpan timeToLive, bool doSlidingExpiration)
        {
            Insert(key, value, timeToLive, doSlidingExpiration, CacheConfigurator.DEFAULT_CACHE_PRIORITY);
        }

        /// <summary>
        /// Inserts an item into the cache.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        /// <param name="timeToLive">The interval between the time the inserted object was last accessed and the time at which that object expires</param>
        /// <param name="doSlidingExpiration">Specifies whether cached data will be sliding expiration or not.</param>
        /// <param name="priorityType">Cache Priority Type</param>
        public void Insert(string key, object value, TimeSpan timeToLive, bool doSlidingExpiration, CachePriorityTypes priorityType)
        {
            try
            {
                CacheItemPriority cacheItemPriority = default(CacheItemPriority);
                DateTime absoluteExpiration = default(DateTime);

                if (string.IsNullOrEmpty(key))
                {
                    //MSG : Cache Key string can not be null. Please, contact your system administrator.		
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00049", null);
                }


                cacheItemPriority = (CacheItemPriority)Enum.Parse(typeof(CacheItemPriority), priorityType.ToString());

                if (TimeSpan.Zero < timeToLive)
                {
                    if (doSlidingExpiration)
                    {
                        this._cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, timeToLive, cacheItemPriority, this._onRemoveDelegator);
                    }
                    else
                    {
                        absoluteExpiration = DateTime.Now.AddSeconds(timeToLive.TotalSeconds);
                        this._cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, cacheItemPriority, this._onRemoveDelegator);
                    }
                }
                else
                {
                    _cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, cacheItemPriority, this._onRemoveDelegator);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return;
        }
        #endregion


        #region INSTNACE METHOD AREA (ETC) *********************
        /// <summary>
        /// Determines whether a key exists in the cache using the specified name.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        public bool ContainsKey(string key)
        {
            bool contains = false;

            try
            {
                foreach (DictionaryEntry entry in this._cache)
                {
                    if (key.Equals(Convert.ToString(entry.Key)))
                        contains = true;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return contains;
        }

        /// <summary>
        /// Determines whether a key exists in the cache using the specified name.
        /// </summary>
        /// <param name="value">The identifier for the cache key to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        public bool ContainsStartsWith(string value)
        {
            bool contains = false;

            try
            {
                foreach (DictionaryEntry entry in this._cache)
                {
                    if (Convert.ToString(entry.Key).StartsWith(value))
                        contains = true;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return contains;
        }
        #endregion
    }
}
