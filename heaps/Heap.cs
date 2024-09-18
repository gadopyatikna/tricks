using System;

namespace queue.heaps
{
    public class Heap
    {
        private int[] _data;
        private int _count = 0;

        private int _topIdx = 1;

        public Heap()
        {
            _data = new int[100];
        }

        public void Insert(int newData)
        {
            if (++_count >= _data.Length)
                Resize();

            var currentMax = _data[_topIdx];
            _data[_count] = currentMax;
            _data[_topIdx] = newData;


            // greater than both children
            // and smaller than parent
            if (IsOrderPreserved(_topIdx))
                return; // all good

            MaxHeapify(_topIdx);
        }

        private void MaxHeapify(int idx)
        {
            var l = GetLeft(idx);
            var r = GetRight(idx);

            var largestIdx = idx;

            if (l <= _count && _data[l] > _data[idx])
                largestIdx = l;

            if (r <= _count && _data[r] > _data[largestIdx])
                largestIdx = r;

            if (largestIdx != idx)
            {
                var newData = _data[idx];
                var largestData = _data[largestIdx];

                _data[largestIdx] = newData;
                _data[idx] = largestData;

                MaxHeapify(largestData);
            }
        }

        private bool IsOrderPreserved(int idx)
        {
            return (_data[idx] > _data[GetLeft(idx)] && _data[idx] > _data[GetRight(idx)]);
        }

        private void Resize()
        {
            var newArr = new int[_data.Length*2];
            Array.Copy(_data, newArr, _data.Length);
            _data = newArr;
        }

        private int GetParent(int i) => i / 2; //floor
        private int GetLeft(int i) => 2 * i;
        private int GetRight(int i) => 2 * i + 1;
    }
}
