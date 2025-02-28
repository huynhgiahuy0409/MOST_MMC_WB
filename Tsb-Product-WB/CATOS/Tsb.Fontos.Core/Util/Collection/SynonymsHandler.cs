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
* DATE        AUTHOR		REVISION    	
* 2013.01.29  CHOI          1.0	First release.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Util.Collection
{
    /// <summary>
    /// Synonym Words Handler Class
    /// </summary>
    public class SynonymsHandler : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA**********************************
        private List<List<string>> _synonymWordsList = null;
        /// <summary>
        /// Gets or Sets list of Synonym words list.
        /// </summary>
        public List<List<string>> SynonymWordsList
        {
            get
            {
                return this._synonymWordsList;
            }
            set
            {
                this._synonymWordsList = value;
            }
        }

        private Dictionary<string, List<string>> _synonymDic = null;
        /// <summary>
        /// Gets or Sets Synonym word dictionary
        /// </summary>
        public Dictionary<string, List<string>> SynonymDic
        {
            get
            {
                return this._synonymDic;
            }
            set
            {
                this._synonymDic = value;
            }
        }
        #endregion


        #region INITIALIZATION AREA *********************************
        /// <summary>
        /// Initialize Instance
        /// </summary>
        public SynonymsHandler()
            : base()
        {
            this.ObjectID = "GNR-FTCO-UTL-SynonymsHandler";
            this._synonymWordsList = new List<List<string>>();
            this._synonymDic = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Initialize Instance using a specified list of synonym words list
        /// </summary>
        public SynonymsHandler(List<List<string>> synonymWordsList)
            : base()
        {
            this.ObjectID = "GNR-FTCO-UTL-SynonymsHandler";
            this._synonymWordsList = synonymWordsList;
            this._synonymDic = new Dictionary<string, List<string>>();
        }
        #endregion

        #region METHOD AREA (BUILD SYNONYM DICTIONARY) **************
        /// <summary>
        /// Builds Synonym Dictionary
        /// </summary>
        public void BuildDictionary()
        {
            List<string> synonymsList = null;

            foreach(List<string> wordGroupList in this._synonymWordsList)
            {
                foreach (string word in wordGroupList)
                {
                    if (this._synonymDic.ContainsKey(word) == false)
                    {
                        synonymsList = new List<string>();
                    }
                    else
                    {
                        synonymsList = this._synonymDic[word];
                    }

                    foreach (string synonym in wordGroupList)
                    {
                        if (word.Equals(synonym))
                        {
                            continue;
                        }
                        else
                        {
                            synonymsList.Add(synonym);
                        }
                    }

                    if (this._synonymDic.ContainsKey(word) == false)
                    {
                        this._synonymDic.Add(word, synonymsList);
                    }
                }
            }

            return;
        }

        #endregion


        #region METHOD AREA (CHECK SYNONYM)**************************
        
        public bool IsSynonymWord(string word, string synonym, bool ignoreCase)
        {
            bool isSynonym = false;

            List<string> synonymsList = null;

            if(ignoreCase)
            {
                if(word.Equals(synonym, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }

            if(this._synonymDic.ContainsKey(word))
            {
                synonymsList = this._synonymDic[word];

                foreach(string strSyno in synonymsList)
                {
                    if(ignoreCase)
                    {
                        isSynonym = synonym.Equals(strSyno, StringComparison.CurrentCultureIgnoreCase);
                    }
                    else
                    {
                        isSynonym = synonym.Equals(strSyno);
                    }
                    
                    if(isSynonym)
                        break;
                }
            }
            else
            {
                isSynonym = false;
            }

            return isSynonym;
        }

        #endregion
    }
}
