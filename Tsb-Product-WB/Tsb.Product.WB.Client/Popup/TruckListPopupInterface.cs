using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Win.Grid.Event;
using Tsb.Product.WB.Common.Item.Popup;
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Param.Popup;
using Tsb.Product.WB.Common.Param.Sample;

namespace Tsb.Most.Wb.Client.Popup
{
    interface TruckListPopupInterface : BaseSingleGridViewInterface
    {

        #region PROPERTY AREA ***************************************
        /// <summary>
        /// Gets FormsName.
        /// </summary>
        string FormName { get; }

        /// <summary>
        /// Gets or sets GridForeColor.
        /// </summary>
        Color GridForeColor { get; set; }

        /// <summary>
        /// Gets or sets GridBackColor.
        /// </summary>
        Color GridBackColor { get; set; }

        /// <summary>
        /// Gets or sets SearchParam.
        /// </summary>
        TruckListPopupParam SearchParam { get; set; }

        /// <summary>
        /// Gets or sets ActiveItem.
        /// </summary>
        TruckListPopupItem ActiveItem { get; set; }
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

        /// <summary>
        /// Refresh SpreadGrid
        /// </summary>
        void SpreadRefresh();
        #endregion

        #region EVENT AREA ******************************************
        /// <summary>
        /// Occurs before GridRow removed.
        /// </summary>
        event SpreadGridRowRemovedHandler GridRowRemoved;
        #endregion
    }
}
