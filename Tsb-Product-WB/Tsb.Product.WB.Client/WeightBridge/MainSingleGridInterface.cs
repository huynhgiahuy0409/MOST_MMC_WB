using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tsb.Fontos.Win.FormTemplate;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.WeightBridge;

namespace Tsb.Product.WB.Client.WeightBridge
{
    public interface MainSingleGridInterface : BaseSingleGridViewInterface
    {
        #region PROPERTY AREA ***************************************
        string StaffCd { get; }
        MainParam MainParam { get; set; }
        MainParam MainGridParam { get; set; }
        JobParam JobParam { get; set; }
        WeightBridgeParam WeightBridgeParam { get; set; }
        /// <summary>
        /// Gets or sets ActiveItem.
        /// </summary>
        WeightInfoItem WeightInfoItem { get; set; }
        WeightInfoItem CurWeightInfoItem { get; set; }

        #endregion

        #region METHOD AREA *****************************************
        /// <summary>
        /// Mandatory Check Function
        /// </summary>
        /// <returns>Message String</returns>
        string SaveMandatoryCheck();

        /// <summary>
        /// Before Search, Mandatory Check.
        /// </summary>
        /// <returns>True or fase after Manadatory Check</returns>
        bool SearchMandatoryCheck();

        bool WeightMandatoryCheck();
        bool CurWeightInfoCheck();

        /// <summary>
        /// Refresh SpreadGrid
        /// </summary>
        void SpreadRefresh();
        #endregion

        #region EVENT AREA ******************************************
        /// <summary>
        /// Occurs before GridRow removed.
        /// </summary>
        //event SpreadGridRowRemovedHandler GridRowRemoved;
        #endregion
    }
}
