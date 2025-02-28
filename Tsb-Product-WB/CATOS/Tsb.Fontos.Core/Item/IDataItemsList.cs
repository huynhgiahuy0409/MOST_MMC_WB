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
* 2009.08.15    CHOI     	1.0 First Release
* 2010.10.27    CHOI        Delete Unused method definition / Add Comments
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;


namespace Tsb.Fontos.Core.Item
{
    /// <summary>
    /// Represents TSB Data Item List of T 
    /// </summary>
    public interface IDataItemsList<T> : IList<T>, IDataItemsList
    {
        /// <summary>
        /// Returns a instance of the BindingList(Of T) list object reference that contains elements copied from the original item list (List(Of T) type, 
        /// </summary>
        /// <returns>Item list object reference as BindingList(Of T) type</returns>
        BindingList<T> GetBindingList();
    }

    /// <summary>
    /// Represents TSB Data Item List
    /// </summary>
    public interface IDataItemsList : IList
    {
        /// <summary>
        /// Gets an Integer containing the number of elements in this item list.
        /// </summary>
        int DataCount { get; }

        /// <summary>
        /// Returns the element item at a specified index in a sequence.
        /// </summary>
        /// <param name="index">The zero-based index of the element item to retrieve.</param>
        /// <returns>The element item at the specified position in the item list.</returns>
        object GetItem(int index);

        /// <summary>
        /// Removes the first occurrence of a specific item object from item list.
        /// </summary>
        /// <param name="item">A item to remove from item list</param>
        /// <returns>
        /// true if item is successfully removed; otherwise, false. 
        /// This method also returns false if item was not found in the List of T
        /// </returns>
        bool RemoveItem(object item);
    }
}
