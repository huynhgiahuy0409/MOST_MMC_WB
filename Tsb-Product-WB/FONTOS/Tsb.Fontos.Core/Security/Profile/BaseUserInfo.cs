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
* 2009.07.04    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Util.Type;

namespace Tsb.Fontos.Core.Security.Profile
{
    /// <summary>
    /// Base User Information class
    /// </summary>
    [Serializable]
    public class BaseUserInfo : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************
        private string staffCD;
        /// <summary>
        /// Gets or Sets User ID
        /// </summary>
        public string UserID 
        {
            get { return staffCD; }
            set { this.staffCD = value; }
        }

        /// <summary>
        /// Gets Staff CD
        /// </summary>
        public string StaffCD
        {
            get { return this.staffCD; }
            set { this.staffCD = value; }
        }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets encrypted password
        /// </summary>
        public string EncrypedPassword { get; set; }

        /// <summary>
        /// Gets or Sets Application use right (for CATOS)
        /// </summary>
        public string UseApp { get; set; }

        /// <summary>
        /// Gets Staff's Department Code
        /// </summary>
        public string DeptCD  { get; set; }
      
        /// <summary>
        /// Gets or Sets Main User Group of user
        /// </summary>
        public string UserGroup { get; set; }

        /// <summary>
        /// Gets or Sets Main Role ID. 
        /// It has same value to StaffCD value if its' User Group value is null or empty
        /// </summary>
        public string RoleID 
        {
            get { return string.IsNullOrEmpty(this.UserGroup) ? this.StaffCD : this.UserGroup; }
        }

        /// <summary>
        /// Gets or Sets Terminal ID (for CATOS)
        /// </summary>
        public string TmnlID { get; set; }

        /// <summary>
        /// Gets or Sets Terminal Code (for CATOS)
        /// </summary>
        public string TmnlCD { get; set; }

        /// <summary>
        /// Gets or Sets Last Password Change time
        /// </summary>
        public DateTime PwdChangeTime { get; set; }
        
        /// <summary>
        /// Gets or Sets Today
        /// </summary>
        public DateTime Today { get; set; }
        
        /// <summary>
        /// Gets or Sets Password Change Interval
        /// </summary>
        public int PwdInterval { get; set; }
        
        /// <summary>
        /// Gets or Sets the access count with wrong password
        /// </summary>
        public int Retry { get; set; }

        /// <summary>
        /// Gets boolean value whether user is a administrator or not
        /// </summary>
        public bool IsAdmin
        {
            get { return ConvertUtil.ToBoolean(this._isAdminYN); }
        }

        private string _isAdminYN = null;
        /// <summary>
        /// Gets or Sets Y/N value whether user is a administrator or not
        /// </summary>
        public string IsAdminYN 
        { 
            get { return _isAdminYN;}
            set { this._isAdminYN = Convert.ToString(value).ToUpper(); } 
        }
        
        /// <summary>
        /// Gets or Sets User Level
        /// </summary>
        public string UserLevel { get; set; }

        /// <summary>
        /// Gets boolean value whether user is expired or not
        /// </summary>
        public bool IsExpire
        {
            get { return ConvertUtil.ToBoolean(this._isExpireYN); }
        }

        private string _isExpireYN = null;
        /// <summary>
        /// Gets or Sets Y/N value whether user is expired or not
        /// </summary>
        public string IsExpireYN
        {
            get { return _isExpireYN; }
            set { this._isExpireYN = Convert.ToString(value).ToUpper(); }
        }

        private int _daysAfterLastLogin = -1;
        /// <summary>
        /// Gets or Sets Days after last login
        /// </summary>
        public int DaysAfterLastLogin 
        { 
            get { return this.LastLoginDate == null ? 0 : this._daysAfterLastLogin; }
            set { this._daysAfterLastLogin = value; }
        }
        
        /// <summary>
        /// Gets or Sets Staff's English Name
        /// </summary>
        public string EngNm { get; set; }

        /// <summary>
        /// Gets or Sets Staff's Local name
        /// </summary>
        public string LclNm { get; set; }

        /// <summary>
        /// Gets or Sets Staff's Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets OS User ID
        /// </summary>
        public string OSUserID { get; set; }
                
        /// <summary>
        /// Gets or Sets Last Login Date
        /// </summary>
        public DateTime LastLoginDate { get; set; }
        
        private IList<BaseGroupInfo> _userGroups;
        /// <summary>
        /// Gets User group info list
        /// </summary>
        public IList<BaseGroupInfo> UserGroups
        {
            get { return _userGroups; }
            set { this._userGroups = value; }
        }

        /// <summary>
        /// Gets user is authenticated or not
        /// </summary>
        public bool Authenticated  { get; set; }
        
        /// <summary>
        /// Gets whether user is authorized or not
        /// </summary>
        public bool Authorized { get; set; }

        /// <summary>
        /// Gets or Sets Staff's IP Address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or Sets Staff's signature image`s byte array.
        /// </summary>
        public byte[] SignatureByteArr { get; set; }

        /// <summary>
        /// Gets or Sets Expire Date
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseUserInfo() : base()
        {
            this.ObjectID   = "GNR-FTCO-SEC-BaseUserInfo";

            this.StaffCD   = "DEFAULT_STAFF_CD";
            this.EngNm     = "DEFAULT_ENG_NAME";
            this.LclNm     = "DEFAULT_LOCAL_NAME";
            this.DeptCD    = "DEFAULT_USER_NAME";
            this.Title     = "DEFAULT_TITLE";
            this.UserLevel = "DEFAULT_LEVEL";
            this.IsAdminYN = "N";

            this._userGroups   = null;
            this.Authenticated = false;
            this.Authorized    = false;

            this.OSUserID = LoginedUserInfo.USER_WINLOGON_NAME;
        }


        /// <summary>
        /// Initilizes BaseUserInfo object with a specified staff cd, dept cd, level, admin, usergroup array, and authenticated 
        /// </summary>
        /// <param name="staffCd">staff code</param>
        /// <param name="deptCd">dept code</param>
        /// <param name="level">user level</param>
        /// <param name="isAdminYN">Y/N string whether user is a admin or not</param>
        /// <param name="userGroups">user group list</param> 
        /// <param name="authenticated">is authenticated or not</param>
        public BaseUserInfo(string staffCd, string deptCd, string level, string isAdminYN, IList<BaseGroupInfo> userGroups, bool authenticated)
            : this()
        {
            this.StaffCD = staffCd;
            this.DeptCD = deptCd;
            this.UserLevel = level;
            this.IsAdminYN = isAdminYN;
            this._userGroups = userGroups;
            this.Authenticated = authenticated;
        }
        #endregion
    }
}
