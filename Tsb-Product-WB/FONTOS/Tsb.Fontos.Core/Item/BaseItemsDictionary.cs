#region Class Definitions
/**
 * CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
 * LIMITED
 *
 * Copyright (C) 2005-2011 TOTAL SOFT BANK LIMITED. All Rights
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
 * 2010.12.20    Jindols    1.0	First release.
 * 
 */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using System.Reflection;

namespace Tsb.Fontos.Core.Item
{
    /// <summary>
    /// TSB Base Item Dictionary class
    /// </summary>
    [Serializable]
    public class BaseItemsDictionary<T> : ITsbBaseObject, IEnumerable<T> where T : IDataItem
    {
        #region FIELD AREA ***************************************
        /// <summary>
        /// Object ID
        /// </summary>
        private string _objectID = null;
        /// <summary>
        /// Source Data of Dictionary Type
        /// </summary>
        private Dictionary<string,T> _sourceDic = null;

        private bool _sortedListChangeRequired = false;
        private object _baseSortedList = null;
        private bool _isChangedList = false;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// Gets or Sets Object ID
        /// </summary>
        public string ObjectID
        {
            get { return this._objectID; }
            set { this._objectID = value; }
        }
        /// <summary>
        /// Gets or Sets Object Type
        /// </summary>
        public ObjectType ObjectType
        {
            get { return ObjectType.ITEM; }
        }
        /// <summary>
        ///  Gets an Integer containing the number of elements in this item list.
        /// </summary>
        public int Count
        {
            get { return _sourceDic.Count; }
        }
        /// <summary>
        /// Gets dictionary data
        /// </summary>
        protected Dictionary<string, T> Source
        {
            get { return _sourceDic; }
        }
        /// <summary>
        /// Gets or Sets a value that specifies whether a element item of list has been changed.
        /// </summary>
        protected bool IsChangedList
        {
            get { return _isChangedList; }
            set
            {
                _isChangedList = value;
                _sortedListChangeRequired = value;
            }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Default Constructor. In this constructor, a empty List(Of T) instance will be created.
        /// </summary>
        public BaseItemsDictionary()
            :this(null)
        {
        }
        /// <summary>
        /// Initializes a new instance of item list using a specified IList(Of T) implementation list.
        /// Initially, all element item of list are set as property changed event enabled.
        /// </summary>
        /// <param name="list">IList(Of T) implementation list</param>
        public BaseItemsDictionary(IList<T> list)
            //: this(list, true)
        {
            _objectID = "GNR-FTCO-ITM-BaseItemsDictionary";
            _sourceDic = MakeDictionaryData(list);
            this.IsChangedList = true;
        }
        ///// <summary>
        ///// Initializes a new instance of item list using a specified IList(Of T) implementation list.
        ///// Initially, all element item of list are set as property changed event enabled.
        ///// </summary>
        ///// <param name="list">IList(Of T) implementation list</param>
        ///// <param name="isBackupRequired">whether to make BackupItem or not</param>
        //public BaseItemsDictionary(IList<T> list, bool isBackupRequired)
        //{
        //    _objectID = "GNR-FTCO-ITM-BaseItemsDictionary";
        //    _sourceDic = MakeDictionaryData(list);
        //}
        /// <summary>
        /// Make Dictionary data
        /// </summary>
        /// <param name="list"IList(Of T) implementation list></param>
        private Dictionary<string,T> MakeDictionaryData(IList<T> list)
        {
            Dictionary<string, T> dic = new Dictionary<string, T>();
            if (list != null)
            {
                foreach (T item in list)
                {
                    if (item != null && item.Key != null)
                    {
                        if (dic.ContainsKey(item.Key) == false)
                        {
                            dic.Add(item.Key, item);
                        }
                    }
                }
            }
            
            return dic;
        }
        #endregion

        #region METHOD AREA (ELEMENT - ADD ITEM)**************
        /// <summary>
        ///  Adds an element with the provided key and value to item list.
        /// </summary>
        /// <param name="key">
        /// The object to use as the key of the element to add. y 
        /// This parameter wiil be set as the key field of Tsb.Fontos.Core.Item.BaseDataItem.</param>
        /// <param name="item">The object to use as the value of the element to add.</param>
        /// <returns>
        /// true, if item is successfully add; otherwise, false.
        /// This method also returns false if item already exist in the item list
        /// </returns>
        public virtual bool Add(string key, T item)
        {
            if (this.ContainsKey(key) == false)
            {
                _sourceDic.Add(key, item);
                this.IsChangedList = true;
                return true;
            }

            return false;
        }
        /// <summary>
        /// Adds item elements of the specified list to the end of the item list.
        /// The key property of item  will be campared  Key field of Tsb.Fontos.Core.Item.BaseDataItem
        /// </summary>
        /// <param name="list">List to add</param>
        public void AddRange(BaseItemsList<T> list)
        {
            try
            {
                if (list == null) return;

                foreach (T item in list)
                {
                    if (item != null && item.Key != null)
                    {
                        this.Add(item.Key, item);
                    }
                }

                this.IsChangedList = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region METHOD AREA (ELEMENT - REMOVE ITEM)**************
        /// <summary>
        /// Removes the item with the specified key from the item list
        /// The key parameter will be campared  Key field of Tsb.Fontos.Core.Item.BaseDataItem
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        public virtual void Remove(string key)
        {
            if (this.ContainsKey(key))
            {
                _sourceDic.Remove(key);
                this.IsChangedList = true;
            }
        }
        /// <summary>
        /// Removes the first occurrence of a specific item object from item list.
        /// The key property of item  will be campared  Key field of Tsb.Fontos.Core.Item.BaseDataItem
        /// </summary>
        /// <param name="item">A item object to remove from item list</param>
        /// <returns>
        /// true if item is successfully removed; otherwise, false. 
        /// This method also returns false if item was not found in the List(Of T).
        /// </returns>
        public virtual bool RemoveItem(T item)
        {
            bool isRemoved = false;
            try
            {
                if (item != null && item.Key != null)
                {
                    this.Remove(item.Key);
                }

                isRemoved = true;
                this.IsChangedList = true;
            }
            catch (Exception)
            {

                isRemoved = false;
            }

            return isRemoved;
        }
        #endregion

        #region METHOD AREA (ELEMENT - CONTAINS)*********************
        /// <summary>
        ///  Determines whether the item list contains an item with the specified key.
        /// </summary>
        /// <param name="key"> The key to locate in the item list.</param>
        /// <returns> 
        ///  true if the item list contains an item with the key; otherwise, false.
        /// </returns>
        public virtual bool ContainsKey(string key)
        {
            return _sourceDic.ContainsKey(key);
        }
        #endregion

        #region METHOD AREA (LIST - GET LIST AS List<T>)*************
        /// <summary>
        /// Returns item list object reference as List(Of T) type
        /// </summary>
        /// <returns>Its item list object reference as List(Of T) type</returns>
        public virtual List<T> GetList()
        {
            List<T> list = new List<T>();

            foreach (T item in _sourceDic.Values)
            {
                list.Add(item);
            }
            
            return list;
        }
        /// <summary>
        /// Returns the item list those element item property name and value are correspond to specified parameters
        /// </summary>
        /// <param name="propName">The property name of item to compare with compareValue</param>
        /// <param name="compareValue">The value of a specified named property</param>
        /// <returns> the item list those element item property name and value are correspond to specified parameters</returns>
        public virtual List<T> GetList(string propName, object compareValue)
        {
            List<T> rtnList = null;
            PropertyInfo propInfo = null;
            Type itemType = null;
            object propValue = null;

            try
            {
                rtnList = new List<T>();

                if (compareValue == null)
                {
                    return rtnList;
                }

                foreach (T item in _sourceDic.Values)
                {
                    itemType = item.GetType();

                    propInfo = itemType.GetProperty(propName);
                    propValue = propInfo.GetValue(item, null);

                    if (propValue != null && propValue.ToString().Equals(compareValue.ToString()))
                    {
                        rtnList.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return rtnList;
        }
        #endregion

        #region METHOD AREA (LIST - CLEAR LIST)**********************
        /// <summary>
        /// Removes all items from the item list
        /// </summary>
        public virtual void Clear()
        {
            try
            {
                if (this._sourceDic != null)
                {
                    _sourceDic.Clear();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return;
        }
        #endregion

        #region METHOD AREA (ELEMENT - GET ITEM)*********************
        /// <summary>
        /// Returns the first occurrence element item those property name and value are correspond to its
        /// </summary>
        /// <param name="propName">The property name of item to compare with compareValue</param>
        /// <param name="compareValue">The value of a specified named property</param>
        /// <returns> The first occurrence element item those property name and value are correspond to its</returns>
        public virtual T GetItem(string propName, object compareValue)
        {
            PropertyInfo propInfo = null;
            Type itemType = null;
            T rtnItem = default(T);
            object propValue = null;

            try
            {
                if (compareValue != null)
                {
                    foreach (T item in _sourceDic.Values)
                    {
                        itemType = item.GetType();

                        propInfo = itemType.GetProperty(propName);
                        propValue = propInfo.GetValue(item, null);

                        if (propValue != null && propValue.ToString().Equals(compareValue.ToString()))
                        {
                            rtnItem = item;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rtnItem;
        }
        /// <summary>
        /// Returns the first occurrence element item with a specified key
        /// The key parameter will be campared  Key field of Tsb.Fontos.Core.Item.BaseDataItem
        /// </summary>
        /// <param name="key">The key of the element to retrieve.</param>
        /// <returns>The element item with a specified key in the item list.</returns>
        public virtual T GetItem(string key)
        {
            if (this.ContainsKey(key) == true)
            {
                return _sourceDic[key];
            }
            return default(T);
        }
        #endregion

        #region METHOD AREA (LIST - GET ENUMERATOR)******************
        /// <summary>
        /// Returns an enumerator that iterates through the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.
        /// </summary>
        /// <returns>
        /// A System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.Enumerator
        /// for the System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _sourceDic.Values.GetEnumerator();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the System.Collections.Generic.Dictionary<TKey,TValue>.
        /// </summary>
        /// <returns>
        /// A System.Collections.Generic.Dictionary<TKey,TValue>.Enumerator structure
        /// for the System.Collections.Generic.Dictionary<TKey,TValue>.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _sourceDic.GetEnumerator();
        }
        #endregion

        #region METHOD AREA (LIST - GET LIST AS SortedList<string, T>)******************
        /// <summary>
        /// Returns a instance of the SortedList(Of string, T) list object reference that contains elements copied from the original item list (List(Of T) type, 
        /// has the same initial capacity as the number of elements copied, and is sorted according to the default IComparer(Of T).
        /// </summary>
        /// <typeparam name="TKey">The type of key for the SortedList.</typeparam>
        /// <returns>A instance of the SortedList(Of string, T) list object reference that contains elements copied from the original item list (List(Of T) type</returns>
        public virtual SortedList<string, T> GetSortedList()
        {
            SortedList<string, T> sortedList = null;
            
            try
            {
                if (this._sortedListChangeRequired)
                {
                    if (this._baseSortedList != null)
                    {
                        (this._baseSortedList as SortedList<string, T>).Clear();
                        this._baseSortedList = null;
                    }

                    sortedList = new SortedList<string, T>();

                    string key = null;

                    foreach (string ikey in this._sourceDic.Keys)
                    {
                        sortedList.Add(key, _sourceDic[ikey]);
                    }

                    this._baseSortedList = sortedList;
                    this._sortedListChangeRequired = false;
                }
                else
                {
                    sortedList = _baseSortedList as SortedList<string, T>;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return sortedList;
        }
        #endregion
    }
}
