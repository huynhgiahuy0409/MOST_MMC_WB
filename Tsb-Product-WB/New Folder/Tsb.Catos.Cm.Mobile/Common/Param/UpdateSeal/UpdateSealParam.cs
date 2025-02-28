using Tsb.Catos.Cm.Mobile.Common.Item.UpdateSeal;
using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Common.Param.UpdateSeal
{
    public class UpdateSealParam : BaseParam
    {
        #region INITIALIZATION AREA *******************************************

        private readonly string OBJECT_ID = "PAR-CT-CTMO-UpdateSealParam";

        public UpdateSealParam(object paramOwner)
            : base(paramOwner)
        {
            ObjectID = OBJECT_ID;
        }

        public UpdateSealParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _mmode = string.Empty;
        private UpdateSealItem _updateSealItem;
        private string _staffCd;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string MMode
        {
            get { return this._mmode; }
            set { this._mmode = value; }
        }

        public UpdateSealItem UpdateSealItem
        {
            get { return this._updateSealItem; }
            set { this._updateSealItem = value; }
        }

        public string StaffCd
        {
            get { return this._staffCd; }
            set { this._staffCd = value; }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
