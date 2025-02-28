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
    /// Code Data Hanlder Class
    /// </summary>
    public abstract class BaseCodeHandler : TsbBaseObject
    {
        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initilize instance
        /// </summary>
        public BaseCodeHandler()
            : base()
        {
            this.ObjectID = "GNR-FTCO-COD-BaseCodeHandler";
        }
        #endregion

        /// <summary>
        /// Returns Code Data Items List using a specified code type string
        /// </summary>
        /// <param name="codeType">Code type string</param>
        /// <returns>Code Data Items List</returns>
        public abstract IList<T> GetCodes<T>(string codeType, params string[] args);

        /// <summary>
        /// Returns Code Data Items List using a specified code type string
        /// </summary>
        /// <param name="codeType">Code type string</param>
        /// <param name="dbType">Database type string</param>
        /// <returns>Code Data Items List</returns>
        public abstract IList<T> GetCodes<T>(string codeType, string dbType, params string[] args);

        /// <summary>
        /// Whether Code Data Items List exists a specified code type string
        /// </summary>
        /// <param name="codeGroupType">Code group type</param>
        /// <param name="codeType">Code type string</param>
        public abstract bool IsCacheCodes(CodeGroupTypes codeGroupType, string key);

        /// <summary>
        /// Remmove Code Data Items List using a specified code type string
        /// </summary>
        /// <param name="codeGroupType">Code group type</param>
        /// <param name="codeType">Code type string</param>
        public abstract bool RemoveCacheCodes(CodeGroupTypes codeGroupType, string key);

        /// <summary>
        /// Remmove Code Data Items List using a specified code type string
        /// </summary>
        /// <param name="codeGroupType">Code group type</param>
        /// <param name="value">value</param>
        public abstract bool RemoveCacheCodesStartsWith(CodeGroupTypes codeGroupType, string value);

        /// <summary>
        /// Returns Description using a specified code type, key string
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract string GetDescriptionByCode(string codeType, string key, params string[] args);

        /// <summary>
        /// Returns Description using a specified code type, key string
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="key"></param>
        /// <param name="dbType">Database type string</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract string GetDescriptionByCode(string codeType, string key, string dbType, params string[] args);

        /// <summary>
        /// Remove all caching datas
        /// </summary>
        public abstract void RemoveCacheAll();

        /// <summary>
        /// Gets Codes unused.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="codeType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract IList<T> GetCodesIncludeUnused<T>(string codeType, params string[] args);

        /// <summary>
        /// Gets Codes unused.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="codeType"></param>
        /// <param name="dbType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract IList<T> GetCodesIncludeUnused<T>(string codeType, string dbType, params string[] args);
    }
}
