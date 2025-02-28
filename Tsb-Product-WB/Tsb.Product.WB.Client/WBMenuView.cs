#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2011 TOTAL SOFT BANK LIMITED. All Rights
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
#endregion

using C1.Win.C1Ribbon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.Codes;
using Tsb.Catos.Cm.Core.Grid;
using Tsb.Fontos.Core.Codes;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Context;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Resources.Menu;
using Tsb.Fontos.Core.Security.Authentication;
using Tsb.Fontos.Core.Security.Authorization;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Validator;
using Tsb.Fontos.Core.Validator.IO;
using Tsb.Fontos.Core.Xml;
using Tsb.Fontos.Win.ExceptionReport.Core;
using Tsb.Fontos.Win.Forms;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Win.Grid.Filter;
using Tsb.Fontos.Win.Init;
using Tsb.Fontos.Win.Menu.Item;
using Tsb.Fontos.Win.Menu.Toolbar;
using Tsb.Fontos.Win.Menu.Toolbar.BizController;
using Tsb.Fontos.Win.Menu.Util;
using Tsb.Fontos.Win.Message;
using Tsb.Fontos.Win.Status;
using Tsb.Fontos.Win.Status.Types;

namespace Tsb.Product.WB.Client
{
    /// <summary>
    /// Template Application Menu View Class
    /// </summary>
    public partial class TpMenuView : BaseMenuView
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TpMenuView()
            : base()
        {
            BaseAppInitializer appInitializer = null;

            try
            {
                this.ObjectID = "VIW-PT-PTWB-TmMenuView";
                this.IsFromDB = false;

                InitializeComponent();

                
                appInitializer = (BaseAppInitializer)ObjectBuilder.GetObjectBuilder().GetObject(SysObjectSpec.SYS_OBJECT_ID_APP_INITILIZER);
                
                //Initialize Application Code Data Handler
                CodeManager.InitilizeInstance(new CodeGeneralHandler());
                
                this.AppInitializer = appInitializer;
                this.AppInitializer.StandardInitApp();

               
                //FilterStoreManager.InitilizeInstance(new FilterStoreGeneralHandler());

                this.MenuXmlNameWithPath = PathUtil.Combine(AppPathInfo.PATH_APP_ENVIRONMENT, AppPathInfo.FILE_NAME_MENU_ITEM);
                this.MainMenuImageFileName = "icon_TpMenuTitle";

                base.ResourceManager = Tsb.Product.WB.Client.Properties.Resources.ResourceManager; // Menu Image, Icon을 Load하기 위함.

                this.Load += new EventHandler(Form_Load);
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG-FTCO-99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }
        }

        /// <summary>
        /// Form Load Event
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            base.TRbnMenu.Minimized = true; // RibbonMenu minimized
        }

        /// <summary>
        /// Initialize main status strip
        /// </summary>
        public override void InitMainStatusStrip()
        {

            this.MainStatusStrip = new Tsb.Fontos.Win.Status.TMainMenuStatusStrip();
            this.MainStatusStrip.BackColor = System.Drawing.SystemColors.Control;
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 695);
            this.MainStatusStrip.Size = new System.Drawing.Size(1109, 22);
            this.MainStatusStrip.TabIndex = 0;

            this.MainStatusStrip.AddStripItem(MainStatusStripItemType.MessageStrip, true);
            this.TsLblMsgArea = this.MainStatusStrip.GetStripItemByName(TMainMenuStatusStrip.NAME_MAIN_MESSAGE_STRIP_DEFAULT) as ToolStripStatusLabel;

            this.MainStatusStrip.AddStripItem(MainStatusStripItemType.GridRowCountStrip, true);
            this.TsLblGridRowCount = this.MainStatusStrip.GetStripItemByName(TMainMenuStatusStrip.NAME_MAIN_GRIDROWCOUNT_STRIP_DEFAULT) as ToolStripStatusLabel;

            this.MainStatusStrip.AddStripItem(MainStatusStripItemType.UserInfoStrip, true);
            this.TsLblUsrInfo = this.MainStatusStrip.GetStripItemByName(TMainMenuStatusStrip.NAME_MAIN_USERINFO_STRIP_DEFAULT) as ToolStripStatusLabel;

            this.MainStatusStrip.AddStripItem(MainStatusStripItemType.NetworkStatusStrip, false);
            this.TsLblNwStatus = this.MainStatusStrip.GetStripItemByName(TMainMenuStatusStrip.NAME_MAIN_NWSTATUS_STRIP_DEFAULT) as ToolStripStatusLabel;

            this.MainStatusStrip.AddStripItem(MainStatusStripItemType.DatabaseStatusStrip, true);
            this.TsLblDBStatus = this.MainStatusStrip.GetStripItemByName(TMainMenuStatusStrip.NAME_MAIN_DBSTATUS_STRIP_DEFAULT) as ToolStripStatusLabel;

            this.MainStatusStrip.AddStripItem(MainStatusStripItemType.CapsLockStrip, true);
            this.TsLblCapsStatus = this.MainStatusStrip.GetStripItemByName(TMainMenuStatusStrip.NAME_MAIN_CAPSLOCK_STRIP_DEFAULT) as ToolStripStatusLabel;

            this.MainStatusStrip.AddStripItem(MainStatusStripItemType.ProgressBarStrip, true);
            this.PrgMainProgress = this.MainStatusStrip.GetStripItemByName(TMainMenuStatusStrip.NAME_MAIN_PROGBAR_STRIP_DEFAULT) as TToolStripStatusProgressBar;

            this.Controls.Add(this.MainStatusStrip);

            return;
        }


    }
}