using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Service;
using Tsb.Product.WB.Common.Constance;
using static Tsb.Product.WB.Common.Constance.WeightBridgeConstance;

namespace Tsb.Product.WB.Common.ServiceSpec
{
    public class ProductWBServiceSpec : BaseServiceSpec
    {
        public readonly static String SAMPLE_SINGLE_GRID_SERVICE = "SVC-PT-PTWB-SPL-SingleGridService";
        public readonly static String WEIGHTBRIDGE_MAIN_SERVICE = "SVC-PT-PTWB-WB-MainService";
        public readonly static String WEIGHTBRIDGE_JOB_SERVICE = "SVC-PT-PTWB-WB-JobService";
        public readonly static String POPUP_TRUCK_LIST_POPUP_SERVICE = "SVC-PT-PTWB-POP-TruckListPopupService";
        public readonly static String WEIGHTBRIDGE_WB_SERVICE = "SVC-PT-PTWB-WB-WeightBridgeService";
        public readonly static MassUnit APP_MASS_UNIT = MassUnit.KG;
        public readonly static MassUnit DB_MASS_UNIT = MassUnit.TON;
        public ProductWBServiceSpec()
            : base()
        {
        }
    }
}
