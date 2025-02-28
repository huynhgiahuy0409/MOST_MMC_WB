#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2013 TOTAL SOFT BANK LIMITED. All Rights
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
* 2013.04.24  Jindols 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Environments.Item
{
    /// <summary>
    /// Resouce Simple Data Item class
    /// </summary>
    [Serializable]
    public class ResourceDataItem : BaseDataItem
    {
        #region FIELD/PROPERTY AREA *********************************
        private string _typeName = null;
        private string _prefix = null;
        private string _resourcAssemblyName = null;
        private string _fileName = null;
        private string _predefineBaseName = null;

        /// <summary>
        /// Gets the resource type name.
        /// </summary>
        public string TypeName
        {
            get { return _typeName; }
        }

        /// <summary>
        /// Gets the resource's prefix name.
        /// </summary>
        public string Prefix
        {
            get { return _prefix; }
        }

        /// <summary>
        /// Gets the main System.Reflection.Assembly for the resources.
        /// </summary>
        public string ResourcAssemblyName
        {
            get { return _resourcAssemblyName; }
        }

        /// <summary>
        /// Gets the root name of the resources. 
        /// For example, the root name for the resource file named "MyResource.en-US.resources" is "MyResource".
        /// </summary>
        public string BaseName
        {
            get { return _fileName; }
        }

        /// <summary>
        /// Gets the root name of the predefined resources. 
        /// For example, the root name for the resource file named "MyResource.en-US.resources" is "MyResource".
        /// </summary>
        public string PredefineBaseName
        {
            get { return _predefineBaseName; }
            set { _predefineBaseName = value; }
        }
        #endregion

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Default constructor
        /// </summary>
        internal ResourceDataItem()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-ResourceDataItem";
        }

        /// <summary>
        /// Initializes a new instance of the ResourceDataItem class.
        /// </summary>
        /// <param name="typeName">The resourece type name</param>
        /// <param name="prefix">The resource's prefix name</param>
        /// <param name="resourcAssemblyName">The main System.Reflection.Assembly for the resources.</param>
        /// <param name="fileName">The root name of the resources. </param>
        /// <param name="predefinefileName">The root name of the predefined resources. </param>
        internal ResourceDataItem(string typeName, string prefix, string resourcAssemblyName, string fileName, string predefinefileName)
            : this()
        {
            _typeName = typeName;
            this._prefix = prefix;
            this._resourcAssemblyName = resourcAssemblyName;
            this._fileName = fileName;
            this._predefineBaseName = predefinefileName;
        }
        #endregion
    }
}
