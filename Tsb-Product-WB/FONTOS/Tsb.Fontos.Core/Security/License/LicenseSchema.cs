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
* 2010.05.15    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Xml;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Security.License
{
    /// <summary>
    /// Grid Filter Schema class
    /// </summary>
    [XmlRoot("TSB-Licenses")]
    public class LicenseSchema : TsbBaseObject
    {
        #region FIELD AREA *************************************
        private const string _objectID = "GNR-FTCO-SEC-LicenseSchema";
        #endregion

        #region PROPERTY AREA **********************************
        
        /// <summary>
        /// Publisher (ex:Total Soft Bank LTD)
        /// </summary>
        [XmlElement("Publisher")]
        public string Publisher { get; set; }


        /// <summary>
        /// Publisher Date YYYYMMDD(ex:20100515)
        /// </summary>
        [XmlElement("PublishDate")]
        public string PublishDate { get; set; }

        /// <summary>
        /// License File Maker(ex:Choi)
        /// </summary>
        [XmlElement("KeyMaker")]
        public string KeyMaker { get; set; }

        /// <summary>
        /// License List
        /// </summary>
        [XmlElement("License")]
        public List<License> Licenses { get; set; }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Consturctor
        /// </summary>
        public LicenseSchema()
            : base()
        {
            this.ObjectID = LicenseSchema._objectID;
        }
        #endregion


        /// <summary>
        /// Returns LicenseSchema object
        /// </summary>
        /// <param name="xmlString">XML formatted string</param>
        /// <returns>LicenseSchema object</returns>
        public static LicenseSchema GetLicenseSchema(string xmlString)
        {
            LicenseSchema licenseSchema = null;

            try
            {
                licenseSchema = XmlUtil.DeserializeFromString<LicenseSchema>(xmlString);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, LicenseSchema._objectID);
            }
            return licenseSchema;
        }

        /// <summary>
        /// Converts License schema object to xml string
        /// </summary>
        /// <param name="objectToSave">Object to convert</param>
        public static string ConvertLicenseSchemaToXml(object schemaObject)
        {
            string xmlString = null;

            try
            {
                xmlString = XmlUtil.SerializeToXmlString<LicenseSchema>(schemaObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, LicenseSchema._objectID);
            }
            return xmlString;
        }
    }

    /// <summary>
    /// License type class for "License" Xml element
    /// </summary>
    public class License : BaseDataItem
    {

        #region PROPERTY AREA **********************************
        /// <summary>
        /// Product Name (ex:CATOS)
        /// </summary>
        [XmlAttribute("ProductName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Product ID (ex:CT)
        /// </summary>
        [XmlAttribute("ProductID")]
        public string ProductID { get; set; }

        /// <summary>
        /// Module Name (ex:Operation Management)
        /// </summary>
        [XmlAttribute("ModuleName")]
        public string ModuleName { get; set; }

        /// <summary>
        /// Module ID (ex:OM)
        /// </summary>
        [XmlAttribute("ModuleID")]
        public string ModuleID { get; set; }

        /// <summary>
        /// Licensee (ex:LICT)
        /// </summary>
        [XmlAttribute("Licensee")]
        public string Licensee { get; set; }

        /// <summary>
        /// Expiration Date yyyyMMdd (ex:20100501)
        /// </summary>
        [XmlAttribute("Expiration")]
        public string Expiration { get; set; }

        /// <summary>
        /// The count of CPU (ex:unlimited  or 1, 2)
        /// </summary>
        [XmlAttribute("CPUs")]
        public string CPUs { get; set; }

        /// <summary>
        /// Granted IP address (ex:any  or 192.168.1.0)
        /// </summary>
        [XmlAttribute("IP")]
        public string IP { get; set; }

        /// <summary>
        /// The number of granted copy units (ex:unlimited  or 1, 2)
        /// </summary>
        [XmlAttribute("Unit")]
        public string Unit { get; set; }

        /// <summary>
        /// Development Language for this module (ex: .NET, VB)
        /// </summary>
        [XmlAttribute("DevLang")]
        public string DevLang { get; set; }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Consturctor
        /// </summary>
        public License()
        {
            this.ObjectID = "GNR-FTCO-SEC-License";
            this.ObjectType = ObjectType.DEFAULT;
        }
        #endregion
    }
}
