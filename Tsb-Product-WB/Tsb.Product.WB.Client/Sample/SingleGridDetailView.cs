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
* 2010.07.30  Tonny.Kim 1.0	First release.
* 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.Codes.Item;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Fontos.Core.Codes;
using Tsb.Fontos.Core.Configuration.Common;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Message;
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Param.Sample;
using Tsb.Fontos.Win.Forms;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Win.Message;

namespace Tsb.Product.WB.Client.Sample
{
    public partial class SingleGridDetailView : BaseDetailView, SingleGridDetailInterface
    {
        #region FIELDS AREA *****************************************
        private SingleGridItem _detailItem;
        #endregion

        #region PROPERTY/DELEGATE AREA ******************************
        /// <summary>
        /// Gets FormName.
        /// </summary>
        public string FormName { get { return this.Name; } }

        /// <summary>
        /// Gets or sets SearchParam.
        /// </summary>
        public SingleGridParam SearchParam { get; set; }

        /// <summary>
        /// Gets or sets DetaiItem.
        /// </summary>
        public SingleGridItem DetailItem
        {
            get
            {
                return this._detailItem;
            }
            set
            {
                this._detailItem = value;
                bdsDetailItem.DataSource = this._detailItem;

                if (this._detailItem.OpCode == OpCodes.CREATE)
                {
                    this.ClearView();
                }
            }
        }
        #endregion

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize Form.
        /// </summary>
        public SingleGridDetailView()
            : this(null)
        {
        }

        /// <summary>
        /// Initialize Form.
        /// </summary>
        /// <param name="mdiPrnt">Parent Form</param>
        public SingleGridDetailView(Form mdiPrnt)
            : base()
        {
            this.InitViewCreate(mdiPrnt);
        }

        /// <summary>
        /// Initialize Form.
        /// </summary>
        /// <param name="mdiPrnt">Parent Form</param>
        private void InitViewCreate(Form mdiPrnt)
        {
            InitializeComponent();
            this.MdiParent = mdiPrnt;
            this.Controller = new SingleGridDetailController(this);
            this.SearchParam = new SingleGridParam(this);
            this.SetBindingControls();
            this.AddEventHandler();
            this.SetCombo(); // Set up a ComboBox
        }

        #endregion

        #region EVENT HANDLER AREA **********************************
        #endregion

        #region METHOD AREA *****************************************
        /// <summary>
        /// Adds Event Handler
        /// </summary>
        private void AddEventHandler()
        {
        }

        /// <summary>
        /// Binding of Control with Search Parameter
        /// </summary>
        private void SetBindingControls()
        {
            try
            {
                bdsDetailItem.DataSource = typeof(SingleGridItem);

                tbxUnid.CreateBinding<SingleGridItem>(bdsDetailItem);
                tbxUnno.CreateBinding<SingleGridItem>(bdsDetailItem);
                tbxIMDG.CreateBinding<SingleGridItem>(bdsDetailItem, tbx => tbx.Class);
                tbxProperShippingName.CreateBinding<SingleGridItem>(bdsDetailItem, tbx => tbx.ProperShipNm);
                tbxSubsidiaryRisk.CreateBinding<SingleGridItem>(bdsDetailItem, tbx => tbx.SubsidiaryRisk);
                cmbMarinePollutant.CreateBinding<SingleGridItem>(bdsDetailItem, cmb => cmb.MarinePollut);
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }
        }

        /// <summary>
        /// Set up a ComboBox
        /// </summary>
        /// <param name="item"></param>
        private void SetCombo()
        {
            FillCombItem(cmbMarinePollutant, CTBizConstant.CodeType.MARINE_POLLUT);
        }

        /// <summary>
        /// Set of ComboBox with BizRule
        /// </summary>
        /// <param name="comboBox">TComboBox</param>
        /// <param name="codeType">CodeType</param>
        /// <param name="args">Arguments</param>
        private void FillCombItem(TComboBox comboBox, string codeType, params string[] args)
        {
            IList<CodeGeneralItem> codeItemList = CodeManager.GetCodes<CodeGeneralItem>(codeType, args);
            codeItemList.Insert(0, new CodeGeneralItem());
            comboBox.DisplayMember = CodeConstant.PROP_NAME_CODE_NAME;
            comboBox.ValueMember = CodeConstant.PROP_NAME_CODE;
            comboBox.DataSource = codeItemList;
        }

        /// <summary>
        /// ToolbarAttribute [New]
        /// </summary>
        private void ClearView()
        {
            this.SearchParam = new SingleGridParam(this);
        }
        #endregion

        #region DetailInterface Implements AREA *********************
        /// <summary>
        /// Save Mandatory Check
        /// </summary>
        /// <returns></returns>
        public string SaveMandatoryCheck()
        {
            return base.MandatoryCheck(pnlMain);
        }
        #endregion
    }
}
