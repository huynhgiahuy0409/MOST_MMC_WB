using System;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;

namespace Tsb.Catos.Cm.Mobile.Win.StoppageResume
{
    public partial class StoppageResumeView : BaseMobileBizView, StoppageResumeInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-StoppageResumeView";
        private bool _useCloseBtn = false; // added by jaeok (2020.02.04) Mantis 105030: Add Close button in Stoppage screen in Tally VB
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************
        
        public string FormName
        {
            get { return Name; }
        }
        
        private StoppageResumeController ThisController
        {
            get { return this.Controller as StoppageResumeController; }
        }

        public bool UsingUndoFunction { get; set; }
        public bool IsUndoEvent { get; set; }

        // added by jaeok (2020.02.04) Mantis 105030: Add Close button in Stoppage screen in Tally VB
        public bool UseCloseBtn
        {
            get { return this._useCloseBtn; }
            set { this._useCloseBtn = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public StoppageResumeView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new StoppageResumeController(this);
            this.InitControl();
            this.AddEventHandler();

            this.tmrDateTime.Start();
        }

        private void InitControl()
        {
            try
            {
                this.tmrDateTime.Interval = 100;
                FormDraggingHandler _formDraggingHdl = new FormDraggingHandler(this.lblTitle, this); // added by YoungOk Kim (2019.05.16) - Mantis 89476: [Tally] 창 이동 기능
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void AddEventHandler()
        {
            this.tmrDateTime.Tick += new EventHandler(tmrDateTime_Tick);
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnUndo.Click += new EventHandler(btnUndo_Click);
            this.btnClose.Click += new EventHandler(btnClose_Click); // added by jaeok (2020.02.04) Mantis 105030: Add Close button in Stoppage screen in Tally VB
        }
        
        #endregion INITIALIZATION AREA ****************************************

        #region EVENT HANDLER AREA ********************************************

        protected override void OnShown(EventArgs e)
        {
            try
            {
                txtStoppage.Text = ThisController.SelectedStoppageReasonItem.StopDesc;
                btnUndo.Visible = UsingUndoFunction;

                // added by jaeok (2020.02.04) Mantis 105030: Add Close button in Stoppage screen in Tally VB
                btnClose.Visible = UseCloseBtn;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
            }
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            try
            {
                this.dttDate.Value = DateTime.Now;
                this.dttTime.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                if (UsingUndoFunction)
                {
                    this.IsUndoEvent = false;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                BaseResult resultObject = ThisController.UpdateStopReason(MOCommonConstants.EQU_STOPPAGE_RESUME);
                if (resultObject != null)
                {
                    this.DialogResult = DialogResult.OK;
                    if (UsingUndoFunction)
                    {
                        this.IsUndoEvent = false;
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                BaseResult resultObject = ThisController.UpdateStopReason(MOCommonConstants.EQU_STOPPAGE_ROLLBACK);
                if (resultObject != null)
                {
                    this.DialogResult = DialogResult.OK;
                    if (UsingUndoFunction)
                    {
                        this.IsUndoEvent = true;
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (UseCloseBtn == true)
                {
                    this.DialogResult = DialogResult.Ignore;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }

        }

        #endregion EVENT HANDLER AREA ********************************************

        #region METHOD AREA ******************************************************
        #endregion METHOD AREA ***************************************************
    }
}
