#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2016 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		REVISION    	
* 2016.09.21    JIndols 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.Configuration.Provider;
using System.IO;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Message;
using System.Windows.Forms;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Message Policy Information class
    /// </summary>
    [Serializable]
    public class GridPolicyInfo : BaseEnvironmentInfo
    {
        #region FIELD/PROPERTY AREA**********************************
        private static GridPolicyInfo _instance = null;

        public const string DEFAULT_FILE_NAME = "GridPolicyInfo.xml";
        public const string XML_ATT_VALUE_SORT_TYPE = "SORT_TYPE";
        public const string XML_ATT_VALUE_HEADER_SORT = "HeaderSort";
        public const string XML_ATT_VALUE_DEFAULT_SORT = "DefaultSort";

        public const string XML_ATT_VALUE_COPY_TYPE = "COPY_TYPE";
        public const string XML_ATT_VALUE_CELL_TEXT_COPY = "CellTextCopy";

        public const string XML_ATT_VALUE_GENERAL = "GENERAL";
        public const string XML_ATT_VALUE_RETAIN_ACTIVEROW_SELECTION = "IsRetainActiveRowSelection";
        public const string XML_ATT_VALUE_AUTOFILL_ON_DOUBLECLICK = "UseAutoFillOnDoubleClick";

        public const string XML_ATT_VALUE_SUMMARY = "SUMMARY";
        public const string XML_ATT_VALUE_GRID_SUMMARY_SELECTION = "UseGridSummarySelection";
        public const string XML_ATT_VALUE_GRID_SUMMARY_CONTEXT = "UseGridSummaryContext";
        public const string XML_ATT_VALUE_GRID_SUMMARY_COMBOBOXFILTER = "UseGridSummaryComboboxFilter";
        public const string XML_ATT_VALUE_SUMMARY_DB = "UseSummaryDB";
        public const string XML_ATT_VALUE_GRID_SUMMARY_DATA_DETAIL = "UseGridSummaryDataDetail";

        public const string XML_ATT_VALUE_RENDERER = "RENDERER";
        public const string XML_ATT_VALUE_RENDERER_CELL = "CellRenderer";

        /// <summary>
        /// Gets or sets the sorting type of grid header.
        /// </summary>
        public GridSortTypes SortType { get; private set; }

        /// <summary>
        /// Gets or sets the copying type of grid cell text.
        /// </summary>
        public GridCellTextCopyTypes CellTextCopyTypes { get; private set; }

        /// <summary>
        /// Gets or sets whether retains active row selection in grid after auto sorted.
        /// </summary>
        public bool IsRetainActiveRowSelection { get; private set; }

        public bool UseAutoFillOnDoubleClick { get; private set; }

        public bool UseGridSummarySelection { get; private set; }
        public bool UseGridSummaryContext { get; private set; }
        public bool UseGridSummaryComboboxFilter { get; private set; }

        public bool UseGridSummaryDataDetail { get; private set; }

        public bool UseGridDefaultSort { get; private set; }

        public GridCellRendererTypes CellGridCellRendererType { get; private set; }
        #endregion

        #region INITIALIZATION AREA *********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        private GridPolicyInfo()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-GridPolicyInfo";
            UseGridSummaryComboboxFilter = false;
        }

        /// <summary>
        /// Gets Grid Policy information object reference.
        /// </summary>
        /// <returns>Grid Policy information object reference</returns>
        public static GridPolicyInfo GetInstance()
        {
            try
            {
                if (_instance == null)
                {
                    _instance = new GridPolicyInfo();
                    _instance.SortType = GridSortTypes.NullLast;
                    _instance.IsRetainActiveRowSelection = false;
                    _instance.CellTextCopyTypes = GridCellTextCopyTypes.Description;
                    _instance.CellGridCellRendererType = GridCellRendererTypes.Renderer4;
                    _instance.LoadGridPolicyInfo();
                }
            }
            catch (TsbBaseException tex)
            {
                MessageBox.Show(MessageBuilder.BuildMessage(tex));
                GeneralLogger.Error(tex);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        
            return _instance;
        }
        #endregion

        #region METHOD AREA (LOAD INFORMATION)******************
        /// <summary>
        /// Load grid policy info
        /// </summary>
        public void LoadGridPolicyInfo()
        {
            string sortTypeValue = string.Empty;
            string applyDefaultSortValue = string.Empty;
            string copyTypeValue = string.Empty;
            string retainActiveRowSelectionValue = string.Empty;
            string useAutoFillOnDoubleClick = string.Empty;
            string useGridSummarySelection = string.Empty;
            string useGridSummaryContext = string.Empty;
            string useGridSummaryComboboxFilter = string.Empty;
            String useGridSummaryDataDetail = string.Empty;

            XmlConfigProvider configProvider = null;

            try
            {                
                if (String.IsNullOrEmpty(AppPathInfo.FILE_NAME_GRIDPOLICY_INFO))
                {
                    //If App.config file do not exist file name about grid policy information.
                    this._sourcePath = Path.Combine(AppPathInfo.PATH_ROOT_ENVIRONMENT, GridPolicyInfo.DEFAULT_FILE_NAME);

                    if (File.Exists(this._sourcePath) == false)
                    {
                        return;
                    }
                }
                else
                {
                    //If App.config file do exist file name about grid policy information.
                    this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_ENVIRONMENT, AppPathInfo.FILE_NAME_GRIDPOLICY_INFO);

                    if (File.Exists(this._sourcePath) == false)
                    {
                        //MSG:{0} does not exist. Please check {1}.
                        throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00121",
                             DefaultMessage.NON_REG_WRD + this._sourcePath,
                            "WRD_FTCO_thisfile"
                            );
                    }
                }
            }
            catch (System.TypeInitializationException initEx)
            {
                if (initEx.InnerException is TsbBaseException)
                {
                    TsbBaseException tsbEx = initEx.InnerException as TsbBaseException;
                    ExceptionHandler.Replace(initEx, initEx.InnerException.GetType(), tsbEx.SourceObjectID, tsbEx.MsgCode, tsbEx.MsgArgs);
                }
                else
                {
                    //MSG:An error occurred when checking the configuration path
                    ExceptionHandler.Wrap(initEx, typeof(TsbSysConfigException), this.ObjectID, "MSG_FTCO_00005", null);
                }
            }

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(this._sourcePath);

                //Sets the sorting type.
                sortTypeValue = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_SORT_TYPE, GridPolicyInfo.XML_ATT_VALUE_HEADER_SORT);

                if (sortTypeValue != string.Empty)
                {
                    this.SortType = this.GetValidType<GridSortTypes>(sortTypeValue, GridPolicyInfo.XML_ATT_VALUE_SORT_TYPE, GridPolicyInfo.XML_ATT_VALUE_HEADER_SORT);
                }

                
                //======================================
                //Grid에 Default Sort 기능 적용 유무.
                applyDefaultSortValue = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_SORT_TYPE, GridPolicyInfo.XML_ATT_VALUE_DEFAULT_SORT);

                if (applyDefaultSortValue != string.Empty)
                {
                    this.UseGridDefaultSort = Tsb.Fontos.Core.Util.Type.ConvertUtil.ToBoolean(applyDefaultSortValue);
                }
                //======================================

                copyTypeValue = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_COPY_TYPE, GridPolicyInfo.XML_ATT_VALUE_CELL_TEXT_COPY);

                if (copyTypeValue != string.Empty)
                {
                    this.CellTextCopyTypes = this.GetValidType<GridCellTextCopyTypes>(copyTypeValue, GridPolicyInfo.XML_ATT_VALUE_COPY_TYPE, GridPolicyInfo.XML_ATT_VALUE_CELL_TEXT_COPY);
                }

                retainActiveRowSelectionValue = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_GENERAL, GridPolicyInfo.XML_ATT_VALUE_RETAIN_ACTIVEROW_SELECTION);

                if (retainActiveRowSelectionValue != string.Empty)
                {
                    this.IsRetainActiveRowSelection = Tsb.Fontos.Core.Util.Type.ConvertUtil.ToBoolean(retainActiveRowSelectionValue);
                }

                useAutoFillOnDoubleClick = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_GENERAL, GridPolicyInfo.XML_ATT_VALUE_AUTOFILL_ON_DOUBLECLICK);

                if (useAutoFillOnDoubleClick != string.Empty)
                {
                    this.UseAutoFillOnDoubleClick = Tsb.Fontos.Core.Util.Type.ConvertUtil.ToBoolean(useAutoFillOnDoubleClick);
                }

                useGridSummarySelection = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_SUMMARY, GridPolicyInfo.XML_ATT_VALUE_GRID_SUMMARY_SELECTION);

                if (useGridSummarySelection != string.Empty)
                {
                    this.UseGridSummarySelection = Tsb.Fontos.Core.Util.Type.ConvertUtil.ToBoolean(useGridSummarySelection);
                }

                useGridSummaryContext = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_SUMMARY, GridPolicyInfo.XML_ATT_VALUE_GRID_SUMMARY_CONTEXT);

                if (useGridSummaryContext != string.Empty)
                {
                    this.UseGridSummaryContext = Tsb.Fontos.Core.Util.Type.ConvertUtil.ToBoolean(useGridSummaryContext);
                }

                useGridSummaryComboboxFilter = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_SUMMARY, GridPolicyInfo.XML_ATT_VALUE_GRID_SUMMARY_COMBOBOXFILTER);

                if (useGridSummaryComboboxFilter != string.Empty)
                {
                    this.UseGridSummaryComboboxFilter = Tsb.Fontos.Core.Util.Type.ConvertUtil.ToBoolean(useGridSummaryComboboxFilter);
                }

                useGridSummaryDataDetail = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_SUMMARY, GridPolicyInfo.XML_ATT_VALUE_GRID_SUMMARY_DATA_DETAIL);

                if (useGridSummaryDataDetail != string.Empty)
                {
                    this.UseGridSummaryDataDetail = Tsb.Fontos.Core.Util.Type.ConvertUtil.ToBoolean(useGridSummaryDataDetail);
                }

                //RENDERER
                string cellRenderTypeValue = this.GetValidValueNotException(ref configProvider, GridPolicyInfo.XML_ATT_VALUE_RENDERER, GridPolicyInfo.XML_ATT_VALUE_RENDERER_CELL);

                if (cellRenderTypeValue != string.Empty)
                {

                    this.CellGridCellRendererType = this.GetValidType<GridCellRendererTypes>(cellRenderTypeValue, GridPolicyInfo.XML_ATT_VALUE_RENDERER, GridPolicyInfo.XML_ATT_VALUE_RENDERER_CELL);
                }

            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }

        #endregion

        public static bool IsNewRender()
        {
            if(GridPolicyInfo.GetInstance().CellGridCellRendererType == GridCellRendererTypes.Renderer14)
            {
                return true;
            }

            return false;
        }
    }

    public enum GridSortTypes
    {
        Normal,
        NullLast
        //NullLastStringNumeric
    }

    public enum GridCellTextCopyTypes
    {
        Normal,
        Description
    }

    public enum GridCellRendererTypes
    {
        Renderer4,
        Renderer14
    }
}
