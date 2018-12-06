using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CarCatalog
{
    internal static class ProgressBar
    {
        #region Fields
        private static bool _isInProgress;
        private static char _sign = '|';
        private static readonly object Mutex = new object();
        private static Thread _thread;
        #endregion

        public static bool IsInProgress
        {
            get { return _isInProgress; }
            set
            {
                lock (Mutex)
                {
                    _isInProgress = value;
                }
            }
        }

        public static void Start()
        {
            IsInProgress = true;

            //GC aware
            _thread = (new Thread(() =>
                            {
                                while (_isInProgress)
                                {
                                    _sign = _sign == '|' ? '-' : '|';
                                    Console.Write("\r{0}", _sign);
                                    Thread.Sleep(100);
                                }
                            }));

            _thread.Start();
        }

        public static void Stop()
        {
            IsInProgress = false;
        }
    }
}
