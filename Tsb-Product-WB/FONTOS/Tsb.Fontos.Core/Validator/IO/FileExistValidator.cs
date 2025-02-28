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
* 2010.02.04   CHOI 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Transaction;

namespace Tsb.Fontos.Core.Validator.IO
{
    /// <summary>
    /// File Exist validator class
    /// </summary>
    public class FileExistValidator : BaseValidator
    {
        #region FIELD/PROPERTY AREA*****************************

        private string _fileName=null;
        /// <summary>
        /// Gets or Sets File Name
        /// </summary>
        public string FileName
        {
            get{ return _fileName;  }
            set{ _fileName = value; }
        }

        private string _path=null;
        /// <summary>
        /// Gets or Sets File path
        /// </summary>
        public string Path
        {
            get{ return _path;  }
            set{ _path = value; }
        }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileExistValidator()
            : base()
        {
            this.ObjectID = "GNR-FTCO-VAL-FileExistValidator";
            this.ObjectType = ObjectType.HELPER;
        }
        #endregion


        #region METHOD AREA (VALIDATE)**************************
        /// <summary>
        /// Validates whether file exists or not
        /// </summary>
        /// <param name="resultList">Result List</param>
        /// <returns>Validation result item list</returns>
        public override List<ValidResultItem> Validate(ref List<ValidResultItem> resultList)
        {
            string fileName = string.Empty;
            string path = string.Empty;

            if(string.IsNullOrEmpty(this.TargetName) || string.IsNullOrEmpty(this.FileName) || string.IsNullOrEmpty(this.Path) )
            {
                ///MSG:Validator syntax error. To use [{0}] Validator, [{1}] properties shoud be set.	
                throw new TsbSysBaseException(this.ObjectID, "MSG_FTCO_00076", DefaultMessage.NON_REG_WRD + "FileExist", 
                    DefaultMessage.NON_REG_WRD+"TargetName,FileName,Path");
            }

            if ( !string.IsNullOrEmpty(this.Path) && this.Path.EndsWith(this.FileName))
            {
                fileName = System.IO.Path.GetFileName(this.Path);
                path = this.Path.Replace(fileName, string.Empty);
            }
            else
            {
                fileName = this.FileName;
                path = this.Path;
            }

            if (!FileUtil.Exists(PathUtil.Combine(path, fileName)))
            {
                //MSG:[{0}] file does not exist. Please check this file (Path:{1})	
                resultList = this.HandleResult(ref resultList, ResultType.ERROR, "MSG_FTCO_00126", DefaultMessage.NON_REG_WRD + this.TargetName, DefaultMessage.NON_REG_WRD + PathUtil.Combine(path, fileName));
            }
            else
            {
                //MSG:Validation is successful
                resultList = this.HandleResult(ref resultList, ResultType.OK, "MSG_FTCO_00075", null);
            }

            return resultList;
        }
        #endregion
    }
}
