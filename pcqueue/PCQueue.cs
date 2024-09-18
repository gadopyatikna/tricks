using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace tricks.parallel
{

    class PCQueue
    {
        private AutoResetEvent _event = new AutoResetEvent(false);
        private object _lock = new object();
        private Queue<string> _queue = new Queue<string>();
        private Thread _worker;

        public PCQueue()
        {
            _worker = new Thread(Worker);
            _worker.Start();
        }

        public void AddTask(string action)
        {
            lock (_lock)
            {
                _queue.Enqueue(action);
            }
            _event.Set();
        }

        public void Worker()
        {
            while (true)
            {
                string workLoad = null;
                lock (_lock)
                {
                    if (_queue.Count > 0)
                    {
                        workLoad = _queue.Dequeue();
                        if (workLoad == null)
                            return;
                    }
                }
                if (workLoad != null)
                {
                    Task.Delay(50);
                    Console.WriteLine($"Phew, so tired! Did task no {workLoad}");
                }
                //else
                //    _event.WaitOne();
            }
        }
    }

    //class MainClass
    //{
    //    public static void Main(string[] args)
    //    {
    //        Console.WriteLine("Hello World!");

    //        var q = new PCQueuePulse();
    //        for (int i = 0; i < 5; i++)
    //        {
    //            q.AddTask("task" + i.ToString());
    //        }
    //        q.AddTask(null);
    //    }

    //    private static void SimulateHeavyLoad(int i)
    //    {
    //        Task.Delay(50);
    //        Console.WriteLine($"Phew, so tired! Did task no {i}");
    //    }
    //}
}
