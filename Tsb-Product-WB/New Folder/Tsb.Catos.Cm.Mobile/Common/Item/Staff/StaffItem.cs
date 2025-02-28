using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.Staff
{
    [Serializable]
    public class StaffItem : BaseDataItem
    {
        #region INITIALIZATION AREA *******************************************

        public StaffItem()
        {
            ObjectID = "ITM-CT-CTTA-Common-StaffItem";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _staffCode = string.Empty;
        private string _localName = string.Empty;
        private string _userGroup = string.Empty;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string StaffCode
        {
            get { return this._staffCode; }
            set { this._staffCode = value; }
        }

        public string LocalName
        {
            get { return this._localName; }
            set { this._localName = value; }
        }

        public string UserGroup
        {
            get { return this._userGroup; }
            set { this._userGroup = value; }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
