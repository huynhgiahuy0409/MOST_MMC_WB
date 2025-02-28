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
* 2009.06.15    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Configuration.Common;
using System.ComponentModel;
using Tsb.Fontos.Core.Objects;
using System.Reflection;
using System.Collections;
using System.Xml.Serialization;
using Tsb.Fontos.Core.Security.Profile;

namespace Tsb.Fontos.Core.Item
{
    /// <summary>
    /// TSB Base DataItem Class
    /// </summary>
    [Serializable]
    public class BaseDataItem : TsbBaseObject, IDataItem, INotifyPropertyChanged
    {
        #region FIELD AREA *************************************
        private string _key = "NOT_ASSIGNED_KEY";
        private OpCodes _opCode = OpCodes.READ;
        private string _version = "NOT_ASSIGNED_VERSION";
        private Guid _guid = default(Guid);
        private bool _notifyPropertyChangedLock = true;
        #endregion


        #region PROPERTY AREA **********************************
        [XmlIgnore]
        [Browsable(false)]
        public BaseDataItem BackupItem { get; set; }

        /// <summary>
        /// Gets or Sets Operation code (CREATE, DELETE, UPDATE, READ, ERROR)
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public OpCodes OpCode
        {
            get { return _opCode; }
            set
            {
                if (value != this._opCode)
                {
                    this._opCode = value;
                }
            }
        }

        /// <summary>
        /// Gest or Sets version string of item
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// Gets or Sets GUID of item
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public Guid GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }

        /// <summary>
        /// Gets or Sets User ID (Staff CD)
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public string UserID {get; set;}

        /// <summary>
        /// Gets or Sets whether the NotifyPropertyChanged method lock
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool LockNotifyPropertyChanged{
            get { return _notifyPropertyChangedLock; }
            set { _notifyPropertyChangedLock = value; }
        }

        #region IDataItem Members
        /// <summary>
        /// Gets or Sets Key string of item
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public virtual string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        #endregion

        #endregion
        

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseDataItem()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ITM-BaseDataItem";
            this.ObjectType = ObjectType.ITEM;
            this.GUID = System.Guid.NewGuid();
        }
        #endregion

       
        #region EVENT HANDLER AREA *****************************

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.LockNotifyPropertyChanged) return;

            if (this.OpCode == OpCodes.READ ||
                this.OpCode == OpCodes.DELETE)
            {
                this.OpCode = OpCodes.UPDATE;
            }

            this.SetPropertyChanged(propertyName);
        }

        private void SetPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        #region METHOD AREA ************************************
        /// <summary>
        /// Make backup Item from current information
        /// </summary>
        public virtual void MakeBackupItem()
        {
            this.LockNotifyPropertyChanged = false;
            if (this.BackupItem != null) this.BackupItem = null;            
            this.BackupItem = this.MemberwiseClone() as BaseDataItem;
        }
        #endregion        
    }
}
