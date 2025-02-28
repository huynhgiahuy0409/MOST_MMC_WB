using System;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item.UpdateSeal;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Resources;
using Tsb.Fontos.Win.Forms;
using Tsb.Fontos.Win.Keyboard;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;

namespace Tsb.Catos.Cm.Mobile.Win.UpdateSealForTwin
{
    public partial class UpdateSealForTwinView : BaseMobileBizView, UpdateSealForTwinInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-UpdateSealForTwinView";

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }
        
        private UpdateSealForTwinController ThisController
        {
            get { return this.Controller as UpdateSealForTwinController; }
        }

        public bool UseDisableSealNo3 { get; set; }
        public bool VisibleSealChk { get; set; } // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************
        public UpdateSealForTwinView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new UpdateSealForTwinController(this);
            this.InitControl();
            this.AddEventHandler();
        }

        private void InitControl()
        {
            try
            {
                this.lblForeSealNo1.TextResourceKey = "WRD_CTMO_CarrierSeal";
                this.lblForeSealNo2.TextResourceKey = "WRD_CTMO_CustomsSeal";
                this.lblForeSealNo3.TextResourceKey = "WRD_CTMO_AuthoritySeal";
                this.lblForeSealChk.TextResourceKey = "WRD_CTMO_SealPresence"; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

                this.lblAfterSealNo1.TextResourceKey = "WRD_CTMO_CarrierSeal";
                this.lblAfterSealNo2.TextResourceKey = "WRD_CTMO_CustomsSeal";
                this.lblAfterSealNo3.TextResourceKey = "WRD_CTMO_AuthoritySeal";
                this.lblAfterSealChk.TextResourceKey = "WRD_CTMO_SealPresence";

                this.tbxForeCntr1stOfSealNo1.LinkedLabelName = lblForeSealNo1.Name;
                this.tbxForeCntr2ndOfSealNo1.LinkedLabelName = lblForeSealNo1.Name;
                this.tbxForeCntr1stOfSealNo2.LinkedLabelName = lblForeSealNo2.Name;
                this.tbxForeCntr2ndOfSealNo2.LinkedLabelName = lblForeSealNo2.Name;
                this.tbxForeCntr1stOfSealNo3.LinkedLabelName = lblForeSealNo3.Name;
                this.tbxForeCntr2ndOfSealNo3.LinkedLabelName = lblForeSealNo3.Name;

                this.tbxAfterCntr1stOfSealNo1.LinkedLabelName = lblForeSealNo1.Name;
                this.tbxAfterCntr2ndOfSealNo1.LinkedLabelName = lblForeSealNo1.Name;
                this.tbxAfterCntr1stOfSealNo2.LinkedLabelName = lblForeSealNo2.Name;
                this.tbxAfterCntr2ndOfSealNo2.LinkedLabelName = lblForeSealNo2.Name;
                this.tbxAfterCntr1stOfSealNo3.LinkedLabelName = lblForeSealNo3.Name;
                this.tbxAfterCntr2ndOfSealNo3.LinkedLabelName = lblForeSealNo3.Name;

                FormDraggingHandler _formDraggingHdl = new FormDraggingHandler(this.lblTitle, this); // added by YoungOk Kim (2019.05.16) - Mantis 89476: [Tally] 창 이동 기능
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), OBJECT_ID, "MSG_CTCM_00008", null);
            }
        }

        private void AddEventHandler()
        {
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            this.tbxForeCntr1stOfSealNo1.Click += new EventHandler(tbxSealNo_Click);
            this.tbxForeCntr2ndOfSealNo1.Click += new EventHandler(tbxSealNo_Click);
            this.tbxForeCntr1stOfSealNo2.Click += new EventHandler(tbxSealNo_Click);
            this.tbxForeCntr2ndOfSealNo2.Click += new EventHandler(tbxSealNo_Click);
            this.tbxForeCntr1stOfSealNo3.Click += new EventHandler(tbxSealNo_Click);
            this.tbxForeCntr2ndOfSealNo3.Click += new EventHandler(tbxSealNo_Click);

            this.tbxAfterCntr1stOfSealNo1.Click += new EventHandler(tbxSealNo_Click);
            this.tbxAfterCntr2ndOfSealNo1.Click += new EventHandler(tbxSealNo_Click);
            this.tbxAfterCntr1stOfSealNo2.Click += new EventHandler(tbxSealNo_Click);
            this.tbxAfterCntr2ndOfSealNo2.Click += new EventHandler(tbxSealNo_Click);
            this.tbxAfterCntr1stOfSealNo3.Click += new EventHandler(tbxSealNo_Click);
            this.tbxAfterCntr2ndOfSealNo3.Click += new EventHandler(tbxSealNo_Click);
        }

        #endregion INITIALIZATION AREA ****************************************

        #region EVENT HANDLER AREA ********************************************

        protected override void OnShown(EventArgs e)
        {
            try
            {
                // fore cntainer
                if (string.IsNullOrEmpty(ThisController.ForeUpdateSealItem.CntrNo) == false)
                {
                    string str = ThisController.ForeUpdateSealItem.CntrNo;
                    tbxForeCntrNo.Text = str;
                }
                if (string.IsNullOrEmpty(ThisController.ForeUpdateSealItem.SealNo1) == false)
                {
                    string[] str = ThisController.ForeUpdateSealItem.SealNo1.Split(',');
                    tbxForeCntr1stOfSealNo1.Text = str[0];
                    if (str.Length > 1)
                    {
                        tbxForeCntr2ndOfSealNo1.Text = str[1];
                    }
                }
                if (string.IsNullOrEmpty(ThisController.ForeUpdateSealItem.SealNo2) == false)
                {
                    string[] str = ThisController.ForeUpdateSealItem.SealNo2.Split(',');
                    tbxForeCntr1stOfSealNo2.Text = str[0];
                    if (str.Length > 1)
                    {
                        tbxForeCntr2ndOfSealNo2.Text = str[1];
                    }
                }
                if (string.IsNullOrEmpty(ThisController.ForeUpdateSealItem.SealNo3) == false)
                {
                    string[] str = ThisController.ForeUpdateSealItem.SealNo3.Split(',');
                    tbxForeCntr1stOfSealNo3.Text = str[0];
                    if (str.Length > 1)
                    {
                        tbxForeCntr2ndOfSealNo3.Text = str[1];
                    }
                }

                // after cntainer
                if (string.IsNullOrEmpty(ThisController.AfterUpdateSealItem.CntrNo) == false)
                {
                    string str = ThisController.AfterUpdateSealItem.CntrNo;
                    tbxAfterCntrNo.Text = str;
                }
                if (string.IsNullOrEmpty(ThisController.AfterUpdateSealItem.SealNo1) == false)
                {
                    string[] str = ThisController.AfterUpdateSealItem.SealNo1.Split(',');
                    tbxAfterCntr1stOfSealNo1.Text = str[0];
                    if (str.Length > 1)
                    {
                        tbxAfterCntr2ndOfSealNo1.Text = str[1];
                    }
                }
                if (string.IsNullOrEmpty(ThisController.AfterUpdateSealItem.SealNo2) == false)
                {
                    string[] str = ThisController.AfterUpdateSealItem.SealNo2.Split(',');
                    tbxAfterCntr1stOfSealNo2.Text = str[0];
                    if (str.Length > 1)
                    {
                        tbxAfterCntr2ndOfSealNo2.Text = str[1];
                    }
                }
                if (string.IsNullOrEmpty(ThisController.AfterUpdateSealItem.SealNo3) == false)
                {
                    string[] str = ThisController.AfterUpdateSealItem.SealNo3.Split(',');
                    tbxAfterCntr1stOfSealNo3.Text = str[0];
                    if (str.Length > 1)
                    {
                        tbxAfterCntr2ndOfSealNo3.Text = str[1];
                    }
                }

                // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
                if (VisibleSealChk)
                {
                    // fore container
                    if (ThisController.ForeUpdateSealItem.JobCode.Equals(CTBizConstant.QuayJobType.DISCHARGING) ||
                        ThisController.ForeUpdateSealItem.JobCode.Equals(CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_DISCHARGING))
                    {
                        if (string.IsNullOrEmpty(ThisController.ForeUpdateSealItem.SealChk))
                        {
                            rbForeCntrSealChk1.Checked = true;
                        }
                        else
                        {
                            if (ThisController.ForeUpdateSealItem.SealChk.Equals("Y"))
                            {
                                rbForeCntrSealChk1.Checked = true;
                            }
                            else
                            {
                                rbForeCntrSealChk2.Checked = true;
                            }
                        }
                    }
                    else
                    {
                        pnlForeCntrSealChk.Enabled = false;

                        if (string.IsNullOrEmpty(ThisController.ForeUpdateSealItem.SealNo2))
                        {
                            pnlForeCntrSealChk.Visible = false;
                        }
                    }

                    // after container
                    if (ThisController.AfterUpdateSealItem.JobCode.Equals(CTBizConstant.QuayJobType.DISCHARGING) ||
                        ThisController.AfterUpdateSealItem.JobCode.Equals(CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_DISCHARGING))
                    {
                        if (string.IsNullOrEmpty(ThisController.AfterUpdateSealItem.SealChk))
                        {
                            rbAfterCntrSealChk1.Checked = true;
                        }
                        else
                        {
                            if (ThisController.AfterUpdateSealItem.SealChk.Equals("Y"))
                            {
                                rbAfterCntrSealChk1.Checked = true;
                            }
                            else
                            {
                                rbAfterCntrSealChk2.Checked = true;
                            }
                        }
                    }
                    else
                    {
                        pnlAfterCntrSealChk.Enabled = false;

                        if (string.IsNullOrEmpty(ThisController.AfterUpdateSealItem.SealNo2))
                        {
                            pnlAfterCntrSealChk.Visible = false;
                        }
                    }
                }
                else
                {
                    pnlForeCntrSealChk.Visible = false;
                    pnlAfterCntrSealChk.Visible = false;
                }

                if (UseDisableSealNo3)
                {
                    this.pnlForeCntrSealNo3.Visible = false;
                    this.pnlAfterCntrSealNo3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSealItem foreItem = new UpdateSealItem();
                UpdateSealItem afterItem = new UpdateSealItem();

                foreItem.CntrNo = this.tbxForeCntrNo.Text;
                foreItem.SealNo1 = JoinSealNoValues(tbxForeCntr1stOfSealNo1.Text, tbxForeCntr2ndOfSealNo1.Text);
                foreItem.SealNo2 = JoinSealNoValues(tbxForeCntr1stOfSealNo2.Text, tbxForeCntr2ndOfSealNo2.Text);
                foreItem.SealNo3 = JoinSealNoValues(tbxForeCntr1stOfSealNo3.Text, tbxForeCntr2ndOfSealNo3.Text);
                foreItem.SealChk = rbForeCntrSealChk1.Checked ? "Y" : "N"; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

                string afterCntrNo = this.tbxAfterCntrNo.Text;
                afterItem.SealNo1 = JoinSealNoValues(tbxAfterCntr1stOfSealNo1.Text, tbxAfterCntr2ndOfSealNo1.Text);
                afterItem.SealNo2 = JoinSealNoValues(tbxAfterCntr1stOfSealNo2.Text, tbxAfterCntr2ndOfSealNo2.Text);
                afterItem.SealNo3 = JoinSealNoValues(tbxAfterCntr1stOfSealNo3.Text, tbxAfterCntr2ndOfSealNo3.Text);
                afterItem.SealChk = rbAfterCntrSealChk1.Checked ? "Y" : "N";

                // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
                if (VisibleSealChk == false)
                {
                    foreItem.SealChk = string.Empty;
                    afterItem.SealChk = string.Empty;
                }
                
                ThisController.UpdateSeal(foreItem, afterItem);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        private void tbxSealNo_Click(object sender, EventArgs e)
        {
            try
            {
                ShowKeyboard(sender);
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        #endregion EVENT HANDLER AREA *****************************************

        #region METHOD AREA ***************************************************

        private void ShowKeyboard(object sender)
        {
            try
            {
                if (sender is TTextBox)
                {
                    TTextBox textBox = sender as TTextBox;

                    // added by YoungOk Kim (2019.03.05) - Mantis 89291: Seal no. typing default value
                    string orgSealNo = textBox.Text;
                    orgSealNo = string.IsNullOrEmpty(orgSealNo) ? string.Empty : orgSealNo.Trim();

                    // modified by YoungOk Kim (2018.07.23) - for WHL. The maximum value is 20.
                    //KeyboardManagerFactory.GetInstance(KeyboardTypes.Virtual).Show(textBox);
                    string keyBoardTitle = ResourceFactory.GetResource().GetLabel(textBox.LinkedLabel.TextResourceKey);
                    string sealNo = KeyboardManagerFactory.GetInstance(KeyboardTypes.Virtual).Show(keyBoardTitle, orgSealNo, 20);
                    textBox.Text = sealNo;
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), OBJECT_ID, "MSG_CTCM_00008", null);
            }
        }

        private string JoinSealNoValues(string first, string second)
        {
            string result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(first) == false && string.IsNullOrEmpty(second) == false)
                {
                    result = string.Join(",", new string[] { first, second });
                }
                else if (string.IsNullOrEmpty(first) == false && string.IsNullOrEmpty(second))
                {
                    result = first;
                }
                else if (string.IsNullOrEmpty(first) && string.IsNullOrEmpty(second) == false)
                {
                    result = second;
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), OBJECT_ID, "MSG_CTCM_00008", null);
            }
            return result;
        }

        #endregion METHOD AREA ************************************************

        private void tLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}
