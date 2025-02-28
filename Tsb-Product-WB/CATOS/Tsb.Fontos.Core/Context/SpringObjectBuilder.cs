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
* 2009.07.22    CHOI 1.0	First release.
* 
*/
#endregion

using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Context
{
    /// <summary>
    /// Spring Object Builder class
    /// </summary>
    public class SpringObjectBuilder : TsbBaseObject, IObjectBuilder
    {
        private string[] _fileNames;
        private static IApplicationContext applicationContext;
        private static bool _isInitCompleted;

        public string[] FileNames
        {
            get { return _fileNames; }
            set { _fileNames = value; }
        }

        public static bool IsInitCompleted
        {
            get
            {
                return _isInitCompleted;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SpringObjectBuilder()
        {
            this.ObjectID = "GNR-FTCO-CTX-SpringObjectBuilder";
        }

        /// <summary>
        /// Initilize this object with specified configuration(application context) fileNames
        /// </summary>
        /// <param name="fileNames"></param>
        public SpringObjectBuilder(string[] fileNames)
        {
            this.FileNames = fileNames;
        }

        /// <summary>
        /// Returns spring IApplicationContext
        /// </summary>
        /// <returns>IApplicationContext Reference</returns>
        private static IApplicationContext GetApplicationContext()
        {
            try
            {
                if (applicationContext == null)
                {
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string currentDirectory = Environment.CurrentDirectory;

                    if (baseDirectory.Equals(currentDirectory) == false)
                    {
                        Environment.CurrentDirectory = baseDirectory;
                    }

                    IApplicationContext ctx = ContextRegistry.GetContext();
                    System.Collections.IDictionary dic = ctx.GetObjectsOfType(typeof(SpringObjectBuilder));
                    SpringObjectBuilder objectBuilder = null;

                    if (dic != null && dic.Count > 1)
                    {
                        foreach (System.Collections.DictionaryEntry value in dic)
                        {
                            if (objectBuilder == null)
                            {
                                objectBuilder = value.Value as SpringObjectBuilder;
                            }
                            else
                            {
                                SpringObjectBuilder additionalObjectBuilder = value.Value as SpringObjectBuilder;
                                IList<string> fileNameList = objectBuilder.FileNames.ToList();
                                if (additionalObjectBuilder != null && fileNameList != null)
                                {
                                    foreach (string fileName in additionalObjectBuilder.FileNames)
                                    {
                                        if (fileNameList.Contains(fileName) == false)
                                        {
                                            fileNameList.Add(fileName);
                                        }
                                    }

                                    objectBuilder.FileNames = fileNameList.ToArray<string>();
                                }
                            }
                        }
                    }
                    else
                    {
                        objectBuilder = (SpringObjectBuilder)ctx.GetObject("applicationContext");
                    }

                    applicationContext = new XmlApplicationContext(objectBuilder.FileNames);
                    _isInitCompleted = true;
                }
            }
            catch (System.Configuration.ConfigurationErrorsException ex)
            {
                if (ex.Message.Contains("Could not find file") || ex.Message.Contains("cannot be resolved to local file path"))
                {
                    IApplicationContext ctx = new XmlApplicationContext(AppPathInfo.PATH_APP_CONTEXT + "\\ApplicationContext-" + ModuleInfo.ModuleName + ".xml");
                    SpringObjectBuilder objectBuilder = (SpringObjectBuilder)ctx.GetObject("applicationContext");
                    applicationContext = new XmlApplicationContext(objectBuilder.FileNames);
                    _isInitCompleted = true;

                    return applicationContext;
                }
                else
                {
                    GeneralLogger.Error(ex);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }

            return applicationContext;
        }

        /// <summary>
        /// Returns created object reference
        /// </summary>
        /// <param name="name">object id to create</param>
        /// <returns>Created object reference</returns>
        public object GetObject(string name)
        {
            object rtnObject = null;

            try
            {
                rtnObject = GetApplicationContext().GetObject(name);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00007", DefaultMessage.NON_REG_WRD + name);
            }

            return rtnObject;
        }

        /// <summary>
        /// Returns created object reference 
        /// </summary>
        /// <param name="name">object id to create</param>
        /// <param name="arguments">Arguments to create specivied name object</param>
        /// <returns>Created object reference</returns>
        public object GetObject(string name, object[] arguments)
        {
            object rtnObject = null;

            try
            {
                rtnObject = GetApplicationContext().GetObject(name, arguments);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00007", DefaultMessage.NON_REG_WRD + name);
            }
            return rtnObject;
        }

        /// <summary>
        /// Checks whether this object factory contain an object with the given name.
        /// </summary>
        /// <param name="name">The name of the object to query.</param>
        /// <returns>True if an object with the given name is defined.</returns>
        public bool ContainsObject(string name)
        {
            return GetApplicationContext().ContainsObject(name);
        }
    }
}
