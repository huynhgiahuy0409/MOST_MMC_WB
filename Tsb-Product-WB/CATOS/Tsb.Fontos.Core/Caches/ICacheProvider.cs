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
* DATE           AUTHOR		     REVISION    	
* 2009.09.07       CHOI 1.0	   First release.
* 2012.03.09  Tonny.Kim 1.1    Add ContainsStartsWith, RemoveStartsWith
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Tsb.Fontos.Core.Caches.Types;

namespace Tsb.Fontos.Core.Caches
{
    /// <summary>
    /// Represents  Cache Provider
    /// </summary>
    interface ICacheProvider
    {
        /// <summary>
        /// Gets a collection of all cache item keys.
        /// </summary>
        List<string> Keys { get; }


        /// <summary>
        /// Retrieves an item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        object Get(string key);


        /// <summary>
        /// Retrieves an item from the cache of the specified type.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        T Get<T>(string key);


        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache item to remove.</param>
        void Remove(string key);

        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <param name="value">The identifier for the cache key to remove.</param>
        void RemoveStartsWith(string value);


        /// <summary>
        /// Removes collection of items from the cache.
        /// </summary>
        /// <param name="keys">The list collection of cache key</param>
        void RemoveAll(List<string> keys);


        /// <summary>
        /// Removes all items from the cache.
        /// </summary>
        void Clear();


        /// <summary>
        /// Inserts an item into the cache.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        void Insert(string key, object value);


        /// <summary>
        /// Inserts an item into the cache.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        /// <param name="timeToLive">The interval between the time the inserted object was last accessed and the time at which that object expires</param>
        /// <param name="doSlidingExpiration">Specifies whether cached data will be sliding expiration or not.</param>
        void Insert(string key, object value, TimeSpan timeToLive, bool doSlidingExpiration);


        /// <summary>
        /// Inserts an item into the cache.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        /// <param name="timeToLive">The interval between the time the inserted object was last accessed and the time at which that object expires</param>
        /// <param name="doSlidingExpiration">Specifies whether cached data will be sliding expiration or not.</param>
        /// <param name="priorityType">Cache Priority Type</param>
        void Insert(string key, object value, TimeSpan timeToLive, bool doSlidingExpiration, CachePriorityTypes priorityType);


        /// <summary>
        /// Determines whether a key exists in the cache using the specified name.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        bool ContainsKey(string key);

        /// <summary>
        /// Determines whether a key exists in the cache using the specified name.
        /// </summary>
        /// <param name="key">The identifier for the cache key to retrieve.</param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        bool ContainsStartsWith(string value);
    }
}
