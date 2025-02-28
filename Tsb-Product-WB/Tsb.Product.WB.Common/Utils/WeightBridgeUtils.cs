using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Product.WB.Common.Constance;
using Tsb.Product.WB.Common.ServiceSpec;
using static Tsb.Product.WB.Common.Constance.WeightBridgeConstance;

namespace Tsb.Product.WB.Common.Utils
{
    public class WeightBridgeUtils
    {
        public static decimal? ConvertWeight(decimal? weight, int mode)
        {
            if (weight == null) return null;
            decimal? result = 0;
            if (ProductWBServiceSpec.APP_MASS_UNIT.Equals(MassUnit.KG) && ProductWBServiceSpec.DB_MASS_UNIT.Equals(MassUnit.TON))
            {
                /* mode is 1 to map value from App to DB and else case is opposite */
                if(mode == 1)
                {
                    result = weight / 1000;
                }
                else
                {
                    result = weight * 1000;
                }
            }
            if (ProductWBServiceSpec.APP_MASS_UNIT.Equals(MassUnit.TON) && ProductWBServiceSpec.DB_MASS_UNIT.Equals(MassUnit.TON))
            {
                result = weight;
            }
            return result;
        }
       
    }
}
