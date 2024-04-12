using System;
using System.Threading;

namespace queue.@lock
{
    public class MyLock
    {

        public MyLock()
        {
        }

        // lock-free == non-blocking ?
        // non-blocking iff any thread suspension
        // doesnt IMPLY other thread inability to finish it's work
        
        private long Blocking_L;
        public void Blocking_WithExchanged()
        {
            // t2 is depentent on t1 state
            while (Interlocked.CompareExchange(ref Blocking_L, 1, 0) == 0);
            try
            {
                // t1 suspended here 
                DoWork();
            }
            finally
            {
                Interlocked.Exchange(ref Blocking_L, 0);
            }
        }

        private object Blocking_Lock;
        public void Blocking_WithLock()
        {
            // t2 is depentent on t1 state
            lock (Blocking_Lock)
            {
                // t1 suspended here 
                DoWork();
            }
        }

        // if any thread suspended
        // other threads proceed without impact

        // overall progress guaranteed
        // individual thread is not guaranteed to make any progress

        // priority inversion
        private long LockFree_Acc;
        public void LockFree()
        {
            long old;
            do
            {
                old = LockFree_Acc;
            } while (Interlocked.CompareExchange(ref LockFree_Acc, old + 1, old) == old);
        }

        // any thread is guaranteed to reach its goal
        // in a finite amount of steps
        private long WaitFree_Acc;
        public void WaitFree()
        {
            Interlocked.Increment(ref WaitFree_Acc);
        }

        // obstruction free
        // if all threads but ONE are suspended
        // that thread is guaranteed to perform the work

        private void DoWork() => Thread.Sleep(1000);
    }
}
