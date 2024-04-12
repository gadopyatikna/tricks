using System;
namespace tricks.singlethreaded
{
    public abstract class SortBase
    {
        protected readonly int[] _array;

        public SortBase(int[] arr)
        {
            _array = arr;
        }

        public void Print() => Console.Write(String.Join(",", _array));

        protected void Swap(int idx1, int idx2)
        {
            var tmp = _array[idx1];
            _array[idx1] = _array[idx2];
            _array[idx2] = tmp;
        }
    }
}
