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
* 2020.10.13     Chun   1.0	First release.
* 
*/

using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.ContainerRules
{
    [Serializable]
    public class ContainerRuleItem : BaseDataItem
    {
        #region FIELD

        public readonly static string MSG_TYPE_ERROR = "E";
        public readonly static string MSG_TYPE_WARNING = "W";
        public readonly static string MSG_TYPE_ASK = "A";

        #endregion

        #region PROPERTY

        public string RuleId { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string RuleCode { get; set; }
        public string RuleDesc { get; set; }
        public string Priority { get; set; }

        #endregion

        #region INITIALIZE

        public ContainerRuleItem()
        {
            ObjectID = "ITM-CT-CTMO-CNTR-ContainerRuleItem";
        }
        #endregion
    }
}
