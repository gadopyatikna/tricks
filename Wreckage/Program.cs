using System;
using System.Threading;

namespace Wreckage
{
    class Test
    {
        public bool myBool;
        private static object myLock = new object();
        public int Foo()
        {
            lock (myLock)
            {
                int cnt = 0;
                while (!myBool) cnt++;
                return cnt;
            }
        }
    }

    class MainClass
    {
        static void Main(string[] args)
        {
            var obj = new Test();
            new Thread(() => {
                Thread.Sleep(1000);
                obj.myBool = true;
            }).Start();
            Console.WriteLine(obj.Foo());
        }

    }
}
