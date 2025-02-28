using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Param;

// author : YH.CHOI
namespace Tsb.Catos.Cm.Mobile.Common.Param.VesselInformation
{
    public class ActivatedVesselInformationParam : BaseParam
    {
        #region INITIALIZATION AREA *******************************************

        private readonly string OBJECT_ID = "PAR-CT-CTMO-ActivatedVesselInformationParam";
        
        public ActivatedVesselInformationParam()
        {
            ObjectID = OBJECT_ID;
        }

        public ActivatedVesselInformationParam(object paramOwner)
            : base(paramOwner)
        {
            ObjectID = OBJECT_ID;
        }

        public ActivatedVesselInformationParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            ObjectID = OBJECT_ID;
        }


        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _vesselCode = string.Empty;
        private string _callSequence = string.Empty;
        private string _callYear = string.Empty;
        private string _equNo = string.Empty;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string VesselCode
        {
            get { return this._vesselCode; }
            set { this._vesselCode = value; }
        }

        public string CallYear
        {
            get { return this._callYear; }
            set { this._callYear = value; }
        }

        public string CallSequence
        {
            get { return this._callSequence; }
            set { this._callSequence = value; }
        }

        public string EquNo
        {
            get { return this._equNo; }
            set { this._equNo = value; }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
