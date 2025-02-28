#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2009 TOTAL SOFT BANK LIMITED. All Rights
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
* 2009.07.15    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Applied System Architecture environment information class
    /// </summary>
    [Serializable]
    public class ArchitectureInfo  : BaseEnvironmentInfo
    {
        private DITechTypes _diTech;
        private ORMTechTypes _ormTech;
        private LogTechTypes _logTech;
        private TierTypes _tierArch;
        private ServiceArchTypes _serviceArch;
        private UITechTypes _uiTech;
        private DBProductTypes _dbProduct;
        private ResourceTypes _resourceType;
        private bool _UITestSupport = true;
        private static ArchitectureInfo _instance = null;

        public const string XML_ATT_VALUE_TECH    = "Tech";
        public const string XML_ATT_VALUE_TYPE    = "Type";
        public const string XML_ATT_VALUE_PRODUCT = "Product";
        public const string XML_ATT_VALUE_DI      = "DI";
        public const string XML_ATT_VALUE_ORM     = "ORM";
        public const string XML_ATT_VALUE_LOG     = "LOG";
        public const string XML_ATT_VALUE_TIER    = "TIER";
        public const string XML_ATT_VALUE_SERVICE = "SERVICE";
        public const string XML_ATT_VALUE_UI      = "UI";
        public const string XML_ATT_VALUE_DB      = "DB";
        public const string XML_ATT_VALUE_RESOURCE="RESOURCE";
        public const string XML_ATT_VALUE_UITEST = "UITEST";
        public const string XML_ATT_VALUE_Support = "Support";
        

        /// <summary>
        /// Applied DI(IoC) Technology
        /// </summary>
        public DITechTypes DITech
        {
            get { return _diTech; }
        }

        /// <summary>
        /// Applied ORM Technology
        /// </summary>
        public ORMTechTypes ORMTech
        {
            get { return _ormTech; }
        }

        /// <summary>
        /// Applied Log Technology
        /// </summary>
        public LogTechTypes LogTech
        {
            get { return _logTech; }
        }

        /// <summary>
        /// Applied Tiered Architecture
        /// </summary>
        public TierTypes TierArch
        {
            get { return _tierArch; }
        }

        /// <summary>
        /// Applied Service Architecture
        /// </summary>
        public ServiceArchTypes ServiceArch
        {
            get { return _serviceArch; }
        }

        /// <summary>
        /// Applied UI Layer Technology
        /// </summary>
        public UITechTypes UITech
        {
            get { return _uiTech; }
        }

        /// <summary>
        /// Database product
        /// </summary>
        public DBProductTypes DBProduct
        {
            get { return _dbProduct; }
        }

        /// <summary>
        /// Resource Type
        /// </summary>
        public ResourceTypes ResourceType
        {
            get { return _resourceType; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the UI test support.
        /// </summary>
        public bool UITestSupport
        {
            get { return _UITestSupport; }
        }
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        private ArchitectureInfo() : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-ArchitectureInfo";
        }

        /// <summary>
        /// Gets Architecture information object reference.
        /// </summary>
        /// <returns>Arrchitecture information object reference</returns>
        public static ArchitectureInfo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ArchitectureInfo();
                _instance.LoadEnvironmentInfo();
            }

            return _instance;
        }

        /// <summary>
        /// Load architecture info
        /// </summary>
        public void LoadEnvironmentInfo()
        {
            string diTechValue    = string.Empty;
            string ormTechValue   = string.Empty;
            string logTechValue   = string.Empty;
            string tierArchValue  = string.Empty;
            string serviceArchValue = string.Empty;
            string uiTechValue    = string.Empty;
            string dbProductValue = string.Empty;
            string resTypeValue   = string.Empty;
            string uiTestSupportValue = string.Empty;

            XmlConfigProvider configProvider = null;

            try
            {
                this._configFileName = AppPathInfo.FILE_NAME_ARCHITECTURE_INFO;
                this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_ENVIRONMENT, AppPathInfo.FILE_NAME_ARCHITECTURE_INFO);
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

            if (string.IsNullOrEmpty(this._sourcePath))
            {
                //MSG:{0} does not exist. Please check {1}.
                throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00121",
                     this._sourcePath,
                    "WRD_FTCO_thisfile"
                    );
            }

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(this._sourcePath);

                diTechValue = this.GetValidValue(ref configProvider, ArchitectureInfo.XML_ATT_VALUE_DI, ArchitectureInfo.XML_ATT_VALUE_TECH);
                this._diTech = this.GetValidType<DITechTypes>(diTechValue, ArchitectureInfo.XML_ATT_VALUE_DI, ArchitectureInfo.XML_ATT_VALUE_TECH);

                ormTechValue = this.GetValidValue(ref configProvider, ArchitectureInfo.XML_ATT_VALUE_ORM, ArchitectureInfo.XML_ATT_VALUE_TECH);
                this._ormTech = this.GetValidType<ORMTechTypes>(ormTechValue, ArchitectureInfo.XML_ATT_VALUE_ORM, ArchitectureInfo.XML_ATT_VALUE_TECH);

                logTechValue = this.GetValidValue(ref configProvider, ArchitectureInfo.XML_ATT_VALUE_LOG, ArchitectureInfo.XML_ATT_VALUE_TECH);
                this._logTech = this.GetValidType<LogTechTypes>(logTechValue, ArchitectureInfo.XML_ATT_VALUE_LOG, ArchitectureInfo.XML_ATT_VALUE_TECH);

                tierArchValue = this.GetValidValue(ref configProvider, ArchitectureInfo.XML_ATT_VALUE_TIER, ArchitectureInfo.XML_ATT_VALUE_TYPE);
                this._tierArch = this.GetValidType<TierTypes>(tierArchValue, ArchitectureInfo.XML_ATT_VALUE_TIER, ArchitectureInfo.XML_ATT_VALUE_TYPE);

                serviceArchValue = this.GetValidValue(ref configProvider, ArchitectureInfo.XML_ATT_VALUE_SERVICE, ArchitectureInfo.XML_ATT_VALUE_TYPE);
                this._serviceArch = this.GetValidType<ServiceArchTypes>(serviceArchValue, ArchitectureInfo.XML_ATT_VALUE_SERVICE, ArchitectureInfo.XML_ATT_VALUE_TYPE);

                uiTechValue = this.GetValidValue(ref configProvider, ArchitectureInfo.XML_ATT_VALUE_UI, ArchitectureInfo.XML_ATT_VALUE_TECH);
                this._uiTech = this.GetValidType<UITechTypes>(uiTechValue, ArchitectureInfo.XML_ATT_VALUE_UI, ArchitectureInfo.XML_ATT_VALUE_TECH);

                dbProductValue = this.GetValidValue(ref configProvider, ArchitectureInfo.XML_ATT_VALUE_DB, ArchitectureInfo.XML_ATT_VALUE_PRODUCT);
                this._dbProduct = this.GetValidType<DBProductTypes>(dbProductValue, ArchitectureInfo.XML_ATT_VALUE_DB, ArchitectureInfo.XML_ATT_VALUE_PRODUCT);

                resTypeValue = this.GetValidValue(ref configProvider, ArchitectureInfo.XML_ATT_VALUE_RESOURCE, ArchitectureInfo.XML_ATT_VALUE_TYPE);
                this._resourceType = this.GetValidType<ResourceTypes>(resTypeValue, ArchitectureInfo.XML_ATT_VALUE_RESOURCE, ArchitectureInfo.XML_ATT_VALUE_TYPE);


                try
                {
                    uiTestSupportValue = this.GetValidValue(ref configProvider, ArchitectureInfo.XML_ATT_VALUE_UITEST, ArchitectureInfo.XML_ATT_VALUE_Support, false);
                    if (String.IsNullOrEmpty(uiTestSupportValue) == false)
                    {
                        this._UITestSupport = Convert.ToBoolean(uiTestSupportValue);
                    }
                    else
                    {
                        this._UITestSupport = true;
                    }
                    
                }
                catch (Exception ex)
                {
                    this._UITestSupport = false;
                    GeneralLogger.Error(ex);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
            

        }
    }
}
