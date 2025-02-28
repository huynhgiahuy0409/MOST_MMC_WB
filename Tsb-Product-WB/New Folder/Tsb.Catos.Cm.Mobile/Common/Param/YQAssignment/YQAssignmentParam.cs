using System;
using System.Collections;
using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Common.Param.YQAssignment
{
    [Serializable]
    public class YQAssignmentParam : BaseParam
    {
        #region INITIALIZATION AREA *******************************************

        public YQAssignmentParam()
            : this(null)
        {
        }

        public YQAssignmentParam(object paramOwner)
            : this(paramOwner, null)
        {
        }

        public YQAssignmentParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private const string OBJECT_ID = "PAR-CT-CTMO-YQAssignmentParam";

        private string _equNo = string.Empty;
        private string _equType = string.Empty;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string EquNo
        {
            get { return this._equNo; }
            set { this._equNo = value; }
        }

        public string EquType
        {
            get { return this._equType; }
            set { this._equType = value; }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
