using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace tricks.parallel
{

    class PCQueuePulse
    {
        private object _lock = new object();
        private Queue<string> _queue = new Queue<string>();
        private Thread _worker;

        public PCQueuePulse()
        {
            _worker = new Thread(Worker);
            _worker.Start();
        }

        public void AddTask(string action)
        {
            lock (_lock)
            {
                _queue.Enqueue(action);
                Monitor.Pulse(_lock);
            }
        }

        public void Worker()
        {
            while (true)
            {
                string workLoad = null;
                lock (_lock)
                {
                    while (_queue.Count < 1) Monitor.Wait(_lock);
                        workLoad = _queue.Dequeue();
                }

                if (workLoad != null)
                {
                    Task.Delay(50);
                    Console.WriteLine($"Phew, so tired! Did task no {workLoad}");
                }  
                else
                    return;
            }
        }
    }
}
