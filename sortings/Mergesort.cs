using System;
namespace tricks.sortings
{
    public class Mergesort : SortBase
    {
        public int Length => _array.Length;

        public Mergesort(int[] arr) : base(arr)
        {}

        public void Run()
        {
            Run(0, Length - 1);
        }

        private void Run(int start, int end)
        {
            if (end - start <= 0)
                return;

            if (end - start == 1)
            {
                if (_array[start] > _array[end])
                    Swap(start, end);

                return;
            }

            var len = end - start + 1; // start 2 end 8 len 6
            var median = start + len / 2; // 2 + 3 = 5

            var start2 = median + 1;

            Run(start, median); // 2, 4
            Run(start2, end); // 6, 8

            // merge
            int i = start;
            int j = start2;

            var buffer = new int[len];
            int idx = 0;

            while (i <= median && j <= end)
            {
                if (_array[i] < _array[j])
                    buffer[idx++] = _array[i++];
                else
                    buffer[idx++] = _array[j++];
            }

            while (j <= end)
                buffer[idx++] = _array[j++];

            while (i <= median)
                buffer[idx++] = _array[i++];

            Array.Copy(buffer, 0, _array, start, len);
        }
    }
}
