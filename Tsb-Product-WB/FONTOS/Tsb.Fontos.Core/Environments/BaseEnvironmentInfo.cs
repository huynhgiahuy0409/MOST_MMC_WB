using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Exceptions.System;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Base Environment Information class
    /// </summary>
    [Serializable]
    public abstract class BaseEnvironmentInfo : TsbBaseObject
    {
        #region FIELD AREA *************************************
        protected string _sourcePath = string.Empty;
        protected string _configFileName = string.Empty;
        #endregion


        #region PROPERTY AREA **********************************
        /// <summary>
        /// Source path of culture information configuration
        /// </summary>
        public string SourcePath
        {
            get { return _sourcePath; }
        }

        /// <summary>
        /// Configuration file name
        /// </summary>
        public string ConfigFileName
        {
            get { return _configFileName; }
        }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseEnvironmentInfo()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-BaseEnvironmentInfo";
        }
        #endregion


        /// <summary>
        /// Returns valid configuration setting value
        /// </summary>
        /// <param name="configProvider">Configruation Provider's object reference</param>
        /// <param name="sectionName">Section name value to read</param>
        /// <param name="settingName">Setting name value to read</param>
        /// <returns>A valid configuration setting value</returns>
        protected virtual string GetValidValue(ref XmlConfigProvider configProvider, string sectionName, string settingName)
        {
            return GetValidValue(ref configProvider, sectionName, settingName, true);
        }

        /// <summary>
        /// Returns valid configuration setting value
        /// </summary>
        /// <param name="configProvider">Configruation Provider's object reference</param>
        /// <param name="sectionName">Section name value to read</param>
        /// <param name="settingName">Setting name value to read</param>
        /// <returns>A valid configuration setting value</returns>
        protected virtual string GetValidValue(ref XmlConfigProvider configProvider, string sectionName, string settingName, bool checkExist)
        {
            string rtnValue = string.Empty;

            object objValue = configProvider.GetValue(sectionName, settingName);

            if (objValue != null)
            {
                rtnValue = objValue.ToString();
            }

            if (checkExist == true && string.IsNullOrEmpty(rtnValue))
            {
                //MSG:Configuration file reading error. [section-{0}][setting-{1}] could not found in {2} file.
                throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00003", sectionName, settingName, this.ConfigFileName);
            }

            return rtnValue;
        }

        /// <summary>
        /// Returns valid configuration setting value
        /// Not Excpetion
        /// </summary>
        /// <param name="configProvider">Configruation Provider's object reference</param>
        /// <param name="sectionName">Section name value to read</param>
        /// <param name="settingName">Setting name value to read</param>
        /// <returns>A valid configuration setting value</returns>
        protected virtual string GetValidValueNotException(ref XmlConfigProvider configProvider, string sectionName, string settingName)
        {
            string rtnValue = string.Empty;

            object objValue = configProvider.GetValue(sectionName, settingName);

            if (objValue != null)
            {
                rtnValue = objValue.ToString();
            }

            return rtnValue;
        }

        /// <summary>
        /// Returns valid type which is converted suitable architecture type
        /// </summary>
        /// <typeparam name="T">type to convert</typeparam>
        /// <param name="settingValue">setting value string</param>
        /// <param name="sectionName">section name</param>
        /// <param name="settingName">setting name</param>
        /// <returns>converted valid type</returns>
        protected virtual T GetValidType<T>(string settingValue, string sectionName, string settingName)
        {
            T rtnEnumType;

            try
            {
                rtnEnumType = (T)Enum.Parse(typeof(T), settingValue);
            }
            catch (ArgumentException argEx)
            {
                //MSG:Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.
                throw new TsbSysConfigException(argEx, this.ObjectID, "MSG_FTCO_00004", sectionName, settingName, this.ConfigFileName);
            }


            return rtnEnumType;
        }

        /// <summary>
        /// Returns valid type which is converted suitable architecture type
        /// </summary>
        /// <typeparam name="T">type to convert</typeparam>
        /// <param name="settingValue">setting value string</param>
        /// <param name="sectionName">section name</param>
        /// <param name="settingName">setting name</param>
        /// <returns>converted valid type</returns>
        protected virtual T GetValidTypeNotException<T>(string settingValue, string sectionName, string settingName)
        {
            T rtnEnumType = default(T);

            Array validTypes = Enum.GetValues(typeof(T));

            foreach (T validType in validTypes)
            {
                if (validType.ToString() == settingValue)
                {
                    rtnEnumType = (T)Enum.Parse(typeof(T), settingValue);
                }
            }

            return rtnEnumType;
        }
    }
}
