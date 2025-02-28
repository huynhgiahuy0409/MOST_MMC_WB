using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Threads.Menu
{
    /// <summary>
    /// Provides a mechanism for threads of menu
    /// </summary>
    public class ManageMenuThreads
    {
        //private static ToolStripProgressBar pBar;
        private static int usingResource = 0;


        public static void ThreadStartProgressBar(object endStep)
        {
            //FormCollection frmCol = Application.OpenForms;
            //Form frm = frmCol[0];
            //System.Windows.Forms.Control.ControlCollection cr = frmCol[0].Controls;
            //StatusStrip st = null;
            //for (int i = 0; i < cr.Count; i++)
            //{
            //    if (cr[i].GetType().Equals(typeof(StatusStrip)))
            //    {
            //        st = cr[i] as StatusStrip;
            //        break;
            //    }
            //}
            //for (int i = 0; i < st.Items.Count; i++)
            //{
            //    if (st.Items[i].GetType().Equals(typeof(ToolStripProgressBar)))
            //    {
            //        pBar = st.Items[i] as ToolStripProgressBar;
            //        break;
            //    }
            //}
            Form mdiFrm = endStep as Form;

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("ThreadStartProgressBar: {0}", i);
                    // Yield the rest of the time slice.
                    //pBar.ProgressBar.Value = 10;
                    UseResource(mdiFrm);
                    //Thread.Sleep(0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //A simple method that denies reentrancy.
        public static bool UseResource(Form mdiFrm)
        {
            //0 indicates that the method is not in use.
            if (0 == Interlocked.Exchange(ref usingResource, 1))
            {
                InvokeStartMenuProgressBar(mdiFrm);

                //Release the lock
                Interlocked.Exchange(ref usingResource, 0);
                return true;
            }
            else
            {
                InvokeStartMenuProgressBar(mdiFrm);
                return false;
            }
        }


        private static void InvokeStartMenuProgressBar(Form mdiFrm)
        {
            try
            {
                mdiFrm.BeginInvoke(performHelper, mdiFrm);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        private static Action<Form> performHelper = new Action<Form>(PerformMenuPregressBarStep);
        private static void PerformMenuPregressBarStep(Form mdiFrm)
        {
            Console.WriteLine("PerformMenuPregressBarStep: {0}", "PerformMenuPregressBarStep");
            //pBar.ProgressBar.Step = 10;
            //pBar.ProgressBar.MarqueeAnimationSpeed = 1;
            //if (endStep.Equals(10))
            //{
            //    pBar.ProgressBar.Value = 100;
            //    Thread.Sleep(1000);
            //    pBar.ProgressBar.Value = 0;
            //}
            //else
            {
                //pBar.ProgressBar.PerformStep();
                //if (pBar.ProgressBar.Value.Equals(100))
                //{                    
                //    pBar.ProgressBar.Value = 100;
                //    TimerCallback timerDelegate = new TimerCallback(InitPrg);
                //    System.Threading.Timer stateTimer = new System.Threading.Timer(timerDelegate, null, 5000, 0);
                //}
            }
            mdiFrm.Name += "TEST^^";
        }

        //private static void InitPrg(object obj)
        //{
        //    pBar.ProgressBar.BeginInvoke(initPrgHelper, "initProgress");
        //}

        //private static Action<string> initPrgHelper = new Action<string>(InitProgressBar);
        //private static void InitProgressBar(string endStep)
        //{
        //    pBar.ProgressBar.Value = 0;
        //}
    }
}
