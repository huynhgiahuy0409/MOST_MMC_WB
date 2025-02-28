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
* 2012.12.21    Jindols 1.0	First release.
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

namespace Tsb.Fontos.Core.Data.IBatis.Batch
{
    /// <summary>
    /// IBatis dynamic sql batch Handler class
    /// </summary>
    internal class OracleDynamicBatchHandler : SqlBaseBatchHandler, IDisposable
    {
        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initialize Instance
        /// </summary>
        public OracleDynamicBatchHandler(ISqlMapper mapper, string statementName, object[] parameterDataObjects)
            : base(mapper, statementName, parameterDataObjects)
        {
            this.ObjectID = "GNR-FTCO-DAT-SqlBulkHandler";
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
            try
            {
                ISqlMapSession session = GetSqlMapSession();

                List<BatchDataInfo> batchGroups = CreateBatchGroup(session, this.ParameterDataObjects);

                if (batchGroups == null)
                {
                    return returnValue;
                }

                foreach (BatchDataInfo item in batchGroups)
                {
                    object[] subParamDataObjects = item.ParamArray.ToArray();
                    List<object[]> parameterBatchDataArray = this.ConvertToBatchData(session, item);

                    int dataCount = subParamDataObjects.Length;
                    int paramCount;

                    IDataParameter parameter;


                    IMappedStatement statement = item.Statement;
                    RequestScope request = item.Request;

                    DbCommandDecorator dbCommandDecorator = request.IDbCommand as DbCommandDecorator;

                    try
                    {
                        //Set the ArrayBindCount to indicate the number of values
                        // Oracle.DataAccess.Client.OracleCommand 개체의 ArrayBindCount 속성에 batch 처리를 위한 값을 설정합니다.
                        Tsb.Fontos.Core.Reflection.PropertyUtil.SetPropertyValueByName(dbCommandDecorator.DbCommand, "ArrayBindCount", dataCount);
                        Tsb.Fontos.Core.Reflection.PropertyUtil.SetPropertyValueByName(dbCommandDecorator.DbCommand, "CommandTimeout", this.CommandTimeout);
                    }
                    catch (Exception)
                    {
                        //MSG_FTCO_00201 : SQL Batch Job is not supported by DB provider.[Command Type:{1}]
                        throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00200", DefaultMessage.NON_REG_WRD + request.IDbCommand.GetType().ToString());
                    }

                    paramCount = request.IDbCommand.Parameters.Count;
                    for (int i = 0; i < paramCount; i++)
                    {
                        parameter = request.IDbCommand.Parameters[i] as IDataParameter;
                        //Exception Handler
                        if (parameter.Value == System.DBNull.Value)
                        {
                            parameter.Value = "";
                        }
                        parameter.Value = parameterBatchDataArray[i];
                    }

                    returnValue = returnValue + request.IDbCommand.ExecuteNonQuery();
                }

            }
            catch (TsbSysBaseException sysEx)
            {
                ExceptionHandler.Propagate(sysEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(this.StatementName, ex);
                //MSG_FTCO_00201 : An error occurred during a database batch SQL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00201", null);
            }

            return returnValue;
        }

        /// <summary>
        /// Convert parameter data Objects to batch parameter data Objects.
        /// </summary>
        /// <returns>The batch parameter data Objects.</returns>
        private List<object[]> ConvertToBatchData(ISqlMapSession session, BatchDataInfo item)
        {
            object[] parameterDataObjects = item.ParamArray.ToArray();
            List<object[]> parameterArray = new List<object[]>();
            try
            {

                IBatisNet.DataMapper.MappedStatements.IMappedStatement statement = item.Statement;
                IBatisNet.DataMapper.Scope.RequestScope request;
                int dataCount = item.ParamArray.Count;
                int paramCount = 0;
                IDataParameter parameter;

                if (dataCount > 0)
                {
                    request = item.Request;
                    paramCount = request.IDbCommand.Parameters.Count;
                }

                for (int i = 0; i < paramCount; i++)
                {
                    parameterArray.Add(null);
                }

                for (int dataIdx = 0; dataIdx < dataCount; dataIdx++)
                {
                    request = item.Request;
                    statement.PreparedCommand.Create(request, session, statement.Statement, parameterDataObjects[dataIdx]);
                    paramCount = request.IDbCommand.Parameters.Count;

                    for (int paramIdx = 0; paramIdx < paramCount; paramIdx++)
                    {
                        parameter = request.IDbCommand.Parameters[paramIdx] as IDataParameter;


                        object[] paramArray = parameterArray[paramIdx];
                        if (paramArray == null)
                        {
                            paramArray = new object[dataCount];
                            parameterArray[paramIdx] = paramArray;
                        }

                        paramArray[dataIdx] = parameter.Value;
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysNetException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return parameterArray;
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
