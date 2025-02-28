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
* 2013.02.15    Jindols 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using IBatisNet.DataMapper;
using System.Collections.Specialized;
using Tsb.Fontos.Core.Logging;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Data.IBatis
{
    
    internal class StatementNameHandler : TsbBaseObject
    {
        #region FIELD AREA ***************************************
        private Dictionary<string, string> _cachedPrefix;
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public StatementNameHandler()
        {
            this.ObjectID = "GNR-FTCO-DAT-StatementNameHandler";

            _cachedPrefix = new Dictionary<string, string>();
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Gets the statement name.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dbType"></param>
        /// <param name="statementName"></param>
        /// <returns></returns>
        public string GetStatementName(ISqlMapper mapper, string dbType, string statementName)
        {
            string newStatementName = statementName;

            try
            {
                string stmtNamePrefix = this.GetStatementNamePrefix(dbType);

                //1.if Statement Name Prefix is null, return 
                if (string.IsNullOrEmpty(stmtNamePrefix) == true)
                {
                    return newStatementName;
                }

                //2. Create the statement name.
                newStatementName = this.CreateStatementName(statementName, stmtNamePrefix);

                //3. If the new statement name does not exist, it is set to the requested statement Name.
                HybridDictionary stmtInfo = mapper.MappedStatements;
                if (stmtInfo.Contains(newStatementName) == false)
                {
                    newStatementName = statementName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newStatementName;
        }
        /// <summary>
        /// Gets the value of statement name prefix.
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private string GetStatementNamePrefix(string dbType)
        {
            string stmeNamePrefix = "";

            if (_cachedPrefix.ContainsKey(dbType) == true)
            {
                return _cachedPrefix[dbType];
            }

            PersistenceInfo.DatabaseInfo databaseInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(dbType);
            if (databaseInfo != null)
            {
                stmeNamePrefix = databaseInfo.StatementNamePrefix;
                _cachedPrefix.Add(dbType, stmeNamePrefix);
            }

            return stmeNamePrefix;

        }

        /// <summary>
        /// Create statement name.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="stmeNamePrefix"></param>
        /// <returns></returns>
        private string CreateStatementName(string statementName, string stmeNamePrefix)
        {
            string newStatementName = string.Empty;

            try
            {

                int idx = statementName.LastIndexOf(".");

                string stmtPath = string.Empty;
                string stmtName = string.Empty;

                if (idx > 0)
                {
                    stmtPath = statementName.Substring(0, idx + 1);
                    stmtName = statementName.Substring(idx + 1, statementName.Length - (idx + 1));
                }

                newStatementName = stmtPath + stmeNamePrefix + stmtName;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }

            return newStatementName;
        }
        #endregion
   }
}
