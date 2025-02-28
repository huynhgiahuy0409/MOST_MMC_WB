using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Catos.Cm.Mobile.Common.Constants
{
    public class CodeConstants
    {
        public static readonly String REG_YARD_OPR = "Yard Operation";
        public static readonly String REG_PARKING = "RTG Parking";

        // Virtual Vessel
        public static readonly String VSL_EVP = "EMTY"; 	// Empty Van Pool Vessel
        public static readonly String VSL_TBA = "TBA";		// To-Be-Advised Vessel (Export Virtual Vessel)
        public static readonly String VSL_DUMMY = "DUMY";  // Dummy vessel
        public static readonly String VSL_STRG = "STRG";  // Storage Import/Export
        public static readonly String VSL_OTHR = "OTHR"; // added by jaeok (2019.05.09) Mantis 89973: [Booking List] Empty Out Enhancement

        /**
         * Added by Steve: 2008.12.31
         */
        // Import/Export CD
        public static readonly String IXCD_IMPORT = "I";      // Import
        public static readonly String IXCD_EXPORT = "X";      // Export
        public static readonly String IXCD_STORAGE = "V";      // V Storage Van Pool
        public static readonly String IXCD_STORAGE_EXPORT = "R";
        public static readonly String IXCD_STORAGE_IMPORT = "D";

        // Container State
        public static readonly String CNTR_STATE_RESERVED = "R";      //    R  Reserved (Before Reconcile)
        public static readonly String CNTR_STATE_BOOKED = "B";      //    B  Booked (After Reconcile)
        public static readonly String CNTR_STATE_STACKED = "Y";      //    Y  Stacked in slot
        public static readonly String CNTR_STATE_OUTGOING = "G";      //    G  In Progress Outgoing
        public static readonly String CNTR_STATE_INCOMING = "O";       //    O  In Progress Incoming
        public static readonly String CNTR_STATE_SHIFTING = "Z";      //    Z  Under Shifting (Re-marshaling)
        public static readonly String CNTR_STATE_DELIVERED = "D";      //    D  Delivered (Gate-Out/Loaded)

        // sRefType Value:
        public readonly static String REF_TYPE_BAPLIE = "B";
        public readonly static String REF_TYPE_IN_COPINO = "I";
        public readonly static String REF_TYPE_OUT_COPINO = "O";
        public readonly static String REF_TYPE_INVENTORY = "V";
        public readonly static String REF_TYPE_MASTER = "M";

        // Container Size Define
        public readonly static String SIZE_10 = "1";
        public readonly static String SIZE_20 = "2";
        public readonly static String SIZE_30 = "3";
        public readonly static String SIZE_40 = "4";
        public readonly static String SIZE_45 = "L";
        public readonly static String SIZE_48 = "M";
        public readonly static String SIZE_50 = "P"; //added by jaehoon(2013.01.25)

        // Yes/No Mode Value
        public readonly static String YES = "Y";
        public readonly static String NO = "N";

        // Block Or Area
        public readonly static String BLOCKAREA_TYPE_AREA = "A";
        public readonly static String BLOCKAREA_TYPE_BLOCK = "B";

        // Yard Job Type
        public readonly static String YJOB_TYPE_VI = "DF"; // DF : lift off for disch.
        public readonly static String YJOB_TYPE_VO = "LO"; // LO: lift on for loading
        public readonly static String YJOB_TYPE_GI = "GF"; // GF : lift off for gate-in
        public readonly static String YJOB_TYPE_GO = "GO"; // GO: lift on for gate-out
        public readonly static String YJOB_TYPE_YY = "YY"; // YY: shift in one bay
        public readonly static String YJOB_TYPE_YO = "YO"; // YO: lift on for shift
        public readonly static String YJOB_TYPE_YF = "YF"; // YF: lift off for shift
        public readonly static String YJOB_TYPE_AUTO_SHIFT = "B1"; // B1 : Auto-Shifting
        public readonly static String YJOB_TYPE_TAKE_BACK = "B2";	// B2 : Take-back after auto-shifting
        public readonly static String YJOB_TYPE_RI = "RF"; // RF : Lift Off for Rail
        public readonly static String YJOB_TYPE_RO = "RO"; // RO : Lift On for Rail

        // shuttle job type
        public readonly static String YJOB_TYPE_SI = "YO";
        public readonly static String YJOB_TYPE_SO = "YF";

        // QC Job Type
        public readonly static String QJOB_TYPE_GD = "GD"; // GD : Discharging
        public readonly static String QJOB_TYPE_GL = "GL"; // GL : Loading
        public readonly static String QJOB_TYPE_SD = "SD"; // SD : 2-time shift(Discharging)
        public readonly static String QJOB_TYPE_SS = "SS"; // SS : 1-time shift
        public readonly static String QJOB_TYPE_SL = "SL"; // SL : 2-time shift(Loading)

        public readonly static String GJOB_TYPE_GT = "GT"; // GATE OUT
        public readonly static String GJOB_TYPE_GI = "GI"; // GATE IN
        public readonly static String GJOB_TYPE_GO = "GO"; // GATE IN

        // Rail Job Type
        public readonly static String RJOB_TYPE_RD = "RD"; // RD : Rail Discharging
        public readonly static String RJOB_TYPE_RL = "RL"; // RL : Rail Loading

        // Reefer Job Type
        public readonly static String JOB_TYPE_REEFER_PI = "PI"; // PI : Plug-IN
        public readonly static String JOB_TYPE_REEFER_PO = "PO"; // PO : Plug-OUT

        // Reefer Plugging Mode
        public readonly static String PLUG_MODE_GI = "GI";
        public readonly static String PLUG_MODE_GO = "GO";
        public readonly static String PLUG_MODE_VI = "VI";
        public readonly static String PLUG_MODE_VO = "VO";
        public readonly static String PLUG_MODE_RI = "RI";
        public readonly static String PLUG_MODE_RO = "RO";
        public readonly static String PLUG_MODE_YI = "YI";
        public readonly static String PLUG_MODE_YO = "YO";

        // RSRV_TYPE
        public readonly static String RSRV_TYPE_RAILBOOKING = "R";  // RAIL BOOKING
        public readonly static String RSRV_TYPE_EXPORTBOOKING = "B";  // EXPORT BOOKING
        public readonly static String RSRV_TYPE_DISCHARGINGBOOKING = "D";  // DISCHARGING BOOKING
        public readonly static String RSRV_TYPE_TSEXPORT = "S";  // T/S Export
        public readonly static String RSRV_TYPE_RETURN2TMNL = "T";  // Return to terminal
        public readonly static String RSRV_TYPE_EXPORTWITHOUTBOOKING = "X";  // EXPORT BOOKING (Without Booking)

        // TRANS_TYPE
        public readonly static String TRANS_TYPE_TRUCK = "T"; // Truck
        public readonly static String TRANS_TYPE_VESSEL = "V"; // Vessel
        public readonly static String TRANS_TYPE_RAIL = "R"; // Rail
        public readonly static String TRANS_TYPE_BARGE = "B"; // Barge
        public readonly static String TRANS_TYPE_CFS = "C"; // CFS
        public readonly static String TRANS_TYPE_MODECHANGE = "M"; // Mode Change
        public readonly static String TRANS_TYPE_INTERFACE = "I"; // Interface
        public readonly static String TRANS_TYPE_ITT = "I"; //2011-11-05 JaeHoon it's same with INTERFACE

        // DISPATCH_MODE
        public readonly static String DMODE_DS = "DS"; // Vessel Discharge
        public readonly static String DMODE_RI = "RI"; // Rail Discharge
        public readonly static String DMODE_IP = "IP"; // Positioning Gate-In
        public readonly static String DMODE_IS = "IS"; // Storage Gate-In
        public readonly static String DMODE_EX = "EX"; // Export Gate-In
        public readonly static String DMODE_II = "II"; // Return to terminal (Empty Return)
        public readonly static String DMODE_TI = "TI"; // Interface In
        public readonly static String DMODE_CI = "CI"; // CFS-In
        public readonly static String DMODE_RT = "RT"; // Return to terminal

        // DISPATCH_MODE2
        public readonly static String DMODE2_LD = "LD"; // Vessel Loading
        public readonly static String DMODE2_RO = "RO"; // Rail Loading
        public readonly static String DMODE2_OP = "OP"; // Positioning Gate-Out
        public readonly static String DMODE2_EP = "EP"; // Empty Pickup
        public readonly static String DMODE2_OS = "OS"; // Storage Gate-Out
        public readonly static String DMODE2_IM = "IM"; // Import Gate-Out
        public readonly static String DMODE2_RE = "RE"; // Loading Cancel/Return
        public readonly static String DMODE2_TO = "TO"; // Interface Out
        public readonly static String DMODE2_CO = "CO"; // CFS-Out
        public readonly static String DMODE2_RM = "RM"; // Dummy Remarshalling (not actually dispatch_mode2)

        public readonly static String DMODE2_OF = "OF"; // Import Transport Out
        public readonly static String DMODE2_IT = "IT"; // TS to Other Terminal
        public readonly static String DMODE2_EF = "EF"; // Export Transport Out

        // Full/Empty CD
        public static readonly String FE_FULL = "F";
        public static readonly String FE_EMPTY = "E";

        // Cargo Type
        public readonly static String CARGO_TYPE_GENERAL = "GP"; // GP   General
        public readonly static String CARGO_TYPE_EMPTY = "MT"; // MT   Empty
        public readonly static String CARGO_TYPE_REEFER = "RF"; // RF   Reefer
        public readonly static String CARGO_TYPE_DANGEROUS = "DG"; // DG   Dangerous
        public readonly static String CARGO_TYPE_EMPTY_DG = "ED"; // ED   Dangerous Empty
        public readonly static String CARGO_TYPE_REEFER_DG = "DR"; // DR   Reefer & DG
        public readonly static String CARGO_TYPE_BUNDLE = "BN"; // BN   Bundle
        public readonly static String CARGO_TYPE_BREAK_BULK = "BB"; // BB   Break Bulk
        public readonly static String CARGO_TYPE_OVER_DIM = "AK"; // AK   Over Dimension
        public readonly static String CARGO_TYPE_FRAGILE = "FR"; // FR   Fragile

        // Position Charcter
        public readonly static String POSITION_ON_FORE = "F"; // FORE
        public readonly static String POSITION_ON_MIDDLE = "M"; // MIDDLE
        public readonly static String POSITION_ON_AFTER = "A"; // AFTER

        //  Delivery Type
        public static readonly String DELV_HOTDELIVERY = "H";
        public static readonly String DELV_DIRECTDELIVERY = "D";
        public static readonly String DELV_TS2VESSEL = "S";
        public static readonly String DELV_TS2TMNL = "P";
        public static readonly String DELV_TSFROMTMNL = "Z";
        public static readonly String DELV_2TIMESHIFT = "T";
        public static readonly String DELV_TSFROMTMNL_DIR = "X";
        public static readonly String DELV_HOTCONNECTION = "Q";
        public static readonly String DELV_REEXPORT = "E";

        // Damage Code
        public readonly static String DMG_COND_SOUND = "S";
        public readonly static String DMG_COND_MINOR = "M";
        public readonly static String DMG_COND_HEAVY = "H";
        public readonly static String DMG_COND_RECHECK = "R";

        // Defective Code
        public readonly static String DEFECT_CODE_SOUND = "S";
        public readonly static String DEFECT_CODE_TO_BE_REPAIRED = "R";
        public readonly static String DEFECT_CODE_TEMPORARY_FIXED = "T";
        public readonly static String DEFECT_CODE_NO_REPAIR = "N";

        // Clean Code
        public readonly static String CLEAN_CODE_CLEAN = "L";
        public readonly static String CLEAN_CODE_SWEEP_ONLY = "S";
        public readonly static String CLEAN_CODE_WASH = "W";
        public readonly static String CLEAN_CODE_HOT_WASH = "H";
        public readonly static String CLEAN_CODE_CHEMICAL_WASH = "C";
        public readonly static String CLEAN_CODE_UNCLEANING = "U";

        // Wheel /Deck
        public readonly static String WD_TYPE_WHEELED = "W"; // Wheeled
        public readonly static String WD_TYPE_DECKED = "D"; // Decked

        // Yard Shift Type
        public readonly static String SHIFT_TYPE_APRON = "A"; // Apron
        public readonly static String SHIFT_TYPE_ON_CHASSIS = "O"; // On Chassis
        public readonly static String SHIFT_TYPE_YARD = "Y"; // Yard

        // hold/deck
        public readonly static String SHD_HOLD = "H"; // Hold
        public readonly static String SHD_DECK = "D"; // Deck

        // AlongSide: Portside (占쏙옙占쏙옙), Starboard (占쏙옙占쏙옙)
        public readonly static String ALONGSIDE_PORTSIDE = "P"; // Portside
        public readonly static String ALONGSIDE_STARBOARD = "S"; // Starboard

        // Remarshalling Order State
        public readonly static String ODR_STATUS_TRANSFERED = "T";  // Transferred at interchange-area, for RM
        //    public readonly static String ODR_STATUS_readonlyIZED     = "F";  // readonlyized: Order is run to readonly destination
        //    public readonly static String ODR_STATUS_COMPLETED     = "C";  // Completed: General completed

        public readonly static String JOB_LIFT_OFF = "F";
        public readonly static String JOB_LIFT_ON = "O";

        public readonly static String POSMODE_POSITIONING = "P";
        public readonly static String POSMODE_RELEASING = "R";

        // REHANDLE_CODE
        public readonly static String REHANDLE_LOADING_CANCEL = "C";
        public readonly static String REHANDLE_EXPORT_RETURN = "R";
        public readonly static String REHANDLE_VERIFIED = "N";

        // STUFF_CHK
        public readonly static String STUFF_CHK_STUFFING = "S";
        public readonly static String STUFF_CHK_STRIPPING = "U";
        public readonly static String STUFF_CHK_PARTIAL_STUFF = "P";
        public readonly static String STUFF_CHK_PARTIAL_STRIP = "F";
        public readonly static String STUFF_CHK_STRIP_EMPTY_LOADING = "M";
        public readonly static String STUFF_CHK_STUFF_FOR_RESTUFF = "R";
        public readonly static String STUFF_CHK_STRIP_FOR_RESTUFF = "T";

        // OVERLAND_CHK
        public readonly static String OVERLAND_CHK_UNEXPECTED = "U";
        public readonly static String OVERLAND_CHK_OPERATIONAL = "O";

        // Container Queue Type
        public readonly static String CNTRQUEUE_TYPE_ROADTRUCK = "ET";
        public readonly static String CNTRQUEUE_TYPE_TRAIN = "TR";

        // RMS by kimjaehoon(2010-05-26)
        public readonly static String RMS_CHK_FORCE = "F";

        // MEDIUM_TYPE (2010-06-09 dsku)
        public readonly static String MEDIUM_TYPE_GATE = "G";
        public readonly static String MEDIUM_TYPE_SERVER = "S";

        // GROUP TYPE
        // 2011-11-05 JaeHoon
        public readonly static String GROUP_TYPE_INTERFACE_IN = "TI";
        public readonly static String GROUP_TYPE_INTERFACE_OUT = "TO";

        //[Auto shift] Mode
        public readonly static String ASMODE_STACK_PRTY = "S";
        public readonly static String ASMODE_TIMEBASED = "T";
        public readonly static String ASMODE_ROLLBACK = "R";
        public readonly static String ASMODE_LAYOUT = "L";
        public readonly static String ASMODE_STACK_PRTY_AND_MIN_MOVEMENT = "SM";
        public readonly static String ASMODE_POSRULE = "P";	// added by Chun (2018.10.24) : 0086810: [TM] > [C3IT Configuration] > [Auto-Shifting] > [Auto-Shifting Mode] add a new option [Pos. Rule Based] 

        // added by jaehoon(2016.09.21) compulsory job creation
        //[Auto shift] Compusory Mode
        public readonly static String AS_COMPULSORY_MODE_NOT_USE = "CN";
        public readonly static String AS_COMPULSORY_MODE_CREATE_JOB = "CC";
        public readonly static String AS_COMPULSORY_MODE_CREATE_JOB_AND_TAKEBACK = "CT";

        //[Auto shift] Stacking Mode
        public readonly static String AUTO_SHIFT_NONSTACK_ONJOB = "N";
        public readonly static String AUTO_SHIFT_STACK_ONJOB = "S";
        public readonly static String AUTO_SHIFT_LOWPRIORITY_ONJOB = "L";

        //[Auto shift] COMEBACK ATTEMPT Mode
        public readonly static String AUTO_SHIFT_COMEBACK_ATTEMPT_NAVER = "N";
        public readonly static String AUTO_SHIFT_COMEBACK_ATTEMPT_TRY_AGAIN = "T";

        // AutoComplete Mode
        public readonly static String AUTO_COMPLETE_GO = "AC_GO";
        public readonly static String AUTO_COMPLETE_LO = "AC_LO";
        public readonly static String AUTO_COMPLETE_RO = "AC_RO";
        public readonly static String AUTO_COMPLETE_YO = "AC_YO";
        public readonly static String AUTO_COMPLETE_VI = "AC_VI";
        public readonly static String AUTO_COMPLETE_RI = "AC_RI";
        public readonly static String AUTO_COMPLETE_GI = "AC_GI";
        public readonly static String AUTO_COMPLETE_SO = "AC_SO"; // added by jaeok (2017.01.11) HMTS_UP Gap ID ITT-019

        // Program Code
        public readonly static String OPERATION_MANAGEMENT = "OM";
        public readonly static String DELIVERY_RESERVATION = "DR";
        public readonly static String DG_CARGO_OPERATION = "DG";
        public readonly static String REEFER_CARGO_OPERATION = "RF";
        public readonly static String BILLING = "BL";
        public readonly static String CFS = "CF";
        public readonly static String CUSTOMS_MANAGEMENT = "CM";
        public readonly static String WEBIP = "IP";
        public readonly static String EDI = "ED";
        public readonly static String BERTH_PLANNING = "BP";
        public readonly static String SHIP_PLANNING = "SP";
        public readonly static String YARD_PLANNING = "YP";
        public readonly static String YARD_DEFINE = "YD";
        public readonly static String VESSEL_DEFINE = "CD";
        public readonly static String TERMINAL_MONITORING = "TM";
        public readonly static String BERTH_MONITORING = "BM";
        public readonly static String GATE = "GT";
        public readonly static String GATE_CHECKER = "GC";
        public readonly static String REEFER_CHECKER = "RE";
        public readonly static String RDT_TALLY = "TA";
        public readonly static String RDT_YQ = "YQ";
        public readonly static String RDT_YT = "YT";
        public readonly static String STATISTICS = "ST";
        public readonly static String SECURITY_MANAGEMENT = "SM";
        public readonly static String C3IT_SERVER = "C3";
        public readonly static String YARD_CHECKER = "YC"; // added by jaeok (2019.06.05) Mantis 90376: [CUSP:N81-N40-YKC-001] Yard Checker

        //added by jaehoon(2012-05-18)
        public readonly static String XIS = "XI";
        public readonly static String AGSI = "AG";

        public readonly static String UNKNOWN_APP = "UNKNOWN APPLICATION";

        //Product Code
        public readonly static String PRODUCT_CODE_CATOS = "CT";
        public readonly static String PRODUCT_CODE_C3IT = "C3";

        //CHAPA CHK  // added by RackHyun Jeong(2012.03.28)
        public readonly static String CHAPA_CHK_CHAPAONLY = "C";
        public readonly static String CHAPA_CHK_ALLUSER = "A";
        public readonly static String CHAPA_CHK_NONE = "N";

        //added by bkkang(2013.07.29)
        public readonly static String GATE_CANCEL_REASON = "GCR";

        //added by jaehoon(2014.07.22)
        public readonly static String POOL_STATE_BOOK_ADDING = "A";
        public readonly static String POOL_STATE_BOOK_DELETING = "D";
        public readonly static String POOL_STATE_WORKING = "W";

        //added by jaehoon(2016.03.02) -- mantis:51740
        public readonly static String ACTIVATE_YARDEQU_JOB = "Y";
        public readonly static String ACTIVATE_YARDEQU_MULTIJOB = "YM";
        public readonly static String ACTIVATE_VESSELEQU_JOB = "V";
        public readonly static String ACTIVATE_BARGEEQU_JOB = "B";
        public readonly static String ACTIVATE_RAILEQU_JOB = "R";

        public readonly static String RETURN_PLACE_TIPC = "KHH1210C";
        public readonly static String RETURN_PLACE_HMTS = "KHH070"; // "KHH0702C";	// modified by Chun (2018.08.28) : 0085538: Import container with Return place like "KHH070%", but system didn't generate "Empty Return" in pre-advice for TSL 
        public readonly static String RETURN_PLACE_APL1 = "KHH0690C"; // added by jaeok (2018.07.26) Mantis 84974: [Common] Movement Type Condition According to POR/FDEST
        public readonly static String RETURN_PLACE_APL2 = "KHH0680C"; // added by jaeok (2018.07.31) Mantis 85055: [In Gate] 'Gate-Out Time' field should be not editable if container is released from APL terminal with Return Place=APL.

        // added by BI.Koo (2018.06.07) : WHL, TM-K-025
        public readonly static String VESSEL_STORAGE = "S";

        // added by Rackhyun Jeong (2018.03.28) - QC OCR
        public readonly static String QCOCR_ACTION_YTCHECK = "Y";
        public readonly static String QCOCR_ACTION_INQUIRY = "I";
        public readonly static String QCOCR_ACTION_CONFIRM = "C";
        public readonly static String QCOCR_ERROR_TYPE_BLOCKING = "BL";
        public readonly static String QCOCR_ERROR_TYPE_NONE_BLOCKING = "NB";

        public readonly static String FIELD_TYPE_FIRE_CODE = "FIRE_CODE";  // added by Rackhyun Jeong (2018.11.20) - GWCT - FIRE_CODE, SV-002

        // added by jaeok (2018.11.27) Mantis 87545: [C3IT] Look Forward (Re-positioning)
        public readonly static String ASGN_STORAGE_YARD_BY_SYSTEM = "AsgnStorageYardBySystem";

        // added by YoungOk Kim (2019.11.11) - Mantis 103773: [YQ] DGPS 좌표를 CATOS에 매핑하여 장비 위치 추적
        // GPS
        public static readonly int GPS_DATA_FORMAT_DMM = 1;	// 3735.0079, 12701.6446
        public static readonly int GPS_DATA_FORMAT_DDD = 2;	// 35.101072, 129.094908
    }
}
