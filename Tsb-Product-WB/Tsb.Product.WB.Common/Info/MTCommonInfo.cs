using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Product.WB.Common.Info
{
    public class MTCommonInfo : TsbBaseObject
    {
        #region FIELD AREA ******************************************
        private static MTCommonInfo _instance = null;       
        private static string _laneName = string.Empty;
        private static string _laneCode = string.Empty;
        private static string _gateName = string.Empty;
        private static string _gateCode = string.Empty;
        private static readonly string OBJECT_ID = "GNR-MTCO-ENV-MTCommonInfo";
        #endregion

        #region PROPERTY AREA ***************************************
        public string LaneName
        {
            get { return _laneName; }
            set { _laneName = value; }
        }
        public string LaneCode
        {
            get { return _laneCode; }
            set { _laneCode = value; }
        }
        public string GateName
        {
            get { return _gateName; }
            set { _gateName = value; }
        }
        public string GateCode
        {
            get { return _gateCode; }
            set { _gateCode = value; }
        }
        #endregion
        #region INITIALIZE AREA *************************************
        private MTCommonInfo()
             : base()
        {
            this.ObjectID = OBJECT_ID;
        }
        #endregion

        #region METHOD AREA *****************************************
        public static MTCommonInfo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MTCommonInfo();

            }
            return _instance;
        }
        #endregion
    }
}
