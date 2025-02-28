using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Catos.Cm.Mobile.Common.readonlyants
{
    public class YDefineConstants
    {
    /** Bay Direction */
    public static readonly string BAYDIR_LEFT2RIGHT = "L";
	public static readonly string BAYDIR_RIGHT2LEFT = "R";
	
	/** Row Direction */
	public static readonly string ROWDIR_LEFT2RIGHT = "L";
	public static readonly string ROWDIR_RIGHT2LEFT = "R";
	
	/** YT_POS */
	public static readonly string YTPOS_START = "S";
	public static readonly string YTPOS_END = "E";
	public static readonly string YTPOS_BOTH = "B";
	
	/** YT_ENTER */
	public static readonly string YTENTER_START = "S";
	public static readonly string YTENTER_END = "E";
	public static readonly string YTENTER_BOTH = "B";

	/** FAC */
	public static readonly string FAC_SC = "S";
	public static readonly string FAC_RS = "R";
	public static readonly string FAC_TC = "T";
	public static readonly string FAC_FL = "F";
	
	/**
	 * Interchange
	 * @see TB_BLOCK.IC_CHK, TB_AREA.IC_CHK
	 */
	public static readonly string IC_QUAY = "Q";
	public static readonly string IC_YARD = "Y";
	public static readonly string IC_GATE = "G";
	public static readonly string IC_RAIL = "R";
	public static readonly string IC_INTERFACE = "I";

	/** TP */
	public static readonly string TP_HORIZONTAL = "H";
	public static readonly string TP_VERTICAL = "V";

	/**
	 * Facility: YQ parking 
	 */
	public static readonly string FAC_USE_PARKING = "P";
	
	/**
	 * Interchange for Rail Side for temporary
	 * @history by kimjaehoon(2008-12-29)
	 * @comment IC_RAIL_WEST_BLOCK="CL"
	 */
	public static readonly string IC_RAIL_WEST_BLOCK ="CL"; //west side (for empty depot contract)
	/**
	 * Interchange for Rail Side for temporary
	 * @history by kimjaehoon(2008-12-29)
	 * @comment IC_RAIL_EAST_BLOCK="CH"
	 */
	public static readonly string IC_RAIL_EAST_BLOCK ="CH"; //east side
	/**
	 * Interchange for Rail Side for temporary
	 * @history by kimjaehoon(2008-12-29)
	 * @comment IC_RAIL_DISCHARGING_AREA="RD"
	 */
	public static readonly string IC_RAIL_DISCHARGING_AREA ="RD"; //east side
	
	//2011-11-04 JaeHoon
	public static readonly string USAGE_INTER_TERMINAL_MOVEMENT_AREA = "T";
	public static readonly string USAGE_AREA_CFS = "C"; // added by jaeok (2018.05.15) Gap ID: YT-K-005
	public static readonly string USAGE_BLOCK_CFS = "CFS";
	/**
	 * Sidelifter yard define
	 */
	public static readonly string SIDE_LIFTER_GATE_GRID = "16";
	public static readonly string SIDE_LIFTER_INTERCHANGE_BLOCK = "SI";
	public static readonly string SIDE_LIFTER_DROPPING_AREA = "KO";
	public static readonly string SIDE_LIFTER_PICKING_BLOCK = "TO";
	
	/**
	 * Volvo yard define
	 */
	public static readonly string VOLVO_INTERCHANGE_AREA_EMTY = "VU";
	public static readonly string VOLVO_INTERCHANGE_AREA_FULL = "VT";
	
	/**
	 * GHAB requirement to apply auto-plugging only to specific block/area
	 * Should be listed without space, separated by comma
	 * example: KB1,KB2
	 */
    public static readonly string AUTO_PLUGIN_BLOCKAREA_LIST = "KB1,KB2";
    public static readonly string AUTO_PLUGOUT_BLOCKAREA_LIST = "KB1,KB2";
    
    /**
     * GHAB dependent readonlyants defining work group for TICS interface 
     */
    public static readonly string WORK_GROUP_CIA_IN = "[NETSS-IN]";
    public static readonly string WORK_GROUP_CIA_OUT = "[NETSS-OUT]";
    public static readonly string WORK_GROUP_TTIA_IN = "[AVESTA-IN]";
    public static readonly string WORK_GROUP_TTIA_OUT = "[AVESTA-OUT]";
    
    /**
     * GHAB dependent readonlyants defining work group
     */
    public static readonly string WORK_GROUP_EMPTY_IN = "[EMPTY-IN]";
    
    
    /**
     * GOCT XLANE POSITION
     */
    public static readonly string XLANE_AREA = "6000";
    
    public static readonly string STACKING_PRIORITY = "S";
    public static readonly string RELEASE_PRIORITY = "R";
    public static readonly string AUTOSHIFT_PRIORITY = "A";
    
    
    /**
     * For New Position Rule
     */
    
    public static readonly bool AVAILABLE_SLOT = true;
    public static readonly bool NOT_AVAILABLE_SLOT = false;
    }
}
