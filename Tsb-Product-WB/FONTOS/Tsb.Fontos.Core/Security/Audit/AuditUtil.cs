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
* 2010.02.23  Tonny Kim 1.0	First release.
* 2010.12.06  Tonny.Kim     Update (GetStaffCd)
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;

namespace Tsb.Fontos.Core.Security.Audit
{
    /// <summary>
    /// Audit Utility
    /// </summary>
    public class AuditUtil : TsbBaseObject
    {
        #region CONSTANT AREA ****************************
        private static readonly string PGM_CDOE_DELIMIT = "-";
        private static readonly int MODULE_ID_MAX_LENGTH = 15;
        private static readonly int USER_ID_MAX_LENGTH = 10;
        private static readonly string NOT_NOTIFY_TO_RMS_DELIMIT = "^";
        #endregion

        #region METHOD AREA (GetStaffCd)********************
        /// <summary>
        /// Gets Staff Code
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public static string GetStaffCd(string formName)
        {
            string staffCd = AppEnv.UserInfo.StaffCD;
            return AuditUtil.GetStaffCd(formName, staffCd);
        }

        /// <summary>
        /// Gets Staff Code
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="isNotNotifytoRMS"></param>
        /// <returns></returns>
        public static string GetStaffCd(string formName, bool isNotNotifytoRMS)
        {
            string staffCd = AppEnv.UserInfo.StaffCD;

            return AuditUtil.GetStaffCd(formName, staffCd, isNotNotifytoRMS);
        }

        /// <summary>
        /// Gets Staff Code
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetStaffCd(string formName, string userID)
        {
            return AuditUtil.GetStaffCd(formName, userID, false);
        }

        /// <summary>
        /// Gets Staff Code
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="userID"></param>
        /// <param name="isNotNotifytoRMS"></param>
        /// <returns></returns>
        public static string GetStaffCd(string formName, string userID, bool isNotNotifytoRMS)
        {
            string notNotifytoRMSDelimit = string.Empty;

            if (isNotNotifytoRMS == true)
            {
                notNotifytoRMSDelimit = NOT_NOTIFY_TO_RMS_DELIMIT;
            }

            string moduleId = ModuleInfo.PgmCode + notNotifytoRMSDelimit + PGM_CDOE_DELIMIT + formName;

            return GenerateStaffCd(moduleId, userID);
        }
        #endregion

        #region METHOD(GetStaffCdUserDefine) AREA *******************
        /// <summary>
        /// Gets user defined StaffCd
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetStaffCdUserDefine(string moduleId, string userID)
        {
            return GenerateStaffCd(moduleId, userID);
        }
        #endregion

        #region METHOD(GENERAL) AREA ********************************
        /// <summary>
        /// Generate StaffCd
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        private static string GenerateStaffCd(string moduleId, string userID)
        {
            string returnStaffCd = AuditUtil.GetMaxLengthPadString(userID, AuditUtil.USER_ID_MAX_LENGTH);

            moduleId = AuditUtil.GetMaxLengthPadString(moduleId, AuditUtil.MODULE_ID_MAX_LENGTH);
            returnStaffCd += moduleId;

            return returnStaffCd;
        }

        /// <summary>
        /// Pad MaxLength String
        /// </summary>
        /// <param name="target"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        private static string GetMaxLengthPadString(string target, int maxLength)
        {
            string rtnValue = "";

            if (target.Length > maxLength) rtnValue = target.Substring(0, maxLength);
            else rtnValue = target.PadRight(maxLength, ' ');

            return rtnValue;
        }
        #endregion
    }
}
