using System;
using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Common.Param
{
    [Serializable]
    public class CudServiceParam : BaseParam
    {
        #region FIELD AREA ****************************************

        private string _staffCd;
        private string _userGroup;

        #endregion FIELD AREA *************************************

        #region PROPERTY AREA *************************************

        public string StaffCd
        {
            get { return _staffCd; }
            set { _staffCd = value; }
        }

        public string UserGroup
        {
            get
            {
                return _userGroup;
            }
            set
            {
                _userGroup = value;
            }
        }

        #endregion PROPERTY AREA **********************************

        #region INITIALIZE AREA ***********************************

        public CudServiceParam()
        {
            this.ObjectID = "PAR-CT-CTMO-CudServiceParam";
        }

        public CudServiceParam(object paramOwner)
            : base(paramOwner)
        {
            this.ObjectID = "PAR-CT-CTMO-CudServiceParam";
        }

        public CudServiceParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            this.ObjectID = "PAR-CT-CTMO-CudServiceParam";
        }

        #endregion INITIALIZE AREA ********************************

    }
}
