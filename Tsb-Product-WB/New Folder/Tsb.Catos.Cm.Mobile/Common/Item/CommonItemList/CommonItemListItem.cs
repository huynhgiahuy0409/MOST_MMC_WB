#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2015 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		          REVISION
* 2018.04.09   YoungOk Kim 1.0	First release.
*/
#endregion

using System;
using System.Drawing;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.CommonItemList
{
    [Serializable]
    public class CommonItemListItem : BaseDataItem
    {
        #region FIELD AREA *************************************
        #endregion FIELD AREA **********************************

        #region PROPERTY AREA **********************************

        public string Code { get; set; }
        public string CodeName { get; set; }
        public string DisplayValue { get; set; }
        public Color BackColor { get; set; }
        public string CustomStyleName { get; set; }

        #endregion PROPERTY AREA *******************************

        #region INITIALIZATION AREA ****************************

        public CommonItemListItem()
        {
            this.ObjectID = "ITM-CT-CTMO-CommonItemListItem";
        }

        #endregion INITIALIZATION AREA *************************
    }
}
