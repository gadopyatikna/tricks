using System;
namespace tricks.singlethreaded
{
    public class InplaceQuicksort : SortBase
    {
        public int Length => _array.Length;

        public InplaceQuicksort(int[] arr) : base(arr)
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

            var len = end - start; // start 2 end 8 len 6
            var median = start + len / 2; // 2 + 3 = 5
            var pivot = _array[median];

            Swap(median, end);

            int i = start;
            int j = end-1;

            while (i <= j)
            {
                if (_array[i] > pivot && _array[j] < pivot)
                {
                    Swap(i, j);
                    ++i;
                    --j;
                    continue;
                }

                if (! (_array[i] > pivot) )
                    ++i;

                if ( !(_array[j] < pivot) )
                    --j;
            }

            Swap(i, end);

            Run(start, i - 1); // 2, 4
            Run(i + 1, end); // 6, 8
        }

        private void Swap(int idx1, int idx2)
        {
            var tmp = _array[idx1];
            _array[idx1] = _array[idx2];
            _array[idx2] = tmp;
        }
    }
}
