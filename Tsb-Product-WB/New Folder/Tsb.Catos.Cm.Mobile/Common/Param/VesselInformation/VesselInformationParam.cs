using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Common.Param.VesselInformation
{
    public class VesselInformationParam : BaseParam
    {
        #region INITIALIZATION AREA *******************************************

        private readonly string OBJECT_ID = "PAR-CT-CTMO-VesselInformationParam";

        public VesselInformationParam(object paramOwner)
            : base(paramOwner)
        {
            ObjectID = OBJECT_ID;
        }

        public VesselInformationParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _vesselCode = string.Empty;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string VesselCode
        {
            get { return this._vesselCode; }
            set { this._vesselCode = value; }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
