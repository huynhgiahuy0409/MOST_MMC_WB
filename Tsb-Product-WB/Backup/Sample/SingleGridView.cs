/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2012 TOTAL SOFT BANK LIMITED. All Rights
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
* 2011.01.25  Tonny.Kim 1.0	First release.
* 
*/
using FarPoint.Win.Spread;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.Codes.Param;
using Tsb.Catos.Cm.Win.Grid;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Tp.Common.Item.Sample;
using Tsb.Fontos.Tp.Common.Param.Sample;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Win.Grid.Event;
using Tsb.Fontos.Win.Grid.Schema;
using Tsb.Fontos.Win.Grid.Spread;
using Tsb.Fontos.Win.Menu.ContextMenu;
using Tsb.Fontos.Win.Message;

namespace Tsb.Product.WB.Client.Sample
{
    public partial class SingleGridView : BaseSingleGridView, SingleGridInterface
    {
        #region FIELDS/READONLY AREA ********************************
        private TSpreadGrid _spreadGrid;
        private Color _gridForeColor;
        private Color _gridBackColor;
        private readonly string VALUE_COLUMN_NAME = "ValueColor";
        #endregion

        #region PROPERTY/DELEGATE AREA ******************************
        /// <summary>
        /// Gets FormsName.
        /// </summary>
        public string FormName { get { return this.Name; } }

        /// <summary>
        /// Gets or sets SearchParam.
        /// </summary>
        public SingleGridParam SearchParam { get; set; }

        /// <summary>
        /// Occurs before GridRow removed.
        /// </summary>
        public event SpreadGridRowRemovedHandler GridRowRemoved;

        /// <summary>
        /// Gets or sets ActiveItem.
        /// </summary>
        public SingleGridItem ActiveItem { get; set; }

