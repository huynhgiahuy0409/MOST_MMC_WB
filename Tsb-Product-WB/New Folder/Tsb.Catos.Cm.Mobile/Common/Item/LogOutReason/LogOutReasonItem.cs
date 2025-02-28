using System;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Resources;

namespace Tsb.Catos.Cm.Mobile.Common.Item.LogOutReason
{
    [Serializable]
    public class LogOutReasonItem : BaseDataItem
    {
        #region FIELD AREA *************************************

        private MOCommonConstants.LogOutReason _reason;

        #endregion FIELD AREA **********************************

        #region PROPERTY AREA **********************************

        public MOCommonConstants.LogOutReason Reason
        {
            get
            {
                return _reason;
            }
        }

        public string Description
        {
            get
            {
                string desc = string.Empty;
                if (Reason == MOCommonConstants.LogOutReason.ChangeEquipmentNo)
                {
                    desc = ResourceFactory.GetResource().GetLabel("WRD_CTMO_ChangeEqNo");
                }
                else if (Reason == MOCommonConstants.LogOutReason.EndOfShift)
                {
                    desc = ResourceFactory.GetResource().GetLabel("WRD_CTMO_EndOfShift");
                }
                else if (Reason == MOCommonConstants.LogOutReason.Others)
                {
                    desc = ResourceFactory.GetResource().GetLabel("WRD_CTMO_Others");
                }
                else if (Reason == MOCommonConstants.LogOutReason.ChangeVessel)
                {
                    desc = ResourceFactory.GetResource().GetLabel("WRD_CTMO_ChangeVessel");
                }
                else if (Reason == MOCommonConstants.LogOutReason.WorkFinished)
                {
                    desc = ResourceFactory.GetResource().GetLabel("WRD_CTMO_WorkFinished");
                }
                else if (Reason == MOCommonConstants.LogOutReason.GangShift)
                {
                    desc = ResourceFactory.GetResource().GetLabel("WRD_CTMO_GangShift");
                }
                else if (Reason == MOCommonConstants.LogOutReason.ChangeOperator)
                {
                    desc = ResourceFactory.GetResource().GetLabel("WRD_CTMO_ChangeOperator");
                }
                return desc;
            }
        }

        public string Code
        {
            get
            {
                string code = string.Empty;
                if (Reason == MOCommonConstants.LogOutReason.ChangeEquipmentNo)
                {
                    code = "C";
                }
                else if (Reason == MOCommonConstants.LogOutReason.EndOfShift)
                {
                    code = "S";
                }
                else if (Reason == MOCommonConstants.LogOutReason.Others)
                {
                    code = "Z";
                }
                else if (Reason == MOCommonConstants.LogOutReason.ChangeVessel)
                {
                    code = "V";
                }
                else if (Reason == MOCommonConstants.LogOutReason.WorkFinished)
                {
                    code = "F";
                }
                else if (Reason == MOCommonConstants.LogOutReason.GangShift)
                {
                    code = "G";
                }
                else if (Reason == MOCommonConstants.LogOutReason.ChangeOperator)
                {
                    code = "O";
                }
                return code;
            }
        }

        #endregion PROPERTY AREA *******************************

        #region INITIALIZATION AREA ****************************

        public LogOutReasonItem(MOCommonConstants.LogOutReason reason)
        {
            this.ObjectID = "ITM-CT-CTMO-LogOutReasonItem";
            this._reason = reason;
        }

        #endregion
    }
}
