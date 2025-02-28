using System;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Resources;
using Tsb.Fontos.Win.Forms;
using Tsb.Fontos.Win.Keyboard;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;
using Tsb.Catos.Cm.Core.Constants;

namespace Tsb.Catos.Cm.Mobile.Win.UpdateSeal
{
    public partial class UpdateSealView : BaseMobileBizView, UpdateSealInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-UpdateSealView";
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************
        
        public string FormName
        {
            get { return Name; }
        }
        
        private UpdateSealController ThisController
        {
            get { return this.Controller as UpdateSealController; }
        }

        public bool UseDisableSealNo3 { get; set; }
        public bool VisibleSealChk { get; set; } // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************
        public UpdateSealView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new UpdateSealController(this);
            this.InitControl();
            this.AddEventHandler();
        }

        private void InitControl()
        {
            try
            {
                this.lblSealNo1.TextResourceKey = "WRD_CTMO_CarrierSeal";
                this.lblSealNo2.TextResourceKey = "WRD_CTMO_CustomsSeal";
                this.lblSealNo3.TextResourceKey = "WRD_CTMO_AuthoritySeal";
                this.lblSealChk.TextResourceKey = "WRD_CTMO_SealPresence"; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

                this.tbx1stOfSealNo1.LinkedLabelName = lblSealNo1.Name;
                this.tbx2ndOfSealNo1.LinkedLabelName = lblSealNo1.Name;
                this.tbx1stOfSealNo2.LinkedLabelName = lblSealNo2.Name;
                this.tbx2ndOfSealNo2.LinkedLabelName = lblSealNo2.Name;
                this.tbx1stOfSealNo3.LinkedLabelName = lblSealNo3.Name;
                this.tbx2ndOfSealNo3.LinkedLabelName = lblSealNo3.Name;

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

            this.tbx1stOfSealNo1.Click += new EventHandler(tbxSealNo_Click);
            this.tbx2ndOfSealNo1.Click += new EventHandler(tbxSealNo_Click);
            this.tbx1stOfSealNo2.Click += new EventHandler(tbxSealNo_Click);
            this.tbx2ndOfSealNo2.Click += new EventHandler(tbxSealNo_Click);
            this.tbx1stOfSealNo3.Click += new EventHandler(tbxSealNo_Click);
            this.tbx2ndOfSealNo3.Click += new EventHandler(tbxSealNo_Click);
        }

        #endregion INITIALIZATION AREA ****************************************

        #region EVENT HANDLER AREA ********************************************

        protected override void OnShown(EventArgs e)
        {
            try
            {
                if (ThisController.UpdateSealItem != null)
                {
                    if (string.IsNullOrEmpty(ThisController.UpdateSealItem.SealNo1) == false)
                    {
                        string[] str = ThisController.UpdateSealItem.SealNo1.Split(',');
                        tbx1stOfSealNo1.Text = str[0];
                        if (str.Length > 1)
                        {
                            tbx2ndOfSealNo1.Text = str[1];
                        }
                    }
                    if (string.IsNullOrEmpty(ThisController.UpdateSealItem.SealNo2) == false)
                    {
                        string[] str = ThisController.UpdateSealItem.SealNo2.Split(',');
                        tbx1stOfSealNo2.Text = str[0];
                        if (str.Length > 1)
                        {
                            tbx2ndOfSealNo2.Text = str[1];
                        }
                    }
                    if (string.IsNullOrEmpty(ThisController.UpdateSealItem.SealNo3) == false)
                    {
                        string[] str = ThisController.UpdateSealItem.SealNo3.Split(',');
                        tbx1stOfSealNo3.Text = str[0];
                        if (str.Length > 1)
                        {
                            tbx2ndOfSealNo3.Text = str[1];
                        }
                    }

                    // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
                    if (VisibleSealChk)
                    {
                        if (ThisController.UpdateSealItem.JobCode.Equals(CTBizConstant.QuayJobType.DISCHARGING) ||
                            ThisController.UpdateSealItem.JobCode.Equals(CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_DISCHARGING))
                        {
                            if (string.IsNullOrEmpty(ThisController.UpdateSealItem.SealChk))
                            {
                                rbSealChk1.Checked = true;
                            }
                            else
                            {
                                if (ThisController.UpdateSealItem.SealChk.Equals("Y"))
                                {
                                    rbSealChk1.Checked = true;
                                }
                                else
                                {
                                    rbSealChk2.Checked = true;
                                }
                            }
                        }
                        else
                        {
                            pnlSealChk.Enabled = false;

                            if (string.IsNullOrEmpty(ThisController.UpdateSealItem.SealNo2))
                            {
                                pnlSealChk.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        pnlSealChk.Visible = false;
                    }
                }

                if (UseDisableSealNo3)
                {
                    this.pnlSealNo3.Visible = false;
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
                string sealNo1 = JoinSealNoValues(tbx1stOfSealNo1.Text, tbx2ndOfSealNo1.Text);
                string sealNo2 = JoinSealNoValues(tbx1stOfSealNo2.Text, tbx2ndOfSealNo2.Text);
                string sealNo3 = JoinSealNoValues(tbx1stOfSealNo3.Text, tbx2ndOfSealNo3.Text);
                string sealChk = rbSealChk1.Checked ? "Y" : "N"; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

                // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
                if (VisibleSealChk == false)
                {
                    sealChk = string.Empty;
                }

                ThisController.UpdateSeal(sealNo1, sealNo2, sealNo3, sealChk);

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
    }
}
