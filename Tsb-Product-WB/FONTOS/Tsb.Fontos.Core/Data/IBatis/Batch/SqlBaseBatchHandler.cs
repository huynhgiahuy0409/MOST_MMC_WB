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
* 2012.12.24    Jindols 1.0	First release.
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
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Data.IBatis.Batch
{
    /// <summary>
    /// IBatis Sql Batch Handler class
    /// </summary>
    internal abstract class SqlBaseBatchHandler : TsbBaseObject, IDisposable
    {
        #region FIELD AREA*****************************
        protected readonly ITsbLog tsbLog = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// Gets or Sets IBatis Mapper object reference
        /// </summary>
        protected ISqlMapper Mapper { get; set; }
        /// <summary>
        /// Gets or Sets The name of the statement to execute
        /// </summary>
        protected string StatementName { get; set; }
        /// <summary>
        /// Gets or Sets The parameter objects
        /// </summary>
        protected object[] ParameterDataObjects { get; set; }
        /// <summary>
        /// Gets or Sets whether the new opens the connection.
        /// </summary>
        protected bool IsOpenConnection { get; set; }
        /// <summary>
        /// Gets or sets the wait time before terminating the attempt to execute a command and generating an error.
        ///  The time (in seconds) to wait for the command to execute. The default value is 60 seconds.
        /// </summary>
        public int CommandTimeout { get; set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initialize Instance
        /// </summary>
        public SqlBaseBatchHandler(ISqlMapper mapper, string statementName, object[] parameterDataObjects)
            : base()
        {
            this.ObjectID = "GNR-FTCO-DAT-SqlBaseBatchHandler";

            this.Mapper = mapper;
            this.StatementName = statementName;
            this.ParameterDataObjects = parameterDataObjects;

            this.IsOpenConnection = false;
            this.CommandTimeout = BatchConstant.BATCH_COMMAND_TIMEOUT;
        }
        #endregion

        #region METHOD AREA ****************************
        /// <summary>
        /// Executes batch of a mapped SQL INSERT/UPDATE statement
        /// Use the BULK INSERT(UPDATE) sql statement to insert large volumes of records 
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        public abstract int ExecuteBatch();

        /// <summary>
        /// Gets the DalSession instance currently being used by the SqlMap.
        /// </summary>
        /// <returns></returns>
        protected ISqlMapSession GetSqlMapSession()
        {
            try
            {
                if (this.Mapper.LocalSession == null)
                {
                    if (!Mapper.IsSessionStarted) Mapper.OpenConnection();
                    this.IsOpenConnection = true;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            ISqlMapSession session = this.Mapper.LocalSession;

            return session;
        }
        #endregion

        #region METHOD(SetQueryErrorLog) AREA ******************
        /// <summary>
        /// Sets Query Error Log
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="errorMessage"></param>
        protected void SetQueryErrorLog(string statementName, Exception ex)
        {
            //MSG_FTCO_00170 : Statement ID : <{0}>, Message : '{1}'
            string errorMessage = ex.Message;
            string logMessage =
                MessageBuilder.BuildMessage("MSG_FTCO_00170", DefaultMessage.NON_REG_WRD + statementName, DefaultMessage.NON_REG_WRD + errorMessage);
            tsbLog.Error(logMessage);

            if (ex.InnerException != null)
            {
                tsbLog.Error(ex.InnerException);
            }
        }
        #endregion

        #region Dispose METHOD *************************************
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (this.IsOpenConnection == true)
                {
                    Mapper.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion
    }

}
