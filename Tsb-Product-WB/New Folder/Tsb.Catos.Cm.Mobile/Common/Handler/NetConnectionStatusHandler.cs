using System;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Catos.Cm.Mobile.Common.Handler
{
    public class NetConnectionStatusHandler : TsbBaseObject
    {
        #region CONST & FIELD AREA ********************************************

        private static NetConnectionStatusHandler _instance = null;
        private bool _isConnectedC3IT = false;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public static NetConnectionStatusHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NetConnectionStatusHandler();
                }

                return _instance;
            }
        }

        public bool IsConnectedC3IT
        {
            get { return this._isConnectedC3IT; }
            set { this._isConnectedC3IT = value; }
        }

        #endregion METHOD AREA ************************************************
    }
}
