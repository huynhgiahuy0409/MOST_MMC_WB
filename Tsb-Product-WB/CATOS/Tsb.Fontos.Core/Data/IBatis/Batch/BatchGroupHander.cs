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
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Data.IBatis.Batch
{
    internal class BatchGroupHander
    {
        #region FIELD AREA*****************************
        private Dictionary<string, BatchDataInfo> _dic;
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region INITIALIZATION AREA ****************************
        public BatchGroupHander()
        {
            _dic = new Dictionary<string, BatchDataInfo>();
        }
        #endregion

        #region METHOD AREA ****************************
        /// <summary>
        /// Add the parameter data of a query.
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        /// <param name="paramArray"></param>
        public void AddParamData(IMappedStatement statement, RequestScope request, object paramData)
        {
            string statementSubKey = request.IDbCommand.CommandText;
            BatchDataInfo batchData = null;

            try
            {
                if (_dic.ContainsKey(statementSubKey) == false)
                {
                    batchData = new BatchDataInfo(statementSubKey, statement, request);

                    _dic.Add(statementSubKey, batchData);
                }
                else
                {
                    batchData = _dic[statementSubKey];
                }

                batchData.AddData(paramData);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

        }
        /// <summary>
        /// Gets the batch group list.
        /// </summary>
        /// <returns></returns>
        public List<BatchDataInfo> GetGroupList()
        {
            return _dic.Values.ToList();
        }
        #endregion
    }
}