        /// <summary>
        /// Gets or sets GridForeColor.
        /// </summary>
        public Color GridForeColor
        {
            get { return this._gridForeColor; }
            set
            {
                this._gridForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets GridBackColor.
        /// </summary>
        public Color GridBackColor
        {
            get { return this._gridBackColor; }
            set
            {
                this._gridBackColor = value;
            }
        }
        #endregion

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize Form
        /// </summary>
        public SingleGridView()
            : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize Form
        /// </summary>
        /// <param name="mdiPrnt">Parent Form</param>
        public SingleGridView(Form mdiPrnt)
            : base()
        {
            this.InitViewCreate(mdiPrnt);
        }

        /// <summary>
        /// Initialize Form
        /// </summary>
        /// <param name="mdiPrnt">Parent Form</param>
        private void InitViewCreate(Form mdiPrnt)
        {
            InitializeComponent();
            this.MdiParent = mdiPrnt;
            this.Grid = this.grd_Sample_SingleGrid;
            this.Controller = new SingleGridController(this);

            this._spreadGrid = this.grd_Sample_SingleGrid;
            this._spreadGrid.ActiveSheet.FrozenColumnCount = 3;
            this.SearchParam = new SingleGridParam(this);

            this.SetBindingControls();
            this.AddEventHandler();
        }
        #endregion

        #region EVENT HANDLER AREA **********************************
        /// <summary>
        /// Occurs when Enterkey is Pressed in SpreadGrid's cell.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        private void SpreadGrid_GridCellEnterKeyPressed(object sender, GridCellEnterKeyPressedEventArgs e)
        {
            if (this._spreadGrid.ActiveSheet.ActiveCell.Locked)
            {
                return;
            }

            bool IsBizRuleColumn = false;
            CodeGeneralParam codeDataParam = null;
            CTGridBizRule bizRule = null;

            try
            {
                bizRule = new CTGridBizRule(this._spreadGrid, this.MdiParent);
                codeDataParam = new CodeGeneralParam(this);

                IsBizRuleColumn = bizRule.HandleGridCellEnterKeyPressed(codeDataParam, e);

                if (!IsBizRuleColumn)
                {
                    ContextMenuHelper.GetInstance().Show(Cursor.Position);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }

            return;
        }

        /// <summary>
        /// Occurs when mouse's right button is clicked in SpreadGrid's cell.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        private void SpreadGrid_GridMouseRightClick(object sender, GridMouseRightClickEventArgs e)
        {
            if (this._spreadGrid.ActiveSheet.ActiveCell.Locked)
            {
                return;
            }

            bool IsBizRuleColumn = false;
            CodeGeneralParam codeDataParam = null;
            CTGridBizRule bizRule = null;

            try
            {
                bizRule = new CTGridBizRule(this._spreadGrid, this.MdiParent);
                codeDataParam = new CodeGeneralParam(this);

                IsBizRuleColumn = bizRule.HandleGridRightClick(codeDataParam, e);

                if (!IsBizRuleColumn)
                {
                    ContextMenuHelper.GetInstance().Show(Cursor.Position);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }

            return;
        }

        /// <summary>
        /// SpreadSheet's selection changed event handler
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        public void SpreadGrid_SelectionChangedSpdGrid(object sender, SelectionChangedEventArgs e)
        {
            this.ActiveGridChanged(sender);
        }

        private void ActiveGridChanged(object sender)
        {
            TSpreadGrid currentSpreadGrid = sender as TSpreadGrid;
            int rowIndex = SpreadGridUtil.GetActiveDataRowIndex(this._spreadGrid.ActiveSheet);

            if ((_spreadGrid.DataSource == null) || (rowIndex < 0))
            {
                return;
            }

            SingleGridItem item = this.SourceItemList[rowIndex] as SingleGridItem;

            if (item != null)
            {
                this.ActiveItem = item;
                Debug.WriteLine("rowIndex : " + rowIndex + ", Unid : " + item.Unid);
            }
        }
        #endregion

        #region METHOD AREA *****************************************
        /// Adds Event Handler
        private void AddEventHandler()
        {
            this._spreadGrid.SelectionChanged += new SelectionChangedEventHandler(SpreadGrid_SelectionChangedSpdGrid);
            this._spreadGrid.GridMouseRightClick += new GridMouseRightClickHandler(SpreadGrid_GridMouseRightClick);
            this._spreadGrid.GridCellEnterKeyPressed += new GridCellEnterKeyPressedHandler(SpreadGrid_GridCellEnterKeyPressed);
            if (this.GridRowRemoved != null)
            {
                this._spreadGrid.SpreadGridRowRemoved += this.GridRowRemoved; // It is used by Controller.
            }
        }

        /// <summary>
        /// Search Parameter of Control Binding 
        /// </summary>
        private void SetBindingControls()
        {
            bdsSearchParam.DataSource = typeof(SingleGridParam);

            tbxImdgClass.CreateBinding<SingleGridParam>(bdsSearchParam);
            tbxUnno.CreateBinding<SingleGridParam>(bdsSearchParam);

            bdsSearchParam.DataSource = this.SearchParam;
        }
        #endregion

        #region SingleGridInterface Implements AREA *****************
        /// <summary>
        /// Before Search, Mandatory Check.
        /// </summary>
        /// <returns>True or fase after Manadatory Check</returns>
        public bool SearchMandatoryCheck()
        {
            this.Validate();

            string strMandatory = base.MandatoryCheck(gbxDisplayOption);

            if (!string.IsNullOrEmpty(strMandatory))
            {
                MessageManager.Show("MSG_CTCM_00000", MessageBoxButtons.OK, MessageBoxIcon.Warning, DefaultMessage.NON_REG_WRD + strMandatory);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Mandatory Check Function
        /// </summary>
        /// <returns>Message String</returns>
        public string SaveMandatoryCheck()
        {
            return this._spreadGrid.GridHelper.CheckMandatory();
        }

        /// <summary>
        /// Call to Toolbar of Delete
        /// </summary>
        public void DeleteRow(object sender, EventArgs e)
        {
            this._spreadGrid.DeleteRow();
        }

        /// <summary>
        /// Refresh SpreadGrid
        /// </summary>
        public void SpreadRefresh()
        {
            this._spreadGrid.DataSource = null;
        }
        #endregion
    }
}
