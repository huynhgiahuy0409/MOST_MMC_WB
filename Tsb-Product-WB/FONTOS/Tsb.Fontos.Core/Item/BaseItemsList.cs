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
 * DATE           AUTHOR		REVISION    	
 * 2010.06.16    Jin Souk    1.0	First release.
 * 2010.10.27    CHOI        Delete Unused method definition / Add Comments / Refactoring
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Exceptions;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Tsb.Fontos.Core.Exceptions.System;

namespace Tsb.Fontos.Core.Item
{
    /// <summary>
    /// TSB Base Item List class
    /// </summary>
    [Serializable]
    public class BaseItemsList<T> : IDataItemsList<T>, ITsbBaseObject where T : IDataItem
    {
        #region FIELD/PROPERTY AREA**********************************
        private IList<T> _sourceList   = null;
        private object _baseSortedList = null;
        private object _baseDictionary = null;
        private object _baseBindingList= null;

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// (This property is a implementation for IList Item property)
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        object IList.this[int index]
        {
            get
            {
                try
                {
                    return (_sourceList as IList)[index];
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            set
            {
                try
                {
                    (_sourceList as IList)[index] = value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// (This property is a implementation for List(Of T) Item property)
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[int index]
        {
            get
            {
                try
                {
                    return _sourceList[index];
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            set
            {
                try
                {
                    _sourceList[index] = value;
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }

        /// <summary>
        /// Gets an Integer containing the number of elements in this item list.
        /// </summary>
        public virtual int DataCount
        {
            get { return this.Count; }
        }

        /// <summary>
        ///  Gets an Integer containing the number of elements in this item list.
        ///  (This property is a implementation for IList Count property)
        /// </summary>
        public int Count
        {
            get { return _sourceList.Count; }
        }

        /// <summary>
        ///  Gets a value indicating whether this item is read-only.
        ///  true if this item list is read-only; otherwise, false.
        ///  (This property is a implementation for IList IsReadOnly property)
        /// </summary>
        public bool IsReadOnly
        {
            get { return _sourceList.IsReadOnly; }
        }

        /// <summary>
        /// Gets a value indicating whether access to this item list is synchronized (thread safe).
        /// (This property is a implementation for IList IsSynchronized property)
        /// </summary>
        /// <returns>
        bool ICollection.IsSynchronized
        {
            get { return (_sourceList as IList).IsSynchronized; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to this item list
        /// (This property is a implementation for IList SyncRoot property)
        /// </summary>
        object ICollection.SyncRoot
        {
            get { return (_sourceList as IList).SyncRoot; }
        }

        /// <summary>
        /// Gets a value indicating whether this item list has a fixed size.
        /// (This property is a implementation for IList IsFixedSize property)
        /// </summary>
        bool IList.IsFixedSize
        {
            get { return (_sourceList as IList).IsFixedSize; }
        }

        private bool _isChangedList = false;
        private bool _sortedListChangeRequired  = false;
        private bool _dictonaryChangeRequired   = false;
        /// <summary>
        /// Gets or Sets a value that specifies whether a element item of list has been changed.
        /// </summary>
        protected bool IsChangedList
        {
            get { return _isChangedList; }
            set 
            {
                _isChangedList = value;
                _sortedListChangeRequired  = value;
                _dictonaryChangeRequired   = value;
            }
        }

        private string _objectID = null;
        /// <summary>
        /// Gets or Sets Object ID
        /// </summary>
        public string ObjectID
        {
            get { return this._objectID; }
            set { this._objectID = value; }
        }

        /// <summary>
        /// Gets Object Type
        /// </summary>
        public ObjectType ObjectType
        {
            get { return ObjectType.ITEM; }
        }

        private bool _isBackupRequired = true;

        /// <summary>
        /// Gets or Sets IsBackupRequired
        /// </summary>
        public bool IsBackupRequired
        {
            get { return this._isBackupRequired; }
            set { this._isBackupRequired = value; }
        }
        #endregion

        #region INITIALIZATION AREA *********************************
        /// <summary>
        /// Default Constructor. In this constructor, a empty List(Of T) instance will be created.
        /// </summary>
        public BaseItemsList()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of item list using a specified IList(Of T) implementation list.
        /// Initially, all element item of list are set as property changed event enabled.
        /// </summary>
        /// <param name="list">IList(Of T) implementation list</param>
        public BaseItemsList(IList<T> list)
            : this(list, true)
        { }

        /// <summary>
        /// Initializes a new instance of item list using a specified IList(Of T) implementation list.
        /// Initially, all element item of list are set as property changed event enabled.
        /// </summary>
        /// <param name="list">IList(Of T) implementation list</param>
        /// <param name="isBackupRequired">whether to make BackupItem or not</param>
        public BaseItemsList(IList<T> list, bool isBackupRequired)
        {
            _objectID = "GNR-FTCO-ITM-BaseItemsList";
            _isBackupRequired = isBackupRequired;

            if (list == null)
            {
                list = new List<T>();
            }

            _sourceList = list;

            this.IsChangedList = true;
            this.UnLockNotifyPropertyChanged(_sourceList);
        }
        #endregion

        #region METHOD AREA (ELEMENT - GET ITEM)*********************
        /// <summary>
        /// Returns the element item at a specified index in a sequence.
        /// (This method is a implementation of IDataItemList GetItem(int) method)
        /// </summary>
        /// <param name="index">The zero-based index of the element item to retrieve.</param>
        /// <returns>The element item at the specified position in the item list.</returns>
        public virtual object GetItem(int index)
        {
            return _sourceList.ElementAt<T>(index);
        }

        /// <summary>
        /// Returns the first occurrence element item with a specified key
        /// The key parameter will be campared  Key field of Tsb.Fontos.Core.Item.BaseDataItem
        /// </summary>
        /// <param name="key">The key of the element to retrieve.</param>
        /// <returns>The element item with a specified key in the item list.</returns>
        public virtual T GetItem(string key)
        {
            foreach (T item in _sourceList)
            {
                if (item != null && (item is IDataItem))
                {
                    if ((item as IDataItem).Key != null && (item as IDataItem).Key.Equals(key))
                    {
                        return item;                        
                    }
                }
            }

            return default(T);
        }

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
                    foreach (T item in _sourceList)
                    {
                        itemType = item.GetType();

                        propInfo  = itemType.GetProperty(propName);
                        propValue = propInfo.GetValue(item, null);

                        if(propValue != null && propValue.ToString().Equals(compareValue.ToString()))
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
        ///  Gets the item with the specified key from the SortedList(Of TKey, TValue) item list.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the SortedList(Of TKey, TValue) item list.</typeparam>
        /// <param name="key">The key of the element to retrieve.</param>
        /// <returns>The item with the specified key.</returns>
        public virtual T GetItemBySortedList<TKey>(TKey key)
        {
            SortedList<TKey, T> sortedList = this.GetSortedList<TKey>();
            if (sortedList != null)
            {
                if (sortedList.ContainsKey(key))
                {
                    return sortedList[key];
                }
            }

            return default(T);
        }

        /// <summary>
        ///  Gets the item with the specified key from the  Dictionary(Of TKey, T) item list
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the Dictionary(Of TKey, T) item list.</typeparam>
        /// <param name="key">The key of the element to retrieve.</param>
        /// <returns>The item with the specified key.</returns>
        public virtual T GetItemByDictionary<TKey>(TKey key)
        {
            Dictionary<TKey, T> dictionary = this.GetDictionary<TKey>();
            if (dictionary != null)
            {
                if (dictionary.ContainsKey(key))
                {
                    return dictionary[key];
                }
            }

            return default(T);
        }
        #endregion

        #region METHOD AREA (ELEMENT - INDEX OF ITEM)****************
        /// <summary>
        /// Searches for the specified item and returns the zero-based index of the first occurrence within the entire item list.
        /// (This method is a implementation of IList(Of T) IndexOf(Of T) method)
        /// </summary>
        /// <param name="item">The item to locate in the item list. The value can be null for reference types</param>
        /// <returns>The index of item if found in the item list; otherwise, -1.</returns>
        public virtual int IndexOf(T item)
        {
            return _sourceList.IndexOf(item);
        }

        /// <summary>
        /// Searches for the specified item and returns the zero-based index of the first occurrence within the entire item list.
        /// (This method is a implementation of IList IndexOf(object) method)
        /// </summary>
        /// <param name="value">The item to locate in the item list. The value can be null for reference types</param>
        /// <returns>The index of value if found in the list; otherwise, -1.</returns>
        int IList.IndexOf(object value)
        {
            return (_sourceList as IList).IndexOf(value);
        }
        #endregion

        #region METHOD AREA (ELEMENT - CONTAINS)*********************
        /// <summary>
        ///  Determines whether the item list contains a specific item.
        ///  (This method is a implementation of ICollection(Of T) Contains(T) method)
        /// </summary>
        /// <param name="item">The item object to check existence within the item list</param>
        /// <returns>true if the item is found in the item list; otherwise, false.</returns>
        public virtual bool Contains(T item)
        {
            return _sourceList.Contains(item);
        }

        /// <summary>
        /// Determines whether the item list contains a specific item.
        /// (This method is a implementation of IList Contains(object) method)
        /// </summary>
        /// <param name="value">The item object to check existence within the item list</param>
        /// <returns>true if the item is found in the item list; otherwise, false.
        /// </returns>
        bool IList.Contains(object value)
        {
            return (_sourceList as IList).Contains(value);
        }

        /// <summary>
        ///  Determines whether the item list contains an item with the specified key.
        /// </summary>
        /// <param name="key"> The key to locate in the item list.</param>
        /// <returns> 
        ///  true if the item list contains an item with the key; otherwise, false.
        /// </returns>
        public virtual bool ContainsKey(string key)
        {
            IList list = _sourceList.Where(p => p.Key == key).ToList();
            if (list != null && list.Count > 0)
            {
                return true;
            }

            return false;

            //bool isContained = false;

            //foreach (IDataItem item in _sourceList)
            //{
            //    if (item != null && item.Key.Equals(key))
            //    {
            //        isContained = true;
            //        break;
            //    }

            //}

            //return isContained;
        }
        #endregion

        #region METHOD AREA (ELEMENT - REMOVE ITEM)******************
        /// <summary>
        /// Removes the first occurrence of a specific item object from item list.
        /// (This method is a implementation of IDataItemList RemoveItem(object item) method)
        /// </summary>
        /// <param name="item">A item object to remove from item list</param>
        /// <returns>
        /// true if item is successfully removed; otherwise, false. 
        /// This method also returns false if item was not found in the List(Of T).
        /// </returns>
        public virtual bool RemoveItem(object item)
        {
            BaseDataItem dataItem = null;
            BaseDataItem toRemoveItem = item as BaseDataItem;

            bool isRemoved = false;


            for (int i = 0; i < this.Count; i++)
            {
                dataItem = this.ElementAt<T>(i) as BaseDataItem;

                if (dataItem.GUID.Equals(toRemoveItem.GUID))
                {
                    this.RemoveAt(i);
                    isRemoved = true;
                    break;
                }
            }

            if (isRemoved)
            {
                this.IsChangedList = true;
            }

            return isRemoved;
        }

        /// <summary>
        /// Removes the element at the specified index of the item list
        /// (This method is a implementation of IList(Of T) RemoveAt(int) method)
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public virtual void RemoveAt(int index)
        {
            try
            {
                if (this._sourceList.Count >= (index +1))
                {
                    this._sourceList.RemoveAt(index);
                    this.IsChangedList = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return;
        }

        /// <summary>
        /// Removes the first occurrence of a specific item object from the item list.
        /// (This method is a implementation of ICollection(Of T) RemoveItem(object item) method)
        /// </summary>
        /// <param name="item">The item object to remove from the item list.</param>
        /// <returns>
        /// true if item was successfully removed from the item list; otherwise, false. 
        /// This method also returns false if item is not found in the item list
        /// </returns>
        public virtual bool Remove(T item)
        {
            bool isRemoved = false;

            isRemoved = _sourceList.Remove(item);

            if (isRemoved)
            {
                this.IsChangedList = true;
            }

            return isRemoved;
        }

        /// <summary>
        /// Removes the first occurrence of a specific item from the item list
        /// (This method is a implementation of IList Remove(object item) method)
        /// </summary>
        /// <param name="value">The item to remove from the item list.</param>
        void IList.Remove(object value)
        {
            bool isContains = false;
            IList tempList = _sourceList as IList;

            try
            {
                isContains = tempList.Contains(value);

                if (isContains == true)
                {
                    tempList.Remove(value);
                    this.IsChangedList = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return;
        }

        /// <summary>
        /// Removes the item with the specified key from the item list
        /// The key parameter will be campared  Key field of Tsb.Fontos.Core.Item.BaseDataItem
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        public virtual void Remove(string key)
        {
            try
            {
                #region BELOW BLOCK IS REFACTORED BY CHOI : 2010.10.28
                //IList<IDataItem> list = new List<IDataItem>();
                //foreach (IDataItem item in _sourceList)
                //{
                //    if (item != null && item.Key.Equals(key))
                //    {
                //        list.Add(item);
                //    }
                //}

                //foreach (T item in list)
                //{
                //    this.Remove(item);
                //}
                #endregion

                IDataItem item = null;

                for(int i = 0; i < this._sourceList.Count ; i ++ )
                {
                    item = this._sourceList[i] as IDataItem;

                    if (item != null && item.Key != null && item.Key.Equals(key))
                    {
                        this.RemoveAt(i);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region METHOD AREA (ELEMENT - INSERT/ADD ITEM)**************
        /// <summary>
        /// Inserts an element into the item list at the specified index.
        /// (This method is a implementation of IList(Of T) Insert(int,T) method)
        /// </summary>
        /// <param name="index">The zero-based index  at which item should be inserted.</param>
        /// <param name="item">The item object to insert into the item list.</param>
        public virtual void Insert(int index, T item)
        {
            try
            {
                _sourceList.Insert(index, item);

                this.IsChangedList = true;
                this.UnLockNotifyPropertyChanged(item);
            }
            catch (Exception e)
            {
                throw e;
            }
            return;
        }

        /// <summary>
        /// Inserts an item to the item list at the specified index.
        /// (This method is a implementation of IList Insert(int,object) method)
        /// </summary>
        /// <param name="index">The zero-based index at which value should be inserted.</param>
        /// <param name="value">The item to insert into the item list.</param>
        void IList.Insert(int index, object value)
        {
            try
            {
                (_sourceList as IList).Insert(index, value);

                this.IsChangedList = true;
                if (value is IDataItem)
                {
                    this.UnLockNotifyPropertyChanged(value as IDataItem);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return;
        }

        /// <summary>
        ///  Adds an item to the item list
        ///  (This method is a implementation of ICollection(Of T) Add(T) method)
        /// </summary>
        /// <param name="item">The object to add to the item list.</param>
        public virtual void Add(T item)
        {
            try
            {
                _sourceList.Add(item);

                this.IsChangedList = true;
                this.UnLockNotifyPropertyChanged(item);
            }
            catch (Exception e)
            {
                throw e;
            }
            return;
        }

        /// <summary>
        /// Adds an item to the item list
        /// (This method is a implementation of IList Add(object) method)
        /// </summary>
        /// <param name="value">The item to add to item list.</param>
        /// <returns>The position into which the new item was inserted.</returns>
        int IList.Add(object value)
        {
            int insertedPos = -1;

            try
            {
                insertedPos = (_sourceList as IList).Add(value);

                if(insertedPos >=0)
                {
                    this.IsChangedList = true;
                    this.UnLockNotifyPropertyChanged(value as IDataItem);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return insertedPos;
        }

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
            try
            {
                if (this.ContainsKey(key) == false)
                {
                    if (item is IDataItem)
                    {
                        (item as IDataItem).Key = key;
                    }

                    this.Add(item);
                    this.IsChangedList = true;
                    this.UnLockNotifyPropertyChanged(item);
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Adds item elements of the specified list to the end of the item list.
        /// </summary>
        /// <param name="list">List to add</param>
        public void AddRange(BaseItemsList<T> list)
        {
            try
            {
                if (list == null) return;

                foreach (T item in list)
                {
                    this.Add(item);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region METHOD AREA (LIST - GET LIST AS List<T>)*************
        /// <summary>
        /// Returns item list object reference as List(Of T) type
        /// </summary>
        /// <returns>Its item list object reference as List(Of T) type</returns>
        public virtual List<T> GetList()
        {
            return this._sourceList as List<T>;
        }
        #endregion

        #region METHOD AREA (LIST - GET CONDITION MATCHED LIST)******
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

                foreach (T item in _sourceList)
                {
                    itemType = item.GetType();

                    propInfo  = itemType.GetProperty(propName);
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

        #region METHOD AREA (LIST - GET LIST AS BindingList<T>)******
        /// <summary>
        /// Returns a instance of the BindingList(Of T) list object reference that contains elements copied from the original item list (List(Of T) type, 
        /// (This method is a implementation of IDataItemList(Of T) GetBindingList() method)
        /// </summary>
        /// <returns>Item list object reference as BindingList(Of T) type</returns>
        public virtual BindingList<T> GetBindingList()
        {
            if (this._baseBindingList == null)
            {
                this._baseBindingList = new BindingList<T>(this._sourceList);
            }

            return this._baseBindingList as BindingList<T>;
        }
        #endregion

        #region METHOD AREA (LIST - GET LIST AS SortedList<TKey, T>)*
        /// <summary>
        /// Returns a instance of the SortedList(Of TKey, T) list object reference that contains elements copied from the original item list (List(Of T) type, 
        /// has the same initial capacity as the number of elements copied, and is sorted according to the default IComparer(Of T).
        /// </summary>
        /// <typeparam name="TKey">The type of key for the SortedList.</typeparam>
        /// <returns>A instance of the SortedList(Of TKey, T) list object reference that contains elements copied from the original item list (List(Of T) type</returns>
        public virtual SortedList<TKey, T> GetSortedList<TKey>()
        {
            SortedList<TKey, T> sortedList = null;
            TKey value;
            try
            {
                if (this._sortedListChangeRequired)
                {
                    if (this._baseSortedList != null)
                    {
                        (this._baseSortedList as SortedList<TKey, T>).Clear();
                        this._baseSortedList = null;
                    }

                    sortedList = new SortedList<TKey, T>();
                    
                    string key = null;

                    foreach (T item in this._sourceList)
                    {
                        if (item != null && item is IDataItem)
                        {
                            key = (item as IDataItem).Key;
                            value = this.GetKeyValue<TKey>(key);
                            
                            if (value != null)
                            {
                                if (sortedList.ContainsKey(value) == false)
                                {
                                    sortedList.Add(value, item);
                                }
                            }
                        }
                    }

                    this._baseSortedList = sortedList;
                    this._sortedListChangeRequired = false;
                }
                else
                {
                    sortedList = _baseSortedList as SortedList<TKey, T>;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return sortedList;
        }
        #endregion

        #region METHOD AREA (LIST - GET LIST AS Dictionary<TKey, T>)*
        /// <summary>
        /// Returns a instance of the Dictionary(Of TKey, T) object reference that contains elements copied from the original item list (List(Of T) type.
        /// </summary>
        /// <typeparam name="TKey">The type of key for the Dictionary.</typeparam>
        /// <returns>A instance of the Dictionary(Of TKey, T) object reference that contains elements copied from the original item list (List(Of T) type</returns>
        public virtual Dictionary<TKey, T> GetDictionary<TKey>()
        {
            Dictionary<TKey, T> dictionaryList = null;
            TKey value;
            try
            {
                if (this._dictonaryChangeRequired)
                {
                    if (_baseDictionary != null)
                    {
                        (_baseDictionary as Dictionary<TKey, T>).Clear();
                        _baseDictionary = null;
                    }

                    dictionaryList = new Dictionary<TKey, T>();
                    string key;

                    foreach (T item in _sourceList)
                    {
                        if (item != null && item is IDataItem)
                        {
                            key = (item as IDataItem).Key;
                            value = this.GetKeyValue<TKey>(key);

                            if (value != null)
                            {
                                if (dictionaryList.ContainsKey(value) == false)
                                {
                                    dictionaryList.Add(this.GetKeyValue<TKey>(key), item);
                                }
                            }
                        }
                    }

                    this._baseDictionary = dictionaryList;
                    this._dictonaryChangeRequired = false;
                }
                else
                {
                    dictionaryList = this._baseDictionary as Dictionary<TKey, T>;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return dictionaryList;
        }
        #endregion

        #region METHOD AREA (LIST - CLEAR LIST)**********************
        /// <summary>
        /// Removes all items from the item list
        /// (This method is a implementation of ICollection(Of T) Clear() method)
        /// </summary>
        public virtual void Clear()
        {
            try
            {
                if (this._baseDictionary != null)
                {
                    if (this._baseDictionary is IDictionary)
                    {
                        (this._baseDictionary as IDictionary).Clear();
                    }

                    this._baseDictionary = null;
                }

                if (this._baseSortedList != null)
                {
                    if (this._baseSortedList is IDictionary)
                    {
                        (this._baseSortedList as IDictionary).Clear();
                    }

                    this._baseSortedList = null;
                }

                foreach (T item in _sourceList)
                {
                    if (item is BaseDataItem)
                    {
                        (item as BaseDataItem).BackupItem = null;
                    }
                }

                this._sourceList.Clear();

                this.IsChangedList = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return;
        }
        #endregion

        #region METHOD AREA (LIST - COPY)****************************
        /// <summary>
        /// Copies the elements of the item list to an Array, starting at a particular Array index.
        /// (This method is a implementation of ICollection(Of T) CopyTo(T[],int) method)
        /// </summary>
        /// <param name="array">
        /// The one-dimensional Array that is the destination of the elements copied from item list.
        /// </param>
        /// <param name="arrayIndex"> The zero-based index in array at which copying begins.</param>
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            try
            {
                _sourceList.CopyTo(array, arrayIndex);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Copies the elements of the item list to an Array, starting at a particular Array index.
        /// (This method is a implementation of ICollection CopyTo(System.Array[],int) method)
        /// </summary>
        /// <param name="array">
        ///  The one-dimensional System.Array that is the destination of the elements
        ///  copied from the item list. 
        /// </param>
        /// <param name="index">
        /// The zero-based index in array at which copying begins.
        /// </param>
        void ICollection.CopyTo(Array array, int index)
        {
            try
            {
                (_sourceList as IList).CopyTo(array, index);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region METHOD AREA (LIST - GET ENUMERATOR)******************
        /// <summary>
        /// Returns an enumerator that iterates through the item list.
        /// (This method is a implementation of IEnumerable(Of T) GetEnumerator() method)
        /// </summary>
        /// <returns>
        /// A System.Collections.Generic.IEnumerator(Of T) that can be used to iterate through the item list.
        /// </returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return _sourceList.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the item list.
        /// (This method is a implementation of IEnumerable GetEnumerator() method)
        /// </summary>
        /// <returns>
        /// An System.Collections.IEnumerator object that can be used to iterate through the item list
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sourceList.GetEnumerator();
        }
        #endregion

        #region METHOD AREA (LIST - GET CLONED ITEM LIST)************
        /// <summary>
        /// Returns a cloned list of this item list
        /// </summary>
        public BaseItemsList<T> GetCloneList()
        {
            BaseItemsList<T> list = new BaseItemsList<T>();

            foreach (T item in this._sourceList)
            {
                Type type = item.GetType();
                T addItem = default(T);
                addItem = ObjectCreator.CloneObject<T>(item);
                list.Add(addItem);
            }

            return list;
        }
        #endregion

        #region METHOD AREA (OTHER - UN LOCK NOTIFY PROPERTY CHANGED*
        /// <summary>
        ///  Un-set(set false) the UnNotifyPropertyChanged flag for all of element item in a specified item list.
        ///  Un-set element item will be affected by NotifyPropertyChanged event
        /// </summary>
        /// <param name="list">A List to un-set LockNotifyProperty</param>
        private void UnLockNotifyPropertyChanged(IList<T> list)
        {
            if (list == null) return;

            foreach (IDataItem item in list)
            {
                this.UnLockNotifyPropertyChanged(item);
            }
            
            return;
        }

        /// <summary>
        ///  Un-set(set false) the UnNotifyPropertyChanged flag for a specified element item
        ///  Un-set element item will be affected by NotifyPropertyChanged event
        /// </summary>
        /// <param name="item">A item to un-set LockNotifyProperty</param>
        private void UnLockNotifyPropertyChanged(IDataItem item)
        {
            if (item != null && 
                item.BackupItem == null &&
                this.IsBackupRequired)
            {
                item.MakeBackupItem();
            }
            
            return;
        }
        #endregion

        #region METHOD AREA (OTHER - MAKE KEY)***********************
        /// <summary>
        /// Returns a TKey type value that is converted from key parameter 
        /// </summary>
        /// <typeparam name="TKey">The type of key</typeparam>
        /// <param name="key">The value string to convert</param>
        /// <returns>The converted TKey type value from key parameter</returns>
        private TKey GetKeyValue<TKey>(string key)
        {
            TKey value = default(TKey);
            object obj;

            Type type = typeof(TKey);
            
            if (type.Equals(Type.GetType("System.Int16")))
            {
                obj = Int16.Parse(key);
                value = (TKey)obj;
            }
            if (type.Equals(Type.GetType("System.Int32")))
            {
                obj = Int32.Parse(key);
                value = (TKey)obj;
            } 
            if (type.Equals(Type.GetType("System.String")))
            {
                obj = key;
                value = (TKey)obj;
            }
            return value;
        }
        #endregion
    }

}
