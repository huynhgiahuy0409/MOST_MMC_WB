using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Catos.Cm.Mobile.Common.Constants
{
    public class MOCommonConstants
    {
        public class EquipmentStatus
        {
            public const string IDLE = "I";
            public const string RUN = "O";
            public const string STOPPAGE = "S";
            public const string PMS = "P";
            public const string REPAIR = "R";
            public const string EMERGENCY_STOP = "E";
        }

        public enum LogOutReason
        {
            EndOfShift,
            ChangeEquipmentNo,
            Others,
            ChangeVessel,
            WorkFinished,
            GangShift,
            ChangeOperator
        };

        public const string EQU_STOPPAGE_RESUME = "RP";
        public const string EQU_STOPPAGE_ROLLBACK = "RK";

        public class StoppageAuthority
        {
            public const string RDT = "1";
            public const string CONTROLLER = "2";
            public const string ALL = "3";
        }
    }
}
