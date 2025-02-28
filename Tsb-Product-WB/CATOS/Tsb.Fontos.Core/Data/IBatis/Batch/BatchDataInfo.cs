#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2012 TOTAL SOFT BANK LIMITED. All Rights
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
* 2013.02.26    Jindols 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;

namespace Tsb.Fontos.Core.Data.IBatis.Batch
{
    internal class BatchDataInfo
    {
        #region FIELD AREA*****************************
        #endregion

        #region PROPERTY AREA ************************************
        public string StatementSubKey { get; private set; }
        public IMappedStatement Statement { get; private set; }
        public RequestScope Request { get; private set; }
        public List<object> ParamArray { get; private set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        public BatchDataInfo(string statementSubKey, IMappedStatement statement, RequestScope request)
        {
            this.StatementSubKey = statementSubKey;
            this.ParamArray = new List<object>();
            this.Statement = statement;
            this.Request = request;
        }
        #endregion

        #region METHOD AREA ****************************
        public void AddData(object paramData)
        {
            this.ParamArray.Add(paramData);
        }
        #endregion



    }
}
