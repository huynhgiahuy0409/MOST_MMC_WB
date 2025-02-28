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
* 2013.02.25    Jindols 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Commands;
using System.Data.Common;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using System.Data;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Logging;
using Tsb.Fontos.Core.Constant;
using System.Data.SqlClient;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Data.IBatis.Batch
{
    /// <summary>
    /// IBatis dynamic sql batch Handler class
    /// </summary>
    internal class MSSqlDynamicBatchHandler : MSSqlBaseBatchHandler, IDisposable
    {
        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initialize Instance
        /// </summary>
        public MSSqlDynamicBatchHandler(ISqlMapper mapper, string statementName, object[] parameterDataObjects)
            : base(mapper, statementName, parameterDataObjects)
        {
            this.ObjectID = "GNR-FTCO-DAT-MSSqlDynamicBatchHandler";
        }
        #endregion

        #region METHOD AREA ****************************
        /// <summary>
        /// Executes batch of a mapped SQL INSERT/UPDATE statement
        /// Use the BULK INSERT(UPDATE) sql statement to insert large volumes of records 
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        public override int ExecuteBatch()
        {
            int returnValue = 0;
            ISqlMapSession session = null;
            bool isError = false;
            bool isLocalTransaction = false;
            try
            {
                session = this.GetSqlMapSession();

                if (session.IsTransactionStart == false)
                {
                    session.BeginTransaction();
                    isLocalTransaction = true;
                }

                List<BatchDataInfo> batchGroups = CreateBatchGroup(session, this.ParameterDataObjects);

                if (batchGroups == null)
                {
                    return returnValue;
                }

                foreach (BatchDataInfo item in batchGroups)
                {
                    BatchSqlTypes batchSqlType = this.GetBatchSqlType(item.Statement.Statement);

                    if (batchSqlType == BatchSqlTypes.None)
                    {
                        throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00210", DefaultMessage.NON_REG_WRD + item.Request.IDbCommand.CommandText);
                    }

                    RequestScope request = item.Request;

                    this.SetDbCommandProperty(request);
                    this.SetSourceColumnName(request);

                    object[] subParamDataObjects = item.ParamArray.ToArray();
                    int dataCount = subParamDataObjects.Length;

                    SqlDataAdapter adapter = this.GetSqlDataAdapter(batchSqlType, request.IDbCommand);
                    adapter.UpdateBatchSize = dataCount;
                    
                    DataTable dt = this.ConvertToBatchData(session, item, batchSqlType);
                    adapter.UpdateBatchSize = dataCount;

                    returnValue = returnValue + adapter.Update(dt);
                }

                if (isLocalTransaction == true)
                {
                    session.CommitTransaction();
                }
            }
            catch (TsbSysBaseException sysEx)
            {
                isError = true;
                ExceptionHandler.Propagate(sysEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                isError = true;
                this.SetQueryErrorLog(this.StatementName, ex);
                //MSG_FTCO_00201 : An error occurred during a database batch SQL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00201", null);
            }
            finally
            {
                if (isLocalTransaction == true && isError == true)
                {
                    if (session != null)
                    {
                        try
                        {
                            session.RollBackTransaction();
                        }
                        catch (Exception ex1)
                        {
                            this.SetQueryErrorLog(this.StatementName, ex1);
                        }
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Convert parameter data Objects to batch parameter data Objects.
        /// </summary>
        /// <returns>The batch parameter data Objects.</returns>
        private DataTable ConvertToBatchData(ISqlMapSession session, BatchDataInfo item, BatchSqlTypes batchSqlType)
        {

            DataTable dt = null;
            object[] parameterDataObjects = item.ParamArray.ToArray();
            try
            {
                IBatisNet.DataMapper.MappedStatements.IMappedStatement statement = item.Statement;
                IBatisNet.DataMapper.Scope.RequestScope request;
                int dataCount = item.ParamArray.Count;

                if (dataCount == 0)
                {
                    return new DataTable("BatchData");
                }

                request = item.Request;

                dt = CreateDataTable(request, session, statement, batchSqlType, parameterDataObjects);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                ExceptionHandler.Wrap(ex, typeof(TsbSysNetException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return dt;
        }

        /// <summary>
        /// Create a batch group depending on the query statement. 
        /// </summary>
        /// <param name="parameterDataObjects">The input data</param>
        /// <returns>Return a batch group.</returns>
        private List<BatchDataInfo> CreateBatchGroup(ISqlMapSession session, object[] parameterDataObjects)
        {
            List<BatchDataInfo> parameterArray = new List<BatchDataInfo>();

            try
            {
                BatchGroupHander batchDataHdl = new BatchGroupHander();

                IBatisNet.DataMapper.MappedStatements.IMappedStatement statement = Mapper.GetMappedStatement(this.StatementName);
                IBatisNet.DataMapper.Scope.RequestScope request;
                int dataCount = this.ParameterDataObjects.Length;

                for (int dataIdx = 0; dataIdx < dataCount; dataIdx++)
                {
                    request = statement.Statement.Sql.GetRequestScope(statement, parameterDataObjects[dataIdx], session);
                    statement.PreparedCommand.Create(request, session, statement.Statement, parameterDataObjects[dataIdx]);
                    batchDataHdl.AddParamData(statement, request, parameterDataObjects[dataIdx]);
                }

                parameterArray = batchDataHdl.GetGroupList();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysNetException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return parameterArray;
        }
        #endregion
    }
}
