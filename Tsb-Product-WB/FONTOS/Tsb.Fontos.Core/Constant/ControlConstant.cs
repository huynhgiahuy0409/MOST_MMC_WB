#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2013 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		     REVISION    	
* 2013.11.29  Eric.Jang 1.0   First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Environments;

namespace Tsb.Fontos.Core.Constant
{
    public class ControlConstant
    {
        #region COSNTANT AREA ***************************************
        public const string TSB_DEFINED_PROPERTY = "TSB Defined Property";
        public const string TSB_STATE_DEFINED_PROPERTY = "TSB State Defined Property";
        public const string TSB_DEFINED_EVENT    = "TSB Defined Event";
        public const string TSB_DOCKING_PROPERTY = "TSB Docking Property";
        public const string CULTURE_SETTING_ARABIC = "ar";
        public const string ARABIC_REGEX_PATTERN = "^[\u0600-\u06FF]";
        public const string NONCHARACTER_REGEX_PATTERN = @"\W";        
        #endregion
    }
}
