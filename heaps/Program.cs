using System;

namespace heaps
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var h = new Heap(new int[] { -1, 4, 5, 2, 7, 1 });
            h.BuildMaxHeap();
            Console.WriteLine(h._data);
        }
    }
}
