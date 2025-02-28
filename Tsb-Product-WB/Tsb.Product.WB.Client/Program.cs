using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using Tsb.Fontos.Core.Caches;
using Tsb.Fontos.Core.Codes;
using Tsb.Fontos.Core.Codes.Type;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Win.Exceptions;
using Tsb.Fontos.Win.Grid.CellStyle;
using Tsb.Fontos.Win.Grid.Schema;
using Tsb.Fontos.Win.Grid.Types;
using Tsb.Fontos.Win.Style;
using Tsb.Product.WB.Common.Constance;
using static Tsb.Product.WB.Common.Constance.WeightBridgeConstance;

namespace Tsb.Product.WB.Client
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TpMenuView());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "/ * /" + ex.StackTrace);
            }
            finally
            {
                Console.ReadLine();
            }

            return;
        }



        #region METHOD AREA (UNHANDLED EXCEPTION HANDLING)******
        /// <summary>
        /// Current Domain UnHandledException Event Handler
        /// </summary>
        /// <param name="sender">The source of the unhandled exception event.</param>
        /// <param name="e">An UnhandledExceptionEventArgs that contains the event data.</param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            ErrorReporterView errorReporter = null;
            DialogResult dialogResult;

            try
            {
                if (e.ExceptionObject is TsbBaseException)
                {
                    errorReporter = new ErrorReporterView(e.ExceptionObject as TsbBaseException);
                }
                else
                {
                    errorReporter = new ErrorReporterView(e.ExceptionObject as Exception);
                }

                dialogResult = errorReporter.ShowDialog();
            }
            finally
            {
                Application.Exit();
            }
            return;
        }

        /// <summary>
        /// ThreadException Event Handler
        /// </summary>
        /// <param name="sender">The source of the ThreadException exception event.</param>
        /// <param name="e">An ThreadExceptionEventArgs that contains the event data.</param>
        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ErrorReporterView errorReporter = null;
            DialogResult dialogResult;

            try
            {
                if (e.Exception is TsbBaseException)
                {
                    errorReporter = new ErrorReporterView(e.Exception as TsbBaseException);
                }
                else
                {
                    errorReporter = new ErrorReporterView(e.Exception as Exception);
                }

                dialogResult = errorReporter.ShowDialog();
            }
            finally
            {
                Application.Exit();
            }
            return;
        }

        #endregion

    }
}
