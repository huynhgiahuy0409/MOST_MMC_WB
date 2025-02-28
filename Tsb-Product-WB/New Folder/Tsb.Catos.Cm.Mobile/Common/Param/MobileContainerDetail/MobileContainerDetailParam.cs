using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Common.Param.MobileContainerDetail
{
    public class MobileContainerDetailParam : BaseParam
    {
        #region INITIALIZATION AREA *******************************************

        private readonly string OBJECT_ID = "PAR-CT-CTMO-MobileContainerDetailParam";

        public MobileContainerDetailParam(object paramOwner)
            : base(paramOwner)
        {
            ObjectID = OBJECT_ID;
        }

        public MobileContainerDetailParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _mmode = string.Empty;
        private string _equipmentNo = string.Empty;
        private string _containerNo = string.Empty;
        private string _vesselCode = string.Empty;
        private string _callYear = string.Empty;
        private string _callSequence = string.Empty;
        private string _qjobType = string.Empty;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string MMode
        {
            get { return this._mmode; }
            set { this._mmode = value; }
        }

        public string EquipmentNo
        {
            get { return this._equipmentNo; }
            set { this._equipmentNo = value; }
        }

        public string ContainerNo
        {
            get { return this._containerNo; }
            set { this._containerNo = value; }
        }

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

        public string QJobType
        {
            get { return this._qjobType; }
            set { this._qjobType = value; }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
