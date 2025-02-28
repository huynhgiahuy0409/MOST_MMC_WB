using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Configuration;

namespace Tsb.Fontos.Core.Exceptions
{
    /// <summary>
    /// Detail Exception Message Formatter
    /// </summary>
    public class DetailExceptionFormatter : TsbBaseObject
    {
        #region INITIALIZATION AREA ****************************
        public DetailExceptionFormatter()
        {
            this.ObjectID = "GNR-FTCO-EXP-DetailException";
        } 
        #endregion

        /// <summary>
        /// Get Whether to view detailed messages
        /// </summary>
        /// <returns>Detailed message use status</returns>
        #region METHOD(Detail Exception) AREA **************
        public static bool GetShowDetailExceptionConfig()
        {
            string isShowDetailException = "false";

            AppConfigProvider configProvider = (AppConfigProvider)ConfigContext.GetAppConfigProvider();
            isShowDetailException = configProvider.GetValue("ShowDetailException");

            if (string.IsNullOrEmpty(isShowDetailException))
            {
                isShowDetailException = "false";
            }

            return bool.Parse(isShowDetailException);
        }

        /// <summary>
        /// Returns excpeiton message using args
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Formatted excpeiton message</returns>
        public static string GetInnerMassage(Exception ex)
        {
            Exception innerException = ex.InnerException;
            string innerMassage = string.Empty;

            while (innerException != null)
            {
                innerMassage += " " + innerException.Message;
                innerException = innerException.InnerException;
            }

            innerMassage = innerMassage.Remove(0, 1);

            return innerMassage;
        }
        #endregion
    }
}
