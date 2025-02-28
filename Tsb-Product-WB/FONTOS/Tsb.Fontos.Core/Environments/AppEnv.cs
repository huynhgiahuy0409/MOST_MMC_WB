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
* 2009.07.15    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Util.File;
using System.IO;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Configuration.Provider;
using System.ComponentModel;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.Security.Authorization;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Application Envronment information class
    /// </summary>
    public class AppEnv : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************
        //private static string ObjectID = "GNR-FTCO-ENV-AppEnv";

        public static BaseUserInfo UserInfo = null;
        public static IList<AuthorInfoItem> OpAuthorInfoList = null;
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        static AppEnv()
        {
            string moduleName = string.Empty;

            try
            {
                //DESIGN MODE CHECK
                if (DeployInfo.IsRuntime(CallingPositionTypes.CONSTRUCTOR))
                {
                    AppEnv.UserInfo = LoginedUserInfo.GetInstance().UserInfo;
                    AppEnv.OpAuthorInfoList = LoginedUserInfo.GetInstance().OPAuthorInfoList;
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
