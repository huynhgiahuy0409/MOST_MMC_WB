using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using Tsb.Fontos.Core.Item;
using System.Reflection;
using Tsb.Fontos.Core.Util;

namespace Tsb.Fontos.Core.Threads.Parallel
{
    public class ParallelUtil : BaseUtil
    {
        #region PROPERTY AREA ***************************************
        public static int AvailableThreadCount
        {
            get
            {
                return System.Environment.ProcessorCount / 2;
            }
        }

        public static EventHandler Completed = delegate { }; 
        #endregion

        #region INTIALIZE AREA **************************************
        private ParallelUtil()
        {

        } 
        #endregion

        #region METHOD AREA *****************************************
        #region FUNCS
        public static T[] ExecuteParallel<T>(params Func<T>[] funcs)
        {
            if (funcs == null) return null;

            long counter = funcs.Length;
            T[] result = new T[funcs.Length];
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < funcs.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    result[actionIndex] = funcs[actionIndex]();
                    // Tell the calling thread that we're done
                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }
                }), i);
            }
            // Wait for all threads to execute
            resetEvents.WaitOne();
            Completed(funcs, EventArgs.Empty);
            return result;
        }

        public static T[] ExecuteParallel<T, U>(U[] param1, params Func<U, T>[] funcs)
        {
            if (funcs == null) return null;
            long counter = funcs.Length;
            T[] result = new T[funcs.Length];
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < funcs.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    result[actionIndex] = funcs[actionIndex](param1[actionIndex]);
                    // Tell the calling thread that we're done
                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }
                }), i);
            }
            // Wait for all threads to execute
            resetEvents.WaitOne();
            Completed(funcs, EventArgs.Empty);
            return result;
        }

        public static T[] ExecuteParallel<T, U, V>(U[] param1, V param2, params Func<U, V, T>[] funcs)
        {
            if (funcs == null) return null;
            long counter = funcs.Length;
            T[] result = new T[funcs.Length];
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < funcs.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    result[actionIndex] = funcs[actionIndex](param1[actionIndex], param2);
                    // Tell the calling thread that we're done
                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }
                }), i);
            }
            // Wait for all threads to execute
            resetEvents.WaitOne();
            Completed(funcs, EventArgs.Empty);
            return result;
        }

        public static T[] ExecuteParallel<T, U, V, W>(U[] param1, V param2, W param3, params Func<U, V, W, T>[] funcs)
        {
            if (funcs == null) return null;
            long counter = funcs.Length;
            T[] result = new T[funcs.Length];
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < funcs.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    result[actionIndex] = funcs[actionIndex](param1[actionIndex], param2, param3);
                    // Tell the calling thread that we're done
                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }
                }), i);
            }
            // Wait for all threads to execute
            resetEvents.WaitOne();
            Completed(funcs, EventArgs.Empty);
            return result;
        }

        public static T[] ExecuteParallel<T, U, V, W, X>(U[] param1, V param2, W param3, X param4, params Func<U, V, W, X, T>[] funcs)
        {
            if (funcs == null) return null;
            long counter = funcs.Length;
            T[] result = new T[funcs.Length];
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < funcs.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    result[actionIndex] = funcs[actionIndex](param1[actionIndex], param2, param3, param4);
                    // Tell the calling thread that we're done
                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }
                }), i);
            }
            // Wait for all threads to execute
            resetEvents.WaitOne();
            Completed(funcs, EventArgs.Empty);
            return result;
        } 
        #endregion

        #region ACTIONS
        public static void ExecuteParallel(params Action[] actions)
        {
            if (actions == null) return;
            long counter = actions.Length;
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < actions.Length; i++)
            {
                //resetEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    actions[actionIndex]();

                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }

                    // Tell the calling thread that we're done
                    //resetEvents[actionIndex].Set();
                }), i);
            }
            // Wait for all threads to execute
            //WaitHandle.WaitAll(resetEvents); // STA 환경에선 지원안됨.
            resetEvents.WaitOne();
            Completed(actions, EventArgs.Empty);
        }

        public static void ExecuteParallel<T>(T[] param1, params Action<T>[] actions)
        {
            if (actions == null) return;
            long counter = actions.Length;
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < actions.Length; i++)
            {
                //resetEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    actions[actionIndex](param1[actionIndex]);

                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }

                    // Tell the calling thread that we're done
                    //resetEvents[actionIndex].Set();
                }), i);
            }
            // Wait for all threads to execute
            //WaitHandle.WaitAll(resetEvents); // STA 환경에선 지원안됨.
            resetEvents.WaitOne();
            Completed(actions, EventArgs.Empty);
        }

        public static void ExecuteParallel<T, U>(T[] param1, U param2, params Action<T, U>[] actions)
        {
            if (actions == null) return;
            long counter = actions.Length;
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < actions.Length; i++)
            {
                //resetEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    actions[actionIndex](param1[actionIndex], param2);

                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }

                    // Tell the calling thread that we're done
                    //resetEvents[actionIndex].Set();
                }), i);
            }
            // Wait for all threads to execute
            //WaitHandle.WaitAll(resetEvents); // STA 환경에선 지원안됨.
            resetEvents.WaitOne();
            Completed(actions, EventArgs.Empty);
        }

        public static void ExecuteParallel<T, U, V>(T[] param1, U param2, V param3, params Action<T, U, V>[] actions)
        {
            if (actions == null) return;
            long counter = actions.Length;
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < actions.Length; i++)
            {
                //resetEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    actions[actionIndex](param1[actionIndex], param2, param3);

                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }

                    // Tell the calling thread that we're done
                    //resetEvents[actionIndex].Set();
                }), i);
            }
            // Wait for all threads to execute
            //WaitHandle.WaitAll(resetEvents); // STA 환경에선 지원안됨.
            resetEvents.WaitOne();
            Completed(actions, EventArgs.Empty);
        }

        public static void ExecuteParallel<T, U, V, W>(T[] param1, U param2, V param3, W param4, params Action<T, U, V, W>[] actions)
        {
            if (actions == null) return;
            long counter = actions.Length;
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent resetEvents = new ManualResetEvent(false);
            // Launch each method in it's own thread
            for (int i = 0; i < actions.Length; i++)
            {
                //resetEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;
                    // Execute the method
                    actions[actionIndex](param1[actionIndex], param2, param3, param4);

                    Interlocked.Decrement(ref counter);

                    if (Interlocked.Read(ref counter) == 0)
                    {
                        resetEvents.Set();
                    }

                    // Tell the calling thread that we're done
                    //resetEvents[actionIndex].Set();
                }), i);
            }
            // Wait for all threads to execute
            //WaitHandle.WaitAll(resetEvents); // STA 환경에선 지원안됨.
            resetEvents.WaitOne();
            Completed(actions, EventArgs.Empty);
        }  
        #endregion
        #endregion
    }
}
