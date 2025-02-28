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
* 2009.07.22    CHOI 1.0	First release.
* 
*/
#endregion


namespace Tsb.Fontos.Core.Context
{
    /// <summary>
    /// Represents Object Builder(Creator)
    /// </summary>
    public interface IObjectBuilder
    {
        /// <summary>
        /// Creates a object indicated by the specified object ID.
        /// </summary>
        /// <param name="objectID">Object ID to create</param>
        /// <returns>A created object reference</returns>
        object GetObject(string objectID);


        /// <summary>
        /// Returns created object reference 
        /// </summary>
        /// <param name="name">object id to create</param>
        /// <param name="arguments">Arguments to create specivied name object</param>
        /// <returns>Created object reference</returns>
        object GetObject(string name, object[] arguments);

        /// <summary>
        /// Checks whether this object factory contain an object with the given name.
        /// </summary>
        /// <param name="name">The name of the object to query.</param>
        /// <returns>True if an object with the given name is defined.</returns>
        bool ContainsObject(string name);
    }
}
