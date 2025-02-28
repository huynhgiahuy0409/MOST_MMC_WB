using System;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item.MobileContainerDetail;
using Tsb.Fontos.Core.Resources;
using Tsb.Fontos.Win.Forms;
using Tsb.Fontos.Win.Grid.Spread.DetailLayout;
using Tsb.Fontos.Win.Keyboard;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;

namespace Tsb.Catos.Cm.Mobile.Win.MobileContainerDetail
{
    public partial class MobileContainerDetailView : BaseMobileBizView, MobileContainerDetailInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-MobileContainerDetailView";

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        public MobileContainerDetailController ThisController
        {
            get { return this.Controller as MobileContainerDetailController; }
        }

        public bool UseSealCheck { get; set; }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public MobileContainerDetailView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new MobileContainerDetailController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnSeal.Click += new EventHandler(btnSeal_Click);
            this.tbxContainerNo.Click += new EventHandler(txtContainerNo_Click);
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
                this.tbxContainerNo.Text = ThisController.SelectedContainerNo;
                SearchContainerInfo();

                btnSeal.Visible = UseSealCheck; // added by SH.Nam (2018.06.01) WHL Gap ID : CR-K-031
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
                ErrorMessageHandler.Show(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ThisController.SelectedContainerNo = this.tbxContainerNo.Text;
                SearchContainerInfo();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        private void btnSeal_Click(object sender, EventArgs e)
        {
            try
            {
                ThisController.ShowUpdateSealView();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        private void txtContainerNo_Click(object sender, EventArgs e)
        {
            try
            {
                var keyboard = KeyboardManagerFactory.GetInstance(KeyboardTypes.Virtual);
                string keyBoardTitle = ResourceFactory.GetResource().GetLabel("WRD_CTMO_ContainerNo");
                string searchValue = keyboard.Show(keyBoardTitle, "", 11);
                if (string.IsNullOrEmpty(searchValue))
                {
                    tbxContainerNo.Text = string.Empty;
                }
                else
                {
                    tbxContainerNo.Text = searchValue.ToUpper();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        #endregion EVENT HANDLER AREA *****************************************

        #region METHOD AREA ***************************************************

        private void SearchContainerInfo()
        {
            try
            {
                ThisController.DoRetrieveData();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        public void SetDetailLayout(MobileContainerDetailItem detailItem)
        {
            try
            {
                this.pnlMain.Controls.Clear();

                var viewName = string.IsNullOrEmpty(ThisController.Module) ? this.Name : ThisController.Module + this.Name; // added by YoungOk Kim (2019.01.03) - Mantis 88302: [YQ] 컨테이너 정보 조회 오류

                TPanel panel = DetailLayoutPanelCreator.GetInstance().CreateDetailLayoutPanel(viewName, detailItem);
                this.pnlMain.Controls.Add(panel);
                this.pnlMain.Refresh();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        public void SetContainerNo(string containerNo)
        {
            try
            {
                this.tbxContainerNo.Text = containerNo;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        #endregion METHOD AREA ************************************************
    }
}