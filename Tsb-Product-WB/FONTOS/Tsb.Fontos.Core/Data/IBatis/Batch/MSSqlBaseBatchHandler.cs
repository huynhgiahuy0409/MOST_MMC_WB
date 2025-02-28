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
using IBatisNet.DataMapper;
using System.Data;
using System.Data.SqlClient;
using Tsb.Fontos.Core.Logging;
using IBatisNet.DataMapper.Configuration.Statements;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Message;
using IBatisNet.DataMapper.Scope;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Commands;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Data.IBatis.Batch
{
    internal enum BatchSqlTypes
    {
        None,
        Insert,
        Update,
        Delete,
        StoredProcedure
    }

    internal abstract class MSSqlBaseBatchHandler : SqlBaseBatchHandler
    {
        #region FIELD AREA*****************************
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initialize Instance
        /// </summary>
        public MSSqlBaseBatchHandler(ISqlMapper mapper, string statementName, object[] parameterDataObjects)
            : base(mapper, statementName, parameterDataObjects)
        {
            this.ObjectID = "GNR-FTCO-DAT-MSSqlBaseBatchHandler";
        }
        #endregion

        #region METHOD AREA ****************************
        /// <summary>
        /// Sets the name of the source column that is mapped to the System.Data.DataSet 
        /// and used for loading or returning the System.Data.IDataParameter.Value.
        /// </summary>
        /// <param name="request"></param>
        protected void SetSourceColumnName(RequestScope request)
        {
            try
            {
                IDataParameter parameter;

                int paramCount = request.IDbCommand.Parameters.Count;
                for (int i = 0; i < paramCount; i++)
                {
                    parameter = request.IDbCommand.Parameters[i] as IDataParameter;
                    parameter.SourceColumn = parameter.ParameterName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Sets the batch default value to a specified named property.
        /// </summary>
        /// <param name="request"></param>
        protected void SetDbCommandProperty(RequestScope request)
        {
            try
            {
                DbCommandDecorator dbCommandDecorator = request.IDbCommand as DbCommandDecorator;
                Tsb.Fontos.Core.Reflection.PropertyUtil.SetPropertyValueByName(dbCommandDecorator.DbCommand, "CommandTimeout", this.CommandTimeout);
            }
            catch (Exception)
            {
                //MSG_FTCO_00201 : SQL Batch Job is not supported by DB provider.[Command Type:{1}]
                throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00200", DefaultMessage.NON_REG_WRD + request.IDbCommand.GetType().ToString());
            }
        }
        /// <summary>
        /// Gets the batch type.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        protected BatchSqlTypes GetBatchSqlType(IStatement statement)
        {
            BatchSqlTypes batchSqlType = BatchSqlTypes.None;

            try
            {
                if (statement is IBatisNet.DataMapper.Configuration.Statements.Insert)
                {
                    batchSqlType = BatchSqlTypes.Insert;
                }
                else if (statement is IBatisNet.DataMapper.Configuration.Statements.Update)
                {
                    batchSqlType = BatchSqlTypes.Update;
                }
                else if (statement is IBatisNet.DataMapper.Configuration.Statements.Delete)
                {
                    batchSqlType = BatchSqlTypes.Delete;
                }
                else if (statement is IBatisNet.DataMapper.Configuration.Statements.Procedure)
                {
                    batchSqlType = BatchSqlTypes.StoredProcedure;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                batchSqlType = BatchSqlTypes.None;
            }

            return batchSqlType;
        }
        /// <summary>
        /// Gets the SqlDataAdapter object.
        /// </summary>
        /// <param name="batchSqlType"></param>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        protected SqlDataAdapter GetSqlDataAdapter(BatchSqlTypes batchSqlType, IDbCommand dbCommand)
        {
            SqlDataAdapter adapter = null;

            try
            {
                adapter = new SqlDataAdapter();

                if (batchSqlType == BatchSqlTypes.Insert)
                {
                    adapter.InsertCommand = ((dbCommand as IBatisNet.DataMapper.Commands.DbCommandDecorator).DbCommand as System.Data.SqlClient.SqlCommand);
                    adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                }
                else if (batchSqlType == BatchSqlTypes.Update)
                {
                    adapter.UpdateCommand = ((dbCommand as IBatisNet.DataMapper.Commands.DbCommandDecorator).DbCommand as System.Data.SqlClient.SqlCommand);
                    adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                }
                else if (batchSqlType == BatchSqlTypes.Delete)
                {
                    adapter.DeleteCommand = ((dbCommand as IBatisNet.DataMapper.Commands.DbCommandDecorator).DbCommand as System.Data.SqlClient.SqlCommand);
                    adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
                }
                else if (batchSqlType == BatchSqlTypes.StoredProcedure)
                {
                    adapter.InsertCommand = ((dbCommand as IBatisNet.DataMapper.Commands.DbCommandDecorator).DbCommand as System.Data.SqlClient.SqlCommand);
                    adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return adapter;
        }
        /// <summary>
        /// Create data table.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="session"></param>
        /// <param name="statement"></param>
        /// <param name="batchSqlType"></param>
        /// <param name="parameterDataObjects"></param>
        /// <returns></returns>
        protected DataTable CreateDataTable(RequestScope request, ISqlMapSession session, IMappedStatement statement, BatchSqlTypes batchSqlType, object[] parameterDataObjects)
        {
            DataTable dt = new DataTable("BatchData");

            try
            {
                IDataParameter parameter;
                int paramCount = request.IDbCommand.Parameters.Count;
                int dataCount = parameterDataObjects.Length;

                //Add column data.
                for (int i = 0; i < paramCount; i++)
                {
                    parameter = request.IDbCommand.Parameters[i] as IDataParameter;
                    parameter.SourceColumn = parameter.ParameterName;
                    dt.Columns.Add(parameter.ParameterName);
                }

                //Add row data.
                for (int dataIdx = 0; dataIdx < dataCount; dataIdx++)
                {
                    request = statement.Statement.Sql.GetRequestScope(statement, parameterDataObjects[dataIdx], session);
                    statement.PreparedCommand.Create(request, session, statement.Statement, parameterDataObjects[dataIdx]);
                    paramCount = request.IDbCommand.Parameters.Count;

                    DataRow newCust = dt.NewRow();

                    //Sets the row value.
                    for (int paramIdx = 0; paramIdx < paramCount; paramIdx++)
                    {
                        parameter = request.IDbCommand.Parameters[paramIdx] as IDataParameter;
                        newCust[parameter.ParameterName] = parameter.Value;
                    }

                    dt.Rows.Add(newCust);

                    // Sets the state of row data.
                    if (batchSqlType == BatchSqlTypes.Update)
                    {
                        newCust.AcceptChanges();
                        newCust.SetModified();
                    }
                    else if (batchSqlType == BatchSqlTypes.Delete)
                    {
                        newCust.AcceptChanges();
                        newCust.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysNetException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return dt;
        }
        #endregion

    }
}
