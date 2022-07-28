using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TaskTimerApp
{
    internal class TimerTask:IDisposable
    {
        /// <summary>
        /// stop running 
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private readonly AutoResetEvent _runMutex = new AutoResetEvent(false);

        public delegate void MinRunOn(DateTime time);

        public event MinRunOn MinRunEvent;

        public void Start()
        {
            var t = new Thread(MinRun);
            t.IsBackground = true;
            t.Start();
        }
        private void MinRun()
        {
            var dt = DateTime.Now;
            if (dt.Second == 0 && dt.Minute == 0 && dt.Hour == 0)
            {
                Thread.Sleep(1000);
            }
            try
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    dt = DateTime.Now;
                    bool b = _runMutex.WaitOne(60000 - dt.Second * 1000 - dt.Millisecond);
                    if (b)
                    {
                        continue;
                    }
                    try
                    {
                        Console.WriteLine($"[{DateTime.Now}] - execute");
                        MinRunEvent?.Invoke(dt);
                        //GenDataTimerEvent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"[{DateTime.Now}] - error - {e.Message}");
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _runMutex.Set();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
