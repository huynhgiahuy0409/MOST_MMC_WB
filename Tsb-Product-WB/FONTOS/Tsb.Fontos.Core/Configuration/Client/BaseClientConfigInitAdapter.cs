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
* 2010.02.04    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Configuration.Provider;
using System.Collections;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Context;
using Tsb.Fontos.Core.Util.File;
using System.IO;
using System.Reflection;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions.System;
using System.Diagnostics;
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Configuration.Types;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Security.Authentication;
using System.Collections.Specialized;
using Tsb.Fontos.Core.Security.Encryption;

namespace Tsb.Fontos.Core.Configuration.Client
{
    /// <summary>
    /// Base Client Config Initializer
    /// </summary>
    public class BaseClientConfigInitAdapter : TsbBaseObject, IConfigInitAdapter
    {
        #region FIELD/PROPERTY AREA*****************************
        private static readonly string OBJECT_ID = "GNR-FTCO-CFG-BaseClientConfigInitAdapter";
        public IBaseEncrypter PasswordEncrypter { get; set; }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseClientConfigInitAdapter(): base()
        {
            this.ObjectID = BaseClientConfigInitAdapter.OBJECT_ID;
            this.ObjectType = ObjectType.HELPER;
        }
        #endregion


        #region METHOD AREA (Initialize) ***********************
        /// <summary>
        /// Initialize Configuration
        /// </summary>
        /// <returns>IConfigInfo implementer object reference</returns>
        public virtual IConfigInfo InitConfig()
        {
            return null;
        }
        #endregion


        #region METHOD AREA (CONFIG PATH INFO)******************
        /// <summary>
        /// Configurates application path information
        /// </summary>
        public virtual void ConfigPathInfo()
        {
            return;
        }
        #endregion





    }
}
