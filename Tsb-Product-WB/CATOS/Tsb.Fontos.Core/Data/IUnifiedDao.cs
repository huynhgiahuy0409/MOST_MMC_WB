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
* 2009.06.22    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;
using System.Collections;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Data
{
    /// <summary>
    /// Represents Data Access Object
    /// </summary>
    public interface IUnifiedDao : ITsbBaseObject
    {
        object InsertItem(string statementName, object parameterObject);
        object Insert(string statementName, object parameterObject);

        int UpdateItem(string statementName, object parameterObject);
        int Update(string statementName, object parameterObject);

        int DeleteItem(string statementName, object parameterObject);
        int Delete(string statementName, object parameterObject);

        IList QueryForList(string statementName, object parameterObject);
        IList<T> QueryForList<T>(string statementName, object parameterObject);

        
        /// <summary>
        /// Executes a mapped SQL SELECT statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns>object of operaiton</returns>
        object QueryForObject(string statementName, object parameterObject);


        /// <summary>
        /// Open Database Connection
        /// </summary>
        void OpenConnection();
        

        /// <summary>
        /// Close Database Connection
        /// </summary>
        void CloseConnection();

    }
}
