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

namespace Tsb.Fontos.Core.Data.IBatis.Batch
{
    /// <summary>
    /// IBatis Sql Batch Handler class
    /// </summary>
    internal class MSSqlStaticBatchHandler : MSSqlBaseBatchHandler, IDisposable
    {
        #region FIELD AREA*****************************
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initialize Instance
        /// </summary>
        public MSSqlStaticBatchHandler(ISqlMapper mapper, string statementName, object[] parameterDataObjects)
            : base(mapper, statementName, parameterDataObjects)
        {
            this.ObjectID = "GNR-FTCO-DAT-MSSqlStaticBatchHandler";
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
                session = GetSqlMapSession();

                if (session.IsTransactionStart == false)
                {
                    session.BeginTransaction();
                    isLocalTransaction = true;
                }

                int dataCount = this.ParameterDataObjects.Length;

                IMappedStatement statement = Mapper.GetMappedStatement(this.StatementName);
                RequestScope request = statement.Statement.Sql.GetRequestScope(statement, this.ParameterDataObjects[0], session);
                statement.PreparedCommand.Create(request, session, statement.Statement, this.ParameterDataObjects[0]);

                BatchSqlTypes batchSqlType = this.GetBatchSqlType(statement.Statement);

                if (batchSqlType == BatchSqlTypes.None)
                {
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00210", DefaultMessage.NON_REG_WRD + request.IDbCommand.CommandText);
                }

                this.SetDbCommandProperty(request);
                this.SetSourceColumnName(request);

                SqlDataAdapter adapter = this.GetSqlDataAdapter(batchSqlType, request.IDbCommand);
                adapter.UpdateBatchSize = dataCount;


                DataTable dt = this.ConvertToBatchData(this.ParameterDataObjects, batchSqlType);
                adapter.UpdateBatchSize = dataCount;

                returnValue = adapter.Update(dt);

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

        //protected void OnRowUpdating(object sender, SqlRowUpdatingEventArgs args)
        //{
        //    if (args.Status != UpdateStatus.Continue)
        //    {
        //    }

        //    if (args.Status == UpdateStatus.ErrorsOccurred)
        //    {
        //        args.Row.RowError = args.Errors.Message;
        //        args.Status = UpdateStatus.ErrorsOccurred;
        //    }
        //}

        //protected void OnRowUpdated(object sender, SqlRowUpdatedEventArgs args)
        //{
        //    if (args.Status == UpdateStatus.ErrorsOccurred)
        //    {
        //        args.Row.RowError = args.Errors.Message;
        //        args.Status = UpdateStatus.ErrorsOccurred;
        //    }
        //}

        //protected void FillError(object sender, FillErrorEventArgs args)
        //{
        //    if (args.Errors.GetType() == typeof(System.OverflowException))
        //    {
        //        // Code to handle precision loss.
        //        //Add a row to table using the values from the first two columns.
        //        //DataRow myRow = args.DataTable.Rows.Add(new object[] { args.Values[0], args.Values[1], DBNull.Value });
        //        //Set the RowError containing the value for the third column.
        //        //args.RowError =
        //        //   "OverflowException Encountered. Value from data source: " +
        //        //   args.Values[2];
        //        args.Continue = false;
        //    }
        //}
        #endregion

        #region METHOD(CONVERT) AREA ******************
        /// <summary>
        /// Convert parameter data Objects to batch parameter data Objects.
        /// </summary>
        /// <returns>The batch parameter data Objects.</returns>
        protected DataTable ConvertToBatchData(object[] parameterDataObjects, BatchSqlTypes batchSqlType)
        {
            DataTable dt = null;

            try
            {
                ISqlMapSession session = GetSqlMapSession();

                IBatisNet.DataMapper.MappedStatements.IMappedStatement statement = Mapper.GetMappedStatement(this.StatementName);
                IBatisNet.DataMapper.Scope.RequestScope request = null;
                int dataCount = this.ParameterDataObjects.Length;
                //int paramCount = 0;
                //IDataParameter parameter;

                if (dataCount == 0)
                {
                    return new DataTable("BatchData");
                }

                request = statement.Statement.Sql.GetRequestScope(statement, parameterDataObjects[0], session);
                statement.PreparedCommand.Create(request, session, statement.Statement, parameterDataObjects[0]);
                //paramCount = request.IDbCommand.Parameters.Count;

                dt = CreateDataTable(request, session, statement, batchSqlType, parameterDataObjects);

                ////Add column data.
                //for (int i = 0; i < paramCount; i++)
                //{
                //    parameter = request.IDbCommand.Parameters[i] as IDataParameter;
                //    parameter.SourceColumn = parameter.ParameterName;
                //    dt.Columns.Add(parameter.ParameterName);
                //}

                ////Add row data.
                //for (int dataIdx = 0; dataIdx < dataCount; dataIdx++)
                //{
                //    request = statement.Statement.Sql.GetRequestScope(statement, parameterDataObjects[dataIdx], session);
                //    statement.PreparedCommand.Create(request, session, statement.Statement, parameterDataObjects[dataIdx]);
                //    paramCount = request.IDbCommand.Parameters.Count;

                //    DataRow newCust = dt.NewRow();

                //    //Sets the row value.
                //    for (int paramIdx = 0; paramIdx < paramCount; paramIdx++)
                //    {
                //        parameter = request.IDbCommand.Parameters[paramIdx] as IDataParameter;
                //        newCust[parameter.ParameterName] = parameter.Value;
                //    }

                //    dt.Rows.Add(newCust);

                //    // Sets the state of row data.
                //    if (batchSqlType == BatchSqlTypes.Update)
                //    {
                //        newCust.AcceptChanges();
                //        newCust.SetModified();
                //    }
                //    else if (batchSqlType == BatchSqlTypes.Delete)
                //    {
                //        newCust.AcceptChanges();
                //        newCust.Delete();
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysNetException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return dt;
        }

        ///// <summary>
        ///// Convert parameter data Objects to batch parameter data Objects.
        ///// </summary>
        ///// <returns>The batch parameter data Objects.</returns>
        //protected DataTable ConvertToBatchData(object[] parameterDataObjects, BatchSqlTypes batchSqlType)
        //{
        //    DataTable dt = new DataTable("BatchData");

        //    try
        //    {
        //        ISqlMapSession session = GetSqlMapSession();

        //        IBatisNet.DataMapper.MappedStatements.IMappedStatement statement = Mapper.GetMappedStatement(this.StatementName);
        //        IBatisNet.DataMapper.Scope.RequestScope request = null;
        //        int dataCount = this.ParameterDataObjects.Length;
        //        int paramCount = 0;
        //        IDataParameter parameter;

        //        if (dataCount == 0)
        //        {
        //            return dt;
        //        }

        //        request = statement.Statement.Sql.GetRequestScope(statement, parameterDataObjects[0], session);
        //        statement.PreparedCommand.Create(request, session, statement.Statement, parameterDataObjects[0]);
        //        paramCount = request.IDbCommand.Parameters.Count;

        //        //Add column data.
        //        for (int i = 0; i < paramCount; i++)
        //        {
        //            parameter = request.IDbCommand.Parameters[i] as IDataParameter;
        //            parameter.SourceColumn = parameter.ParameterName;
        //            dt.Columns.Add(parameter.ParameterName);
        //        }

        //        //Add row data.
        //        for (int dataIdx = 0; dataIdx < dataCount; dataIdx++)
        //        {
        //            request = statement.Statement.Sql.GetRequestScope(statement, parameterDataObjects[dataIdx], session);
        //            statement.PreparedCommand.Create(request, session, statement.Statement, parameterDataObjects[dataIdx]);
        //            paramCount = request.IDbCommand.Parameters.Count;

        //            DataRow newCust = dt.NewRow();

        //            //Sets the row value.
        //            for (int paramIdx = 0; paramIdx < paramCount; paramIdx++)
        //            {
        //                parameter = request.IDbCommand.Parameters[paramIdx] as IDataParameter;
        //                newCust[parameter.ParameterName] = parameter.Value;
        //            }

        //            dt.Rows.Add(newCust);

        //            // Sets the state of row data.
        //            if (batchSqlType == BatchSqlTypes.Update)
        //            {
        //                newCust.AcceptChanges();
        //                newCust.SetModified();
        //            }
        //            else if (batchSqlType == BatchSqlTypes.Delete)
        //            {
        //                newCust.AcceptChanges();
        //                newCust.Delete();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler.Wrap(ex, typeof(TsbSysNetException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
        //    }

        //    return dt;
        //}
        #endregion
    }
}
