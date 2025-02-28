#region Class Definitions
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
* 2009.10.05    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Tsb.Fontos.Core.Constant;
using System.Collections;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Codes.Type;

namespace Tsb.Fontos.Core.Codes
{
    /// <summary>
    /// Code Data Manager Class
    /// </summary>
    public class CodeManager : TsbBaseObject
    {
        #region FIELD AREA ******************************************
        private static object syncRoot = new Object();
        private static BaseCodeHandler _codeHandler = null;
        #endregion

        #region PROPERTY AREA ***************************************
        #endregion

        #region INITIALIZATION AREA *********************************
        /// <summary>
        /// Initilize Singleton instance
        /// </summary>
        private CodeManager()
            : base()
        {
            this.ObjectID = "GNR-FTCO-COD-CodeManager";
        }

        /// <summary>
        /// Initilize Code Manager object
        /// </summary>
        /// <param name="codeService">Code handling service instance</param>
        /// <returns>Code Manager object reference</returns>
        public static void InitilizeInstance(BaseCodeHandler codeHandler)
        {
            if (CodeManager._codeHandler == null)
            {
                lock (syncRoot)
                {
                    CodeManager._codeHandler = codeHandler;
                }
            }
        }
        #endregion

        #region METHOD AREA(Gets Codes) *****************************
        /// <summary>
        /// Returns Code Data Items List using a specified code type string
        /// </summary>
        /// <param name="codeType">Code type string</param>
        /// <returns>Code Data Items List</returns>
        public static IList<T> GetCodes<T>(string codeType, params string[] args)
        {
            return _codeHandler.GetCodes<T>(codeType, args);
        }

        /// <summary>
        /// Returns Code Data Items List using a specified code type string
        /// </summary>
        /// <param name="codeType">Code type string</param>
        /// <param name="dbType">Database type string</param>
        /// <returns>Code Data Items List</returns>
        public static IList<T> GetCodesFromDBType<T>(string codeType, string dbType, params string[] args)
        {
            return _codeHandler.GetCodes<T>(codeType, dbType, args);
        }

        /// <summary>
        /// Returns Description using a specified code type, key string
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetDescriptionByCode(string codeType, string key, params string[] args)
        {
            return _codeHandler.GetDescriptionByCode(codeType, key, args);
        }

        /// <summary>
        /// Returns Description using a specified code type, key string
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="key"></param>
        /// <param name="dbType">Database type string</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetDescriptionByCodeFromDBType(string codeType, string key, string dbType, params string[] args)
        {
            return _codeHandler.GetDescriptionByCode(codeType, key, dbType, args);
        }

        /// <summary>
        /// Gets codes unused.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="codeType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IList<T> GetCodesIncludeUnused<T>(string codeType, params string[] args)
        {
            return _codeHandler.GetCodesIncludeUnused<T>(codeType, args);
        }
        #endregion

        #region METHOD AREA(Cached Code) ****************************
        /// <summary>
        /// Whether Code Data Items List exists a specified code type string
        /// </summary>
        /// <param name="codeType">Code type string</param>
        public static bool IsCacheCodes(string key)
        {
            return _codeHandler.IsCacheCodes(CodeGroupTypes.BUSINESS_CODE, key);
        }

        /// <summary>
        /// Whether Code Data Items List exists a specified code type string
        /// </summary>
        /// <param name="codeGroupTypes">Code group type</param>
        /// <param name="codeType">Code type string</param>
        public static bool IsCacheCodes(CodeGroupTypes codeGroupType, string key)
        {
            return _codeHandler.IsCacheCodes(codeGroupType, key);
        }

        /// <summary>
        /// Remove Cache Code Data Items List using a specified code type string
        /// </summary>
        /// <param name="codeType">Code type string</param>
        /// <returns>true if cache is removed</returns>
        public static bool RemoveCacheCodes(string key)
        {
            return _codeHandler.RemoveCacheCodes(CodeGroupTypes.BUSINESS_CODE, key);
        }

        /// <summary>
        /// Remove Cache Code Data Items List using a specified code type string
        /// </summary>
        /// <param name="codeGroupTypes">Code group type</param>
        /// <param name="codeType">Code type string</param>
        public static void RemoveCacheCodes(CodeGroupTypes codeGroupType, string key)
        {
            _codeHandler.RemoveCacheCodes(codeGroupType, key);
        }

        /// <summary>
        /// Remmove Code Data Items List using a specified code type string.
        /// </summary>
        /// </summary>
        /// <param name="value">value string</param>
        /// <returns>true if cache is removed</returns>
        public static bool RemoveCacheStartsWith(string value)
        {
            return _codeHandler.RemoveCacheCodesStartsWith(CodeGroupTypes.BUSINESS_CODE, value);
        }

        /// <summary>
        /// Remmove Code Data Items List using a specified code type string.
        /// </summary>
        /// </summary>
        /// <param name="codeGroupTypes">Code group type</param>
        /// <param name="value">value string</param>
        public static void RemoveCacheStartsWith(CodeGroupTypes codeGroupType, string value)
        {
            _codeHandler.RemoveCacheCodesStartsWith(codeGroupType, value);
        }

        /// <summary>
        /// Remove all cashing datas
        /// </summary>
        public static void RemoveCacheAll()
        {
            _codeHandler.RemoveCacheAll();
        }
        #endregion
    }
}
