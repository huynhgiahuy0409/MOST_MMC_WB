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
* 2012.08.31    Jindols 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using IBatisNet.DataMapper;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Data.IBatis.Batch;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Data.IBatis.Batch
{
    internal enum SqlStatementTypes
    {
        STATIC,
        DYNAMIC
    }
    /// <summary>
    /// IBatis Sql Batch Manage class
    /// </summary>
    internal class SqlBatchManager : TsbBaseObject
    {
        #region FIELD AREA ***************************************
        private Dictionary<string, List<object>> _batchJobDic;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// Gets or Sets DBTYPE
        /// </summary>
        private string DBType { get; set; }

        /// <summary>
        /// Gets or Sets DBProduct
        /// </summary>
        private DBProductTypes DBProduct { get; set; }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Initialize Instance
        /// </summary>
        public SqlBatchManager(string dbType, DBProductTypes dbProduct)
            : base()
        {
            this.ObjectID = "GNR-FTCO-DAT-SqlBatchManager";

            _batchJobDic = new Dictionary<string, List<object>>();

            this.DBType = dbType;
            this.DBProduct = dbProduct;
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Adds the given SQL command to the current list of commmands for this Statement object.
        /// The commands in this list can be executed as a batch by calling the method executeBatch.
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object</param>
        /// <returns></returns>
        public void AddBatch(string statementName, object parameterDataObject)
        {
            List<object> parameterDataObjectList = null;

            try
            {
                if (_batchJobDic.ContainsKey(statementName) == false)
                {
                    parameterDataObjectList = new List<object>();
                    _batchJobDic.Add(statementName, parameterDataObjectList);
                }
                else
                {
                    parameterDataObjectList = _batchJobDic[statementName];
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            parameterDataObjectList.Add(parameterDataObject);
        }
        /// <summary>
        /// Executes(flushes) statements batched by the specified batch size.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <param name="mapper">IBatis Mapper object</param>
        /// <batchsize>The maximum  batch size</batchsize>
        /// <sqlStatementTypes>The type of batch sql statement.</sqlStatementTypes>
        /// <commandTimeout> The wait time before terminating the attempt to execute a command</commandTimeout>
        /// <returns>returns the number of rows updated in the batch</returns>
        public int ExecuteBatch(ISqlMapper mapper, int batchsize, SqlStatementTypes sqlStatementTypes, int commandTimeout)
        {
            int count = 0;

            try
            {
                if (_batchJobDic == null)
                {
                    return count;
                }

                foreach (string statementName in _batchJobDic.Keys)
                {
                    List<object> dataList = _batchJobDic[statementName];

                    if (dataList == null)
                    {
                        continue;
                    }

                    object[] parameterObjects = dataList.ToArray();

                    List<List<object>> divList = GetBatchDivList(batchsize, parameterObjects);

                    foreach (List<object> item in divList)
                    {
                        object[] divparameterObjects = item.ToArray();
                        count = count + this.ExecuteBatch(mapper, statementName, divparameterObjects, sqlStatementTypes, commandTimeout);
                    }

                    //count = count + this.ExecuteBatch(mapper, statementName, parameterObjects);
                }

                _batchJobDic.Clear();
            }
            catch (Exception ex)
            {
                //MSG_FTCO_00116 : An error occurred during a database SELECT execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00116", null);
            }

            return count;
        }

        /// <summary>
        /// Gets the batch data list.
        /// </summary>
        /// <param name="batchsize"></param>
        /// <param name="parameterObjects"></param>
        /// <returns></returns>
        private List<List<object>> GetBatchDivList(int batchsize, object[] parameterObjects)
        {
            List<List<object>> divList = new List<List<object>>();

            try
            {
                if (batchsize <= 0)
                {
                    batchsize = 9999999;
                }

                int totalCount = parameterObjects.Length;
                //int divCount = (int)(totalCount / batchsize) + ((totalCount % batchsize > 0) ? 1 : 0);

                int tempCount = 9999999;


                List<object> subDivList = null;
                for (int i = 0; i < totalCount; i++)
                {
                    if (tempCount >= batchsize)
                    {
                        subDivList = new List<object>();

                        divList.Add(subDivList);
                        tempCount = 0;
                    }

                    subDivList.Add(parameterObjects[i]);
                    tempCount++;
                }


                Console.WriteLine("divList.Count" + divList.Count);

                foreach (List<object> item in divList)
                {
                    object[] divparameterObjects = item.ToArray();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return divList;
        }


        /// <summary>
        /// Executes batch of a mapped SQL INSERT/UPDATE statement
        /// Use the BULK INSERT(UPDATE) sql statement to insert large volumes of records 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object</param>
        /// <sqlStatementTypes>The type of batch sql statement.</sqlStatementTypes>
        /// <commandTimeout> The wait time before terminating the attempt to execute a command</commandTimeout>
        private int ExecuteBatch(ISqlMapper mapper, string statementName, object[] parameterObject, SqlStatementTypes sqlStatementTypes, int commandTimeout)
        {
            int count = 0;
            SqlBaseBatchHandler sqlBulkHandler = null;
            try
            {
                //sqlBulkHandler = new SqlBatchHandler(mapper, statementName, parameterObject);
                sqlBulkHandler = this.GetBatchHandler(mapper, statementName, parameterObject, sqlStatementTypes);
                sqlBulkHandler.CommandTimeout = commandTimeout;
                count = sqlBulkHandler.ExecuteBatch();
            }
            catch (Exception ex)
            {
                //MSG_FTCO_00116 : An error occurred during a database SELECT execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00201", null);
            }
            finally
            {
                if (sqlBulkHandler != null)
                {
                    sqlBulkHandler.Dispose();
                }
            }

            return count;
        }

        private SqlBaseBatchHandler GetBatchHandler(ISqlMapper mapper, string statementName, object[] parameterObject, SqlStatementTypes sqlStatementTypes)
        {
            SqlBaseBatchHandler sqlBulkHandler = null;

            try
            {
                if (this.DBProduct == DBProductTypes.ORACLE)
                {
                    switch (sqlStatementTypes)
                    {
                        case SqlStatementTypes.STATIC:
                            {
                                sqlBulkHandler = new OracleStaticBatchHandler(mapper, statementName, parameterObject);
                                break;
                            }

                        case SqlStatementTypes.DYNAMIC:
                            {
                                sqlBulkHandler = new OracleDynamicBatchHandler(mapper, statementName, parameterObject);
                                break;
                            }

                        default:
                            {
                                throw new NotImplementedException("Does not support this batch type.{" + sqlStatementTypes.ToString() + "}");
                            }
                    }
                }
                else if (this.DBProduct == DBProductTypes.MSSQL)
                {
                    switch (sqlStatementTypes)
                    {
                        case SqlStatementTypes.STATIC:
                            {
                                sqlBulkHandler = new MSSqlStaticBatchHandler(mapper, statementName, parameterObject);
                                break;
                            }

                        case SqlStatementTypes.DYNAMIC:
                            {
                                sqlBulkHandler = new MSSqlDynamicBatchHandler(mapper, statementName, parameterObject);
                                break;
                            }

                        default:
                            {
                                throw new NotImplementedException("Does not support this batch type.{" + sqlStatementTypes.ToString() + "}");
                            }
                    }
                }
                
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return sqlBulkHandler;
        }
        /// <summary>
        /// Executes batch of a mapped dynamic SQL INSERT/UPDATE statement
        /// Use the BULK INSERT(UPDATE) sql statement to insert large volumes of records 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object</param>
        private int ExecuteDynamicBatch(ISqlMapper mapper, string statementName, object[] parameterObject)
        {
            int count = 0;
            OracleDynamicBatchHandler sqlBulkHandler = null;
            try
            {
                sqlBulkHandler = new OracleDynamicBatchHandler(mapper, statementName, parameterObject);
                count = sqlBulkHandler.ExecuteBatch();
            }
            catch (Exception ex)
            {
                //MSG_FTCO_00116 : An error occurred during a database SELECT execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00201", null);
            }
            finally
            {
                if (sqlBulkHandler != null)
                {
                    sqlBulkHandler.Dispose();
                }
            }

            return count;
        }
        #endregion


    }
}
