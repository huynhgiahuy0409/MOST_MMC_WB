using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Common.Param.Stoppage
{
    public class StoppageReasonParam : BaseParam
    {
        #region INITIALIZATION AREA *******************************************

        private readonly string OBJECT_ID = "PAR-CT-CTMO-StoppageReasonParam";

        public StoppageReasonParam(object paramOwner)
            : base(paramOwner)
        {
            ObjectID = OBJECT_ID;
        }

        public StoppageReasonParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _equNo = string.Empty;
        private bool _isExcludeVesselStoppage = false; // added by BG.Kim (2023.01.10)	[PCT] Not show vessel stoppages on Tally VMT

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string EquNo
        {
            get { return this._equNo; }
            set { this._equNo = value; }
        }

        public bool IsExcludeVesselStoppage // added by BG.Kim (2023.01.10)	[PCT] Not show vessel stoppages on Tally VMT
        {
            get { return this._isExcludeVesselStoppage; }
            set { this._isExcludeVesselStoppage = value; }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
