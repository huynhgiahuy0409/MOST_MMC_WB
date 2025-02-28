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
* DATE           AUTHOR		       REVISION    	
* 2010.01.25    CHOI 1.0	    First release.
* 2011.01.18  Tonny.Kim 1.1     Add a comments
*/
#endregion

using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

using log4net;
using log4net.Core;
using log4net.Config;
using log4net.Repository;
using log4net.Util;
using System.Xml;
using System.Configuration;
using Tsb.Fontos.Core.Objects;
using System.Diagnostics;

namespace Tsb.Fontos.Core.Logging
{
    public class LogFactory : TsbBaseObject
    {
        #region CONST AREA *************************************
        private const string APP_LOG_PATH_KEY = "Config.App.Log";
        private static readonly WrapperMap s_wrapperMap = new WrapperMap(new WrapperCreationHandler(WrapperCreationHandler));
        #endregion

        #region INITIALIZE AREA ********************************
        private LogFactory()
        {
            this.ObjectID = "GNR-FTCO-LOG-LogFactory";
        }

        static LogFactory()
        {
            log4net.GlobalContext.Properties["PID"] = Process.GetCurrentProcess().Id.ToString();
            Environment.SetEnvironmentVariable("PID", Process.GetCurrentProcess().Id.ToString(), EnvironmentVariableTarget.Process);
        }
        #endregion

        #region METHOD AREA ************************************
        public static ITsbLog GetLogger(string name)
        {
            return GetLogger(Assembly.GetCallingAssembly(), name);
        }


        private static ITsbLog GetLogger(Assembly assembly, string name)
        {
            return WrapLogger(LoggerManager.GetLogger(assembly, name));
        }


        public static ITsbLog GetLogger(Type type)
        {
            return GetLogger(Assembly.GetCallingAssembly(), type);
        }


        private static ITsbLog GetLogger(Assembly assembly, Type type)
        {
            return WrapLogger(LoggerManager.GetLogger(assembly, type));
        }


        private static ITsbLog WrapLogger(ILogger logger)
        {
            return (ITsbLog)s_wrapperMap.GetWrapper(logger);
        }


        private static ILoggerWrapper WrapperCreationHandler(ILogger logger)
        {
            Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
            return new TsbLogImpl(logger);
        }


        static public void Configure(ILoggerRepository repository)
        {
            try
            {
                // file path
                string configLocation = ConfigurationManager.AppSettings[APP_LOG_PATH_KEY];
                string currentDirectory = Environment.CurrentDirectory;
                configLocation = currentDirectory + configLocation;
                try
                {
                    if (string.IsNullOrEmpty(configLocation))
                    {
                        Console.WriteLine("[ Warning!!! ] Failed to find configuration section 'log4net' in the application's .config file. "
                                    + "Check your .config file for the <appSettings> elements. "
                                    + "The configuration appSettings should look like: "
                                    + "<add key=\"log4net.configLocation\" value=\"~/../log4net_config.xml\"/>");
                    }
                    else
                    {
                        if (File.Exists(configLocation))
                        {
                            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(configLocation));
                            return;
                        }                        
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("[ Error!!! ] file path : " + configLocation + "\n\r "
                                    + "======================================================="
                                    + e.StackTrace);
                }

                // App.config 
                XmlElement configElement = null;

                configElement = System.Configuration.ConfigurationManager.GetSection("log4net") as XmlElement;

                if (configElement == null)
                {
                    Console.WriteLine("[ Warning!!! ] Failed to find configuration section 'log4net' in the application's .config file. "
                                    + "Check your .config file for the <log4net> and <configSections> elements. "
                                    + "The configuration section should look like: "
                                    + "<section name=\"log4net\" type=\"log4net.Config.Log4NetConfigurationSectionHandler,log4net\" />");
                    BasicConfigurator.Configure();
                }
                else
                {
                    ConfigureFromXml(repository, configElement);
                }
            }
            catch (System.Configuration.ConfigurationException confEx)
            {
                if (confEx.BareMessage.IndexOf("Unrecognized element") >= 0)
                {
                    Console.WriteLine("[ Warning!!! ] Failed to parse config file. Check your "
                                    + ".config file is well formed XML.", confEx);
                }
                else
                {
                    string configSectionStr = "<section name=\"log4net\" type=\"log4net.Config.Log4NetConfigurationSectionHandler,"
                                                + Assembly.GetExecutingAssembly().FullName + "\" />";
                    Console.WriteLine("[ Warning!!! ] Failed to parse config file. "
                                    + "Is the <configSections> specified as: " + configSectionStr, confEx);
                }

                BasicConfigurator.Configure();
            }

        }

        static private void ConfigureFromXml(ILoggerRepository repository, XmlElement element)
        {
            IXmlRepositoryConfigurator configurableRepository = repository as IXmlRepositoryConfigurator;
            if (configurableRepository == null)
            {
                Console.WriteLine("[ Warning!!! ] Repository [" + repository
                                + "] does not support the XmlConfigurator");
            }
            else
            {
                XmlDocument newDoc = new XmlDocument();
                XmlElement newElement = (XmlElement)newDoc.AppendChild(newDoc.ImportNode(element, true));

                configurableRepository.Configure(newElement);
            }
        }
        #endregion
    }
}
