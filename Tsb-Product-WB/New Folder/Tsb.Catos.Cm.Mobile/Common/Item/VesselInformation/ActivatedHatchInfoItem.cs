using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation
{
    [Serializable]
    //added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이
    public class ActivatedHatchInfoItem : BaseDataItem
    {
        #region CONST & FIELD AREA ********************************************

        public const string OBJECT_ID = "ITM-CT-CTMO-ActivatedHatchInfoItem";

        private string _hatchIdx = string.Empty;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string HatchIdx
        {
            get { return this._hatchIdx; }
            set { this._hatchIdx = value; }
        }

        #endregion PROPERTY AREA **********************************************
        

        #region INITIALIZATION AREA *******************************************

        public ActivatedHatchInfoItem()
        {
            this.ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************

    }
}
