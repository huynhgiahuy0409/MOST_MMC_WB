using System;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;

namespace Tsb.Catos.Cm.Mobile.Win.Stoppage
{
    public partial class StoppageView : BaseMobileBizView, StoppageInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-StoppageView";
        private bool _excludeVesselStoppage = false;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        private StoppageController ThisController
        {
            get { return this.Controller as StoppageController; } 
        }
        
        public bool ExcludeVesselStoppage // added by BG.Kim (2023.01.10)	[PCT] Not show vessel stoppages on Tally VMT
        {
            get { return _excludeVesselStoppage; }
            set { _excludeVesselStoppage = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************
        public StoppageView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new StoppageController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void InitControls()
        {
            try
            {
                FormDraggingHandler _formDraggingHdl = new FormDraggingHandler(this.lblTitle, this); // added by YoungOk Kim (2019.05.16) - Mantis 89476: [Tally] 창 이동 기능
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion INITIALIZATION AREA ****************************************

        #region EVENT HANDLER AREA ********************************************

        protected override void OnShown(EventArgs e)
        {
            try
            {
                this.dttDate.Value = DateTime.Now;
                this.dttTime.Value = DateTime.Now;

                ThisController.DoRetrieveData();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
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
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedCode = this.stoppageList.GetSelectedItemCode();
                if (selectedCode.Length > 0)
                {
                    BaseResult resultObject = ThisController.InsertStopReason(selectedCode);
                    if (resultObject != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion EVENT HANDLER AREA *****************************************

        #region METHOD AREA ***************************************************

        public void SetEquipmentNo(string equipmentNo)
        {
            try
            {
                if (equipmentNo.Length > 0)
                {
                    this.txtEquipment.Text = equipmentNo;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void SetItemList(BaseItemsList<ItemListControlItem> itemList)
        {
            try
            {
                this.stoppageList.ItemList = itemList;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion METHOD AREA ************************************************
    }
}
