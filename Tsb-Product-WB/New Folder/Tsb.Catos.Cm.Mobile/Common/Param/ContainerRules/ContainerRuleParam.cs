using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Common.Param.ContainerRules
{
    public class ContainerRuleParam : BaseParam
    {

        public readonly static string TRAN_VESSEL_IN = "VIN";
        public readonly static string TRAN_VESSEL_OUT = "VOT";
        public readonly static string TRAN_GATE_IN = "GIN";
        public readonly static string TRAN_GATE_OUT = "GOT";
        public readonly static string TRAN_YARD_IN = "YIN";
        public readonly static string TRAN_YARD_OUT = "YOT";

        public readonly static string TIMING_CREATE = "CRE";
        public readonly static string TIMING_INQUIRY = "INQ";
        public readonly static string TIMING_CONFIRM = "CON";

        public readonly static string KEY_TABLE_COPARN = "TB_COPARN";
        public readonly static string KEY_TABLE_RESERVE = "TB_RESERVE";
        public readonly static string KEY_TABLE_INVENTORY = "TB_INVENTORY";

        public readonly static string KEY_FIELD_CNTR_NO = "CNTR_NO";
        public readonly static string KEY_FIELD_CNTR_ID = "CNTR_ID";
        public readonly static string KEY_FIELD_SIC_NO = "SIC_NO";

        #region PROPERTY

        public string TransactionType { get; set; }
        public string PgmCode { get; set; }
        public string Timing { get; set; }
        public string KeyField { get; set; }
        public string KeyTable { get; set; }
        public string KeyValue { get; set; }
        public string ParamId { get; set; }

        #endregion

        #region INITIALIZATION

        private readonly string OBJECT_ID = "PAR-CT-CTMO-CNTR-ContainerRuleParam";

        public ContainerRuleParam(object paramOwner)
            : base(paramOwner)
        {
            ObjectID = OBJECT_ID;
        }

        public ContainerRuleParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            ObjectID = OBJECT_ID;
        }

        #endregion
    }
}
