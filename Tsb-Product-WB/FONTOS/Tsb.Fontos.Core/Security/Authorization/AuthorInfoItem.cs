using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Security.Type;

namespace Tsb.Fontos.Core.Security.Authorization
{
    /// <summary>
    /// Item class of authorization Information for object
    /// </summary>
    [Serializable]
    public class AuthorInfoItem : BaseDataItem
    {
        #region FIELD/PROPERTY AREA*****************************

        /// <summary>
        /// Gets Module ID (like FTSA/CTOM/CTSP)
        /// </summary>
        public string ModuleID  { get; set; }

        /// <summary>
        /// Gets or Sets Authorization Target ID
        /// </summary>
        public string TargetID { get; set; }

        /// <summary>
        /// Gets Parent Target Module ID (like FTSA/CTOM/CTSP)
        /// </summary>
        public string ParentModuleID  { get; set; }

        /// <summary>
        /// Gets or Sets Parent Authorization Target ID
        /// </summary>
        public string ParentTargetID { get; set; }

        /// <summary>
        /// Gets or Sets Authorization rule scope
        /// </summary>
        public AuthorRuleScopeTypes RuleScope { get; set; }

        /// <summary>
        /// Gets or Sets User or Group ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// Gets or Sets Authorization Target Name
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Gets or Sets Authorization Target Type (UIControl, Operation, Object)
        /// </summary>
        public AuthorTargetTypes TargetType { get; set; }

        /// <summary>
        /// Gets or Sets Authorization Target Type ID 
        /// (like Textbox/Menu for UI Target type and Insert/Save for Operation Target Type)
        /// </summary>
        public string TargetTypeID { get; set; }

        /// <summary>
        /// Gets boolean value whether authorization target is visible or not
        /// </summary>
        public Nullable<bool> Visible
        {
            get { return ConvertUtil.ToNullableBoolean(this._visibleYN); }
        }

        private string _visibleYN = null;
        /// <summary>
        /// Gets or Sets a Y/N string whether authorization target is visible or not
        /// </summary>
        public string VisibleYN
        {
            get { return _visibleYN; }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._visibleYN = Convert.ToString(value).ToUpper();
                }
                else
                {
                    this._visibleYN = null;
                }
            } 
        }

        /// <summary>
        /// Gets boolean value whether authorization target is enable or not
        /// </summary>
        public Nullable<bool> Enable
        {
            get { return ConvertUtil.ToNullableBoolean(this._enableYN); }
        }

        private string _enableYN = null;
        /// <summary>
        /// Gets or Sets a Y/N string whether authorization target is enable or not
        /// </summary>
        public string EnableYN
        {
            get { return _enableYN; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._enableYN = Convert.ToString(value).ToUpper();
                }
                else
                {
                    this._enableYN = null;
                }
            }
        }

        /// <summary>
        /// Gets or Sets Optional Info for this authorization information
        /// </summary>
        public string OptInfo { get; set; }

        #endregion
 

        #region INITIALIZATION AREA ****************************
        public AuthorInfoItem()
            : base()
        {
            this.ObjectID = "ITM-FTCO-SEC-AuthorInfoItem";
        }
        #endregion
    }
}
