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
* 2010.02.04    CHOI 1.0	First release.
* 2011.04.25   Tonny.Kim    EncryptedPassword
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Information class of Enterprise Information System (DATABASE)
    /// </summary>
    [Serializable]
    public class Server : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************

        private string _serverRole = string.Empty;
        /// <summary>
        /// Server role
        /// </summary>
        public string ServerRole
        {
            get { return _serverRole; }
            set { _serverRole = value; }
        }

        private ServerTypes _serverType = default(ServerTypes);
        /// <summary>
        /// Server Type
        /// </summary>
        public ServerTypes ServerType
        {
            get { return _serverType; }
            set { _serverType = value; }
        }

        private string _productName = string.Empty;
        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        private string _productVersion = string.Empty;
        /// <summary>
        /// Product Version
        /// </summary>
        public string ProductVersion
        {
            get { return _productVersion; }
            set { _productVersion = value; }
        }


        private string _productDesc = string.Empty;
        /// <summary>
        /// Product Description
        /// </summary>
        public string ProductDesc
        {
            get { return _productDesc; }
            set { _productDesc = value; }
        }

        private string _address = string.Empty;
        /// <summary>
        /// Address (IP Address)
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _port = string.Empty;
        /// <summary>
        /// Port
        /// </summary>
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private string _alias = string.Empty;
        /// <summary>
        /// Alias information (like TNS Name)
        /// </summary>
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        private string _connString = string.Empty;
        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnString
        {
            get { return _connString; }
            set { _connString = value; }
        }

        private string _userId = string.Empty;
        /// <summary>
        /// User ID to access server
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _encryptedPassword = string.Empty;
        /// <summary>
        /// Encrypted Password to access server
        /// </summary>
        public string EncryptedPassword
        {
            get { return _encryptedPassword; }
            set { _encryptedPassword = value; }
        }

        private string _password = string.Empty;
        /// <summary>
        /// Password to access server
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _adapterAssembly = string.Empty;
        /// <summary>
        /// Adapter class's assembly information
        /// </summary>
        public string AdapterAssembly
        {
            get { return _adapterAssembly; }
            set { _adapterAssembly = value; }
        }

        private string _adapterClass = string.Empty;
        /// <summary>
        /// Adapter class's full name (strong type) 
        /// </summary>
        public string AdapterClass
        {
            get { return _adapterClass; }
            set { _adapterClass = value; }
        }

        private string _connAssembly = string.Empty;
        /// <summary>
        /// Connection class's assembly information
        /// </summary>
        public string ConnAssembly
        {
            get { return _connAssembly; }
            set { _connAssembly = value; }
        }

        private string _connClass = string.Empty;
        /// <summary>
        /// Connection class's full name (string type)
        /// </summary>
        public string ConnClass
        {
            get { return _connClass; }
            set { _connClass = value; }
        }

        private bool _isDefaut = false;
        /// <summary>
        /// Indicator whether this server is default or not
        /// </summary>
        public bool IsDefaut
        {
            get { return _isDefaut; }
            set { _isDefaut = value; }
        }
        private bool _enabled = false;

        /// <summary>
        /// Indicator whether this server is enable or not
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool _useLDAP = false;

        /// <summary>
        /// Indicator whether this server use or doesn't use LDAP
        /// </summary>
        public bool UseLDAP
        {
            get { return _useLDAP; }
            set { _useLDAP = value; }
        }

        private string _dbConnectionTimeout = string.Empty;

        /// <summary>
        /// Indicator whether this server set Database Connection Timeout.
        /// </summary>
        public string DBConnectionTimeout
        {
            get { return _dbConnectionTimeout; }
            set { _dbConnectionTimeout = value; }
        }

        private string _dbConnectionCustomAttributes = string.Empty;

        /// <summary>
        /// Indicator whether this server set DB Connection Custom Attributes.
        /// </summary>
        public string DBConnectionCustomAttributes
        {
            get { return _dbConnectionCustomAttributes; }
            set { _dbConnectionCustomAttributes = value; }
        }
        #endregion


        #region INITIALIZATION AREA ****************************
         /// <summary>
        /// Default Constructor
        /// </summary>
        public Server() : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-Server";
            this.ObjectType = ObjectType.HELPER;
        }

        /// <summary>
        /// Initialize instance using server role
        /// </summary>
        public Server(string serverRole)
            : this()
        {
            this._serverRole = serverRole;
        }

        #endregion
    }
}
