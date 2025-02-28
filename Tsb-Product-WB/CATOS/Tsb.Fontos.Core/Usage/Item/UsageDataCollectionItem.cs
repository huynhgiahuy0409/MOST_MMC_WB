#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2021 TOTAL SOFT BANK LIMITED. All Rights
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
* 2021.12.22     WM.Jeong	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Usage.Item
{
    [Serializable]
    public class UsageDataCollectionItem : BaseDataItem
    {
        #region FIELD AREA ********************************
        private string _viewGuid;
        private string _terminalCode;
        private string _modulePgmCode;
        private string _menuId;
        private string _viewTitle;
        private string _actionType;
        private DateTime? _startTime;
        private DateTime? _endTime;
        #endregion

        #region PROPERTY AREA *****************************
        public string ViewGuid
        {
            get
            {
                return _viewGuid;
            }
        }

        public string TerminalCode
        {
            get
            {
                return _terminalCode;
            }
        }

        public string ModulePgmCode
        {
            get
            {
                return _modulePgmCode;
            }
        }

        public string MenuId
        {
            get
            {
                return _menuId;
            }
        }

        public string ViewTitle
        {
            get
            {
                return _viewTitle;
            }
        }

        public string ActionType
        {
            get
            {
                return _actionType;
            }
        }

        public DateTime? StartTime
        {
            get
            {
                return _startTime;
            }
        }

        public DateTime? EndTime
        {
            get
            {
                return _endTime;
            }
        }

        public override string Key
        {
            get
            {
                return base.GUID.ToString();
            }
        }
        #endregion

        #region INITIALIZE AREA ***************************
        public UsageDataCollectionItem(string actionType)
        {
            _actionType = actionType;
            base.ObjectID = "ITM-FT-FTCO-USG-UsageDataItem";
        }
        #endregion

        #region METHOD AREA *******************************
        public void SetDefaultModuleInfo()
        {
            _modulePgmCode = ModuleInfo.PgmCode;

            if (AppEnv.UserInfo != null)
            {
                _terminalCode = AppEnv.UserInfo.TmnlCD;
                base.UserID = AppEnv.UserInfo.UserID;
            }
        }

        public void SetDefaultMenuInfo(string viewGuid, string menuId, string viewTitle)
        {
            _viewGuid = viewGuid;
            _menuId = menuId;
            _viewTitle = viewTitle;
        }

        public void SetCurrentStartTime()
        {
            _startTime = DateTime.Now;
        }

        public void SetCurrentEndTime()
        {
            _endTime = DateTime.Now;
        }
        #endregion
    }
}
