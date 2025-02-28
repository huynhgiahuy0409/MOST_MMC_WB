using System;
using System.Windows.Forms;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Logging;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Win.Message;

namespace Tsb.Catos.Cm.Mobile.Common.Handler
{
    public class ErrorMessageHandler : TsbBaseObject
    {
        #region CONST & FIELD AREA ********************************************
        private readonly string OBJECT_ID = "CTL-CT-CTMO-CM-ErrorMessageHandler";
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************
        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessageHandler"/> class.
        /// </summary>
        public ErrorMessageHandler()
        {
            this.ObjectID = OBJECT_ID;
        }
        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA ***************************************************
        /// <summary>
        /// Shows the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="objectID">The object ID.</param>
        /// <param name="isWriteLog">if set to <c>true</c> [is write log].</param>
        public static void Show(Object exception, string objectID, bool isWriteLog)
        {
            try
            {
                ITsbLog logger = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                if (logger == null)
                {
                    MessageManager.Show("WRD_CTMO_ErrorHandler", "MSG_CTMO_CM_00002", MessageBoxButtons.OK,
                                                            MessageBoxIcon.Warning, DefaultMessage.NON_REG_WRD);
                }

                if (exception is TsbBaseException)
                {
                    var ex = exception as TsbBaseException;

                    if (isWriteLog && logger != null)
                        logger.Error(ex.OriginalException + " /+/ " + ex.Tracer);

                    MessageManager.Show(ex);
                }
                else if (exception is Exception)
                {
                    var ex = exception as Exception;

                    if (isWriteLog && logger != null)
                        logger.Error(ex.Message + " /+/ " + ex.StackTrace);

                    //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                    MessageManager.Show(new TsbBaseException(ex, objectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        /// <summary>
        /// Shows the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="objectID">The object ID.</param>
        public static void Show(Object exception, string objectID)
        {
            Show(exception, objectID, true);
        }

        /// <summary>
        /// Shows the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void Show(Object exception)
        {
            Show(exception, "", true);
        }

        public static void ErrorLog(Object exception)
        {
            try
            {
                ITsbLog logger = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                if (exception is TsbBaseException)
                {
                    var ex = exception as TsbBaseException;
                    if (logger != null) logger.Error(ex.OriginalException + " /+/ " + ex.Tracer);
                }
                else if (exception is Exception)
                {
                    var ex = exception as Exception;
                    if (logger != null) logger.Error(ex.Message + " /+/ " + ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public static void ErrorLog(string errorMessage)
        {
            try
            {
                ITsbLog logger = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                if (logger != null)
                {
                    logger.Error(errorMessage);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public static void WriteLog(string msg)
        {
            try
            {
                ITsbLog logger = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                logger.Debug(msg);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public static void WriteLog4Debug(string msg)
        {
            try
            {
                ITsbLog logger = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Warn("#D# " + msg);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion METHOD AREA ************************************************
    }
}
