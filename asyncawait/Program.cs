using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace asyncawait
{
	class Program
	{
		[ThreadStatic]
		static int value= 1;
		
		public static void Main()
		{
            for (int i = 0; i < 1000; i++)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
					value++;
                    Console.WriteLine(value);
                    Thread.Sleep(1000);
                });
            }
            Console.ReadLine();
        }
	}

	static class MyThreadPool
    {
		private static readonly BlockingCollection<Action> _bc = new BlockingCollection<Action>();

		public static void QueueUserWorkItem(Action action)
        {
			_bc.Add(action);
        }

		static MyThreadPool()
        {
			for (int i=0; i<Environment.ProcessorCount; i++)
            {
				new Thread(() =>
				{
					while (true)
                    {
						var workItem = _bc.Take();
						workItem();
                    }
				}) { IsBackground = true }.Start();
            }
        }
    }
}