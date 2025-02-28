/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2014 TOTAL SOFT BANK LIMITED. All Rights
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
* 2014.04.24  Jindols 1.0	First release.
* 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Item
{
    public class EnumDictionary<Enum, TValue>
    {
        #region FIELD AREA ***************************************
        private Dictionary<string, TValue> _dataDictionary;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// Gets the number of key/value pairs contained in the System.Collections.Generic.Dictionary<TKey,TValue>.
        /// </summary>
        public int Count
        {
            get { return _dataDictionary.Count; }
        }

        /// <summary>
        /// ets a collection containing the keys in the System.Collections.Generic.Dictionary<Enum,TValue>.
        /// </summary>
        public Dictionary<string, TValue>.KeyCollection Keys 
        {
            get { return _dataDictionary.Keys; }
        }

        /// <summary>
        ///  Gets a collection containing the values in the System.Collections.Generic.Dictionary<Enum,TValue>.
        /// </summary>
        public Dictionary<string, TValue>.ValueCollection Values
        {
            get { return _dataDictionary.Values; }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>
        /// The value associated with the specified key. If the specified key is not
        /// found, a get operation throws a System.Collections.Generic.KeyNotFoundException,
        /// and a set operation creates a new element with the specified key.
        /// </returns>
        /// <exception>
        /// System.ArgumentNullException:
        ///     key is null.
        /// System.Collections.Generic.KeyNotFoundException:
        ///     The property is retrieved and key does not exist in the collection.
        /// </exception>
        public TValue this[Enum key]
        {
            get
            {
                string keyStr = this.GetKey(key);

                return _dataDictionary[keyStr];
            }
            set
            {
                string keyStr = this.GetKey(key);

                _dataDictionary[keyStr] = value;
            }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.Dictionary<Enum,TValue>
        /// class that is empty, has the default initial capacity, and uses the default
        /// equality comparer for the key type.
        /// </summary>
        public EnumDictionary()
        {
            _dataDictionary = new Dictionary<string, TValue>();
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value"> The value of the element to add. The value can be null for reference types.</param>
        /// <exception>
        /// System.ArgumentNullException:
        ///     An element with the same key already exists in the System.Collections.Generic.Dictionary<TKey,TValue>.
        /// </exception>
        public void Add(Enum key, TValue value)
        {
            string keyStr = this.GetKey(key);

            _dataDictionary.Add(keyStr, value);
        }

        /// <summary>
        /// Removes the value with the specified key from the System.Collections.Generic.Dictionary<Enum,TValue>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully found and removed; otherwise, false.
        /// This method returns false if key is not found in the System.Collections.Generic.Dictionary<Enum,TValue>.
        /// </returns>
        /// <exception>
        /// System.ArgumentNullException:
        ///     key is null.
        /// </exception>
        public bool Remove(Enum key)
        {
            string keyStr = this.GetKey(key);

            return _dataDictionary.Remove(keyStr);
        }

        /// <summary>
        ///  Determines whether the System.Collections.Generic.Dictionary<Enum,TValue> contains the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to locate in the System.Collections.Generic.Dictionary<Enum,TValue>.
        /// </param>
        /// <returns>
        /// true if the System.Collections.Generic.Dictionary<Enum,TValue> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <exception>
        /// System.ArgumentNullException:
        ///     key is null.
        /// </exception>
        public bool ContainsKey(Enum key)
        {
            string keyStr = this.GetKey(key);
            return _dataDictionary.ContainsKey(keyStr);
        }

        /// <summary>
        /// Determines whether the System.Collections.Generic.Dictionary<Enum,TValue> contains a specific value.
        /// </summary>
        /// <param name="value">
        /// The value to locate in the System.Collections.Generic.Dictionary<Enum,TValue>.
        /// The value can be null for reference types.
        /// </param>
        /// <returns>
        /// true if the System.Collections.Generic.Dictionary<Enum,TValue> contains an element with the specified value; otherwise, false.
        /// </returns>
        public bool ContainsValue(TValue value)
        {
            return _dataDictionary.ContainsValue(value);
        }

        ///// <summary>
        /////  Removes the value with the specified key from the System.Collections.Generic.Dictionary<Enum,TValue>.
        ///// </summary>
        ///// <param name="key">The key of the element to remove.</param>
        ///// <returns>
        ///// true if the element is successfully found and removed; otherwise, false.
        ///// This method returns false if key is not found in the System.Collections.Generic.Dictionary<Enum,TValue>.
        ///// </returns>
        ///// <exception>
        ///// System.ArgumentNullException:
        /////     key is null.
        ///// </exception>
        //public bool Remove(Enum key)
        //{
        //    string keyStr = this.GetKey(key);
        //    return _dataDictionary.Remove(keyStr);
        //}

        public TValue Get(Enum key)
        {
            TValue returnValue = default(TValue);

            string keyStr = this.GetKey(key);

            if (_dataDictionary.ContainsKey(keyStr) == true)
            {
                returnValue = _dataDictionary[keyStr];
            }

            return returnValue;
        }

        /// <summary>
        /// Removes all keys and values from the System.Collections.Generic.Dictionary<Enum,TValue>.
        /// </summary>
        public void Clear()
        {
            _dataDictionary.Clear();
        }
        #endregion

        #region OTHER METHOD AREA*************************************
        private string GetKey(Enum key)
        {
            return key.ToString();
        }
        #endregion
    }
}
