using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Common.Param.Staff
{
    public class GetStaffParam: BaseParam
    {
        #region INITIALIZATION AREA *******************************************

        private readonly string OBJECT_ID = "PAR-CT-CTMO-GetStaffParam";

        public GetStaffParam(object paramOwner)
            : base(paramOwner)
        {
            ObjectID = OBJECT_ID;
        }

        public GetStaffParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _userGroup = string.Empty;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string UserGroup
        {
            get { return this._userGroup; }
            set { this._userGroup = value; }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
