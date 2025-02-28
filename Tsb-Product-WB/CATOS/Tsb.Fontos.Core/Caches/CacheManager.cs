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
using Tsb.Fontos.Core.Caches.Types;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Caches
{
    /// <summary>
    /// Cache Manager Class
    /// </summary>
    public class CacheManager : TsbBaseObject
    {
        #region FIELD AREA *************************************
        private static ICacheProvider _cacheProvider;
        #endregion


        #region PROPERTY AREA **********************************
        /// <summary>
        /// Gets the cache keys list
        /// </summary>
        public static List<string> Keys
        {
            get
            {
                return _cacheProvider.Keys;
            }
        }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initialize CacheManager Instance
        /// </summary>
        static CacheManager()
        {
            _cacheProvider = new HttpRuntimeCacheProvider();
        }
        #endregion


        #region METHOD AREA (GET) ******************************
        /// <summary>
        /// Retrieves an item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        public static object Get(string key)
        {
            return _cacheProvider.Get(key);
        }


        /// <summary>
        /// Retrieves an item from the cache of the specified type.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        public static T Get<T>(string key)
        {
            return _cacheProvider.Get<T>(key);
        }
        #endregion


        #region METHOD AREA (REMOVE) ***************************
        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache item to remove.</param>
        public static void Remove(string key)
        {
            _cacheProvider.Remove(key);
        }


        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <param name="value">The identifier for the cache item to remove.</param>
        public static void RemoveStartsWith(string value)
        {
            _cacheProvider.RemoveStartsWith(value);
        }


        /// <summary>
        /// Removes collection of items from the cache.
        /// </summary>
        /// <param name="keys">The list collection of cache key</param>
        public static void RemoveAll(List<string> keys)
        {
            _cacheProvider.RemoveAll(keys);
        }


        /// <summary>
        /// Removes all items from the cache.
        /// </summary>
        public static void Clear()
        {
            _cacheProvider.Clear();
        }
        #endregion


        #region METHOD AREA (INSERT) ***************************
        /// <summary>
        /// Inserts an item into the cache.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        public static void Insert(string key, object value)
        {
            _cacheProvider.Insert(key, value);           
        }


        /// <summary>
        /// Inserts an item into the cache.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        /// <param name="timeToLive">The interval between the time the inserted object was last accessed and the time at which that object expires</param>
        /// <param name="doSlidingExpiration">Specifies whether cached data will be sliding expiration or not.</param>
        public static void Insert(string key, object value, TimeSpan timeToLive, bool doSlidingExpiration)
        {
            _cacheProvider.Insert(key, value, timeToLive, doSlidingExpiration);
        }


        /// <summary>
        /// Inserts an item into the cache.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        /// <param name="timeToLive">The interval between the time the inserted object was last accessed and the time at which that object expires</param>
        /// <param name="doSlidingExpiration">Specifies whether cached data will be sliding expiration or not.</param>
        /// <param name="priorityType">Cache Priority Type</param>
        public static void Insert(string key, object value, TimeSpan timeToLive, bool doSlidingExpiration, CachePriorityTypes priorityType)
        {
            _cacheProvider.Insert(key, value, timeToLive, doSlidingExpiration, priorityType);
        }
        #endregion


        #region METHOD AREA (ETC) ******************************

        /// <summary>
        /// Determines whether a key exists in the cache using the specified name.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        public static bool ContainsKey(string key)
        {
            return _cacheProvider.ContainsKey(key);
        }

        public static bool ContainsStartsWith(string value)
        {
            return _cacheProvider.ContainsStartsWith(value);
        }
        #endregion
    }
}
